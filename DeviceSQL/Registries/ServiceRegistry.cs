using DeviceSQL.Device;
using DeviceSQL.IO.Channels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DeviceSQL.Registries
{
    public class ServiceRegistry
    {
        private static readonly ConcurrentDictionary<string, IChannel> _channels = new ConcurrentDictionary<string, IChannel>();
        private static readonly ConcurrentDictionary<string, IDevice> _devices = new ConcurrentDictionary<string, IDevice>();

        // Add or Update Channel
        public static void RegisterChannel(IChannel channel)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));

            _channels[channel.Name] = channel;
        }

        // Retrieve Channel
        public static IChannel GetChannel(string name)
        {
            _channels.TryGetValue(name, out IChannel channel);
            return channel;
        }

        public static IEnumerable<IChannel> GetChannels()
        {
            return _channels.Values;
        }

        // Add or Update Device
        public static void RegisterDevice(IDevice device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            _devices[device.Name] = device;
        }

        // Retrieve Device
        public static IDevice GetDevice(string name)
        {
            _devices.TryGetValue(name, out IDevice device);
            return device;
        }

        public static IEnumerable<IDevice> GetDevices()
        {
            return _devices.Values;
        }

        // Remove Channel
        public static bool RemoveChannel(string name)
        {
            return _channels.TryRemove(name, out _);
        }

        // Remove Device
        public static bool RemoveDevice(string name)
        {
            return _devices.TryRemove(name, out _);
        }

        // Clear all Channels
        public static void ClearChannels()
        {
            _channels.Clear();
        }

        // Clear all Devices
        public static void ClearDevices()
        {
            _devices.Clear();
        }
    }
}
