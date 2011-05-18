
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

using log4net;
using log4net.Config;

using Mindplex.Commons.Test;

using NUnit.Framework;

#endregion

namespace Mindplex.Commons.Model.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class EntityCollectionTest : GenericTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestAffiliateEntityCollection()
        {
            TestEntity affiliate = new TestEntity();
            affiliate.Id = 1000;

            TestEntity affiliate0 = new TestEntity();
            affiliate0.Id = 1001;

            TestEntity affiliate1 = new TestEntity();
            affiliate1.Id = 1002;

            EntityCollection<TestEntity> collection = new EntityCollection<TestEntity>();
            collection.Add(affiliate);
            collection.Add(affiliate0);
            collection.Add(affiliate1);

            foreach (TestEntity aff in collection)
            {
                Console.WriteLine("Affiliate: {0}", aff.Id);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        [Test]
        public void TestDataViewEntityCollection()
        {
            TestEntity affiliate = new TestEntity();
            affiliate.Id = 1000;

            TestEntity affiliate0 = new TestEntity();
            affiliate0.Id = 1001;

            TestEntity affiliate1 = new TestEntity();
            affiliate1.Id = 1002;

            EntityCollection<TestEntity> collection = new EntityCollection<TestEntity>();
            collection.Add(affiliate);
            collection.Add(affiliate0);
            collection.Add(affiliate1);

            foreach (TestEntity aff in collection)
            {
                Console.WriteLine("Affiliate: {0}", aff.Id);
            }

            DataView view = collection.GetDataView(typeof(TestEntity));
            view.ToString();

        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        ///// <param name="collection"></param>
        ///// 
        ///// <returns></returns>
        ///// 
        private List<TestEntity> GetEntityCollection(List<TestEntity> collection)
        {
            return collection;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        [Test]
        public void TestEntityCollectionOperatorOverloading()
        {

            EntityCollection<TestEntity> collection = new EntityCollection<TestEntity>();
            TestEntity lead = new TestEntity();
            lead.Id = 1000;
            collection.Add(lead);

            EntityCollection<TestEntity> otherCollection = new EntityCollection<TestEntity>();
            TestEntity otherLead = new TestEntity();
            otherLead.Id = 2000;
            otherCollection.Add(otherLead);

            EntityCollection<TestEntity> finalCollection = collection + otherCollection;

            foreach (TestEntity tempLead in finalCollection)
            {
                Console.WriteLine("Lead: " + tempLead.Id);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        [Test]
        public void TestEntityCollectionMerge()
        {
            EntityCollection<TestEntity> collection = new EntityCollection<TestEntity>();
            TestEntity lead = new TestEntity();
            lead.Id = 1000;
            collection.Add(lead);

            EntityCollection<TestEntity> otherCollection = new EntityCollection<TestEntity>();
            TestEntity otherLead = new TestEntity();
            otherLead.Id = 2000;
            otherCollection.Add(otherLead);

            collection.Merge(otherCollection);

            foreach (TestEntity tempLead in collection)
            {
                Console.WriteLine("Lead: " + tempLead.Id);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        [Test]
        public void TestImplicitCast()
        {
            List<TestAccountEntity> listCollection = new List<TestAccountEntity>();
            listCollection.Add(new TestAccountEntity(100, 5));
            listCollection.Add(new TestAccountEntity(101, 15));
            listCollection.Add(new TestAccountEntity(102, 8));

            EntityCollection<TestAccountEntity> otherCollection = listCollection;

            Assert.IsNotNull(otherCollection);
            foreach (TestAccountEntity entity in otherCollection)
            {
                Console.WriteLine(entity);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        [Test]
        public void TestTakeOperator()
        {
            EntityCollection<TestAccountEntity> entities = new EntityCollection<TestAccountEntity>();
            entities.Add(new TestAccountEntity(100, 5));
            entities.Add(new TestAccountEntity(101, 15));
            entities.Add(new TestAccountEntity(102, 8));

            IEnumerable<TestAccountEntity> result = entities.Take(2);

            Assert.IsNotNull(result);
            foreach (TestAccountEntity entity in result)
            {
                Assert.IsNotNull(entity);
                Console.WriteLine(entity);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        [Test]
        public void TestSkipOperator()
        {
            EntityCollection<TestAccountEntity> entities = new EntityCollection<TestAccountEntity>();
            entities.Add(new TestAccountEntity(100, 5));
            entities.Add(new TestAccountEntity(101, 15));
            entities.Add(new TestAccountEntity(102, 8));

            IEnumerable<TestAccountEntity> result = entities.Skip(2);

            Assert.IsNotNull(result);
            foreach (TestAccountEntity entity in result)
            {
                Assert.IsNotNull(entity);
                Console.WriteLine(entity);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        [Test]
        public void TestTakeWhileOperator()
        {
            EntityCollection<TestAccountEntity> entities = new EntityCollection<TestAccountEntity>();
            entities.Add(new TestAccountEntity(100, 5));
            entities.Add(new TestAccountEntity(102, 8));
            entities.Add(new TestAccountEntity(101, 15));
            entities.Add(new TestAccountEntity(101, 2));

            IEnumerable<TestAccountEntity> result =
                    entities.TakeWhile(
                            delegate(TestAccountEntity entity)
                            {
                                return (entity.Amount < 10) ? true : false;
                            });

            Assert.IsNotNull(result);
            foreach (TestAccountEntity entity in result)
            {
                Assert.IsNotNull(entity);
                Console.WriteLine(entity);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        [Test]
        public void TestSkipWhileOperator()
        {
            EntityCollection<TestAccountEntity> entities = new EntityCollection<TestAccountEntity>();
            entities.Add(new TestAccountEntity(100, 5));
            entities.Add(new TestAccountEntity(102, 8));
            entities.Add(new TestAccountEntity(101, 15));
            entities.Add(new TestAccountEntity(101, 2));

            IEnumerable<TestAccountEntity> result =
                    entities.SkipWhile(
                            delegate(TestAccountEntity entity)
                            {
                                return (entity.Amount < 10) ? true : false;
                            });

            Assert.IsNotNull(result);
            foreach (TestAccountEntity entity in result)
            {
                Assert.IsNotNull(entity);
                Console.WriteLine(entity);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        [Test]
        public void TestConcatOperator()
        {
            EntityCollection<TestAccountEntity> entities = new EntityCollection<TestAccountEntity>();
            entities.Add(new TestAccountEntity(100, 5));
            entities.Add(new TestAccountEntity(102, 8));

            EntityCollection<TestAccountEntity> entities0 = new EntityCollection<TestAccountEntity>();
            entities0.Add(new TestAccountEntity(103, 15));
            entities0.Add(new TestAccountEntity(104, 2));

            IEnumerable<TestAccountEntity> result = entities.Concat(entities0);

            Assert.IsNotNull(result);
            foreach (TestAccountEntity entity in result)
            {
                Assert.IsNotNull(entity);
                Console.WriteLine(entity);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        [Test]
        public void TestFirstOperator()
        {
            EntityCollection<TestAccountEntity> entities = new EntityCollection<TestAccountEntity>();
            entities.Add(new TestAccountEntity(100, 5));
            entities.Add(new TestAccountEntity(102, 8));
            entities.Add(new TestAccountEntity(103, 15));
            entities.Add(new TestAccountEntity(104, 2));

            IEnumerable<TestAccountEntity> result =
                    entities.First(
                            delegate(TestAccountEntity entity)
                            {
                                return (entity.Amount > 8) ? true : false;
                            });

            Assert.IsNotNull(result);
            foreach (TestAccountEntity entity in result)
            {
                Assert.IsNotNull(entity);
                Console.WriteLine(entity);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// 
        [Test]
        public void TestOrderByOperator()
        {
            EntityCollection<TestAccountEntity> entities = new EntityCollection<TestAccountEntity>();
            entities.Add(new TestAccountEntity(100, 5));
            entities.Add(new TestAccountEntity(102, 8));
            entities.Add(new TestAccountEntity(103, 15));
            entities.Add(new TestAccountEntity(104, 2));

            IEnumerable<TestAccountEntity> result =
                    entities.OrderBy<long>(
                            delegate(TestAccountEntity entity)
                            {
                                return entity.Amount;
                            });

            Assert.IsNotNull(result);
            foreach (TestAccountEntity entity in result)
            {
                Assert.IsNotNull(entity);
                Console.WriteLine(entity);
            }
        }

        [Test]
        public void TestGetCSV()
        {
            TestEntity affiliate = new TestEntity();
            affiliate.Id = 1000;
            affiliate.Description = "desc 1";

            TestEntity affiliate0 = new TestEntity();
            affiliate0.Id = 1001;
            affiliate0.Description = "desc 2";

            TestEntity affiliate1 = new TestEntity();
            affiliate1.Id = 1002;
            affiliate1.Description = "desc 3";

            String[] stringArray = new string[] { "amin", "abel" };

            affiliate1.StringArray = stringArray;

            EntityCollection<TestEntity> collection = new EntityCollection<TestEntity>();
            collection.Add(affiliate);
            collection.Add(affiliate0);
            collection.Add(affiliate1);

            Console.Write(collection.GetCSV());
        }
    }
}

#endif
