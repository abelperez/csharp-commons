
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
    [Service("SimpleService")]
    public sealed class SimpleServiceFactory : ServiceFactory<ISimpleService>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class ServiceFactoryTest : ServiceTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestCreatService()
        {
            SimpleServiceFactory factory = new SimpleServiceFactory();
            // ISimpleService service = factory.Create();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestCreatServiceFromAlias()
        {
            ISimpleService service = ServiceFactory<SimpleService>.Create("SimpleService");
            Assert.IsNotNull(service);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestForEachConcurrent()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            SimpleService service = new SimpleService();
            watch.Start();
            service.ForEachConcurrent(new string[] { "1", "2", "3" });
            watch.Stop();

            Console.WriteLine("Elapsed: {0}", watch.ElapsedMilliseconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestForEachSync()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            Manager service = new Manager();

            watch.Start();
            service.Execute();
            service.Execute();
            service.Execute();
            watch.Stop();

            Console.WriteLine("Elapsed: {0}", watch.ElapsedMilliseconds);
        }
    }
}

#endif