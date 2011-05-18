
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
using System.Collections.Generic;
using System.Messaging;
using System.Text;
using System.Xml;

using log4net;

#endregion

namespace Mindplex.Commons.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class MessageSenderGateway : GenericMessageGateway, IMessageSenderGateway
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="destination"></param>
        /// 
        public MessageSenderGateway(string destination)
            : base(destination)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="destination"></param>
        /// <param name="targetTypes"></param>
        /// 
        public MessageSenderGateway(string destination, Type[] targetTypes)
            : base(destination, targetTypes)
        {
        }

        #region Send Object Message

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public void Send(object message)
        {
            Guard.CheckForNull(message);
            OneWay(message);    
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// 
        public void Send(object message, string destination)
        {
            Guard.CheckForNull(message);
            Guard.CheckIsNullOrEmpty(destination);

            using (MessageQueue queue = GetMessageQueue(destination))
            {
                OneWay(queue, message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        /// <param name="targetTypes"></param>
        /// 
        public void Send(object message, string destination, Type[] targetTypes)
        {
            Guard.CheckForNull(message, targetTypes);
            Guard.IsNullOrEmpty(destination);

            using (MessageQueue queue = GetMessageQueue(destination))
            {
                queue.Formatter = CreateMessageFormatter(targetTypes);
                OneWay(queue, message);
            }
        }

        #endregion

        #region Send String Message

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public void Send(string message)
        {
            Guard.CheckIsNullOrEmpty(message);
            OneWay(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// 
        public void Send(string message, string destination)
        {
            Guard.CheckIsNullOrEmpty(message, destination);
            using (MessageQueue queue = GetMessageQueue(destination))
            {
                OneWay(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// <param name="targetTypes"></param>
        /// 
        public void Send(string message, string destination, Type[] targetTypes)
        {
            Guard.CheckIsNullOrEmpty(message, destination);
            Guard.CheckForNull(targetTypes);

            using (MessageQueue queue = GetMessageQueue(destination))
            {
                queue.Formatter = CreateMessageFormatter(targetTypes);
                OneWay(message);
            }
        }

        #endregion

        #region Send XmlDocument Message

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public void Send(XmlDocument message)
        {
            Guard.CheckForNull(message);
            OneWay(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// 
        public void Send(XmlDocument message, string destination)
        {
            Guard.CheckForNull(message);
            Guard.CheckIsNullOrEmpty(destination);

            using (MessageQueue queue = GetMessageQueue(destination)) 
            {
                OneWay(queue, message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// <param name="targetTypes"></param>
        /// 
        public void Send(XmlDocument message, string destination, Type[] targetTypes)
        {
            Guard.CheckForNull(message, targetTypes);
            Guard.CheckIsNullOrEmpty(destination);

            using (MessageQueue queue = GetMessageQueue(destination))
            {
                queue.Formatter = CreateMessageFormatter(targetTypes);
                OneWay(queue, message);
            }
        }

        #endregion

        #region Send Message Message

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public void Send(Message message)
        {
            Guard.CheckForNull(message);
            OneWay(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// 
        public void Send(Message message, string destination)
        {
            Guard.CheckForNull(message);
            Guard.CheckIsNullOrEmpty(destination);

            using (MessageQueue queue = GetMessageQueue(destination))
            {
                OneWay(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// <param name="targetTypes"></param>
        /// 
        public void Send(Message message, string destination, Type[] targetTypes)
        {
            Guard.CheckForNull(message, targetTypes);
            Guard.CheckIsNullOrEmpty(destination);

            using (MessageQueue queue = GetMessageQueue(destination))
            {
                queue.Formatter = CreateMessageFormatter(targetTypes);
                OneWay(message);
            }
        }

        #endregion

    }
}
