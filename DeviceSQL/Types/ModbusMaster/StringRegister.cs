using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;

namespace DeviceSQL.Types.MODBUSMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 264)]
    public struct MODBUSMaster_StringRegister : INullable, IBinarySerialize
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
                    data = new byte[Length.Value];
                }
                return data;
            }
            set
            {
                data = value.Value;
                Length = Convert.ToByte(data.Length);
            }
        }

        public SqlByte Length
        {
            get;
            set;
        }

        public MODBUSMaster_MODBUSAddress Address
        {
            get;
            set;
        }

        public bool IsNull
        {
            get;
            private set;
        }

        public SqlString Value
        {
            get
            {
                return new DeviceSQL.Device.MODBUS.Data.StringRegister(new DeviceSQL.Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value), Length.Value).Value;
            }
            set
            {
                Data = new DeviceSQL.Device.MODBUS.Data.StringRegister(new DeviceSQL.Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(Address.RelativeAddress), Address.IsZeroBased.Value), Length.Value) { Value = value.Value }.Data;
            }
        }

        public static MODBUSMaster_StringRegister Null
        {
            get
            {
                return (new MODBUSMaster_StringRegister() { IsNull = true });
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
                return string.Format("Address=[{0}];Length={1};Value={2};", Address.ToString(), Length.Value, Value.Value);
            }
        }

        public static MODBUSMaster_StringRegister Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedStringRegisterData = stringToParse.Value.Split(",".ToCharArray());
            var parsedStringRegister = new MODBUSMaster_StringRegister() { Address = MODBUSMaster_MODBUSAddress.Parse(parsedStringRegisterData[0]), Length = byte.Parse(parsedStringRegisterData[1]), Value = parsedStringRegisterData[2] };
            return parsedStringRegister;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();

            if (!IsNull)
            {
                Address.Read(binaryReader);
                Length = binaryReader.ReadByte();
                Data = binaryReader.ReadBytes(Length.Value);
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);

            if (!IsNull)
            {
                Address.Write(binaryWriter);
                binaryWriter.Write(Length.Value);
                binaryWriter.Write(Data.Value, 0, Length.Value);
            }
        }

        #endregion

    }
}
    
