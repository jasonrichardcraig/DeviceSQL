using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using System.Linq;
using System.IO;

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct ShortRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ShortRegister> shortRegisters;

        #endregion

        #region Properties

        internal ShortRegister this[int index]
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

        private List<ShortRegister> ShortRegisters
        {
            get
            {
                if (shortRegisters == null)
                {
                    shortRegisters = new List<ShortRegister>();
                }
                return shortRegisters;
            }
        }

        public static ShortRegisterArray Null
        {
            get
            {
                return (new ShortRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", ShortRegisters.Select(shortRegister => shortRegister.ToString()));
        }

        public ShortRegisterArray AddShortRegister(ShortRegister shortRegister)
        {
            ShortRegisters.Add(shortRegister);
            return this;
        }

        public static ShortRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedShortRegisterArray = new ShortRegisterArray()
            {
                shortRegisters = new List<ShortRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedShortRegisterArray.ShortRegisters.Add(ShortRegister.Parse(parsedString[i]));
            }

            return parsedShortRegisterArray;
        }

        public ShortRegister GetShortRegister(SqlInt32 index)
        {
            return ShortRegisters[index.Value];
        }

        public static ShortRegisterArray Empty()
        {
            var shortRegisterArray = new ShortRegisterArray() { shortRegisters = new List<ShortRegister>() };
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
                    var shortRegister = new ShortRegister();
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