using Microsoft.Extensions.DependencyInjection;

namespace ShellApp.Items.WebApi
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddItemsControllers(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder
                .AddApplicationPart(typeof(MvcBuilderExtensions).Assembly)
                .AddControllersAsServices();

            return mvcBuilder;
        }
    }
}
