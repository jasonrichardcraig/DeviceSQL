#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode139Request : RocMessage, IRocRequestMessage
    {

        #region Properties

        public byte Command
        {
            get;
            private set;
        }

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

        public bool RequestTimeStamps
        {
            get;
            set;
        }

        public List<byte> RequestedHistoryPoints
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
                switch (Command)
                {
                    case 0:
                        return 8;
                    case 1:
                        return 13;
                    default:
                        throw new Exception("Unsupported Command");
                }
            }
        }

        public override byte[] Data
        {
            get
            {
                var data = new List<byte>();

                data.Add(Command);
                data.Add(HistorySegment);

                switch (Command)
                {
                    case 0:
                        break;
                    case 1:
                        data.AddRange(BitConverter.GetBytes(HistoryIndex));
                        data.Add(HistoryType);
                        data.Add(NumberOfTimePeriods);
                        data.Add((byte)(RequestTimeStamps ? 1 : 0));
                        data.Add((byte)RequestedHistoryPoints.Count);
                        data.AddRange(RequestedHistoryPoints);
                        break;
                    default:
                        throw new Exception("Unsupported Command");
                }

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode139Request()
        {
        }

        public OpCode139Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte historySegment)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode139)
        {
            Command = 0;
            HistorySegment = historySegment;
        }

        public OpCode139Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte historySegment, ushort historyIndex, byte historyType, bool requestTimeStamps, List<byte> requestedHistoryPoints, byte numberOfTimePeriods)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode139)
        {
            this.Command = 1;
            this.HistorySegment = historySegment;
            this.HistoryIndex = historyIndex;
            this.HistoryType = historyType;
            this.RequestTimeStamps = requestTimeStamps;
            this.RequestedHistoryPoints = requestedHistoryPoints;
            this.NumberOfTimePeriods = numberOfTimePeriods;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IRocResponseMessage response)
        {
            var opCode139Response = response as OpCode139Response;
            Debug.Assert(opCode139Response != null, "Argument response should be of type OpCode139Response.");

            switch (Command)
            {
                case 0:
                    if (opCode139Response.Command != 0)
                    {
                        throw new FormatException("Command requested does not equal command recieved");
                    }
                    break;
                case 1:
                    {
                        if (opCode139Response.Command != 1)
                        {
                            throw new FormatException("Command requested does not equal command recieved");
                        }

                        if (opCode139Response.NumberOfPoints != RequestedHistoryPoints.Count)
                        {
                            throw new FormatException("Number of history points recieved does not equal number of history points requested.");
                        }

                        if (opCode139Response.NumberOfTimePeriods != NumberOfTimePeriods)
                        {
                            throw new FormatException("Number of history points recieved does not equal number of history points requested.");
                        }

                        if (opCode139Response.NumberOfPoints > 0)
                        {
                            var meterHistory = opCode139Response.MeterHistory;

                            if (meterHistory.First().HistoryPointNumber != RequestedHistoryPoints.First())
                            {
                                throw new FormatException("Starting history point recieved does not equal starting history point requested.");
                            }

                            if (meterHistory.First().Index != HistoryIndex)
                            {
                                throw new FormatException("Starting history record index recieved does not equal starting history record index requested.");
                            }
                        }
                    }
                    break;
                default:
                    throw new Exception("Unsupported Command");
            }
        }

        #endregion

    }
}
