namespace DeviceSQL.Device.Modbus.Message
{
    internal interface IModbusResponseMessage : IModbusMessage
    {
        void Initialize(byte[] frame, bool isExtendedUnitId);
        void Initialize(byte[] frame, bool isExtendedUnitId, IModbusRequestMessage requestMessage);
    }
}
