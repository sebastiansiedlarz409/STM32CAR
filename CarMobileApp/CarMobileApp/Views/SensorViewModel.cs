﻿using System;
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
        private float Xvalue = 0;
        private float Yvalue = 0;
        private float Zvalue = 0;

        private bool connection;

        private int counterSend = 0;

        //navigator to switch views
        private readonly INavigation navigation;

        //sender
        private readonly DataSender _sender;

        //event
        public event PropertyChangedEventHandler PropertyChanged;

        //render object setter for x,y,z
        public Action<float, float, float> SetRotation;

        public ICommand SwitchViewCommand { get; }

        public SensorViewModel()
        {
            SwitchViewCommand = new Command(async () => await Switch());

            if(!Accelerometer.IsMonitoring)
                Accelerometer.Start(SensorSpeed.Default);

            Accelerometer.ReadingChanged += SensorUpdateEvent;
        }

        public SensorViewModel(INavigation navigation, DataSender sender) : this()
        {
            this.navigation = navigation;
            _sender = sender;

            _sender.Connect();
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
                return $"X: {String.Format("{0:0.00}", X)} Y: {String.Format("{0:0.00}", Y)} Z: {String.Format("{0:0.00}", Z)} {ConnectionInfo}";
            }
        }

        public float X
        {
            get => Xvalue;
            set
            {
                Xvalue = value;
                OnPropertyChanged(nameof(PrintValues));
            }
        }

        public float Y
        {
            get => Yvalue;
            set
            {
                Yvalue = value;
                OnPropertyChanged(nameof(PrintValues));
            }
        }

        public float Z
        {
            get => Zvalue;
            set
            {
                Zvalue = value;
                OnPropertyChanged(nameof(PrintValues));
            }
        }

        public string ConnectionInfo
        {
            get => connection ? "Połączono" : "Brak połączenia";
        }

        public bool Connection
        {
            set
            {
                connection = value;
                OnPropertyChanged(nameof(PrintValues));
            }
        }

        public void SensorUpdateEvent(object sender, AccelerometerChangedEventArgs e)
        {
            Connection = _sender.IsConnected();

            X = e.Reading.Acceleration.X;
            Y = e.Reading.Acceleration.Y;
            Z = e.Reading.Acceleration.Z;

            if (SetRotation is { })
                SetRotation(X * 10, Y * 10, Z * 10);

            if (counterSend == 30)
            {
                if (Z > 0.6)
                    Z = 0.6f;

                if (Z < -0.6)
                    Z = -0.6f;

                _sender.SendData(SenderMode.ACCELEROMETER, (int)(X * 10), (int)(Y * 15), (int)(Z * 20));

                counterSend = 0;
            }
            else
            {
                counterSend++;
            }
            
        }

        public async Task Switch()
        {
            Accelerometer.Stop();

            _sender.SendData(SenderMode.ACCELEROMETER, 0, 0, 0);

            await navigation.PushAsync(new Buttons());
        }

    }
}
