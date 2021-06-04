using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Views;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using IdentityModel.OidcClient;
using System.Net.Http;
using IdentityModel.Client;
using System.Threading.Tasks;
using IdentityModel;
using ShellApp.Authorization;

namespace ShellApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly IServiceProvider serviceProvider;
        private readonly JwtTokenAuthenticationStateProvider authenticationStateProvider;
        private string name;

        public AboutViewModel(IServiceProvider serviceProvider, JwtTokenAuthenticationStateProvider authenticationStateProvider)
        {
            this.serviceProvider = serviceProvider;
            this.authenticationStateProvider = authenticationStateProvider;
            Title = "About";

            OpenWebCommand = new Command(async () => await Xamarin.Essentials.Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

            LogoutCommand = new Command(async () =>
            {
                await authenticationStateProvider.ClearAuthTokenAsync();

                await Logout();

                var app = serviceProvider.GetService<Application>();
                app.MainPage = serviceProvider.GetService<LoginPage>();
            });
        }

        private async Task Logout()
        {
            var options = new OidcClientOptions
            {
                Authority = Constants.AuthorityUri,
                ClientId = Constants.ClientId,
                Scope = Constants.Scope,
                RedirectUri = Constants.RedirectUri,
                //ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
                Browser = new WebAuthenticatorBrowser(),

                BackchannelHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                    {
                        if (cert.Issuer.Equals("CN=localhost"))
                            return true;
                        return errors == System.Net.Security.SslPolicyErrors.None;
                    }
                },

                //Debug
                Policy = new Policy { Discovery = new DiscoveryPolicy { RequireHttps = false } },
            };

            var _oidcClient = new OidcClient(options);

            try
            {
                var _logoutResult = await _oidcClient.LogoutAsync(new LogoutRequest());

                if (_logoutResult.IsError)
                {
                    Console.WriteLine("ERROR: {0}", _logoutResult.Error);
                    return;
                }
            }
            catch(Exception)
            {

            }
        }

        public async void OnAppearing()
        {
            var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var claims = authenticationState.User.Claims;

            Name = claims.FirstOrDefault(claim => claim.Type == "firstname" /* JwtClaimTypes.Name */)?.Value;
        }

        public string Name { get => name; set => SetProperty(ref name, value); }

        public ICommand OpenWebCommand { get; }

        public ICommand LogoutCommand { get; }
    }
}
