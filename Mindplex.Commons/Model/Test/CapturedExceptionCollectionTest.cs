
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

using Mindplex.Commons.Test;
using Mindplex.Commons.Common;
using Mindplex.Commons.Service;

#endregion

namespace Mindplex.Commons.Model.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    [TestFixture]
    public class CapturedExceptionCollectionTest : GenericTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestCapturedExceptionCollection()
        {
            CapturedExceptionCollection<CapturedException> exceptions = new CapturedExceptionCollection<CapturedException>();
            exceptions.Add(new CapturedException(new ServiceException("Failed to invoke service")));

            Assert.IsNotEmpty(exceptions.GetEntities());
            Assert.IsNotNull(exceptions.AppDomainName);
            Assert.IsNotNull(exceptions.AssemblyFullName);
            Assert.IsNotNull(exceptions.MachineName);
            Assert.IsNotNull(exceptions.ThreadIdentity);
            Assert.IsNotNull(exceptions.TimeStamp);
            Assert.IsNotNull(exceptions.WindowsIdentity);

            foreach (CapturedException exception in exceptions)
            {
                Assert.IsNotNull(exception);
                Console.WriteLine(exception);
            }
        }
    }
}

#endif
