using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMobileApp
{
    public class BluetoothBLE
    {
        private readonly IBluetoothLE ble = CrossBluetoothLE.Current;
        private readonly IAdapter adapter = CrossBluetoothLE.Current.Adapter;

        public BluetoothBLE()
        {

        }

        public async Task<bool> Connect()
        {
            //list of all paired devices
            IReadOnlyList<IDevice> devices = adapter.GetSystemConnectedOrPairedDevices();

            //find car
            IDevice car = null;

            foreach (IDevice device in devices)
            {
                if (device.Name.Equals("stm32car"))
                {
                    car = device;
                    break;
                }
            }

            if (car is null)
                return false;

            adapter.ScanMode = ScanMode.Balanced;


            await adapter.DisconnectDeviceAsync(car);
            await adapter.ConnectToDeviceAsync(car);
            return true;
        }

        public void Send(byte[] data, int size)
        {

        }

        public bool IsConnected()
        {
            return false;
        }
    }
}
