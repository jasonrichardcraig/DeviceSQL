using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;

namespace DeviceSQL.Functions
{
    public partial class Watchdog
    {
        [SqlFunction]
        public static SqlInt32 GetCounterValue()
        {
            try
            {
                return new SqlInt32(DeviceSQL.Watchdog.Worker.WatchdogCounter);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error getting watchdog counter value: {0}", ex.Message));
            }
            return 0;
        }
    }
}
