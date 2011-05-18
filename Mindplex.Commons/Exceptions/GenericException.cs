
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

namespace Mindplex.Commons.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class GenericException : Exception, IGenericException
    {
        /// <summary>
        /// Simple constructor.
        /// </summary>
        /// 
        public GenericException()
            : base()
        {
        }

        /// <summary>
        /// Constructs this exception with the specified error message.
        /// </summary>
        /// 
        /// <param name="message">the error message that describes this exception.</param>
        /// 
        public GenericException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructs this exception with the specified error message and
        /// root exception that caused the error this exception encapsulates.
        /// </summary>
        /// 
        /// <param name="message">the error message that describes this exception.</param>
        /// <param name="exception">the root exception this exception encapsulates.</param>
        /// 
        public GenericException(string message, Exception exception)
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
        public GenericException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
