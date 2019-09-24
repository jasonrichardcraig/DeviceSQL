#region Imported Types

using System;

#endregion

#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.Data
#else
namespace DeviceSQL.Device.ROC.Data
#endif
{
    public struct ROCPlusHistoryRecord
    {

        #region Fields

        public byte HistorySegment;
        public byte HistoryPointNumber;
        public int Index;
        public byte[] Value;

        #endregion

        #region Helper Methods

        public DateTime? DateTimeStamp
        {
            get
            {
                if (Value.Length == 8)
                {
                    return new DateTime(1970, 1, 1).AddSeconds(BitConverter.ToUInt32(Value, 0));
                }
                else
                {
                    return null;
                }
            }
        }

        public float ToFloat()
        {
            if (Value.Length == 8)
            {
                return BitConverter.ToSingle(Value, 4);
            }
            else
            {
                return BitConverter.ToSingle(Value, 0);
            }
        }

        public float? ToNullableFloat()
        {
            var value = this.ToFloat();

            if (Single.IsNaN(value) || Single.IsInfinity(value))
            {
                return null;
            }
            else
            {
                return value;
            }
        }

        #endregion

    }
}
