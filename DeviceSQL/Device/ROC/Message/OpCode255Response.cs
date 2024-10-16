#region Imported Types

using System;
using System.Collections.Generic;
using System.Globalization;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode255Response : RocMessage, IRocResponseMessage
    {

        public struct RocPlusError
        {
            public byte ErrorCode
            {
                get;
                set;
            }

            public byte Offset
            {
                get;
                set;
            }

        }

        #region Constants

        private const string OpCode120_Error56 = "Number of data bytes > 0.";
        private const string OpCode121_Error57 = "Number of data bytes <> 3 or Starting alarm pointer > 239.";
        private const string OpCode122_Error58 = "Number of data bytes <> 3 or Starting event pointer > 239.";
        private const string OpCode126_Error59 = "One of the following conditions: 1) Number of data bytes > 2. 2) Invalid point number for requested RAM area.3) Invalid RAM area number.";
        private const string OpCode130_Error62 = "One of the following conditions: 1) The number of data values requested exceeds the number of data values defined for that history RAM area. 2) The data portion of the message did not consist soley of 5 bytes. 3) The module number exceeds or is equal to the maximum number of modules supported by the Roc.";
        private const string OpCode130_Error63 = "One of the following conditions: 1) The point number exceeds the number in the requested module. 2) The requested point number has an invalid archival type.";
        private const string OpCode131_Error103 = "Industry Canada audit log retrieval error.";
        private const string OpCode132_Error104 = "Industry Canada clear audit log error.";
        private const string OpCode133_Error103 = "Industry Canada audit log retrieval error.";
        private const string OpCode166_Error088 = "Recieved 4 or fewer data bytes, or invalid point type.";
        private const string OpCode166_Error091 = "Point does not exist.";
        private const string OpCode167_Error008 = "More than 250 data bytes in response.";
        private const string OpCode167_Error009 = "Invalid parameter.";
        private const string OpCode167_Error090 = "One of the following conditions: 1) Did not receive 4 data bytes 2) Invalid point type.";
        private const string OpCode167_Error091 = "Point does not exist.";
        private const string OpCode167_Error092 = "Point does not exist.";
        private const string OpCode167_Error093 = "Invalid range of parameters asked for.";
        private const string OpCode167_Error094 = "Too many data bytes to sent (more than 240).";
        private const string OpCode180_Error = "Point or TLPdoes not exist.";
        private const string OpCode181_Error103 = "Received less than 4 data bytes.";
        private const string OpCode181_Error104 = "Point type out of range (1 - 24 are valid).";
        private const string OpCode181_Error105 = "Point does not exist, or invalid parameter.";
        private const string OpCode181_Error106 = "Not enough data bytes recieved.";
        private const string OpCode181_Error251 = "Industry Canada audit log full.";
        private const string OpCode255ResponseErrorMessageFormat = "OpCode: {1}{0}Error Code: {2} - {3}";
        private const string UnknownOpCodeError = "Unknown OpCode error";

        #endregion

        #region Fields

        private static readonly Dictionary<ushort, string> errorMessages = CreateErrorMessages();

        protected byte? errorOpCode { get; set; }

        protected byte? errorCode { get; set; }

        protected byte? errorNumber { get; set; }

        protected byte? errorPointer { get; set; }


        #endregion

        #region Properties

        public byte? ErrorOpCode
        {
            get
            {
                return this.errorOpCode;
            }
            set
            {
                this.errorOpCode = value;
            }
        }

        public byte? ErrorCode
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

        public byte? ErrorNumber
        {
            get
            {
                return this.errorNumber;
            }
            set
            {
                this.errorNumber = value;
            }
        }

        public byte? ErrorPointer
        {
            get
            {
                return this.errorPointer;
            }
            set
            {
                this.errorPointer = value;
            }
        }

        public List<RocPlusError> RocPlusErrors
        {
            get;
            internal set;
        }

        public override int MinimumFrameSize
        {
            get { return 9; }
        }

        public override byte[] Data
        {
            get
            {
                if (ErrorCode.HasValue && ErrorOpCode.HasValue && ErrorPointer.HasValue)
                {
                    return new byte[] { ErrorCode.Value, ErrorOpCode.Value, ErrorPointer.Value };
                }
                else
                {
                    return new byte[] { ErrorCode.Value, ErrorPointer.Value };
                }
            }
        }

        #endregion

        #region Helper Methods

        public override string ToString()
        {

            string message = null;

            switch (this.ErrorCode)
            {
                case Device.OpCode180:
                    message = errorMessages[Convert.ToUInt16(this.ErrorCode << 8)];
                    break;
                default:
                    if (errorMessages.ContainsKey(Convert.ToUInt16(this.ErrorCode)))
                    {
                        message = errorMessages[Convert.ToUInt16((this.ErrorCode << 8) | this.ErrorCode)];
                    }
                    else
                    {
                        message = UnknownOpCodeError;
                    }
                    break;
            }
            return String.Format(CultureInfo.InvariantCulture, OpCode255ResponseErrorMessageFormat, Environment.NewLine, this.ErrorOpCode, this.ErrorCode, message);
        }

        internal static Dictionary<ushort, string> CreateErrorMessages()
        {
            // TODO: Add Roc Plus Specific errors
            Dictionary<ushort, string> messages = new Dictionary<ushort, string>();

            messages.Add(((Device.OpCode120 << 8) | 56), OpCode120_Error56);
            messages.Add(((Device.OpCode121 << 8) | 57), OpCode121_Error57);
            messages.Add(((Device.OpCode122 << 8) | 58), OpCode122_Error58);
            messages.Add(((Device.OpCode126 << 8) | 59), OpCode126_Error59);
            messages.Add(((Device.OpCode130 << 8) | 62), OpCode130_Error62);
            messages.Add(((Device.OpCode130 << 8) | 63), OpCode130_Error63);
            messages.Add(((Device.OpCode131 << 8) | 103), OpCode131_Error103);
            messages.Add(((Device.OpCode132 << 8) | 104), OpCode132_Error104);
            messages.Add(((Device.OpCode133 << 8) | 103), OpCode133_Error103);
            messages.Add(((Device.OpCode166 << 8) | 88), OpCode166_Error088);
            messages.Add(((Device.OpCode166 << 8) | 91), OpCode166_Error091);
            messages.Add(((Device.OpCode167 << 8) | 8), OpCode167_Error008);
            messages.Add(((Device.OpCode167 << 8) | 9), OpCode167_Error009);
            messages.Add(((Device.OpCode167 << 8) | 90), OpCode167_Error090);
            messages.Add(((Device.OpCode167 << 8) | 91), OpCode167_Error091);
            messages.Add(((Device.OpCode167 << 8) | 92), OpCode167_Error092);
            messages.Add(((Device.OpCode167 << 8) | 93), OpCode167_Error093);
            messages.Add(((Device.OpCode167 << 8) | 94), OpCode167_Error094);
            messages.Add(((Device.OpCode180 << 8)), OpCode180_Error);
            messages.Add(((Device.OpCode181 << 8) | 103), OpCode181_Error103);
            messages.Add(((Device.OpCode181 << 8) | 104), OpCode181_Error104);
            messages.Add(((Device.OpCode181 << 8) | 105), OpCode181_Error105);
            messages.Add(((Device.OpCode181 << 8) | 106), OpCode181_Error106);
            messages.Add(((Device.OpCode181 << 8) | 251), OpCode181_Error251);

            return messages;
        }

        void IRocResponseMessage.Initialize(byte[] frame)
        {
            base.Initialize(frame);
        }

        void IRocResponseMessage.Initialize(byte[] frame, IRocRequestMessage requestMessage)
        {
            base.Initialize(frame);

            if (frame.Length < MinimumFrameSize)
            {
                throw new FormatException("Message frame does not contain enough bytes.");
            }

            ErrorOpCode = requestMessage.OpCode;

            switch (frame.Length)
            {
                case 9:
                    ErrorCode = frame[5];
                    ErrorNumber = frame[6];
                    break;
                case 10:
                    ErrorCode = frame[5];
                    ErrorNumber = frame[6];
                    ErrorPointer = frame[7];
                    break;
                default:
                    {
                        var messageFrameLengthNoCrc = Convert.ToByte(frame.Length - 2);
                        RocPlusErrors = new List<RocPlusError>();
                        for (var i = 5; messageFrameLengthNoCrc > i; i += 2)
                        {
                            RocPlusErrors.Add(new RocPlusError() { ErrorCode = frame[i - 1], Offset = frame[i] });
                        }
                    }
                    break;
            }
        }

        #endregion

    }
}
