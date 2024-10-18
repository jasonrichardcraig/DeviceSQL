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
    internal class ReadDiscreteInputRegistersRequest : ModbusMessage, IModbusRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.Boolean;
            }
        }

        public List<DiscreteInputRegister> DiscreteInputRegisters
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

                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)DiscreteInputRegisters.First().Address.AbsoluteAddress)));
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)DiscreteInputRegisters.Count)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadDiscreteInputRegistersRequest()
        {
        }

        public ReadDiscreteInputRegistersRequest(ushort unitId, List<DiscreteInputRegister> discreteInputRegisters, bool isExtendedUnitId)
            : base(unitId, Device.ReadDiscreteInputs)
        {
            this.DiscreteInputRegisters = discreteInputRegisters;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IModbusResponseMessage response)
        {
            var readDiscreteInputRegistersResponse = response as ReadDiscreteInputRegistersResponse;
            Debug.Assert(readDiscreteInputRegistersResponse != null, "Argument response should be of type ReadDiscreteInputRegistersResponse.");

            var discreteInputCount = this.DiscreteInputRegisters.Count();

            var expectedByteCount = (discreteInputCount / 8) + (discreteInputCount % 8) > 0 ? 1 : 0;

            if (expectedByteCount != readDiscreteInputRegistersResponse.Data[0])
            {
                throw new FormatException("Number of discrete input registers recieved does not equal number of discrete input registers requested.");
            }
        }

        #endregion

    }
}
