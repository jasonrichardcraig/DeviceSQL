using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceSQL.TestHarness
{
    class TcpChannelTest
    {
        public static void Test()
        {




            DeviceSQL.Functions.ChannelManager.ChannelManager_RegisterTcpChannel("tcp://96.53.12.52:4000", "96.53.12.52", 4000, 5, 5000, 5000);

            DeviceSQL.Functions.DeviceManager.DeviceManager_RegisterROCMaster("tcp://96.53.12.52:4000", "FB103-01", 1, 2, 3, 1, 5, 200, 0, 0);

            var index = 0;

            while (1000 > index)
            {
                var deviceDateTime = DeviceSQL.Functions.ROCMaster.ROCMaster_GetRealTimeClockValueWithCentury("FB103-01", 2000);
            }

            DeviceSQL.Functions.DeviceManager.DeviceManager_UnregisterDevice("FB103-01");

            DeviceSQL.Functions.ChannelManager.ChannelManager_UnregisterChannel("tcp://96.53.12.52:4000");

            index++;
        }
    }
}
