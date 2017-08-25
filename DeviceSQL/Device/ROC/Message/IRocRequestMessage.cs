namespace DeviceSQL.Device.ROC.Message
{
    public interface IROCRequestMessage : IROCMessage
    {
        void ValidateResponse(IROCResponseMessage response);
    }
}
