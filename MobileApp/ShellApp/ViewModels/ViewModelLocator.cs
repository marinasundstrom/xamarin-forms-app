using System;
using Microsoft.Extensions.DependencyInjection;

namespace ShellApp.ViewModels
{
    public static class ViewModelLocator
    {
        public static IServiceProvider AppServiceProvider { set; get; }

        public static ItemDetailViewModel ItemDetailViewModel => AppServiceProvider.GetService<ItemDetailViewModel>();

        public static NewItemViewModel NewItemViewModel => AppServiceProvider.GetService<NewItemViewModel>();
    }
}
