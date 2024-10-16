namespace DeviceSQL.Device.Modbus.Message

{
    internal static class ModbusMessageFactory
    {

        #region Constants

        private const int MinRequestFrameLength = 6;

        #endregion

        #region Factory Methods

        public static IModbusResponseMessage CreateModbusResponseMessage<TResponseMessage>(byte[] frame, bool isExtendedUnitId)
            where TResponseMessage : IModbusResponseMessage, new()
        {
            TResponseMessage responseMessage = new TResponseMessage();
            responseMessage.Initialize(frame, isExtendedUnitId, null);
            return responseMessage;
        }

        public static IModbusResponseMessage CreateModbusResponseMessage<TResponseMessage>(byte[] frame, bool isExtendedUnitId, IModbusRequestMessage requestMessage)
            where TResponseMessage : IModbusResponseMessage, new()
        {
            TResponseMessage responseMessage = new TResponseMessage();
            responseMessage.Initialize(frame, isExtendedUnitId, requestMessage);
            return responseMessage;
        }

        #endregion

    }
}
