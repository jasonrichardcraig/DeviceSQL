#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

#endregion

namespace DeviceSQL.Device.Modbus.Message

{
    internal class WriteBooleanRequest : ModbusMessage, IModbusRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.Boolean;
            }
        }

        public BooleanRegister BooleanRegister
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

                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)BooleanRegister.Address.AbsoluteAddress)));

                if(BooleanRegister)
                {
                     data.AddRange(new byte[] {0xff, 0x00});
                }
                else
                {
                     data.AddRange(new byte[] {0x00, 0x00});
                }
                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public WriteBooleanRequest()
        {
        }

        public WriteBooleanRequest(ushort unitId, BooleanRegister booleanRegister, bool isExtendedUnitId)
            : base(unitId, Device.WriteSingleCoil)
        {
            this.BooleanRegister = booleanRegister;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IModbusResponseMessage response)
        {
            var writeBooleanResponse = response as WriteBooleanResponse;
            Debug.Assert(writeBooleanResponse != null, "Argument response should be of type ReadFloatsResponse.");

            if ((BitConverter.ToUInt32(this.Data, 0) != (BitConverter.ToUInt32(writeBooleanResponse.Data, 0))))
            {
                throw new FormatException("Boolean write response does not match boolean write request.");
            }
        }

        #endregion

    }
}
