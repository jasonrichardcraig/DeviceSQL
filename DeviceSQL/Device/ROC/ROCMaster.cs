
#region Imported Types

using DeviceSQL.Device.Roc.Data;
using DeviceSQL.Device.Roc.IO;
using DeviceSQL.Device.Roc.Message;
using DeviceSQL.IO.Channels;
using DeviceSQL.IO.Channels.Transport;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc
{
    public class RocMaster : IDevice
    {

        #region Fields

        private RocTransport transport;

        #endregion

        #region Properties

        public RocTransport Transport
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

        ITransport IDevice.Transport
        {
            get
            {
                return transport;
            }
            set
            {
                transport = value as RocTransport;
            }
        }

        public string Name
        {
            get;
            set;
        }

        public byte DeviceAddress
        {
            get; set;

        }

        public byte DeviceGroup
        {
            get;
            set;
        }

        public byte HostAddress
        {
            get;
            set;
        }

        public byte HostGroup
        {
            get;
            set;
        }

        public string Address
        {
            get
            {
                return string.Format("Device Address={0};Device Group={1};Host Address={2};Host Group={3}", DeviceAddress.ToString(), DeviceGroup.ToString(), HostAddress.ToString(), HostGroup.ToString());
            }
        }

        #endregion

        #region Constructor(s)

        public RocMaster(IChannel channel)
        {
            this.transport = new RocTransport(channel);
        }

        #endregion

        #region Security Methods

        public void SetOperatorIdentificationWithoutPassword(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, string operatorId)
        {
            var request = new OpCode017Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), operatorId);
            var response = Transport.UnicastMessage<OpCode017Response>(request);
        }

        public void SetOperatorIdentification(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, string operatorId, UInt16 password)
        {
            var request = new OpCode017Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), operatorId, password);
            var response = Transport.UnicastMessage<OpCode017Response>(request);
        }

        public void SetOperatorIdentificationWithPasswordAndAccessLevel(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, string operatorId, UInt16 password, string accessLevel)
        {
            var request = new OpCode017Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), operatorId, password, accessLevel);
            var response = Transport.UnicastMessage<OpCode017Response>(request);
        }

        #endregion

        #region Parameter Methods

        internal bool ParameterListIsContiguous<T>(List<T> parameters) where T : Parameter
        {
            var parameterCount = parameters.Count;

            if (parameterCount > 0)
            {
                var paramQuery = from p in parameters
                                 orderby p.Tlp.Parameter
                                 select p;

                var parameterArray = paramQuery.ToArray();

                byte FirstPointType = paramQuery.First().Tlp.PointType;
                byte FirstLogicalNumber = paramQuery.First().Tlp.LogicalNumber;
                byte FirstParameter = paramQuery.First().Tlp.Parameter;

                for (int i = 1; i <= parameterCount - 1; i++)
                {
                    if (FirstPointType != parameterArray[i].Tlp.PointType || FirstLogicalNumber != parameterArray[i].Tlp.LogicalNumber || ((parameterArray[i - 1].Tlp.Parameter) + 1) != parameterArray[i].Tlp.Parameter)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                throw new ArgumentException("Parameters list cannot be empty");
            }

        }

        public void ReadParameter<T>(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, ref T parameter) where T : Parameter
        {
            var request = new OpCode167Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), new List<Parameter>(new T[] { parameter }));
            var response = Transport.UnicastMessage<OpCode167Response>(request);
            parameter = (T)response.Parameters.First();
        }

        public void WriteParameter<T>(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, T parameter) where T : Parameter
        {
            var request = new OpCode166Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), new List<Parameter>(new T[] { parameter }));
            var response = Transport.UnicastMessage<OpCode166Response>(request);
        }

        public void ReadParameters(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, ref List<Parameter> parameters)
        {

            bool UseOpcode167 = ParameterListIsContiguous<Parameter>(parameters);

            if (UseOpcode167)
            {
                var request = new OpCode167Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), parameters);
                var response = Transport.UnicastMessage<OpCode167Response>(request);
                parameters = response.Parameters;
            }
            else
            {
                var request = new OpCode180Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), parameters);
                var response = Transport.UnicastMessage<OpCode180Response>(request);
                parameters = response.Parameters;
            }

        }

        public void WriteParameters(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, List<Parameter> parameters)
        {
            bool UseOpcode167 = ParameterListIsContiguous<Parameter>(parameters);

            if (UseOpcode167)
            {
                var request = new OpCode166Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), parameters);
                var response = Transport.UnicastMessage<OpCode166Response>(request);
            }
            else
            {
                var request = new OpCode181Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), parameters);
                var response = Transport.UnicastMessage<OpCode181Response>(request);
            }
        }

        #endregion

        #region DateTime Methods

        public DateTime GetRealTimeClockValue(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, ushort century)
        {
            var request = new OpCode007Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup));
            var response = Transport.UnicastMessage<OpCode007Response>(request);
            return response.GetDateTime(century);
        }

        public DateTime GetRealTimeClockValue(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup)
        {
            var request = new OpCode007Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup));
            var response = Transport.UnicastMessage<OpCode007Response>(request);
            return response.DateTime;
        }

        public void SetRealTimeClock(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, DateTime dateTime)
        {
            var request = new OpCode008Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), dateTime);
            var response = Transport.UnicastMessage<OpCode008Response>(request);
        }

        #endregion

        #region Archive Methods

        public ArchiveInfo GetArchiveInfo(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup)
        {
            var request = new OpCode120Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup));
            var response = Transport.UnicastMessage<OpCode120Response>(request);
            return response.ArchiveInfo;
        }

        public byte ClearAuditLogEventFlags(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte numberOfAuditLogRecordsToClear, ushort startingAuditLogPointer)
        {
            var request = new OpCode132Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), numberOfAuditLogRecordsToClear, startingAuditLogPointer);
            var response = Transport.UnicastMessage<OpCode132Response>(request);
            return response.AuditLogSize;
        }

        public ushort GetCurrentRocPlusAlarmIndex(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup)
        {
            var request = new OpCode118Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), 0, 0);
            var response = Transport.UnicastMessage<OpCode118Response>(request);
            return response.CurrentAlarmLogIndex;
        }

        public ushort GetCurrentRocPlusEventIndex(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup)
        {
            var request = new OpCode119Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), 0, 0);
            var response = Transport.UnicastMessage<OpCode119Response>(request);
            return response.CurrentEventLogIndex;
        }

        public ushort GetCurrentRocPlusHistorySegmentIndex(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte historySegment, byte historyType)
        {
            var request = new OpCode139Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), historySegment, 0, historyType, false, new List<byte>(), 0);
            var response = Transport.UnicastMessage<OpCode139Response>(request);
            return response.CurrentIndex;
        }

        public List<byte> GetRocPlusConfiguredHistoryPoints(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte historySegment)
        {
            var request = new OpCode139Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), historySegment);
            var response = Transport.UnicastMessage<OpCode139Response>(request);
            return response.ConfiguredPoints;
        }

        public List<AlarmRecord> GetAlarms(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte count, ushort startIndex)
        {
            var request = new OpCode121Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), count, startIndex);
            var response = Transport.UnicastMessage<OpCode121Response>(request);
            return response.MeterAlarms;
        }

        public List<EventRecord> GetEvents(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte count, ushort startIndex)
        {
            var request = new OpCode122Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), count, startIndex);
            var response = Transport.UnicastMessage<OpCode122Response>(request);
            return response.MeterEvents;
        }

        public List<AuditLogRecord> GetAuditLogRecords(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte count, ushort startIndex)
        {
            var request = new OpCode131Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), count, startIndex);
            var response = Transport.UnicastMessage<OpCode131Response>(request);
            return response.AuditLogRecords;
        }

        public List<RocPlusAlarmRecord> GetRocPlusAlarms(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte count, ushort startIndex)
        {
            var request = new OpCode118Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), count, startIndex);
            var response = Transport.UnicastMessage<OpCode118Response>(request);
            return response.RocPlusAlarms;
        }

        public List<RocPlusEventRecord> GetRocPlusEvents(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte count, ushort startIndex)
        {
            var request = new OpCode119Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), count, startIndex);
            var response = Transport.UnicastMessage<OpCode119Response>(request);
            return response.RocPlusEvents;
        }

        public List<HistoryRecord> GetHistory(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte historicalRamArea, byte historyPointNumber, byte count, ushort startIndex)
        {
            var request = new OpCode130Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), historicalRamArea, historyPointNumber, count, startIndex);
            var response = Transport.UnicastMessage<OpCode130Response>(request);
            return response.MeterHistory;
        }

        public List<HistoryRecord> GetMinutelyHistory(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte historyPointNumber)
        {
            var request = new OpCode126Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), historyPointNumber);
            var response = Transport.UnicastMessage<OpCode126Response>(request);
            return response.MeterHistory;
        }

        public List<RocPlusHistoryRecord> GetRocPlusHistory(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte historySegment, ushort historyIndex, byte historyType, byte startingHistoryPoint, byte numberOfHistoryPoints, byte numberOfTimePeriods)
        {
            var request = new OpCode136Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), historySegment, historyIndex, historyType, startingHistoryPoint, numberOfHistoryPoints, numberOfTimePeriods);
            var response = Transport.UnicastMessage<OpCode136Response>(request);
            return response.MeterHistory;
        }

        public List<RocPlusHistoryRecord> GetRocPlusHistory(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte historySegment, ushort historyIndex, byte historyType, bool requestTimeStamps, List<byte> requestedHistoryPoints, byte numberOfTimePeriods)
        {
            var request = new OpCode139Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), historySegment, historyIndex, historyType, requestTimeStamps, requestedHistoryPoints, numberOfTimePeriods);
            var response = Transport.UnicastMessage<OpCode139Response>(request);
            return response.MeterHistory;
        }

        #endregion

        #region History Point Configuration Methods

        public List<HistoryPointConfiguration> GetHistoryPointConfigurations(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte historicalRamArea, byte startingDatabaseNumber)
        {
            var request = new OpCode165Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), historicalRamArea, startingDatabaseNumber);
            var response = Transport.UnicastMessage<OpCode165Response>(request);
            return response.HistoryPointConfigurations;
        }

        #endregion

        #region FST Methods

        public FstHeaderInfo GetFstHeaderInfo(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte fstNumber)
        {
            if(fstNumber > 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            var request = new OpCode080Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), fstNumber == 0 ? OpCode080Function.ReadFst0HeaderInformation : OpCode080Function.ReadFst1HeaderInformation, null, null);
            var response = Transport.UnicastMessage<OpCode080Response>(request);
            return new FstHeaderInfo(fstNumber, response.FstVersion, response.FstDescription);
        }

        public FstCodeChunk GetFstCodeChunk(byte? destinationUnit, byte? destinationGroup, byte? sourceUnit, byte? sourceGroup, byte fstNumber, byte offset, byte length)
        {
            if (fstNumber > 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            var request = new OpCode080Request(destinationUnit.GetValueOrDefault(DeviceAddress), destinationGroup.GetValueOrDefault(DeviceGroup), sourceUnit.GetValueOrDefault(HostAddress), sourceGroup.GetValueOrDefault(HostGroup), fstNumber == 0 ? OpCode080Function.ReadFst0Code : OpCode080Function.ReadFst1Code, offset, length);
            var response = Transport.UnicastMessage<OpCode080Response>(request);
            return new FstCodeChunk(offset, length, new List<byte>(response.Data.ToArray()));
        }

        #endregion

    }
}
