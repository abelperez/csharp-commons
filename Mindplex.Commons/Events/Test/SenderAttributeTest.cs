
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

#if UNIT_TEST

#region Imports

using System;

using NUnit.Core;
using NUnit.Framework;

#endregion

namespace Mindplex.Commons.Events.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    [ConsumerType(typeof(SenderAttributeTest))]
    public class SenderAttributeTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Sender("channel")]
        public event EventHandler Sender;

        [Sender("channel")]
        public event EventHandler SenderAgain;

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Sender("internal.channel")]
        public event EventHandler InternalSender;

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSenderAttribute()
        {
            Sender += delegate(object sender, EventArgs @event) {};
            InternalSender += delegate(object sender, EventArgs @event) { };

            ConsumerAttacher.Attach(this);

            Sender(this, new EventArgs());
            SenderAgain(this, new EventArgs());
            InternalSender(this, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="event"></param>
        /// 
        [Consumer("channel")]
        public void Receive(object sender, EventArgs @event)
        {
            Assert.IsNotNull(sender);
            Assert.IsNotNull(@event);

            Console.WriteLine("Consumer received event from channel.");
        }

        [Consumer("channel")]
        public void ReceiveAgain(object sender, EventArgs @event)
        {
            Assert.IsNotNull(sender);
            Assert.IsNotNull(@event);

            Console.WriteLine("Consumer received event from channel again.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="event"></param>
        /// 
        [Consumer("internal.channel")]
        public void InternalReceive(object sender, EventArgs @event)
        {
            Assert.IsNotNull(sender);
            Assert.IsNotNull(@event);

            Console.WriteLine("Consumer received event from internal channel.");
        }
    }
}

#endif