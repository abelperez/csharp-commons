
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

#endregion

namespace Mindplex.Commons.Model.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class TestAccountEntity
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
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        private long amount;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public long Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// 
        public TestAccountEntity(long id, long amount)
        {
            this.id = id;
            this.amount = amount;
        }

    }
}

#endif