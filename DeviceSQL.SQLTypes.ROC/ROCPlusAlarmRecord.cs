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
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 28)]
    public struct ROCPlusAlarmRecord : INullable, IBinarySerialize
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

        public static ROCPlusAlarmRecord Null
        {
            get
            {
                return new ROCPlusAlarmRecord() { IsNull = true };
            }
        }

        public byte[] Data
        {
            get
            {
                if (data == null)
                {
                    data = new byte[23];
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
                var dateTimeStamp = new Data.ROCPlusAlarmRecord(Convert.ToUInt16(Index), Data).DateTimeStamp;
                return dateTimeStamp.HasValue ? dateTimeStamp.Value : SqlDateTime.Null;
            }
        }

        public SqlString AlarmSrbxState
        {
            get
            {
                return new Data.ROCPlusAlarmRecord(Convert.ToUInt16(Index), Data).AlarmSrbxState.ToString();
            }
        }

        public SqlString AlarmCondition
        {
            get
            {
                return new Data.ROCPlusAlarmRecord(Convert.ToUInt16(Index), Data).AlarmCondition.ToString();
            }
        }

        public SqlString AlarmType
        {
            get
            {
                return new Data.ROCPlusAlarmRecord(Convert.ToUInt16(Index), Data).AlarmType.ToString();
            }
        }

        public SqlString AlarmCode
        {
            get
            {
                var alarmCode = new Data.ROCPlusAlarmRecord(Convert.ToUInt16(Index), Data).AlarmCode;
                return alarmCode.HasValue ? alarmCode.Value.ToString() : SqlString.Null;
            }
        }

        public string AlarmDescription
        {
            get
            {
                return new Data.ROCPlusAlarmRecord(Convert.ToUInt16(Index), Data).AlarmDescription;
            }
        }

        internal Tlp Tlp
        {
            get
            {
                return new Data.ROCPlusAlarmRecord(Convert.ToUInt16(Index), Data).Tlp;
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

        public SqlByte ParameterNumber
        {
            get
            {
                return Tlp.Parameter;
            }
        }

        public Parameter ParameterValue
        {
            get
            {
                if (!PointType.IsNull && !ParameterNumber.IsNull)
                {
                    var pointType = PointType.Value;
                    var parameterNumber = ParameterNumber.Value;
                    var parameterDefinition = ParameterDatabase.ParameterDefinitions.Where(pd => pd.PointType == pointType && pd.Parameter == parameterNumber).FirstOrDefault();
                    var data = new Data.ROCPlusAlarmRecord(Convert.ToUInt16(Index), Data).data.Skip(19).Take(4).ToArray();
                    switch (parameterDefinition.DataType)
                    {
                        case "BIN":
                            return new Parameter() { RawType = ParameterType.BIN, RawValue = data.Take(1).ToArray() };
                        case "FL":
                            return new Parameter() { RawType = ParameterType.FL, RawValue = data };
                        case "INT16":
                            return new Parameter() { RawType = ParameterType.INT16, RawValue = data.Take(2).ToArray() };
                        case "INT32":
                            return new Parameter() { RawType = ParameterType.INT32, RawValue = data };
                        case "INT8":
                            return new Parameter() { RawType = ParameterType.INT8, RawValue = data.Take(1).ToArray() };
                        case "TLP":
                            return new Parameter() { RawType = ParameterType.TLP, RawValue = data.Take(3).ToArray() };
                        case "UINT16":
                            return new Parameter() { RawType = ParameterType.UINT16, RawValue = data.Take(2).ToArray() };
                        case "UINT32":
                            return new Parameter() { RawType = ParameterType.UINT32, RawValue = data };
                        case "TIME":
                            return new Parameter() { RawType = ParameterType.TIME, RawValue = data };
                        case "UINT8":
                            return new Parameter() { RawType = ParameterType.UINT8, RawValue = data.Take(1).ToArray() };
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

        public SqlSingle Value
        {
            get
            {
                var value = new Data.ROCPlusAlarmRecord(Convert.ToUInt16(Index), Data).Value;
                return value.HasValue ? value.Value : SqlSingle.Null;
            }
        }

        #endregion

        #region Helper Methods

        public static ROCPlusAlarmRecord Parse(SqlString stringToParse)
        {
            var parsedROCPlusAlarmRecord = stringToParse.Value.Split(",".ToCharArray());
            var base64Bytes = Convert.FromBase64String(parsedROCPlusAlarmRecord[1]);
            if (base64Bytes.Length == 23)
            {
                return new ROCPlusAlarmRecord() { Index = ushort.Parse(parsedROCPlusAlarmRecord[0]), Data = base64Bytes };
            }
            else
            {
                throw new ArgumentException("Input must be exactly 23 bytes");
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
                Data = binaryReader.ReadBytes(23);
            }
        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Index);
            if (!IsNull)
            {
                binaryWriter.Write(Data, 0, 23);
            }
        }

        #endregion

    }
}
