#region Imported Types

using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;

#endregion

namespace DeviceSQL.IO.Channels
{
    namespace EnerWorx.ChannelService.IO.Channels
    {
        public class SerialPortMuxChannel : SerialPortChannel, IMuxChannel
        {

            #region Fields

            private IChannel sourceChannel = null;
            private int requestDelay = 0;
            private int responseDelay = 0;
            private int responseTimeout = 3000;

            #endregion

            #region Properties

            public IChannel SourceChannel
            {
                get
                {
                    return sourceChannel;
                }
                set
                {
                    if (!(value is IMuxChannel))
                    {
                        sourceChannel = value;
                    }
                    else
                    {
                        throw new Exception("Source channel must not be a MUX channel");
                    }
                }
            }

            public int RequestDelay
            {
                get
                {
                    return requestDelay;
                }
                set
                {
                    requestDelay = value;
                }
            }

            public int ResponseDelay
            {
                get
                {
                    return responseDelay;
                }
                set
                {
                    responseDelay = value;
                }
            }

            public int ResponseTimeout
            {
                get
                {
                    return responseTimeout;
                }
                set
                {
                    responseTimeout = value;
                }
            }

            #endregion

            #region Mux Methods

            public void Run()
            {
                SerialPort.DataReceived -= SerialPort_DataReceived;
                SerialPort.DataReceived += SerialPort_DataReceived;
            }

            public void Stop()
            {
                SerialPort.DataReceived -= SerialPort_DataReceived;
            }

            #endregion

            #region Serial Port Events

            private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
            {
                var startTime = DateTime.Now;
                var lockObject = SourceChannel.LockObject;
                lock (lockObject)
                {
                    var masterStopWatch = new Stopwatch();
                    var requestBytes = new byte[] { };

                    TimedThreadBlocker.Wait(requestDelay);

                    requestBytes = new byte[SerialPort.BytesToRead];

                    if (requestBytes.Length > 0)
                    {
                        Read(ref requestBytes, 0, requestBytes.Length, 0);
                        SourceChannel.Write(ref requestBytes, 0, requestBytes.Length);
                        TimedThreadBlocker.Wait(responseDelay);
                        masterStopWatch.Start();

                        var sequence = 0;
                        while (SourceChannel.NumberOfBytesAvailable > 0 && responseTimeout > masterStopWatch.ElapsedMilliseconds)
                        {
                            var responseBytes = new byte[SourceChannel.NumberOfBytesAvailable];

                            SourceChannel.Read(ref responseBytes, 0, responseBytes.Length, sequence++);
                            Write(ref responseBytes, 0, responseBytes.Length);
                        }

                        masterStopWatch.Stop();

                    }
                }
            }

            #endregion

        }
    }
}

