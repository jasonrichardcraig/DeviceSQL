namespace DeviceSQL.Device.MODBUS.Message

{
    internal static class MODBUSMessageFactory
    {

        #region Constants

        private const int MinRequestFrameLength = 6;

        #endregion

        #region Factory Methods

        public static IMODBUSResponseMessage CreateMODBUSResponseMessage<TResponseMessage>(byte[] frame, bool isExtendedUnitId)
            where TResponseMessage : IMODBUSResponseMessage, new()
        {
            TResponseMessage responseMessage = new TResponseMessage();
            responseMessage.Initialize(frame, isExtendedUnitId, null);
            return responseMessage;
        }

        public static IMODBUSResponseMessage CreateMODBUSResponseMessage<TResponseMessage>(byte[] frame, bool isExtendedUnitId, IMODBUSRequestMessage requestMessage)
            where TResponseMessage : IMODBUSResponseMessage, new()
        {
            TResponseMessage responseMessage = new TResponseMessage();
            responseMessage.Initialize(frame, isExtendedUnitId, requestMessage);
            return responseMessage;
        }

        #endregion

    }
}
