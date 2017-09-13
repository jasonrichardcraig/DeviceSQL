#region Imported Types

using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ChannelManager
    {
        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlBoolean ChannelManager_UnregisterChannel(SqlString channelName)
        {
            try
            {
                var channelNameValue = channelName.Value;
                var channels = DeviceSQL.Watchdog.Worker.Channels;
                var channelsToRemove = channels.Where(channel => channel.Name == channelNameValue).ToList();
                channelsToRemove.ForEach((channel) =>
                {
                    try
                    {
                        channel.Dispose();
                        channels.TryTake(out channel);
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(string.Format("Error unregistering channel: {0}", ex.Message));
                    }
                });
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error unregistering channel: {0}", ex.Message));
            }
            return new SqlBoolean(true);
        }
    }

}
