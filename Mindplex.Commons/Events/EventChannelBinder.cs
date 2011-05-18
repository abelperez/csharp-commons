
#region Imports

using System;

#endregion

namespace InternetBrands.Core.Events
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class EventChannelBinder
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="channel"></param>
        /// 
        public static void Bind(EventChannel channel)
        {
            Delegate targetConsumer = Delegate.CreateDelegate(channel.Event.EventHandlerType, channel.Consumer, channel.Method);
            channel.Event.AddEventHandler(channel.Sender, targetConsumer);
        }
    }
}
