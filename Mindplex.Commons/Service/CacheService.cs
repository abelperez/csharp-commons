
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

using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

#endregion

namespace Mindplex.Commons.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class CacheService : ICacheService
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private string configurationName;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private CacheManager cacheManager;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public CacheService()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="configurationName"></param>
        /// 
        public CacheService(string configurationName)
        {
            if (string.IsNullOrEmpty(configurationName))
            {
                throw new ArgumentException("Configuration name is null.", "configurationName");
            }
            this.configurationName = configurationName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void Init()
        {
            cacheManager = CacheFactory.GetCacheManager(this.configurationName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// 
        public void Add(string key, object value)
        {
            cacheManager.Add(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="key"></param>
        /// 
        /// <returns></returns>
        /// 
        public object Fetch(string key)
        {
            return cacheManager.GetData(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void Flush()
        {
            cacheManager.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="key"></param>
        /// 
        /// <returns></returns>
        /// 
        public bool Contains(string key)
        {
            return cacheManager.Contains(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string ConfigurationName
        {
            get { return this.configurationName; }
            set { this.configurationName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public override string ToString()
        {
            return string.Format("Configuration Name: {0}", this.configurationName);
        }
    }
}
