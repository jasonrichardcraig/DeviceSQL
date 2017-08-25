namespace DeviceSQL.IO.Channels.Transport
{
    public interface ITransport
    {
        bool LoggingEnabled { get; set; }
        int WaitToRetryMilliseconds { get; set; }
        int NumberOfRetries { get; set; }
        int ResponseReadDelayMilliseconds { get; set; }
        int RequestWriteDelayMilliseconds { get; set; }
        IChannel Channel { get; set; }
    }
}
