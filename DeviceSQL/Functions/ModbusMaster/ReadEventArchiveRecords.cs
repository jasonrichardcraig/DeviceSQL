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
        public static Types.ModbusMaster.ModbusMaster_EventArchiveRecordArray ModbusMaster_ReadEventArchiveRecords(SqlString deviceName, Types.ModbusMaster.ModbusMaster_ModbusAddress eventArchiveAddress, SqlInt32 index)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var eventArchiveRecords = (device as Device.Modbus.ModbusMaster).ReadEventArchiveRecord(null, new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(eventArchiveAddress.RelativeAddress.Value), eventArchiveAddress.IsZeroBased.Value), Convert.ToUInt16(index.Value), null);
            return new Types.ModbusMaster.ModbusMaster_EventArchiveRecordArray() { eventArchiveRecords = eventArchiveRecords.Select(ear => new Types.ModbusMaster.ModbusMaster_EventArchiveRecord() { Index = ear.Index, Data = ear.Data }).ToList() };
        }
    }
}
