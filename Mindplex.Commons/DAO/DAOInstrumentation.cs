
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

using InternetBrands.Core.Instrumentation;

#endregion

namespace InternetBrands.Core.DAO
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class DAOInstrumentation
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public static readonly int logId = 100;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public static readonly string source = "InternetBrands.Core.DAO";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static GenericInstrumentation instrumentation = new GenericInstrumentation(source, logId);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        public static void FireInfoEvent(string message, params object[] parameters)
        {
            instrumentation.FireInfoEvent(message, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        public static void FireWarningEvent(string message, params object[] parameters)
        {
            instrumentation.FireWarningEvent(message, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        public static void FireErrorEvent(string message, params object[] parameters)
        {
            instrumentation.FireErrorEvent(message, parameters);
        }
    }
}
