using IdentityModel.Client;
using IdentityModel.OidcClient;
using ShellApp.Views;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShellApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private OidcClient _oidcClient;
        private LoginResult _loginResult;

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        public async void OnAppearing()
        {
            var authToken = await SecureStorage.GetAsync("AuthToken");
            if(authToken != null)
            {
                await GoToAboutPage();
            }
        }

        private async void OnLoginClicked(object obj)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost"))
                        return true;
                    return errors == System.Net.Security.SslPolicyErrors.None;
                };

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

            _oidcClient = new OidcClient(options);
            _loginResult = await _oidcClient.LoginAsync(new LoginRequest());

            if (_loginResult.IsError)
            {
                Console.WriteLine("ERROR: {0}", _loginResult.Error);
                return;
            }

            await SecureStorage.SetAsync("AuthToken", _loginResult?.AccessToken);

            await GoToAboutPage();
        }

        private static async System.Threading.Tasks.Task GoToAboutPage()
        {
            App.Current.MainPage = new AppShell();

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
