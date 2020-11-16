namespace CarMobileApp.Sender
{
    public interface IMyBluetoothAdapter
    {
        public bool Prepare();
        public void Send(byte[] data, int size);

        public bool IsConnected();
    }
}
