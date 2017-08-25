#region Imported Types

using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode080Request : ROCMessage, IROCRequestMessage
    {

        #region Properties

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
                var dataBytes = new List<byte>();
                switch (Function)
                {
                    case OpCode080Function.ReadFst0HeaderInformation:
                        dataBytes.Add(0);
                        dataBytes.Add(0);
                        dataBytes.Add(3);
                        break;
                    case OpCode080Function.ReadFst1HeaderInformation:
                        dataBytes.Add(0);
                        dataBytes.Add(1);
                        dataBytes.Add(3);
                        break;
                    case OpCode080Function.ReadFst0Code:
                        dataBytes.Add(0);
                        dataBytes.Add(0);
                        dataBytes.Add(2);
                        dataBytes.Add(0);
                        dataBytes.Add(Offset.Value);
                        dataBytes.Add(Length.Value);
                        break;
                    case OpCode080Function.ReadFst1Code: 
                        dataBytes.Add(0);
                        dataBytes.Add(1);
                        dataBytes.Add(2);
                        dataBytes.Add(0);
                        dataBytes.Add(Offset.Value);
                        dataBytes.Add(Length.Value);
                        break;
                }
                return dataBytes.ToArray();
            }
        }

        public OpCode080Function Function
        {
            get;
            private set;
        }

        public byte? Offset
        {
            get;
            private set;
        }

        public byte? Length
        {
            get;
            private set;
        }

        #endregion

        #region Constructor(s)

        public OpCode080Request()
        {
        }

        public OpCode080Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, OpCode080Function function, byte? offset, byte? length)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode80)
        {
            this.Function = function;
            Offset = offset;
            Length = length;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IROCResponseMessage response)
        {
            var opCode080Response = response as OpCode080Response;
            Debug.Assert(opCode080Response != null, "Argument response should be of type OpCode080Response.");
        }

        #endregion

    }
}
