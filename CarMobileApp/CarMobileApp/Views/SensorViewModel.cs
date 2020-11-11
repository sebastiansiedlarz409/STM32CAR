using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using CarMobileApp.Sender;

namespace CarMobileApp.Views
{
    public class SensorViewModel : INotifyPropertyChanged
    {
        private double Xvalue = 0;
        private double Yvalue = 0;
        private double Zvalue = 0;

        //navigator to switch views
        private readonly INavigation navigation;

        //sender
        private readonly DataSender _sender;

        //event
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SwitchViewCommand { get; }

        public SensorViewModel()
        {
            SwitchViewCommand = new Command(async () => await Switch());

            if(!Accelerometer.IsMonitoring)
                Accelerometer.Start(SensorSpeed.Game);

            Accelerometer.ReadingChanged += SensorUpdateEvent;
        }

        public SensorViewModel(INavigation navigation, DataSender sender) : this()
        {
            this.navigation = navigation;
            _sender = sender;
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
                return $"X: {String.Format("{0:0.00}", X)} Y: {String.Format("{0:0.00}", Y)} Z: {String.Format("{0:0.00}", Z)}";
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

        public void SensorUpdateEvent(object sender, AccelerometerChangedEventArgs e)
        {
            X = e.Reading.Acceleration.X;
            Y = e.Reading.Acceleration.Y;
            Z = e.Reading.Acceleration.Z;

            _sender.SendData(SenderMode.ACCELEROMETER, X, Y, Z);
        }

        public async Task Switch()
        {
            Accelerometer.Stop();
            await navigation.PushAsync(new Buttons());
        }

    }
}
