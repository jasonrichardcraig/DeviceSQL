using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 8)]
    public struct DiscreteInputRegister : INullable, IBinarySerialize
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

        public ModbusAddress Address
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
                return new DeviceSQL.Device.Modbus.Data.DiscreteInputRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value)).Value;
            }
            set
            {
                Data = new DeviceSQL.Device.Modbus.Data.DiscreteInputRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value)) { Value = value.Value }.Data;
            }
        }

        public static DiscreteInputRegister Null
        {
            get
            {
                return (new DiscreteInputRegister() { IsNull = true });
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

        public static DiscreteInputRegister Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedDiscreteInputRegisterData = stringToParse.Value.Split(",".ToCharArray());
            var parsedDiscreteInputRegister = new DiscreteInputRegister() { Address = ModbusAddress.Parse(parsedDiscreteInputRegisterData[0]), Value = bool.Parse(parsedDiscreteInputRegisterData[1]) };
            return parsedDiscreteInputRegister;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();

            if (!IsNull)
            {
                Address.Read(binaryReader);
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
