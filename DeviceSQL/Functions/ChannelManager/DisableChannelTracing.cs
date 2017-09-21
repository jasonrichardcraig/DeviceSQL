#region Imported Types

using DeviceSQL.IO.Channels;
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
        public static SqlBoolean ChannelManager_DisableChannelLogging (SqlString channelName)
        {
            try
            {
                var channelNameValue = channelName.Value;
                var channels = DeviceSQL.Watchdog.Worker.Channels;
                var channel = channels.FirstOrDefault(c => c.Name == channelName.Value);

                if (channel != null)
                {

                    channel.TracingEnabled = false;

                    return new SqlBoolean(true);

                }
                else
                {
                    throw new ArgumentException("Channel does not exist");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error disabling channel tracing: {0}", ex.Message));
            }
            return new SqlBoolean(false);
        }
    }

}
