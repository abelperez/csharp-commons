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

// ==============================================================================
// GenericService is a base class that provides generic functionality required
// by Service Layer components.  All service layer components should extend
// this class in order to minimize code redundancy across service layer 
// components.
// 
// GenericService.cs
// @author Abel Perez
// ==============================================================================

#region Imports

using System;

using Mindplex.Commons.Instrumentation;
using log4net;

#endregion

namespace Mindplex.Commons.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    /// 
    public delegate void ServiceEventHandler<T, E>(T sender, ServiceEventArgs<E> args);

    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <param name="sender"></param>
    /// <param name="args"></param>
    /// 
    public delegate void GenericServiceEventHandler(object sender, GenericServiceEventArgs args);

    /// <summary>
    /// <para>
    /// GenericService is a base class that provides generic functionality required
    /// by Service Layer components.  All service layer components should extend
    /// this class in order to minimize code redundancy across service layer 
    /// components.
    /// </para>
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public abstract class GenericService
    {
        /// <summary>
        /// The name of this service.
        /// </summary>
        /// 
        private string name;

        /// <summary>
        /// Default logger for this service.
        /// </summary>
        /// 
        private static ILog logger;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private Object syncRoot = new Object();

        /// <summary>
        /// 
        /// </summary>
        /// 
        public event GenericServiceEventHandler OnServiceEvent;
        
        /// <summary>
        /// Simple constructor.
        /// </summary>
        /// 
        public GenericService()
        {
        }

        /// <summary>
        /// Initializes this service.
        /// </summary>
        /// 
        /// <param name="Name">The name of this service.</param>
        /// 
        public virtual void Initialize(string Name)
        {
            this.name = Name;
            ServiceInstrumentation.FireInfoEvent("Initialized {0} under name: {1}.", GetType(), Name); 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// 
        protected void FireServiceEvent()
        {
            if (OnServiceEvent == null)
            {
                return;
            }
            OnServiceEvent(this, new GenericServiceEventArgs());
            ServiceInstrumentation.FireInfoEvent("{0} fired service event.", GetType()); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        protected void FireInfoEvent(string message, params object[] parameters)
        {
            ServiceInstrumentation.FireInfoEvent(message, parameters); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        protected void FireWarningEvent(string message, params object[] parameters)
        {
            ServiceInstrumentation.FireWarningEvent(message, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        protected void FireErrorEvent(string message, params object[] parameters)
        {
            ServiceInstrumentation.FireErrorEvent(message, parameters);
        }

        /// <summary>
        /// Get's and set's the name of this service.
        /// </summary>
        /// 
        public string Name
        {
            get 
            { 
                return name; 
            }
            set 
            { 
                name = value; 
            }
        }

        /// <summary>
        /// Get's default logger instance for this service. 
        /// </summary>
        /// 
        protected ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    lock (SyncRoot) {
                        if (logger == null)
                        {
                            logger = LogManager.GetLogger(GetType());
                            ServiceInstrumentation.FireInfoEvent("Initialized {0} log manager.", GetType());
                        }
                    }
                }
                return logger;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public Object SyncRoot
        {
            get
            {
                return syncRoot;
            }
        }
    }
}
