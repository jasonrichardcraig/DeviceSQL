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
    public struct ModbusMaster_FloatRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ModbusMaster_FloatRegister> floatRegisters;

        #endregion

        #region Properties

        internal ModbusMaster_FloatRegister this[int index]
        {
            get
            {
                return FloatRegisters[index];
            }
            set
            {
                FloatRegisters[index] = value;
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
                return FloatRegisters.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<ModbusMaster_FloatRegister> FloatRegisters
        {
            get
            {
                if (floatRegisters == null)
                {
                    floatRegisters = new List<ModbusMaster_FloatRegister>();
                }
                return floatRegisters;
            }
        }

        public static ModbusMaster_FloatRegisterArray Null
        {
            get
            {
                return (new ModbusMaster_FloatRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", FloatRegisters.Select(floatRegister => floatRegister.ToString()));
        }

        public ModbusMaster_FloatRegisterArray AddFloatRegister(ModbusMaster_FloatRegister floatRegister)
        {
            FloatRegisters.Add(floatRegister);
            return this;
        }

        public static ModbusMaster_FloatRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedFloatRegisterArray = new ModbusMaster_FloatRegisterArray()
            {
                floatRegisters = new List<ModbusMaster_FloatRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedFloatRegisterArray.FloatRegisters.Add(ModbusMaster_FloatRegister.Parse(parsedString[i]));
            }

            return parsedFloatRegisterArray;
        }

        public ModbusMaster_FloatRegister GetFloatRegister(SqlInt32 index)
        {
            return FloatRegisters[index.Value];
        }

        public static ModbusMaster_FloatRegisterArray Empty()
        {
            var floatRegisterArray = new ModbusMaster_FloatRegisterArray() { floatRegisters = new List<ModbusMaster_FloatRegister>() };
            return floatRegisterArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            FloatRegisters.Clear();
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
                    var floatRegister = new ModbusMaster_FloatRegister();
                    floatRegister.Read(binaryReader);
                    FloatRegisters.Add(floatRegister);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; FloatRegisters.Count > i; i++)
                {
                    FloatRegisters[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}   
