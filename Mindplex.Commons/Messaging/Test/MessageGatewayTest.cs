
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
using System.Xml;

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
    public class MessageGatewayTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string Destination = "Mindplex.Commons.messaging.test.messagegateway";

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendReceiveObject()
        {
            MessageGateway<object> gateway = new MessageGateway<object>(Destination);
            gateway.Send("Object Message Test");
            object response = gateway.Receive();

            Assert.IsNotNull(response);
            
            Console.WriteLine(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendReceiveString()
        {
            MessageGateway<string> gateway = new MessageGateway<string>(Destination);
            gateway.Send("String Message Test");
            string response = gateway.Receive();

            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);

            Console.WriteLine(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendReceiveXmlDocument()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\"?><Message><Test>Xml Document Test</Test></Message>");

            MessageGateway<XmlDocument> gateway = new MessageGateway<XmlDocument>(Destination);
            gateway.Send(xml);
            XmlDocument response = gateway.Receive();

            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.OuterXml);

            Console.WriteLine(response.OuterXml);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendReceiveCustomType()
        {
            MessageGateway<MockMessage> gateway = new MessageGateway<MockMessage>(Destination);
            gateway.Send(new MockMessage("Custom Type Test"));
            MockMessage response = gateway.Receive();

            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.Name);

            Console.WriteLine(response.Name);
        }

        [Test]
        public void TestAsyncReceive()
        {
            MessageGateway<MockMessage> gateway = new MessageGateway<MockMessage>(Destination);
            gateway.OnMessageReceived += ReceiveMessage;
            gateway.StopAsync();
            gateway.StartAsync();
            gateway.StartAsync();
            gateway.StopAsync();
            gateway.StopAsync();
            
            //gateway.Send(new MockMessage("1 Custom Type Test"));
            //gateway.Send(new MockMessage("2 Custom Type Test"));
            //Console.WriteLine("moving on...");

            //System.Threading.Thread.Sleep(3000);
            //gateway.StopAsync();

            ////MockMessage response = gateway.Receive(5000);
            ////Console.WriteLine("caught: {0}.", response.Name);

            //gateway.Send(new MockMessage("3 Custom Type Test"));
            //gateway.Send(new MockMessage("4 Custom Type Test"));
            //Console.WriteLine("After sending two messages and async stoped...");

            //gateway.StartAsync();
            //Console.WriteLine("started async again...");
            //gateway.Send(new MockMessage("Custom Type Test"));

            //Assert.IsNotNull(response);
            //Assert.IsNotEmpty(response.Name);

            //Console.WriteLine(response.Name);
            System.Threading.Thread.Sleep(10000);
        }

        public void ReceiveMessage(MockMessage message)
        {
            Console.WriteLine("received async: {0}.", message.Name);
        }
    }
}
