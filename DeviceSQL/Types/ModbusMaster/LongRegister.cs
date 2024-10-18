#region Imported Types

using DeviceSQL.Helpers;
using DeviceSQL.Helpers.DeviceSQL.Helpers;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 13)]
    public struct ModbusMaster_LongRegister : INullable, IBinarySerialize
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
                if (data == null || data.Length < 8)
                {
                    return 0; // Handle null or uninitialized case
                }

                byte[] processedData = (byte[])data.Clone(); // Clone the data to avoid modifying the original array

                return BitConverter.ToInt64(ByteSwapper.ApplySwaps(data, ByteSwap.Value, WordSwap.Value, 8), 0);

            }
            set
            {
                // Convert the value to a byte array (Int64 is 8 bytes)
                var newData = BitConverter.GetBytes(value.Value);

                data = ByteSwapper.ApplySwaps(newData, ByteSwap.Value, WordSwap.Value, 8);
            }
        }


        public static ModbusMaster_LongRegister Null
        {
            get
            {
                return (new ModbusMaster_LongRegister() { IsNull = true });
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

        public static ModbusMaster_LongRegister Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedLongRegisterData = stringToParse.Value.Split(",".ToCharArray());
            var parsedLongRegister = new ModbusMaster_LongRegister() { Address = ModbusMaster_ModbusAddress.Parse(parsedLongRegisterData[0]), ByteSwap = bool.Parse(parsedLongRegisterData[1]), WordSwap = bool.Parse(parsedLongRegisterData[2]), Value = Int32.Parse(parsedLongRegisterData[3]) };
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
