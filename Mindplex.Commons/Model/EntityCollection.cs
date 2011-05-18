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

// ==============================================================================
// EntityCollection.cs
// @author Abel Perez
// ==============================================================================

#region Imports

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

#endregion

namespace Mindplex.Commons.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="TR"></typeparam>
    /// <param name="t0"></param>
    /// 
    /// <returns></returns>
    /// 
    public delegate TR Func<T0, TR>(T0 t0);

    /// <summary>
    /// 
    /// </summary>
    ///  
    /// <typeparam name="T"></typeparam>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class EntityCollection<T> : IEnumerable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private List<T> entities = null;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public EntityCollection()
        {
             entities = new List<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entities"></param>
        /// 
        public EntityCollection(List<T> entities)
        {
            if (entities == null)
            {
                this.entities = new List<T>();
                return;
            }

            this.entities = new List<T>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="position"></param>
        /// 
        /// <returns></returns>
        /// 
        public T GetEntity(int position)
        {
            return entities[position];
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="index"></param>
        /// 
        /// <returns></returns>
        /// 
        public T this[int index]
        {
            get { return this.entities[index]; }
            set { this.entities[index] = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entity"></param>
        /// 
        public void Add(T entity)
        {
            if (entity != null)
            {
                entities.Add(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void ClearEntities()
        {
            entities.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public int Count()
        {
            return entities.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        IEnumerator IEnumerable.GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public List<T> GetEntities()
        {
            return new List<T>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entries"></param>
        /// 
        public void SetEntities(List<T> entities)
        {
            this.entities = new List<T>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        ///
        [Obsolete("see EntityCollection.GetEntities")]
        public List<T> GetEntries()
        {
            return new List<T>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entries"></param>
        /// 
        [Obsolete("see EntityCollection.SetEntities")]
        public void SetEntries(List<T> entries)
        {
            this.entities = new List<T>(entries);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (T entity in entities)
            {
                result.Append(entity);
                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }

        #region Restriction Operators

        /// <summary>
        /// When the object returned by Where is enumerated, it enumerates this entity 
        /// collection and yields those entities for which the predicate function returns true. 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// 
        /// <returns></returns>
        /// 
        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("Predicate is null.");
            }

            foreach (T t in this)
            {
                if (predicate(t))
                {
                    yield return t;
                }
            }
        }

        #endregion 

        #region  Partition Operators

        /// <summary>
        /// When the object returned by Take is enumerated, it enumerates this entity collection 
        /// and yields entities until the number of entities given by the count argument have 
        /// been yielded or the end of this entity collection is reached. If the count argument is less 
        /// than or equal to zero, this entity collection is not enumerated and no entities are yielded.
        /// </summary>
        /// 
        /// <remarks>
        /// The Take and Skip operators are functional complements: For a given entity 
        /// collection <c>entityCollection</c>, the concatenation of <c>entityCollection.Take(n)</c> 
        /// and <c>entityCollection.Skip(n)</c> yields the same entity collection as entityCollection.
        /// </remarks>
        /// 
        /// <param name="count"></param>
        /// 
        /// <returns></returns>
        /// 
        public IEnumerable<T> Take(int count)
        {
            if (count <= 0)
            {
                yield break;
            }

            int counter = 1;

            foreach (T t in this)
            {
                if (counter <= count)
                {
                    yield return t;
                    counter++;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// When the object returned by Skip is enumerated, it enumerates this entity collection, 
        /// skipping the number of entities given by the count argument and yielding the rest. 
        /// If this entity collection contains fewer elements than given by the count argument, 
        /// nothing is yielded. If the count argument is less than or equal to zero, all entities 
        /// of this entity collection are yielded.
        /// </summary>
        /// 
        /// <remarks>
        /// The Take and Skip operators are functional complements: For a given entity 
        /// collection <c>entityCollection</c>, the concatenation of <c>entityCollection.Take(n)</c> 
        /// and <c>entityCollection.Skip(n)</c> yields the same entity collection as <c>entityCollection</c>.
        /// </remarks>
        /// 
        /// <param name="count"></param>
        /// 
        /// <returns></returns>
        /// 
        public IEnumerable<T> Skip(int count)
        {
            if (Count() < count)
            {
                yield break;
            }

            int counter = 1;

            foreach (T t in this)
            {
                if (counter > count)
                {
                    yield return t;
                }

                counter++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="predicate"></param>
        /// 
        /// <returns></returns>
        /// 
        public IEnumerable<T> TakeWhile(Func<T, bool> predicate)
        {
            foreach (T t in this)
            {
                if (predicate(t))
                {
                    yield return t;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="predicate"></param>
        /// 
        /// <returns></returns>
        /// 
        public IEnumerable<T> SkipWhile(Func<T, bool> predicate)
        {
            bool reached = false;

            foreach (T t in this)
            {
                if (!predicate(t))
                {
                    reached = true;
                }

                if (reached)
                {
                    yield return t;
                }
            }
        }

        #endregion

        #region Concatenation Operators

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// 
        /// <returns></returns>
        /// 
        public IEnumerable<T> Concat(IEnumerable<T> entities)
        {
            foreach (T t in this)
            {
                yield return t;
            }

            foreach (T t in entities)
            {
                yield return t;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entityCollection"></param>
        /// 
        public void Merge(EntityCollection<T> entityCollection)
        {
            foreach (T entity in entityCollection)
            {
                entities.Add(entity);
            }
        }

        #endregion

        #region Ordering Operators

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public IEnumerable<T> Reverse()
        {
            List<T> result = new List<T>(entities);
            result.Reverse();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="K"></typeparam>
        /// <param name="keySelector"></param>
        /// 
        /// <returns></returns>
        /// 
        public IEnumerable<T> OrderBy<K>(Func<T, K> keySelector)
        {
            Dictionary<K, T> order = new Dictionary<K, T>();

            foreach (T t in this)
            {
                K key = keySelector(t);
                if (order.ContainsKey(key))
                {
                    continue;
                }
                order.Add(key, t);
            }

            List<K> keys = new List<K>(order.Keys);
            keys.Sort();

            foreach (K k in keys)
            {
                T t = order[k];
                yield return t;
            }
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public T[] ToArray()
        {
            return entities.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public List<T> ToList()
        {
            return new List<T>(entities);
        }

        #endregion

        #region Element Operators

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="predicate"></param>
        /// 
        /// <returns></returns>
        /// 
        public IEnumerable<T> First(Func<T, bool> predicate)
        {
            foreach (T t in this)
            {
                if (predicate(t))
                {
                    yield return t;
                    break;
                }
            }
        }

        #endregion

        #region Overloaded Operators

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entityCollection"></param>
        /// <param name="otherEntityCollection"></param>
        /// 
        /// <returns></returns>
        /// 
        public static EntityCollection<T> operator +(EntityCollection<T> entityCollection, EntityCollection<T> otherEntityCollection)
        {
            EntityCollection<T> result = new EntityCollection<T>();

            foreach (T entity in entityCollection)
            {
                result.Add(entity);
            }

            foreach (T otherEntity in otherEntityCollection)
            {
                result.Add(otherEntity);
            }

            return result;
        }

        #endregion

        #region Implicit Type Conversions

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="entityList"></param>
        /// 
        /// <returns></returns>
        /// 
        public static implicit operator EntityCollection<T>(List<T> entityList)
        {
            return new EntityCollection<T>(new List<T>(entityList));
        }

        #endregion

        #region Data View

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="type"></param>
        /// 
        /// <returns></returns>
        /// 
        [Obsolete("This method will soon be moved to a different class.")]
        public DataView GetDataView(Type type)
        {
            DataTable dataTable = new DataTable();
            FieldInfo[] fieldInfos = type.GetFields();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                dataTable.Columns.Add(new DataColumn(fieldInfo.Name, typeof(String)));
            }

            int rowIndex = 0;
            foreach (Object candidate in entities)
            {
                FieldInfo[] fields = candidate.GetType().GetFields();

                int columnIndex = 0;
                DataRow dataRow = dataTable.NewRow();
                foreach (FieldInfo field in fields)
                {
                    dataRow[columnIndex] = field.GetValue(candidate);
                    columnIndex++;
                }
                dataTable.Rows.Add(dataRow);
                columnIndex = 0;
                rowIndex++;
            }

            DataView dataView = new DataView(dataTable);
            return dataView;
        }

        #endregion

        #region CSV
        public String GetCSV()
        {
            StringBuilder csv = null;

            try
            {
                if (this.Count() > 0)
                {
                    csv = new StringBuilder();

                    PropertyInfo[] propertyInfos = this[0].GetType().GetProperties();

                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        csv.Append(propertyInfo.Name).Append(",");
                    }
                    csv.Append(Environment.NewLine);

                    foreach (Object candidate in this)
                    {
                        PropertyInfo[] candidatePropertyInfo = 
                            candidate.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                        foreach (PropertyInfo propertyInfo in candidatePropertyInfo)
                        {
                            csv.Append(Convert.ToString(propertyInfo.GetValue(candidate, null))).Append(",");
                        }
                        csv.Append(Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return csv.ToString();
        }
        #endregion
    }
}