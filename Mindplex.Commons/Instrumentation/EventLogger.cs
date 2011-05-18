
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
    public class EventLogger 
    {
        /// <summary/>
        /// 
        //private EventLogEntryType defaultLogType;

        /// <summary/>
        /// 
        //private int logId;

        /// <summary/>
        /// 
        //private EventLog eventLog;

        /// <summary/>
        /// 
        private object sync = new object();

        /// <summary/>
        /// 
        /// <param name="source"/>
        /// <param name="logType"/>
        /// <param name="logId"/>
        /// 
        /// <exclude/>
        /// 
        public EventLogger(string source, EventLogEntryType logType, int logId)
            : this(string.Empty, ".", source, logType, logId)
        {
        }

        /// <summary/>
        /// 
        /// <param name="logName"></param>
        /// <param name="machineName"></param>
        /// <param name="source"></param>
        /// <param name="defaultLogType"></param>
        /// <param name="logId"/>
        /// 
        public EventLogger(string logName, string machineName, string source, EventLogEntryType defaultLogType, int logId)
        {
            //this.eventLog = new EventLog(logName, machineName, source);
            //this.defaultLogType = defaultLogType;
            //this.logId = logId;
        }

        /// <summary/>
        /// <param name="message"/>
        /// 
        /// <exclude/>
        /// 
        public virtual void Log(string message)
        {
            lock (sync)
            {
                //eventLog.WriteEntry(message, defaultLogType, logId);
            }
        }

        /// <summary/>
        /// 
        /// <param name="message"/>
        /// <param name="eventLogType"/>
        /// 
        /// <exclude/>
        /// 
        public virtual void Log(string message, EventLogEntryType eventLogType)
        {
            lock (sync)
            {
                //eventLog.WriteEntry(message, eventLogType, logId);
            }
        }
    }
}