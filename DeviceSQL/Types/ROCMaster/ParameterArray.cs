#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.Types.RocMaster
{

    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct RocMaster_ParameterArray : INullable, IBinarySerialize
    {

        #region Fields

        private List<RocMaster_Parameter> parameters;

        #endregion

        #region Properties

        internal RocMaster_Parameter this[int index]
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

        private List<RocMaster_Parameter> Parameters
        {
            get
            {
                if (parameters == null)
                {
                    parameters = new List<RocMaster_Parameter>();
                }
                return parameters;
            }
        }

        public static RocMaster_ParameterArray Null
        {
            get
            {
                return (new RocMaster_ParameterArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", Parameters.Select(parameter => parameter.ToString()));
        }

        public RocMaster_ParameterArray AddParameter(RocMaster_Parameter parameter)
        {
            Parameters.Add(parameter);
            return this;
        }

        public static RocMaster_ParameterArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedRocParameterArray = new RocMaster_ParameterArray()
            {
                parameters = new List<RocMaster_Parameter>()
            };

            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedRocParameterArray.Parameters.Add(RocMaster_Parameter.Parse(parsedString[i]));
            }

            return parsedRocParameterArray;
        }

        public RocMaster_Parameter GetParameter(SqlInt32 index)
        {
            return Parameters[index.Value];
        }

        public static RocMaster_ParameterArray Empty()
        {
            var rocParameterArray = new RocMaster_ParameterArray() { parameters = new List<RocMaster_Parameter>() };
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
                    var parameter = new RocMaster_Parameter();
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
