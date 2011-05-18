
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

using InternetBrands.Core.Exceptions;

#endregion

namespace Mindplex.Commons.Mail
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class EmailGatewayException : Exception, IGenericException
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public EmailGatewayException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public EmailGatewayException(string message)
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
        public EmailGatewayException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
