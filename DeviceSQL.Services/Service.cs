using DeviceSQL.Device;
using DeviceSQL.IO.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DeviceSQL.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IService
    {

        #region Fields

        public static Dictionary<string, IChannel> Channels = new Dictionary<string, IChannel>();
        public static Dictionary<string, IMaster> Devices = new Dictionary<string, IMaster>();

        public List<IChannel> GetChannels()
        {
            throw new NotImplementedException();
        }

        public List<IMaster> GetDevices()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Service Methods


        #endregion

    }
}
