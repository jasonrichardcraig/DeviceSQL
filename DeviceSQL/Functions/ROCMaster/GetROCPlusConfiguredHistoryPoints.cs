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
        public static Types.RocMaster.RocMaster_RocPlusHistoryPointArray RocMaster_GetRocPlusConfiguredHistoryPoints(SqlString deviceName, byte historySegment)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var configuredHistoryPoints = (device as Device.Roc.RocMaster).GetRocPlusConfiguredHistoryPoints(null, null, null, null, historySegment);
            return new Types.RocMaster.RocMaster_RocPlusHistoryPointArray() { historyPoints = configuredHistoryPoints };
        }
    }
}
