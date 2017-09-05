#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;

#endregion

namespace DeviceSQL.Functions
{
    public partial class Watchdog
    {
        [SqlFunction]
        public static SqlInt32 Watchdog_GetCounterValue()
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
