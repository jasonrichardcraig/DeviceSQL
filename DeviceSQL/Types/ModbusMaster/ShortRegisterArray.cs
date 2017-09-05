#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.Types.MODBUSMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct MODBUSMaster_ShortRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<MODBUSMaster_ShortRegister> shortRegisters;

        #endregion

        #region Properties

        internal MODBUSMaster_ShortRegister this[int index]
        {
            get
            {
                return ShortRegisters[index];
            }
            set
            {
                ShortRegisters[index] = value;
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
                return ShortRegisters.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<MODBUSMaster_ShortRegister> ShortRegisters
        {
            get
            {
                if (shortRegisters == null)
                {
                    shortRegisters = new List<MODBUSMaster_ShortRegister>();
                }
                return shortRegisters;
            }
        }

        public static MODBUSMaster_ShortRegisterArray Null
        {
            get
            {
                return (new MODBUSMaster_ShortRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", ShortRegisters.Select(shortRegister => shortRegister.ToString()));
        }

        public MODBUSMaster_ShortRegisterArray AddShortRegister(MODBUSMaster_ShortRegister shortRegister)
        {
            ShortRegisters.Add(shortRegister);
            return this;
        }

        public static MODBUSMaster_ShortRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedShortRegisterArray = new MODBUSMaster_ShortRegisterArray()
            {
                shortRegisters = new List<MODBUSMaster_ShortRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedShortRegisterArray.ShortRegisters.Add(MODBUSMaster_ShortRegister.Parse(parsedString[i]));
            }

            return parsedShortRegisterArray;
        }

        public MODBUSMaster_ShortRegister GetShortRegister(SqlInt32 index)
        {
            return ShortRegisters[index.Value];
        }

        public static MODBUSMaster_ShortRegisterArray Empty()
        {
            var shortRegisterArray = new MODBUSMaster_ShortRegisterArray() { shortRegisters = new List<MODBUSMaster_ShortRegister>() };
            return shortRegisterArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            ShortRegisters.Clear();
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
                    var shortRegister = new MODBUSMaster_ShortRegister();
                    shortRegister.Read(binaryReader);
                    ShortRegisters.Add(shortRegister);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; ShortRegisters.Count > i; i++)
                {
                    ShortRegisters[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}