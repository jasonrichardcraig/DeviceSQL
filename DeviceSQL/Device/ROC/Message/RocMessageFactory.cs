﻿// DeviceSQL Device Roc Libraries
// Copyright 2015, DeviceSQL
// Date: 2017-08-07
// Author: Jason R. Craig
// Phone: +1-403-618-6945
// Email: jason.craig@outlook.com

#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal static class RocMessageFactory
    {

        #region Constants

        private const int MinRequestFrameLength = 6;

        #endregion

        #region Factory Methods

        public static IRocResponseMessage CreateRocResponseMessage<TResponseMessage>(byte[] frame)
            where TResponseMessage : IRocResponseMessage, new()
        {
            TResponseMessage responseMessage = new TResponseMessage();
            responseMessage.Initialize(frame, null);
            return responseMessage;
        }

        public static IRocResponseMessage CreateRocResponseMessage<TResponseMessage>(byte[] frame, IRocRequestMessage requestMessage)
            where TResponseMessage : IRocResponseMessage, new()
        {
            TResponseMessage responseMessage = new TResponseMessage();
            responseMessage.Initialize(frame, requestMessage);
            return responseMessage;
        }

        #endregion

    }
}
