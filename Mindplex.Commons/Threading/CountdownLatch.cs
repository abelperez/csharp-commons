
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
using System.Threading;

#endregion

namespace Mindplex.Commons.Threading
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class CountdownLatch
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private int countdown;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private ManualResetEvent signal;

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="count"></param>
        /// 
        public CountdownLatch(int count)
        {
            this.countdown = count;
            signal = new ManualResetEvent(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void Signal()
        {
            if (Interlocked.Decrement(ref countdown) == 0)
            {
                signal.Set();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void Wait()
        {
            signal.WaitOne();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void Wait(int timeout)
        {
            signal.WaitOne(timeout, false);
        }
    }
}