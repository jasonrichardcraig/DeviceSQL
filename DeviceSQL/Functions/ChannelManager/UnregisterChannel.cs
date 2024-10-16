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
        public static SqlBoolean ChannelManager_UnregisterChannel(SqlString channelName)
        {
            try
            {
                try
                {
                    var channel = ServiceRegistry.GetChannel(channelName.Value);
                    channel.Dispose();
                    ServiceRegistry.RemoveChannel(channelName.Value);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(string.Format("Error unregistering channel: {0}", ex.Message));
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error unregistering channel: {0}", ex.Message));
            }
            return new SqlBoolean(true);
        }
    }

}
