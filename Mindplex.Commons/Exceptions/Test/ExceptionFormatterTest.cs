
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

using InternetBrands.Core.Model;
using InternetBrands.Core.Service;

using NUnit.Core;
using NUnit.Framework;

#endregion

namespace Mindplex.Commons.Exceptions.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    [TestFixture]
    public class ExceptionFormatterTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestCaptureException()
        {
            ServiceException service = new ServiceException("Failed to invoke service.", new ServiceException("Failed to invoke other service."));
            CapturedExceptionCollection<CapturedException> exceptions = ExceptionFormatter.CaptureException(service);

            Assert.IsNotNull(exceptions);
            Assert.IsNotEmpty(exceptions.GetEntities());

            foreach (CapturedException exception in exceptions)
            {
                Assert.IsNotNull(exception);
                Console.WriteLine(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestFormat()
        {
            string summary = ExceptionFormatter.Format(new ServiceException("Failed to invoke service.", new ServiceException("Failed to invoke other service.")));

            Assert.IsNotNull(summary);
            Assert.IsNotEmpty(summary);
            Console.WriteLine(summary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestFormatAsXml()
        {
            string summary = ExceptionFormatter.FormatAsXml(new ServiceException("Failed to invoke service.", new ServiceException("Failed to invoke other service.")));

            Assert.IsNotNull(summary);
            Assert.IsNotEmpty(summary);
            Console.WriteLine(summary);
        }
    }
}

#endif
