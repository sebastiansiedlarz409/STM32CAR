using System;
using System.Collections.Generic;
using System.Linq;
using Android.Bluetooth;
using Android.Runtime;
using CarMobileApp.Droid;
using CarMobileApp.Sender;
using Java.Lang;

[assembly: Xamarin.Forms.Dependency(typeof(MyBluetoothAdapter))]
namespace CarMobileApp.Droid
{
    public class MyBluetoothAdapter : IMyBluetoothAdapter
	{
		private string name = "stm32car";

		private BluetoothAdapter adapter;
		private BluetoothSocket BthSocket;

		public bool Prepare()
        {
            try
            {
				Connect();
				return true;
            }
            catch (Java.IO.IOException)
            {
				return false;
            }
            catch (NullReferenceException)
            {
				return false;
            }
        }

		private void Connect()
        {
			adapter = BluetoothAdapter.DefaultAdapter;

			//all bonded devices
			List<BluetoothDevice> devices = adapter.BondedDevices.ToList();

			//find device by name
			BluetoothDevice device = devices.FirstOrDefault(t => t.Name.Equals(name));

			//find createRfcommSocket using reflections and call it
			var deviceMethods = device.JavaCast<BluetoothDevice>().Class.GetMethods();
			var createRfcommSocketMethod = deviceMethods.FirstOrDefault(t => t.Name.Equals("createRfcommSocket"));
			BthSocket = (BluetoothSocket)createRfcommSocketMethod.Invoke(device, new Java.Lang.Object[] { Integer.ValueOf(1) });

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
            try
            {
				return BthSocket.IsConnected;
			}
            catch (NullReferenceException)
            {
				return false;
            }
        }
	}
}