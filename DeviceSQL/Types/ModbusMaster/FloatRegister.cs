#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 13)]
    public struct ModbusMaster_FloatRegister : INullable, IBinarySerialize
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
                    data = new byte[4];
                }
                return data;
            }
            set
            {
                data = value.Value;
            }
        }

        public ModbusMaster_ModbusAddress Address
        {
            get;
            set;
        }

        public SqlBoolean ByteSwap
        {
            get;
            set;
        }

        public SqlBoolean WordSwap
        {
            get;
            set;
        }

        public bool IsNull
        {
            get;
            private set;
        }

        public SqlSingle Value
        {
            get
            {
                return new DeviceSQL.Device.Modbus.Data.FloatRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value), ByteSwap.Value, WordSwap.Value).Value;
            }
            set
            {
                Data = new DeviceSQL.Device.Modbus.Data.FloatRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value), ByteSwap.Value, WordSwap.Value) { Value = Convert.ToUInt16(value) }.Data;
            }
        }

        public static ModbusMaster_FloatRegister Null
        {
            get
            {
                return (new ModbusMaster_FloatRegister() { IsNull = true });
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
                return string.Format("Address=[{0}];Byte Swap={1};Word Swap={2};Value={3};", Address.ToString(), ByteSwap.Value, Value.Value);
            }
        }

        public static ModbusMaster_FloatRegister Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedFloatRegisterData = stringToParse.Value.Split(",".ToCharArray());
            var parsedFloatRegister = new ModbusMaster_FloatRegister() { Address = ModbusMaster_ModbusAddress.Parse(parsedFloatRegisterData[0]), ByteSwap = bool.Parse(parsedFloatRegisterData[1]), WordSwap = bool.Parse(parsedFloatRegisterData[2]), Value = Int32.Parse(parsedFloatRegisterData[3]) };
            return parsedFloatRegister;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();

            if (!IsNull)
            {
                Address.Read(binaryReader);
                ByteSwap = binaryReader.ReadBoolean();
                WordSwap = binaryReader.ReadBoolean();
                Data = binaryReader.ReadBytes(4);
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);

            if (!IsNull)
            {
                Address.Write(binaryWriter);
                binaryWriter.Write(ByteSwap.Value);
                binaryWriter.Write(WordSwap.Value);
                binaryWriter.Write(Data.Value, 0, 4);
            }
        }

        #endregion

    }
}   
