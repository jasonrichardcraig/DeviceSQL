namespace DeviceSQL.Device.Modbus.Message
{
    public interface IModbusResponseMessage : IModbusMessage
    {
        void Initialize(byte[] frame, bool isExtendedUnitId);
        void Initialize(byte[] frame, bool isExtendedUnitId, IModbusRequestMessage requestMessage);
    }
}
