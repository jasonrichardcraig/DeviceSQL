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
    internal class ReadFloatsRequest : MODBUSMessage, IMODBUSRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.Float;
            }
        }

        public List<FloatRegister> FloatRegisters
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

                data.AddRange(FloatRegisters.First().Address.ToArray());
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)FloatRegisters.Count)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadFloatsRequest()
        {
        }

        public ReadFloatsRequest(ushort unitId, List<FloatRegister> floatRegisters, bool isExtendedUnitId)
            : base(unitId, Device.ReadHoldingRegisters)
        {
            this.FloatRegisters = floatRegisters;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IMODBUSResponseMessage response)
        {
            var readFloatsResponse = response as ReadFloatsResponse;
            Debug.Assert(readFloatsResponse != null, "Argument response should be of type ReadFloatsResponse.");

            if (this.FloatRegisters.Count != (readFloatsResponse.Data.Length - 1) / 4)
            {
                throw new FormatException("Number of float registers recieved does not equal number of float registers requested.");
            }
        }

        #endregion

    }
}
