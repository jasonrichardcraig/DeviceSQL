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
        public static SqlBoolean Watchdog_Run()
        {
            try
            {
                DeviceSQL.Watchdog.Worker.Run();
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error starting channel manager: {0}", ex.Message));
            }
            return new SqlBoolean(true);
        }
    }
}
