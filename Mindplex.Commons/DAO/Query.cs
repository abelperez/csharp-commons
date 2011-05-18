
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
using System.Reflection;
using System.Text;

using Mindplex.Commons.Model;

#endregion

namespace Mindplex.Commons.DAO
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// 
    public class Query<T, E> : GenericDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public Query()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public Query(string databaseInstance)
            : base(databaseInstance)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="TR"></typeparam>
        /// 
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public EntityCollection<TR> Join<TR>(RowMapper<TR> rowMapper)
        {
            return FindAll<TR>(GenerateJoinQuery(), rowMapper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string GenerateJoinQuery()
        {
            return string.Format("select {0} from {1} EntityA inner join {2} EntityB on EntityA.Id = EntityB.Id", GetJoinColumns(), typeof(T).Name, typeof(E).Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        private string GetJoinColumns()
        {
            StringBuilder query = new StringBuilder();

            PropertyInfo[] propertyInfoT = typeof(T).GetProperties();
            int length = propertyInfoT.Length;

            for (int i = 0; i < length; i++)
            {
                query.Append(string.Format(" EntityA.{0} as {1}{2},", propertyInfoT[i].Name, typeof(T).Name, propertyInfoT[i].Name));
            }

            PropertyInfo[] propertyInfoE = typeof(E).GetProperties();
            length = propertyInfoE.Length;

            for (int i = 0; i < length; i++)
            {
                query.Append(string.Format(" EntityB.{0} as {1}{2}", propertyInfoE[i].Name, typeof(E).Name, propertyInfoE[i].Name));

                if (i < length - 1)
                {
                    query.Append(",");
                }
            }

            return query.ToString();
        }
    }
}