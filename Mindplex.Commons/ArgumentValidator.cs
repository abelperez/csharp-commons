
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

namespace Mindplex.Commons
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez</author>
    /// 
    [Obsolete("ArgumentValidator is obsolete, please use InternetBrands.Core.Guard")]
    public sealed class ArgumentValidator
    {
        /// <summary>
        /// Prevent direct instantiation.
        /// </summary>
        /// 
        private ArgumentValidator()
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

        public static void Main(string[] args)
        {
        }
    }
}
