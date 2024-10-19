#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 8)]
    public struct ModbusMaster_DiscreteInputRegister : INullable, IBinarySerialize
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
                    data = new byte[1];
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

        public bool IsNull
        {
            get;
            private set;
        }

        public SqlBoolean Value
        {
            get
            {
                return BitConverter.ToBoolean(data, 0);
            }
            set
            {
                Data = BitConverter.GetBytes(value.Value);
            }
        }

        public static ModbusMaster_DiscreteInputRegister Null
        {
            get
            {
                return (new ModbusMaster_DiscreteInputRegister() { IsNull = true });
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
                return string.Format("Address=[{0}];Value={1};", Address.ToString(), Value.Value);
            }
        }

        public static ModbusMaster_DiscreteInputRegister Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedDiscreteInputRegisterData = stringToParse.Value.Split(",".ToCharArray());
            var parsedDiscreteInputRegister = new ModbusMaster_DiscreteInputRegister() { Address = ModbusMaster_ModbusAddress.Parse(parsedDiscreteInputRegisterData[0]), Value = bool.Parse(parsedDiscreteInputRegisterData[1]) };
            return parsedDiscreteInputRegister;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();

            if (!IsNull)
            {
                var address = new ModbusMaster_ModbusAddress();
                address.Read(binaryReader);
                Address = address;
                Data = binaryReader.ReadBytes(1);
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);

            if (!IsNull)
            {
                Address.Write(binaryWriter);
                binaryWriter.Write(Data.Value, 0, 1);
            }
        }

        #endregion

    }
}
