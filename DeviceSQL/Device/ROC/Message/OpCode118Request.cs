#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode118Request : RocMessage, IRocRequestMessage
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

        public OpCode118Request()
        {
        }

        public OpCode118Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte numberOfAlarmsRequested, ushort startingAlarmIndexPointer)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode118)
        {
            this.NumberOfAlarmsRequested = numberOfAlarmsRequested;
            this.StartingAlarmIndexPointer = startingAlarmIndexPointer;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IRocResponseMessage response)
        {
            var opCode118Response = response as OpCode118Response;
            Debug.Assert(opCode118Response != null, "Argument response should be of type OpCode118Response.");

            if (opCode118Response.NumberOfAlarms != NumberOfAlarmsRequested)
            {
                throw new FormatException("Number of alarms recieved does not equal number of alarms requested.");
            }

            var meterAlarms = opCode118Response.RocPlusAlarms;
            if (meterAlarms.Count > 0)
            {
                if (meterAlarms.First().Index != StartingAlarmIndexPointer)
                {
                    throw new FormatException("Starting alarm index recieved does not equal starting alarm index requested.");
                }
            }
        }

        #endregion

    }
}
