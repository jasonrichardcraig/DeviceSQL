#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct ModbusMaster_InputRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ModbusMaster_InputRegister> inputRegisters;

        #endregion

        #region Properties

        internal ModbusMaster_InputRegister this[int index]
        {
            get
            {
                return InputRegisters[index];
            }
            set
            {
                InputRegisters[index] = value;
            }
        }

        public bool IsNull
        {
            get;
            internal set;
        }

        public int Length
        {
            get
            {
                return InputRegisters.Count;
            }
        }

        #endregion

        #region Helper Methods

        public SqlByte GetByte(SqlByte registerIndex, SqlInt32 offset)
        {
            return InputRegisters[registerIndex.Value].data[offset.Value];
        }

        public SqlBinary GetBytes(SqlByte registerIndex, SqlInt32 registerCount)
        {
            var bytes = new List<byte>();

            for (var i = 0; registerCount.Value > i; i++)
            {
                bytes.AddRange(InputRegisters[i].Data.Value);
            }

            return bytes.ToArray();

        }

        public SqlInt32 GetShort(SqlByte registerIndex, SqlBoolean byteSwap)
        {
            var address = InputRegisters[registerIndex.Value].Address;
            return (new ModbusMaster_ShortRegister() { Address = new ModbusMaster_ModbusAddress() { RelativeAddress = address.RelativeAddress, IsZeroBased = true }, ByteSwap = byteSwap, Data = inputRegisters[Convert.ToByte(registerIndex.Value)].data.ToArray() }).Value;
        }

        public SqlSingle GetFloat(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            var address = InputRegisters[registerIndex.Value].Address;
            return (new ModbusMaster_FloatRegister() { Address = new ModbusMaster_ModbusAddress() { RelativeAddress = address.RelativeAddress, IsZeroBased = true }, ByteSwap = byteSwap, WordSwap = wordSwap, Data = inputRegisters[Convert.ToByte(registerIndex.Value + 1)].data.Concat(inputRegisters[Convert.ToByte(registerIndex.Value)].data).ToArray() }).Value;
        }

        public SqlInt64 GetLong(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            var address = InputRegisters[registerIndex.Value].Address;
            return (new ModbusMaster_LongRegister() { Address = new ModbusMaster_ModbusAddress() { RelativeAddress = address.RelativeAddress, IsZeroBased = true }, ByteSwap = byteSwap, WordSwap = wordSwap, Data = inputRegisters[Convert.ToByte(registerIndex.Value + 1)].data.Concat(inputRegisters[Convert.ToByte(registerIndex.Value)].data).ToArray() }).Value;
        }

        public SqlString GetString(SqlByte registerIndex, SqlByte length)
        {
            var data = new List<byte>();

            // Iterate through the registers and extract the appropriate number of bytes
            for (int i = registerIndex.Value; i < registerIndex.Value + (length.Value / 2); i++)
            {
                // Add both bytes from the 16-bit register, swapping the byte order (little-endian to big-endian)
                data.Add(inputRegisters[i].data[1]); // Swap: Add second byte (low byte) first
                data.Add(inputRegisters[i].data[0]); // Swap: Add first byte (high byte) second
            }

            // Handle the case where length is odd (add 1 more byte from the next register)
            if (length.Value % 2 != 0)
            {
                // Add the first byte (low byte) of the next register
                data.Add(inputRegisters[registerIndex.Value + (length.Value / 2)].data[1]);
            }

            // Convert the byte list to a string (ModbusMaster_StringRegister) and return
            return new ModbusMaster_StringRegister { Data = data.ToArray() }.Value;
        }
        private List<ModbusMaster_InputRegister> InputRegisters
        {
            get
            {
                if (inputRegisters == null)
                {
                    inputRegisters = new List<ModbusMaster_InputRegister>();
                }
                return inputRegisters;
            }
        }

        public static ModbusMaster_InputRegisterArray Null
        {
            get
            {
                return (new ModbusMaster_InputRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", InputRegisters.Select(inputRegister => inputRegister.ToString()));
        }

        public ModbusMaster_InputRegisterArray AddInputRegister(ModbusMaster_InputRegister inputRegister)
        {
            InputRegisters.Add(inputRegister);
            return this;
        }

        public static ModbusMaster_InputRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedInputRegisterArray = new ModbusMaster_InputRegisterArray()
            {
                inputRegisters = new List<ModbusMaster_InputRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedInputRegisterArray.InputRegisters.Add(ModbusMaster_InputRegister.Parse(parsedString[i]));
            }

            return parsedInputRegisterArray;
        }

        public ModbusMaster_InputRegister GetInputRegister(SqlInt32 index)
        {
            return InputRegisters[index.Value];
        }

        public static ModbusMaster_InputRegisterArray Empty()
        {
            var inputRegisterArray = new ModbusMaster_InputRegisterArray() { inputRegisters = new List<ModbusMaster_InputRegister>() };
            return inputRegisterArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            InputRegisters.Clear();
            IsNull = binaryReader.ReadBoolean();

            if (IsNull)
            {
                return;
            }
            else
            {
                var length = binaryReader.ReadInt32();

                for (var i = 0; length > i; i++)
                {
                    var inputRegister = new ModbusMaster_InputRegister();
                    inputRegister.Read(binaryReader);
                    InputRegisters.Add(inputRegister);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; InputRegisters.Count > i; i++)
                {
                    InputRegisters[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}