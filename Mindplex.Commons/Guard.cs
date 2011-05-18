
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

#endregion

namespace InternetBrands.Core
{
    /// <summary>
    /// TODO: Add check for null/empty with overload for exception message 
    /// (in case we want to override the message in ArgumentException).
    /// </summary>
    ///
    /// <author>Abel Perez</author>
    /// 
    public sealed class Guard
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private Guard()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="target"></param>
        /// 
        public static void CheckForNull(object target)
        {
            if (null == target)
            {
                throw new ArgumentException("Argument is null.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="target"></param>
        /// 
        public static void CheckForNull(object target, string parameterName)
        {
            if (null == target)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="targets"></param>
        /// 
        public static void CheckForNull(params object[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentException("Arguments are null.");
            }

            foreach (object target in targets)
            {
                if (null == target)
                {
                    throw new ArgumentException("Argument is null.");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="target"></param>
        /// 
        public static void CheckIsNullOrEmpty(string target)
        {
            if (String.IsNullOrEmpty(target))
            {
                throw new ArgumentException("Argument is null.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="target"></param>
        /// 
        public static void CheckIsNullOrEmpty(string target, string parameterName)
        {
            if (String.IsNullOrEmpty(target))
            {
                throw new ArgumentException(string.Format("Argument {0} cannot be null or empty.", parameterName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="targets"></param>
        /// 
        public static void CheckIsNullOrEmpty(params string[] targets)
        {
            if (targets == null)
            {
                throw new ArgumentException("Arguments are null.");
            }

            foreach (string target in targets)
            {
                if (String.IsNullOrEmpty(target))
                {
                    throw new ArgumentException("Argument is null.");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="target"></param>
        /// 
        public static bool IsNull(object target)
        {
            return (null == target);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="target"></param>
        /// 
        public static bool IsNullOrEmpty(string target)
        {
            return String.IsNullOrEmpty(target);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="collection"></param>
        /// 
        /// <returns></returns>
        /// 
        public static bool IsEmpty(ICollection collection)
        {
            return !(null != collection && collection.Count > 0);
        }
    }
}