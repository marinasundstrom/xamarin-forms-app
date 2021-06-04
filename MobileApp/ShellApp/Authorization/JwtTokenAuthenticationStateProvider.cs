using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ShellApp.Authorization
{
    public class JwtTokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState authenticationState;
        private const string AuthTokenKey = "AuthToken";

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (authenticationState == null)
            {
                var authToken = await SecureStorage.GetAsync(AuthTokenKey);
                await UpdateAuthenticationState(authToken);
            }

            return authenticationState;
        }

        public async Task SetAuthTokenAsync(string authToken)
        {
            await UpdateAuthenticationState(authToken);

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        public async Task ClearAuthTokenAsync()
        {
            await UpdateAuthenticationState(null);

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        private async Task UpdateAuthenticationState(string authToken)
        {
            ClaimsPrincipal identity = null;

            if (authToken != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(authToken);
                var token = jsonToken as JwtSecurityToken;

                identity = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "Jwt"));

                await SecureStorage.SetAsync(AuthTokenKey, authToken);
            }
            else
            {
                identity = new ClaimsPrincipal(
                    new ClaimsIdentity());
                SecureStorage.Remove(AuthTokenKey);
            }

            authenticationState = new AuthenticationState(identity);
        }
    }
}

