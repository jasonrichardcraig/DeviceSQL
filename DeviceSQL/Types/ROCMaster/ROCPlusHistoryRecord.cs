#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.RocMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 15)]
    public struct RocMaster_RocPlusHistoryRecord : INullable, IBinarySerialize
    {

        #region Fields

        public byte[] value;

        #endregion

        #region Properties

        public bool IsNull
        {
            get;
            internal set;
        }

        public bool HasTimeStamp
        {
            get
            {
                return Length == 8;
            }
        }

        public static RocMaster_RocPlusHistoryRecord Null
        {
            get
            {
                return new RocMaster_RocPlusHistoryRecord() { IsNull = true };
            }
        }

        public byte HistorySegment
        {
            get;
            internal set;
        }

        public byte HistoryPointNumber
        {
            get;
            internal set;
        }

        public int Index
        {
            get;
            internal set;
        }

        public int Length
        {
            get
            {
                return Value.Length;
            }
        }

        public byte[] Value
        {
            get
            {
                if (value == null)
                {
                    value = new byte[8];
                }
                return value;
            }
            internal set
            {
                this.value = value;
            }
        }

        #endregion

        #region Helper Methods

        public SqlDateTime ToDateTimeStamp(DateTime deviceDateTime)
        {
            var dateTimeStamp = new Device.Roc.Data.RocPlusHistoryRecord() { HistoryPointNumber = this.HistoryPointNumber, HistorySegment = this.HistorySegment, Index = this.Index, Value = this.Value }.DateTimeStamp;
            return dateTimeStamp.HasValue ? dateTimeStamp.Value : SqlDateTime.Null;
        }

        public SqlSingle ToFloat()
        {
            var floatValue = new Device.Roc.Data.RocPlusHistoryRecord() { HistoryPointNumber = this.HistoryPointNumber, HistorySegment = this.HistorySegment, Index = this.Index, Value = this.Value }.ToNullableFloat();
            return floatValue.HasValue ? floatValue.Value : SqlSingle.Null;
        }

        public static RocMaster_RocPlusHistoryRecord Parse(SqlString stringToParse)
        {
            var parsedRocPlusHistoryRecord = stringToParse.Value.Split(",".ToCharArray());
            var base64Bytes = Convert.FromBase64String(parsedRocPlusHistoryRecord[3]);
            if (base64Bytes.Length == 8 || base64Bytes.Length == 4)
            {
                return new RocMaster_RocPlusHistoryRecord() { Index = ushort.Parse(parsedRocPlusHistoryRecord[0]), HistorySegment = byte.Parse(parsedRocPlusHistoryRecord[1]), HistoryPointNumber = byte.Parse(parsedRocPlusHistoryRecord[2]), value = base64Bytes };
            }
            else
            {
                throw new ArgumentException("Input must be exactly 8 or 4 bytes");
            }
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", Index, HistorySegment, HistoryPointNumber, Convert.ToBase64String(Value));
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();
            if (!IsNull)
            {
                var length = binaryReader.ReadInt32();
                Index = binaryReader.ReadInt32();
                HistorySegment = binaryReader.ReadByte();
                HistoryPointNumber = binaryReader.ReadByte();
                Value = binaryReader.ReadBytes(length);
            }
        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            if (!IsNull)
            {
                binaryWriter.Write(Length);
                binaryWriter.Write(Index);
                binaryWriter.Write(HistorySegment);
                binaryWriter.Write(HistoryPointNumber);
                binaryWriter.Write(Value, 0, Length);
            }
        }

        #endregion

    }
}
