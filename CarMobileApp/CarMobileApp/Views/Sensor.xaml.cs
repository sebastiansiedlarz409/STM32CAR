using CarMobileApp.Sender;
using Xamarin.Forms;
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

            Label.BackgroundColor = new Color(1, 1, 1, 0.5);
            Switch.BackgroundColor = new Color(1, 1, 1, 0.5);
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