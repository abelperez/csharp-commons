
#region Imports

using System;

#endregion

namespace InternetBrands.Core.Events
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class ConsumerTypeAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private Type consumerType;

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="consumerType"></param>
        /// 
        public ConsumerTypeAttribute(Type consumerType)
        {
            Guard.CheckForNull(consumerType);
            this.consumerType = consumerType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public Type ConsumerType
        {
            get { return consumerType; }
            set { consumerType = value; }
        }
    }
}