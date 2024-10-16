#region Imported Types

using DeviceSQL.Device.Roc.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode118Response : RocMessage, IRocResponseMessage
    {

        #region Fields

        internal byte[] data;

        #endregion

        #region Properties

        public override int MinimumFrameSize
        {
            get
            {
                return 10;
            }
        }

        public byte NumberOfAlarms
        {
            get
            {
                return data[0];
            }
        }

        public ushort StartingAlarmLogIndex
        {
            get
            {
                return BitConverter.ToUInt16(data, 1);
            }
        }

        public ushort CurrentAlarmLogIndex
        {
            get
            {
                return BitConverter.ToUInt16(data, 3);
            }
        }

        public override byte[] Data
        {
            get
            {
                return data;
            }
        }

        public List<RocPlusAlarmRecord> RocPlusAlarms
        {
            get
            {
                byte alarmCount = NumberOfAlarms;
                ushort startIndex = StartingAlarmLogIndex;

                var RocPlusAlarms = new List<RocPlusAlarmRecord>(alarmCount);

                for (ushort alarmIndex = 5; alarmIndex < Data.Length; alarmIndex += 23)
                {
                    ushort relativeAlarmIndex = (ushort)(startIndex + ((alarmIndex - 5) / 23));

                    var alarmBytes = Data.ToList().GetRange(alarmIndex, 23).ToArray();

                    RocPlusAlarms.Add(new RocPlusAlarmRecord(relativeAlarmIndex, alarmBytes));
                }

                return RocPlusAlarms;
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
