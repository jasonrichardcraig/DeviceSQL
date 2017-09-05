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
    internal class WriteFloatsRequest : MODBUSMessage, IMODBUSRequestMessage
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

                data.Add(Convert.ToByte(FloatRegisters.Count * 4));

                for (int i = 0; i < FloatRegisters.Count; i ++)
                {
                    data.AddRange(FloatRegisters[i].Data);
                }

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public WriteFloatsRequest()
        {
        }

        public WriteFloatsRequest(ushort unitId, List<FloatRegister> floatRegisters, bool isExtendedUnitId)
            : base(unitId, Device.WriteMultipleRegisters)
        {
            this.FloatRegisters = floatRegisters;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IMODBUSResponseMessage response)
        {
            var writeFloatsResponse = response as WriteFloatsResponse;
            Debug.Assert(writeFloatsResponse != null, "Argument response should be of type WriteFloatsResponse.");

            if (this.FloatRegisters.Count != writeFloatsResponse.Count)
            {
                throw new FormatException("Number of float registers acknowledged does not equal number of float registers written.");
            }

            if (this.FloatRegisters[0].Address.AbsoluteAddress != writeFloatsResponse.StartingAddress.AbsoluteAddress)
            {
                throw new FormatException("Starting address acknowledged does not match starting address written.");
            }
        }

        #endregion

    }
}
