#region Imported Types

using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class RocMaster
    {
        [SqlFunction]
        public static int RocMaster_GetCurrentRocPlusHistorySegmentIndex(SqlString deviceName, byte historySegment, byte historyType)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            return (device as Device.Roc.RocMaster).GetCurrentRocPlusHistorySegmentIndex(null, null, null, null, historySegment, historyType);
        }
    }
}
