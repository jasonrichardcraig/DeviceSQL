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
    public struct FloatRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<FloatRegister> floatRegisters;

        #endregion

        #region Properties

        internal FloatRegister this[int index]
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

        private List<FloatRegister> FloatRegisters
        {
            get
            {
                if (floatRegisters == null)
                {
                    floatRegisters = new List<FloatRegister>();
                }
                return floatRegisters;
            }
        }

        public static FloatRegisterArray Null
        {
            get
            {
                return (new FloatRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", FloatRegisters.Select(floatRegister => floatRegister.ToString()));
        }

        public FloatRegisterArray AddFloatRegister(FloatRegister floatRegister)
        {
            FloatRegisters.Add(floatRegister);
            return this;
        }

        public static FloatRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedFloatRegisterArray = new FloatRegisterArray()
            {
                floatRegisters = new List<FloatRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedFloatRegisterArray.FloatRegisters.Add(FloatRegister.Parse(parsedString[i]));
            }

            return parsedFloatRegisterArray;
        }

        public FloatRegister GetFloatRegister(SqlInt32 index)
        {
            return FloatRegisters[index.Value];
        }

        public static FloatRegisterArray Empty()
        {
            var floatRegisterArray = new FloatRegisterArray() { floatRegisters = new List<FloatRegister>() };
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
                    var floatRegister = new FloatRegister();
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
