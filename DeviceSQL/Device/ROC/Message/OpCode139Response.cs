#region Imported Types

using DeviceSQL.Device.Roc.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode139Response : RocMessage, IRocResponseMessage
    {

        #region Fields

        private byte[] data;
        private byte requestCommand;
        private ushort historyIndex;
        private List<byte> requestedHistoryPoints;

        #endregion

        #region Properties

        public override int MinimumFrameSize
        {
            get
            {
                return 10;
            }
        }

        public override byte[] Data
        {
            get
            {
                return data;
            }
        }

        public byte Command
        {
            get
            {
                return Data[0];
            }
        }

        public byte HistorySegment
        {
            get
            {
                return Data[0];
            }
        }

        public ushort CurrentIndex
        {
            get
            {
                if (Command == 1)
                {
                    return BitConverter.ToUInt16(Data, 2);
                }
                else
                {
                    return 0;
                }
            }
        }

        public byte NumberOfTimePeriods
        {
            get
            {
                if (Command == 1)
                {
                    return Data[4];
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool RequestTimeStamps
        {
            get
            {
                if (Command == 1)
                {
                    return (Data[5] == 1 ? true : false);
                }
                else
                {
                    return false;
                }
            }
        }

        public byte NumberOfPoints
        {
            get
            {
                if (Command == 1)
                {
                    return Data[6];
                }
                else
                {
                    return 0;
                }
            }
        }

        public byte NumberOfConfiguredPoints
        {
            get
            {
                if (Command == 0)
                {
                    return Data[2];
                }
                else
                {
                    return 0;
                }
            }
        }

        public List<byte> ConfiguredPoints
        {
            get
            {
                if (Command == 0)
                {
                    return this.Data.Skip(3).ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        public List<RocPlusHistoryRecord> MeterHistory
        {
            get
            {
                if (Command != 1)
                {
                    return null;
                }
                else
                {
                    var recordCount = (NumberOfTimePeriods * NumberOfPoints);
                    var index = historyIndex;
                    var requestTimeStamps = RequestTimeStamps;
                    var recordOffset = requestTimeStamps ? (4 + (NumberOfPoints * (4))) : (NumberOfPoints * (4));

                    var meterHistory = new List<RocPlusHistoryRecord>(recordCount);

                    for (var offset = 7; (offset + (NumberOfPoints * (4))) < Data.Length; offset += recordOffset)
                    {
                        var dataOffset = requestTimeStamps ? offset + 4 : offset;

                        foreach (var historyPoint in requestedHistoryPoints)
                        {
                            if (requestTimeStamps)
                            {
                                meterHistory.Add(new RocPlusHistoryRecord() { HistorySegment = HistorySegment, HistoryPointNumber = historyPoint, Index = index, Value = new byte[] { Data[offset], Data[offset + 1], Data[offset + 2], Data[offset + 3], Data[dataOffset + 0], Data[dataOffset + 1], Data[dataOffset + 2], Data[dataOffset + 3] } });
                            }
                            else
                            {
                                meterHistory.Add(new RocPlusHistoryRecord() { HistorySegment = HistorySegment, HistoryPointNumber = historyPoint, Index = index, Value = new byte[] { Data[dataOffset + 0], Data[dataOffset + 1], Data[dataOffset + 2], Data[dataOffset + 3] } });
                            }
                            dataOffset += 4;
                        }
                        index++;
                    }
                    return meterHistory;
                }
            }
        }

        #endregion

        #region Helper Methods

        void IRocResponseMessage.Initialize(byte[] frame)
        {
            base.Initialize(frame);
        }

        void IRocResponseMessage.Initialize(byte[] frame, IRocRequestMessage requestMessage)
        {

            var opCode139RequestMessage = requestMessage as OpCode139Request;

            if (opCode139RequestMessage != null)
            {
                switch (opCode139RequestMessage.Command)
                {
                    case 0:
                        requestCommand = opCode139RequestMessage.Command;
                        break;
                    case 1:
                        historyIndex = opCode139RequestMessage.HistoryIndex;
                        requestedHistoryPoints = new List<byte>(opCode139RequestMessage.RequestedHistoryPoints.ToArray());
                        break;
                }
            }

            base.Initialize(frame);

            var dataLength = frame[5];

            data = new byte[dataLength];

            Buffer.BlockCopy(frame, 6, data, 0, dataLength);
        }

        #endregion

    }
}
