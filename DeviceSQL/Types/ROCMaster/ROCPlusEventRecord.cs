#region Imported Types

using DeviceSQL.Device.Roc.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.Types.RocMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 27)]
    public struct RocMaster_RocPlusEventRecord : INullable, IBinarySerialize
    {

        #region Fields

        private byte[] data;

        #endregion

        #region Properties

        public bool IsNull
        {
            get;
            internal set;
        }

        public static RocMaster_RocPlusEventRecord Null
        {
            get
            {
                return new RocMaster_RocPlusEventRecord() { IsNull = true };
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

        public int Index
        {
            get;
            internal set;
        }

        public SqlDateTime DateTimeStamp
        {
            get
            {
                var dateTimeStamp = new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).DateTimeStamp;
                return dateTimeStamp.HasValue ? dateTimeStamp.Value : SqlDateTime.Null;
            }
        }

        public SqlString EventType
        {
            get
            {
                return new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).EventType.ToString();
            }
        }

        public SqlString EventCode
        {
            get
            {
                var eventCode = new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).EventCode;
                return eventCode.HasValue ? eventCode.Value.ToString() : SqlString.Null;
            }
        }

        public string OperatorId
        {
            get
            {
                return new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).OperatorId;
            }
        }

        internal Tlp Tlp
        {
            get
            {
                return new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).Tlp;
            }
        }

        public SqlByte PointType
        {
            get
            {
                return Tlp.PointType;
            }
        }

        public SqlByte LogicalNumber
        {
            get
            {
                return Tlp.LogicalNumber;
            }
        }

        public SqlByte Parameter
        {
            get
            {
                return Tlp.Parameter;
            }
        }

        public SqlString DataType
        {
            get
            {
                var dataType = new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).DataType;
                return dataType.HasValue ? dataType.Value.ToString() : SqlString.Null;
            }
        }

        public SqlBinary OldValue
        {
            get
            {
                return new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).OldValue;
            }
        }

        public SqlBinary NewValue
        {
            get
            {
                return new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).NewValue;
            }
        }

        public SqlInt32 Spare
        {
            get
            {
                return new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).Spare;
            }
        }

        public SqlString Description
        {
            get
            {
                return new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).Description;
            }
        }

        public SqlByte FstNumber
        {
            get
            {
                var fstNumber = new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).FstNumber;
                return fstNumber.HasValue ? fstNumber.Value : SqlByte.Null;
            }
        }

        public SqlSingle FstValue
        {
            get
            {
                var fstValue = new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).FstValue;
                return fstValue.HasValue ? fstValue.Value : SqlSingle.Null;
            }
        }

        public SqlDateTime DateTimeValue
        {
            get
            {
                var dateTimeValue = new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).DateTimeValue;
                return dateTimeValue.HasValue ? dateTimeValue.Value : SqlDateTime.Null;
            }
        }

        public SqlSingle CalibrationRawValue
        {
            get
            {
                var calibrationRawValue = new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).CalibrationRawValue;
                return calibrationRawValue.HasValue ? calibrationRawValue.Value : SqlSingle.Null;
            }
        }

        public SqlSingle CalibrationCalibratedValue
        {
            get
            {
                var calibrationCalibratedValue = new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).CalibrationCalibratedValue;
                return calibrationCalibratedValue.HasValue ? calibrationCalibratedValue.Value : SqlSingle.Null;
            }
        }

        public RocMaster_Parameter OldParameterValue
        {
            get
            {
                if (!PointType.IsNull && !Parameter.IsNull)
                {
                    switch (new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).DataType)
                    {
                        case ParameterType.AC3:
                            return new RocMaster_Parameter() { RawType = ParameterType.AC3, RawValue = OldValue.Value.Take(3).ToArray() };
                        case ParameterType.BIN:
                            return new RocMaster_Parameter() { RawType = ParameterType.BIN, RawValue = OldValue.Value.Take(1).ToArray() };
                        case ParameterType.FL:
                            return new RocMaster_Parameter() { RawType = ParameterType.FL, RawValue = OldValue.Value };
                        case ParameterType.INT16:
                            return new RocMaster_Parameter() { RawType = ParameterType.INT16, RawValue = OldValue.Value.Take(2).ToArray() };
                        case ParameterType.INT32:
                            return new RocMaster_Parameter() { RawType = ParameterType.INT32, RawValue = OldValue.Value };
                        case ParameterType.INT8:
                            return new RocMaster_Parameter() { RawType = ParameterType.INT8, RawValue = OldValue.Value.Take(1).ToArray() };
                        case ParameterType.TLP:
                            return new RocMaster_Parameter() { RawType = ParameterType.TLP, RawValue = OldValue.Value.Take(3).ToArray() };
                        case ParameterType.UINT16:
                            return new RocMaster_Parameter() { RawType = ParameterType.UINT16, RawValue = OldValue.Value.Take(2).ToArray() };
                        case ParameterType.UINT32:
                            return new RocMaster_Parameter() { RawType = ParameterType.UINT32, RawValue = OldValue.Value };
                        case ParameterType.TIME:
                            return new RocMaster_Parameter() { RawType = ParameterType.TIME, RawValue = OldValue.Value };
                        case ParameterType.UINT8:
                            return new RocMaster_Parameter() { RawType = ParameterType.UINT8, RawValue = OldValue.Value.Take(1).ToArray() };
                        default:
                            return RocMaster_Parameter.Null;
                    }
                }
                else
                {
                    return RocMaster_Parameter.Null;
                }
            }
        }

        public RocMaster_Parameter NewParameterValue
        {
            get
            {
                if (!PointType.IsNull && !Parameter.IsNull)
                {
                    switch (new Device.Roc.Data.RocPlusEventRecord(Convert.ToUInt16(Index), Data).DataType)
                    {
                        case ParameterType.AC3:
                            return new RocMaster_Parameter() { RawType = ParameterType.AC3, RawValue = NewValue.Value.Take(3).ToArray() };
                        case ParameterType.AC7:
                            return new RocMaster_Parameter() { RawType = ParameterType.AC7, RawValue = NewValue.Value.Union(new byte[3]).ToArray() };
                        case ParameterType.AC10:
                            return new RocMaster_Parameter() { RawType = ParameterType.AC10, RawValue = NewValue.Value.Union(OldValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Spare.Value))).ToArray() };
                        case ParameterType.AC12:
                            return new RocMaster_Parameter() { RawType = ParameterType.AC12, RawValue = NewValue.Value.Union(OldValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Spare.Value))).Union(new byte[2]).ToArray() };
                        case ParameterType.AC20:
                            return new RocMaster_Parameter() { RawType = ParameterType.AC20, RawValue = NewValue.Value.Union(OldValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Spare.Value))).Union(new byte[10]).ToArray() };
                        case ParameterType.AC30:
                            return new RocMaster_Parameter() { RawType = ParameterType.AC30, RawValue = NewValue.Value.Union(OldValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Spare.Value))).Union(new byte[20]).ToArray() };
                        case ParameterType.AC40:
                            return new RocMaster_Parameter() { RawType = ParameterType.AC40, RawValue = NewValue.Value.Union(OldValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Spare.Value))).Union(new byte[30]).ToArray() };
                        case ParameterType.BIN:
                            return new RocMaster_Parameter() { RawType = ParameterType.BIN, RawValue = NewValue.Value.Take(1).ToArray() };
                        case ParameterType.FL:
                            return new RocMaster_Parameter() { RawType = ParameterType.FL, RawValue = NewValue.Value };
                        case ParameterType.DOUBLE:
                            return new RocMaster_Parameter() { RawType = ParameterType.DOUBLE, RawValue = NewValue.Value.Union(OldValue.Value).ToArray() };
                        case ParameterType.INT16:
                            return new RocMaster_Parameter() { RawType = ParameterType.INT16, RawValue = NewValue.Value.Take(2).ToArray() };
                        case ParameterType.INT32:
                            return new RocMaster_Parameter() { RawType = ParameterType.INT32, RawValue = NewValue.Value };
                        case ParameterType.INT8:
                            return new RocMaster_Parameter() { RawType = ParameterType.INT8, RawValue = NewValue.Value.Take(1).ToArray() };
                        case ParameterType.TLP:
                            return new RocMaster_Parameter() { RawType = ParameterType.TLP, RawValue = NewValue.Value.Take(3).ToArray() };
                        case ParameterType.UINT16:
                            return new RocMaster_Parameter() { RawType = ParameterType.UINT16, RawValue = NewValue.Value.Take(2).ToArray() };
                        case ParameterType.UINT32:
                            return new RocMaster_Parameter() { RawType = ParameterType.UINT32, RawValue = NewValue.Value };
                        case ParameterType.TIME:
                            return new RocMaster_Parameter() { RawType = ParameterType.TIME, RawValue = NewValue.Value };
                        case ParameterType.UINT8:
                            return new RocMaster_Parameter() { RawType = ParameterType.UINT8, RawValue = NewValue.Value.Take(1).ToArray() };
                        default:
                            return RocMaster_Parameter.Null;
                    }
                }
                else
                {
                    return RocMaster_Parameter.Null;
                }
            }
        }

        #endregion

        #region Helper Methods

        public static RocMaster_RocPlusEventRecord Parse(SqlString stringToParse)
        {
            var parsedEventRecord = stringToParse.Value.Split(",".ToCharArray());
            var base64Bytes = Convert.FromBase64String(parsedEventRecord[1]);
            if (base64Bytes.Length == 22)
            {
                return new RocMaster_RocPlusEventRecord() { Index = ushort.Parse(parsedEventRecord[0]), Data = base64Bytes };
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
