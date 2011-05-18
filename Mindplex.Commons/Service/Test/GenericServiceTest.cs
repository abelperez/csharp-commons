
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
using System.Collections.Generic;
using System.Management;
using System.Management.Instrumentation;
using System.Text;

using NUnit.Core;
using NUnit.Framework;

#endregion

namespace Mindplex.Commons.Service.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    [TestFixture]
    public class GenericServiceTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [SetUp]
        public void SetUp()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestGetLogger()
        {
            SimpleService service = new SimpleService();
            service.DisplayMessage("Logger test passed!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestGenericServiceEvent()
        {
            SimpleService service = new SimpleService();
            service.OnServiceEvent += new GenericServiceEventHandler(OnGenericServiceEvent);
            service.DoSomething();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestServiceEvent()
        {
            SimpleService service = new SimpleService();
            service.OnSaved += new ServiceEventHandler<SimpleService, string>(OnServiceEvent);
            service.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestFireInfoEvent()
        {
            SimpleService service = new SimpleService();
            service.Execute();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// 
        private void OnGenericServiceEvent(object sender, GenericServiceEventArgs args)
        {
            Assert.IsNotNull(sender);
            Assert.IsNotNull(args);

            ((SimpleService)sender).DisplayMessage("recieved generic service event!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="service"></param>
        /// <param name="args"></param>
        /// 
        private void OnServiceEvent(SimpleService service, ServiceEventArgs<string> args)
        {
            Assert.IsNotNull(service);
            Assert.IsNotNull(args);

            service.DisplayMessage("recieved service event!");
        }
    }
}

#endif
