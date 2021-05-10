using System;
using System.ComponentModel;
using ShellApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShellApp.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage(AboutViewModel aboutViewModel)
        {
            InitializeComponent();
            BindingContext = aboutViewModel;
        }
    }
}
