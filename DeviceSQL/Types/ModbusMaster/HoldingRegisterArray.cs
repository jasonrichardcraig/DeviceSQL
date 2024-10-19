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
    public struct ModbusMaster_HoldingRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ModbusMaster_HoldingRegister> holdingRegisters;

        #endregion

        #region Properties

        internal ModbusMaster_HoldingRegister this[int index]
        {
            get
            {
                return HoldingRegisters[index];
            }
            set
            {
                HoldingRegisters[index] = value;
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
                return HoldingRegisters.Count;
            }
        }

        #endregion

        #region Helper Methods

        public SqlByte GetByte(SqlByte registerIndex, SqlInt32 offset)
        {
            return HoldingRegisters[registerIndex.Value].data[offset.Value];
        }

        public SqlBinary GetBytes(SqlByte registerIndex, SqlInt32 registerCount)
        {
            var bytes = new List<byte>();

            for (var i = 0; registerCount.Value > i; i++)
            {
                bytes.AddRange(HoldingRegisters[i].Data.Value);
            }

            return bytes.ToArray();

        }

        public SqlInt32 GetShort(SqlByte registerIndex, SqlBoolean byteSwap)
        {
            var address = HoldingRegisters[registerIndex.Value].Address;
            return (new ModbusMaster_ShortRegister() { Address = new ModbusMaster_ModbusAddress() { RelativeAddress = address.RelativeAddress, IsZeroBased = true }, ByteSwap = byteSwap, Data = holdingRegisters[Convert.ToByte(registerIndex.Value)].data.ToArray() }).Value;
        }

        public SqlSingle GetFloat(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            var address = HoldingRegisters[registerIndex.Value].Address;
            return (new ModbusMaster_FloatRegister() { Address = new ModbusMaster_ModbusAddress() { RelativeAddress = address.RelativeAddress, IsZeroBased = true }, ByteSwap = byteSwap, WordSwap = wordSwap, Data = holdingRegisters[Convert.ToByte(registerIndex.Value + 1)].data.Concat(holdingRegisters[Convert.ToByte(registerIndex.Value)].data).ToArray() }).Value;
        }

        public SqlInt64 GetLong(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            var address = HoldingRegisters[registerIndex.Value].Address;
            return (new ModbusMaster_LongRegister() { Address = new ModbusMaster_ModbusAddress() { RelativeAddress = address.RelativeAddress, IsZeroBased = true }, ByteSwap = byteSwap, WordSwap = wordSwap, Data = holdingRegisters[Convert.ToByte(registerIndex.Value + 1)].data.Concat(holdingRegisters[Convert.ToByte(registerIndex.Value)].data).ToArray() }).Value;
        }

        public SqlString GetString(SqlByte registerIndex, SqlByte length)
        {
            var data = new List<byte>();

            // Iterate through the registers and extract the appropriate number of bytes
            for (int i = registerIndex.Value; i < registerIndex.Value + (length.Value / 2); i++)
            {
                // Add both bytes from the 16-bit register, swapping the byte order (little-endian to big-endian)
                data.Add(holdingRegisters[i].data[1]); // Swap: Add second byte (low byte) first
                data.Add(holdingRegisters[i].data[0]); // Swap: Add first byte (high byte) second
            }

            // Handle the case where length is odd (add 1 more byte from the next register)
            if (length.Value % 2 != 0)
            {
                // Add the first byte (low byte) of the next register
                data.Add(holdingRegisters[registerIndex.Value + (length.Value / 2)].data[1]);
            }

            // Convert the byte list to a string (ModbusMaster_StringRegister) and return
            return new ModbusMaster_StringRegister { Data = data.ToArray() }.Value;
        }

        private List<ModbusMaster_HoldingRegister> HoldingRegisters
        {
            get
            {
                if (holdingRegisters == null)
                {
                    holdingRegisters = new List<ModbusMaster_HoldingRegister>();
                }
                return holdingRegisters;
            }
        }

        public static ModbusMaster_HoldingRegisterArray Null
        {
            get
            {
                return (new ModbusMaster_HoldingRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", HoldingRegisters.Select(holdingRegister => holdingRegister.ToString()));
        }

        public ModbusMaster_HoldingRegisterArray AddHoldingRegister(ModbusMaster_HoldingRegister holdingRegister)
        {
            HoldingRegisters.Add(holdingRegister);
            return this;
        }

        public static ModbusMaster_HoldingRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedHoldingRegisterArray = new ModbusMaster_HoldingRegisterArray()
            {
                holdingRegisters = new List<ModbusMaster_HoldingRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedHoldingRegisterArray.HoldingRegisters.Add(ModbusMaster_HoldingRegister.Parse(parsedString[i]));
            }

            return parsedHoldingRegisterArray;
        }

        public ModbusMaster_HoldingRegister GetHoldingRegister(SqlInt32 index)
        {
            return HoldingRegisters[index.Value];
        }

        public static ModbusMaster_HoldingRegisterArray Empty()
        {
            var holdingRegisterArray = new ModbusMaster_HoldingRegisterArray() { holdingRegisters = new List<ModbusMaster_HoldingRegister>() };
            return holdingRegisterArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            HoldingRegisters.Clear();
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
                    var holdingRegister = new ModbusMaster_HoldingRegister();
                    holdingRegister.Read(binaryReader);
                    HoldingRegisters.Add(holdingRegister);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; HoldingRegisters.Count > i; i++)
                {
                    HoldingRegisters[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
