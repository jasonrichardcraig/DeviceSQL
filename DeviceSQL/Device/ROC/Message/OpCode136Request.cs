#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode136Request : RocMessage, IRocRequestMessage
    {

        #region Properties

        public byte HistorySegment
        {
            get;
            set;
        }

        public ushort HistoryIndex
        {
            get;
            set;
        }

        public byte HistoryType
        {
            get;
            set;
        }

        public byte StartingHistoryPoint
        {
            get;
            set;
        }

        public byte NumberOfHistoryPoints
        {
            get;
            set;
        }

        public byte NumberOfTimePeriods
        {
            get;
            set;
        }

        public override int MinimumFrameSize
        {
            get
            {
                return 13;
            }
        }

        public override byte[] Data
        {
            get
            {
                var data = new List<byte>();

                data.Add(HistorySegment);
                data.AddRange(BitConverter.GetBytes(HistoryIndex));
                data.Add(HistoryType);
                data.Add(StartingHistoryPoint);
                data.Add(NumberOfHistoryPoints);
                data.Add(NumberOfTimePeriods);

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode136Request()
        {
        }

        public OpCode136Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte historySegment, ushort historyIndex, byte historyType, byte startingHistoryPoint, byte numberOfHistoryPoints, byte numberOfTimePeriods)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode136)
        {
            this.HistorySegment = historySegment;
            this.HistoryIndex = historyIndex;
            this.HistoryType = historyType;
            this.StartingHistoryPoint = startingHistoryPoint;
            this.NumberOfHistoryPoints = numberOfHistoryPoints;
            this.NumberOfTimePeriods = numberOfTimePeriods;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IRocResponseMessage response)
        {
            var opCode136Response = response as OpCode136Response;
            Debug.Assert(opCode136Response != null, "Argument response should be of type OpCode136Response.");

            if (opCode136Response.NumberOfDataElements != ((NumberOfHistoryPoints + 1) * NumberOfTimePeriods))
            {
                throw new FormatException("Number of history records recieved does not equal number of history records requested.");
            }

            var meterHistory = opCode136Response.MeterHistory;

            if (meterHistory.First().HistoryPointNumber != StartingHistoryPoint)
            {
                throw new FormatException("Starting history point recieved does not equal starting history point requested.");
            }

            if (meterHistory.First().Index != HistoryIndex)
            {
                throw new FormatException("Starting history record index recieved does not equal starting history record index requested.");
            }

        }

        #endregion

    }
}
