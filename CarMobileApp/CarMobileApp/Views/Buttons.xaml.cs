using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Buttons : ContentPage
    {
        public Buttons()
        {
            InitializeComponent();

            BindingContext = new ButtonsViewModel(Navigation);
        }
    }
}