#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class Watchdog
    {
        [SqlFunction]
        public static SqlBoolean Watchdog_Stop()
        {
            try
            {
                var channels = DeviceSQL.Watchdog.Worker.Channels;
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
                DeviceSQL.Watchdog.Worker.Stop();
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error stopping channel manager: {0}", ex.Message));
            }
            return new SqlBoolean(true);
        }
    }
}
