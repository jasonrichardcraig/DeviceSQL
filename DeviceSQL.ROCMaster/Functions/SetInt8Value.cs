#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlBoolean SetInt8Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlInt16 value)
        {
            //var deviceNameValue = deviceName.Value;
            //var int8Parameter = new Int8Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = Convert.ToSByte(value.Value) };
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as DeviceSQL.Device.ROC.ROCMaster).WriteParameter<Int8Parameter>(null, null, null, null, int8Parameter);
            return true;
        }
    }
}
