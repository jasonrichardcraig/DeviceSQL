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
        public static Types.ModbusMaster.ModbusMaster_StringRegister ModbusMaster_ReadString(SqlString deviceName, Types.ModbusMaster.ModbusMaster_StringRegister stringRegister)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var stringRegisterValue = new Device.Modbus.Data.StringRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(stringRegister.Address.RelativeAddress.Value), stringRegister.Address.IsZeroBased.Value), stringRegister.Length.Value);
            (device as Device.Modbus.ModbusMaster).ReadStringRegister(null, null, ref stringRegisterValue);
            stringRegister.Data = stringRegisterValue.Data;
            return stringRegister;
        }
    }
}
