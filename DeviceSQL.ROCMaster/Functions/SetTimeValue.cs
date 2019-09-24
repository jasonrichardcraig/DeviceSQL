#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlBoolean SetTimeValue(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlDateTime value)
        {
            //var deviceNameValue = deviceName.Value;
            //var timeParameter = new TimeParameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = value.Value };
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).WriteParameter<TimeParameter>(null, null, null, null, timeParameter);
            return true;
        }
    }
}
