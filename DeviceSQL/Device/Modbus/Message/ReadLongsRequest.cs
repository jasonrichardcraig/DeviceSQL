#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

#endregion

namespace DeviceSQL.Device.Modbus.Message

{
    internal class ReadLongsRequest : ModbusMessage, IModbusRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.Long;
            }
        }

        public List<LongRegister> LongRegisters
        {
            get;
            set;
        }

        public override int MinimumFrameSize
        {
            get
            {
                return 7;
            }
        }

        public override byte[] Data
        {
            get
            {
                var data = new List<byte>();

                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)LongRegisters.First().Address.AbsoluteAddress)));
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)LongRegisters.Count)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadLongsRequest()
        {
        }

        public ReadLongsRequest(ushort unitId, List<LongRegister> longRegisters, bool isExtendedUnitId)
            : base(unitId, Device.ReadHoldingRegisters)
        {
            this.LongRegisters = longRegisters;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IModbusResponseMessage response)
        {
            var readLongsResponse = response as ReadLongsResponse;
            Debug.Assert(readLongsResponse != null, "Argument response should be of type ReadLongsResponse.");

            if (this.LongRegisters.Count != (readLongsResponse.Data.Length - 1) / 4)
            {
                throw new FormatException("Number of long registers recieved does not equal number of long registers requested.");
            }
        }

        #endregion

    }
}
