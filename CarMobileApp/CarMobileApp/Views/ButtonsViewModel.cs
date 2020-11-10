using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarMobileApp.Views
{
    public class ButtonsViewModel: INotifyPropertyChanged
    {
        private double X;
        private double Y;
        private double Z;

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
            SwitchViewCommand = new Command(Switch);
        }

        //this fuction notify property
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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

        public void Switch()
        {

        }

    }
}
