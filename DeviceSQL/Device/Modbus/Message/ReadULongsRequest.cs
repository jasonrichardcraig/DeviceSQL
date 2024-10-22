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
    internal class ReadULongsRequest : ModbusMessage, IModbusRequestMessage
    {

        #region Properties

        public Device.DataType DataType
        {
            get
            {
                return Device.DataType.Long;
            }
        }

        public List<ULongRegister> ULongRegisters
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

                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)ULongRegisters.First().Address.AbsoluteAddress)));
                data.AddRange(BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)ULongRegisters.Count)));

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public ReadULongsRequest()
        {
        }

        public ReadULongsRequest(ushort unitId, List<ULongRegister> uLongRegisters, bool isExtendedUnitId)
            : base(unitId, Device.ReadHoldingRegisters)
        {
            this.ULongRegisters = uLongRegisters;
            this.IsExtendedUnitId = isExtendedUnitId;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IModbusResponseMessage response)
        {
            var readULongsResponse = response as ReadULongsResponse;
            Debug.Assert(readULongsResponse != null, "Argument response should be of type ReadULongsResponse.");

            if (this.ULongRegisters.Count != (readULongsResponse.Data.Length - 1) / 4)
            {
                throw new FormatException("Number of ulong registers recieved does not equal number of ulong registers requested.");
            }
        }

        #endregion

    }
}
