#region Imported Types


using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.SQLTypes.ROC
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 27)]
    public struct AlarmRecord : INullable, IBinarySerialize
    {

        #region Fields

        public byte[] data;

        #endregion

        #region Properties

        public bool IsNull
        {
            get;
            internal set;
        }

        public static AlarmRecord Null
        {
            get
            {
                return new AlarmRecord() { IsNull = true };
            }
        }

        public SqlDateTime DateTimeStamp
        {
            get
            {
                var alarmRecord = new Data.AlarmRecord(Convert.ToUInt16(Index), Data);
                return alarmRecord.DateTimeStamp.HasValue ? alarmRecord.DateTimeStamp.Value : SqlDateTime.Null;
            }
        }

        public int Index
        {
            get;
            set;
        }

        public string AlarmCode
        {
            get
            {
                return new Data.AlarmRecord(Convert.ToUInt16(Index), Data).AlarmCode.ToString();
            }
        }

        public string AlarmClass
        {
            get
            {
                return new Data.AlarmRecord(Convert.ToUInt16(Index), Data).AlarmClass.ToString();
            }
        }

        public string AlarmState
        {
            get
            {
                return new Data.AlarmRecord(Convert.ToUInt16(Index), Data).AlarmState.ToString();
            }
        }

        public string Tag
        {
            get
            {
                return new Data.AlarmRecord(Convert.ToUInt16(Index), Data).Tag;
            }
        }

        public SqlSingle Value
        {
            get
            {
                var alarmRecord = new Data.AlarmRecord(Convert.ToUInt16(Index), Data);
                return alarmRecord.NullableValue.HasValue ? alarmRecord.Value : SqlSingle.Null;
            }
        }

        public byte[] Data
        {
            get
            {
                if (data == null)
                {
                    data = new byte[22];
                }
                return data;
            }

            internal set
            {
                data = value;
            }
        }

        #endregion

        #region Helper Methods

        public static AlarmRecord Parse(SqlString stringToParse)
        {
            var parsedEventRecord = stringToParse.Value.Split(",".ToCharArray());
            var base64Bytes = Convert.FromBase64String(parsedEventRecord[1]);
            if (base64Bytes.Length == 22)
            {
                return new AlarmRecord() { Index = ushort.Parse(parsedEventRecord[0]), Data = base64Bytes };
            }
            else
            {
                throw new ArgumentException("Input must be exactly 22 bytes");
            }
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", Index, Convert.ToBase64String(Data));
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();
            Index = binaryReader.ReadInt32();
            if (!IsNull)
            {
                Data = binaryReader.ReadBytes(22);
            }
        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Index);
            if (!IsNull)
            {
                binaryWriter.Write(Data, 0, 22);
            }
        }

        #endregion

    }
}
