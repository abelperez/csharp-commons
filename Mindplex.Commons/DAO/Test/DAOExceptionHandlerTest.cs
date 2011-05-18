
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

using NUnit.Framework;

#endregion

namespace InternetBrands.Core.DAO.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class DAOExceptionHandlerTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestProcessSupressException()
        {
            try
            {
                string trap = null;
                trap.ToLower();
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, true, "Failed to complete operation: {0}.", "TRAP");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestProcessException()
        {
            try
            {
                string trap = null;
                trap.ToLower();
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, true, "Failed to complete operation: TRAP.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(DAOException))]
        public void TestProcessFormatException()
        {
            try
            {
                string trap = null;
                trap.ToLower();
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to complete operation: {0}.", "TRAP");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestNotify()
        {
            try
            {
                string trap = null;
                trap.ToLower();
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, true, true, "Failed to capture traps.");
            }
        }
    }
}

#endif