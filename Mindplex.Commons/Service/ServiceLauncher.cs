
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
    public class ServiceLauncher
    {

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public GenericService Start()
        {
            foreach (ServiceAttribute attribute in GetType().GetCustomAttributes(typeof(ServiceAttribute), true))
            {
                if ((attribute.ClassType != null) && ((attribute.ClassType.IsSubclassOf(typeof(GenericService)))))
                {
                    GenericService service = (GenericService)attribute.ClassType.GetConstructor(System.Type.EmptyTypes).Invoke(null);
                    service.Initialize(attribute.Name);
                    return service;
                }
            }
            throw new Exception(GetType().ToString() + " class does not have a Service.ClassType attribute or implement GenericService.");
        }
    }
}
