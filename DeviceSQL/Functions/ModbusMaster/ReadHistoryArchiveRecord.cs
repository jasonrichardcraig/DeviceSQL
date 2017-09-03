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
        public static UserDefinedTypes.HistoryArchiveRecord ReadHistoryArchiveRecord(SqlString deviceName, UserDefinedTypes.ModbusAddress historyArchiveAddress, SqlInt32 index, SqlByte recordSize)
        {
            var deviceNameValue = deviceName.Value;
            var historyArchiveRecord = (Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadHistoryArchiveRecord(null, new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(historyArchiveAddress.RelativeAddress.Value), historyArchiveAddress.IsZeroBased.Value), Convert.ToUInt16(index.Value), recordSize.Value, null);
            return new UserDefinedTypes.HistoryArchiveRecord() { Index = historyArchiveRecord.Index, Data = historyArchiveRecord.Data };
        }
    }
}
