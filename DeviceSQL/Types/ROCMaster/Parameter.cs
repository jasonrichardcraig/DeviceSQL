#region Imported Types

using DeviceSQL.Device.Roc.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.RocMaster
{

    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 61)]
    public struct RocMaster_Parameter : INullable, IBinarySerialize
    {

        #region Fields

        private byte parameterType;
        private byte[] rawValue;

        #endregion

        #region Properties

        public bool IsNull
        {
            get;
            private set;
        }

        public static RocMaster_Parameter Null
        {
            get
            {
                return (new RocMaster_Parameter() { IsNull = true });
            }
        }

        public byte PointType
        {
            get; set;
        }

        public byte LogicalNumber
        {
            get; set;
        }

        public byte Parameter
        {
            get; set;
        }

        public string Type
        {
            get
            {
                return RawType.ToString();
            }
        }

        internal ParameterType RawType
        {
            get
            {
                return (ParameterType)parameterType;
            }
            set
            {
                parameterType = (byte)value;
            }
        }

        internal byte[] RawValue
        {
            get
            {
                if (rawValue == null)
                {
                    switch ((ParameterType)parameterType)
                    {
                        case ParameterType.AC3:
                            rawValue = new byte[3];
                            break;
                        case ParameterType.AC7:
                            rawValue = new byte[3];
                            break;
                        case ParameterType.AC10:
                            rawValue = new byte[10];
                            break;
                        case ParameterType.AC12:
                            rawValue = new byte[12];
                            break;
                        case ParameterType.AC20:
                            rawValue = new byte[20];
                            break;
                        case ParameterType.AC30:
                            rawValue = new byte[30];
                            break;
                        case ParameterType.AC40:
                            rawValue = new byte[40];
                            break;
                        case ParameterType.BIN:
                            rawValue = new byte[1];
                            break;
                        case ParameterType.FL:
                            rawValue = new byte[4];
                            break;
                        case ParameterType.DOUBLE:
                            rawValue = new byte[8];
                            break;
                        case ParameterType.INT16:
                            rawValue = new byte[2];
                            break;
                        case ParameterType.INT32:
                            rawValue = new byte[4];
                            break;
                        case ParameterType.INT8:
                            rawValue = new byte[1];
                            break;
                        case ParameterType.TLP:
                            rawValue = new byte[3];
                            break;
                        case ParameterType.UINT16:
                            rawValue = new byte[2];
                            break;
                        case ParameterType.UINT32:
                            rawValue = new byte[4];
                            break;
                        case ParameterType.TIME:
                            rawValue = new byte[4];
                            break;
                        case ParameterType.UINT8:
                            rawValue = new byte[10];
                            break;
                    }
                }

                return rawValue;
            }
            set
            {
                rawValue = value;
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
                switch ((ParameterType)parameterType)
                {
                    case ParameterType.AC3:
                        return (new Ac3Parameter() { Data = RawValue }).Value;
                    case ParameterType.AC7:
                        return (new Ac7Parameter() { Data = RawValue }).Value;
                    case ParameterType.AC10:
                        return (new Ac10Parameter() { Data = RawValue }).Value;
                    case ParameterType.AC12:
                        return (new Ac12Parameter() { Data = RawValue }).Value;
                    case ParameterType.AC20:
                        return (new Ac20Parameter() { Data = RawValue }).Value;
                    case ParameterType.AC30:
                        return (new Ac30Parameter() { Data = RawValue }).Value;
                    case ParameterType.AC40:
                        return (new Ac40Parameter() { Data = RawValue }).Value;
                    case ParameterType.BIN:
                        return (new BinParameter() { Data = RawValue }).Value.ToString();
                    case ParameterType.FL:
                        return (new FlpParameter() { Data = RawValue }).Value.ToString();
                    case ParameterType.DOUBLE:
                        return (new DoubleParameter() { Data = RawValue }).Value.ToString();
                    case ParameterType.INT16:
                        return (new Int16Parameter() { Data = RawValue }).Value.ToString();
                    case ParameterType.INT32:
                        return (new Int32Parameter() { Data = RawValue }).Value.ToString();
                    case ParameterType.INT8:
                        return (new Int8Parameter() { Data = RawValue }).Value.ToString();
                    case ParameterType.TLP:
                        {
                            var tlpParameter = new TlpParameter() { Data = RawValue };
                            return string.Format("{0}.{1}.{2}", tlpParameter.Value.PointType.ToString(), tlpParameter.Value.LogicalNumber.ToString(), tlpParameter.Value.Parameter.ToString());
                        }
                    case ParameterType.UINT16:
                        return (new UInt16Parameter() { Data = RawValue }).Value.ToString();
                    case ParameterType.UINT32:
                        return (new Int32Parameter() { Data = RawValue }).Value.ToString();
                    case ParameterType.TIME:
                        return (new TimeParameter() { Data = RawValue }).Value.ToString();
                    case ParameterType.UINT8:
                        return (new UInt8Parameter() { Data = RawValue }).Value.ToString();
                    default:
                        return "NULL";
                }
            }
        }

        public SqlByte ToBin()
        {
            return (new BinParameter() { Data = RawValue }).Value;
        }

        public SqlByte ToUInt8()
        {
            return (new UInt8Parameter() { Data = RawValue }).Value;
        }

        public SqlInt16 ToInt8()
        {
            return (new Int8Parameter() { Data = RawValue }).Value;
        }

        public SqlInt16 ToInt16()
        {
            return (new Int16Parameter() { Data = RawValue }).Value;
        }
        public SqlInt32 ToUInt16()
        {
            return (new UInt16Parameter() { Data = RawValue }).Value;
        }

        public SqlInt32 ToInt32()
        {
            return (new Int32Parameter() { Data = RawValue }).Value;
        }

        public SqlInt64 ToUInt32()
        {
            return (new UInt32Parameter() { Data = RawValue }).Value;
        }

        public SqlDateTime ToTime()
        {
            return (new TimeParameter() { Data = RawValue }).Value;
        }

        public SqlSingle ToFl()
        {
            var flpValue = new FlpParameter() { Data = RawValue }.NullableValue;
            return flpValue.HasValue ? flpValue.Value : SqlSingle.Null;
        }

        public SqlDouble ToDouble()
        {
            var doubleValue = new DoubleParameter() { Data = RawValue }.NullableValue;
            return doubleValue.HasValue ? doubleValue.Value : SqlDouble.Null;
        }

        public static RocMaster_Parameter Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedRocPointData = stringToParse.Value.Split(",".ToCharArray());
            var parsedRocParameter = new RocMaster_Parameter() { PointType = byte.Parse(parsedRocPointData[0]), LogicalNumber = byte.Parse(parsedRocPointData[1]), Parameter = byte.Parse(parsedRocPointData[2]) };

            parsedRocParameter.parameterType = (byte)((ParameterType)Enum.Parse(typeof(ParameterType), parsedRocPointData[3]));

            switch (parsedRocPointData[3])
            {
                case "AC3":
                    parsedRocParameter.rawValue = (new Ac3Parameter() { Value = parsedRocPointData[4] }).Data;
                    break;
                case "AC7":
                    parsedRocParameter.rawValue = (new Ac7Parameter() { Value = parsedRocPointData[4] }).Data;
                    break;
                case "AC10":
                    parsedRocParameter.rawValue = (new Ac10Parameter() { Value = parsedRocPointData[4] }).Data;
                    break;
                case "AC12":
                    parsedRocParameter.rawValue = (new Ac12Parameter() { Value = parsedRocPointData[4] }).Data;
                    break;
                case "AC20":
                    parsedRocParameter.rawValue = (new Ac20Parameter() { Value = parsedRocPointData[4] }).Data;
                    break;
                case "AC30":
                    parsedRocParameter.rawValue = (new Ac30Parameter() { Value = parsedRocPointData[4] }).Data;
                    break;
                case "AC40":
                    parsedRocParameter.rawValue = (new Ac40Parameter() { Value = parsedRocPointData[4] }).Data;
                    break;
                case "BIN":
                    parsedRocParameter.rawValue = (new BinParameter() { Value = byte.Parse(parsedRocPointData[4]) }).Data;
                    break;
                case "FL":
                    parsedRocParameter.rawValue = (new FlpParameter() { Value = float.Parse(parsedRocPointData[4]) }).Data;
                    break;
                case "DOUBLE":
                    parsedRocParameter.rawValue = (new DoubleParameter() { Value = double.Parse(parsedRocPointData[8]) }).Data;
                    break;
                case "INT16":
                    parsedRocParameter.rawValue = (new Int16Parameter() { Value = short.Parse(parsedRocPointData[4]) }).Data;
                    break;
                case "INT32":
                    parsedRocParameter.rawValue = (new Int32Parameter() { Value = int.Parse(parsedRocPointData[4]) }).Data;
                    break;
                case "Int8":
                    parsedRocParameter.rawValue = (new Int8Parameter() { Value = SByte.Parse(parsedRocPointData[4]) }).Data;
                    break;
                case "TLP":
                    {
                        var parsedTlp = parsedRocPointData[4].Split(".".ToCharArray());
                        parsedRocParameter.rawValue = (new TlpParameter() { Value = new Tlp(byte.Parse(parsedTlp[0]), byte.Parse(parsedTlp[1]), byte.Parse(parsedTlp[2])) }).Data;
                    }
                    break;
                case "UINT16":
                    parsedRocParameter.rawValue = (new UInt16Parameter() { Value = ushort.Parse(parsedRocPointData[4]) }).Data;
                    break;
                case "UINT32":
                    parsedRocParameter.rawValue = (new UInt32Parameter() { Value = uint.Parse(parsedRocPointData[4]) }).Data;
                    break;
                case "TIME":
                    parsedRocParameter.rawValue = (new TimeParameter() { Value = (new DateTime(1970, 01, 01).AddSeconds(uint.Parse(parsedRocPointData[4]))) }).Data;
                    break;
                case "UINT8":
                    parsedRocParameter.rawValue = (new UInt8Parameter() { Value = byte.Parse(parsedRocPointData[4]) }).Data;
                    break;
            }
            return parsedRocParameter;
        }

        public static RocMaster_Parameter ParseTlp(byte pointType, byte logicalNumber, byte parameter, byte pointTypeValue, byte logicalNumberValue, byte parameterValue)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.TLP, rawValue = (new TlpParameter() { Value = new Tlp(pointTypeValue, logicalNumberValue, parameterValue) }).Data };
        }

        public static RocMaster_Parameter ParseAc3(byte pointType, byte logicalNumber, byte parameter, string value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.AC3, rawValue = (new Ac3Parameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseAc7(byte pointType, byte logicalNumber, byte parameter, string value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.AC7, rawValue = (new Ac3Parameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseAc10(byte pointType, byte logicalNumber, byte parameter, string value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.AC10, rawValue = (new Ac10Parameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseAc12(byte pointType, byte logicalNumber, byte parameter, string value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.AC12, rawValue = (new Ac12Parameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseAc20(byte pointType, byte logicalNumber, byte parameter, string value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.AC20, rawValue = (new Ac20Parameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseAc30(byte pointType, byte logicalNumber, byte parameter, string value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.AC30, rawValue = (new Ac30Parameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseAc40(byte pointType, byte logicalNumber, byte parameter, string value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.AC40, rawValue = (new Ac40Parameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseBin(byte pointType, byte logicalNumber, byte parameter, byte value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.BIN, rawValue = (new BinParameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseInt8(byte pointType, byte logicalNumber, byte parameter, short value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.INT8, rawValue = (new Int8Parameter() { Value = Convert.ToSByte(value) }).Data };
        }

        public static RocMaster_Parameter ParseUInt8(byte pointType, byte logicalNumber, byte parameter, byte value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.UINT8, rawValue = (new UInt8Parameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseInt16(byte pointType, byte logicalNumber, byte parameter, short value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.INT16, rawValue = (new Int16Parameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseUInt16(byte pointType, byte logicalNumber, byte parameter, int value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.UINT16, rawValue = (new UInt16Parameter() { Value = Convert.ToUInt16(value) }).Data };
        }

        public static RocMaster_Parameter ParseInt32(byte pointType, byte logicalNumber, byte parameter, int value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.INT32, rawValue = (new Int32Parameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseUInt32(byte pointType, byte logicalNumber, byte parameter, long value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.UINT32, rawValue = (new UInt32Parameter() { Value = Convert.ToUInt32(value) }).Data };
        }

        public static RocMaster_Parameter ParseTime(byte pointType, byte logicalNumber, byte parameter, DateTime value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.TIME, rawValue = (new TimeParameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseFl(byte pointType, byte logicalNumber, byte parameter, float value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.FL, rawValue = (new FlpParameter() { Value = value }).Data };
        }

        public static RocMaster_Parameter ParseDouble(byte pointType, byte logicalNumber, byte parameter, double value)
        {
            return new RocMaster_Parameter() { PointType = pointType, LogicalNumber = logicalNumber, Parameter = parameter, parameterType = (byte)ParameterType.DOUBLE, rawValue = (new DoubleParameter() { Value = value }).Data };
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();

            if (!IsNull)
            {
                PointType = binaryReader.ReadByte();
                LogicalNumber = binaryReader.ReadByte();
                Parameter = binaryReader.ReadByte();
                parameterType = binaryReader.ReadByte();
                rawValue = new byte[binaryReader.ReadInt32()];
                binaryReader.Read(rawValue, 0, rawValue.Length);
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);

            if (!IsNull)
            {
                binaryWriter.Write(PointType);
                binaryWriter.Write(LogicalNumber);
                binaryWriter.Write(Parameter);
                binaryWriter.Write(parameterType);
                binaryWriter.Write(RawValue.Length);
                binaryWriter.Write(RawValue);
            }
        }

        #endregion

    }
}
