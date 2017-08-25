#region Imported Types

using DeviceSQL.Device.ROC.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode165Response : ROCMessage, IROCResponseMessage
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

        public byte StartingDatabaseNumber
        {
            get;
            internal set;
        }

        public byte HistoricalRamArea
        {
            get
            {
                return data[1];
            }
        }

        public byte NumberOfDatabasePointsSent
        {
            get
            {
                return data[2];
            }
        }

        public override byte[] Data
        {
            get
            {
                return data;
            }
        }

        public List<HistoryPointConfiguration> HistoryPointConfigurations
        {
            get
            {
                byte historyPointConfigurationCount = NumberOfDatabasePointsSent;

                byte startIndex = StartingDatabaseNumber;

                var historyPointConfigurations = new List<HistoryPointConfiguration>(historyPointConfigurationCount);

                for (byte historyPointConfigurationIndex = 3; historyPointConfigurationIndex < NumberOfDatabasePointsSent; historyPointConfigurationIndex += 4)
                {
                    byte relativeHistoryPointConfigurationIndex = (byte)(startIndex + ((historyPointConfigurationIndex - 3) / 4));

                    var historyPointConfigurationBytes = Data.ToList().GetRange(historyPointConfigurationIndex, 4).ToArray();

                    historyPointConfigurations.Add(new HistoryPointConfiguration() { Index = relativeHistoryPointConfigurationIndex, HistoricalRamArea = Data[1], ArchiveType = (HistoryPointConfiguration.HistoryPointArchiveType)historyPointConfigurationBytes[0], Tlp = new Tlp(historyPointConfigurationBytes[1], historyPointConfigurationBytes[2], historyPointConfigurationBytes[3]) });
                }

                return historyPointConfigurations;
            }
        }

        #endregion

        #region Helper Methods

        void IROCResponseMessage.Initialize(byte[] frame)
        {
            base.Initialize(frame);
        }

        void IROCResponseMessage.Initialize(byte[] frame, IROCRequestMessage requestMessage)
        {
            base.Initialize(frame);

            var dataLength = frame[5];

            data = new byte[dataLength];

            Buffer.BlockCopy(frame, 6, data, 0, dataLength);

            StartingDatabaseNumber = (requestMessage as OpCode165Request).StartingDatabaseNumber;

        }

        #endregion

    }
}
