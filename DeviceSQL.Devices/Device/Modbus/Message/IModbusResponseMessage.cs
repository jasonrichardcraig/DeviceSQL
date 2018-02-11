namespace DeviceSQL.Device.MODBUS.Message
{
    internal interface IMODBUSResponseMessage : IMODBUSMessage
    {
        void Initialize(byte[] frame, bool isExtendedUnitId);
        void Initialize(byte[] frame, bool isExtendedUnitId, IMODBUSRequestMessage requestMessage);
    }
}
