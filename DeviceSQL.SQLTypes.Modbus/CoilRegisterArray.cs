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
    public struct CoilRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<CoilRegister> coilRegisters;

        #endregion

        #region Properties

        internal CoilRegister this[int index]
        {
            get
            {
                return CoilRegisters[index];
            }
            set
            {
                CoilRegisters[index] = value;
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
                return CoilRegisters.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<CoilRegister> CoilRegisters
        {
            get
            {
                if (coilRegisters == null)
                {
                    coilRegisters = new List<CoilRegister>();
                }
                return coilRegisters;
            }
        }

        public static CoilRegisterArray Null
        {
            get
            {
                return (new CoilRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", CoilRegisters.Select(coilRegister => coilRegister.ToString()));
        }

        public CoilRegisterArray AddCoilRegister(CoilRegister coilRegister)
        {
            CoilRegisters.Add(coilRegister);
            return this;
        }

        public static CoilRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedCoilRegisterArray = new CoilRegisterArray()
            {
                coilRegisters = new List<CoilRegister>()
            };
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedCoilRegisterArray.CoilRegisters.Add(CoilRegister.Parse(parsedString[i]));
            }

            return parsedCoilRegisterArray;
        }

        public CoilRegister GetCoilRegister(SqlInt32 index)
        {
            return CoilRegisters[index.Value];
        }

        public static CoilRegisterArray Empty()
        {
            var coilRegisterArray = new CoilRegisterArray() { coilRegisters = new List<CoilRegister>() };
            return coilRegisterArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            CoilRegisters.Clear();
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
                    var coilRegister = new CoilRegister();
                    coilRegister.Read(binaryReader);
                    CoilRegisters.Add(coilRegister);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; CoilRegisters.Count > i; i++)
                {
                    CoilRegisters[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}