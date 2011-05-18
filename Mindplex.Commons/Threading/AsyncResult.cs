
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
using System.Threading;

#endregion

namespace Mindplex.Commons.Threading
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class AsyncResult<TR> : IAsyncResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private AsyncCallback callback;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private object asyncState;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private bool completedSync = false;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private bool completed = false;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private ManualResetEvent signal;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private Exception exception;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private TR result;

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// 
        public AsyncResult(AsyncCallback callback, object asyncState)
        {
            this.callback = callback;
            this.asyncState = asyncState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="result"></param>
        /// <param name="exception"></param>
        /// <param name="completedSync"></param>
        /// 
        public void Signal(TR result, Exception exception, bool completedSync)
        {
            this.result = result;
            this.exception = exception;
            this.completedSync = completedSync;
            completed = true;

            if (signal != null)
            {
                signal.Set();
            }

            if (callback != null)
            {
                callback(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public TR EndInvoke()
        {
            if (!IsCompleted)
            {
                AsyncWaitHandle.WaitOne();
                AsyncWaitHandle.Close();
            }

            if (exception != null)
            {
                throw exception;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public object AsyncState
        {
            get
            {
                return asyncState;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public WaitHandle AsyncWaitHandle
        {
            get
            {
                if (signal == null)
                {
                    Boolean done = IsCompleted;
                    ManualResetEvent internalWaiter = new ManualResetEvent(done);
                    if (Interlocked.CompareExchange(ref signal, internalWaiter, null) != null)
                    {
                        // Another thread created the waiter; dispose the waiter we just created
                        internalWaiter.Close();
                    }
                    else
                    {
                        if (!done && IsCompleted)
                        {
                            // If the operation wasn't done when we created 
                            // the internal waiter but is now done then set the waiter.
                            signal.Set();
                        }
                    }
                }
                return signal;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public bool CompletedSynchronously
        {
            get
            {
                return completedSync;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public bool IsCompleted
        {
            get
            {
                return completed;
            }
        }
    }
}
