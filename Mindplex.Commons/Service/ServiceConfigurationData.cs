
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
using System.Configuration;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

#endregion

namespace Mindplex.Commons.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    ///<author>Abel Perez (java.aperez@gmail.com)</author>
    ///
    public class ServiceConfigurationData  : NamedConfigurationElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public ServiceConfigurationData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="name"></param>
        /// 
        public ServiceConfigurationData(string name)
            : base(name)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)base["type"];
            }
            set
            {
                base["type"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [ConfigurationProperty("singletonMethod", IsRequired = true)]
        public string SingletonMethod
        {
            get
            {
                return (string)base["singletonMethod"];
            }
            set
            {
                base["singletonMethod"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [ConfigurationProperty("returnType", IsRequired = true)]
        public string ReturnType
        {
            get
            {
                return (string)base["returnType"];
            }
            set
            {
                base["returnType"] = value;
            }
        }
    }
}