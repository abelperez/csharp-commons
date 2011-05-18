
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
using System.Management;
using System.Management.Instrumentation;

#endregion

namespace InternetBrands.Core.Instrumentation
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    [ManagedName("InstrumentationEvent")]
    public class InstrumentationEvent : BaseEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private int id;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string message;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public InstrumentationEvent()
        {
            Message = string.Empty;
            Id = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary/>
        /// 
        /// <exclude/>
        /// 
        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public override string ToString()
        {
            return "Message = " + message;
        }
    }
}