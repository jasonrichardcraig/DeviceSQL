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
        public static SqlBoolean IsAlive()
        {
            try
            {
                return new SqlBoolean(DeviceSQL.Watchdog.Worker.IsAlive);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error getting watchdog IsAlive value: {0}", ex.Message));
            }
            return false;
        }
    }
}
