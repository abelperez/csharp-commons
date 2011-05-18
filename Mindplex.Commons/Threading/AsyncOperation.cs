
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

#endregion

namespace Mindplex.Commons.Threading
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class AsyncOperation<T, T0>
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private T state;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private T0 asyncResult;
                
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="state"></param>
        /// <param name="asyncResult"></param>
        /// 
        public AsyncOperation(T state, T0 asyncResult)
        {
            this.asyncResult = asyncResult;
            this.state = state;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public T State
        {
            get { return state; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public T0 AsyncResult
        {
            get { return asyncResult; }
        }
    }
}
