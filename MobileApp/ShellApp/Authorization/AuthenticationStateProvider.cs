using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShellApp.Authorization
{
    public abstract class AuthenticationStateProvider 
    {
        public abstract Task<AuthenticationState> GetAuthenticationStateAsync();

        protected void NotifyAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            AuthenticationStateChanged?.Invoke(task);
        }

        public event AuthenticationStateChangedHandler AuthenticationStateChanged;
    }

    public delegate void AuthenticationStateChangedHandler(Task<AuthenticationState> task);
}

