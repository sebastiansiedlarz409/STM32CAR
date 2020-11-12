using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Threading.Tasks;

namespace CarMobileApp.Sender
{
    public class DataSender
    {
        private static readonly DataSender _instance = new DataSender();

        public static DataSender GetSingleInstance()
        {
            return _instance;
        }

        private IBluetoothLE BLE = CrossBluetoothLE.Current;
        private IAdapter adapter = CrossBluetoothLE.Current.Adapter;

        private DataSender()
        {
            //device discovered event
            adapter.DeviceDiscovered += (s, a) => { DeviceDiscovered(s, a); };
        }

        public async Task StartScanning()
        {
            var state = BLE.State;

            adapter.ScanMode = ScanMode.Balanced;
            await adapter.StartScanningForDevicesAsync();

            state = BLE.State;
        }

        public bool IsScanning()
        {
            return adapter.IsScanning;
        }

        private void DeviceDiscovered(object s, DeviceEventArgs a)
        {
            
        }

        public void SendData(SenderMode mode, double x, double y, double z)
        {

        }
    }
}
