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
    internal class ReadShortsRequest : MODBUSMessage, IMODBUSRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.Short;
            }
        }

        public List<ShortRegister> ShortRegisters
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

                data.AddRange(ShortRegisters.First().Address.ToArray());
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)ShortRegisters.Count)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadShortsRequest()
        {
        }

        public ReadShortsRequest(ushort unitId, List<ShortRegister> shortRegisters, bool isExtendedUnitId)
            : base(unitId, Device.ReadHoldingRegisters)
        {
            this.ShortRegisters = shortRegisters;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IMODBUSResponseMessage response)
        {
            var readShortsResponse = response as ReadShortsResponse;
            Debug.Assert(readShortsResponse != null, "Argument response should be of type ReadShortsResponse.");

            if (this.ShortRegisters.Count != (readShortsResponse.Data.Length - 1) / 2)
            {
                throw new FormatException("Number of short registers recieved does not equal number of short registers requested.");
            }
        }

        #endregion

    }
}
