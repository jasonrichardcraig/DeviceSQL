using DeviceSQL.Device;
using DeviceSQL.IO.Channels;
using DeviceSQL.Service.IOC;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.ServiceProcess;

namespace DeviceSQL.Service
{
    public partial class DeviceSqlService : ServiceBase
    {

        private IDisposable webAppServer = null;

        public DeviceSqlService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            SimpleIOC.Default.Register(() => { return new ConcurrentDictionary<string, IChannel>(); });
            SimpleIOC.Default.Register(() => { return new ConcurrentDictionary<string, IMaster>(); });
            SimpleIOC.Default.Register(() => { return EventLog; });
            webAppServer = WebApp.Start<Startup>(url: Properties.Settings.Default.BaseAddress);
        }

        protected override void OnStop()
        {
            if (webAppServer != null)
            {
                try
                {
                    webAppServer.Dispose();
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry($"Unable to dispose web app server: {ex.Message}", EventLogEntryType.Error);
                }
            }

            if (SimpleIOC.Default.ContainsCreated<ConcurrentDictionary<string, IChannel>>())
            {

                var channelConcurrentDictionary = SimpleIOC.Default.GetInstance<ConcurrentDictionary<string, IChannel>>();

                foreach (var channelKeyValuePair in channelConcurrentDictionary)
                {
                    try
                    {
                        channelKeyValuePair.Value.Dispose();
                    }
                    catch (Exception ex)
                    {
                        EventLog.WriteEntry($"Unable to dispose channel: {ex.Message}", EventLogEntryType.Error);
                    }
                }                
            }

            base.OnStop();

        }
    }
}
