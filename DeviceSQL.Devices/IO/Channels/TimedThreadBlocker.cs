#region Imported Types

using System.Threading;

#endregion

namespace DeviceSQL.IO.Channels
{
    public class TimedThreadBlocker
    {
        public static void Wait(int milliSecondsToWait)
        {
            if (milliSecondsToWait > 0)
            {
                using (var eventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset))
                using (var timer = new Timer((e) =>
                 {
                     eventWaitHandle.Set();
                 }, null, milliSecondsToWait, Timeout.Infinite))
                {
                    eventWaitHandle.WaitOne();
                }
            }
        }
    }

}
