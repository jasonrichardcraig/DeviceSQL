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
        public static Types.ModbusMaster.ModbusMaster_HistoryArchiveRecord ModbusMaster_ReadHistoryArchiveRecord(SqlString deviceName, Types.ModbusMaster.ModbusMaster_ModbusAddress historyArchiveAddress, SqlInt32 index, SqlByte recordSize)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var historyArchiveRecord = (device as Device.Modbus.ModbusMaster).ReadHistoryArchiveRecord(null, new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(historyArchiveAddress.RelativeAddress.Value), historyArchiveAddress.IsZeroBased.Value), Convert.ToUInt16(index.Value), recordSize.Value, null);
            return new Types.ModbusMaster.ModbusMaster_HistoryArchiveRecord() { Index = historyArchiveRecord.Index, Data = historyArchiveRecord.Data };
        }
    }
}
