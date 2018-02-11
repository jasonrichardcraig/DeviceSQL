namespace DeviceSQL.Device.ROC.Message
{
    public interface IROCMessage : IMessage
    {
        byte SourceUnit { get; set; }
        byte SourceGroup { get; set; }
        byte DestinationUnit { get; set; }
        byte DestinationGroup { get; set; }
        byte OpCode { get; set; }
    }
}
