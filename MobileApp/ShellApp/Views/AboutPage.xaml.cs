using System;
using System.ComponentModel;
using ShellApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShellApp.Views
{
    public partial class AboutPage : ContentPage
    {
        private AboutViewModel _aboutViewModel;

        public AboutPage(AboutViewModel aboutViewModel)
        {
            InitializeComponent();
            BindingContext = _aboutViewModel = aboutViewModel;
        }

        protected override void OnAppearing()
        {
            _aboutViewModel.OnAppearing();
        }
    }
}
