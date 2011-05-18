
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

using System.ComponentModel;
using System.Management.Instrumentation;
using System.Diagnostics;

#endregion

namespace InternetBrands.Core.Instrumentation
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class GenericInstrumentation
    {
        /// <summary/>
        /// 
        internal EventLogger eventLogger;

        /// <summary/>
        /// 
        public GenericInstrumentation(string source, int logId)
        {
            eventLogger = new EventLogger(source, EventLogEntryType.Information, logId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// 
        public void FireInfoEvent(string message)
        {
            eventLogger.Log(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// 
        public void FireInfoEvent(string message, params object[] args)
        {
            FireInfoEvent(string.Format(message, args));
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public void FireErrorEvent(string message)
        {
            eventLogger.Log(message, EventLogEntryType.Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// 
        public void FireErrorEvent(string message, params object[] args)
        {
            FireErrorEvent(string.Format(message, args));
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public void FireWarningEvent(string message)
        {
            eventLogger.Log(message, EventLogEntryType.Warning);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// 
        public void FireWarningEvent(string message, params object[] args)
        {
            FireWarningEvent(string.Format(message, args));
        }
    }
}