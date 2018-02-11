#region Imported Types

using DeviceSQL.Device.MODBUS.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

#endregion

namespace DeviceSQL.Device.MODBUS.Message

{
    internal class ReadEventArchiveRequest : MODBUSMessage, IMODBUSRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.EventArchive;
            }
        }

        public override int MinimumFrameSize
        {
            get
            {
                return 7;
            }
        }

        public MODBUSAddress EventArchiveAddress
        {
            get;
            private set;
        }

        public ushort Index
        {
            get;
            private set;
        }

        public override byte[] Data
        {
            get
            {
                var data = new List<byte>();

                data.AddRange(EventArchiveAddress.ToArray());
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)Index)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadEventArchiveRequest()
        {
        }

        public ReadEventArchiveRequest(ushort unitId, MODBUSAddress eventArchiveAddress, ushort index, bool isExtendedUnitId)
            : base(unitId, Device.ReadHoldingRegisters)
        {
            this.EventArchiveAddress = new MODBUSAddress(eventArchiveAddress.RelativeAddress, eventArchiveAddress.IsZeroBased);
            this.Index = index;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IMODBUSResponseMessage response)
        {
            var readEventArchiveResponse = response as ReadEventArchiveResponse;
            Debug.Assert(readEventArchiveResponse != null, "Argument response should be of type ReadEventArchiveResponse.");
        }

        #endregion

    }
}
