namespace DeviceSQL.Device.ROC.Message
{
    public interface IROCResponseMessage : IROCMessage
    {
        void Initialize(byte[] frame);
        void Initialize(byte[] frame, IROCRequestMessage requestMessage);
    }
}
