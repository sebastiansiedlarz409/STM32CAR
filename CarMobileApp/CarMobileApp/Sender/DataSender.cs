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

        public void SendData(SenderMode mode, int x, int y, int z)
        {
            byte[] data = new byte[7];

            if(mode == SenderMode.ACCELEROMETER)
            {
                data[0] = (byte)'A';

                data[1] = x < 0 ? (byte)'-' : (byte)'+';
                data[2] = (byte)(x + 65);

                data[3] = y < 0 ? (byte)'-' : (byte)'+';
                data[4] = (byte)(y + 65);

                data[5] = z < 0 ? (byte)'-' : (byte)'+';
                data[6] = (byte)(z + 65);
            }
            else
            {
                data[0] = (byte)'B';

                data[1] = x < 0 ? (byte)'-' : (byte)'+';
                data[2] = (byte)(x + 65);

                data[3] = y < 0 ? (byte)'-' : (byte)'+';
                data[4] = (byte)(y + 65);

                data[5] = z < 0 ? (byte)'-' : (byte)'+';
                data[6] = (byte)(z + 65);
            }

            if (bt.IsConnected())
                bt.Send(data, 7);
        }
    }
}
