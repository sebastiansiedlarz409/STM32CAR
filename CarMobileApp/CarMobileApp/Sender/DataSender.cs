using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarMobileApp.Sender
{
    public class DataSender
    {
        private static readonly DataSender _instance = new DataSender();

        private readonly IMyBluetoothAdapter bt;

        public static DataSender GetSingleInstance()
        {
            return _instance;
        }

        private DataSender()
        {
            bt = DependencyService.Get<IMyBluetoothAdapter>();
            bt.Prepare();
        }

        public void SendData(SenderMode mode, double x, double y, double z)
        {
            //if android
            bt.Send();
        }
    }
}
