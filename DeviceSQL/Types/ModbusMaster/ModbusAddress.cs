#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.Types.MODBUSMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 6)]
    public struct MODBUSMaster_MODBUSAddress : INullable, IBinarySerialize
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

        public static MODBUSMaster_MODBUSAddress Null
        {
            get
            {
                return (new MODBUSMaster_MODBUSAddress() { IsNull = true });
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

        public static MODBUSMaster_MODBUSAddress Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedMODBUSAddressData = stringToParse.Value.Split("|".ToCharArray());
            var parsedMODBUSAddress = new MODBUSMaster_MODBUSAddress() { IsZeroBased = bool.Parse(parsedMODBUSAddressData[0]), RelativeAddress = int.Parse(parsedMODBUSAddressData[1]) };
            return parsedMODBUSAddress;
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