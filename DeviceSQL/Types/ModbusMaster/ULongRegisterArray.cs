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
    public struct ModbusMaster_ULongRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ModbusMaster_ULongRegister> uLongRegisters;

        #endregion

        #region Properties

        internal ModbusMaster_ULongRegister this[int index]
        {
            get
            {
                return ULongRegisters[index];
            }
            set
            {
                ULongRegisters[index] = value;
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
                return ULongRegisters.Count;
            }
        }

        #endregion

        #region Helper Methods

        public List<ModbusMaster_ULongRegister> ULongRegisters
        {
            get
            {
                if (uLongRegisters == null)
                {
                    uLongRegisters = new List<ModbusMaster_ULongRegister>();
                }
                return uLongRegisters;
            }
        }

        public static ModbusMaster_ULongRegisterArray Null
        {
            get
            {
                return (new ModbusMaster_ULongRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", ULongRegisters.Select(longRegister => longRegister.ToString()));
        }

        public ModbusMaster_ULongRegisterArray AddLongRegister(ModbusMaster_ULongRegister uLongRegister)
        {
            ULongRegisters.Add(uLongRegister);
            return this;
        }

        public static ModbusMaster_ULongRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedULongRegisterArray = new ModbusMaster_ULongRegisterArray()
            {
                uLongRegisters = new List<ModbusMaster_ULongRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedULongRegisterArray.ULongRegisters.Add(ModbusMaster_ULongRegister.Parse(parsedString[i]));
            }

            return parsedULongRegisterArray;
        }

        public ModbusMaster_ULongRegister GetULongRegister(SqlInt32 index)
        {
            return ULongRegisters[index.Value];
        }

        public static ModbusMaster_ULongRegisterArray Empty()
        {
            var uLongRegisterArray = new ModbusMaster_ULongRegisterArray() { uLongRegisters = new List<ModbusMaster_ULongRegister>() };
            return uLongRegisterArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            ULongRegisters.Clear();
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
                    var uLongRegister = new ModbusMaster_ULongRegister();
                    uLongRegister.Read(binaryReader);
                    ULongRegisters.Add(uLongRegister);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; ULongRegisters.Count > i; i++)
                {
                    ULongRegisters[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}

