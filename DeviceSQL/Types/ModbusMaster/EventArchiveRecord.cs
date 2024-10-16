#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 264)]
    public struct ModbusMaster_EventArchiveRecord : INullable, IBinarySerialize
    {

        #region Fields

        internal byte[] data;

        #endregion

        #region Properties

        public SqlBinary Data
        {
            get
            {
                if (data == null)
                {
                    data = new byte[Length.Value];
                }
                return data;
            }
            set
            {
                data = value.Value;
                Length = data.Length;
            }
        }

        public SqlInt32 Index
        {
            get;
            set;
        }

        public SqlInt32 Length
        {
            get;
            set;
        }

        public bool IsNull
        {
            get;
            private set;
        }

        public SqlInt16 GetShortValue(SqlByte index, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            return new DeviceSQL.Device.Modbus.Data.EventArchiveRecord(Convert.ToUInt16(Index), Data.Value).GetShortValue(index.Value, byteSwap.Value);
        }

        public static SqlDateTime GetDateTimeValue(SqlSingle dateValue, SqlSingle timeValue, SqlInt32 baseYear)
        {
            var dateTime = DeviceSQL.Device.Modbus.Data.EventArchiveRecord.ParseNullableDateTimeValue(dateValue.Value, timeValue.Value, baseYear.Value);
            return dateTime ?? SqlDateTime.Null;
        }

        public SqlSingle GetFloatValue(SqlByte index, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            var floatValue = new DeviceSQL.Device.Modbus.Data.EventArchiveRecord(Convert.ToUInt16(Index), Data.Value).GetNullableFloatValue(index.Value, byteSwap.Value, wordSwap.Value);
            return floatValue ?? SqlSingle.Null;
        }

        public static ModbusMaster_EventArchiveRecord Null
        {
            get
            {
                return (new ModbusMaster_EventArchiveRecord() { IsNull = true });
            }
        }

        #endregion

        #region Helper Methods

        public override string ToString()
        {
            if (this.IsNull)
            {
                return "NULL";
            }
            else
            {
                return string.Format("Index={0};Length={1};Value={2};", Index, Length, Convert.ToBase64String(Data.Value));
            }
        }

        public static ModbusMaster_EventArchiveRecord Parse(SqlString eventArchiveToParse)
        {
            if (eventArchiveToParse.IsNull)
            {
                return Null;
            }

            var parsedEventArchiveRecordData = eventArchiveToParse.Value.Split(",".ToCharArray());
            var parsedEventArchiveRecord = new ModbusMaster_EventArchiveRecord() { Index = Int32.Parse(parsedEventArchiveRecordData[0]), Length = Int32.Parse(parsedEventArchiveRecordData[1]), Data = Convert.FromBase64String(parsedEventArchiveRecordData[2]) };
            return parsedEventArchiveRecord;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();

            if (!IsNull)
            {
                Index = binaryReader.ReadInt32();
                Length = binaryReader.ReadByte();
                Data = binaryReader.ReadBytes(Length.Value);
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);

            if (!IsNull)
            {
                binaryWriter.Write(Index.Value);
                binaryWriter.Write(Length.Value);
                binaryWriter.Write(Data.Value, 0, Length.Value);
            }
        }

        #endregion

    }
}   
