using CarMobileApp.Sender;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Buttons : ContentPage
    {
        private readonly DataSender dataSender;

        public Buttons(BluetoothBLE ble = null)
        {
            InitializeComponent();

            dataSender = DataSender.GetSingleInstance();

            BindingContext = new ButtonsViewModel(Navigation, dataSender);
        }
    }
}