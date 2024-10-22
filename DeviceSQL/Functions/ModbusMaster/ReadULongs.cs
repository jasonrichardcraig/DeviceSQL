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
        public static Types.ModbusMaster.ModbusMaster_ULongRegisterArray ModbusMaster_ReadULongs(SqlString deviceName, Types.ModbusMaster.ModbusMaster_ULongRegisterArray uLongRegisterArray)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var uLongRegisters = new List<Device.Modbus.Data.ULongRegister>(uLongRegisterArray.uLongRegisters.Select(uLongRegister => new Device.Modbus.Data.ULongRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(uLongRegister.Address.RelativeAddress.Value), uLongRegister.Address.IsZeroBased.Value), uLongRegister.ByteSwap.Value, uLongRegister.WordSwap.Value)));
            (device as Device.Modbus.ModbusMaster).ReadULongRegisters(null, ref uLongRegisters, null);
            return new Types.ModbusMaster.ModbusMaster_ULongRegisterArray() { uLongRegisters = uLongRegisters.Select(uLongRegister => new Types.ModbusMaster.ModbusMaster_ULongRegister() { Address = new Types.ModbusMaster.ModbusMaster_ModbusAddress { RelativeAddress = uLongRegister.Address.RelativeAddress, IsZeroBased = uLongRegister.Address.IsZeroBased }, Data = uLongRegister.Data }).ToList() };
        }
    }
}
