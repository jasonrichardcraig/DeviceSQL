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
        public static UserDefinedTypes.EventArchiveRecordArray ModbusMaster_ReadEventArchiveRecords(SqlString deviceName, UserDefinedTypes.ModbusAddress eventArchiveAddress, SqlInt32 index)
        {
            var deviceNameValue = deviceName.Value;
            var eventArchiveRecords = (Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadEventArchiveRecord(null, new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(eventArchiveAddress.RelativeAddress.Value), eventArchiveAddress.IsZeroBased.Value), Convert.ToUInt16(index.Value), null);
            return new UserDefinedTypes.EventArchiveRecordArray() { eventArchiveRecords = eventArchiveRecords.Select(ear => new UserDefinedTypes.EventArchiveRecord() { Index = ear.Index, Data = ear.Data }).ToList() };
        }
    }
}
