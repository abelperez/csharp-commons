
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
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

#endregion

namespace Mindplex.Commons.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class ServiceConfiguration : SerializableConfigurationSection
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public static readonly string configurationSection = "serviceConfiguration";

        /// <summary>
        /// 
        /// </summary>
        /// 
        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [ConfigurationProperty("serviceManager", IsRequired = true)]
        public NamedElementCollection<ServiceConfigurationData> ServiceManager
        {
            get
            {
                return (NamedElementCollection<ServiceConfigurationData>)base["serviceManager"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("Name = {0};", Name);

            return result.ToString();
        }
 
    }
}
