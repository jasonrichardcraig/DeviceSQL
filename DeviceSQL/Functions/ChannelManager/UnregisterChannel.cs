using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace DeviceSQL.ChannelManager
{
    partial class Functions
    {
        public static SqlBoolean UnregisterChannel(SqlString channelName)
        {
            try
            {
                var channelNameValue = channelName.Value;
                var channels = Watchdog.Worker.Channels;
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
