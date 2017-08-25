namespace DeviceSQL.Device.Modbus.Message
{
    internal interface IModbusRequestMessage : IModbusMessage
    {
        Device.DataType DataType { get; }
        void ValidateResponse(IModbusResponseMessage response);
    }
}
