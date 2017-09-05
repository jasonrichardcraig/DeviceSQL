#region Imported Types

using DeviceSQL.Device.MODBUS.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace DeviceSQL.Device.MODBUS.Message

{
    internal class ReadStringRequest : MODBUSMessage, IMODBUSRequestMessage
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

                data.AddRange(StringRegister.Address.ToArray());
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

        public void ValidateResponse(IMODBUSResponseMessage response)
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
