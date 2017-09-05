#region Imported Types

using DeviceSQL.Device.ROC.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.ROCMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 11)]
    public struct ROCMaster_HistoryRecord : INullable, IBinarySerialize
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

        public static ROCMaster_HistoryRecord Null
        {
            get
            {
                return new ROCMaster_HistoryRecord() { IsNull = true };
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

        public byte[] Value
        {
            get
            {
                if (value == null)
                {
                    value = new byte[4];
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

        public static ROCMaster_HistoryRecord Parse(SqlString stringToParse)
        {
            var parsedHistoryRecord = stringToParse.Value.Split(",".ToCharArray());
            var base64Bytes = Convert.FromBase64String(parsedHistoryRecord[3]);
            if (base64Bytes.Length == 4)
            {
                return new ROCMaster_HistoryRecord() { Index = ushort.Parse(parsedHistoryRecord[0]), HistorySegment = byte.Parse(parsedHistoryRecord[1]), HistoryPointNumber = byte.Parse(parsedHistoryRecord[2]), value = base64Bytes };
            }
            else
            {
                throw new ArgumentException("Input must be exactly 4 bytes");
            }
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", Index, HistorySegment, HistoryPointNumber, Convert.ToBase64String(Value));
        }

        public SqlDateTime ToDateTimeStamp(DateTime deviceDateTime)
        {
            var dateTimeStamp = new HistoryRecord() { HistoryPointNumber = this.HistoryPointNumber, HistorySegment = this.HistorySegment, Index = this.Index, Value = this.Value }.ToDateTimeStamp(deviceDateTime);
            return dateTimeStamp.HasValue ? dateTimeStamp.Value : SqlDateTime.Null;
        }

        public DateTime ToExtendedTimeStamp()
        {
            return new HistoryRecord() { HistoryPointNumber = this.HistoryPointNumber, HistorySegment = this.HistorySegment, Index = this.Index, Value = this.Value }.ToExtendedTimeStamp();
        }

        public SqlSingle ToFloat()
        {
            var floatValue = new HistoryRecord() { HistoryPointNumber = this.HistoryPointNumber, HistorySegment = this.HistorySegment, Index = this.Index, Value = this.Value }.ToNullableFloat();
            return floatValue.HasValue ? floatValue.Value : SqlSingle.Null;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();
            if (!IsNull)
            {
                Index = binaryReader.ReadInt32();
                HistorySegment = binaryReader.ReadByte();
                HistoryPointNumber = binaryReader.ReadByte();
                Value = binaryReader.ReadBytes(4);
            }
        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Index);
            binaryWriter.Write(HistorySegment);
            binaryWriter.Write(HistoryPointNumber);
            if (!IsNull)
            {
                binaryWriter.Write(Value, 0, 4);
            }
        }

        #endregion

    }
}
