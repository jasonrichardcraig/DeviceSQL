#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlInt16 GetInt8Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            //var deviceNameValue = deviceName.Value;
            //var int8Parameter = new Int8Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            // (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).ReadParameter<Int8Parameter>(null, null, null, null, ref int8Parameter);
            return 0; // int8Parameter.Value;
        }
    }
}
