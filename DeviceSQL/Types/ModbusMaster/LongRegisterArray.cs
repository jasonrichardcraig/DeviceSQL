#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.Types.ModbusMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct ModbusMaster_LongRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ModbusMaster_LongRegister> longRegisters;

        #endregion

        #region Properties

        internal ModbusMaster_LongRegister this[int index]
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

        private List<ModbusMaster_LongRegister> LongRegisters
        {
            get
            {
                if (longRegisters == null)
                {
                    longRegisters = new List<ModbusMaster_LongRegister>();
                }
                return longRegisters;
            }
        }

        public static ModbusMaster_LongRegisterArray Null
        {
            get
            {
                return (new ModbusMaster_LongRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", LongRegisters.Select(longRegister => longRegister.ToString()));
        }

        public ModbusMaster_LongRegisterArray AddLongRegister(ModbusMaster_LongRegister longRegister)
        {
            LongRegisters.Add(longRegister);
            return this;
        }

        public static ModbusMaster_LongRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedLongRegisterArray = new ModbusMaster_LongRegisterArray()
            {
                longRegisters = new List<ModbusMaster_LongRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedLongRegisterArray.LongRegisters.Add(ModbusMaster_LongRegister.Parse(parsedString[i]));
            }

            return parsedLongRegisterArray;
        }

        public ModbusMaster_LongRegister GetLongRegister(SqlInt32 index)
        {
            return LongRegisters[index.Value];
        }

        public static ModbusMaster_LongRegisterArray Empty()
        {
            var longRegisterArray = new ModbusMaster_LongRegisterArray() { longRegisters = new List<ModbusMaster_LongRegister>() };
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
                    var longRegister = new ModbusMaster_LongRegister();
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

