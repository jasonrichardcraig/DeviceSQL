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
        public static Types.ModbusMaster.ShortRegisterArray ReadShorts(SqlString deviceName, Types.ModbusMaster.ShortRegisterArray shortRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var shortRegisters = new List<Device.Modbus.Data.ShortRegister>(shortRegisterArray.shortRegisters.Select(longRegister => new Device.Modbus.Data.ShortRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(longRegister.Address.RelativeAddress.Value), longRegister.Address.IsZeroBased.Value), longRegister.ByteSwap.Value)));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadShortRegisters(null, ref shortRegisters, null);
            return new Types.ModbusMaster.ShortRegisterArray() { shortRegisters = shortRegisters.Select(shortRegister => new Types.ModbusMaster.ShortRegister() { Address = new Types.ModbusMaster.ModbusAddress { RelativeAddress = shortRegister.Address.RelativeAddress, IsZeroBased = shortRegister.Address.IsZeroBased }, Data = shortRegister.Data }).ToList() };
        }
    }
}
