using System;
using System.Collections.Generic;
using ShellApp.ViewModels;
using ShellApp.Views;
using Xamarin.Forms;

namespace ShellApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
