
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

#endregion

namespace Mindplex.Commons.Events
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
