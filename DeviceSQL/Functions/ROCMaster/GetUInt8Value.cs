#region Imported Types

using DeviceSQL.Device.ROC.Data;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlByte ROCMaster_GetUInt8Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            var deviceNameValue = deviceName.Value;
            var uInt8Parameter = new UInt8Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).ReadParameter<UInt8Parameter>(null, null, null, null, ref uInt8Parameter);
            return uInt8Parameter.Value;
        }
    }
}
