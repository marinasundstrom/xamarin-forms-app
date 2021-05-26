using Microsoft.Extensions.DependencyInjection;
using ShellApp.Identity.UI;

namespace ShellApp.Identity.WebApi
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddIdentityControllers(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder
                .AddApplicationPart(typeof(MvcBuilderExtensions).Assembly)
                .AddIdentityUI()
                .AddControllersAsServices();

            return mvcBuilder;
        }
    }
}
