using DeviceSQL.Device.ROC.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

namespace DeviceSQL.Types.ROCMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 29)]
    public struct ROCMaster_AuditLogRecord : INullable, IBinarySerialize
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

        public static ROCMaster_AuditLogRecord Null
        {
            get
            {
                return new ROCMaster_AuditLogRecord() { IsNull = true };
            }
        }

        public byte[] Data
        {
            get
            {
                if (data == null)
                {
                    data = new byte[24];
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
                var dateTimeStamp = new AuditLogRecord(Convert.ToUInt16(Index), Data).DateTimeStamp;
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
                var fstNumber = new AuditLogRecord(Convert.ToUInt16(Index), Data).FstNumber;
                return fstNumber.HasValue ? fstNumber.Value : SqlByte.Null;
            }
        }

        public SqlByte PointType
        {
            get
            {
                var pointType = new AuditLogRecord(Convert.ToUInt16(Index), Data).PointType;
                return pointType.HasValue ? pointType.Value : SqlByte.Null;
            }
        }

        public SqlByte LogicalNumber
        {
            get
            {
                var logicalNumber = new AuditLogRecord(Convert.ToUInt16(Index), Data).LogicalNumber;
                return logicalNumber.HasValue ? logicalNumber.Value : SqlByte.Null;
            }
        }

        public SqlByte ParameterNumber
        {
            get
            {
                var parameterNumber = new AuditLogRecord(Convert.ToUInt16(Index), Data).ParameterNumber;
                return parameterNumber.HasValue ? parameterNumber.Value : SqlByte.Null;
            }
        }

        public SqlInt32 Tag
        {
            get
            {
                var tag = new AuditLogRecord(Convert.ToUInt16(Index), Data).Tag;
                return tag.HasValue ? tag.Value : SqlInt32.Null;
            }
        }

        public SqlDateTime PowerRemovedDateTime
        {
            get
            {
                var powerRemovedDateTime = new AuditLogRecord(Convert.ToUInt16(Index), Data).PowerRemovedDateTime;
                return powerRemovedDateTime.HasValue ? powerRemovedDateTime.Value : SqlDateTime.Null;
            }
        }

        public SqlString CalibrationPointType
        {
            get
            {
                var calibrationPointType = new AuditLogRecord(Convert.ToUInt16(Index), Data).CalibrationPointType;
                return calibrationPointType.HasValue ? calibrationPointType.Value.ToString() : SqlString.Null;
            }

        }

        public SqlString CalibrationMultivariableSensorInput
        {
            get
            {
                var calibrationMultivariableSensorInput = new AuditLogRecord(Convert.ToUInt16(Index), Data).CalibrationMultivariableSensorInput;
                return calibrationMultivariableSensorInput.HasValue ? calibrationMultivariableSensorInput.Value.ToString() : SqlString.Null;
            }
        }

        public SqlString CalibrationType
        {
            get
            {
                var calibrationType = new AuditLogRecord(Convert.ToUInt16(Index), Data).CalibrationType;
                return calibrationType.HasValue ? calibrationType.Value.ToString() : SqlString.Null;
            }
        }

        public SqlString EventCode
        {
            get
            {
                return new AuditLogRecord(Convert.ToUInt16(Index), Data).EventCode.ToString();
            }
        }

        public SqlString OperatorId
        {
            get
            {
                return new AuditLogRecord(Convert.ToUInt16(Index), Data).OperatorId;
            }
        }

        public SqlString EventText
        {
            get
            {
                return new AuditLogRecord(Convert.ToUInt16(Index), Data).EventText;
            }
        }

        public SqlBinary OldValue
        {
            get
            {
                return new AuditLogRecord(Convert.ToUInt16(Index), Data).OldValue;
            }
        }

        public SqlSingle FstFloatValue
        {
            get
            {
                var fstFloatValue = new AuditLogRecord(Convert.ToUInt16(Index), Data).FstFloatValue;
                return fstFloatValue.HasValue ? fstFloatValue.Value : SqlSingle.Null;
            }
        }

        public SqlBinary NewValue
        {
            get
            {
                return new AuditLogRecord(Convert.ToUInt16(Index), Data).NewValue;
            }
        }

        public ROCMaster_Parameter OldParameterValue
        {
            get
            {
                if (!PointType.IsNull && !ParameterNumber.IsNull)
                {
                    var pointType = PointType.Value;
                    var parameterNumber = ParameterNumber.Value;
                    var parameterDefinition = Device.ROC.Message.ParameterDatabase.ParameterDefinitions.Where(pd => pd.PointType == pointType && pd.Parameter == parameterNumber).FirstOrDefault();
                    switch (parameterDefinition.DataType)
                    {
                        case "AC":
                            switch (parameterDefinition.Length)
                            {
                                case 3:
                                    return new ROCMaster_Parameter() { RawType = ParameterType.AC3, RawValue = OldValue.Value.Take(3).ToArray() };
                                default:
                                    return ROCMaster_Parameter.Null;
                            }
                        case "BIN":
                            return new ROCMaster_Parameter() { RawType = ParameterType.BIN, RawValue = OldValue.Value.Take(1).ToArray() };
                        case "FL":
                            return new ROCMaster_Parameter() { RawType = ParameterType.FL, RawValue = OldValue.Value };
                        case "INT16":
                            return new ROCMaster_Parameter() { RawType = ParameterType.INT16, RawValue = OldValue.Value.Take(2).ToArray() };
                        case "INT32":
                            return new ROCMaster_Parameter() { RawType = ParameterType.INT32, RawValue = OldValue.Value };
                        case "INT8":
                            return new ROCMaster_Parameter() { RawType = ParameterType.INT8, RawValue = OldValue.Value.Take(1).ToArray() };
                        case "TLP":
                            return new ROCMaster_Parameter() { RawType = ParameterType.TLP, RawValue = OldValue.Value.Take(3).ToArray() };
                        case "UINT16":
                            return new ROCMaster_Parameter() { RawType = ParameterType.UINT16, RawValue = OldValue.Value.Take(2).ToArray() };
                        case "UINT32":
                            return new ROCMaster_Parameter() { RawType = ParameterType.UINT32, RawValue = OldValue.Value };
                        case "TIME":
                            return new ROCMaster_Parameter() { RawType = ParameterType.TIME, RawValue = OldValue.Value };
                        case "UINT8":
                            return new ROCMaster_Parameter() { RawType = ParameterType.UINT8, RawValue = OldValue.Value.Take(1).ToArray() };
                        default:
                            return ROCMaster_Parameter.Null;
                    }

                }
                else
                {
                    return ROCMaster_Parameter.Null;
                }
            }
        }

        public ROCMaster_Parameter NewParameterValue
        {
            get
            {
                if (!PointType.IsNull && !ParameterNumber.IsNull)
                {
                    var pointType = PointType.Value;
                    var parameterNumber = ParameterNumber.Value;
                    var parameterDefinition = Device.ROC.Message.ParameterDatabase.ParameterDefinitions.Where(pd => pd.PointType == pointType && pd.Parameter == parameterNumber).FirstOrDefault();
                    switch (parameterDefinition.DataType)
                    {
                        case "AC":
                            switch (parameterDefinition.Length)
                            {
                                case 3:
                                    return new ROCMaster_Parameter() { RawType = ParameterType.AC3, RawValue = NewValue.Value.Take(3).ToArray() };
                                case 7:
                                    return new ROCMaster_Parameter() { RawType = ParameterType.AC7, RawValue = NewValue.Value.Union(new byte[3]).ToArray() };
                                case 10:
                                    return new ROCMaster_Parameter() { RawType = ParameterType.AC10, RawValue = OldValue.Value.Union(NewValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Tag.Value))).ToArray() };
                                case 12:
                                    return new ROCMaster_Parameter() { RawType = ParameterType.AC12, RawValue = OldValue.Value.Union(NewValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Tag.Value))).Union(new byte[2]).ToArray() };
                                case 20:
                                    return new ROCMaster_Parameter() { RawType = ParameterType.AC20, RawValue = OldValue.Value.Union(NewValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Tag.Value))).Union(new byte[10]).ToArray() };
                                case 30:
                                    return new ROCMaster_Parameter() { RawType = ParameterType.AC30, RawValue = OldValue.Value.Union(NewValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Tag.Value))).Union(new byte[20]).ToArray() };
                                case 40:
                                    return new ROCMaster_Parameter() { RawType = ParameterType.AC40, RawValue = OldValue.Value.Union(NewValue.Value).Union(BitConverter.GetBytes(Convert.ToUInt16(Tag.Value))).Union(new byte[30]).ToArray() };
                                default:
                                    return ROCMaster_Parameter.Null;
                            }
                        case "BIN":
                            return new ROCMaster_Parameter() { RawType = ParameterType.BIN, RawValue = NewValue.Value.Take(1).ToArray() };
                        case "FL":
                            return new ROCMaster_Parameter() { RawType = ParameterType.FL, RawValue = NewValue.Value };
                        case "INT16":
                            return new ROCMaster_Parameter() { RawType = ParameterType.INT16, RawValue = NewValue.Value.Take(2).ToArray() };
                        case "INT32":
                            return new ROCMaster_Parameter() { RawType = ParameterType.INT32, RawValue = NewValue.Value };
                        case "INT8":
                            return new ROCMaster_Parameter() { RawType = ParameterType.INT8, RawValue = NewValue.Value.Take(1).ToArray() };
                        case "TLP":
                            return new ROCMaster_Parameter() { RawType = ParameterType.TLP, RawValue = NewValue.Value.Take(3).ToArray() };
                        case "UINT16":
                            return new ROCMaster_Parameter() { RawType = ParameterType.UINT16, RawValue = NewValue.Value.Take(2).ToArray() };
                        case "UINT32":
                            return new ROCMaster_Parameter() { RawType = ParameterType.UINT32, RawValue = NewValue.Value };
                        case "TIME":
                            return new ROCMaster_Parameter() { RawType = ParameterType.TIME, RawValue = NewValue.Value };
                        case "UINT8":
                            return new ROCMaster_Parameter() { RawType = ParameterType.UINT8, RawValue = NewValue.Value.Take(1).ToArray() };
                        default:
                            return ROCMaster_Parameter.Null;
                    }

                }
                else
                {
                    return ROCMaster_Parameter.Null;
                }
            }
        }

        public SqlInt32 SequenceNumber
        {
            get
            {
                return new AuditLogRecord(Convert.ToUInt16(Index), Data).SequenceNumber;
            }
        }

        public SqlBoolean EventNotSaved
        {
            get
            {
                return new AuditLogRecord(Convert.ToUInt16(Index), Data).EventNotSaved;
            }
        }

        #endregion

        #region Helper Methods

        public static ROCMaster_AuditLogRecord Parse(SqlString stringToParse)
        {
            var parsedAuditLogRecord = stringToParse.Value.Split(",".ToCharArray());
            var base64Bytes = Convert.FromBase64String(parsedAuditLogRecord[1]);
            if (base64Bytes.Length == 24)
            {
                return new ROCMaster_AuditLogRecord() { Index = ushort.Parse(parsedAuditLogRecord[0]), Data = base64Bytes };
            }
            else
            {
                throw new ArgumentException("Input must be exactly 24 bytes");
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
                Data = binaryReader.ReadBytes(24);
            }
        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Index);
            if (!IsNull)
            {
                binaryWriter.Write(Data, 0, 24);
            }
        }

        #endregion

    }
}
