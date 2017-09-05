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
        public static Types.ModbusMaster.LongRegisterArray ReadLongs(SqlString deviceName, Types.ModbusMaster.LongRegisterArray longRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var longRegisters = new List<Device.Modbus.Data.LongRegister>(longRegisterArray.longRegisters.Select(longRegister => new Device.Modbus.Data.LongRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(longRegister.Address.RelativeAddress.Value), longRegister.Address.IsZeroBased.Value), longRegister.ByteSwap.Value, longRegister.WordSwap.Value)));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadLongRegisters(null, ref longRegisters, null);
            return new Types.ModbusMaster.LongRegisterArray() { longRegisters = longRegisters.Select(floatRegister => new Types.ModbusMaster.LongRegister() { Address = new Types.ModbusMaster.ModbusAddress { RelativeAddress = floatRegister.Address.RelativeAddress, IsZeroBased = floatRegister.Address.IsZeroBased }, Data = floatRegister.Data }).ToList() };
        }
    }
}
