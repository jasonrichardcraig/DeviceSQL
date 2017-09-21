#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.MODBUSMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 10)]
    public struct MODBUSMaster_ShortRegister : INullable, IBinarySerialize
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
                    data = new byte[2];
                }
                return data;
            }
            set
            {
                data = value.Value;
            }
        }

        public MODBUSMaster_MODBUSAddress Address
        {
            get;
            set;
        }

        public SqlBoolean ByteSwap
        {
            get;
            set;
        }

        public bool IsNull
        {
            get;
            private set;
        }

        public SqlInt32 Value
        {
            get
            {
                return new DeviceSQL.Device.MODBUS.Data.ShortRegister(new DeviceSQL.Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value), ByteSwap.Value).Value;
            }
            set
            {
                Data = new DeviceSQL.Device.MODBUS.Data.ShortRegister(new DeviceSQL.Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value), ByteSwap.Value) { Value = Convert.ToInt16((int)value.Value) }.Data;
            }
        }

        public static MODBUSMaster_ShortRegister Null
        {
            get
            {
                return (new MODBUSMaster_ShortRegister() { IsNull = true });
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
                return string.Format("Address=[{0}];Byte Swap={1};Value={2};", Address.ToString(), ByteSwap.Value, Value.Value);
            }
        }

        public static MODBUSMaster_ShortRegister Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedShortRegisterData = stringToParse.Value.Split(",".ToCharArray());
            var parsedShortRegister = new MODBUSMaster_ShortRegister() { Address = MODBUSMaster_MODBUSAddress.Parse(parsedShortRegisterData[0]), ByteSwap = bool.Parse(parsedShortRegisterData[1]), Value = UInt16.Parse(parsedShortRegisterData[2]) };
            return parsedShortRegister;
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
                Data = binaryReader.ReadBytes(2);
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);

            if (!IsNull)
            {
                Address.Write(binaryWriter);
                binaryWriter.Write(ByteSwap.Value);
                binaryWriter.Write(Data.Value, 0, 2);
            }
        }

        #endregion

    }
}