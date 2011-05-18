
#region Imports

using System;

#endregion

namespace InternetBrands.Core.Events
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Event, AllowMultiple=false)]
    public class SenderAttribute : Attribute
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
        public SenderAttribute(string channel)
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
