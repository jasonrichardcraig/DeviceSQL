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
    public struct ModbusMaster_StringRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ModbusMaster_StringRegister> stringRegisters;

        #endregion

        #region Properties

        internal ModbusMaster_StringRegister this[int index]
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

        private List<ModbusMaster_StringRegister> StringRegisters
        {
            get
            {
                if (stringRegisters == null)
                {
                    stringRegisters = new List<ModbusMaster_StringRegister>();
                }
                return stringRegisters;
            }
        }

        public static ModbusMaster_StringRegisterArray Null
        {
            get
            {
                return (new ModbusMaster_StringRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", StringRegisters.Select(stringRegister => stringRegister.ToString()));
        }

        public ModbusMaster_StringRegisterArray AddStringRegister(ModbusMaster_StringRegister stringRegister)
        {
            StringRegisters.Add(stringRegister);
            return this;
        }

        public static ModbusMaster_StringRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedStringRegisterArray = new ModbusMaster_StringRegisterArray()
            {
                stringRegisters = new List<ModbusMaster_StringRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedStringRegisterArray.StringRegisters.Add(ModbusMaster_StringRegister.Parse(parsedString[i]));
            }

            return parsedStringRegisterArray;
        }

        public ModbusMaster_StringRegister GetStringRegister(SqlInt32 index)
        {
            return StringRegisters[index.Value];
        }

        public static ModbusMaster_StringRegisterArray Empty()
        {
            var stringRegisterArray = new ModbusMaster_StringRegisterArray() { stringRegisters = new List<ModbusMaster_StringRegister>() };
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
                    var stringRegister = new ModbusMaster_StringRegister();
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