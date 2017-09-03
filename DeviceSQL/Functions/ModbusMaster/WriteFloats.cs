#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.ModbusMaster
{
    public partial class Functions
    {
        [SqlFunction]
        public static SqlBoolean WriteFloats(SqlString deviceName, UserDefinedTypes.FloatRegisterArray floatRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var floatRegisters = new List<Device.Modbus.Data.FloatRegister>(floatRegisterArray.floatRegisters.Select(floatRegister => new Device.Modbus.Data.FloatRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(floatRegister.Address.RelativeAddress.Value), floatRegister.Address.IsZeroBased.Value), floatRegister.ByteSwap.Value, floatRegister.WordSwap.Value)));
            (Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).WriteFloatRegisters(null, floatRegisters, null);
            return true;
        }
    }
}
