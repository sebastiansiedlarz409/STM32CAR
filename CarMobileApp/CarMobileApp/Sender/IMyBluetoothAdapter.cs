namespace CarMobileApp.Sender
{
    public interface IMyBluetoothAdapter
    {
        public void Prepare();
        public void Send(byte[] data, int size);

        public bool IsConnected();
    }
}
