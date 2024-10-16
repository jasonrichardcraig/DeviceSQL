namespace DeviceSQL.Device.Roc.Data
{
    public class HistoryPointConfiguration
    {
        public enum HistoryPointArchiveType : byte
        {
            NotDefined = 0,
            FST_DayMonthHourMinute = 64,
            FST_DayHourMinuteSecond = 65,
            FST_DirectWrite = 66,
            ArchivedEveryHour_Average = 128,
            ArchivedEveryHour_Accumulated = 129,
            ArchivedEveryHour_Current = 130,
            ArchivedEveryHour_Totalize = 134
        }

        public byte Index
        {
            get;
            set;
        }

        public byte HistoricalRamArea
        {
            get;
            set;
        }

        public HistoryPointArchiveType ArchiveType
        {
            get;
            set;
        }

        public Tlp Tlp
        {
            get; set;
        }

    }
}
