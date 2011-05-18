
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
    public class CapturedExceptionCollection<T> : EntityCollection<T> where T : CapturedException
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string TimeStamp
        {
            get
            {
                return DateTime.UtcNow.ToString(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string ThreadIdentity
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.Name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string AppDomainName
        {
            get
            {
                return AppDomain.CurrentDomain.FriendlyName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string AssemblyFullName
        {
            get
            {
                return Assembly.GetExecutingAssembly().FullName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string MachineName
        {
            get
            {
                string machineName = String.Empty;
                try
                {
                    machineName = Environment.MachineName;
                }
                catch (SecurityException)
                {
                    machineName = PermissionDenied;
                }

                return machineName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string WindowsIdentity
        {
            get
            {
                string windowsIdentity = String.Empty;
                try
                {
                    windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                }
                catch (SecurityException)
                {
                    windowsIdentity = PermissionDenied;
                }

                return windowsIdentity;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        internal static string PermissionDenied
        {
            get
            {
                return "Permission Denied";
            }
        }
    }
}
