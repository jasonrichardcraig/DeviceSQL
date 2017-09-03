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
        public static UserDefinedTypes.ShortRegisterArray ReadShorts(SqlString deviceName, UserDefinedTypes.ShortRegisterArray shortRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var shortRegisters = new List<Device.Modbus.Data.ShortRegister>(shortRegisterArray.shortRegisters.Select(longRegister => new Device.Modbus.Data.ShortRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(longRegister.Address.RelativeAddress.Value), longRegister.Address.IsZeroBased.Value), longRegister.ByteSwap.Value)));
            (Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadShortRegisters(null, ref shortRegisters, null);
            return new UserDefinedTypes.ShortRegisterArray() { shortRegisters = shortRegisters.Select(shortRegister => new UserDefinedTypes.ShortRegister() { Address = new UserDefinedTypes.ModbusAddress { RelativeAddress = shortRegister.Address.RelativeAddress, IsZeroBased = shortRegister.Address.IsZeroBased }, Data = shortRegister.Data }).ToList() };
        }
    }
}
