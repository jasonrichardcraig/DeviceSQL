#region Imported Types

using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class DeviceManager
    {
        [SqlFunction]
        public static SqlBoolean DeviceManager_UnregisterDevice(SqlString deviceName)
        {
            try
            {
                ServiceRegistry.RemoveDevice(deviceName.Value);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error unregistering device: {0}", ex.Message));
            }
            return new SqlBoolean(true);
        }
    }
}
