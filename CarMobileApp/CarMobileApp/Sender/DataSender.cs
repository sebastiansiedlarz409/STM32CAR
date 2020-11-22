namespace CarMobileApp.Sender
{
    public class DataSender
    {
        private static readonly DataSender _instance = new DataSender();

        private readonly ACRBluetoothLE bt;

        public static DataSender GetSingleInstance()
        {
            return _instance;
        }

        private DataSender()
        {
            bt = new ACRBluetoothLE();
        }

        public void Connect()
        {
            bt.Connect();
        }

        public void SendData(SenderMode mode, int x, int y, int z)
        {
            byte[] data = new byte[9];

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

            data[7] = 0;
            data[8] = 0;

            byte[] result = FletcherCheksum.CalculateChecksum(data);

            data[7] = result[0];
            data[8] = result[1];

            if (bt.IsConnected())
                bt.Send(data, 9);
            else
                bt.Connect();
        }

        public bool IsConnected()
        {
            return bt.IsConnected();
        }
    }
}
