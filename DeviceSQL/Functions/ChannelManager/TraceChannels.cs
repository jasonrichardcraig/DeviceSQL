#region Imported Types

using DeviceSQL.IO.Channels;
using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ChannelManager
    {
        [SqlFunction(FillRowMethodName = "TraceChannels_FillRow", TableDefinition = "[MessageDateTimeStamp] datetime, [ChannelType] nvarchar(64), [ChannelName] nvarchar(1024), [Operation] nvarchar(32), [Sequence] int, [StartTime] datetime, [Duration] float, [Count] int, [Data] nvarchar(640)")]
        public static IEnumerable ChannelManager_TraceChannels()
        {
            var queueLockObject = new object();
            using (var channelTraceListener = new ChannelTraceListener())
            {
                var channelTraceListenerMessageQueue = new Queue<TraceChannels_Result>();
                channelTraceListener.ChannelTraceMessageReceived += (object sender, ChannelTraceEventArgs e) =>
                {
                    lock (queueLockObject)
                    {
                        channelTraceListenerMessageQueue.Enqueue(new TraceChannels_Result(e.MessageDateTimeStamp, e.ChannelType, e.Name, e.Operation, e.Sequence, e.StartTime, e.Duration, e.Count, e.Data));
                    }
                };

                while (channelTraceListener != null)
                {
                    lock (queueLockObject)
                    {
                        while (channelTraceListenerMessageQueue.Count > 0)
                        {
                            yield return channelTraceListenerMessageQueue.Dequeue();
                        }
                    }
                    TimedThreadBlocker.Wait(125);
                }

            }
        }

        internal class TraceChannels_Result
        {

            internal SqlDateTime MessageDateTimeStamp { get; set; }
            internal SqlString Name { get; set; }
            internal SqlDateTime StartTime { get; set; }
            internal SqlString Operation { get; set; }
            internal SqlDouble Duration { get; set; }
            internal SqlInt32 Sequence { get; set; }
            internal SqlInt32 Count { get; set; }
            internal SqlString Data { get; set; }
            internal SqlString ChannelType { get; set; }

            public TraceChannels_Result(SqlDateTime messageDateTimeStamp, SqlString channelType, SqlString name, SqlString operation, SqlInt32 sequence, SqlDateTime startTime, SqlDouble duration, SqlInt32 count, SqlString data)
            {
                MessageDateTimeStamp = messageDateTimeStamp;
                Name = name;
                StartTime = startTime;
                Operation = operation;
                Duration = duration;
                Sequence = sequence;
                Count = count;
                Data = data;
                ChannelType = channelType;
            }
        }

        public static void TraceChannels_FillRow(object TraceChannels_ResultObj, out SqlDateTime messageDateTimeStamp, out SqlString channelType, out SqlString name, out SqlString operation, out SqlInt32 sequence, out SqlDateTime startTime, out SqlDouble duration, out SqlInt32 count, out SqlString data)
        {
            var TraceChannels_Result = (TraceChannels_ResultObj as TraceChannels_Result);

            messageDateTimeStamp = TraceChannels_Result.MessageDateTimeStamp;
            name = TraceChannels_Result.Name;
            startTime = TraceChannels_Result.StartTime;
            operation = TraceChannels_Result.Operation;
            duration = TraceChannels_Result.Duration;
            sequence = TraceChannels_Result.Sequence;
            count = TraceChannels_Result.Count;
            data = TraceChannels_Result.Data;
            channelType = TraceChannels_Result.ChannelType;

        }
    }
}
