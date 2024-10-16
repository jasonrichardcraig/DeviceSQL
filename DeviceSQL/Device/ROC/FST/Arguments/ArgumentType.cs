namespace DeviceSQL.Device.Roc.FST.Arguments
{
    public enum ArgumentType : byte
    {
        None = 0,
        DatabasePointOrConstantValue = 1,
        Label = 2,
        FSTPoint = 3,
        AOPoint = 4,
        DOPoint = 5,
        DatabasePoint = 6,
        Text = 7,
        HistorySegmentHistoryPoint = 8,
        HistoryIndex = 9,
        MonthDay = 10,
        TimeElement = 11
    }
}
