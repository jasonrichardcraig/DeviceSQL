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
    public struct BooleanRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        private List<BooleanRegister> booleanRegisters;

        #endregion

        #region Properties

        internal BooleanRegister this[int index]
        {
            get
            {
                return BooleanRegisters[index];
            }
            set
            {
                BooleanRegisters[index] = value;
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
                return BooleanRegisters.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<BooleanRegister> BooleanRegisters
        {
            get
            {
                if (booleanRegisters == null)
                {
                    booleanRegisters = new List<BooleanRegister>();
                }
                return booleanRegisters;
            }
        }

        public static BooleanRegisterArray Null
        {
            get
            {
                return (new BooleanRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", BooleanRegisters.Select(booleanRegister => booleanRegister.ToString()));
        }

        public BooleanRegisterArray AddBooleanRegister(BooleanRegister booleanRegister)
        {
            BooleanRegisters.Add(booleanRegister);
            return this;
        }

        public static BooleanRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedBooleanRegisterArray = new BooleanRegisterArray()
            {
                booleanRegisters = new List<BooleanRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedBooleanRegisterArray.BooleanRegisters.Add(BooleanRegister.Parse(parsedString[i]));
            }

            return parsedBooleanRegisterArray;
        }

        public BooleanRegister GetBooleanRegister(SqlInt32 index)
        {
            return BooleanRegisters[index.Value];
        }

        public static BooleanRegisterArray Empty()
        {
            var booleanRegisterArray = new BooleanRegisterArray() { booleanRegisters = new List<BooleanRegister>() };
            return booleanRegisterArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            BooleanRegisters.Clear();
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
                    var booleanRegister = new BooleanRegister();
                    booleanRegister.Read(binaryReader);
                    BooleanRegisters.Add(booleanRegister);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; BooleanRegisters.Count > i; i++)
                {
                    BooleanRegisters[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}