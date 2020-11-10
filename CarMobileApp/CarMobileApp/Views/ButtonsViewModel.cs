﻿using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarMobileApp.Views
{
    public class ButtonsViewModel : INotifyPropertyChanged
    {
        private double X = 0;
        private double Y = 0;
        private double Z = 0;

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
