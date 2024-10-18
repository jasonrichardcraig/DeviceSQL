#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

#endregion

namespace DeviceSQL.Device.Modbus.Message

{
    internal class ReadEventArchiveRequest : ModbusMessage, IModbusRequestMessage
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

        public ModbusAddress EventArchiveAddress
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

                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)EventArchiveAddress.AbsoluteAddress)));
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)Index)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadEventArchiveRequest()
        {
        }

        public ReadEventArchiveRequest(ushort unitId, ModbusAddress eventArchiveAddress, ushort index, bool isExtendedUnitId)
            : base(unitId, Device.ReadHoldingRegisters)
        {
            this.EventArchiveAddress = new ModbusAddress(eventArchiveAddress.RelativeAddress, eventArchiveAddress.IsZeroBased);
            this.Index = index;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IModbusResponseMessage response)
        {
            var readEventArchiveResponse = response as ReadEventArchiveResponse;
            Debug.Assert(readEventArchiveResponse != null, "Argument response should be of type ReadEventArchiveResponse.");
        }

        #endregion

    }
}
