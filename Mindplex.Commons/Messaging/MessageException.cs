
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
using System.Runtime.Serialization;
using System.Text;

using Mindplex.Commons.Exceptions;

#endregion

namespace Mindplex.Commons.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class MessageException : Exception, IGenericException
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public MessageException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public MessageException(string message)
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
        public MessageException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Constructs this exception with the specified serialization info and 
        /// streaming context.
        /// </summary>
        /// 
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        /// 
        public MessageException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
