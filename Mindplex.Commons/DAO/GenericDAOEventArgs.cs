
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

#endregion

namespace Mindplex.Commons.DAO
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class GenericDAOEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private object entity;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public GenericDAOEventArgs()
            : base()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entity"></param>
        /// 
        public GenericDAOEventArgs(object entity) 
            : base()
        {
            this.entity = entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public object Entity
        {
            get 
            { 
                return entity; 
            }
            set 
            { 
                entity = value; 
            }
        }
    }
}
