#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode122Request : RocMessage, IRocRequestMessage
    {

        #region Properties

        public byte NumberOfEventsRequested
        {
            get;
            set;
        }

        public ushort StartingEventIndexPointer
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

                data.Add(NumberOfEventsRequested);
                data.AddRange(BitConverter.GetBytes(StartingEventIndexPointer));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode122Request()
        {
        }

        public OpCode122Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte numberOfEventsRequested, ushort startingEventIndexPointer)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode122)
        {
            this.NumberOfEventsRequested = numberOfEventsRequested;
            this.StartingEventIndexPointer = startingEventIndexPointer;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IRocResponseMessage response)
        {
            var opCode122Response = response as OpCode122Response;
            Debug.Assert(opCode122Response != null, "Argument response should be of type OpCode122Response.");

            var meterEvents = opCode122Response.MeterEvents;

            if (meterEvents.Count != NumberOfEventsRequested)
            {
                throw new FormatException("Number of events recieved does not equal number of events requested.");
            }

            if (meterEvents.First().Index != StartingEventIndexPointer)
            {
                throw new FormatException("Starting event index recieved does not equal starting event index requested.");
            }

        }

        #endregion

    }
}
