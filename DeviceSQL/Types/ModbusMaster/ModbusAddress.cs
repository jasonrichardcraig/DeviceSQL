#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 6)]
    public struct ModbusMaster_ModbusAddress : INullable, IBinarySerialize
    {

        #region Properties

        public SqlInt32 AbsoluteAddress
        {
            get
            {
                if (this.IsZeroBased)
                {
                    return (ushort)(this.RelativeAddress - 1);
                }
                else
                {
                    return (ushort)(this.RelativeAddress);
                }
            }
        }

        public SqlInt32 RelativeAddress
        {
            get;
            set;
        }

        public SqlBoolean IsZeroBased
        {
            get;
            set;
        }

        public bool IsNull
        {
            get;
            private set;
        }

        public static ModbusMaster_ModbusAddress Null
        {
            get
            {
                return (new ModbusMaster_ModbusAddress() { IsNull = true });
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
                return string.Format("Is Zero Based={0};Relative Address={1};Absolute Address={2}", IsZeroBased, RelativeAddress, AbsoluteAddress);
            }
        }

        public static ModbusMaster_ModbusAddress Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedModbusAddressData = stringToParse.Value.Split(";".ToCharArray());
            var parsedModbusAddress = new ModbusMaster_ModbusAddress() { IsZeroBased = bool.Parse(parsedModbusAddressData[0]), RelativeAddress = int.Parse(parsedModbusAddressData[1]) };
            return parsedModbusAddress;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();

            if (!IsNull)
            {
                IsZeroBased = binaryReader.ReadBoolean();
                RelativeAddress = binaryReader.ReadInt32();
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);

            if (!IsNull)
            {
                binaryWriter.Write(IsZeroBased.Value);
                binaryWriter.Write(RelativeAddress.Value);
            }
        }

        #endregion

    }
}