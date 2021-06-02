using IdentityModel;
using IdentityServer4.Models;

namespace ShellApp.Identity.Infrastructure
{
    public class ProfileWithRoleIdentityResource
        : IdentityResources.Profile
    {
        public ProfileWithRoleIdentityResource()
        {
            this.UserClaims.Add(JwtClaimTypes.Role);
        }
    }
}
