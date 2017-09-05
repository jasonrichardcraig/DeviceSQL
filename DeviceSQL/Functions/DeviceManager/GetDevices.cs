using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using System.Linq;

namespace DeviceSQL.Functions
{
    public partial class DeviceManager
    {

        [SqlFunction(FillRowMethodName = "GetDevices_FillRow", TableDefinition = "DeviceName nvarchar(512), ChannelName nvarchar(512), DeviceType nvarchar(512), Address nvarchar(512), NumberOfRetries int, WaitToRetry int, RequestWriteDelay int, ResponseReadDelay int")]
        public static IEnumerable DeviceManager_GetDevices()
        {
            ArrayList resultCollection = new ArrayList();
            var devices = DeviceSQL.Watchdog.Worker.Devices;
            devices.ToList().ForEach(device => resultCollection.Add(new GetDevices_Result(device.Name, device.Transport.Channel.Name, device.GetType().Name, device.Address, device.Transport.NumberOfRetries, device.Transport.WaitToRetryMilliseconds, device.Transport.RequestWriteDelayMilliseconds, device.Transport.ResponseReadDelayMilliseconds)));
            return resultCollection;
        }

        internal class GetDevices_Result
        {

            #region Fields

            public SqlString ChannelName;
            public SqlString DeviceName;
            public SqlString DeviceType;
            public SqlString Address;
            public SqlInt32 NumberOfRetries;
            public SqlInt32 WaitToRetry;
            public SqlInt32 RequestWriteDelay;
            public SqlInt32 ResponseReadDelay;

            #endregion

            #region Constructor

            public GetDevices_Result(SqlString channelName, SqlString deviceName, SqlString deviceType, SqlString address, SqlInt32 numberOfRetries, SqlInt32 waitToRetry, SqlInt32 requestWriteDelay, SqlInt32 responseReadDelay)
            {
                ChannelName = channelName;
                DeviceName = deviceName;
                DeviceType = deviceType;
                Address = address;
                NumberOfRetries = numberOfRetries;
                WaitToRetry = waitToRetry;
                RequestWriteDelay = requestWriteDelay;
                ResponseReadDelay = responseReadDelay;
            }

            #endregion

        }

        public static void GetDevices_FillRow(object getDevices_ResultObj, out SqlString channelName, out SqlString deviceName, out SqlString deviceType, out SqlString address, out SqlInt32 numberOfRetries, out SqlInt32 waitToRetry, out SqlInt32 requestWriteDelay, out SqlInt32 responseReadDelay)
        {
            GetDevices_Result getDevices_Result = (getDevices_ResultObj as GetDevices_Result);
            channelName = getDevices_Result.ChannelName;
            deviceName = getDevices_Result.DeviceName;
            deviceType = getDevices_Result.DeviceType;
            address = getDevices_Result.Address;
            numberOfRetries = getDevices_Result.NumberOfRetries;
            waitToRetry = getDevices_Result.WaitToRetry;
            requestWriteDelay = getDevices_Result.RequestWriteDelay;
            responseReadDelay = getDevices_Result.ResponseReadDelay;
        }

    }

}
