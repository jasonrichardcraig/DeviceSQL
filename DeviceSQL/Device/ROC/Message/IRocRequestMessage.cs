namespace DeviceSQL.Device.Roc.Message
{
    public interface IRocRequestMessage : IRocMessage
    {
        void ValidateResponse(IRocResponseMessage response);
    }
}
