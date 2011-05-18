
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

namespace InternetBrands.Core.DAO
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <typeparam name="E"></typeparam>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class DAOEventArgs<E> : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private E entity;

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entity"></param>
        /// 
        public DAOEventArgs(E entity)
        {
            this.entity = entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public E Entity
        {
            get
            {
                return entity;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public override string ToString()
        {
            if (entity == null)
            {
                return base.ToString();
            }

            return "ServiceEvent for Entity = " + entity.ToString();
        }
    }
}
