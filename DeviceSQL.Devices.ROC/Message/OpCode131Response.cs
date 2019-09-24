﻿#region Imported Types

using DeviceSQL.Device.ROC.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode131Response : ROCMessage, IROCResponseMessage
    {

        #region Fields

        private byte[] data;

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

        public List<AuditLogRecord> AuditLogRecords
        {
            get
            {
                byte auditLogRecordCount = Data[0];
                ushort startIndex = BitConverter.ToUInt16(Data,1);

                var auditLogRecords = new List<AuditLogRecord>(auditLogRecordCount);

                for (ushort auditLogRecordIndex = 5; auditLogRecordIndex < Data.Length; auditLogRecordIndex += 24)
                {
                    ushort relativeAuditLogRecordIndex = (ushort)(startIndex + ((auditLogRecordIndex - 5) / 24));

                    var eventBytes = Data.ToList().GetRange(auditLogRecordIndex, 24).ToArray();

                    auditLogRecords.Add(new AuditLogRecord(relativeAuditLogRecordIndex, eventBytes));
                }

                return auditLogRecords;
            }
        }

        #endregion

        #region Helper Methods

        void IROCResponseMessage.Initialize(byte[] frame)
        {
            base.Initialize(frame);
        }

        void IROCResponseMessage.Initialize(byte[] frame, IROCRequestMessage requestMessage)
        {
            base.Initialize(frame);

            var dataLength = frame[5];

            data = new byte[dataLength];

            Buffer.BlockCopy(frame, 6, data, 0, dataLength);
        }

        #endregion

    }
}
