
#region Imports

using System;
using System.Reflection;

#endregion

namespace InternetBrands.Core.Events
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class EventChannel
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private string name;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private object sender;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private object consumer;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private EventInfo @event;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private MethodInfo method;

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="name"></param>
        /// <param name="sender"></param>
        /// <param name="consumer"></param>
        /// <param name="event"></param>
        /// <param name="method"></param>
        /// 
        public EventChannel(string name, object sender, object consumer, EventInfo @event, MethodInfo method)
        {
            Guard.CheckIsNullOrEmpty(name);
            Guard.CheckForNull(sender, consumer, @event, method);

            this.name = name;
            this.sender = sender;
            this.consumer = consumer;
            this.@event = @event;
            this.method = method;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public object Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public object Consumer
        {
            get { return consumer; }
            set { consumer = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public EventInfo Event
        {
            get { return @event; }
            set { @event = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public MethodInfo Method
        {
            get { return method; }
            set { method = value; }
        }
    }
}