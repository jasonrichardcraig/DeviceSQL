#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#endregion

#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.Data
#else
namespace DeviceSQL.Device.ROC.Data
#endif
{

    #region Enums

    public enum AlarmClass : byte
    {
        SensorDP = 1,
        SensorAP = 2,
        SensorPT = 3,
        IOpoint = 5,
        AGA = 6,
        UserText = 7,
        UserValue = 8,
        MVSSensor = 9,
        SensorModule = 10,
        FST = 15,
        Unknown
    }

    public enum AlarmState : byte
    {
        Clear = 0,
        Set = 1,
        ClearPulseInput = 2,
        SetPulseInput = 3,
        ClearSRBX = 4,
        SetSRBX = 5,
        Unknown
    }

    public enum AlarmCode
    {
        LowAlarm,
        LoLoAlarm,
        HighAlarm,
        HiHiAlarm,
        RateAlarm,
        StatusChange,
        ADFailure,
        ManualMode,
        RedundantTotalCount,
        RedundantFlowAlarm,
        NoFlowAlarm,
        LogicAlarm,
        InputFreezeMode,
        EIA485FailAlarm,
        SensorCommunicationsFailAlarm,
        OffScanMode,
        SequenceOutofOrderAlarm,
        PhaseDiscrepancyDetectedAlarm,
        InconsistentPulseCountAlarm,
        FrequencyDiscrepancyAlarm,
        ChannelAFailureAlarm,
        ChannelBFailureAlarm,
        Unknown
    }

    #endregion

    public class AlarmRecord
    {

        #region Fields

        public byte[] data;
        public ushort index;

        #endregion

        #region Properties

        public DateTime? DateTimeStamp
        {
            get
            {
                var dateTimeBytes = new byte[] { data[2], data[3], data[4], data[5], data[6], data[7] };
                return ParseAlarmDateTime(dateTimeBytes);
            }
        }

        public ushort Index
        {
            get { return index; }
        }

        public AlarmCode AlarmCode
        {
            get
            {
                return ParseAlarmCode(data[0], data[1]);
            }
        }

        public AlarmClass AlarmClass
        {
            get
            {
                return ParseAlarmClass(data[0]);
            }
        }

        public AlarmState AlarmState
        {
            get
            {
                return ParseAlarmState(data[0]);
            }
        }

        public string Tag
        {
            get
            {
                return System.Text.ASCIIEncoding.Default.GetString(data, 8, 10).Replace("\0", "").Trim();
            }
        }

        public float Value
        {
            get
            {
                return BitConverter.ToSingle(data, 18); ;
            }
        }

        public float? NullableValue
        {
            get
            {
                var value = this.Value;

                if (Single.IsNaN(value) || Single.IsInfinity(value))
                {
                    return null;
                }
                else
                {
                    return value;
                }
            }
        }

        #endregion

        #region Constructor(s)

        internal AlarmRecord(ushort index, byte[] data)
        {

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            if (data.Length != 22)
            {
                throw new ArgumentOutOfRangeException("data", "Data must be exactly 22 bytes");
            }

            this.data = data;

            this.index = index;

        }

        #endregion

        #region Helper Methods

        internal static AlarmClass ParseAlarmClass(byte alarmType)
        {
            AlarmClass alarmClass;

            int highNibble = ((128 & alarmType) >> 4) | ((64 & alarmType) >> 4) | ((32 & alarmType) >> 4) | ((16 & alarmType) >> 4);

            switch (highNibble)
            {
                case 1:
                    alarmClass = AlarmClass.SensorDP;
                    break;
                case 2:
                    alarmClass = AlarmClass.SensorAP;
                    break;
                case 3:
                    alarmClass = AlarmClass.SensorPT;
                    break;
                case 5:
                    alarmClass = AlarmClass.IOpoint;
                    break;
                case 6:
                    alarmClass = AlarmClass.AGA;
                    break;
                case 7:
                    alarmClass = AlarmClass.UserText; ;
                    break;
                case 8:
                    alarmClass = AlarmClass.UserValue;
                    break;
                case 9:
                    alarmClass = AlarmClass.MVSSensor;
                    break;
                case 10:
                    alarmClass = AlarmClass.SensorModule;
                    break;
                case 15:
                    alarmClass = AlarmClass.FST;
                    break;
                default:
                    alarmClass = AlarmClass.Unknown;
                    break;
            }

            return alarmClass;

        }

        internal static AlarmState ParseAlarmState(byte alarmType)
        {
            AlarmState alarmState;

            int highNibble = ((8 & alarmType)) | ((4 & alarmType)) | ((2 & alarmType)) | ((1 & alarmType));

            switch (highNibble)
            {
                case 0:
                    alarmState = AlarmState.Clear;
                    break;
                case 1:
                    alarmState = AlarmState.Set;
                    break;
                case 2:
                    alarmState = AlarmState.ClearPulseInput; ;
                    break;
                case 3:
                    alarmState = AlarmState.SetPulseInput;
                    break;
                case 4:
                    alarmState = AlarmState.ClearSRBX;
                    break;
                case 5:
                    alarmState = AlarmState.SetSRBX;
                    break;
                default:
                    alarmState = AlarmState.Unknown;
                    break;
            }

            return alarmState;

        }

        internal static AlarmCode ParseAlarmCode(byte alarmType, byte alarmSubType)
        {
            AlarmCode alarmCode;

            int highNibble = ((128 & alarmType) >> 4) | ((64 & alarmType) >> 4) | ((32 & alarmType) >> 4) | ((16 & alarmType) >> 4);

            switch (highNibble)
            {
                case 1:
                case 2:
                case 3:
                case 5:
                    switch (alarmSubType)
                    {
                        case 0:
                            alarmCode = AlarmCode.LowAlarm;
                            break;
                        case 1:
                            alarmCode = AlarmCode.LoLoAlarm;
                            break;
                        case 2:
                            alarmCode = AlarmCode.HighAlarm;
                            break;
                        case 3:
                            alarmCode = AlarmCode.HiHiAlarm;
                            break;
                        case 4:
                            alarmCode = AlarmCode.RateAlarm;
                            break;
                        case 5:
                            alarmCode = AlarmCode.StatusChange;
                            break;
                        case 6:
                            alarmCode = AlarmCode.ADFailure;
                            break;
                        case 7:
                            alarmCode = AlarmCode.ManualMode;
                            break;
                        default:
                            alarmCode = AlarmCode.Unknown;
                            break;
                    }
                    break;
                case 6:
                    switch (alarmSubType)
                    {
                        case 0:
                            alarmCode = AlarmCode.LowAlarm;
                            break;
                        case 2:
                            alarmCode = AlarmCode.HighAlarm;
                            break;
                        case 4:
                            alarmCode = AlarmCode.RedundantTotalCount;
                            break;
                        case 5:
                            alarmCode = AlarmCode.RedundantFlowAlarm;
                            break;
                        case 6:
                            alarmCode = AlarmCode.NoFlowAlarm;
                            break;
                        case 7:
                            alarmCode = AlarmCode.ManualMode;
                            break;
                        default:
                            alarmCode = AlarmCode.Unknown;
                            break;
                    }
                    break;
                case 8:
                    switch (alarmSubType)
                    {
                        case 0:
                            alarmCode = AlarmCode.LogicAlarm;
                            break;
                        default:
                            alarmCode = AlarmCode.Unknown;
                            break;
                    }
                    break;
                case 9:
                    switch (alarmSubType)
                    {
                        case 4:
                            alarmCode = AlarmCode.InputFreezeMode;
                            break;
                        case 5:
                            alarmCode = AlarmCode.EIA485FailAlarm;
                            break;
                        case 6:
                            alarmCode = AlarmCode.SensorCommunicationsFailAlarm;
                            break;
                        case 7:
                            alarmCode = AlarmCode.OffScanMode;
                            break;
                        default:
                            alarmCode = AlarmCode.Unknown;
                            break;
                    }
                    break;
                case 10:
                    switch (alarmSubType)
                    {
                        case 0:
                            alarmCode = AlarmCode.SequenceOutofOrderAlarm;
                            break;
                        case 1:
                            alarmCode = AlarmCode.PhaseDiscrepancyDetectedAlarm;
                            break;
                        case 2:
                            alarmCode = AlarmCode.InconsistentPulseCountAlarm;
                            break;
                        case 3:
                            alarmCode = AlarmCode.FrequencyDiscrepancyAlarm;
                            break;
                        case 4:
                            alarmCode = AlarmCode.ChannelAFailureAlarm;
                            break;
                        case 5:
                            alarmCode = AlarmCode.ChannelBFailureAlarm;
                            break;
                        default:
                            alarmCode = AlarmCode.Unknown;
                            break;
                    }
                    break;
                default:
                    alarmCode = AlarmCode.Unknown;
                    break;
            }

            return alarmCode;

        }

        internal static DateTime? ParseAlarmDateTime(byte[] value)
        {

            if (value[4] == 0)
            {
                return null;
            }

            int year = value[5] + 2000;

            try
            {
                return new DateTime(year, value[4], value[3], value[2], value[1], value[0]); ;
            }
            catch
            {
                Debug.WriteLine("Unable to Parse Alarm DateTime.");
            }

            return null;
        }

        internal static byte[] ParseDateTimeBytes(DateTime value)
        {
            return new byte[] { Convert.ToByte(value.Second), Convert.ToByte(value.Minute), Convert.ToByte(value.Hour), Convert.ToByte(value.Day), Convert.ToByte(value.Month), Convert.ToByte(value.Year - 2000) };
        }

        #endregion

    }

}
