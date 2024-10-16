#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.Roc.Data
{

    #region Enums

    public enum RocPlusAlarmSrbxState
    {
        NoSrbx = 0,
        SrbxIssued = 1
    }

    public enum RocPlusAlarmCondition
    {
        Cleared = 0,
        Set = 1
    }

    public enum RocPlusAlarmType : byte
    {
        NoAlarm = 0,
        ParameterAlarm = 1,
        FstAlarm = 2,
        UserTextAlarm = 3,
        UserValueAlarm = 4
    }

    public enum RocPlusAlarmCode
    {
        LowAlarm = 0,
        LowLowAlarm = 1,
        HighAlarm = 2,
        HighHighAlarm = 3,
        RateAlarm = 4,
        StatusChange = 5,
        PointFail = 6,
        ScanningDisabled = 7,
        ScanningManual = 8,
        RedundantTotalCounts = 9,
        RedundantFlowRegister = 10,
        NoFlowAlarm = 11,
        InputFreezeMode = 12,
        SensorCommunicationFailure = 13,
        RS485CommunicationFailure = 14,
        OffScanMode = 15,
        ManualFlowInputs = 16,
        MeterTemperatureFailureAlarm = 17,
        CompressibilityCalculationAlarm = 18,
        SequenceOutofOrder = 19,
        PhaseDiscrepancy = 20,
        PulseSynchronizationFailure = 21,
        FrequencyDiscrepancy = 22,
        PulseInputOneFailure = 23,
        PulseInputTwoFailure = 24,
        PulseOutputBufferOverrun = 25,
        PulseOutputBufferWarning = 26,
        RelayFault = 27,
        RelayFailure = 28,
        StaticPressureLowLimited = 29,
        TemperatureLowLimited = 30,
        AnalogOutputReadbackError = 31,
        BadLevelAPulseStream = 32,
        MarketPulseAlarm = 33
    }

    #endregion

    public class RocPlusAlarmRecord
    {

        #region Fields

        internal byte[] data;
        internal ushort index;

        #endregion

        #region Properties

        public DateTime? DateTimeStamp
        {
            get
            {
                var timeStamp = BitConverter.ToUInt32(data, 1);

                if (timeStamp > 0)
                {
                    return new DateTime(1970, 1, 1).AddSeconds(timeStamp);
                }
                else
                {
                    return null;
                }
            }
        }

        public ushort Index
        {
            get { return index; }
        }

        public RocPlusAlarmSrbxState AlarmSrbxState
        {
            get
            {
                var alarmSrbxState = (byte)(data[0] & (byte)128) >> 7;
                return (RocPlusAlarmSrbxState)alarmSrbxState;
            }
        }

        public RocPlusAlarmCondition AlarmCondition
        {
            get
            {
                var alarmCondition = (byte)(data[0] & (byte)64) >> 6;
                return (RocPlusAlarmCondition)alarmCondition;
            }
        }

        public RocPlusAlarmType AlarmType
        {
            get
            {
                var alarmType = (byte)(data[0] & (byte)31);
                return (RocPlusAlarmType)alarmType;
            }
        }

        public RocPlusAlarmCode? AlarmCode
        {
            get
            {
                if (AlarmType == RocPlusAlarmType.ParameterAlarm)
                {
                    return (RocPlusAlarmCode)data[5];
                }
                else
                {
                    return null;
                }
            }
        }

        public string AlarmDescription
        {
            get
            {
                switch (AlarmType)
                {
                    case RocPlusAlarmType.ParameterAlarm:
                        return System.Text.ASCIIEncoding.Default.GetString(data, 9, 10).Replace("\0", "").Trim();
                    case RocPlusAlarmType.FstAlarm:
                        return System.Text.ASCIIEncoding.Default.GetString(data, 6, 13).Replace("\0", "").Trim();
                    case RocPlusAlarmType.UserTextAlarm:
                        return System.Text.ASCIIEncoding.Default.GetString(data, 5, 18).Replace("\0", "").Trim();
                    case RocPlusAlarmType.UserValueAlarm:
                        return System.Text.ASCIIEncoding.Default.GetString(data, 5, 14).Replace("\0", "").Trim();
                    default:
                        return null;
                }
            }
        }

        public Tlp Tlp
        {
            get
            {
                if (AlarmType == RocPlusAlarmType.ParameterAlarm)
                {
                    return new Tlp(data[6], data[7], data[8]);
                }
                else
                {
                    return null;
                }
            }
        }

        public float? Value
        {
            get
            {
                switch (AlarmType)
                {
                    case RocPlusAlarmType.ParameterAlarm:
                    case RocPlusAlarmType.FstAlarm:
                    case RocPlusAlarmType.UserValueAlarm:
                        return BitConverter.ToSingle(data, 19);
                    case RocPlusAlarmType.UserTextAlarm:
                    default:
                        return null;
                }
            }
        }

        public float? NullableValue
        {
            get
            {
                if (Value.HasValue)
                {
                    var value = this.Value.Value;

                    if (Single.IsNaN(value) || Single.IsInfinity(value))
                    {
                        return null;
                    }
                    else
                    {
                        return value;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region Constructor(s)

        internal RocPlusAlarmRecord(ushort index, byte[] data)
        {

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            if (data.Length != 23)
            {
                throw new ArgumentOutOfRangeException("data", "Data must be exactly 23 bytes");
            }

            this.data = data;

            this.index = index;

        }

        #endregion

    }

}
