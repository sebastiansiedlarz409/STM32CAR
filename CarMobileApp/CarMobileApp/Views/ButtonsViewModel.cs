using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarMobileApp.Views
{
    public class ButtonsViewModel : INotifyPropertyChanged
    {
        private double Xvalue;
        private double Yvalue;
        private double Zvalue;

        private INavigation navigation;

        //event
        public event PropertyChangedEventHandler PropertyChanged;

        //commands, left, right, up, down, switch view
        public ICommand LeftCommand { get; }
        public ICommand RightCommand { get; }
        public ICommand UpCommand { get; }
        public ICommand DownCommand { get; }
        public ICommand SwitchViewCommand { get; }

        public ButtonsViewModel()
        {
            LeftCommand = new Command(Left);
            RightCommand = new Command(Right);
            UpCommand = new Command(Up);
            DownCommand = new Command(Down);
            SwitchViewCommand = new Command(async () => await Switch());

            X = 0;
            Y = 0;
            Z = 0;
        }

        public ButtonsViewModel(INavigation navigation) : this()
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

        public double X 
        { 
            get => Xvalue;
            set
            {
                Xvalue = value;
                OnPropertyChanged(nameof(PrintValues));
            }
        }

        public double Y 
        { 
            get => Yvalue;
            set
            {
                Yvalue = value;
                OnPropertyChanged(nameof(PrintValues));
            }
        }

        public double Z
        {
            get => Zvalue;
            set
            {
                Zvalue = value;
                OnPropertyChanged(nameof(PrintValues));
            }
        }

        public void Left()
        {

        }

        public void Right()
        {

        }

        public void Up()
        {

        }

        public void Down()
        {

        }

        public async Task Switch()
        {
            await navigation.PushAsync(new Sensor());
        }

    }
}
