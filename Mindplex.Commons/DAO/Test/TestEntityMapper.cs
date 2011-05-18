
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

#if UNIT_TEST

#region Imports

using System;
using System.Data;
using System.Data.SqlClient;

using Mindplex.Commons.Model.Test;

#endregion

namespace Mindplex.Commons.DAO.Test
{
    /// <summary>
    ///
    /// </summary>
    ///
    public partial class TestEntityMapper
    {
        /// <summary>
        ///
        /// </summary>
        ///
        /// <param name="reader"></param>
        ///
        /// <returns></returns>
        ///
        public static TestEntity GetTestEntityEntity(IDataReader reader)
        {
            TestEntity entity = new TestEntity();
            entity.Id = Convert.ToInt64(reader["Id"]);
            entity.Name = Convert.ToString(reader["Name"]);
            entity.Description = Convert.ToString(reader["Description"]);
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="reader"></param>
        /// 
        /// <returns></returns>
        /// 
        public static string GetTestJoinEntity(IDataReader reader)
        {
            return "";
        }
    }
}

#endif