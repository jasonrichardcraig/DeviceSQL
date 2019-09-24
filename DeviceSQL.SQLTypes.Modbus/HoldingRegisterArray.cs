#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.SQLTypes.Modbus
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct HoldingRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<HoldingRegister> holdingRegisters;

        #endregion

        #region Properties

        internal HoldingRegister this[int index]
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

        public SqlInt16 GetShort(SqlByte registerIndex, SqlBoolean byteSwap)
        {
            var address = HoldingRegisters[registerIndex.Value].Address;
            return new DeviceSQL.Device.MODBUS.Data.ShortRegister(new DeviceSQL.Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value)) { ByteSwap = byteSwap.Value, Data = holdingRegisters[Convert.ToByte(registerIndex.Value)].data }.Value;
        }

        public SqlSingle GetFloat(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            var address = HoldingRegisters[registerIndex.Value].Address;
            var floatRegisterValue = new DeviceSQL.Device.MODBUS.Data.FloatRegister(new DeviceSQL.Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value)) { ByteSwap = byteSwap.Value, WordSwap = wordSwap.Value, Data = holdingRegisters[Convert.ToByte(registerIndex.Value)].data.Concat(holdingRegisters[Convert.ToByte(registerIndex.Value + 1)].data).ToArray(), }.NullableValue;
            return floatRegisterValue ?? SqlSingle.Null;
        }

        public SqlInt32 GetLong(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            var address = HoldingRegisters[registerIndex.Value].Address;
            return new DeviceSQL.Device.MODBUS.Data.LongRegister(new DeviceSQL.Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value)) { ByteSwap = byteSwap.Value, WordSwap = wordSwap.Value, Data = holdingRegisters[Convert.ToByte(registerIndex.Value)].data.Concat(holdingRegisters[Convert.ToByte(registerIndex.Value + 1)].data).ToArray(), }.Value;
        }

        public SqlString GetString(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap, SqlByte length)
        {
            var address = HoldingRegisters[registerIndex.Value].Address;
            return new DeviceSQL.Device.MODBUS.Data.StringRegister(new DeviceSQL.Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value), length.Value) { ByteSwap = byteSwap.Value, WordSwap = wordSwap.Value, Data = holdingRegisters[Convert.ToByte(registerIndex.Value)].data.Concat(holdingRegisters[Convert.ToByte(registerIndex.Value + 1)].data).ToArray(), }.Value;
        }

        private List<HoldingRegister> HoldingRegisters
        {
            get
            {
                if (holdingRegisters == null)
                {
                    holdingRegisters = new List<HoldingRegister>();
                }
                return holdingRegisters;
            }
        }

        public static HoldingRegisterArray Null
        {
            get
            {
                return (new HoldingRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", HoldingRegisters.Select(holdingRegister => holdingRegister.ToString()));
        }

        public HoldingRegisterArray AddHoldingRegister(HoldingRegister holdingRegister)
        {
            HoldingRegisters.Add(holdingRegister);
            return this;
        }

        public static HoldingRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedHoldingRegisterArray = new HoldingRegisterArray()
            {
                holdingRegisters = new List<HoldingRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedHoldingRegisterArray.HoldingRegisters.Add(HoldingRegister.Parse(parsedString[i]));
            }

            return parsedHoldingRegisterArray;
        }

        public HoldingRegister GetHoldingRegister(SqlInt32 index)
        {
            return HoldingRegisters[index.Value];
        }

        public static HoldingRegisterArray Empty()
        {
            var holdingRegisterArray = new HoldingRegisterArray() { holdingRegisters = new List<HoldingRegister>() };
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
                    var holdingRegister = new HoldingRegister();
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
