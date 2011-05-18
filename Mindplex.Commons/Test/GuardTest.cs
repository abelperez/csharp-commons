
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

using NUnit.Framework;

#endregion

namespace Mindplex.Commons.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class GuardTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCheckForNull()
        {
            object target = null;
            Guard.CheckForNull(target);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCheckForNullWithParameterName()
        {
            object name = null;
            Guard.CheckForNull(name, "name");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCheckIsNullOrEmpty()
        {
            string name = String.Empty;
            Guard.CheckIsNullOrEmpty(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCheckIsNullOrEmptyWithParameterName()
        {
            string name = String.Empty;
            Guard.CheckIsNullOrEmpty(name, "name");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCheckIsNullOrEmptyArguments()
        {
            string name = String.Empty;
            string lastname = String.Empty;
            Guard.CheckIsNullOrEmpty(name, lastname);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestIsEmpty()
        {
            List<string> collection = new List<string>(); 
            Assert.IsTrue(Guard.IsEmpty(collection));
            Assert.IsTrue(Guard.IsEmpty(null));
        }
    }
}
