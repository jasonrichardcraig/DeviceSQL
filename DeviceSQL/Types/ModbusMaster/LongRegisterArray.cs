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
    public struct MODBUSMaster_LongRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<MODBUSMaster_LongRegister> longRegisters;

        #endregion

        #region Properties

        internal MODBUSMaster_LongRegister this[int index]
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

        private List<MODBUSMaster_LongRegister> LongRegisters
        {
            get
            {
                if (longRegisters == null)
                {
                    longRegisters = new List<MODBUSMaster_LongRegister>();
                }
                return longRegisters;
            }
        }

        public static MODBUSMaster_LongRegisterArray Null
        {
            get
            {
                return (new MODBUSMaster_LongRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", LongRegisters.Select(longRegister => longRegister.ToString()));
        }

        public MODBUSMaster_LongRegisterArray AddLongRegister(MODBUSMaster_LongRegister longRegister)
        {
            LongRegisters.Add(longRegister);
            return this;
        }

        public static MODBUSMaster_LongRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedLongRegisterArray = new MODBUSMaster_LongRegisterArray()
            {
                longRegisters = new List<MODBUSMaster_LongRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedLongRegisterArray.LongRegisters.Add(MODBUSMaster_LongRegister.Parse(parsedString[i]));
            }

            return parsedLongRegisterArray;
        }

        public MODBUSMaster_LongRegister GetLongRegister(SqlInt32 index)
        {
            return LongRegisters[index.Value];
        }

        public static MODBUSMaster_LongRegisterArray Empty()
        {
            var longRegisterArray = new MODBUSMaster_LongRegisterArray() { longRegisters = new List<MODBUSMaster_LongRegister>() };
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
                    var longRegister = new MODBUSMaster_LongRegister();
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

