#region Imported Types

using DeviceSQL.Device.Roc.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode121Response : RocMessage, IRocResponseMessage
    {

        #region Fields

        private byte[] data;

        #endregion

        #region Properties

        public override int MinimumFrameSize
        {
            get
            {
                return 10;
            }
        }

        public override byte[] Data
        {
            get
            {
                return data;
            }
        }

        public List<AlarmRecord> MeterAlarms
        {
            get
            {
                byte alarmCount = Data[0];
                ushort startIndex = BitConverter.ToUInt16(Data,1);

                var meterAlarms = new List<AlarmRecord>(alarmCount);

                for (ushort alarmIndex = 5; alarmIndex < Data.Length; alarmIndex += 22)
                {
                    ushort relativeAlarmIndex = (ushort)(startIndex + ((alarmIndex - 5) / 22));

                    var alarmBytes = Data.ToList().GetRange(alarmIndex, 22).ToArray();

                    meterAlarms.Add(new AlarmRecord(relativeAlarmIndex, alarmBytes));
                }

                return meterAlarms;
            }
        }

        #endregion

        #region Helper Methods

        void IRocResponseMessage.Initialize(byte[] frame)
        {
            base.Initialize(frame);
        }

        void IRocResponseMessage.Initialize(byte[] frame, IRocRequestMessage requestMessage)
        {
            base.Initialize(frame);

            var dataLength = frame[5];

            data = new byte[dataLength];

            Buffer.BlockCopy(frame, 6, data, 0, dataLength);
        }

        #endregion

    }
}
