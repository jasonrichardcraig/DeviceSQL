#region Imported Types

using DeviceSQL.SQLTypes.ROC.Data;

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.SQLTypes.ROC
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 27)]
    public struct EventRecord : INullable, IBinarySerialize
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

        public static EventRecord Null
        {
            get
            {
                return new EventRecord() { IsNull = true };
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

        public SqlDateTime DateTimeStamp
        {
            get
            {
                var dateTimeStamp = new Data.EventRecord(Convert.ToUInt16(Index), Data).DateTimeStamp;
                return dateTimeStamp.HasValue ? dateTimeStamp.Value : SqlDateTime.Null;
            }
        }

        public int Index
        {
            get;
            internal set;
        }

        public SqlByte FstNumber
        {
            get
            {
                var fstNumber = new Data.EventRecord(Convert.ToUInt16(Index), Data).FstNumber;
                return fstNumber.HasValue ? fstNumber.Value : SqlByte.Null;
            }
        }

        public SqlByte PointType
        {
            get
            {
                var pointType = new Data.EventRecord(Convert.ToUInt16(Index), Data).PointType;
                return pointType.HasValue ? pointType.Value : SqlByte.Null;
            }
        }

        public SqlByte LogicalNumber
        {
            get
            {
                var logicalNumber = new Data.EventRecord(Convert.ToUInt16(Index), Data).LogicalNumber;
                return logicalNumber.HasValue ? logicalNumber.Value : SqlByte.Null;
            }
        }

        public SqlByte ParameterNumber
        {
            get
            {
                var parameterNumber = new Data.EventRecord(Convert.ToUInt16(Index), Data).ParameterNumber;
                return parameterNumber.HasValue ? parameterNumber.Value : SqlByte.Null;
            }
        }

        public SqlInt32 Tag
        {
            get
            {
                var tag = new Data.EventRecord(Convert.ToUInt16(Index), Data).Tag;
                return tag.HasValue ? tag.Value : SqlInt32.Null;
            }
        }

        public SqlDateTime PowerRemovedDateTime
        {
            get
            {
                var powerRemovedDateTime = new Data.EventRecord(Convert.ToUInt16(Index), Data).PowerRemovedDateTime;
                return powerRemovedDateTime.HasValue ? powerRemovedDateTime.Value : SqlDateTime.Null;
            }
        }

        public SqlString CalibrationPointType
        {
            get
            {
                var calibrationPointType = new Data.EventRecord(Convert.ToUInt16(Index), Data).CalibrationPointType;
                return calibrationPointType.HasValue ? calibrationPointType.Value.ToString() : SqlString.Null;
            }

        }

        public SqlString CalibrationMultivariableSensorInput
        {
            get
            {
                var calibrationMultivariableSensorInput = new Data.EventRecord(Convert.ToUInt16(Index), Data).CalibrationMultivariableSensorInput;
                return calibrationMultivariableSensorInput.HasValue ? calibrationMultivariableSensorInput.Value.ToString() : SqlString.Null;
            }
        }

        public SqlString CalibrationType
        {
            get
            {
                var calibrationType = new Data.EventRecord(Convert.ToUInt16(Index), Data).CalibrationType;
                return calibrationType.HasValue ? calibrationType.Value.ToString() : SqlString.Null;
            }
        }

        public SqlString EventCode
        {
            get
            {
                return new Data.EventRecord(Convert.ToUInt16(Index), Data).EventCode.ToString();
            }
        }

        public SqlString OperatorId
        {
            get
            {
                return new Data.EventRecord(Convert.ToUInt16(Index), Data).OperatorId;
            }
        }

        public SqlString EventText
        {
            get
            {
                return new Data.EventRecord(Convert.ToUInt16(Index), Data).EventText;
            }
        }

        public SqlBinary OldValue
        {
            get
            {
                return new Data.EventRecord(Convert.ToUInt16(Index), Data).OldValue;
            }
        }

        public SqlSingle FstFloatValue
        {
            get
            {
                var fstFloatValue = new Data.EventRecord(Convert.ToUInt16(Index), Data).FstFloatValue;
                return fstFloatValue.HasValue ? fstFloatValue.Value : SqlSingle.Null;
            }
        }

        public SqlBinary NewValue
        {
            get
            {
                return new Data.EventRecord(Convert.ToUInt16(Index), Data).NewValue;
            }
        }

        public Parameter OldParameterValue
        {
            get
            {
                if (!PointType.IsNull && !ParameterNumber.IsNull)
                {
                    var pointType = PointType.Value;
                    var parameterNumber = ParameterNumber.Value;
                    var parameterDefinition = ParameterDatabase.ParameterDefinitions.Where(pd => pd.PointType == pointType && pd.Parameter == parameterNumber).FirstOrDefault();
                    switch (parameterDefinition.DataType)
                    {
                        case "AC":
                            switch (parameterDefinition.Length)
                            {
                                case 3:
                                    return new Parameter() { RawType = ParameterType.AC3, RawValue = OldValue.Value.Take(3).ToArray() };
                                default:
                                    return Parameter.Null;
                            }
                        case "BIN":
                            return new Parameter() { RawType = ParameterType.BIN, RawValue = OldValue.Value.Take(1).ToArray() };
                        case "FL":
                            return new Parameter() { RawType = ParameterType.FL, RawValue = OldValue.Value };
                        case "INT16":
                            return new Parameter() { RawType = ParameterType.INT16, RawValue = OldValue.Value.Take(2).ToArray() };
                        case "INT32":
                            return new Parameter() { RawType = ParameterType.INT32, RawValue = OldValue.Value };
                        case "INT8":
                            return new Parameter() { RawType = ParameterType.INT8, RawValue = OldValue.Value.Take(1).ToArray() };
                        case "TLP":
                            return new Parameter() { RawType = ParameterType.TLP, RawValue = OldValue.Value.Take(3).ToArray() };
                        case "UINT16":
                            return new Parameter() { RawType = ParameterType.UINT16, RawValue = OldValue.Value.Take(2).ToArray() };
                        case "UINT32":
                            return new Parameter() { RawType = ParameterType.UINT32, RawValue = OldValue.Value };
                        case "TIME":
                            return new Parameter() { RawType = ParameterType.TIME, RawValue = OldValue.Value };
                        case "UINT8":
                            return new Parameter() { RawType = ParameterType.UINT8, RawValue = OldValue.Value.Take(1).ToArray() };
                        default:
                            return Parameter.Null;
                    }

                }
                else
                {
                    return Parameter.Null;
                }
            }
        }

        public Parameter NewParameterValue
        {
            get
            {
                if (!PointType.IsNull && !ParameterNumber.IsNull)
                {
                    var pointType = PointType.Value;
                    var parameterNumber = ParameterNumber.Value;
                    var parameterDefinition = ParameterDatabase.ParameterDefinitions.Where(pd => pd.PointType == pointType && pd.Parameter == parameterNumber).FirstOrDefault();
                    switch (parameterDefinition.DataType)
                    {
                        case "AC":
                            switch (parameterDefinition.Length)
                            {
                                case 3:
                                    return new Parameter() { RawType = ParameterType.AC3, RawValue = NewValue.Value.Take(3).ToArray() };
                                case 7:
                                    return new Parameter() { RawType = ParameterType.AC7, RawValue = NewValue.Value.Union(new byte[3]).ToArray() };
                                case 10:
                                    return new Parameter() { RawType = ParameterType.AC10, RawValue = OldValue.Value.Union(NewValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Tag.Value))).ToArray() };
                                case 12:
                                    return new Parameter() { RawType = ParameterType.AC12, RawValue = OldValue.Value.Union(NewValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Tag.Value))).Union(new byte[2]).ToArray() };
                                case 20:
                                    return new Parameter() { RawType = ParameterType.AC20, RawValue = OldValue.Value.Union(NewValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Tag.Value))).Union(new byte[10]).ToArray() };
                                case 30:
                                    return new Parameter() { RawType = ParameterType.AC30, RawValue = OldValue.Value.Union(NewValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Tag.Value))).Union(new byte[20]).ToArray() };
                                case 40:
                                    return new Parameter() { RawType = ParameterType.AC40, RawValue = OldValue.Value.Union(NewValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Tag.Value))).Union(new byte[30]).ToArray() };
                                default:
                                    return Parameter.Null;
                            }
                        case "BIN":
                            return new Parameter() { RawType = ParameterType.BIN, RawValue = NewValue.Value.Take(1).ToArray() };
                        case "FL":
                            return new Parameter() { RawType = ParameterType.FL, RawValue = NewValue.Value };
                        case "INT16":
                            return new Parameter() { RawType = ParameterType.INT16, RawValue = NewValue.Value.Take(2).ToArray() };
                        case "INT32":
                            return new Parameter() { RawType = ParameterType.INT32, RawValue = NewValue.Value };
                        case "INT8":
                            return new Parameter() { RawType = ParameterType.INT8, RawValue = NewValue.Value.Take(1).ToArray() };
                        case "TLP":
                            return new Parameter() { RawType = ParameterType.TLP, RawValue = NewValue.Value.Take(3).ToArray() };
                        case "UINT16":
                            return new Parameter() { RawType = ParameterType.UINT16, RawValue = NewValue.Value.Take(2).ToArray() };
                        case "UINT32":
                            return new Parameter() { RawType = ParameterType.UINT32, RawValue = NewValue.Value };
                        case "TIME":
                            return new Parameter() { RawType = ParameterType.TIME, RawValue = NewValue.Value };
                        case "UINT8":
                            return new Parameter() { RawType = ParameterType.UINT8, RawValue = NewValue.Value.Take(1).ToArray() };
                        default:
                            return Parameter.Null;
                    }

                }
                else
                {
                    return Parameter.Null;
                }
            }
        }

        #endregion

        #region Helper Methods

        public static EventRecord Parse(SqlString stringToParse)
        {
            var parsedEventRecord = stringToParse.Value.Split(",".ToCharArray());
            var base64Bytes = Convert.FromBase64String(parsedEventRecord[1]);
            if (base64Bytes.Length == 22)
            {
                return new EventRecord() { Index = ushort.Parse(parsedEventRecord[0]), Data = base64Bytes };
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
