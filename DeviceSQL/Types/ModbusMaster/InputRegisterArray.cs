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

        public SqlInt16 GetShort(SqlByte registerIndex, SqlBoolean byteSwap)
        {
            var address = InputRegisters[registerIndex.Value].Address;
            return new DeviceSQL.Device.Modbus.Data.ShortRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value)) { ByteSwap = byteSwap.Value, Data = inputRegisters[Convert.ToByte(registerIndex.Value)].data }.Value;
        }

        public SqlSingle GetFloat(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            var address = InputRegisters[registerIndex.Value].Address;
            var floatRegisterValue = new DeviceSQL.Device.Modbus.Data.FloatRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value)) { ByteSwap = byteSwap.Value, WordSwap = wordSwap.Value, Data = inputRegisters[Convert.ToByte(registerIndex.Value)].data.Concat(inputRegisters[Convert.ToByte(registerIndex.Value + 1)].data).ToArray(), }.NullableValue;
            return floatRegisterValue ?? SqlSingle.Null;
        }

        public SqlInt32 GetLong(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap)
        {
            var address = InputRegisters[registerIndex.Value].Address;
            return new DeviceSQL.Device.Modbus.Data.LongRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value)) { ByteSwap = byteSwap.Value, WordSwap = wordSwap.Value, Data = inputRegisters[Convert.ToByte(registerIndex.Value)].data.Concat(inputRegisters[Convert.ToByte(registerIndex.Value + 1)].data).ToArray(), }.Value;
        }

        public SqlString GetString(SqlByte registerIndex, SqlBoolean byteSwap, SqlBoolean wordSwap, SqlByte length)
        {
            var address = InputRegisters[registerIndex.Value].Address;
            return new DeviceSQL.Device.Modbus.Data.StringRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(address.RelativeAddress.Value), address.IsZeroBased.Value), length.Value) { ByteSwap = byteSwap.Value, WordSwap = wordSwap.Value, Data = inputRegisters[Convert.ToByte(registerIndex.Value)].data.Concat(inputRegisters[Convert.ToByte(registerIndex.Value + 1)].data).ToArray(), }.Value;
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