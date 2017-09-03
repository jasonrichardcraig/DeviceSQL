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
        public static SqlBoolean Run()
        {
            try
            {
                Watchdog.Worker.Run();
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error starting channel manager: {0}", ex.Message));
            }
            return new SqlBoolean(true);
        }
    }
}
