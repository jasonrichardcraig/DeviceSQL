namespace DeviceSQL.Device.Modbus.Message
{
    public interface IModbusRequestMessage : IModbusMessage
    {
        Device.DataType DataType { get; }
        void ValidateResponse(IModbusResponseMessage response);
    }
}
