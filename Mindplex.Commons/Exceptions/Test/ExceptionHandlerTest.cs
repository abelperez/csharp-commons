
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
using System.IO;
using System.Text;
using System.Reflection;
using System.Text;

using NUnit.Framework;

#endregion

namespace Mindplex.Commons.Exceptions.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class ExceptionHandlerTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private static bool SUPRESS = true;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static bool NOTIFY = true;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private void CauseException()
        {
            string trap = null;
            trap.ToLower();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(GenericException))]
        public void TestProcess()
        {
            try
            {
                CauseException();
            }
            catch (Exception exception)
            {
                ExceptionHandler<GenericExceptionFactory>.Process(exception, "Failed to process trap.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(GenericException))]
        public void TestProcessWithFormatMessage()
        {
            try
            {
                CauseException();
            }
            catch (Exception exception)
            {
                ExceptionHandler<GenericExceptionFactory>.Process(exception, "Failed to process {0} trap.", "format");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestProcessSuppression()
        {
            try
            {
                CauseException();
            }
            catch (Exception exception)
            {
                ExceptionHandler<GenericExceptionFactory>.Process(exception, SUPRESS, "Failed to process trap.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestProcessSuppressionWithFormatMessage()
        {
            try
            {
                CauseException();
            }
            catch (Exception exception)
            {
                ExceptionHandler<GenericExceptionFactory>.Process(exception, SUPRESS, "Failed to process {0} trap.", "suppression");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestProcessSuppressionWithNotify()
        {
            try
            {
                CauseException();
            }
            catch (Exception exception)
            {
                ExceptionHandler<GenericExceptionFactory>.Process(exception, SUPRESS, NOTIFY, "Failed to process trap.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestProcessSuppressionWithNotifyAndFormatMessage()
        {
            try
            {
                CauseException();
            }
            catch (Exception exception)
            {
                ExceptionHandler<GenericExceptionFactory>.Process(exception, SUPRESS, NOTIFY, "Failed to process {0} trap.", "notify");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(GenericException))]
        public void TestThrow()
        {
            ExceptionHandler<GenericExceptionFactory>.Throw(true, "Failed to do something!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(GenericException))]
        public void TestThrowWithNotify()
        {
            ExceptionHandler<GenericExceptionFactory>.Throw(true, "Failed to do something!");
        }
    }
}
