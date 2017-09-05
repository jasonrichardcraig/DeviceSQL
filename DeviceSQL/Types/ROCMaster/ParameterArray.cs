#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.Types.ROCMaster
{

    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct ROCMaster_ParameterArray : INullable, IBinarySerialize
    {

        #region Fields

        private List<ROCMaster_Parameter> parameters;

        #endregion

        #region Properties

        internal ROCMaster_Parameter this[int index]
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

        private List<ROCMaster_Parameter> Parameters
        {
            get
            {
                if (parameters == null)
                {
                    parameters = new List<ROCMaster_Parameter>();
                }
                return parameters;
            }
        }

        public static ROCMaster_ParameterArray Null
        {
            get
            {
                return (new ROCMaster_ParameterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", Parameters.Select(parameter => parameter.ToString()));
        }

        public ROCMaster_ParameterArray AddParameter(ROCMaster_Parameter parameter)
        {
            Parameters.Add(parameter);
            return this;
        }

        public static ROCMaster_ParameterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCParameterArray = new ROCMaster_ParameterArray()
            {
                parameters = new List<ROCMaster_Parameter>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCParameterArray.Parameters.Add(ROCMaster_Parameter.Parse(parsedString[i]));
            }

            return parsedROCParameterArray;
        }

        public ROCMaster_Parameter GetParameter(SqlInt32 index)
        {
            return Parameters[index.Value];
        }

        public static ROCMaster_ParameterArray Empty()
        {
            var rocParameterArray = new ROCMaster_ParameterArray() { parameters = new List<ROCMaster_Parameter>() };
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
                    var parameter = new ROCMaster_Parameter();
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
