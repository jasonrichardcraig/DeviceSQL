#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.ModbusMaster
{
    public partial class Functions
    {
        [SqlFunction]
        public static UserDefinedTypes.StringRegister ModbusMaster_ReadString(SqlString deviceName, UserDefinedTypes.StringRegister stringRegister)
        {
            var deviceNameValue = deviceName.Value;
            var stringRegisterValue = new Device.Modbus.Data.StringRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(stringRegister.Address.RelativeAddress.Value), stringRegister.Address.IsZeroBased.Value), stringRegister.Length.Value);
            (Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadStringRegister(null, null, ref stringRegisterValue);
            stringRegister.Data = stringRegisterValue.Data;
            return stringRegister;
        }
    }
}
