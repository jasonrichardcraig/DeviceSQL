#region Imported Types

using DeviceSQL.SQLTypes.Modbus;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class MODBUSMaster
    {
        [SqlFunction]
        public static SqlBoolean WriteBoolean(SqlString deviceName, BooleanRegister booleanRegister)
        {
            ///var deviceNameValue = deviceName.Value;
            //var booleanRegisterRaw = new Device.MODBUS.Data.BooleanRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(booleanRegister.Address.RelativeAddress.Value), booleanRegister.Address.IsZeroBased.Value));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).WriteBooleanRegister(null, booleanRegisterRaw, null);
            return true;
        }
    }
}
