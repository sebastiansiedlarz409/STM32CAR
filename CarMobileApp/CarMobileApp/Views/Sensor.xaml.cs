using CarMobileApp._3D;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var render = await Model3D.Show<Render3D>(new Urho.ApplicationOptions(assetsFolder: null));

            //xdd
            ((SensorViewModel)BindingContext).SetRotation = render.SetRotation;
        }
    }
}