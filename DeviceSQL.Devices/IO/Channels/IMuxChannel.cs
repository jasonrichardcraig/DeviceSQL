namespace DeviceSQL.IO.Channels
{
    public interface IMuxChannel : IChannel
    {
        string SourceChannelName { get; set; }
        IChannel SourceChannel { get; }
        int RequestDelay { get; set; }
        int ResponseDelay { get; set; }
        int ResponseTimeout { get; set; }

    }
}
