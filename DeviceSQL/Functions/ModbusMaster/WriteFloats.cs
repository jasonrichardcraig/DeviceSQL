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
        public static SqlBoolean ModbusMaster_WriteFloats(SqlString deviceName, Types.ModbusMaster.ModbusMaster_FloatRegisterArray floatRegisterArray)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var floatRegisters = new List<Device.Modbus.Data.FloatRegister>(floatRegisterArray.floatRegisters.Select(floatRegister => new Device.Modbus.Data.FloatRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(floatRegister.Address.RelativeAddress.Value), floatRegister.Address.IsZeroBased.Value), floatRegister.ByteSwap.Value, floatRegister.WordSwap.Value)));
            (device as Device.Modbus.ModbusMaster).WriteFloatRegisters(null, floatRegisters, null);
            return true;
        }
    }
}
