using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using ShellApp.Services;
using ShellApp.Views;
using ShellApp.Markup;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Client;
using ShellApp.ViewModels;
using System.Net.Http;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using ShellApp.Authorization;

namespace ShellApp
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:5020" : "https://localhost:5020";
        public static bool UseMockDataStore = false;
        private IHost host;

        public App()
        {
            InitializeComponent();

            var builder = new HostBuilder()
                .ConfigureServices(ConfigureServices);

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                builder.UseContentRoot(System.Environment.GetFolderPath(
                  System.Environment.SpecialFolder.Personal));
            }

            var host = builder.Build();

            ViewModelLocator.AppServiceProvider = host.Services;
            DIDataTemplate.AppServiceProvider = host.Services;

            AuthorizationView.AuthenticationStateProvider = host.Services.GetRequiredService<AuthenticationStateProvider>();

            //var authToken = await SecureStorage.GetAsync("authToken");

            MainPage = host.Services.GetService<LoginPage>();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddSingleton<JwtTokenAuthenticationStateProvider>();
            services.AddSingleton<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtTokenAuthenticationStateProvider>());

            services.AddScoped<CheckUnauthorizedHandler>();

            services.AddHttpClient<IItemsClient>((provider, client) =>
                {
                    client.BaseAddress = new Uri(AzureBackendUrl);
                })
                .AddTypedClient<IItemsClient>((http, sp) => new ItemsClient(http)
                {
                    RetrieveAuthorizationToken = () => SecureStorage.GetAsync("AuthToken")
                })
            .ConfigurePrimaryHttpMessageHandler(GetInsecureHandler)
            .AddHttpMessageHandler<CheckUnauthorizedHandler>();

            if (UseMockDataStore)
                services.AddSingleton<IItemsDataService<Item>, MockItemsDataService>();
            else
                services.AddSingleton<IItemsDataService<Item>, ItemsDataService>();

            services.AddSingleton<IMessageBus>(MessageBus.Instance);

            services.AddSingleton<AppShell>();

            services.AddSingleton<AboutPage>();
            services.AddSingleton<AboutViewModel>();

            services.AddTransient<ItemDetailPage>();
            services.AddTransient<ItemDetailViewModel>();

            services.AddSingleton<ItemsPage>();
            services.AddSingleton<ItemsViewModel>();

            services.AddTransient<LoginPage>();
            services.AddTransient<LoginViewModel>();

            services.AddTransient<NewItemPage>();
            services.AddTransient<NewItemViewModel>();

            services.AddSingleton<Application>(this);

            services.AddTransient<IItemsNotificationService>(sp =>
            {
                var connection = new HubConnectionBuilder()
                .WithUrl($"{AzureBackendUrl}/hubs/items", (options) =>
                {
                    options.AccessTokenProvider = () => SecureStorage.GetAsync("AuthToken");

                    options.HttpMessageHandlerFactory = (message) =>
                    {
                        if (message is HttpClientHandler clientHandler)
                            // bypass SSL certificate
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        return message;
                    };
                })
                .WithAutomaticReconnect()
                .Build();

                return new ItemsNotificationService(connection);
            });
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        // This method must be in a class in a platform project, even if
        // the HttpClient object is constructed in a shared project.
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost"))
                        return true;
                    return errors == System.Net.Security.SslPolicyErrors.None;
                }
            };
            return handler;
        }
    }
}
