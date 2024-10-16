#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using DeviceSQL.Device.Modbus.IO;
using DeviceSQL.Device.Modbus.Message;
using DeviceSQL.IO.Channels;
using DeviceSQL.IO.Channels.Transport;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Modbus
{
    public class ModbusMaster : IDevice
    {

        #region Fields

        private Transport transport;

        #endregion

        #region Properties

        public Transport Transport
        {
            get
            {
                return transport;
            }
            set
            {
                transport = value;
            }
        }

        public string Name
        {
            get;
            set;
        }

        public ushort UnitId
        {
            get; set;

        }

        public bool UseExtendedAddressing
        {
            get;
            set;
        }

        ITransport IDevice.Transport
        {
            get
            {
                return transport;
            }
            set
            {
                transport = value as Transport;
            }
        }

        public string Address
        {
            get
            {
                return string.Format("Device Address={0};Use Extended Addressing={1}", UnitId.ToString(), UseExtendedAddressing.ToString());
            }
        }

        #endregion

        #region Constructor(s)

        public ModbusMaster()
        {
        }

        public ModbusMaster(IChannel channel)
        {
            this.transport = new Transport(channel);
        }

        #endregion

        #region Register Methods

        internal bool RegisterListIsContiguous<T>(List<T> registers) where T : ModbusRegister
        {
            var registerCount = registers.Count;

            if (registerCount > 0)
            {
                var registerQuery = from r in registers
                                    orderby r.Address.AbsoluteAddress
                                    select r;

                var registerArray = registerQuery.ToArray();

                ushort previousAddress = registerQuery.First().Address.AbsoluteAddress;

                for (int i = 1; i <= registerCount - 1; i++)
                {
                    if (previousAddress != (registerArray[i].Address.AbsoluteAddress - 1))
                    {
                        return false;
                    }
                    previousAddress = registerArray[i].Address.AbsoluteAddress;
                }
                return true;
            }
            else
            {
                throw new ArgumentException("Register list cannot be empty");
            }

        }

        public void ReadFloatRegisters(ushort? unitId, ref List<FloatRegister> floatRegisters, bool? isExtendedUnitId)
        {
            if (!RegisterListIsContiguous(floatRegisters))
            {
                throw new Exception("Register list must be contiguous and contain no duplicates.");
            }

            var request = new ReadFloatsRequest(unitId.HasValue ? unitId.Value : UnitId, floatRegisters, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            var response = Transport.UnicastMessage<ReadFloatsResponse>(request);
            floatRegisters = response.FloatRegisters;
        }

        public void ReadLongRegisters(ushort? unitId, ref List<LongRegister> longRegisters, bool? isExtendedUnitId)
        {
            if (!RegisterListIsContiguous(longRegisters))
            {
                throw new Exception("Register list must be contiguous and contain no duplicates.");
            }

            var request = new ReadLongsRequest(unitId.HasValue ? unitId.Value : UnitId, longRegisters, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            var response = Transport.UnicastMessage<ReadLongsResponse>(request);
            longRegisters = response.LongRegisters;
        }

        public void ReadStringRegister(ushort? unitId, bool? isExtendedUnitId, ref StringRegister stringRegister)
        {
            var request = new ReadStringRequest(unitId.HasValue ? unitId.Value : UnitId, stringRegister, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            var response = Transport.UnicastMessage<ReadStringResponse>(request);
            stringRegister = response.StringRegister;
        }

        public void ReadShortRegisters(ushort? unitId, ref List<ShortRegister> shortRegisters, bool? isExtendedUnitId)
        {
            if (!RegisterListIsContiguous(shortRegisters))
            {
                throw new Exception("Register list must be contiguous and contain no duplicates.");
            }

            var request = new ReadShortsRequest(unitId.HasValue ? unitId.Value : UnitId, shortRegisters, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            var response = Transport.UnicastMessage<ReadShortsResponse>(request);
            shortRegisters = response.ShortRegisters;
        }

        public void ReadHoldingRegisters(ushort? unitId, ref List<HoldingRegister> holdingRegisters, bool? isExtendedUnitId)
        {
            if (!RegisterListIsContiguous(holdingRegisters))
            {
                throw new Exception("Register list must be contiguous and contain no duplicates.");
            }

            var request = new ReadHoldingRegistersRequest(unitId.HasValue ? unitId.Value : UnitId, holdingRegisters, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            var response = Transport.UnicastMessage<ReadHoldingRegistersResponse>(request);
            holdingRegisters = response.HoldingRegisters;
        }

        public void ReadInputRegisters(ushort? unitId, ref List<InputRegister> inputRegisters, bool? isExtendedUnitId)
        {
            if (!RegisterListIsContiguous(inputRegisters))
            {
                throw new Exception("Register list must be contiguous and contain no duplicates.");
            }

            var request = new ReadInputRegistersRequest(unitId.HasValue ? unitId.Value : UnitId, inputRegisters, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            var response = Transport.UnicastMessage<ReadInputRegistersResponse>(request);
            inputRegisters = response.InputRegisters;
        }

        public void ReadCoilRegisters(ushort? unitId, ref List<CoilRegister> coilRegisters, bool? isExtendedUnitId)
        {
            if (!RegisterListIsContiguous(coilRegisters))
            {
                throw new Exception("Register list must be contiguous and contain no duplicates.");
            }

            var request = new ReadCoilRegistersRequest(unitId.HasValue ? unitId.Value : UnitId, coilRegisters, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            var response = Transport.UnicastMessage<ReadCoilRegistersResponse>(request);
            coilRegisters = response.CoilRegisters;
        }

        public void ReadDiscreteInputRegisters(ushort? unitId, ref List<DiscreteInputRegister> discreteInputRegisters, bool? isExtendedUnitId)
        {
            if (!RegisterListIsContiguous(discreteInputRegisters))
            {
                throw new Exception("Register list must be contiguous and contain no duplicates.");
            }

            var request = new ReadDiscreteInputRegistersRequest(unitId.HasValue ? unitId.Value : UnitId, discreteInputRegisters, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            var response = Transport.UnicastMessage<ReadDiscreteInputRegistersResponse>(request);
            discreteInputRegisters = response.DiscreteInputRegisters;
        }

        public void WriteFloatRegisters(ushort? unitId, List<FloatRegister> floatRegisters, bool? isExtendedUnitId)
        {
            if (!RegisterListIsContiguous(floatRegisters))
            {
                throw new Exception("Register list must be contiguous and contain no duplicates.");
            }

            var request = new WriteFloatsRequest(unitId.HasValue ? unitId.Value : UnitId, floatRegisters, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            var response = Transport.UnicastMessage<WriteFloatsResponse>(request);
        }

        public void WriteBooleanRegister(ushort? unitId, BooleanRegister booleanRegister, bool? isExtendedUnitId)
        {
            var request = new WriteBooleanRequest(unitId.HasValue ? unitId.Value : UnitId, booleanRegister, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            Transport.UnicastMessage<WriteBooleanResponse>(request);
        }

        #endregion

        #region Archive Methods

        public HistoryArchiveRecord ReadHistoryArchiveRecord(ushort? unitId, ModbusAddress historyArchiveAddress, ushort index, byte recordSize, bool? isExtendedUnitId)
        {
            var request = new ReadHistoryArchiveRequest(unitId.HasValue ? unitId.Value : UnitId, historyArchiveAddress, index, recordSize, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            var response = Transport.UnicastMessage<ReadHistoryArchiveResponse>(request);
            return response.HistoryArchiveRecord;
        }

        public List<EventArchiveRecord> ReadEventArchiveRecord(ushort? unitId, ModbusAddress eventArchiveAddress, ushort index, bool? isExtendedUnitId)
        {
            var request = new ReadEventArchiveRequest(unitId.HasValue ? unitId.Value : UnitId, eventArchiveAddress, index, isExtendedUnitId.HasValue ? isExtendedUnitId.Value : UseExtendedAddressing);
            var response = Transport.UnicastMessage<ReadEventArchiveResponse>(request);
            return response.EventArchiveRecords;
        }

        #endregion

    }
}
