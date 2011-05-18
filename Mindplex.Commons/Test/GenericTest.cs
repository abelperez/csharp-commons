
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

using log4net;
using log4net.Config;

using NUnit.Framework;

using Spring.Core;
using Spring.Context;
using Spring.Context.Support;

#endregion

namespace Mindplex.Commons.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class GenericTest
    {
        private ILog logger;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public GenericTest()
        {
            XmlConfigurator.Configure();
            logger = LogManager.GetLogger(GetType());
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public virtual IApplicationContext GetApplicationContext()
        {
            Logger.Debug("initializing Spring application context...");
            return ContextRegistry.GetContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        protected ILog Logger
        {
            get { return this.logger; }
            set { this.logger = value; }
        }
    }
}
