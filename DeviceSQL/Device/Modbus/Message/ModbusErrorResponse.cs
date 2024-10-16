#region Imported Types

using System;
using System.Collections.Generic;
using System.Globalization;

#endregion

namespace DeviceSQL.Device.Modbus.Message
{
    internal class ModbusErrorResponse : ModbusMessage, IModbusResponseMessage
    {

        #region Fields

        private static readonly Dictionary<ushort, string> errorMessages = CreateErrorMessages();

        protected byte errorCode { get; set; }

        #endregion

        #region Properties

        public byte ErrorCode
        {
            get
            {
                return this.errorCode;
            }
            set
            {
                this.errorCode = value;
            }
        }

        public override int MinimumFrameSize
        {
            get
            {
                return (IsExtendedUnitId ? 4 : 3);
            }
        }

        public override byte[] Data
        {
            get
            {
                return new byte[] { ErrorCode };
            }
        }

        #endregion

        #region Helper Methods

        public override string ToString()
        {

            string message = null;

            switch (this.ErrorCode)
            {
                case Device.IllegalFunction:
                case Device.IllegalDataAddress:
                case Device.IllegalDataValue:
                case Device.SlaveDeviceFailure:
                case Device.Acknowledge:
                case Device.SlaveDeviceBusy:
                case Device.NegativeAcknowlegde:
                case Device.MemoryParityError:
                    message = errorMessages[errorCode];
                    break;
                default:
                    message = "Unknown error";
                    break;
            }

            return string.Format(CultureInfo.InvariantCulture, "Function Code: {1}{0}Exception Code: {2}", Environment.NewLine, this.FunctionCode, message);
        }

        internal static Dictionary<ushort, string> CreateErrorMessages()
        {
            Dictionary<ushort, string> messages = new Dictionary<ushort, string>();

            messages.Add(1, "The function code received in the query is not an allowable action for the server (or slave). This may be because the function code is only applicable to newer devices, and was not implemented in the unit selected. It could also indicate that the server (or slave) is in the wrong state to process a request of this type, for example because it is unconfigured and is being asked to return register values.");
            messages.Add(2, "The data address received in the query is not an allowable address for the server (or slave). More specifically, the combination of reference number and transfer length is invalid. For a controller with 100 registers, the PDU addresses the first register as 0, and the last one as 99. If a request is submitted with a starting register address of 96 and a quantity of registers of 4, then this request will successfully operate (address-wise at least) on registers 96, 97, 98, 99. If a request is submitted with a starting register address of 96 and a quantity of registers of 5, then this request will fail with Exception Code 0x02 “Illegal Data Address” since it attempts to operate on registers 96, 97, 98, 99 and 100, and there is no register with address 100.");
            messages.Add(3, "A value contained in the query data field is not an allowable value for server (or slave). This indicates a fault in the structure of the remainder of a complex request, such as that the implied length is incorrect. It specifically does NOT mean that a data item submitted for storage in a register has a value outside the expectation of the application program, since the Modbus protocol is unaware of the significance of any particular value of any particular register.");
            messages.Add(4, "An unrecoverable error occurred while the server (or slave) was attempting to perform the requested action.");
            messages.Add(5, "Specialized use in conjunction with programming commands. The server (or slave) has accepted the request and is processing it, but a long duration of time will be required to do so. This response is returned to prevent a timeout error from occurring in the client (or master). The client (or master) can next issue a Poll Program Complete message to determine if processing is completed.");
            messages.Add(6, "Specialized use in conjunction with programming commands. The server (or slave) is engaged in processing a long–duration program command. The client (or master) should retransmit the message later when the server (or slave) is free.");
            messages.Add(7, "Specialized use in conjunction with programming commands. The slave cannot perform the function recieved in the query. This code is returned for an unsuccessful program request using function code 13 or 14 decimal. The master should request diagnostic or error information from the slave.");
            messages.Add(8, "Specialized use in conjunction with function codes 20 and 21 and reference type 6, to indicate that the extended file area failed to pass a consistency check.");
            return messages;
        }

        void IModbusResponseMessage.Initialize(byte[] frame, bool isExtendedUnitId)
        {
            base.Initialize(frame, isExtendedUnitId);
        }

        void IModbusResponseMessage.Initialize(byte[] frame, bool isExtendedUnitId, IModbusRequestMessage requestMessage)
        {
            base.Initialize(frame, isExtendedUnitId);

            if (frame.Length < (IsExtendedUnitId ? Device.MinimumFrameSizeWithExtendedUnitId : Device.MinimumFrameSize))
            {
                throw new FormatException("Message frame does not contain enough bytes.");
            }

            ErrorCode = IsExtendedUnitId ? frame[3] : frame[2];
        }

        #endregion

    }
}
