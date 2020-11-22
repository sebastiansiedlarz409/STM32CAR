using Plugin.BluetoothLE;
using System;
using System.Diagnostics;
using System.Reactive.Linq;

namespace CarMobileApp
{
    public class ACRBluetoothLE
    {
        private IDevice car = null;
        private bool connected = false;

        private IGattCharacteristic writeCharacteristic;

        public bool Connect()
        {
            CrossBleAdapter.Current.GetPairedDevices().Subscribe(result =>
            {
                foreach (IDevice device in result)
                {
                    if (device.Name.Equals("stm32car"))
                    {
                        Debug.WriteLine("Car found");

                        car = device;
                        break;
                    }
                }

                car.Connect();
            });

            car.WhenAnyCharacteristicDiscovered().Subscribe(characteristic =>
            {
                if (characteristic.CanWrite())
                {
                    Debug.WriteLine("Write characteristic found");

                    writeCharacteristic = characteristic;
                }
            });

            connected = car.Status == ConnectionStatus.Connected;
            return connected;
        }

        public void Send(byte[] data, int size)
        {
            if (writeCharacteristic is { } && IsConnected())
            {
                writeCharacteristic.Write(data).Subscribe(result => 
                {
                    Debug.WriteLine($"Send {size} bytes");
                });
            }
        }

        public bool IsConnected()
        {
            return connected;
        }
    }
}
