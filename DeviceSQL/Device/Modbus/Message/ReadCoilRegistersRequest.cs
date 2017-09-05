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
    internal class ReadCoilRegistersRequest : MODBUSMessage, IMODBUSRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.Boolean;
            }
        }

        public List<CoilRegister> CoilRegisters
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

                data.AddRange(CoilRegisters.First().Address.ToArray());
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)CoilRegisters.Count)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadCoilRegistersRequest()
        {
        }

        public ReadCoilRegistersRequest(ushort unitId, List<CoilRegister> coilRegisters, bool isExtendedUnitId)
            : base(unitId, Device.ReadCoils)
        {
            this.CoilRegisters = coilRegisters;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IMODBUSResponseMessage response)
        {
            var readCoilRegistersResponse = response as ReadCoilRegistersResponse;
            Debug.Assert(readCoilRegistersResponse != null, "Argument response should be of type ReadCoilRegistersResponse.");

            var coilCount = this.CoilRegisters.Count();

            var expectedByteCount = (coilCount / 8) + (coilCount % 8) > 0 ? 1 : 0;

            if (expectedByteCount != readCoilRegistersResponse.Data[0])
            {
                throw new FormatException("Number of coil registers recieved does not equal number of coil registers requested.");
            }
        }

        #endregion

    }
}
