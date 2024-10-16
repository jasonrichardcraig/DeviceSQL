#region Imported Types

using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class RocMaster
    {
        [SqlFunction]
        public static SqlDateTime RocMaster_GetRealTimeClockValue(SqlString deviceName)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            return new SqlDateTime((device as Device.Roc.RocMaster).GetRealTimeClockValue(null, null, null, null));
        }
    }
}
