using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarMobileApp.Views
{
    public class SensorViewModel : INotifyPropertyChanged
    {
        private double X = 0;
        private double Y = 0;
        private double Z = 0;

        private INavigation navigation;

        //event
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SwitchViewCommand { get; }

        public SensorViewModel()
        {
            SwitchViewCommand = new Command(async () => await Switch());
        }

        public SensorViewModel(INavigation navigation) : this()
        {
            this.navigation = navigation;
        }

        //this fuction notify property
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string PrintValues
        {
            get
            {
                return $"X: {X} Y: {Y} Z: {Z}";
            }
        }

        public async Task Switch()
        {
            await navigation.PushAsync(new Buttons());
        }

    }
}
