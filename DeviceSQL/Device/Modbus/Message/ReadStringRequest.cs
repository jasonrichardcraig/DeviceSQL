#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

#endregion

namespace DeviceSQL.Device.Modbus.Message

{
    internal class ReadStringRequest : ModbusMessage, IModbusRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.String;
            }
        }

        public override int MinimumFrameSize
        {
            get
            {
                return 7;
            }
        }

        public StringRegister StringRegister
        {
            get;
            set;
        }

        public override byte[] Data
        {
            get
            {
                var data = new List<byte>();

                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)StringRegister.Address.AbsoluteAddress)));
                data.Add(0);
                data.Add(1);
                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadStringRequest()
        {
        }

        public ReadStringRequest(ushort unitId, StringRegister stringRegister, bool isExtendedUnitId)
            : base(unitId, Device.ReadHoldingRegisters)
        {
            this.StringRegister = stringRegister;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IModbusResponseMessage response)
        {
            var readStringResponse = response as ReadStringResponse;
            Debug.Assert(readStringResponse != null, "Argument response should be of type ReadStringResponse.");

            if (this.StringRegister.Length != (readStringResponse.Data.Length - 1))
            {
                throw new FormatException("String length does not equal string length specified.");
            }
        }

        #endregion

    }
}
