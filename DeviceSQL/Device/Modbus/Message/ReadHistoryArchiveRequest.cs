#region Imported Types

using DeviceSQL.Device.MODBUS.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

#endregion

namespace DeviceSQL.Device.MODBUS.Message

{
    internal class ReadHistoryArchiveRequest : MODBUSMessage, IMODBUSRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.HistoryArchive;
            }
        }

        public override int MinimumFrameSize
        {
            get
            {
                return 7;
            }
        }

        public MODBUSAddress HistoryArchiveAddress
        {
            get;
            private set;
        }

        public ushort Index
        {
            get;
            private set;
        }

        public byte RecordSize
        {
            get;
            private set;
        }

        public override byte[] Data
        {
            get
            {
                var data = new List<byte>();

                data.AddRange(HistoryArchiveAddress.ToArray());
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)Index)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadHistoryArchiveRequest()
        {
        }

        public ReadHistoryArchiveRequest(ushort unitId, MODBUSAddress historyArchiveAddress, ushort index, byte recordSize, bool isExtendedUnitId)
            : base(unitId, Device.ReadHoldingRegisters)
        {
            this.HistoryArchiveAddress = new MODBUSAddress(historyArchiveAddress.RelativeAddress, historyArchiveAddress.IsZeroBased);
            this.Index = index;
            this.RecordSize = recordSize;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IMODBUSResponseMessage response)
        {
            var readHistoryArchiveResponse = response as ReadHistoryArchiveResponse;
            Debug.Assert(readHistoryArchiveResponse != null, "Argument response should be of type ReadHistoryArchiveResponse.");

            if (this.RecordSize != (readHistoryArchiveResponse.Data.Length - 1))
            {
                throw new FormatException("History record size does not equal history record specified.");
            }
        }

        #endregion

    }
}
