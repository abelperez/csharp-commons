
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

using InternetBrands.Core.Model;
using InternetBrands.Core.DAO;

#endregion

namespace InternetBrands.Core.DAO
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <typeparam name="T"></typeparam>
    /// 
    public class DataAccess<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private StandardDAO<T> dao;

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="database"></param>
        /// 
        public DataAccess(string database)
        {
            dao = new StandardDAO<T>(database);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="id"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Select(long id, RowMapper<T> rowMapper)
        {
            return dao.FindEntityById(id, rowMapper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public EntityCollection<T> SelectAll(RowMapper<T> rowMapper)
        {
            return dao.FindEntities(rowMapper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entity"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Insert(T entity, RowMapper<T> rowMapper)
        {
            return dao.SaveEntity(entity, rowMapper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entity"></param>
        /// 
        public void Delete(long id)
        {
            dao.DeleteEntity(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entity"></param>
        /// 
        public void Update(T entity)
        {
            dao.UpdateEntity(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public GenericDAO GenericDataAccess
        {
            get  { return dao; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="TR"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public EntityCollection<TR> Join<TR, E>(RowMapper<TR> rowMapper)
        {
            return new InternalQuery<T, E>(GenericDataAccess).Join(rowMapper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        internal class StandardDAO<E> : GenericDAO
        {

            /// <summary>
            /// 
            /// </summary>
            /// 
            /// <param name="databaseInstance"></param>
            /// 
            public StandardDAO(string databaseInstance)
                : base(databaseInstance)
            {
            }

            /// <summary>
            /// 
            /// </summary>
            /// 
            /// <param name="id"></param>
            /// 
            /// <returns></returns>
            /// 
            public E FindEntityById(long id, RowMapper<E> rowMapper)
            {
                E result = default(E);

                try
                {
                    result = Find(string.Format("select * from {0} where id = {1}", typeof(E).Name, id), rowMapper);
                }
                catch (Exception exception)
                {
                    DAOExceptionHandler.Process(exception, "Failed to find entity {0} by id: {1}.", typeof(E).Name, id);
                }

                return result;
            }

            /// <summary>
            /// 
            /// </summary>
            /// 
            /// <param name="rowMapper"></param>
            /// 
            /// <returns></returns>
            /// 
            public EntityCollection<E> FindEntities(RowMapper<E> rowMapper)
            {
                EntityCollection<E> result = default(EntityCollection<E>);

                try
                {
                    result = FindAll(string.Format("select * from {0}", typeof(E).Name), rowMapper);
                }
                catch (Exception exception)
                {
                    DAOExceptionHandler.Process(exception, "Failed to find entities of type: {0}.", typeof(E).Name);
                }

                return result;
            }

            /// <summary>
            /// 
            /// </summary>
            /// 
            /// <param name="entity"></param>
            /// 
            /// <returns></returns>
            /// 
            public E SaveEntity(E entity, RowMapper<E> rowMapper)
            {
                E result = default(E);

                try
                {
                    result = Save(entity, rowMapper);
                }
                catch (Exception exception)
                {
                    DAOExceptionHandler.Process(exception, "Failed to save entity: {0}.", typeof(E).Name);
                }

                return result;
            }

            /// <summary>
            /// 
            /// </summary>
            /// 
            /// <param name="entity"></param>
            /// 
            public void DeleteEntity(long id)
            {
                try
                {
                    Delete<E>(id);
                }
                catch (Exception exception)
                {
                    DAOExceptionHandler.Process(exception, "Failed to delete entity: {0}.", typeof(E).Name);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// 
            /// <param name="entity"></param>
            /// 
            public void UpdateEntity(E entity)
            {
                try
                {
                    Update(entity);
                }
                catch (Exception exception)
                {
                    DAOExceptionHandler.Process(exception, "Failed to update entity: {0}.", typeof(E).Name);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// 
        internal class InternalQuery<K, E>
        {
            /// <summary>
            /// 
            /// </summary>
            /// 
            private GenericDAO dao;

            /// <summary>
            /// 
            /// </summary>
            /// 
            public InternalQuery(GenericDAO dao)
            {
                this.dao = dao;
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
                return dao.FindAll<TR>(GenerateJoinQuery(), rowMapper);
            }

            /// <summary>
            /// 
            /// </summary>
            /// 
            /// <returns></returns>
            /// 
            public string GenerateJoinQuery()
            {
                return string.Format("select {0} from {1} EntityA inner join {2} EntityB on EntityA.Id = EntityB.Id", GetJoinColumns(), typeof(K).Name, typeof(E).Name);
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

                PropertyInfo[] propertyInfoT = typeof(K).GetProperties();
                int length = propertyInfoT.Length;

                for (int i = 0; i < length; i++)
                {
                    query.Append(string.Format(" EntityA.{0} as {1}{2},", propertyInfoT[i].Name, typeof(K).Name, propertyInfoT[i].Name));
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
}