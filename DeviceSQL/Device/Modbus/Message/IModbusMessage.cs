namespace DeviceSQL.Device.Modbus.Message
{
    public interface IModbusMessage : IMessage
    {
        bool IsExtendedUnitId { get; }
        ushort UnitId { get; set; }
        byte FunctionCode { get; set; }
    }
}
