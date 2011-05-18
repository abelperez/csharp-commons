
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
using System.Collections.Generic;
using System.Text;

using InternetBrands.Core.Model;
using InternetBrands.Core.Model.Test;
using InternetBrands.Core.DAO;
using InternetBrands.Core.DAO.Test;


using NUnit.Core;
using NUnit.Framework;

#endregion

namespace InternetBrands.Core.DAO.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    [TestFixture]
    public class DataAccessTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private RowMapper<TestEntity> rowMapper;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private DataAccess<TestEntity> dataAccess;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string database = "IB";

        /// <summary>
        /// 
        /// </summary>
        /// 
        [SetUp]
        public void SetUp()
        {
            dataAccess = new DataAccess<TestEntity>(database);
            rowMapper = new RowMapper<TestEntity>(TestEntityMapper.GetTestEntityEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [TearDown]
        public void TearDown()
        {
            dataAccess = null;
            rowMapper = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSelect()
        {
            TestEntity result = dataAccess.Select(1, rowMapper);

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSelectAll()
        {
            EntityCollection<TestEntity> result = dataAccess.SelectAll(rowMapper);

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.GetEntities());
            foreach (TestEntity entity in result)
            {
                Assert.IsNotNull(entity.Id);
                Console.WriteLine(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestInsert()
        {
            TestEntity entity = new TestEntity(1, "TestEntity", "Insert Test Case for DataAccess.");
            TestEntity result = dataAccess.Insert(entity, rowMapper);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Console.WriteLine(result);
        }

        [Test]
        public void TestDelete()
        {
            dataAccess.Delete(100);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestUpdate()
        {
            TestEntity entity = new TestEntity(1, "TestEntity", "Update Test Case for DataAccess.");
            dataAccess.Update(entity);

            TestEntity result = dataAccess.Select(1, rowMapper);

            Assert.IsNotNull(result);
            Assert.AreEqual("Update Test Case for DataAccess.", result.Description);
            Console.WriteLine(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestJoin()
        {
            RowMapper<string> mapper = new RowMapper<string>(TestEntityMapper.GetTestJoinEntity);
            dataAccess.Join<string, TestAccountEntity>(mapper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestGenericDataAccess()
        {
            TestEntity result = dataAccess.GenericDataAccess.Find(string.Format("select * from {0}", typeof(TestEntity).Name), rowMapper);

            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }
    }
}

#endif