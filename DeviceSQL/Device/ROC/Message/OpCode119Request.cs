#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode119Request : RocMessage, IRocRequestMessage
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

        public OpCode119Request()
        {
        }

        public OpCode119Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte numberOfEventsRequested, ushort startingEventIndexPointer)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode119)
        {
            this.NumberOfEventsRequested = numberOfEventsRequested;
            this.StartingEventIndexPointer = startingEventIndexPointer;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IRocResponseMessage response)
        {
            var opCode119Response = response as OpCode119Response;
            Debug.Assert(opCode119Response != null, "Argument response should be of type OpCode119Response.");

            if (opCode119Response.NumberOfEvents != NumberOfEventsRequested)
            {
                throw new FormatException("Number of events recieved does not equal number of events requested.");
            }

            var meterEvents = opCode119Response.RocPlusEvents;

            if (meterEvents.Count > 0)
            {
                if (meterEvents.First().Index != StartingEventIndexPointer)
                {
                    throw new FormatException("Starting event index recieved does not equal starting event index requested.");
                }
            }
        }

        #endregion

    }
}
