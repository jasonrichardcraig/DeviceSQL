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

        public SqlInt16 GetShort(SqlByte registerIndex, SqlBoolean byteSwap)
        {
            var address = HoldingRegisters[registerIndex.Value].Address;
            return new DeviceSQL.Device.Modbus.Data.ShortRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value)) { ByteSwap = byteSwap.Value, Data = holdingRegisters[Convert.ToByte(registerIndex.Value)].data }.Value;
        }

        public SqlSingle GetFloat(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            var address = HoldingRegisters[registerIndex.Value].Address;
            var floatRegisterValue = new DeviceSQL.Device.Modbus.Data.FloatRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value)) { ByteSwap = byteSwap.Value, WordSwap = wordSwap.Value, Data = holdingRegisters[Convert.ToByte(registerIndex.Value)].data.Concat(holdingRegisters[Convert.ToByte(registerIndex.Value + 1)].data).ToArray(), }.NullableValue;
            return floatRegisterValue ?? SqlSingle.Null;
        }

        public SqlInt32 GetLong(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            var address = HoldingRegisters[registerIndex.Value].Address;
            return new DeviceSQL.Device.Modbus.Data.LongRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value)) { ByteSwap = byteSwap.Value, WordSwap = wordSwap.Value, Data = holdingRegisters[Convert.ToByte(registerIndex.Value)].data.Concat(holdingRegisters[Convert.ToByte(registerIndex.Value + 1)].data).ToArray(), }.Value;
        }

        public SqlString GetString(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap, SqlByte length)
        {
            var address = HoldingRegisters[registerIndex.Value].Address;
            return new DeviceSQL.Device.Modbus.Data.StringRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value), length.Value) { ByteSwap = byteSwap.Value, WordSwap = wordSwap.Value, Data = holdingRegisters[Convert.ToByte(registerIndex.Value)].data.Concat(holdingRegisters[Convert.ToByte(registerIndex.Value + 1)].data).ToArray(), }.Value;
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
