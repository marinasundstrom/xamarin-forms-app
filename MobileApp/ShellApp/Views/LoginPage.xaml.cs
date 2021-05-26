using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShellApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShellApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel _loginViewModel;

        public LoginPage(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            BindingContext = _loginViewModel = loginViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _loginViewModel.OnAppearing();
        }
    }
}
