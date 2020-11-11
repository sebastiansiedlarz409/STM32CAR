using CarMobileApp._3D;
using CarMobileApp.Sender;
using Urho;
using Urho.Forms;
using Urho.Gui;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Model3D.Show<Render3D>(new Urho.ApplicationOptions(assetsFolder: null));
        }
    }
}