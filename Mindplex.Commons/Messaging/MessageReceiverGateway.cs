
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
using System.Messaging;
using System.Xml;

#endregion

namespace Mindplex.Commons.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class MessageReceiverGateway : GenericMessageGateway, IMessageReceiverGateway
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="destination"></param>
        /// 
        public MessageReceiverGateway(string destination)
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
        public MessageReceiverGateway(string destination, Type[] targetTypes)
            : base(destination, targetTypes)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public object ReceiveObject()
        {
            Message message = Queue.Receive();
            return ObjectRecieved(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="timeout"></param>
        /// 
        /// <returns></returns>
        /// 
        public object ReceiveObject(int timeout)
        {
            Message message = Queue.Receive(new TimeSpan(0, 0, timeout));
            return ObjectRecieved(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string ReceiveString()
        {
            Message message = Queue.Receive();
            return MessageRecieved(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="timeout"></param>
        /// 
        /// <returns></returns>
        /// 
        public string ReceiveString(int timeout)
        {
            Message response = Queue.Receive(new TimeSpan(0, 0, timeout));
            return MessageRecieved(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public XmlDocument ReceiveXmlDocument()
        {
            Message message = Queue.Receive();
            return ObjectRecieved(message) as XmlDocument;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="timeout"></param>
        /// 
        /// <returns></returns>
        /// 
        public XmlDocument ReceiveXmlDocument(int timeout)
        {
            Message message = Queue.Receive(new TimeSpan(0, 0, timeout));
            return ObjectRecieved(message) as XmlDocument;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public Message ReceiveMessage()
        {
            return Queue.Receive();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="timeout"></param>
        /// 
        /// <returns></returns>
        /// 
        public Message ReceiveMessage(int timeout)
        {
            return Queue.Receive(new TimeSpan(0, 0, timeout));
        }
    }
}
