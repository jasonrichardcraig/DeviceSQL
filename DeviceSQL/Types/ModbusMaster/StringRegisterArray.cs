using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using System.Linq;
using System.IO;

namespace DeviceSQL.ModbusMaster
{
    public partial class UserDefinedTypes
    {
        [Serializable()]
        [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
        public struct StringRegisterArray : INullable, IBinarySerialize
        {

            #region Fields

            internal List<StringRegister> stringRegisters;

            #endregion

            #region Properties

            internal StringRegister this[int index]
            {
                get
                {
                    return StringRegisters[index];
                }
                set
                {
                    StringRegisters[index] = value;
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
                    return StringRegisters.Count;
                }
            }

            #endregion

            #region Helper Methods

            private List<StringRegister> StringRegisters
            {
                get
                {
                    if (stringRegisters == null)
                    {
                        stringRegisters = new List<StringRegister>();
                    }
                    return stringRegisters;
                }
            }

            public static StringRegisterArray Null
            {
                get
                {
                    return (new StringRegisterArray() { IsNull = true });
                }
            }

            public override string ToString()
            {
                return string.Join("|", StringRegisters.Select(stringRegister => stringRegister.ToString()));
            }

            public StringRegisterArray AddStringRegister(StringRegister stringRegister)
            {
                StringRegisters.Add(stringRegister);
                return this;
            }

            public static StringRegisterArray Parse(SqlString stringToParse)
            {
                if (stringToParse.IsNull)
                {
                    return Null;
                }

                var parsedStringRegisterArray = new StringRegisterArray()
                {
                    stringRegisters = new List<StringRegister>()
                };

                var parsedString = stringToParse.Value.Split("|".ToCharArray());

                for (var i = 0; parsedString.Length > i; i++)
                {
                    parsedStringRegisterArray.StringRegisters.Add(StringRegister.Parse(parsedString[i]));
                }

                return parsedStringRegisterArray;
            }

            public StringRegister GetStringRegister(SqlInt32 index)
            {
                return StringRegisters[index.Value];
            }

            public static StringRegisterArray Empty()
            {
                var stringRegisterArray = new StringRegisterArray() { stringRegisters = new List<StringRegister>() };
                return stringRegisterArray;
            }

            #endregion

            #region Serialization Methods

            public void Read(BinaryReader binaryReader)
            {
                StringRegisters.Clear();
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
                        var stringRegister = new StringRegister();
                        stringRegister.Read(binaryReader);
                        StringRegisters.Add(stringRegister);
                    }
                }

            }

            public void Write(BinaryWriter binaryWriter)
            {
                binaryWriter.Write(IsNull);
                binaryWriter.Write(Length);

                if (Length > 0)
                {
                    for (var i = 0; StringRegisters.Count > i; i++)
                    {
                        StringRegisters[i].Write(binaryWriter);
                    }
                }
            }

            #endregion

        }
    }
}
