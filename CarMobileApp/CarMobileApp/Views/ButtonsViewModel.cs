﻿using CarMobileApp.Sender;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Svg;

namespace CarMobileApp.Views
{
    public class ButtonsViewModel : INotifyPropertyChanged
    {
        private double Xvalue;
        private double Yvalue;
        private double Zvalue;

        //navigator to switch views
        private readonly INavigation navigation;

        //sender
        private readonly DataSender _sender;

        //event
        public event PropertyChangedEventHandler PropertyChanged;

        //commands, left, right, up, down, switch view
        public ICommand LeftCommand { get; }
        public ICommand RightCommand { get; }
        public ICommand ThrottleCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand SwitchViewCommand { get; }

        //images
        public ImageSource LeftImage { get; set; }
        public ImageSource RightImage { get; set; }
        public ImageSource ThrottleImage { get; set; }
        public ImageSource StopImage { get; set; }

        public ButtonsViewModel()
        {
            LeftCommand = new Command(Left);
            RightCommand = new Command(Right);
            ThrottleCommand = new Command(Throttle);
            StopCommand = new Command(Stop);
            SwitchViewCommand = new Command(async () => await Switch());

            //specify resource's path
            LeftImage = SvgImageSource.FromSvgResource("Images.left.svg");
            RightImage = SvgImageSource.FromSvgResource("Images.right.svg");
            ThrottleImage = SvgImageSource.FromSvgResource("Images.throttle.svg");
            StopImage = SvgImageSource.FromSvgResource("Images.stop.svg");

            X = 0;
            Y = 0;
            Z = 0;
        }

        public ButtonsViewModel(INavigation navigation, DataSender sender) : this()
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

        public void Left()
        {
            Y -= 0.2;

            _sender.SendData(SenderMode.BUTTONS, X, Y, Z);
        }

        public void Right()
        {
            Y += 0.2;

            _sender.SendData(SenderMode.BUTTONS, X, Y, Z);
        }

        public void Throttle()
        {
            Z += 0.1;

            _sender.SendData(SenderMode.BUTTONS, X, Y, Z);
        }

        public void Stop()
        {
            if (Z >= 0.1)
                Z -= 0.1;

            _sender.SendData(SenderMode.BUTTONS, X, Y, Z);
        }

        public async Task Switch()
        {
            await navigation.PushAsync(new Sensor());
        }
    }
}
