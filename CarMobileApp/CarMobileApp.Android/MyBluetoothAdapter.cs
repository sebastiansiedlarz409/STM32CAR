using System.Linq;
using Android.Bluetooth;
using Android.Runtime;
using CarMobileApp.Droid;
using CarMobileApp.Sender;
using Java.Lang;
using Java.Util;

[assembly: Xamarin.Forms.Dependency(typeof(MyBluetoothAdapter))]
namespace CarMobileApp.Droid
{
    public class MyBluetoothAdapter : IMyBluetoothAdapter
	{
		private string name = "stm32car";

		private BluetoothAdapter adapter;
		private BluetoothSocket BthSocket;

		private readonly UUID uuid = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

		public void Prepare()
        {
			adapter = BluetoothAdapter.DefaultAdapter;

			var devices = adapter.BondedDevices;

			BluetoothDevice device = devices.FirstOrDefault(t => t.Name.Equals(name));

			//some magic here :)
			var JavaMethods = device.JavaCast<BluetoothDevice>().Class.GetMethods();
			var crfsmethod = JavaMethods.FirstOrDefault(t => t.Name.Equals("createRfcommSocket"));
			BthSocket = (BluetoothSocket)crfsmethod.Invoke(device, new Java.Lang.Object[] { Integer.ValueOf(1) });

			if (!BthSocket.IsConnected)
            {
				BthSocket.Connect();
            }
		}

		public void Send(byte[] data, int size)
		{
			if (BthSocket.IsConnected)
            {
				var @out = BthSocket.OutputStream;
				@out.Write(data, 0, size);
			}
		}

		public bool IsConnected()
        {
			return BthSocket.IsConnected;
        }
	}
}