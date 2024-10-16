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
        public static Types.ModbusMaster.ModbusMaster_CoilRegisterArray ModbusMaster_ReadCoils(SqlString deviceName, Types.ModbusMaster.ModbusMaster_CoilRegisterArray coilRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var coilRegisters = new List<Device.Modbus.Data.CoilRegister>(coilRegisterArray.coilRegisters.Select(coilRegister => new Device.Modbus.Data.CoilRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(coilRegister.Address.RelativeAddress.Value), coilRegister.Address.IsZeroBased.Value))));
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            (device as Device.Modbus.ModbusMaster).ReadCoilRegisters(null, ref coilRegisters, null);
            return new Types.ModbusMaster.ModbusMaster_CoilRegisterArray() { coilRegisters = coilRegisters.Select(coilRegister => new Types.ModbusMaster.ModbusMaster_CoilRegister() { Address = new Types.ModbusMaster.ModbusMaster_ModbusAddress { RelativeAddress = coilRegister.Address.RelativeAddress, IsZeroBased = coilRegister.Address.IsZeroBased }, Data = coilRegister.Data }).ToList() };
        }
    }
}
