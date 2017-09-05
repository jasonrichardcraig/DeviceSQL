using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Linq;

namespace DeviceSQL.Types.MODBUSMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct MODBUSMaster_FloatRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<MODBUSMaster_FloatRegister> floatRegisters;

        #endregion

        #region Properties

        internal MODBUSMaster_FloatRegister this[int index]
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

        private List<MODBUSMaster_FloatRegister> FloatRegisters
        {
            get
            {
                if (floatRegisters == null)
                {
                    floatRegisters = new List<MODBUSMaster_FloatRegister>();
                }
                return floatRegisters;
            }
        }

        public static MODBUSMaster_FloatRegisterArray Null
        {
            get
            {
                return (new MODBUSMaster_FloatRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", FloatRegisters.Select(floatRegister => floatRegister.ToString()));
        }

        public MODBUSMaster_FloatRegisterArray AddFloatRegister(MODBUSMaster_FloatRegister floatRegister)
        {
            FloatRegisters.Add(floatRegister);
            return this;
        }

        public static MODBUSMaster_FloatRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedFloatRegisterArray = new MODBUSMaster_FloatRegisterArray()
            {
                floatRegisters = new List<MODBUSMaster_FloatRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedFloatRegisterArray.FloatRegisters.Add(MODBUSMaster_FloatRegister.Parse(parsedString[i]));
            }

            return parsedFloatRegisterArray;
        }

        public MODBUSMaster_FloatRegister GetFloatRegister(SqlInt32 index)
        {
            return FloatRegisters[index.Value];
        }

        public static MODBUSMaster_FloatRegisterArray Empty()
        {
            var floatRegisterArray = new MODBUSMaster_FloatRegisterArray() { floatRegisters = new List<MODBUSMaster_FloatRegister>() };
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
                    var floatRegister = new MODBUSMaster_FloatRegister();
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
