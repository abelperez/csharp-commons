
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

namespace Mindplex.Commons.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    ///  
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ServiceAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Name;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public Type ClassType;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public ServiceAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="Name"></param>
        /// <param name="ClassType"></param>
        /// 
        public ServiceAttribute(string Name)
        {
            this.Name = Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="Name"></param>
        /// <param name="ClassType"></param>
        /// 
        public ServiceAttribute(string Name, Type ClassType)
        {
            this.Name = Name;
            this.ClassType = ClassType;
        }
    }
}