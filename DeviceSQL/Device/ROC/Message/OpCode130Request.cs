#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode130Request : ROCMessage, IROCRequestMessage
    {

        #region Properties

        public byte HistoricalRamArea
        {
            get;
            set;
        }

        public byte HistoryPointNumber
        {
            get;
            set;
        }

        public byte NumberOfHistoryRecordsRequested
        {
            get;
            set;
        }

        public ushort StartingHistoryIndexPointer
        {
            get;
            set;
        }

        public override int MinimumFrameSize
        {
            get
            {
                return 11;
            }
        }

        public override byte[] Data
        {
            get
            {
                var data = new List<byte>();

                data.Add(HistoricalRamArea);
                data.Add(HistoryPointNumber);
                data.Add(NumberOfHistoryRecordsRequested);
                data.AddRange(BitConverter.GetBytes(StartingHistoryIndexPointer));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode130Request()
        {
        }

        public OpCode130Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte historicalRamArea, byte historyPointNumber, byte numberOfHistoryRecordsRequested, ushort startingHistoryIndexPointer)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode130)
        {
            this.HistoricalRamArea = historicalRamArea;
            this.HistoryPointNumber = historyPointNumber;
            this.NumberOfHistoryRecordsRequested = numberOfHistoryRecordsRequested;
            this.StartingHistoryIndexPointer = startingHistoryIndexPointer;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IROCResponseMessage response)
        {
            var opCode130Response = response as OpCode130Response;
            Debug.Assert(opCode130Response != null, "Argument response should be of type OpCode130Response.");

            var meterHistory = opCode130Response.MeterHistory;

            if (meterHistory.Count != NumberOfHistoryRecordsRequested)
            {
                throw new FormatException("Number of history records recieved does not equal number of history records requested.");
            }

            if (meterHistory.First().Index != StartingHistoryIndexPointer)
            {
                throw new FormatException("Starting history record index recieved does not equal starting history record index requested.");
            }
        }

        #endregion

    }
}
