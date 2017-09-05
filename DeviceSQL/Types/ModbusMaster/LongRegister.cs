using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 13)]
    public struct LongRegister : INullable, IBinarySerialize
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

        public ModbusAddress Address
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

        public SqlInt32 Value
        {
            get
            {
                return new DeviceSQL.Device.Modbus.Data.LongRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value), ByteSwap.Value, WordSwap.Value).Value;
            }
            set
            {
                Data = new DeviceSQL.Device.Modbus.Data.LongRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value), ByteSwap.Value, WordSwap.Value) { Value = Convert.ToUInt16(value) }.Data;
            }
        }

        public static LongRegister Null
        {
            get
            {
                return (new LongRegister() { IsNull = true });
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

        public static LongRegister Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedLongRegisterData = stringToParse.Value.Split(",".ToCharArray());
            var parsedLongRegister = new LongRegister() { Address = ModbusAddress.Parse(parsedLongRegisterData[0]), ByteSwap = bool.Parse(parsedLongRegisterData[1]), WordSwap = bool.Parse(parsedLongRegisterData[2]), Value = Int32.Parse(parsedLongRegisterData[3]) };
            return parsedLongRegister;
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
