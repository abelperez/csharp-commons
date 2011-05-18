
#region Imports

using System;
using System.Collections.Generic;
using System.Reflection;

#endregion

namespace InternetBrands.Core.Events
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class ConsumerAttacher
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// 
        public static void Attach(object sender)
        {
            object consumer = GetConsumer(sender.GetType());

            Dictionary<string, List<EventInfo>> @events = GetEventSenders(sender);
            Dictionary<string, List<MethodInfo>> methods = GetTargetConsumers(consumer);

            Join(@events, methods, sender, consumer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="senderType"></param>
        /// 
        /// <returns></returns>
        /// 
        private static object GetConsumer(Type senderType)
        {
            ConsumerTypeAttribute result = Attribute.GetCustomAttribute(senderType, typeof(ConsumerTypeAttribute), true) as ConsumerTypeAttribute;
            return Activator.CreateInstance(result.ConsumerType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// 
        /// <returns></returns>
        /// 
        private static Dictionary<string, List<EventInfo>> GetEventSenders(object sender)
        {
            Dictionary<string, List<EventInfo>> result = new Dictionary<string, List<EventInfo>>();

            foreach (EventInfo member in sender.GetType().GetEvents())
            {
                object[] attributes = member.GetCustomAttributes(typeof(SenderAttribute), false);
                foreach (SenderAttribute senderAttribute in attributes)
                {
                    if (!result.ContainsKey(senderAttribute.Channel))
                    {
                        result.Add(senderAttribute.Channel, new List<EventInfo>());
                    }
                    result[senderAttribute.Channel].Add(member);
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="consumer"></param>
        /// 
        /// <returns></returns>
        /// 
        private static Dictionary<string, List<MethodInfo>> GetTargetConsumers(object consumer)
        {
            Dictionary<string, List<MethodInfo>> result = new Dictionary<string, List<MethodInfo>>();

            foreach (MethodInfo member in consumer.GetType().GetMethods())
            {
                object[] attributes = member.GetCustomAttributes(typeof(ConsumerAttribute), false);
                foreach (ConsumerAttribute consumerAttribute in attributes)
                {
                    if (!result.ContainsKey(consumerAttribute.Channel))
                    {
                        result.Add(consumerAttribute.Channel, new List<MethodInfo>());
                    }
                    result[consumerAttribute.Channel].Add(member);
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="eventSenders"></param>
        /// <param name="targetConsumers"></param>
        /// <param name="sender"></param>
        /// <param name="consumer"></param>
        /// 
        private static void Join(Dictionary<string, List<EventInfo>> eventSenders, Dictionary<string, List<MethodInfo>> targetConsumers, object sender, object consumer)
        {
            foreach (string channel in eventSenders.Keys)
            {
                if (!targetConsumers.ContainsKey(channel))
                {
                    continue;
                }

                List<EventInfo> senders = eventSenders[channel];
                List<MethodInfo> consumers = targetConsumers[channel];

                foreach (EventInfo eventInfo in senders)
                {
                    foreach (MethodInfo methodInfo in consumers)
                    {
                        EventChannelBinder.Bind(new EventChannel(channel, sender, consumer, eventInfo, methodInfo));
                    }
                }
            }
        }
    }
}
