namespace DeviceSQL.Device.MODBUS.Message
{
    internal interface IMODBUSRequestMessage : IMODBUSMessage
    {
        Device.DataType DataType { get; }
        void ValidateResponse(IMODBUSResponseMessage response);
    }
}
