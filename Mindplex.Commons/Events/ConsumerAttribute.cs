
#region Imports

using System;

#endregion

namespace InternetBrands.Core.Events
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public class ConsumerAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private string channel;
                
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="channel"></param>
        /// 
        public ConsumerAttribute(string channel)
        {
            Guard.CheckIsNullOrEmpty(channel);
            this.channel = channel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Channel
        {
            get { return channel; }
            set { channel = value; }
        }
    }
}
