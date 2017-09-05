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
        public static Types.ModbusMaster.StringRegister ReadString(SqlString deviceName, Types.ModbusMaster.StringRegister stringRegister)
        {
            var deviceNameValue = deviceName.Value;
            var stringRegisterValue = new Device.Modbus.Data.StringRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(stringRegister.Address.RelativeAddress.Value), stringRegister.Address.IsZeroBased.Value), stringRegister.Length.Value);
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadStringRegister(null, null, ref stringRegisterValue);
            stringRegister.Data = stringRegisterValue.Data;
            return stringRegister;
        }
    }
}
