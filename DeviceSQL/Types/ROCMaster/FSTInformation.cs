using DeviceSQL.Device.ROC.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

namespace DeviceSQL.Types.ROCMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct ROCMaster_FSTInformation : INullable, IBinarySerialize
    {

        #region Fields

        private byte fstNumber;
        private string version;
        private string description;
        private int fstCodeLength;
        private byte[] fstCode;

        #endregion

        #region Properties

        public bool IsNull
        {
            get;
            internal set;
        }

        public static ROCMaster_FSTInformation Null
        {
            get
            {
                return new ROCMaster_FSTInformation() { IsNull = true };
            }
        }

        public byte FSTNumber
        {
            get
            {
                return fstNumber;
            }
            set
            {
                fstNumber = value;
            }
        }

        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public byte[] FSTCode
        {
            get
            {
                if (fstCode == null)
                {
                    fstCode = new byte[0];
                }
                return fstCode;
            }
            set
            {
                fstCode = value;
            }
        }

        public int Length
        {
            get
            {
                return fstCode.Length;
            }
        }

        public string SHA256Hash
        {
            get
            {
                return IO.Channels.HexConverter.ToHexString(System.Security.Cryptography.SHA256.Create().ComputeHash(fstCode));
            }
        }

        #endregion

        #region Helper Methods

        public static ROCMaster_FSTInformation Parse(SqlString stringToParse)
        {
            var parsedString = stringToParse.Value.Split(",".ToCharArray());
            var fstNumber = byte.Parse(parsedString[0]);
            var version = parsedString[1];
            var description = parsedString[2];
            var fstCode = Convert.FromBase64String(parsedString[3]);
            return new ROCMaster_FSTInformation() { fstNumber = fstNumber, version = version, description = description, fstCode = fstCode };
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", FSTNumber, Version, Description, Convert.ToBase64String(FSTCode));
        }


        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();
            if (!IsNull)
            {
                fstNumber = binaryReader.ReadByte();
                version = binaryReader.ReadString();
                description = binaryReader.ReadString();
                var fstCodeLength = binaryReader.ReadUInt16();
                fstCode = binaryReader.ReadBytes(fstCodeLength);
            }
        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            if (!IsNull)
            {
                binaryWriter.Write(fstNumber);
                binaryWriter.Write(version);
                binaryWriter.Write(description);
                binaryWriter.Write(Convert.ToUInt16(fstCode.Length));
                binaryWriter.Write(fstCode);
            }
        }

        #endregion

    }
}
