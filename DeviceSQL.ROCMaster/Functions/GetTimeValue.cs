#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlDateTime GetTimeValue(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            //var deviceNameValue = deviceName.Value;
            //var timeParameter = new Device.ROC.Data.TimeParameter(new Device.ROC.Data.Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).ReadParameter<Device.ROC.Data.TimeParameter>(null, null, null, null, ref timeParameter);
            return SqlDateTime.Null; // timeParameter.Value;
        }
    }
}
