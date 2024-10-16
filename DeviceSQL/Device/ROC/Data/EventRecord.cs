#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.Roc.Data
{

    #region Enums

    public enum EventCode : byte
    {
        UploadToDisk = 49,
        DownloadToRoc = 50,
        Calibration = 79,
        InitializationSequence = 144,
        AllPowerRemoved = 145,
        InitializeFromDefaults = 146,
        ROMCRCError = 147,
        DatabaseIntialized = 148,
        Diagnostic = 149,
        ProgramFlash = 150,
        ClockSet = 200,
        Fst = 240,
        Informational = 248,
        ParameterChanged,
        Unknown
    }

    public enum CalibrationPointType : byte
    {
        MVS = 40,
        AI = 3,
        Unknown
    }

    public enum CalibrationType : byte
    {
        SetZero = 0,
        SetSpan = 1,
        SetMidPoint1 = 2,
        SetMidPoint2 = 3,
        SetMidPoint3 = 4,
        CalVerified = 5,
        SetZeroShift = 10,
        SetStaticPresOffset,
        SetRtdBias,
        CalCancelled = 29,
        Unknown
    }

    public enum CalibrationMultivariableSensorInput : byte
    {
        DP = 1,
        StaticPres = 2,
        Temp = 3,
        LowDp = 4,
        AI = 0
    }

    #endregion

    public class EventRecord
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
                var dateTimeBytes = new byte[] { data[2], data[3], data[4], data[5], data[6], data[7] };
                return ParseEventDateTime(dateTimeBytes);
            }
        }

        public ushort Index
        {
            get { return index; }
        }

        public byte? FstNumber
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.Fst:
                        return data[1];
                    default:
                        return null;
                }
            }
        }

        public Byte? PointType
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.ParameterChanged:
                        return data[0];
                    default:
                        return null;
                }
            }
        }

        public Byte? LogicalNumber
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.Informational:
                    case Data.EventCode.ParameterChanged:
                    case Data.EventCode.Calibration:
                        return data[8];
                    default:
                        return null;
                }
            }
        }

        public Byte? ParameterNumber
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.ParameterChanged:
                        return data[1];
                    default:
                        return null;
                }
            }
        }

        public ushort? Tag
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.ParameterChanged:
                        return BitConverter.ToUInt16(data, 20);
                    default:
                        return null;
                }
            }
        }

        public DateTime? PowerRemovedDateTime
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.AllPowerRemoved:
                        var dateTimeBytes = new byte[] { data[8], data[9], data[10], data[11], data[12], data[13] };
                        return ParseEventDateTime(dateTimeBytes);
                    default:
                        return null;
                }
            }
        }

        public CalibrationPointType? CalibrationPointType
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.Calibration:
                        switch (data[20])
                        {
                            case 3:
                                return Data.CalibrationPointType.AI;
                            case 40:
                                return Data.CalibrationPointType.MVS;
                            default:
                                return Data.CalibrationPointType.Unknown;
                        }
                    default:
                        return null;
                }
            }

        }

        public CalibrationMultivariableSensorInput? CalibrationMultivariableSensorInput
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.Calibration:
                        switch (data[21])
                        {
                            case 1:
                                return Data.CalibrationMultivariableSensorInput.DP;
                            case 2:
                                return Data.CalibrationMultivariableSensorInput.StaticPres;
                            case 3:
                                return Data.CalibrationMultivariableSensorInput.Temp;
                            case 4:
                                return Data.CalibrationMultivariableSensorInput.LowDp;
                            default:
                                return Data.CalibrationMultivariableSensorInput.AI;
                        }
                    default:
                        return null;
                }
            }
        }

        public CalibrationType? CalibrationType
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.Calibration:
                        switch (data[1])
                        {
                            case 0:
                                return Data.CalibrationType.SetZero;
                            case 1:
                                return Data.CalibrationType.SetSpan;
                            case 2:
                                return Data.CalibrationType.SetMidPoint1;
                            case 3:
                                return Data.CalibrationType.SetMidPoint2;
                            case 4:
                                return Data.CalibrationType.SetMidPoint3;
                            case 5:
                                return Data.CalibrationType.CalVerified;
                            case 10:
                                switch (CalibrationMultivariableSensorInput.GetValueOrDefault(Data.CalibrationMultivariableSensorInput.AI))
                                {
                                    case Data.CalibrationMultivariableSensorInput.DP:
                                    case Data.CalibrationMultivariableSensorInput.LowDp:
                                        return Data.CalibrationType.SetZeroShift;
                                    case Data.CalibrationMultivariableSensorInput.StaticPres:
                                        return Data.CalibrationType.SetStaticPresOffset;
                                    case Data.CalibrationMultivariableSensorInput.Temp:
                                        return Data.CalibrationType.SetRtdBias;
                                    default:
                                        return Data.CalibrationType.Unknown;
                                }
                            case 29:
                                return Data.CalibrationType.CalCancelled;
                            default:
                                return Data.CalibrationType.Unknown;
                        }
                    default:
                        return null;
                }
            }
        }

        public EventCode EventCode
        {
            get
            {
                return ParseEventCode(data[0]);
            }
        }

        public string OperatorId
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.ClockSet:
                    case Data.EventCode.DownloadToRoc:
                    case Data.EventCode.Informational:
                    case Data.EventCode.UploadToDisk:
                    case Data.EventCode.Diagnostic:
                    case Data.EventCode.ParameterChanged:
                    case Data.EventCode.Calibration:
                        return System.Text.ASCIIEncoding.Default.GetString(data, 9, 3).Replace("\0", "").Trim();
                    default:
                        return null;
                }
            }
        }

        public string EventText
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.ClockSet:
                    case Data.EventCode.DownloadToRoc:
                    case Data.EventCode.Informational:
                    case Data.EventCode.UploadToDisk:
                        return System.Text.ASCIIEncoding.Default.GetString(data, 12, 10).Replace("\0", "").Trim();
                    case Data.EventCode.Fst:
                        return System.Text.ASCIIEncoding.Default.GetString(data, 8, 10).Replace("\0", "").Trim();
                    default:
                        return null;
                }
            }
        }

        public byte[] OldValue
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.Calibration:
                    case Data.EventCode.Diagnostic:
                    case Data.EventCode.ParameterChanged:
                        return new byte[] { data[12], data[13], data[14], data[15] };
                    default:
                        return null;
                }
            }
        }

        public Single? FstFloatValue
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.Fst:
                        return BitConverter.ToSingle(data, 18);
                    default:
                        return null;
                }
            }
        }

        public byte[] NewValue
        {
            get
            {
                switch (EventCode)
                {
                    case Data.EventCode.Calibration:
                    case Data.EventCode.Diagnostic:
                    case Data.EventCode.ParameterChanged:
                        return new byte[] { data[16], data[17], data[18], data[19] };
                    default:
                        return null;
                }
            }
        }

        #endregion

        #region Constructor(s)

        internal EventRecord(ushort index, byte[] data)
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

        public byte[] GetBytes()
        {
            var bytes = new byte[data.Length];
            Buffer.BlockCopy(data, 0, bytes, 0, data.Length);
            return bytes;
        }

        public static float? ConvertToNullableFloat(byte[] value)
        {
            if (value.Length < 4)
            {
                return null;
            }
            else
            {
                var floatValue = BitConverter.ToSingle(value, 0);

                if (Single.IsNaN(floatValue) || Single.IsInfinity(floatValue))
                {
                    return null;
                }
                else
                {
                    return floatValue;
                }
            }
        }

        public static double? ConvertToNullableDouble(byte[] value)
        {
            if (value.Length < 8)
            {
                return null;
            }
            else
            {
                var doubleValue = BitConverter.ToDouble(value, 0);

                if (Double.IsNaN(doubleValue) || Double.IsInfinity(doubleValue))
                {
                    return null;
                }
                else
                {
                    return doubleValue;
                }
            }
        }

        internal static EventCode ParseEventCode(byte pointType)
        {
            EventCode eventCode;

            switch (pointType)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                    eventCode = Data.EventCode.ParameterChanged;
                    break;
                case 49:
                    eventCode = Data.EventCode.UploadToDisk;
                    break;
                case 50:
                    eventCode = Data.EventCode.Informational;
                    break;
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                    eventCode = Data.EventCode.ParameterChanged;
                    break;
                case 79:
                    eventCode = Data.EventCode.Calibration;
                    break;
                case 81:
                case 83:
                case 84:
                case 86:
                    eventCode = Data.EventCode.ParameterChanged;
                    break;
                case 144:
                    eventCode = Data.EventCode.InitializationSequence;
                    break;
                case 145:
                    eventCode = Data.EventCode.AllPowerRemoved;
                    break;
                case 146:
                    eventCode = Data.EventCode.InitializeFromDefaults;
                    break;
                case 147:
                    eventCode = Data.EventCode.ROMCRCError;
                    break;
                case 148:
                    eventCode = Data.EventCode.DatabaseIntialized;
                    break;
                case 149:
                    eventCode = Data.EventCode.Diagnostic;
                    break;
                case 150:
                    eventCode = Data.EventCode.ProgramFlash;
                    break;
                case 200:
                    eventCode = Data.EventCode.ClockSet;
                    break;
                case 240:
                    eventCode = Data.EventCode.Fst;
                    break;
                case 248:
                    eventCode = Data.EventCode.Informational;
                    break;
                default:
                    eventCode = Data.EventCode.Unknown;
                    break;
            }

            return eventCode;
        }

        internal static DateTime? ParseEventDateTime(byte[] value)
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
                Debug.WriteLine("Unable to Parse Event DateTime.");
            }

            return null;
        }

        #endregion

    }

}
