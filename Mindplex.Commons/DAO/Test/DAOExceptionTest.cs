
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

namespace Mindplex.Commons.DAO.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class DAOExceptionTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(DAOException))]
        public void TestThrowDAOException()
        {
            throw new DAOException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(DAOException))]
        public void TestThrowDAOExceptionErrorMessage()
        {
            throw new DAOException("Failed to persist entity.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(DAOException))]
        public void TestThrowDAOExceptionErrorMessageInnerException()
        {
            ApplicationException exception = new ApplicationException("Test inner exception.");
            throw new DAOException("Failed to persist entity.", exception);
        }
    }
}

#endif
