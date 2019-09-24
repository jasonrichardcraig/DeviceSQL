#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlSingle GetFloatValue(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            //var deviceNameValue = deviceName.Value;
            //var flpParameter = new FlpParameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).ReadParameter<FlpParameter>(null, null, null, null, ref flpParameter);
            return 0;// flpParameter.NullableValue.HasValue ? flpParameter.Value : SqlSingle.Null;
        }

    }
}
