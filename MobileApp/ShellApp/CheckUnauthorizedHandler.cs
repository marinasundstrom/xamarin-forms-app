using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Views;
using Xamarin.Essentials;
using Microsoft.Extensions.Logging;

namespace ShellApp
{
    public class CheckUnauthorizedHandler : DelegatingHandler
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<CheckUnauthorizedHandler> logger;

        public CheckUnauthorizedHandler(IServiceProvider serviceProvider, ILogger<CheckUnauthorizedHandler> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // base.SendAsync calls the inner handler
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                try
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                        SecureStorage.Remove("AuthToken"));

                    var app = serviceProvider.GetService<Application>();
                    app.MainPage = serviceProvider.GetService<LoginPage>();
                }
                catch(Exception e)
                {
                    logger.LogError(e, "Error");
                }
            }

            return response;
        }
    }
}