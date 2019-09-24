#region Imported Types

using DeviceSQL.Device.MODBUS.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.MODBUS
{
    public class MODBUSSlaveException : Exception
    {

        #region Constants

        private const string SlaveAddressPropertyName = "SlaveAdress";
        private const string FunctionCodePropertyName = "FunctionCode";
        private const string SlaveExceptionCodePropertyName = "SlaveExceptionCode";

        #endregion

        #region Properties

        public byte ErrorCode
        {
            get
            {
                return this.MODBUSErrorResponse.ErrorCode;
            }
        }

        internal MODBUSErrorResponse MODBUSErrorResponse
        {
            get;
            private set;
        }

        public override string Message
        {
            get
            {
                return String.Concat(base.Message, this.MODBUSErrorResponse != null ? String.Concat(Environment.NewLine, this.MODBUSErrorResponse) : String.Empty);
            }
        }

        #endregion

        #region Constructor(s)

        public MODBUSSlaveException()
        {
        }

        public MODBUSSlaveException(string message)
            : base(message)
        {
        }

        public MODBUSSlaveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal MODBUSSlaveException(MODBUSErrorResponse MODBUSErrorResponse)
        {
            this.MODBUSErrorResponse = MODBUSErrorResponse;
        }

        #endregion

    }
}
