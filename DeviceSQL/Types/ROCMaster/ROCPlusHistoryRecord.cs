#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.ROCMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 15)]
    public struct ROCMaster_ROCPlusHistoryRecord : INullable, IBinarySerialize
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

        public static ROCMaster_ROCPlusHistoryRecord Null
        {
            get
            {
                return new ROCMaster_ROCPlusHistoryRecord() { IsNull = true };
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
            var dateTimeStamp = new Device.ROC.Data.ROCPlusHistoryRecord() { HistoryPointNumber = this.HistoryPointNumber, HistorySegment = this.HistorySegment, Index = this.Index, Value = this.Value }.DateTimeStamp;
            return dateTimeStamp.HasValue ? dateTimeStamp.Value : SqlDateTime.Null;
        }

        public SqlSingle ToFloat()
        {
            var floatValue = new Device.ROC.Data.ROCPlusHistoryRecord() { HistoryPointNumber = this.HistoryPointNumber, HistorySegment = this.HistorySegment, Index = this.Index, Value = this.Value }.ToNullableFloat();
            return floatValue.HasValue ? floatValue.Value : SqlSingle.Null;
        }

        public static ROCMaster_ROCPlusHistoryRecord Parse(SqlString stringToParse)
        {
            var parsedROCPlusHistoryRecord = stringToParse.Value.Split(",".ToCharArray());
            var base64Bytes = Convert.FromBase64String(parsedROCPlusHistoryRecord[3]);
            if (base64Bytes.Length == 8 || base64Bytes.Length == 4)
            {
                return new ROCMaster_ROCPlusHistoryRecord() { Index = ushort.Parse(parsedROCPlusHistoryRecord[0]), HistorySegment = byte.Parse(parsedROCPlusHistoryRecord[1]), HistoryPointNumber = byte.Parse(parsedROCPlusHistoryRecord[2]), value = base64Bytes };
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
