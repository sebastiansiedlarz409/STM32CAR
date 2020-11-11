using CarMobileApp.Sender;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sensor : ContentPage
    {
        public Sensor()
        {
            InitializeComponent();
            BindingContext = new SensorViewModel(Navigation, new DataSender());
        }
    }
}