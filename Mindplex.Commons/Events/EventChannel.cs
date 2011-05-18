
// Copyright (C) 2011 Mindplex Media, LLC.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
// file except in compliance with the License. You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.

#region Imports

using System;
using System.Reflection;

#endregion

namespace Mindplex.Commons.Events
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