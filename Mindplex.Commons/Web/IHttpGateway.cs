
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

#endregion

namespace Mindplex.Commons.Web
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public interface IHttpGateway
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        string Verb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        int Timeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        string AuthMethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        string ContentType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        string[] Headers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="payload"></param>
        /// 
        /// <returns></returns>
        /// 
        HttpGatewayResponse Request(string payload);
    }
}
