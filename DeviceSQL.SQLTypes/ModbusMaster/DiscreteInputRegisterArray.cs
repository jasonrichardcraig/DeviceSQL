#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.SQLTypes.MODBUSMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct MODBUSMaster_DiscreteInputRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<MODBUSMaster_DiscreteInputRegister> discreteInputRegisters;

        #endregion

        #region Properties

        internal MODBUSMaster_DiscreteInputRegister this[int index]
        {
            get
            {
                return DiscreteInputRegisters[index];
            }
            set
            {
                DiscreteInputRegisters[index] = value;
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
                return DiscreteInputRegisters.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<MODBUSMaster_DiscreteInputRegister> DiscreteInputRegisters
        {
            get
            {
                if (discreteInputRegisters == null)
                {
                    discreteInputRegisters = new List<MODBUSMaster_DiscreteInputRegister>();
                }
                return discreteInputRegisters;
            }
        }

        public static MODBUSMaster_DiscreteInputRegisterArray Null
        {
            get
            {
                return (new MODBUSMaster_DiscreteInputRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", DiscreteInputRegisters.Select(discreteInputRegister => discreteInputRegister.ToString()));
        }

        public MODBUSMaster_DiscreteInputRegisterArray AddDiscreteInputRegister(MODBUSMaster_DiscreteInputRegister discreteInputRegister)
        {
            DiscreteInputRegisters.Add(discreteInputRegister);
            return this;
        }

        public static MODBUSMaster_DiscreteInputRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedDiscreteInputRegisterArray = new MODBUSMaster_DiscreteInputRegisterArray()
            {
                discreteInputRegisters = new List<MODBUSMaster_DiscreteInputRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedDiscreteInputRegisterArray.DiscreteInputRegisters.Add(MODBUSMaster_DiscreteInputRegister.Parse(parsedString[i]));
            }

            return parsedDiscreteInputRegisterArray;
        }

        public MODBUSMaster_DiscreteInputRegister GetDiscreteInputRegister(SqlInt32 index)
        {
            return DiscreteInputRegisters[index.Value];
        }

        public static MODBUSMaster_DiscreteInputRegisterArray Empty()
        {
            var discreteInputRegisterArray = new MODBUSMaster_DiscreteInputRegisterArray() { discreteInputRegisters = new List<MODBUSMaster_DiscreteInputRegister>() };
            return discreteInputRegisterArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            DiscreteInputRegisters.Clear();
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
                    var discreteInputRegister = new MODBUSMaster_DiscreteInputRegister();
                    discreteInputRegister.Read(binaryReader);
                    DiscreteInputRegisters.Add(discreteInputRegister);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; DiscreteInputRegisters.Count > i; i++)
                {
                    DiscreteInputRegisters[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}   
