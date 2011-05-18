
#if UNIT_TEST

#region Imports

using System;

using NUnit.Core;
using NUnit.Framework;

#endregion

namespace InternetBrands.Core.Events.Test
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