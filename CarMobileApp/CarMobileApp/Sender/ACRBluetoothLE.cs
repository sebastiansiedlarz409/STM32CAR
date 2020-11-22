using Plugin.BluetoothLE;
using System;
using System.Diagnostics;
using System.Reactive.Linq;

namespace CarMobileApp.Sender
{
    public class ACRBluetoothLE
    {
        private IDevice car = null;

        private IGattCharacteristic writeCharacteristic;

        public void Connect()
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

                if (car is null)
                    return;

                car.Connect();

            });

            if (car is null)
                return;

            car.WhenAnyCharacteristicDiscovered().Subscribe(characteristic =>
            {
                if (characteristic.CanWrite())
                {
                    Debug.WriteLine("Write characteristic found");

                    writeCharacteristic = characteristic;
                }
            });
        }

        public void Disconnect()
        {
            car.CancelConnection();
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
            if (car is null)
                return false;

            return car.Status == ConnectionStatus.Connected ? true : false;
        }
    }
}
