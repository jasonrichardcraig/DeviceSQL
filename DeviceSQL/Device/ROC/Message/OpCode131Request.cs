#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode131Request : ROCMessage, IROCRequestMessage
    {

        #region Properties

        public byte NumberOfAuditLogRecordsRequested
        {
            get;
            set;
        }

        public ushort StartingAuditLogRecordIndexPointer
        {
            get;
            set;
        }

        public override int MinimumFrameSize
        {
            get
            {
                return 8;
            }
        }

        public override byte[] Data
        {
            get
            {
                var data = new List<byte>();

                data.Add(NumberOfAuditLogRecordsRequested);
                data.AddRange(BitConverter.GetBytes(StartingAuditLogRecordIndexPointer));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode131Request()
        {
        }

        public OpCode131Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte numberOfAuditLogRecordsRequested, ushort startingAuditLogRecordIndexPointer)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode131)
        {
            this.NumberOfAuditLogRecordsRequested = numberOfAuditLogRecordsRequested;
            this.StartingAuditLogRecordIndexPointer = startingAuditLogRecordIndexPointer;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IROCResponseMessage response)
        {
            var opCode131Response = response as OpCode131Response;
            Debug.Assert(opCode131Response != null, "Argument response should be of type OpCode131Response.");

            var meterEvents = opCode131Response.AuditLogRecords;

            if (meterEvents.Count != NumberOfAuditLogRecordsRequested)
            {
                throw new FormatException("Number of audit log events recieved does not equal number of events requested.");
            }

            if (meterEvents.First().Index != StartingAuditLogRecordIndexPointer)
            {
                throw new FormatException("Starting audit log event index recieved does not equal to starting event index requested.");
            }

        }

        #endregion

    }
}
