using CarMobileApp.Sender;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Buttons : ContentPage
    {
        private readonly DataSender dataSender;
        public Buttons()
        {
            InitializeComponent();

            dataSender = DataSender.GetSingleInstance();

            BindingContext = new ButtonsViewModel(Navigation, dataSender);

            Label.BackgroundColor = new Color(1, 1, 1, 0.5);
            Switch.BackgroundColor = new Color(1, 1, 1, 0.5);
        }
    }
}