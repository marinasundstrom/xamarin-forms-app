using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ShellApp.Items.WebApi.Hubs;

namespace ShellApp.Items.WebApi
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapItemsEndpoints(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapHub<ItemsHub>("/hubs/items");

            return endpoint;
        }
    }
}
