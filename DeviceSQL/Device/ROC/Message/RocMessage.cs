#region Imported Types

using System;
using System.Collections.Generic;
using System.Globalization;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal abstract class RocMessage : IRocMessage
    {

        #region Properties

        public byte SourceUnit {get; set;}

        public byte SourceGroup { get; set; }

        public byte DestinationUnit { get; set; }

        public byte DestinationGroup { get; set; }

        public byte OpCode { get; set; }

        public abstract Byte[] Data { get; }

        public byte[] MessageFrame
        {
            get
            {
                List<byte> frame = new List<byte>();
                frame.Add(DestinationUnit);
                frame.Add(DestinationGroup);
                frame.Add(SourceUnit);
                frame.Add(SourceGroup);
                frame.AddRange(ProtocolDataUnit);

                return frame.ToArray();
            }
        }

        public byte[] ProtocolDataUnit
        {
            get
            {
                List<byte> pdu = new List<byte>();

                pdu.Add(OpCode);

                var data = Data;
                var dataLength = 0;

                if (data != null)
                {
                    dataLength = data.Length;
                }

                pdu.Add((byte)dataLength);

                if (dataLength > 0)
                {
                    pdu.AddRange(data);
                }
                return pdu.ToArray();
            }
        }

        public abstract int MinimumFrameSize { get; }

        #endregion

        #region Constructor(s)

        public RocMessage()
        {
        }

        public RocMessage(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte opCode)
        {
            this.DestinationUnit = destinationUnit;
            this.DestinationGroup = destinationGroup;
            this.SourceUnit = sourceUnit;
            this.SourceGroup = sourceGroup;
            this.OpCode = opCode;
        }

        #endregion

        #region Helper Methods

        protected void Initialize(byte[] frame)
        {
            if (frame == null)
                throw new ArgumentNullException("frame", "Argument frame cannot be null.");

            if (frame.Length < Device.MinimumFrameSize)
                throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Message frame must contain at least {0} bytes of data.", Device.MinimumFrameSize));

            this.DestinationUnit = frame[0];
            this.DestinationGroup = frame[1];
            this.SourceUnit = frame[2];
            this.SourceGroup = frame[3];
            this.OpCode = frame[4];
        }

        #endregion

    }
}
