namespace DeviceSQL.Device.Roc.Message
{
    public interface IRocResponseMessage : IRocMessage
    {
        void Initialize(byte[] frame);
        void Initialize(byte[] frame, IRocRequestMessage requestMessage);
    }
}
