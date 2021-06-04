using System.Security.Claims;

namespace ShellApp.Authorization
{
    public class AuthenticationState
    {
        public ClaimsPrincipal User { get; }

        public AuthenticationState(ClaimsPrincipal user)
        {
            User = user;
        }
    }
}

