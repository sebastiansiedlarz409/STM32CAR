using CarMobileApp;
using CarMobileApp.Sender;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace CarMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sensor : ContentPage
    {
        private readonly DataSender dataSender;

        public Sensor()
        {
            InitializeComponent();

            dataSender = DataSender.GetSingleInstance();

            BindingContext = new SensorViewModel(Navigation, dataSender);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var render = await Model3D.Show<Render3D>(new Urho.ApplicationOptions(assetsFolder: "Data"));

            //xdd
            ((SensorViewModel)BindingContext).SetRotation = render.SetRotation;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}