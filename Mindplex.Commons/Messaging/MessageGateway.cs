
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
using System.Threading;

using log4net;

#endregion

namespace Mindplex.Commons.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <param name="message"></param>
    /// 
    public delegate void OnMessageEvent<T>(T message);

    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <typeparam name="T"></typeparam>
    /// 
    public class MessageGateway<T> : GenericMessageGateway, IMessageGateway<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private static ILog logger = LogManager.GetLogger(typeof(MessageGateway<T>));

        /// <summary>
        /// 
        /// </summary>
        /// 
        public event OnMessageEvent<T> OnMessageReceived;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private int active = 0;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private object sync = new object();

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="destination"></param>
        /// 
        public MessageGateway(string destination)
            : base(destination, new Type[] { typeof(T) })
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="destination"></param>
        /// <param name="async"></param>
        /// 
        public MessageGateway(string destination, bool async)
            : this(destination)
        {
            if (async)
            {
                StartAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void StartAsync()
        {
            if (Interlocked.Exchange(ref active, 1) == 0)
            {
                if (logger.IsInfoEnabled)
                {
                    logger.Info("Enabling Async mode.");
                }

                Queue.ReceiveCompleted += new ReceiveCompletedEventHandler(ReceiveCompleted);

                try
                {
                    Queue.BeginReceive();
                }
                catch (Exception exception)
                {
                    if (logger.IsErrorEnabled)
                    {
                        logger.Error("Failed to start Async mode.", exception);
                    }

                    if (Interlocked.Exchange(ref active, 0) == 1)
                    {
                        Queue.ReceiveCompleted -= new ReceiveCompletedEventHandler(ReceiveCompleted);
                    }

                    throw new MessageException("Failed to start Async mode.", exception);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void StopAsync()
        {
            if (Interlocked.Exchange(ref active, 0) == 1)
            {
                if (logger.IsInfoEnabled)
                {
                    logger.Info("Disabling Async mode.");
                }

                Queue.ReceiveCompleted -= new ReceiveCompletedEventHandler(ReceiveCompleted);
            }
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public void Send(T message)
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
        public void Send(T message, string destination)
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
        /// <returns></returns>
        /// 
        public T Receive()
        {
            Message message = Queue.Receive();
            return message.Body as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="destination"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Receive(string destination)
        {
            Guard.CheckIsNullOrEmpty(destination);
            MessageQueue queue = GetMessageQueue(destination);
            queue.Formatter = CreateMessageFormatter();
            Message message = queue.Receive(); 
            return message.Body as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="destination"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Receive(string destination, int timeout)
        {
            Guard.CheckIsNullOrEmpty(destination);
            MessageQueue queue = GetMessageQueue(destination);
            queue.Formatter = CreateMessageFormatter();
            Message message = queue.Receive(new TimeSpan(0, 0, timeout));
            return message.Body as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="timeout"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Receive(int timeout)
        {
            Message message = Queue.Receive(new TimeSpan(0, 0, timeout));
            return message.Body as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="event"></param>
        /// 
        private void ReceiveCompleted(object sender, ReceiveCompletedEventArgs @event)
        {
            MessageQueue queue = null;

            try
            {
                queue = sender as MessageQueue;
                Message message = queue.EndReceive(@event.AsyncResult);

                if (OnMessageReceived != null)
                {
                    OnMessageReceived(message.Body as T);
                }
            }
            catch (Exception exception)
            {
                if (logger.IsErrorEnabled)
                {
                    logger.Error("Failed to process ReceiveCompleted event.", exception);
                }
            }
            finally
            {
                if (active == 1)
                {
                    queue.BeginReceive();
                }
            }
        }
    }
}
