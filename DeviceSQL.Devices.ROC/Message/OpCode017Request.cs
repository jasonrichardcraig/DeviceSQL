#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode017Request : ROCMessage, IROCRequestMessage
    {

        #region Enums

        public enum LoginType
        {
            Set_OperatorId,
            Set_OperatorId_Password,
            Set_OperatorId_Password_AccessLevel
        }

        #endregion

        #region Properties

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
                var loginBytes = new List<byte>();
                loginBytes.AddRange(System.Text.ASCIIEncoding.Default.GetBytes(OperatorId.PadRight(3).Substring(0, 3)));

                switch (RequestLoginType)
                {
                    case LoginType.Set_OperatorId_Password:
                        loginBytes.AddRange(BitConverter.GetBytes(Password));
                        break;
                    case LoginType.Set_OperatorId_Password_AccessLevel:
                        loginBytes.AddRange(BitConverter.GetBytes(Password));
                        loginBytes.AddRange(System.Text.ASCIIEncoding.Default.GetBytes(AccessLevel.PadRight(6).Substring(0, 6)));
                        break;     
                }
                
                return loginBytes.ToArray();
            }
        }

        public LoginType RequestLoginType
        {
            get;
            set;
        }

        public string OperatorId
        {
            get;
            private set;
        }

        public string AccessLevel
        {
            get;
            private set;
        }

        public UInt16 Password
        {
            get;
            private set;
        }

        #endregion

        #region Constructor(s)

        public OpCode017Request()
        {
        }

        public OpCode017Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, string operatorId)
    : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode17)
        {
            this.OperatorId = operatorId;
            RequestLoginType = LoginType.Set_OperatorId;
        }

        public OpCode017Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, string operatorId, UInt16 password)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode17)
        {
            this.OperatorId = operatorId;
            this.Password = password;
            RequestLoginType = LoginType.Set_OperatorId_Password;
        }

        public OpCode017Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, string operatorId, UInt16 password, string accessLevel)
    : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode17)
        {
            this.OperatorId = operatorId;
            this.Password = password;
            this.AccessLevel = accessLevel;
            RequestLoginType = LoginType.Set_OperatorId_Password_AccessLevel;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IROCResponseMessage response)
        {
            var opCode017Response = response as OpCode017Response;
            Debug.Assert(opCode017Response != null, "Argument response should be of type OpCode017Response.");
        }

        #endregion

    }
}
