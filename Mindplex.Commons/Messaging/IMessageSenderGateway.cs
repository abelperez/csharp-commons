
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
    public interface IMessageSenderGateway
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        string Destination { get; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        Type[] TargetTypes { get; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        void Send(object message);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// 
        void Send(object message, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// <param name="targetTypes"></param>
        /// 
        void Send(object message, string destination, Type[] targetTypes);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        void Send(string message);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// 
        void Send(string message, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// <param name="targetTypes"></param>
        /// 
        void Send(string message, string destination, Type[] targetTypes);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        void Send(XmlDocument message);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// 
        void Send(XmlDocument message, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// <param name="targetTypes"></param>
        /// 
        void Send(XmlDocument message, string destination, Type[] targetTypes);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        void Send(Message message);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// 
        void Send(Message message, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="destination"></param>
        /// <param name="targetTypes"></param>
        /// 
        void Send(Message message, string destination, Type[] targetTypes);
    }
}
