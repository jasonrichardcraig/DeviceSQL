using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceSQL.TestHarness
{
    class SerialPortChannelTest
    {
        public static void Test()
        {
            using (var serialPortChannel = new IO.Channels.SerialPortChannel()
            {
                Name = "com1://localhost",
                ReadTimeout = 5000,
                WriteTimeout = 5000,
                TracingEnabled = true
            })
            {
                var rocMaster = new Device.Roc.RocMaster(serialPortChannel)
                {
                    Name = "FB103-01",
                    DeviceAddress = 1,
                    DeviceGroup = 2,
                    HostAddress = 3,
                    HostGroup = 1
                };

                rocMaster.Transport.NumberOfRetries = 3;

                serialPortChannel.SerialPort.BaudRate = 19200;
                serialPortChannel.SerialPort.DataBits = 8;
                serialPortChannel.SerialPort.Parity = System.IO.Ports.Parity.None;
                serialPortChannel.SerialPort.StopBits = System.IO.Ports.StopBits.One;
                serialPortChannel.SerialPort.Handshake = System.IO.Ports.Handshake.None;

                serialPortChannel.SerialPort.Open();

                var deviceRealTimeClockValue = rocMaster.GetRealTimeClockValue(null, null, null, null, 2000);

                serialPortChannel.SerialPort.Close();

            }
        }
    }
}
