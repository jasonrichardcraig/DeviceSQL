#region Imported Types

using System;
using System.Diagnostics;

#endregion

namespace DeviceSQL.IO.Channels
{

    public class ChannelTraceListener : TraceListener
    {

        #region Events

        public event EventHandler<ChannelTraceEventArgs> ChannelTraceMessageReceived;

        #endregion

        #region Constants

        public const string TRACE_CATEGORY = "Channel";

        #endregion

        #region Fields

        bool disposed = false;

        #endregion

        public ChannelTraceListener()
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
                if (message.StartsWith(TRACE_CATEGORY + ","))
                {
                    var parsedMessage = message.Split(new char[] { ',' });
                    if (parsedMessage.Length == 9)
                    {

                        ChannelTraceMessageReceived?.Invoke(this, new ChannelTraceEventArgs()
                        {
                            StartTime = DateTime.Now,
                            Name = parsedMessage[1],
                            MessageDateTimeStamp = DateTime.Parse(parsedMessage[2]),
                            Duration = double.Parse(parsedMessage[3]),
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
