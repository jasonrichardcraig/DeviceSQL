#region Imported Types

using DeviceSQL.Registries;
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
        public static SqlBoolean ChannelManager_EnableChannelLogging(SqlString channelName)
        {
            try
            {
                var channel = ServiceRegistry.GetChannel(channelName.Value);

                if (channel != null)
                {

                    channel.TracingEnabled = true;

                    return new SqlBoolean(true);

                }
                else
                {
                    throw new ArgumentException("Channel does not exist");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error enabling channel tracing: {0}", ex.Message));
            }
            return new SqlBoolean(false);
        }
    }

}
