#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlBoolean SetUInt32Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlInt64 value)
        {
            //var deviceNameValue = deviceName.Value;
            //var uInt32Parameter = new UInt32Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = Convert.ToUInt32(value.Value) };
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).WriteParameter<UInt32Parameter>(null, null, null, null, uInt32Parameter);
            return true;
        }
    }
}
