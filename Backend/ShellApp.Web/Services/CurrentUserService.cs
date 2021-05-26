using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using ShellApp.Application.Common.Interfaces;

namespace ShellApp.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Subject);
    }
}
