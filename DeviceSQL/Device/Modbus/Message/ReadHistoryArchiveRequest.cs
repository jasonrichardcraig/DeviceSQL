#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

#endregion

namespace DeviceSQL.Device.Modbus.Message

{
    internal class ReadHistoryArchiveRequest : ModbusMessage, IModbusRequestMessage
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

        public ModbusAddress HistoryArchiveAddress
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

                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)HistoryArchiveAddress.AbsoluteAddress)));
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)Index)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadHistoryArchiveRequest()
        {
        }

        public ReadHistoryArchiveRequest(ushort unitId, ModbusAddress historyArchiveAddress, ushort index, byte recordSize, bool isExtendedUnitId)
            : base(unitId, Device.ReadHoldingRegisters)
        {
            this.HistoryArchiveAddress = new ModbusAddress(historyArchiveAddress.RelativeAddress, historyArchiveAddress.IsZeroBased);
            this.Index = index;
            this.RecordSize = recordSize;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IModbusResponseMessage response)
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
