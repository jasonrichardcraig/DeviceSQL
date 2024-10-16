#region Imported Types

using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode165Request : RocMessage, IRocRequestMessage
    {

        #region Properties

        public bool SetConfigurableHistoricalData
        {
            get;
            set;
        }

        public byte HistoricalRamArea
        {
            get;
            set;
        }

        public byte StartingDatabaseNumber
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

                data.Add((byte)(SetConfigurableHistoricalData ? 1 : 0));
                data.Add(HistoricalRamArea);
                data.Add(StartingDatabaseNumber);
                if (!SetConfigurableHistoricalData)
                {
                    data.Add(0);
                }
                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode165Request()
        {
        }

        public OpCode165Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte historicalRamArea, byte startingDatabaseNumber)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode165)
        {
            this.HistoricalRamArea = historicalRamArea;
            this.StartingDatabaseNumber = startingDatabaseNumber;
            this.SetConfigurableHistoricalData = false;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IRocResponseMessage response)
        {
            var opCode118Response = response as OpCode165Response;
            Debug.Assert(opCode118Response != null, "Argument response should be of type OpCode165Response.");
        }

        #endregion

    }
}
