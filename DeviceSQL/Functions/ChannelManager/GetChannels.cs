#region Imported Types

using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System.Collections;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ChannelManager
    {
        [SqlFunction(FillRowMethodName = "GetChannels_FillRow", TableDefinition = "ChannelName nvarchar(512), ChannelType nvarchar(512), ReadTimeout int, WriteTimeout int, ConnectionString nvarchar(512), TracingEnabled bit")]
        public static IEnumerable ChannelManager_GetChannels()
        {
            ArrayList resultCollection = new ArrayList();
            ServiceRegistry.GetChannels().ToList().ForEach(channel => resultCollection.Add(new GetChannels_Result(channel.Name, channel.GetType().Name, channel.ReadTimeout, channel.WriteTimeout, channel.ConnectionString, channel.TracingEnabled)));
            return resultCollection;
        }

        internal class GetChannels_Result
        {
            public SqlString ChannelName;
            public SqlString ChannelType;
            public SqlInt32 ReadTimeout;
            public SqlInt32 WriteTimeout;
            public SqlString ConnectionString;
            public SqlBoolean TracingEnabled;

            public GetChannels_Result(SqlString channelName, SqlString channelType, SqlInt32 readTimeout, SqlInt32 writeTimeout, SqlString connectionString, SqlBoolean tracingEnabled)
            {
                ChannelName = channelName;
                ChannelType = channelType;
                ReadTimeout = readTimeout;
                WriteTimeout = writeTimeout;
                ConnectionString = connectionString;
                TracingEnabled = tracingEnabled;
            }
        }

        public static void GetChannels_FillRow(object getChannels_ResultObj, out SqlString channelName, out SqlString channelType, out SqlInt32 readTimeout, out SqlInt32 writeTimeout, out SqlString connectionString, out SqlBoolean tracingEnabled)
        {
            var getChannels_Result = (getChannels_ResultObj as GetChannels_Result);
            channelName = getChannels_Result.ChannelName;
            channelType = getChannels_Result.ChannelType;
            readTimeout = getChannels_Result.ReadTimeout;
            writeTimeout = getChannels_Result.WriteTimeout;
            connectionString = getChannels_Result.ConnectionString;
            tracingEnabled = getChannels_Result.TracingEnabled;
        }
    }
}
