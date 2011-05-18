
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
using System.Text;

using NUnit.Core;
using NUnit.Framework;

#endregion

namespace Mindplex.Commons.Messaging.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class MessageSenderGatewayTest
    {
        #region Message Type Destination

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string BaseDestination = "core.messaging.test.";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string ObjectType = "object";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string StringType = "string";
                
        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string XmlDocumentType = "xmldocument";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string MessageType = "message";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string Destination = "destination";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string TargetTypes = "targettypes";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string SEPERATOR = ".";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string ObjectMessage = BaseDestination + ObjectType;
        private const string ObjectMessageWithDestination = ObjectMessage + SEPERATOR + Destination;
        private const string ObjectMessageWithDestinationAndTargetTypes = ObjectMessageWithDestination + SEPERATOR + TargetTypes;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string StringMessage = BaseDestination + StringType;
        private const string StringMessageWithDestination = StringMessage + SEPERATOR + Destination;
        private const string StringMessageWithDestinationAndTargetTypes = StringMessageWithDestination + SEPERATOR + TargetTypes;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string XmlDocumentMessage = BaseDestination + XmlDocumentType;
        private const string XmlDocumentMessageWithDestination = XmlDocumentMessage + SEPERATOR + Destination;
        private const string XmlDocumentMessageWithDestinationAndTargetTypes = XmlDocumentMessageWithDestination + SEPERATOR + TargetTypes;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string MessageMessage = BaseDestination + MessageType;
        private const string MessageMessageWithDestination = MessageMessage + SEPERATOR + Destination;
        private const string MessageMessageWithDestinationAndTargetTypes = MessageMessageWithDestination + SEPERATOR + TargetTypes;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly Type[] targetTypes = new Type[] { typeof(string) };

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly Type[] mockTargetTypes = new Type[] { typeof(MockMessage) };
        
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestConstructorWithDestination()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(BaseDestination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestConstructorWithDestinationAndTypes()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(BaseDestination, targetTypes);
        }

        #region Send Object Test

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendObjectMessage()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(ObjectMessage);
            gateway.Send(new MockMessage());
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendObjectMessageWithDestination()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(ObjectMessage);
            gateway.Send(new MockMessage(), ObjectMessageWithDestination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendObjectMessageWithDestinationAndTargetTypes()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(ObjectMessage);
            gateway.Send(new MockMessage(), ObjectMessageWithDestinationAndTargetTypes);
        }

        #endregion

        #region Send String Test

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendStringMessage()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(StringMessage);
            gateway.Send(new MockMessage());
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendStringMessageWithDestination()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(StringMessage);
            gateway.Send(new MockMessage(), StringMessageWithDestination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendStringMessageWithDestinationAndTargetTypes()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(StringMessage);
            gateway.Send(new MockMessage(), StringMessageWithDestinationAndTargetTypes);
        }

        #endregion

        #region Send XmlDocument Test

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendXmlDocumentMessage()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(XmlDocumentMessage);
            gateway.Send(new MockMessage());
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendXmlDocumentMessageWithDestination()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(XmlDocumentMessage);
            gateway.Send(new MockMessage(), XmlDocumentMessageWithDestination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendXmlDocumentMessageWithDestinationAndTargetTypes()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(XmlDocumentMessage);
            gateway.Send(new MockMessage(), XmlDocumentMessageWithDestinationAndTargetTypes);
        }

        #endregion

        #region Send Message Test

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendMessageMessage()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(MessageMessage);
            gateway.Send(new MockMessage());
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendMessageMessageWithDestination()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(MessageMessage);
            gateway.Send(new MockMessage(), MessageMessageWithDestination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendMessageMessageWithDestinationAndTargetTypes()
        {
            IMessageSenderGateway gateway = new MessageSenderGateway(MessageMessage);
            gateway.Send(new MockMessage(), MessageMessageWithDestinationAndTargetTypes);
        }

        #endregion

    }
}
