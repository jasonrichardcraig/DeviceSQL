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
    public struct LongRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<LongRegister> longRegisters;

        #endregion

        #region Properties

        internal LongRegister this[int index]
        {
            get
            {
                return LongRegisters[index];
            }
            set
            {
                LongRegisters[index] = value;
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
                return LongRegisters.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<LongRegister> LongRegisters
        {
            get
            {
                if (longRegisters == null)
                {
                    longRegisters = new List<LongRegister>();
                }
                return longRegisters;
            }
        }

        public static LongRegisterArray Null
        {
            get
            {
                return (new LongRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", LongRegisters.Select(longRegister => longRegister.ToString()));
        }

        public LongRegisterArray AddLongRegister(LongRegister longRegister)
        {
            LongRegisters.Add(longRegister);
            return this;
        }

        public static LongRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedLongRegisterArray = new LongRegisterArray()
            {
                longRegisters = new List<LongRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedLongRegisterArray.LongRegisters.Add(LongRegister.Parse(parsedString[i]));
            }

            return parsedLongRegisterArray;
        }

        public LongRegister GetLongRegister(SqlInt32 index)
        {
            return LongRegisters[index.Value];
        }

        public static LongRegisterArray Empty()
        {
            var longRegisterArray = new LongRegisterArray() { longRegisters = new List<LongRegister>() };
            return longRegisterArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            LongRegisters.Clear();
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
                    var longRegister = new LongRegister();
                    longRegister.Read(binaryReader);
                    LongRegisters.Add(longRegister);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; LongRegisters.Count > i; i++)
                {
                    LongRegisters[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}

