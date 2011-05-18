
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

namespace InternetBrands.Core.DAO
{
    /// <summary>
    /// 
    /// </summary>
    ///                                              
    public class DAOExceptionFactory : IGenericExceptionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        /// <returns></returns>
        /// 
        public IGenericException Create(string message)
        {
            return new DAOException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// 
        /// <returns></returns>
        /// 
        public IGenericException Create(string message, Exception innerException)
        {
            return new DAOException(message, innerException);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        /// 
        /// <returns></returns>
        /// 
        public IGenericException Create(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            return new DAOException(serializationInfo, streamingContext);
        }
    }
}
