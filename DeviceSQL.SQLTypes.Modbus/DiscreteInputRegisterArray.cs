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
    public struct DiscreteInputRegisterArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<DiscreteInputRegister> discreteInputRegisters;

        #endregion

        #region Properties

        internal DiscreteInputRegister this[int index]
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

        private List<DiscreteInputRegister> DiscreteInputRegisters
        {
            get
            {
                if (discreteInputRegisters == null)
                {
                    discreteInputRegisters = new List<DiscreteInputRegister>();
                }
                return discreteInputRegisters;
            }
        }

        public static DiscreteInputRegisterArray Null
        {
            get
            {
                return (new DiscreteInputRegisterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", DiscreteInputRegisters.Select(discreteInputRegister => discreteInputRegister.ToString()));
        }

        public DiscreteInputRegisterArray AddDiscreteInputRegister(DiscreteInputRegister discreteInputRegister)
        {
            DiscreteInputRegisters.Add(discreteInputRegister);
            return this;
        }

        public static DiscreteInputRegisterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedDiscreteInputRegisterArray = new DiscreteInputRegisterArray()
            {
                discreteInputRegisters = new List<DiscreteInputRegister>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedDiscreteInputRegisterArray.DiscreteInputRegisters.Add(DiscreteInputRegister.Parse(parsedString[i]));
            }

            return parsedDiscreteInputRegisterArray;
        }

        public DiscreteInputRegister GetDiscreteInputRegister(SqlInt32 index)
        {
            return DiscreteInputRegisters[index.Value];
        }

        public static DiscreteInputRegisterArray Empty()
        {
            var discreteInputRegisterArray = new DiscreteInputRegisterArray() { discreteInputRegisters = new List<DiscreteInputRegister>() };
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
                    var discreteInputRegister = new DiscreteInputRegister();
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
