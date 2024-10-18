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
    internal class ReadShortsRequest : ModbusMessage, IModbusRequestMessage
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

                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)ShortRegisters.First().Address.AbsoluteAddress)));
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

        public void ValidateResponse(IModbusResponseMessage response)
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
