
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

namespace InternetBrands.Core.DAO.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class QueryTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestGenerateJoinQuery()
        {
            Query<Owner, Car> query = new Query<Owner, Car>();
            string join = query.GenerateJoinQuery();

            Assert.IsNotNull(join);
            Console.WriteLine(join);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public class Car
        {
            /// <summary>
            /// 
            /// </summary>
            /// 
            private long id;

            /// <summary>
            /// 
            /// </summary>
            /// 
            private string name;

            /// <summary>
            /// 
            /// </summary>
            /// 
            public long Id
            {
                get { return id; }
                set { id = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// 
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public class Owner
        {
            /// <summary>
            /// 
            /// </summary>
            /// 
            private long id;

            /// <summary>
            /// 
            /// </summary>
            /// 
            private string name;

            /// <summary>
            /// 
            /// </summary>
            /// 
            private string lastName;

            /// <summary>
            /// 
            /// </summary>
            /// 
            public long Id
            {
                get { return id; }
                set { id = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// 
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// 
            public string LastName
            {
                get { return lastName; }
                set { lastName = value; }
            }
        }
    }
}

#endif
