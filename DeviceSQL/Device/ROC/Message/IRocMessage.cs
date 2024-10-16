namespace DeviceSQL.Device.Roc.Message
{
    public interface IRocMessage : IMessage
    {
        byte SourceUnit { get; set; }
        byte SourceGroup { get; set; }
        byte DestinationUnit { get; set; }
        byte DestinationGroup { get; set; }
        byte OpCode { get; set; }
    }
}
