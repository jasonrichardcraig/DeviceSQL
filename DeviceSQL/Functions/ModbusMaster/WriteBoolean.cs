#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class MODBUSMaster
    {
        [SqlFunction]
        public static SqlBoolean WriteBoolean(SqlString deviceName, Types.MODBUSMaster.MODBUSMaster_BooleanRegister booleanRegister)
        {
            var deviceNameValue = deviceName.Value;
            var booleanRegisterRaw = new Device.MODBUS.Data.BooleanRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(booleanRegister.Address.RelativeAddress.Value), booleanRegister.Address.IsZeroBased.Value));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).WriteBooleanRegister(null, booleanRegisterRaw, null);
            return true;
        }
    }
}
