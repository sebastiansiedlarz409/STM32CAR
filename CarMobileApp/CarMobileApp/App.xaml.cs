using CarMobileApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Svg;

namespace CarMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SvgImageSource.RegisterAssembly();

            MainPage = new NavigationPage(new Sensor());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
