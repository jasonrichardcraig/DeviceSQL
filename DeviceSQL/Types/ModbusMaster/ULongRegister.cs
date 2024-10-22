#region Imported Types

using DeviceSQL.Helpers;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 13)]
    public struct ModbusMaster_ULongRegister : INullable, IBinarySerialize
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
                    data = new byte[8];
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

        public SqlInt64 Value // SQL has no ULong type, so we use Int64
        {
            get
            {
                // Ensure the data array is initialized and has at least 8 bytes for a 64-bit long
                if (data == null || data.Length < 4)
                {
                    return 0; // Handle null or uninitialized case
                }
                return BitConverter.ToUInt32(ByteSwapper.ApplySwaps(data, ByteSwap.Value, WordSwap.Value, 2), 0);
            }
            set
            {
                data = ByteSwapper.ApplySwaps(BitConverter.GetBytes(Convert.ToUInt32(value.Value)), ByteSwap.Value, WordSwap.Value, 2);
            }
        }


        public static ModbusMaster_ULongRegister Null
        {
            get
            {
                return (new ModbusMaster_ULongRegister() { IsNull = true });
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

        public static ModbusMaster_ULongRegister Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedULongRegisterData = stringToParse.Value.Split(",".ToCharArray());
            var parsedULongRegister = new ModbusMaster_ULongRegister() { Address = ModbusMaster_ModbusAddress.Parse(parsedULongRegisterData[0]), ByteSwap = bool.Parse(parsedULongRegisterData[1]), WordSwap = bool.Parse(parsedULongRegisterData[2]), Value = UInt32.Parse(parsedULongRegisterData[3]) };
            return parsedULongRegister;
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
