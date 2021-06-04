using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;

namespace ShellApp.Identity.Infrastructure
{
    public class ProfileWithRoleIdentityResource
        : IdentityResources.Profile
    {
        public ProfileWithRoleIdentityResource()
        {
            this.UserClaims.Add(ClaimTypes.Role);
        }
    }
}
