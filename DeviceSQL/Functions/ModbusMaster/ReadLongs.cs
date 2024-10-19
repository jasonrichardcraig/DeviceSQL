#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using DeviceSQL.Registries;
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
        public static Types.ModbusMaster.ModbusMaster_LongRegisterArray ModbusMaster_ReadLongs(SqlString deviceName, Types.ModbusMaster.ModbusMaster_LongRegisterArray longRegisterArray)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var longRegisters = new List<Device.Modbus.Data.LongRegister>(longRegisterArray.longRegisters.Select(longRegister => new Device.Modbus.Data.LongRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(longRegister.Address.RelativeAddress.Value), longRegister.Address.IsZeroBased.Value), longRegister.ByteSwap.Value, longRegister.WordSwap.Value)));
            (device as Device.Modbus.ModbusMaster).ReadLongRegisters(null, ref longRegisters, null);
            return new Types.ModbusMaster.ModbusMaster_LongRegisterArray() { longRegisters = longRegisters.Select(longRegister => new Types.ModbusMaster.ModbusMaster_LongRegister() { Address = new Types.ModbusMaster.ModbusMaster_ModbusAddress { RelativeAddress = longRegister.Address.RelativeAddress, IsZeroBased = longRegister.Address.IsZeroBased }, Data = longRegister.Data }).ToList() };
        }
    }
}
