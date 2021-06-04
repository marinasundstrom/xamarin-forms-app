using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Xamarin.Forms;

namespace ShellApp.Authorization
{
    public class AuthorizationView : ContentView
    {
        public static BindableProperty RoleProperty = BindableProperty.Create(nameof(Role), typeof(string), typeof(AuthorizationView), null, propertyChanged: OnPropertyChanged);

        public static BindableProperty AuthorizedTemplateProperty = BindableProperty.Create(nameof(AuthorizedTemplate), typeof(DataTemplate), typeof(AuthorizationView), null, propertyChanged: OnPropertyChanged);

        public static BindableProperty UnauthorizedTemplateProperty = BindableProperty.Create(nameof(UnauthorizedTemplate), typeof(DataTemplate), typeof(AuthorizationView), null, propertyChanged: OnPropertyChanged);

        private static async void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is AuthorizationView authorizationView)
            {
                await CheckAuthorized(authorizationView);
            }
        }

        public AuthorizationView()
        {
            AuthenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;

            Initialize();
        }

        private async void Initialize()
        {
            await CheckAuthorized(this);
        }

        private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            var authenticationState = await task;
            CheckAuthorized(authenticationState);
        }

        public string Role
        {
            get => (string)GetValue(RoleProperty);
            set => SetValue(RoleProperty, value);
        }

        public DataTemplate AuthorizedTemplate
        {
            get => (DataTemplate)GetValue(AuthorizedTemplateProperty);
            set => SetValue(AuthorizedTemplateProperty, value);
        }

        public DataTemplate UnauthorizedTemplate
        {
            get => (DataTemplate)GetValue(UnauthorizedTemplateProperty);
            set => SetValue(UnauthorizedTemplateProperty, value);
        }

        public new View Content
        {
            get => base.Content;
            set => throw new NotSupportedException("Direct content is not supported.");
        }

        private static async Task CheckAuthorized(AuthorizationView authorizationView)
        {
            var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            authorizationView.CheckAuthorized(authenticationState);
        }

        private void CheckAuthorized(AuthenticationState authenticationState)
        {
            var user = authenticationState.User;

            bool authorized = false;

            if (user.Identity.IsAuthenticated)
            {
                if (Role != null)
                {
                    var roles = Role.Split(',').Select(x => x.Trim());

                    authorized = roles.Any(role => authenticationState.User.HasClaim(JwtClaimTypes.Role, role));
                }
                else
                {
                    authorized = true;
                }
            }

            base.Content = (View)(authorized ? AuthorizedTemplate?.CreateContent()
                : UnauthorizedTemplate?.CreateContent());

            this.ForceLayout();
        }

        public static AuthenticationStateProvider AuthenticationStateProvider;
    }
}

