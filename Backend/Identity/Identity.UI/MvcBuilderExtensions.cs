using Microsoft.Extensions.DependencyInjection;

namespace ShellApp.Identity.UI
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddIdentityUI(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder
                .AddApplicationPart(typeof(IdentityServerHost.Quickstart.UI.AccountController).Assembly)
                .AddControllersAsServices();

            return mvcBuilder;
        }
    }
}
