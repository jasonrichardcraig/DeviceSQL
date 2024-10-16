#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode121Request : RocMessage, IRocRequestMessage
    {

        #region Properties

        public byte NumberOfAlarmsRequested
        {
            get;
            set;
        }

        public ushort StartingAlarmIndexPointer
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

                data.Add(NumberOfAlarmsRequested);
                data.AddRange(BitConverter.GetBytes(StartingAlarmIndexPointer));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode121Request()
        {
        }

        public OpCode121Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte numberOfAlarmsRequested, ushort startingAlarmIndexPointer)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode121)
        {
            this.NumberOfAlarmsRequested = numberOfAlarmsRequested;
            this.StartingAlarmIndexPointer = startingAlarmIndexPointer;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IRocResponseMessage response)
        {
            var opCode121Response = response as OpCode121Response;
            Debug.Assert(opCode121Response != null, "Argument response should be of type OpCode121Response.");

            var meterAlarms = opCode121Response.MeterAlarms;

            if (meterAlarms.Count != NumberOfAlarmsRequested)
            {
                throw new FormatException("Number of alarms recieved does not equal number of alarms requested.");
            }

            if (meterAlarms.First().Index != StartingAlarmIndexPointer)
            {
                throw new FormatException("Starting alarm index recieved does not equal starting alarm index requested.");
            }


        }

        #endregion

    }
}
