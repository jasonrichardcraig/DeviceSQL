#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.SQLTypes.ROC
{

    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct ParameterArray : INullable, IBinarySerialize
    {

        #region Fields

        private List<Parameter> parameters;

        #endregion

        #region Properties

        internal Parameter this[int index]
        {
            get
            {
                return Parameters[index];
            }
            set
            {
                Parameters[index] = value;
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
                return Parameters.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<Parameter> Parameters
        {
            get
            {
                if (parameters == null)
                {
                    parameters = new List<Parameter>();
                }
                return parameters;
            }
        }

        public static ParameterArray Null
        {
            get
            {
                return (new ParameterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", Parameters.Select(parameter => parameter.ToString()));
        }

        public ParameterArray AddParameter(Parameter parameter)
        {
            Parameters.Add(parameter);
            return this;
        }

        public static ParameterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCParameterArray = new ParameterArray()
            {
                parameters = new List<Parameter>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCParameterArray.Parameters.Add(Parameter.Parse(parsedString[i]));
            }

            return parsedROCParameterArray;
        }

        public Parameter GetParameter(SqlInt32 index)
        {
            return Parameters[index.Value];
        }

        public static ParameterArray Empty()
        {
            var rocParameterArray = new ParameterArray() { parameters = new List<Parameter>() };
            return rocParameterArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            Parameters.Clear();
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
                    var parameter = new Parameter();
                    parameter.Read(binaryReader);
                    Parameters.Add(parameter);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; Parameters.Count > i; i++)
                {
                    Parameters[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
