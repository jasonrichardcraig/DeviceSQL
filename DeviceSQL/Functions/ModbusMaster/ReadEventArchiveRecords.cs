#region Imported Types

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
        public static Types.ModbusMaster.EventArchiveRecordArray ReadEventArchiveRecords(SqlString deviceName, Types.ModbusMaster.ModbusAddress eventArchiveAddress, SqlInt32 index)
        {
            var deviceNameValue = deviceName.Value;
            var eventArchiveRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadEventArchiveRecord(null, new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(eventArchiveAddress.RelativeAddress.Value), eventArchiveAddress.IsZeroBased.Value), Convert.ToUInt16(index.Value), null);
            return new Types.ModbusMaster.EventArchiveRecordArray() { eventArchiveRecords = eventArchiveRecords.Select(ear => new Types.ModbusMaster.EventArchiveRecord() { Index = ear.Index, Data = ear.Data }).ToList() };
        }
    }
}
