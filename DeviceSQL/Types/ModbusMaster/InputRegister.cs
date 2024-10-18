#region Imported Types

using DeviceSQL.Helpers.DeviceSQL.Helpers;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 10)]
    public struct ModbusMaster_InputRegister : INullable, IBinarySerialize
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
                    data = new byte[2];
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

        public bool IsNull
        {
            get;
            private set;
        }

        public SqlInt32 Value // SQL has no UShort type, so we use Int32
        {
            get
            {
                // Ensure the data array is initialized and has at least 2 bytes
                if (data == null || data.Length < 2)
                {
                    return 0; // Handle null or uninitialized case
                }
                return BitConverter.ToUInt16(ByteSwapper.ApplySwaps(data, ByteSwap.Value, false, 2), 0);
            }
            set
            {
                // Convert the value to a 2-byte array (UInt16)
                var newData = BitConverter.GetBytes((ushort)value.Value);

                data = ByteSwapper.ApplySwaps(newData, ByteSwap.Value, false, 2);
            }
        }

        public static ModbusMaster_InputRegister Null
        {
            get
            {
                return (new ModbusMaster_InputRegister() { IsNull = true });
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
                return string.Format("Address=[{0}];Byte Swap={1};Value={2};", Address.ToString(), ByteSwap.Value, Value.Value);
            }
        }

        public static ModbusMaster_InputRegister Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedInputRegisterData = stringToParse.Value.Split(",".ToCharArray());
            var parsedInputRegister = new ModbusMaster_InputRegister() { Address = ModbusMaster_ModbusAddress.Parse(parsedInputRegisterData[0]), ByteSwap = bool.Parse(parsedInputRegisterData[1]), Value = UInt16.Parse(parsedInputRegisterData[2]) };
            return parsedInputRegister;
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
                Data = binaryReader.ReadBytes(2);
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);

            if (!IsNull)
            {
                Address.Write(binaryWriter);
                binaryWriter.Write(ByteSwap.Value);
                binaryWriter.Write(Data.Value, 0, 2);
            }
        }

        #endregion

    }
}
