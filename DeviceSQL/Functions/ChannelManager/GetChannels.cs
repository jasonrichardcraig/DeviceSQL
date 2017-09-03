using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using System.Linq;

namespace DeviceSQL.ChannelManager
{
    public partial class Functions
    {
        [SqlFunction(FillRowMethodName = "GetChannels_FillRow", TableDefinition = "ChannelName nvarchar(512), ChannelType nvarchar(512), ReadTimeout int, WriteTimeout int, ConnectionString nvarchar(512)")]
        public static IEnumerable GetChannels()
        {
            ArrayList resultCollection = new ArrayList();
            var channels = Watchdog.Worker.Channels;
            channels.ToList().ForEach(channel => resultCollection.Add(new GetChannels_Result(channel.Name, channel.GetType().Name, channel.ReadTimeout, channel.WriteTimeout, channel.ConnectionString)));
            return resultCollection;
        }

        internal class GetChannels_Result
        {
            public SqlString ChannelName;
            public SqlString ChannelType;
            public SqlInt32 ReadTimeout;
            public SqlInt32 WriteTimeout;
            public SqlString ConnectionString;

            public GetChannels_Result(SqlString channelName, SqlString channelType, SqlInt32 readTimeout, SqlInt32 writeTimeout, SqlString connectionString)
            {
                ChannelName = channelName;
                ChannelType = channelType;
                ReadTimeout = readTimeout;
                WriteTimeout = writeTimeout;
                ConnectionString = connectionString;
            }
        }

        public static void GetChannels_FillRow(object getChannels_ResultObj, out SqlString channelName, out SqlString channelType, out SqlInt32 readTimeout, out SqlInt32 writeTimeout, out SqlString connectionString)
        {
            var getChannels_Result = (getChannels_ResultObj as GetChannels_Result);
            channelName = getChannels_Result.ChannelName;
            channelType = getChannels_Result.ChannelType;
            readTimeout = getChannels_Result.ReadTimeout;
            writeTimeout = getChannels_Result.WriteTimeout;
            connectionString = getChannels_Result.ConnectionString;
        }


    }

}
