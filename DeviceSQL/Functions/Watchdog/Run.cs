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
        public static SqlBoolean Run()
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
