using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShellApp.Markup
{
    public class DIDataTemplate : IMarkupExtension<DataTemplate>
    {
        public static IServiceProvider AppServiceProvider { set; get; }

        public Type Type { set; get; }

        public DataTemplate ProvideValue(IServiceProvider serviceProvider)
        {
            return new DataTemplate(() => AppServiceProvider.GetService(Type));
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<DataTemplate>).ProvideValue(serviceProvider);
        }
    }
}
