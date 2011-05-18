
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
using System.Reflection;

#endregion 

namespace Mindplex.Commons.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class ServiceFactory<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="alias"></param>
        /// 
        /// <returns></returns>
        /// 
        public static T Create(string alias)
        {
            // gets the service configuration based on the specified alias.
            ServiceConfiguration serviceConfiguration = ConfigurationManager.GetSection(ServiceConfiguration.configurationSection) as ServiceConfiguration;
            ServiceConfigurationData serviceConfigurationData = serviceConfiguration.ServiceManager.Get(alias);

            // sets service type and singleton method return type.
            Type serviceType = Type.GetType(serviceConfigurationData.Type);
            Type singletonMethodReturnType = Type.GetType(serviceConfigurationData.ReturnType);

            // triggers creation of the singleton service by getting the configured service property.
            PropertyInfo propertyInfo = serviceType.GetProperty(serviceConfigurationData.SingletonMethod, singletonMethodReturnType);
            return (T)propertyInfo.GetValue(serviceType, null);
        }
    }
}
