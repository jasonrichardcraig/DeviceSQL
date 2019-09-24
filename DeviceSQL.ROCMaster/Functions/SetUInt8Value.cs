#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlBoolean SetUInt8Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlByte value)
        {
            //var deviceNameValue = deviceName.Value;
            //var uInt8Parameter = new UInt8Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = value.Value };
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).WriteParameter<UInt8Parameter>(null, null, null, null, uInt8Parameter);
            return true;
        }
    }
}
