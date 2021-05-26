using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ShellApp.Identity.WebApi
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder endpoint)
        {
            return endpoint;
        }
    }
}
