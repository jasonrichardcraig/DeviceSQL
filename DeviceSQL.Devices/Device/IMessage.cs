namespace DeviceSQL.Device
{
    public interface IMessage
    {
        byte[] MessageFrame { get; }
        byte[] ProtocolDataUnit { get; }
    }
}
