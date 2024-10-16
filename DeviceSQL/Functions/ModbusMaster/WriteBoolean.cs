#region Imported Types

using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ModbusMaster
    {
        [SqlFunction]
        public static SqlBoolean ModbusMaster_WriteBoolean(SqlString deviceName, Types.ModbusMaster.ModbusMaster_BooleanRegister booleanRegister)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var booleanRegisterRaw = new Device.Modbus.Data.BooleanRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(booleanRegister.Address.RelativeAddress.Value), booleanRegister.Address.IsZeroBased.Value));
            (device as Device.Modbus.ModbusMaster).WriteBooleanRegister(null, booleanRegisterRaw, null);
            return true;
        }
    }
}
