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
        public static SqlBoolean Watchdog_IsAlive()
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
