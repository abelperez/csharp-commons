
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

#if UNIT_TEST

#region Imports

using System;
using System.Management;
using System.Management.Instrumentation;
using System.Threading;

using NUnit.Core;
using NUnit.Framework;

#endregion

namespace InternetBrands.Core.Instrumentation.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    [TestFixture]
    public class GenericInstrumentationTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        protected bool captured = false;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const string WmiPath = @"\root\InternetBrands\Core\Instrumentation";

        /// <summary>
        /// 
        /// </summary>
        /// 
        public delegate void WmiEventExecutor();
                                       
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestFireEntitySavedEvent()
        {
            WmiEventExecutor executor = new WmiEventExecutor(FireEntitySavedEvent);
            ExecuteWMI("InstrumentationEvent", executor, "Message = \"Entity entity-1 has been saved.\"");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestFireInstrumentationEvent()
        {
            for (int i = 0; i < 10; i++)
            {
                InstrumentationEvent instrumentationEvent = new InstrumentationEvent();
                instrumentationEvent.Message = string.Format("Entity entity-{0} has been saved.", i);
                instrumentationEvent.Id = (i * 100);
                instrumentationEvent.Fire();
            }
         }

        /// <summary>
        /// 
        /// </summary>
        /// 
        private void FireEntitySavedEvent()
        {
            InstrumentationEvent instrumentationEvent = new InstrumentationEvent();
            instrumentationEvent.Message = "Entity entity-1 has been saved.";
            instrumentationEvent.Id = 100;
            instrumentationEvent.Fire();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        private void StartRunner()
        {
            bool loop = true;
            int count = 0;
            int timeoutCount = 100;

            while (loop)
            {
                loop = !captured && (count++ < timeoutCount);
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// 
        private void EventArrived(object sender, EventArrivedEventArgs args)
        {
            this.captured = true;

            string id = args.NewEvent.GetPropertyValue("Id").ToString();
            string message = args.NewEvent.GetPropertyValue("Message").ToString();
            Console.WriteLine(string.Format("recieved event {0} with message: {1}", id, message));
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="eventName"></param>
        /// <param name="executor"></param>
        /// <param name="expected"></param>
        /// 
        public void ExecuteWMI(string eventName, WmiEventExecutor executor, string expected)
        {
            ManagementScope scope = new ManagementScope(@"\\." + WmiPath);
            scope.Options.EnablePrivileges = true;

            EventQuery query = new EventQuery("select * from " + eventName);

            using (ManagementEventWatcher watcher = new ManagementEventWatcher(scope, query))
            {
                watcher.EventArrived += new EventArrivedEventHandler(EventArrived);
                watcher.Start();

                executor();

                StartRunner();
                watcher.Stop();
            }
        }
    }
}

#endif
