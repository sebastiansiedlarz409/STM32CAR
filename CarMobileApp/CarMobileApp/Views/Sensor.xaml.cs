using CarMobileApp._3D;
using CarMobileApp.Sender;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sensor : ContentPage
    {
        private DataSender dataSender;

        public Sensor()
        {
            InitializeComponent();

            dataSender = DataSender.GetSingleInstance();

            BindingContext = new SensorViewModel(Navigation, dataSender);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //enable scannning bt
            if (!dataSender.IsScanning())
            {
                await dataSender.StartScanning();
            }

            var render = await Model3D.Show<Render3D>(new Urho.ApplicationOptions(assetsFolder: null));

            //xdd
            ((SensorViewModel)BindingContext).SetRotation = render.SetRotation;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}