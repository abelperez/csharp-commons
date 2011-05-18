
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
    public class MessageReceiverGatewayTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestReceiveObject()
        {
            //IMessageReceiverGateway gateway = new MessageReceiverGateway("");
            //object result = gateway.ReceiveObject();

            MessageGateway<string> gateway = new MessageGateway<string>("boomerang");
            gateway.Send("abel");
            string response = gateway.Receive(10000);

            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);

            Console.WriteLine(response);
            
        }
    }
}
