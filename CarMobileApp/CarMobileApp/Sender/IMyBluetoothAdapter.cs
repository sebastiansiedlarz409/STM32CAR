using System.Threading.Tasks;

namespace CarMobileApp.Sender
{
    public interface IMyBluetoothAdapter
    {
        public void Prepare();
        public void Send();
    }
}
