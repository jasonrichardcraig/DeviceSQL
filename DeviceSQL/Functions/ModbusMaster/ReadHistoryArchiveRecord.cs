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
        public static Types.ModbusMaster.HistoryArchiveRecord ReadHistoryArchiveRecord(SqlString deviceName, Types.ModbusMaster.ModbusAddress historyArchiveAddress, SqlInt32 index, SqlByte recordSize)
        {
            var deviceNameValue = deviceName.Value;
            var historyArchiveRecord = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadHistoryArchiveRecord(null, new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(historyArchiveAddress.RelativeAddress.Value), historyArchiveAddress.IsZeroBased.Value), Convert.ToUInt16(index.Value), recordSize.Value, null);
            return new Types.ModbusMaster.HistoryArchiveRecord() { Index = historyArchiveRecord.Index, Data = historyArchiveRecord.Data };
        }
    }
}
