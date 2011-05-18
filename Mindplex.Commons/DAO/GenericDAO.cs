
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
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Transactions;
using System.Xml;

using InternetBrands.Core.Model;
using InternetBrands.Core.DAO;
using InternetBrands.Core.Common;
using InternetBrands.Core.Instrumentation;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

#endregion

namespace InternetBrands.Core.DAO
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <param name="sender"></param>
    /// <param name="args"></param>
    /// 
    public delegate void GenericDAOEventHandler(object sender, GenericDAOEventArgs args);

    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <typeparam name="T"></typeparam>
    /// <param name="reader"></param>
    /// 
    /// <returns></returns>
    /// 
    public delegate T RowMapper<T>(IDataReader reader);

    /// <summary>
    /// Generic DAO that provides most of the functionality required by a
    /// concrete DAO implementation.
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// <author>Amin Bandeali (java.aperez@gmail.com)</author>
    /// 
    public abstract class GenericDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public event GenericDAOEventHandler OnEntitySaved;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public event GenericDAOEventHandler OnEntityUpdated;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public event GenericDAOEventHandler OnEntityDeleted;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private Dictionary<string, bool> cache = new Dictionary<string, bool>();

        /// <summary>
        /// Default <c>Database</c> instance that provides database connectivity.
        /// </summary>
        /// 
        private Database database;

        /// <summary>
        /// Default database instance name this DAO will use.
        /// </summary>
        /// 
        private string databaseInstance;

        /// <summary>
        /// Simple constructor for Generic DAO.
        /// </summary>
        /// 
        public GenericDAO()
        {
        }

        /// <summary>
        /// Constructors this DAO with the specified database instance name.
        /// </summary>
        /// 
        public GenericDAO(string dbInstance)
        {
            if (dbInstance == null)
            {
                DAOExceptionHandler.Process(null, "Failed to construct DAO.  Specified database instance name is null.");
            }
            this.databaseInstance = dbInstance;
            this.init();
        }

        /// <summary>
        /// Initializes this DAO by setting up default database and transactional attribute cache.
        /// </summary>
        /// 
        public void init()
        {
            database = DatabaseFactory.CreateDatabase(databaseInstance);            
            ProcessAttributes();

            OnEntitySaved += new GenericDAOEventHandler(EmptyOnEntitySaved);
            OnEntityUpdated += new GenericDAOEventHandler(EmptyOnEntityUpdated);
            OnEntityDeleted += new GenericDAOEventHandler(EmptyOnEntityDeleted);

            DAOInstrumentation.FireInfoEvent("Initialized {0} with connection string: {1}", GetType(), databaseInstance); 
        }

        #region GetSingleEntity

        /// <summary>
        /// Gets a single entity from the specified data reader based
        /// on the given row mapper type.  If no entity is found this method
        /// returns null.
        /// </summary>
        /// 
        /// <typeparam name="T">the entity row mapper type.</typeparam>
        /// 
        /// <param name="reader">the data reader that holds a set of entities.</param>
        /// <param name="mapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public T GetSingleEntity<T>(IDataReader reader, RowMapper<T> rowMapper)
        {
            if (reader == null || rowMapper == null)
            {
                DAOExceptionHandler.Process(null, "Failed to find single entity (reader/rowMapper is null).");
            }

            while (reader.Read())
            {
                return rowMapper(reader);
            }

            // Note: changed by Abel.
            DAOExceptionHandler.Process(null, "Failed to find single entity from reader.");
            return default(T);
        }

        #endregion

        #region GetEntities

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="reader"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public EntityCollection<T> GetEntities<T>(IDataReader reader, RowMapper<T> rowMapper)
        {
            if (reader == null || rowMapper == null)
            {
                DAOExceptionHandler.Process(null, "Failed to find entities (reader/rowMapper is null).");
            }

            EntityCollection<T> results = new EntityCollection<T>();

            while (reader.Read())
            {
                T result = rowMapper(reader);
                results.Add(result);
            }

            return results;
        }

        #endregion

        #region Find

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="query"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Find<T>(string query, RowMapper<T> rowMapper)
        {
            T result = default(T);

            try
            {
                using (IDataReader reader = GetQueryDataReader(query))
                {
                    result = GetSingleEntity<T>(reader, rowMapper);
                }
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to get {0} by Id.", typeof(T));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="query"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Find<T>(string sProcName, SqlParameter[] sqlParams, RowMapper<T> rowMapper)
        {
            T result = default(T);

            try
            {
                using (IDataReader reader = GetStoredProcedureDataReader(sProcName, sqlParams))
                {
                    return GetSingleEntity<T>(reader, rowMapper);
                }
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to get {0} by Id.", typeof(T));
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="method"></param>
        /// <param name="query"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Find<T>(string method, string query, RowMapper<T> rowMapper)
        {
            T result = default(T);

            try
            {
                if (cache.ContainsKey(method))
                {
                    bool required;
                    if (cache.TryGetValue(method, out required))
                    {
                        if (required)
                        {
                            using (IDataReader reader = GetTransactionalQueryDataReader(query))
                            {
                                // Executing in transaction mode.
                                return GetSingleEntity<T>(reader, rowMapper);
                            }
                        }
                    }
                }

                using (IDataReader reader = GetQueryDataReader(query))
                {
                    // Executing in non-transaction mode.
                    result = GetSingleEntity<T>(reader, rowMapper);
                }
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to get {0} by Id.", typeof(T));
            }

            return result;
        }

        #endregion

        #region FindAll

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="query"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public EntityCollection<T> FindAll<T>(string query, RowMapper<T> rowMapper)
        {
            EntityCollection<T> result = null;

            try
            {
                using (IDataReader reader = GetQueryDataReader(query))
                {
                    result = GetEntities<T>(reader, rowMapper);
                }
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to find any {0} entities.", typeof(T)); 
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="sqlParameters"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public EntityCollection<T> FindAll<T>(string query, SqlParameter[] sqlParameters, RowMapper<T> rowMapper)
        {
            EntityCollection<T> result = null;

            try
            {
                using (IDataReader reader = GetStoredProcedureDataReader(query, sqlParameters))
                {
                    result = GetEntities<T>(reader, rowMapper);
                }
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to find any {0} entities.", typeof(T)); 
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="method"></param>
        /// <param name="query"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public EntityCollection<T> FindAll<T>(string method, string query, RowMapper<T> rowMapper)
        {
            EntityCollection<T> result = null;

            try
            {
                if (cache.ContainsKey(method))
                {
                    bool required;
                    if (cache.TryGetValue(method, out required))
                    {
                        if (required)
                        {
                            using (IDataReader reader = GetTransactionalQueryDataReader(query))
                            {
                                // Executing in transaction mode.
                                return GetEntities<T>(reader, rowMapper);
                            }
                        }
                    }
                }

                using (IDataReader reader = this.GetQueryDataReader(query))
                {
                    // Executing in non-transaction mode.
                    result = GetEntities<T>(reader, rowMapper);
                }
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to find any {0} entities.", typeof(T));
            }

            return result;
        }

        #endregion


        #region Bulk Insert

        public void BulkInsert(string tableName, DataTable data)
        {
            try
            {
                this.BulkInsert(tableName, data, ConfigurationManager.ConnectionStrings[this.databaseInstance].ConnectionString);
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed bulk save data into table {0}", tableName);
            }
        }


        public void BulkInsert(string tableName, DataTable data, string connectionString)
        {
            try
            {
                using (SqlBulkCopy copy = new SqlBulkCopy(connectionString))
                {                    
                    copy.DestinationTableName = tableName;
                    copy.BatchSize = 100;
                    copy.WriteToServer(data);
                }
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed bulk save data into table {0}", tableName);
            }
        }


        #endregion


        #region Save

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="query"></param>
        /// <param name="sqlParameters"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Save<T>(string query, SqlParameter[] sqlParameters, RowMapper<T> rowMapper)
        {
            T result = default(T);

            try
            {
                using (IDataReader reader = GetStoredProcedureDataReader(query, sqlParameters))
                {
                    result = GetSingleEntity(reader, rowMapper);
                    OnEntitySaved(this, new GenericDAOEventArgs(result));
                }
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to save {0} entity.", typeof(T));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="sqlParameters"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Save<T>(T entity, SqlParameter[] sqlParameters, RowMapper<T> rowMapper)
        {
            return Save<T>(string.Format("Save{0}", entity.GetType().Name), sqlParameters, rowMapper);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Save<T>(T entity, RowMapper<T> rowMapper)
        {
            return Save<T>(string.Format("Save{0}", entity.GetType().Name), GetSqlParameters(entity), rowMapper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// 
        /// <returns></returns>
        /// 
        public T Save<T>(T entity)
        {
            return Save<T>(string.Format("Save{0}", entity.GetType().Name), GetSqlParameters(entity), GetRowMapper<T>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="storedProcOrQuery"></param>
        /// 
        /// <param name="sqlParameters"></param>
        /// 
        public void Save<T>(string storedProcOrQuery, SqlParameter[] sqlParameters)
        {
            try
            {
                DbCommand command = GetStoredProcedureCommand(storedProcOrQuery);

                foreach (SqlParameter sqlParameter in sqlParameters)
                {
                    database.AddInParameter(command, sqlParameter.ParameterName, sqlParameter.DbType, sqlParameter.Value);
                }
                database.ExecuteNonQuery(command);
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to Save entity {0}.", typeof(T));
            }

        }

		#endregion

		#region Update

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="query"></param>
        /// <param name="sqlParameters"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public void Update<T>(string query, SqlParameter[] sqlParameters)
        {
            try
            {
                ExecuteTransactionalStoredProcedure(query, sqlParameters);
                OnEntityUpdated(this, new GenericDAOEventArgs(null));
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to Update entity {0}.", typeof(T));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="sqlParameters"></param>
        /// 
        public void Update<T>(T entity, SqlParameter[] sqlParameters)
        {
            try
            {
                ExecuteTransactionalStoredProcedure(string.Format("Update{0}", entity.GetType().Name), sqlParameters);
                OnEntityUpdated(this, new GenericDAOEventArgs(entity));
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to Update entity {0}.", typeof(T));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="entity"></param>
        /// 
        public void Update<T>(T entity)
        {
            try
            {
                ExecuteTransactionalStoredProcedure(string.Format("Update{0}", entity.GetType().Name), GetSqlParameters(entity));
                OnEntityUpdated(this, new GenericDAOEventArgs(entity));
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to Update entity {0}.", typeof(T));
            }
        }

		#endregion

		#region Delete

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="query"></param>
        /// <param name="sqlParameters"></param>
        /// <param name="rowMapper"></param>
        /// 
        /// <returns></returns>
        /// 
        public void Delete<T>(string query, SqlParameter[] sqlParameters)
        {
            try
            {
                ExecuteTransactionalStoredProcedure(query, sqlParameters);
                OnEntityDeleted(this, new GenericDAOEventArgs(null));
                DAOInstrumentation.FireInfoEvent("Successfully executed {0}.", query);
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to Delete entity {0}.", typeof(T));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="sqlParameters"></param>
        /// 
        public void Delete<T>(T entity, SqlParameter[] sqlParameters)
        {
            try
            {
                ExecuteTransactionalStoredProcedure(string.Format("Delete{0}", entity.GetType().Name), sqlParameters);
                OnEntityDeleted(this, new GenericDAOEventArgs(entity));
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to Delete entity {0}.", typeof(T));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// 
        public void Delete<T>(T entity, long id)
        {
            Delete<T>(string.Format("Delete{0}", entity.GetType().Name), new SqlParameter[] { new SqlParameter("@Id", id) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// 
        public void Delete<T>(long id)
        {
            Delete<T>(string.Format("Delete{0}", typeof(T).Name), new SqlParameter[] { new SqlParameter("@Id", id) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="entity"></param>
        /// 
        public void Delete<T>(T entity)
        {
            int id = Convert.ToInt32(entity.GetType().GetProperty("Id").GetValue(entity, null));
            Delete<T>(string.Format("Delete{0}", entity.GetType().Name), new SqlParameter[] { new SqlParameter("@Id", id) });
        }

        #endregion

        #region GetSqlParameters

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// 
        /// <returns></returns>
        /// 
        protected SqlParameter[] GetSqlParameters<T>(T entity)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
            {
                parameters.Add(new SqlParameter(string.Format("@{0}", propertyInfo.Name), propertyInfo.GetValue(entity, null)));
            }

            return parameters.ToArray();
        }

        #endregion

        #region GetRowMapper

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <returns></returns>
        /// 
        protected virtual RowMapper<T> GetRowMapper<T>()
        {
            throw new DAOException("Operation not supported.");
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        protected void FireInfoEvent(string message, params object[] parameters)
        {
            DAOInstrumentation.FireInfoEvent(message, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        protected void FireWarningEvent(string message, params object[] parameters)
        {
            DAOInstrumentation.FireWarningEvent(message, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        protected void FireErrorEvent(string message, params object[] parameters)
        {
            DAOInstrumentation.FireErrorEvent(message, parameters);
        }

        #region GetQueryCommand

        /// <summary>
        /// Get's database command wrapper based on the specified sql query.
        /// </summary>
        /// 
        /// <param name="query">the sql query to set on the database command wrapper.</param>
        /// <returns>database command wrapper based on the specified sql query.</returns>
        /// 
        protected DbCommand GetQueryCommand(string query)
        {
            return database.GetSqlStringCommand(query);
        }

        #endregion

        #region GetQueryDataReader

        /// <summary>
        /// Get's data reader populated with the results of the specified sql query.
        /// </summary>
        /// 
        /// <param name="query">the sql query to populate the result data reader with.</param>
        /// <returns>data reader populated with the results of the specified sql query.</returns>
        /// 
        protected IDataReader GetQueryDataReader(string query)
        {
            DbCommand command = GetQueryCommand(query);
            return database.ExecuteReader(command);
        }

        /// <summary>
        /// Get's data reader populated with the results of the specified sql query.
        /// </summary>
        /// 
        /// <param name="query">the sql query to populate the result data reader with.</param>
        /// <param name="commandTimeOut">time out for the completion of query or store procedure</param>
        /// <returns>data reader populated with the results of the specified sql query.</returns>
        /// 
        protected IDataReader GetQueryDataReader(string query, int commandTimeOut)
        {
            DbCommand command = GetQueryCommand(query);
            command.CommandTimeout = commandTimeOut;
            return database.ExecuteReader(command);
        }

        #endregion

        #region GetQueryDataset

        /// <summary>
        /// Get's dataset populated with the results of the specified sql query.
        /// </summary>
        /// 
        /// <param name="query">the sql query to populate the result dataset with.</param>
        /// <returns>dataset populated with the results of the specified sql query.</returns>
        /// 
        protected DataSet GetQueryDataset(string query)
        {
            DbCommand command = GetQueryCommand(query);
            return database.ExecuteDataSet(command);
        }

        /// <summary>
        /// Get's dataset populated with the results of the specified sql query.
        /// </summary>
        /// 
        /// <param name="query">the sql query to populate the result dataset with.</param>
        /// <param name="timeOut">time out for the completion of query or store procedure</param>
        /// <returns>dataset populated with the results of the specified sql query.</returns>
        /// 
        protected DataSet GetQueryDataset(string query, int timeOut)
        {
            DbCommand command = GetQueryCommand(query);
            command.CommandTimeout = timeOut;
            return database.ExecuteDataSet(command);
        }

        #endregion

        #region GetTransactionalQueryDataReader

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="query"></param>
        /// 
        /// <returns></returns>
        /// 
        protected IDataReader GetTransactionalQueryDataReader(string query)
        {
            IDataReader result = null;
            DbCommand command = this.GetQueryCommand(query);

            using (DbConnection connection = this.database.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    result = database.ExecuteReader(command, transaction);
                    transaction.Commit();
                    return result;
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    DAOExceptionHandler.Process(exception, "Failed to execute transaction [{0}]", query);
                    return result;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion

        #region GetStoredProcedureCommand

        /// <summary>
        /// Get's database command wrapper based on the specified stored procedure name.
        /// </summary>
        /// 
        /// <note>
        /// Replaced DBCommandWrapper with DBCommand
        /// </note>
        /// 
        /// <param name="storedProcedureName">the stored procedure name to set on the database command wrapper.</param>
        /// <returns>database command wrapper based on the specified stored procedure name.</returns>
        /// 
        protected DbCommand GetStoredProcedureCommand(string storedProcedureName)
        {
            return database.GetStoredProcCommand(storedProcedureName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// 
        /// <returns></returns>
        /// 
        protected DbCommand GetStoredProcedureCommand(string storedProcedureName, SqlParameter[] parameters)
        {
            return database.GetStoredProcCommand(storedProcedureName, parameters);
        }

        #endregion

        #region GetStoredProcedureDataReader

        /// <summary>
        /// Get's data reader populated with the results of the specified stored procedure name
        /// and parameters.
        /// </summary>
        /// 
        /// <param name="storedProcedureName">the stored procedure name to set on the data reader.</param>
        /// <param name="sqlParameters">an array of sql parmeters for the specified stored procedure.</param>
        /// <returns>data reader populated with the results of the specified stored procedure name 
        /// and parameters.</returns>
        /// 
        protected IDataReader GetStoredProcedureDataReader(string storedProcedureName, SqlParameter[] sqlParameters)
        {
            DbCommand command = GetStoredProcedureCommand(storedProcedureName);

            foreach (SqlParameter sqlParameter in sqlParameters)
            {
                database.AddInParameter(command, sqlParameter.ParameterName, sqlParameter.DbType, sqlParameter.Value);
            }

            return database.ExecuteReader(command);
        }

        /// <summary>
        /// Get's data reader populated with the results of the specified stored procedure name
        /// and parameters.
        /// </summary>
        /// 
        /// <param name="storedProcedureName">the stored procedure name to set on the data reader.</param>
        /// <param name="sqlParameters">an array of sql parmeters for the specified stored procedure.</param>
        /// <param name="timeOut">time out for the completion of query or store procedure</param>
        /// <returns>data reader populated with the results of the specified stored procedure name 
        /// and parameters.</returns>
        /// 
        protected IDataReader GetStoredProcedureDataReader(string storedProcedureName, SqlParameter[] sqlParameters, int timeOut)
        {
            DbCommand command = GetStoredProcedureCommand(storedProcedureName);
            command.CommandTimeout = timeOut;

            foreach (SqlParameter sqlParameter in sqlParameters)
            {
                database.AddInParameter(command, sqlParameter.ParameterName, sqlParameter.DbType, sqlParameter.Value);
            }

            return this.database.ExecuteReader(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="timeOut"></param>
        /// 
        /// <returns></returns>
        /// 
        protected IDataReader GetStoredProcedureDataReader(string storedProcedureName, int timeOut, SqlParameter[] parameters)
        {
            DbCommand command = GetStoredProcedureCommand(storedProcedureName, parameters);
            command.CommandTimeout = timeOut;
            return database.ExecuteReader(command);
        }

        #endregion

        #region GetStoredProcedureDataSet

        /// <summary>
        /// Returns an instance of DataSet with data returned by the store procedure
        /// </summary>
        /// 
        /// <param name="storedProcedureName">Store Procedure Name</param>
        /// <param name="sqlParameters">An array of SQL parmeters for the Store Procedure</param>
        /// <returns>DataSet with Data</returns>
        /// 
        protected DataSet GetStoredProcedureDataSet(string storedProcedureName, SqlParameter[] sqlParameters)
        {
            DbCommand command = GetStoredProcedureCommand(storedProcedureName);

            foreach (SqlParameter sqlParameter in sqlParameters)
            {
                database.AddInParameter(command, sqlParameter.ParameterName, sqlParameter.DbType, sqlParameter.Value);
            }

            return database.ExecuteDataSet(command);
        }

        /// <summary>
        /// Returns an instance of DataSet with data returned by the store procedure
        /// </summary>
        /// 
        /// <param name="storedProcedureName">Store Procedure Name</param>
        /// <param name="sqlParameters">An array of SQL parmeters for the Store Procedure</param>
        /// <param name="timeOut">time out for the completion of query or store procedure</param>
        /// <returns>DataSet with Data</returns>
        /// 
        protected DataSet GetStoredProcedureDataSet(string storedProcedureName, SqlParameter[] sqlParameters, int timeOut)
        {
            DbCommand command = GetStoredProcedureCommand(storedProcedureName);
            command.CommandTimeout = timeOut;

            foreach (SqlParameter sqlParameter in sqlParameters)
            {
                database.AddInParameter(command, sqlParameter.ParameterName, sqlParameter.DbType, sqlParameter.Value);
            }

            return database.ExecuteDataSet(command);
        }

        #endregion

        #region GetScalarStoredProcedure

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="storedProcedureName"></param>
        /// <param name="sqlParameters"></param>
        /// 
        /// <returns></returns>
        /// 
        protected Object GetScalarStoredProcedure(string storedProcedureName, SqlParameter[] sqlParameters)
        {
            DbCommand command = GetStoredProcedureCommand(storedProcedureName);

            foreach (SqlParameter parameter in sqlParameters)
            {
                database.AddInParameter(command, parameter.ParameterName, parameter.DbType, parameter.Value);
            }

            return database.ExecuteScalar(command);
        }

        #endregion

        #region GetStoredProcedureDataset

        /// <summary>
        /// Returns an instance of DataSet with data returned by the store procedure
        /// </summary>
        /// 
        /// <param name="storedProcedureName">Store Procedure Name</param>
        /// <param name="sqlParameters">An array of SQL parmeters for the Store Procedure</param>
        /// <param name="timeOut">time out for the completion of query or store procedure</param>
        /// <returns>DataSet with Data</returns>
        /// 
        protected DataSet GetStoredProcedureDataset(string storedProcedureName, SqlParameter[] sqlParameters, int timeOut)
        {
            DbCommand command = GetStoredProcedureCommand(storedProcedureName);
            command.CommandTimeout = timeOut;

            foreach (SqlParameter sqlParameter in sqlParameters)
            {
                database.AddInParameter(command, sqlParameter.ParameterName, sqlParameter.DbType, sqlParameter.Value);
            }

            return database.ExecuteDataSet(command);
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="query"></param>
        /// 
        protected void ExecuteNonQuery(string query)
        {
            DbCommand command = GetQueryCommand(query);
            database.ExecuteNonQuery(command);
        }

        #endregion

        #region ExecuteTransactionalQuery

        /// <summary>
        /// Executes a Query in a transaction
        /// </summary>
        /// 
        /// <param name="query">SQL Query</param>
        /// <returns>A bool with a Success for a Failiure flag</returns>
        /// 
        protected bool ExecuteTransactionalQuery(string query)
        {
            DbCommand command = GetQueryCommand(query);

            using (DbConnection connection = database.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    database.ExecuteNonQuery(command, transaction);
                    transaction.Commit();
                    return true;
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw exception;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="query"></param>
        /// <param name="transaction"></param>
        /// 
        /// <returns></returns>
        /// 
        protected bool ExecuteTransactionalQuery(string query, DbTransaction transaction)
        {
            DbCommand command = this.GetQueryCommand(query);
            database.ExecuteNonQuery(command, transaction);
            return true;
        }


        /// <summary>
        /// Executes a Query in a transaction
        /// </summary>
        /// 
        /// <param name="query">SQL Query</param>
        /// <param name="timeOut">time out for the completion of query or store procedure</param>
        /// <returns>A bool with a Success for a Failiure flag</returns>
        /// 
        protected bool ExecuteTransactionalQuery(string query, int timeOut)
        {
            DbCommand command = GetQueryCommand(query);
            command.CommandTimeout = timeOut;
            using (DbConnection connection = database.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                database.ExecuteNonQuery(command, transaction);
                transaction.Commit();

                return true;
            }
        }

        #endregion

        #region ExecuteTransactionalQueries

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="commands"></param>
        /// 
        /// <returns></returns>
        /// 
        protected bool ExecuteTransactionalQueries(DbCommand[] commands)
        {
            using (DbConnection connection = database.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (DbCommand command in commands)
                    {
                        database.ExecuteNonQuery(command, transaction);
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw exception;
                }
            }
        }

        #endregion

        #region ExecuteTransactionalStoredProcedure

        /// <summary>
        /// Executes a Store Proc in a transaction
        /// </summary>
        /// 
        /// <param name="storedProcedureName">Store Procedure Name</param>
        /// <param name="sqlParameters">An array of SQL parmeters for the Store Procedure</param>
        /// 
        /// <returns>A bool with a Success for a Failiure flag</returns>
        /// 
        protected bool ExecuteTransactionalStoredProcedure(string storedProcedureName, SqlParameter[] sqlParameters)
        {
            DbCommand command = GetStoredProcedureCommand(storedProcedureName);

            foreach (SqlParameter sqlParameter in sqlParameters)
            {
                database.AddInParameter(command, sqlParameter.ParameterName, sqlParameter.DbType, sqlParameter.Value);
            }

            using (DbConnection connection = database.CreateConnection())
            {
                connection.Open();

                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    database.ExecuteNonQuery(command, transaction);
                    transaction.Commit();
                    return true;
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    DAOExceptionHandler.Process(exception, "Failed to execute transaction [{0}]", storedProcedureName);
                    return false;
                }
            }
        }

        /// <summary>
        /// Executes a Store Proc in a transaction
        /// </summary>
        /// 
        /// <param name="storedProcedureName">Store Procedure Name</param>
        /// <param name="sqlParameters">An array of SQL parmeters for the Store Procedure</param>
        /// <param name="timeOut">time out for the completion of query or store procedure</param>
        /// <returns>A bool with a Success for a Failiure flag</returns>
        /// 
        protected bool ExecuteTransactionalStoredProcedure(string storedProcedureName, SqlParameter[] sqlParameters, int timeOut)
        {
            DbCommand command = GetStoredProcedureCommand(storedProcedureName);
            command.CommandTimeout = timeOut;

            foreach (SqlParameter sqlParameter in sqlParameters)
            {
                database.AddInParameter(command, sqlParameter.ParameterName, sqlParameter.DbType, sqlParameter.Value);
            }

            using (DbConnection connection = database.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                database.ExecuteNonQuery(command, transaction);
                transaction.Commit();

                return true;
            }
        }

        #endregion

        #region ExecuteQueryAsXml

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="query"></param>
        /// 
        /// <returns></returns>
        /// 
        protected string ExecuteQueryAsXml(string query)
        {
            SqlDatabase sqlDatabase = database as SqlDatabase;
            DbCommand command = sqlDatabase.GetSqlStringCommand(query + " FOR XML AUTO");

            XmlReader reader = null;
            StringBuilder result = new StringBuilder();

            try
            {
                reader = sqlDatabase.ExecuteXmlReader(command);
                while (!reader.EOF)
                {
                    if (reader.IsStartElement())
                    {
                        result.Append(reader.ReadOuterXml());
                        result.Append(Environment.NewLine);
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (command.Connection != null)
                {
                    command.Connection.Close();
                }
            }

            return result.ToString();
        }

        #endregion

        #region Property Accessors

        /// <summary>
        /// Get's and Set's the Database instance name this DAO will use.
        /// </summary>
        /// 
        public string DatabaseInstance
        {
            get 
            { 
                return databaseInstance; 
            }
            set 
            { 
                databaseInstance = value; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        protected Database Database
        {
            get 
            { 
                return this.database; 
            }
        }

        #endregion

        #region ProcessAttributes

        /// <summary>
        /// This method reflects on itself (subclasses) and adds any methods that are 
        /// anotated with the transaction attribute to an internal cache.  The cache can 
        /// be consulted per dao method invocation to verify if the current method being 
        /// invoked is transactional or not.
        /// 
        /// </summary>
        /// 
        private void ProcessAttributes()
        {
            try
            {
                MethodInfo[] methods = GetType().GetMethods();

                foreach (MethodInfo methodInfo in methods)
                {
                    foreach (TransactionAttribute transactionAttribute in methodInfo.GetCustomAttributes(typeof(TransactionAttribute), true))
                    {
                        if (transactionAttribute != null)
                        {
                            cache.Add(methodInfo.Name, transactionAttribute.Rquired);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                DAOExceptionHandler.Process(exception, "Failed to process DAO attributes.");
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Overrides base toString by providing the name of the database
        /// instance this DAO will default to for all database related operations.
        /// </summary>
        /// 
        /// <returns>description of this DAO.</returns>
        /// 
        public override string ToString()
        {
            return "Database Instance = " + databaseInstance;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// 
        private void EmptyOnEntityDeleted(object sender, GenericDAOEventArgs args)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// 
        private void EmptyOnEntityUpdated(object sender, GenericDAOEventArgs args)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// 
        private void EmptyOnEntitySaved(object sender, GenericDAOEventArgs args)
        {
        }

        #region IDataAccess Members

        public IDbConnection CreateConnection()
        {
            return Database.CreateConnection() as IDbConnection;
        }

        #endregion
    }
}