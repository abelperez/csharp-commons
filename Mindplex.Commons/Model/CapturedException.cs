
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
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Text;

#endregion

namespace Mindplex.Commons.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class CapturedException
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private Exception exception;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private bool outerException = false;

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// 
        public CapturedException(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            this.exception = exception;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public bool OuterException
        {
            get 
            { 
                return outerException; 
            }
            set 
            { 
                outerException = value; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string ExceptionType
        {
            get
            {
                return exception.GetType().FullName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string ExceptionMessage
        {
            get
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string ExceptionSource
        {
            get
            {
                return exception.Source;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string StackTrace
        {
            get
            {
                return exception.StackTrace;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public override string ToString()
        {
            return new StringBuilder().AppendFormat("OuterException = {0}, Type = {1}, Message = {2}, Source = {3};", OuterException, ExceptionType, ExceptionMessage, ExceptionSource).ToString();
        }
    }
}


