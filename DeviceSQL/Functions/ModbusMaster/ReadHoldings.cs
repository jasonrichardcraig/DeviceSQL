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
        public static Types.ModbusMaster.ModbusMaster_HoldingRegisterArray ModbusMaster_ReadHoldings(SqlString deviceName, Types.ModbusMaster.ModbusMaster_HoldingRegisterArray holdingRegisterArray)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var holdingRegisters = new List<Device.Modbus.Data.HoldingRegister>(holdingRegisterArray.holdingRegisters.Select(holdingRegister => new Device.Modbus.Data.HoldingRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(holdingRegister.Address.RelativeAddress.Value), holdingRegister.Address.IsZeroBased.Value), holdingRegister.ByteSwap.Value)));
            (device as Device.Modbus.ModbusMaster).ReadHoldingRegisters(null, ref holdingRegisters, null);
            return new Types.ModbusMaster.ModbusMaster_HoldingRegisterArray() { holdingRegisters = holdingRegisters.Select(holdingRegister => new Types.ModbusMaster.ModbusMaster_HoldingRegister() { Address = new Types.ModbusMaster.ModbusMaster_ModbusAddress { RelativeAddress = holdingRegister.Address.RelativeAddress, IsZeroBased = holdingRegister.Address.IsZeroBased }, ByteSwap = holdingRegister.ByteSwap, Data = holdingRegister.Data }).ToList() };
        }
    }
}
