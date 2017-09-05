#region Imported Types

using DeviceSQL.Device;
using DeviceSQL.IO.Channels;
using DeviceSQL.IOC;
using DeviceSQL.ServiceLocation;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;

#endregion

namespace DeviceSQL.Watchdog
{
    public static class Worker
    {
        #region Fields

        private static volatile bool isRunning;
        private static volatile bool stopRequested;
        private static volatile int watchdogCounter;
        public readonly static BlockingCollection<IChannel> Channels = new BlockingCollection<IChannel>();
        public readonly static BlockingCollection<IMaster> Devices = new BlockingCollection<IMaster>();
        public readonly static SimpleIOC MainContainer = SimpleIOC.Default;

        #endregion

        #region Properties

        public static int WatchdogCounter
        {
            get
            {
                return watchdogCounter;
            }
        }

        public static bool IsAlive
        {
            get
            {
                return isRunning;
            }
        }

        #endregion

        #region Watchdog Methods

        public static void Run()
        {
            if (isRunning)
            {
                return;
            }
            stopRequested = false;
            try
            {
                ServiceLocator.SetLocatorProvider(() => MainContainer);
                MainContainer.Register<BlockingCollection<IChannel>>(() => { return Channels; });
                MainContainer.Register<BlockingCollection<IMaster>>(() => { return Devices; });

                while (!stopRequested)
                {
                    isRunning = true;
                    if (MainContainer != SimpleIOC.Default)
                    {
                        Trace.TraceWarning("SimpleIOC \"Default\" object reference changed");
                    }
                    if (Channels != MainContainer.GetInstance<BlockingCollection<IChannel>>())
                    {
                        Trace.TraceWarning("Channels object reference changed");
                    }
                    if (Devices != MainContainer.GetInstance<BlockingCollection<IMaster>>())
                    {
                        Trace.TraceWarning("Devices object reference changed");
                    }
                    if (watchdogCounter >= (int.MaxValue - 1))
                    {
                        watchdogCounter = 0;
                    }
                    else
                    {
                        watchdogCounter++;
                    }
                    TimedThreadBlocker.Wait(300);
                }
            }
            catch (ThreadAbortException taex)
            {
                Trace.TraceError(string.Format("Watchdog thread aborted: {0}", taex.Message));
            }
            finally
            {
                if (isRunning)
                {
                    isRunning = false;
                    var channelList = Channels.ToList();
                    var deviceList = Devices.ToList();
                    channelList.ToList().ForEach((channel) =>
                    {
                        try
                        {
                            channel.Dispose();
                            Channels.TryTake(out channel);
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError(string.Format("Error removing channel: {0}", ex.Message));
                        }
                    });
                    deviceList.ToList().ForEach((device) =>
                    {
                        try
                        {
                            Devices.TryTake(out device);
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError(string.Format("Error removing device: {0}", ex.Message));
                        }
                    });
                    MainContainer.Unregister<BlockingCollection<IMaster>>();
                    MainContainer.Unregister<BlockingCollection<IChannel>>();
                }
            }

        }

        public static void Stop()
        {
            while (isRunning)
            {
                stopRequested = true;
                TimedThreadBlocker.Wait(100);
            }
            isRunning = false;
        }

        #endregion
    }
}
