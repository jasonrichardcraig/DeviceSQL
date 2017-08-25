#region Imported Types

using System;
using System.Diagnostics;

#endregion

namespace DeviceSQL.Device.Modbus.Data
{
    public class EventArchiveRecord
    {

        #region Properties

        public ushort Index
        {
            get;
            private set;
        }

        public byte[] Data
        {
            get;
            private set;
        }

        #endregion

        #region Constructor(s)

        internal EventArchiveRecord(ushort index, byte[] data)
        {
            this.Index = Index;
            this.Data = data;
        }

        #endregion

        #region Helper Methods

        public static DateTime? ParseNullableDateTimeValue(float dateValue, float timeValue, int baseYear)
        {
            try
            {
                var dateValueString = dateValue.ToString("000000");
                var timeValueString = timeValue.ToString("000000");

                var monthValue = int.Parse(dateValueString.Substring(0, 2));
                var dayValue = int.Parse(dateValueString.Substring(2, 2));
                var yearValue = int.Parse(dateValueString.Substring(4, 2));

                var hourValue = int.Parse(timeValueString.Substring(0, 2));
                var minuteValue = int.Parse(timeValueString.Substring(2, 2));
                var secondValue = int.Parse(timeValueString.Substring(4, 2));

                return new DateTime((yearValue + baseYear), monthValue, dayValue, hourValue, minuteValue, secondValue);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

        }

        public System.Int16 GetShortValue(int index, bool byteSwap)
        {
            var data = new byte[] { Data[index], Data[index + 1] };

            var word = ushort.MinValue;

            if (!byteSwap)
            {
                var words = ModbusConverter.NetworkBytesToHostUInt16(data);
                word = words[0];
            }
            else
            {
                word = BitConverter.ToUInt16(data, 0);
            }

            return (Int16)word;
        }

        public System.Single GetFloatValue(int index, bool byteSwap, bool wordSwap)
        {
            var data = new byte[] { Data[index], Data[index + 1], Data[index + 2], Data[index + 3] };
            var highWord = ushort.MinValue;
            var lowWord = ushort.MinValue;

            if (!byteSwap)
            {
                var words = ModbusConverter.NetworkBytesToHostUInt16(data);
                highWord = words[1];
                lowWord = words[0];
            }
            else
            {
                highWord = BitConverter.ToUInt16(data, 2);
                lowWord = BitConverter.ToUInt16(data, 0);
            }

            var highWordBytes = BitConverter.GetBytes(highWord);
            var lowWordBytes = BitConverter.GetBytes(lowWord);

            if (!wordSwap)
            {
                return BitConverter.ToSingle(new byte[] { highWordBytes[0], highWordBytes[1], lowWordBytes[0], lowWordBytes[1] }, 0);
            }
            else
            {
                return BitConverter.ToSingle(new byte[] { lowWordBytes[0], lowWordBytes[1], highWordBytes[0], highWordBytes[1] }, 0);
            }
        }

        public System.Single? GetNullableFloatValue(int index, bool byteSwap, bool wordSwap)
        {
            var value = this.GetFloatValue(index,byteSwap,wordSwap);

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
