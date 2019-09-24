#region Imported Types

using System;
using System.Collections.Generic;
using System.Globalization;

#endregion

namespace DeviceSQL.Device.MODBUS.Message
{
    internal abstract class MODBUSMessage : IMODBUSMessage
    {

        #region Properties

        public bool IsExtendedUnitId
        {
            get;
            set;
        }

        public ushort UnitId { get; set; }

        public byte FunctionCode { get; set; }

        public abstract Byte[] Data { get; }

        public byte[] MessageFrame
        {
            get
            {
                List<byte> frame = new List<byte>();
                if (IsExtendedUnitId)
                {
                    var unitIdBytes = BitConverter.GetBytes(UnitId);
                    frame.Add(unitIdBytes[1]);
                    frame.Add(unitIdBytes[0]);
                }
                else
                {
                    frame.Add((byte)UnitId);
                }
                frame.AddRange(ProtocolDataUnit);

                return frame.ToArray();
            }
        }

        public byte[] ProtocolDataUnit
        {
            get
            {
                List<byte> pdu = new List<byte>();

                pdu.Add(FunctionCode);

                var data = Data;
                var dataLength = 0;

                if (data != null)
                {
                    dataLength = data.Length;
                }

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

        public MODBUSMessage()
        {
        }

        public MODBUSMessage(ushort unitId, byte functionCode)
        {
            this.UnitId = unitId;
            this.FunctionCode = functionCode;
        }

        #endregion

        #region Helper Methods

        protected void Initialize(byte[] frame, bool isExtendedUnitId)
        {
            if (frame == null)
                throw new ArgumentNullException("frame", "Argument frame cannot be null.");

            if (frame.Length < (IsExtendedUnitId ? Device.MinimumFrameSizeWithExtendedUnitId : Device.MinimumFrameSize))
                throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Message frame must contain at least {0} bytes of data.", Device.MinimumFrameSize));

            this.IsExtendedUnitId = isExtendedUnitId;

            if (this.IsExtendedUnitId)
            {
                this.UnitId = BitConverter.ToUInt16(new byte[] { frame[1], frame[0] }, 0);
                this.FunctionCode = frame[2];
            }
            else
            {
                this.UnitId = frame[0];
                this.FunctionCode = frame[1];
            }
        }

        #endregion

    }
}
