
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
using System.Runtime.Serialization;

#endregion

namespace Mindplex.Commons.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class ServiceException : System.ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public ServiceException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public ServiceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// 
        public ServiceException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="serializationInfo"></param>
        /// <param name="streamContext"></param>
        /// 
        public ServiceException(SerializationInfo serializationInfo, StreamingContext streamContext)
            : base(serializationInfo, streamContext)
        {
        }
    }
}