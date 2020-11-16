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
            byte[] data = new byte[8];

            if(mode == SenderMode.ACCELEROMETER)
            {
                data[0] = (byte)'A';

                data[1] = x < 0 ? (byte)'-' : (byte)'+';
                data[2] = (byte)(x < 0 ? x * -1 : x);

                data[3] = y < 0 ? (byte)'-' : (byte)'+';
                data[4] = (byte)(y < 0 ? y * -1 : y);

                data[5] = z < 0 ? (byte)'-' : (byte)'+';
                data[6] = (byte)(z < 0 ? z * -1 : z);
            }
            else
            {
                data[0] = (byte)'B';

                data[1] = x < 0 ? (byte)'-' : (byte)'+';
                data[2] = (byte)(x < 0 ? x * -1 : x);

                data[3] = y < 0 ? (byte)'-' : (byte)'+';
                data[4] = (byte)(y < 0 ? y * -1 : y);

                data[5] = z < 0 ? (byte)'-' : (byte)'+';
                data[6] = (byte)(z < 0 ? z * -1 : z);
            }

            int sum = data[0] + data[1] + data[2] + data[3] + data[4] + data[5] + data[6];

            int checksum = sum % 256;

            data[7] = (byte)checksum;

            if (bt.IsConnected())
                bt.Send(data, 8);
        }
    }
}
