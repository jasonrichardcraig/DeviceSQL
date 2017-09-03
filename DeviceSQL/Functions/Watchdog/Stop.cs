using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace DeviceSQL
{
    public partial class Functions
    {
        [SqlFunction]
        public static SqlBoolean Watchdog_Stop()
        {
            try
            {
                var channels = Watchdog.Worker.Channels;
                var channelList = channels.ToList();
                channelList.ForEach((channel) =>
                {
                    try
                    {
                        channel.Dispose();
                        channels.TryTake(out channel);
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(string.Format("Error disposing channel: {0}", ex.Message));
                    }
                });
                Watchdog.Worker.Stop();
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error stopping channel manager: {0}", ex.Message));
            }
            return new SqlBoolean(true);
        }
    }
}
