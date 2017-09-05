#region Imported Types

using DeviceSQL.Device.MODBUS.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

#endregion

namespace DeviceSQL.Device.MODBUS.Message

{
    internal class ReadInputRegistersRequest : MODBUSMessage, IMODBUSRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.UShort;
            }
        }

        public List<InputRegister> InputRegisters
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

                data.AddRange(InputRegisters.First().Address.ToArray());
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)InputRegisters.Count)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadInputRegistersRequest()
        {
        }

        public ReadInputRegistersRequest(ushort unitId, List<InputRegister> inputRegisters, bool isExtendedUnitId)
            : base(unitId, Device.ReadInputRegisters)
        {
            this.InputRegisters = inputRegisters;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IMODBUSResponseMessage response)
        {
            var readInputRegistersResponse = response as ReadInputRegistersResponse;
            Debug.Assert(readInputRegistersResponse != null, "Argument response should be of type ReadInputRegistersResponse.");

            if (this.InputRegisters.Count != (readInputRegistersResponse.Data.Length - 1) / 2)
            {
                throw new FormatException("Number of input registers recieved does not equal number of input registers requested.");
            }
        }

        #endregion

    }
}
