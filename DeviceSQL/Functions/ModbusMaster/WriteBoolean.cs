#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ModbusMaster
    {
        [SqlFunction]
        public static SqlBoolean WriteBoolean(SqlString deviceName, Types.ModbusMaster.BooleanRegister booleanRegister)
        {
            var deviceNameValue = deviceName.Value;
            var booleanRegisterRaw = new Device.Modbus.Data.BooleanRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(booleanRegister.Address.RelativeAddress.Value), booleanRegister.Address.IsZeroBased.Value));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).WriteBooleanRegister(null, booleanRegisterRaw, null);
            return true;
        }
    }
}
