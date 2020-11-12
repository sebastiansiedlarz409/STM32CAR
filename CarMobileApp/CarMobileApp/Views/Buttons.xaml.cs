using CarMobileApp.Sender;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Buttons : ContentPage
    {
        private DataSender dataSender;

        public Buttons()
        {
            InitializeComponent();

            dataSender = DataSender.GetSingleInstance();

            BindingContext = new ButtonsViewModel(Navigation, dataSender);
        }
    }
}