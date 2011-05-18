
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
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

#endregion

namespace Mindplex.Commons.Web
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public sealed class CertificatePolicy : ICertificatePolicy
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="srvPoint"></param>
        /// <param name="certificate"></param>
        /// <param name="request"></param>
        /// <param name="certificateProblem"></param>
        /// 
        /// <returns></returns>
        /// 
        public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem)
        {
            return true;
        }
    }
}
