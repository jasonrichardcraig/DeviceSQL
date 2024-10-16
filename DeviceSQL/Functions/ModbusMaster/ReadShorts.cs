#region Imported Types

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
        public static Types.ModbusMaster.ModbusMaster_ShortRegisterArray ModbusMaster_ReadShorts(SqlString deviceName, Types.ModbusMaster.ModbusMaster_ShortRegisterArray shortRegisterArray)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var shortRegisters = new List<Device.Modbus.Data.ShortRegister>(shortRegisterArray.shortRegisters.Select(longRegister => new Device.Modbus.Data.ShortRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(longRegister.Address.RelativeAddress.Value), longRegister.Address.IsZeroBased.Value), longRegister.ByteSwap.Value)));
            (device as Device.Modbus.ModbusMaster).ReadShortRegisters(null, ref shortRegisters, null);
            return new Types.ModbusMaster.ModbusMaster_ShortRegisterArray() { shortRegisters = shortRegisters.Select(shortRegister => new Types.ModbusMaster.ModbusMaster_ShortRegister() { Address = new Types.ModbusMaster.ModbusMaster_ModbusAddress { RelativeAddress = shortRegister.Address.RelativeAddress, IsZeroBased = shortRegister.Address.IsZeroBased }, Data = shortRegister.Data }).ToList() };
        }
    }
}
