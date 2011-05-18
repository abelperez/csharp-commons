
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
using System.Text;
using System.Web.Services;

using Spring.Context;
using Spring.Context.Support;

#endregion

namespace Mindplex.Commons.Web.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class GenericWebService : WebService
    {
        /// <summary>
        /// Get's default application context.
        /// </summary>
        /// 
        public IApplicationContext ApplicationContext
        {
            get { return ContextRegistry.GetContext(); }
        }

        /// <summary>
        /// Get's an instance of a service from the default application context 
        /// based on the specified service name.
        /// </summary>
        /// 
        /// <param name="serviceName">The name os the service to get from the 
        /// default application context</param>
        /// <returns>Instance of a service based on the given service name.</returns>
        /// 
        public Object GetService(string serviceName)
        {
            return ContextRegistry.GetContext().GetObject(serviceName);
        }
    }
}