#region Imported Types

using DeviceSQL.Device.ROC.Message;
using System;

#endregion

namespace DeviceSQL.Device.ROC
{
    public class OpCode255Exception : Exception
    {

        #region Constants

        private const string SlaveAddressPropertyName = "SlaveAdress";
        private const string FunctionCodePropertyName = "FunctionCode";
        private const string SlaveExceptionCodePropertyName = "SlaveExceptionCode";

        #endregion

        #region Fields

        private readonly OpCode255Response opCode255Response;

        #endregion

        #region Constructor(s)

        public OpCode255Exception()
        {
        }

        public OpCode255Exception(string message)
            : base(message)
        {
        }

        public OpCode255Exception(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal OpCode255Exception(OpCode255Response opCode255Response)
        {
            this.opCode255Response = opCode255Response;
        }

        #endregion

        #region Properties

        public override string Message
        {
            get
            {
                return String.Concat(base.Message, opCode255Response != null ? String.Concat(Environment.NewLine, opCode255Response) : String.Empty);
            }
        }

        public byte OpCode
        {
            get
            {
                return opCode255Response != null ? opCode255Response.OpCode : (byte)0;
            }
        }

        public byte? ErrorCode
        {
            get
            {
                return opCode255Response != null ? opCode255Response.ErrorCode : (byte?)null;
            }
        }

        public byte DestinationUnit
        {
            get
            {
                return opCode255Response != null ? opCode255Response.DestinationUnit : (byte)0;
            }
        }

        public byte DestinationGroup
        {
            get
            {
                return opCode255Response != null ? opCode255Response.DestinationGroup : (byte)0;
            }
        }

        public byte SourceUnit
        {
            get
            {
                return opCode255Response != null ? opCode255Response.SourceUnit : (byte)0;
            }
        }

        public byte SourceGroup
        {
            get
            {
                return opCode255Response != null ? opCode255Response.SourceGroup : (byte)0;
            }
        }

        #endregion

    }
}
