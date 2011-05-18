
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

namespace Mindplex.Commons.Messaging.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [Serializable]
    public class MockMessage
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private string name;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public MockMessage()
        {
            this.name = "Test Message";
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="name"></param>
        public MockMessage(string name)
        {
            Guard.CheckIsNullOrEmpty(name);
            this.name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
