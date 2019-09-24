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
    public struct ROCPlusHistoryPointArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<byte> historyPoints;

        #endregion

        #region Properties

        internal byte this[int index]
        {
            get
            {
                return HistoryPoints[index];
            }
            set
            {
                HistoryPoints[index] = value;
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
                return HistoryPoints.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<byte> HistoryPoints
        {
            get
            {
                if (historyPoints == null)
                {
                    historyPoints = new List<byte>();
                }
                return historyPoints;
            }
        }

        public static ROCPlusHistoryPointArray Null
        {
            get
            {
                return (new ROCPlusHistoryPointArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", HistoryPoints.Select(parameter => parameter.ToString()));
        }

        public ROCPlusHistoryPointArray AddHistoryPoint(byte historyPoint)
        {
            HistoryPoints.Add(historyPoint);
            return this;
        }

        public static ROCPlusHistoryPointArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCPlusHistoryPointArray = new ROCPlusHistoryPointArray();
            parsedROCPlusHistoryPointArray.historyPoints = new List<byte>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCPlusHistoryPointArray.HistoryPoints.Add(byte.Parse(parsedString[i]));
            }

            return parsedROCPlusHistoryPointArray;
        }

        public byte GetHistoryPoint(SqlInt32 index)
        {
            return HistoryPoints[index.Value];
        }

        public static ROCPlusHistoryPointArray Empty()
        {
            var rocPlusHistoryPointArray = new ROCPlusHistoryPointArray() { historyPoints = new List<byte>() };
            return rocPlusHistoryPointArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            HistoryPoints.Clear();
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
                    HistoryPoints.Add(binaryReader.ReadByte());
                }
            }
        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; HistoryPoints.Count > i; i++)
                {
                    binaryWriter.Write(HistoryPoints[i]);
                }
            }
        }

        #endregion

    }
}
