
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

namespace Mindplex.Commons.Service.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class SimpleResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private string firstValue;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string secondValue;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string FirstValue
        {
            get { return firstValue; }
            set { firstValue = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string SecondValue
        {
            get { return secondValue; }
            set { secondValue = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public override string ToString()
        {
            return string.Format("FirstValue = {0}, SecondValue = {1}", firstValue, secondValue);
        }

    }
}

#endif
