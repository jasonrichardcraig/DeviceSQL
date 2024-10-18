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
    internal class ReadHoldingRegistersRequest : ModbusMessage, IModbusRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.UShort;
            }
        }

        public List<HoldingRegister> HoldingRegisters
        {
            get;
            set;
        }

        public override int MinimumFrameSize
        {
            get
            {
                return 7;
            }
        }

        public override byte[] Data
        {
            get
            {
                var data = new List<byte>();

                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)HoldingRegisters.First().Address.AbsoluteAddress)));
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)HoldingRegisters.Count)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadHoldingRegistersRequest()
        {
        }

        public ReadHoldingRegistersRequest(ushort unitId, List<HoldingRegister> holdingRegisters, bool isExtendedUnitId)
            : base(unitId, Device.ReadHoldingRegisters)
        {
            this.HoldingRegisters = holdingRegisters;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IModbusResponseMessage response)
        {
            var readHoldingRegistersResponse = response as ReadHoldingRegistersResponse;
            Debug.Assert(readHoldingRegistersResponse != null, "Argument response should be of type ReadHoldingRegistersResponse.");

            if (this.HoldingRegisters.Count != (readHoldingRegistersResponse.Data.Length - 1) / 2)
            {
                throw new FormatException("Number of holding registers recieved does not equal number of holding registers requested.");
            }
        }

        #endregion

    }
}
