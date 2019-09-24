namespace DeviceSQL.IO.Channels
{
    public interface IMuxChannel : IChannel
    {
        IChannel SourceChannel { get; set; }
        int RequestDelay { get; set; }
        int ResponseDelay { get; set; }
        int ResponseTimeout { get; set; }

    }
}
