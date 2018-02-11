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
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        List<IChannel> GetChannels();

        [OperationContract]
        List<IMaster> GetDevices();
        
    }

}
