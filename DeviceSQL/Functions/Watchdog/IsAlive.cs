using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;

namespace DeviceSQL.Watchdog
{
    public partial class Functions
    {
        [SqlFunction]
        public static SqlBoolean IsAlive()
        {
            try
            {
                return new SqlBoolean(Watchdog.Worker.IsAlive);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error getting watchdog IsAlive value: {0}", ex.Message));
            }
            return false;
        }
    }
}
