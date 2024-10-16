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
    public struct ModbusMaster_ShortRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ModbusMaster_ShortRegister> shortRegisters;

        #endregion

        #region Properties

        internal ModbusMaster_ShortRegister this[int index]
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

        private List<ModbusMaster_ShortRegister> ShortRegisters
        {
            get
            {
                if (shortRegisters == null)
                {
                    shortRegisters = new List<ModbusMaster_ShortRegister>();
                }
                return shortRegisters;
            }
        }

        public static ModbusMaster_ShortRegisterArray Null
        {
            get
            {
                return (new ModbusMaster_ShortRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", ShortRegisters.Select(shortRegister => shortRegister.ToString()));
        }

        public ModbusMaster_ShortRegisterArray AddShortRegister(ModbusMaster_ShortRegister shortRegister)
        {
            ShortRegisters.Add(shortRegister);
            return this;
        }

        public static ModbusMaster_ShortRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedShortRegisterArray = new ModbusMaster_ShortRegisterArray()
            {
                shortRegisters = new List<ModbusMaster_ShortRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedShortRegisterArray.ShortRegisters.Add(ModbusMaster_ShortRegister.Parse(parsedString[i]));
            }

            return parsedShortRegisterArray;
        }

        public ModbusMaster_ShortRegister GetShortRegister(SqlInt32 index)
        {
            return ShortRegisters[index.Value];
        }

        public static ModbusMaster_ShortRegisterArray Empty()
        {
            var shortRegisterArray = new ModbusMaster_ShortRegisterArray() { shortRegisters = new List<ModbusMaster_ShortRegister>() };
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
                    var shortRegister = new ModbusMaster_ShortRegister();
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