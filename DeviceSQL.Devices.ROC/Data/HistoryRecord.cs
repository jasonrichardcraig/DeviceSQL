#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.Data
#else
namespace DeviceSQL.Device.ROC.Data
#endif
{
    public struct HistoryRecord
    {

        #region Fields

        public byte HistorySegment;
        public byte HistoryPointNumber;
        public int Index;
        public byte[] Value;

        #endregion

        #region Helper Methods

        public DateTime? ToDateTimeStamp(DateTime deviceDateTime)
        {
            if (Value[3] != 0 && Value[2] != 0)
            {
                // Hack: Prevents new years day bug (Records in device more than 2 years can not not be interpreted)
                if (deviceDateTime.Month >= Value[3])
                {
                    return new DateTime(deviceDateTime.Year, Value[3], Value[2], Value[1], Value[0], 0);
                }
                else
                {
                    return new DateTime((deviceDateTime.Year - 1), Value[3], Value[2], Value[1], Value[0], 0);
                }
            }
            else
            {
                return null;
            }
        }

        public DateTime ToExtendedTimeStamp()
        {
            return new DateTime(1970, 1, 1).AddSeconds(BitConverter.ToUInt32(Value, 0));
        }

        public float ToFloat()
        {
            return BitConverter.ToSingle(Value, 0);
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
