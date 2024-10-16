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
    public struct ModbusMaster_CoilRegister : INullable, IBinarySerialize
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
                return new DeviceSQL.Device.Modbus.Data.CoilRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value)).Value;
            }
            set
            {
                Data = new DeviceSQL.Device.Modbus.Data.CoilRegister(new DeviceSQL.Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value)) { Value = value.Value }.Data;
            }
        }

        public static ModbusMaster_CoilRegister Null
        {
            get
            {
                return (new ModbusMaster_CoilRegister() { IsNull = true });
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

        public static ModbusMaster_CoilRegister Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedCoilRegisterData = stringToParse.Value.Split(",".ToCharArray());
            var parsedCoilRegister = new ModbusMaster_CoilRegister() { Address = ModbusMaster_ModbusAddress.Parse(parsedCoilRegisterData[0]), Value = bool.Parse(parsedCoilRegisterData[1]) };
            return parsedCoilRegister;
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