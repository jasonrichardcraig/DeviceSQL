#region Imported Types

using System;
using System.Diagnostics;

#endregion

namespace DeviceSQL.IO.Channels
{

    internal class ChannelTraceListener : TraceListener
    {

        #region Events

        internal event EventHandler<ChannelTraceEventArgs> ChannelTraceMessageReceived;

        #endregion

        #region Constants

        public const string TRACE_CHANNEL_SUBSYSTEM = "Channel";

        #endregion

        #region Fields

        bool disposed = false;

        #endregion

        internal ChannelTraceListener()
        {
            try
            {
                Trace.Listeners.Add(this);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error initializing channel trace listener: {ex.Message}");
            }
        }

        public override void Write(string message)
        {
            RaiseChannelTraceMessageReceived(message);
        }

        public override void WriteLine(string message)
        {
            RaiseChannelTraceMessageReceived(message);
        }

        public void RaiseChannelTraceMessageReceived(string message)
        {
            try
            {
                var parsedMessage = message.Split(new char[] { ',' });
                if (parsedMessage.Length == 7)
                {
                    var traceSubSystem = parsedMessage[0];
                    if (traceSubSystem == TRACE_CHANNEL_SUBSYSTEM)
                    {
                        ChannelTraceMessageReceived?.Invoke(this, new ChannelTraceEventArgs()
                        {
                            StartTime = DateTime.Now,
                            Name = parsedMessage[0],
                            MessageDateTimeStamp = DateTime.Parse(parsedMessage[1]),
                            Duration = double.Parse(parsedMessage[2]),
                            Operation = parsedMessage[4],
                            Sequence = int.Parse(parsedMessage[5]),
                            Count = int.Parse(parsedMessage[6]),
                            Data = parsedMessage[7],
                            ChannelType = parsedMessage[8]                            
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error raising channel trace message received event: {ex.Message}");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            else if (disposing)
            {
                try
                {
                    Trace.Listeners.Remove(this);
                    base.Dispose(disposing);
                }
                catch (Exception ex)
                {
                    Trace.TraceError($"Error disposing Channel Trace Listener: {ex.Message}");
                }
            }
            disposed = true;
        }

    }
}
