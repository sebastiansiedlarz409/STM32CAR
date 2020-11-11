using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarMobileApp
{
    [ContentProperty(nameof(Source))]
    public class ImageHelperExtension : IMarkupExtension
    {
        //path
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source is { })
                return ImageSource.FromResource(Source, typeof(ImageHelperExtension));

            return null;
        }
    }
}
