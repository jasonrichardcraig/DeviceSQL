#region Imported Types

using DeviceSQL.Device.Modbus.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.Modbus
{
    public class ModbusSlaveException : Exception
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
                return this.ModbusErrorResponse.ErrorCode;
            }
        }

        internal ModbusErrorResponse ModbusErrorResponse
        {
            get;
            private set;
        }

        public override string Message
        {
            get
            {
                return String.Concat(base.Message, this.ModbusErrorResponse != null ? String.Concat(Environment.NewLine, this.ModbusErrorResponse) : String.Empty);
            }
        }

        #endregion

        #region Constructor(s)

        public ModbusSlaveException()
        {
        }

        public ModbusSlaveException(string message)
            : base(message)
        {
        }

        public ModbusSlaveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal ModbusSlaveException(ModbusErrorResponse ModbusErrorResponse)
        {
            this.ModbusErrorResponse = ModbusErrorResponse;
        }

        #endregion

    }
}
