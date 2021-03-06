
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
using System.IO;
using System.Messaging;

using log4net;

#endregion

namespace Mindplex.Commons.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public abstract class GenericMessageGateway
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private readonly ILog logger;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private readonly Type[] targetTypes;
        
        /// <summary>
        /// 
        /// </summary>
        private readonly string destination;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private readonly MessageQueue queue;

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="destination"></param>
        /// 
        public GenericMessageGateway(string destination)
        {
            Guard.CheckIsNullOrEmpty(destination);
            this.destination = destination;

            logger = LogManager.GetLogger(GetType()); // TODO: handle potential log4net error?
            this.queue = GetMessageQueue(destination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="destination"></param>
        /// <param name="targetTypes"></param>
        /// 
        public GenericMessageGateway(string destination, Type[] targetTypes)
            : this(destination)
        {
            Guard.CheckForNull(targetTypes);
            this.targetTypes = targetTypes;

            queue.Formatter = CreateMessageFormatter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="destination"></param>
        /// 
        /// <returns></returns>
        /// 
        protected virtual MessageQueue GetMessageQueue(string destination)
        {
            if (destination.IndexOf('\\') == -1)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("prepending destination name with: .\\private$\\.");
                }

                destination = ".\\private$\\" + destination;
            }

            if (!MessageQueue.Exists(destination))
            {
                if (logger.IsWarnEnabled)
                {
                    logger.WarnFormat("destination does not exist: {0}.", destination);
                }

                try
                {
                    MessageQueue.Create(destination);

                    if (logger.IsInfoEnabled)
                    {
                        logger.InfoFormat("creating destination: {0}.", destination);
                    }
                }
                catch (MessageQueueException exception)
                {
                    if (logger.IsErrorEnabled)
                    {
                        logger.ErrorFormat("Failed to create destination: {0}.", destination);
                    }
                    throw new MessageException(string.Format("Failed to create destination: {0}.", destination), exception);
                }
            }

            return new MessageQueue(destination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        protected void OneWay(object message)
        {
            try
            {
                queue.Send(message);
            }
            catch (MessageQueueException exception)
            {
                throw new MessageException("Failed to send message.", exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="messageQueue"></param>
        /// <param name="message"></param>
        /// 
        protected void OneWay(MessageQueue messageQueue, Object message)
        {
            try
            {
                messageQueue.Send(message);
            }
            catch (MessageQueueException exception)
            {
                throw new MessageException("Failed to send message.", exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        protected virtual IMessageFormatter CreateMessageFormatter()
        {
            if (Guard.IsNull(targetTypes))
            {
                if (logger.IsWarnEnabled)
                {
                    logger.Warn("using default formatter.");
                }

                // returns default message formatter.
                return CreateMessageFormatter(new Type[] { typeof(string) });
            }
            else
            {
                return CreateMessageFormatter(targetTypes);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public virtual IMessageFormatter CreateMessageFormatter(Type[] targetTypes)
        {
            Guard.CheckForNull(targetTypes);

            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat("creating {0} formatter...", targetTypes);
            }

            return new XmlMessageFormatter(targetTypes);
        }

        /// <summary>
        /// Gets the contents of the Message received by this gateway.
        /// </summary>
        /// 
        /// <param name="message">The Message recieved by this gateway.</param>
        /// 
        protected virtual string MessageRecieved(Message message)
        {
            Guard.CheckForNull(message);
            return new StreamReader(message.BodyStream).ReadToEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        /// <returns></returns>
        /// 
        protected virtual Object ObjectRecieved(Message message)
        {
            Guard.CheckForNull(message);
            return message.Body;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public Type[] TargetTypes
        {
            get { return targetTypes; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Destination
        {
            get { return destination; }
        } 

        /// <summary>
        /// 
        /// </summary>
        /// 
        protected ILog Logger
        {
            get { return logger; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        protected MessageQueue Queue
        {
            get { return queue; }
        }
    }
}
