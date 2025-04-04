using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Globalization;
////using WindNet.Utils;
using System.Web;
using Base.Configuration;
using Base.BaseUtils;
using System.Linq;

namespace Base.DataBase
{
    public static class TemplateStoreProcName
    {
        public const string SingleSelect = "sp__result_select";
        public const string MultipleSelect = "sp__multiple_result_select";
        public const string SingleDelete = "sp__result_delete";
        public const string MultipleDelete = "sp__multiple_delete";
    }

    public class WindDatabase
    {
        #region ExecuteNonQuery

        /// <summary>
        /// Method for executing a sql query command (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="query">SQL Command to execute (INSERT, UPDATE, DELETE)</param>
        /// <returns>Return the number of records affected by the command.</returns>
        public static int ExecuteNonQuery(string query)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(query);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Method for executing a sql query command (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="query">SQL query command to execute (INSERT, UPDATE, DELETE)</param>
        /// <returns>Return the number of records affected by the command.</returns>
        public static int ExecuteNonQuery(Database db, string query)
        {
            DbCommand cmd = db.GetSqlStringCommand(query);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Method for executing a stored procedure (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="procName">Stored procedure name.</param>
        /// <param name="parameters">Stored procedure parameters.</param>
        /// <returns>Return the number of records affected by the command.</returns>
        public static int ExecuteNonQuery(string procName, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }

        public static int ExecuteNonQuery(string procName, out object returnValue, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);

            int retVal = db.ExecuteNonQuery(cmd);
            returnValue = cmd.Parameters["@RETURN_VALUE"].Value;

            return retVal;
        }

        /// <summary>
        /// Method for executing a sql query command (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="procName">Stored procedure name.</param>
        /// <param name="parameters">Stored procedure parameters.</param>
        /// <returns>Return the number of records affected by the command.</returns>
        public static int ExecuteNonQuery(Database db, string procName, params object[] parameters)
        {
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Method for executing a stored procedure (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="queryObj">User-defined query object.</param>
        /// <returns>Return the number of records affected by the command.</returns>
        public static int ExecuteNonQuery(Query queryObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(queryObj.QueryString);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Method for executing a sql query command (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="queryObj">User-defined query object.</param>
        /// <returns>Return the number of records affected by the command.</returns>
        public static int ExecuteNonQuery(Database db, Query queryObj)
        {
            DbCommand cmd = db.GetSqlStringCommand(queryObj.QueryString);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Method for executing a stored procedure (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="sspObj">Standard store procedure user-defined object.</param>
        /// <returns>Return the number of records affected by the command.</returns>
        public static int ExecuteNonQuery(StandardStoreProc sspObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(sspObj.Name, sspObj.Parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Method for executing a stored procedure (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="sspObj">Standard store procedure user-defined object.</param>
        /// <returns>Return the number of records affected by the command.</returns>
        public static int ExecuteNonQuery(Database db, StandardStoreProc sspObj)
        {
            DbCommand cmd = db.GetStoredProcCommand(sspObj.Name, sspObj.Parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// Method for executing sql query command containing single/multiple select commands (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="query">SQL command to execute.</param>
        /// <returns>Returns one newly created dataset containing corresponding DataTable objects for each select command in the query.</returns>
        public static DataSet ExecuteDataSet(string query)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(query);
            SetCommandTimeout(cmd);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Method for executing sql query command containing single/multiple select commands (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="query">SQL command to execute.</param>
        /// <returns>Returns one newly created dataset containing corresponding DataTable objects for each select command in the query.</returns>
        public static DataSet ExecuteDataSet(Database db, string query)
        {
            DbCommand cmd = db.GetSqlStringCommand(query);
            SetCommandTimeout(cmd);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Method for executing stored procedure containing single/multiple select commands (WITHOUT TRANSACTION)
        /// </summary>
        /// <param name="procName">Stored procedure name.</param>
        /// <param name="parameters">Stored procedure parameters.</param>
        /// <returns>Returns one newly created dataset containing corresponding DataTable objects for each select command in the stored procedure.</returns>
        public static DataSet ExecuteDataSet(string procName, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Method for executing stored procedure containing single/multiple select commands (WITH TRANSACTION)
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="procName">Stored procedure name.</param>
        /// <param name="parameters">Stored procedure parameters.</param>
        /// <returns>Returns one newly created dataset containing corresponding DataTable objects for each select command in the stored procedure.</returns>
        public static DataSet ExecuteDataSet(Database db, string procName, params object[] parameters)
        {
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Method for executing stored procedure containing single/multiple select commands (WITHOUT TRANSACTION)
        /// </summary>
        /// <param name="queryObj">Query user-defined object.</param>
        /// <returns>Returns one newly created dataset containing corresponding DataTable objects for each select command in the query user-defined object.</returns>
        public static DataSet ExecuteDataSet(Query queryObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(queryObj.QueryString);
            SetCommandTimeout(cmd);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Method for executing stored procedure containing single/multiple select commands (WITH TRANSACTION)
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="queryObj">Query user-defined object.</param>
        /// <returns>Returns one newly created dataset containing corresponding DataTable objects for each select command in the query user-defined object.</returns>
        public static DataSet ExecuteDataSet(Database db, Query queryObj)
        {
            DbCommand cmd = db.GetSqlStringCommand(queryObj.QueryString);
            SetCommandTimeout(cmd);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Method for executing stored procedure containing single/multiple select commands (WITHOUT TRANSACTION)
        /// </summary>
        /// <param name="sspObj">Standard store procedure user-defined object.</param>
        /// <returns>Returns one newly created dataset containing corresponding DataTable objects for each select command in the query user-defined object.</returns>
        public static DataSet ExecuteDataSet(StandardStoreProc sspObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(sspObj.Name, sspObj.Parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Method for executing stored procedure containing single/multiple select commands (WITH TRANSACTION)
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="sspObj">Standard store procedure user-defined object.</param>
        /// <returns>Returns one newly created dataset containing corresponding DataTable objects for each select command in the query user-defined object.</returns>
        public static DataSet ExecuteDataSet(Database db, StandardStoreProc sspObj)
        {
            DbCommand cmd = db.GetStoredProcCommand(sspObj.Name, sspObj.Parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Method for loading DataTables data (inside dataset) based on store procedure with table parameters (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="dataset">Dataset containing tables to be loaded.</param>
        /// <param name="tableNames">Tables inside dataset that receive data. </param>
        /// <param name="procName">Store procedure name.</param>
        /// <param name="parameters">Store procedure parameters.</param>
        public static void LoadDataSetEx(DataSet dataset, string[] tableNames, string procName, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters.Select(x => (x is System.Data.SqlClient.SqlParameter) ? (x as System.Data.SqlClient.SqlParameter).Value : x).ToArray());
            foreach (System.Data.SqlClient.SqlParameter p in parameters.Where(x => (x is System.Data.SqlClient.SqlParameter)))
            {
                (cmd.Parameters[p.ParameterName] as System.Data.SqlClient.SqlParameter).TypeName = p.TypeName;
            }
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);
        }

        #endregion

        #region ExecuteDataReader

        /// <summary>
        /// Method for retreaving data records based on a query (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="query">Query string to be executed on database server.</param>
        /// <returns>Returns an IDataReader. Be carefull to close the reader object.</returns>
        public static IDataReader ExecuteDataReader(string query)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(query);
            SetCommandTimeout(cmd);
            return db.ExecuteReader(cmd);
        }

        /// <summary>
        /// Method for retreaving data records based on a query (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="query">Query string to be executed on database server.</param>
        /// <returns>Returns an IDataReader. Be carefull to close the reader object and dispose it.</returns>
        public static IDataReader ExecuteDataReader(Database db, string query)
        {
            DbCommand cmd = db.GetSqlStringCommand(query);
            SetCommandTimeout(cmd);
            return db.ExecuteReader(cmd);
        }

        /// <summary>
        /// Method for retreaving data records based on a stored procedure (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="procName">Stored procedure name.</param>
        /// <param name="parameters">Stored procedure parameters.</param>
        /// <returns>Returns an IDataReader. Be carefull to close the reader object and dispose it.</returns>
        public static IDataReader ExecuteDataReader(string procName, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteReader(cmd);
        }

        /// <summary>
        /// Method for retreaving data records based on a stored procedure (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="procName">Stored procedure name.</param>
        /// <param name="parameters">Stored procedure parameters.</param>
        /// <returns>Returns an IDataReader. Be carefull to close the reader object and dispose it.</returns>
        public static IDataReader ExecuteDataReaderEx(string procName, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters.Select(x=>(x is System.Data.SqlClient.SqlParameter)?(x as System.Data.SqlClient.SqlParameter).Value:x).ToArray());
            foreach(System.Data.SqlClient.SqlParameter p in parameters.Where(x=>(x is System.Data.SqlClient.SqlParameter)))
            {
                (cmd.Parameters[p.ParameterName] as System.Data.SqlClient.SqlParameter).TypeName = p.TypeName;
            }
            SetCommandTimeout(cmd);
            return db.ExecuteReader(cmd);
        }

        /// <summary>
        /// Method for retreaving data records based on a stored procedure (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="procName">Stored procedure name.</param>
        /// <param name="parameters">Stored procedure parameters.</param>
        /// <returns>Returns an IDataReader. Be carefull to close the reader object and dispose it.</returns>
        public static IDataReader ExecuteDataReader(Database db, string procName, params object[] parameters)
        {
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteReader(cmd);
        }

        /// <summary>
        /// Method for retreaving data records based on a query user-defined object. (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="queryObj">Query user-defined object containing one sql query string.</param>
        /// <returns>Returns an IDataReader. Be carefull to close the reader object and dispose it.</returns>
        public static IDataReader ExecuteDataReader(Query queryObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(queryObj.QueryString);
            SetCommandTimeout(cmd);
            return db.ExecuteReader(cmd);
        }

        /// <summary>
        /// Method for retreaving data records based on a query user-defined object. (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="queryObj">Query user-defined object containing one sql query string.</param>
        /// <returns>Returns an IDataReader. Be carefull to close the reader object and dispose it.</returns>
        public static IDataReader ExecuteDataReader(Database db, Query queryObj)
        {
            DbCommand cmd = db.GetSqlStringCommand(queryObj.QueryString);
            SetCommandTimeout(cmd);
            return db.ExecuteReader(cmd);
        }

        /// <summary>
        /// Method for retreaving data records based on a standard store procedure user-defined object. (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="sspObj">Standard store procedure user-defined object.</param>
        /// <returns>Returns an IDataReader. Be carefull to close the reader object and dispose it.</returns>
        public static IDataReader ExecuteDataReader(StandardStoreProc sspObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(sspObj.Name, sspObj.Parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteReader(cmd);
        }

        /// <summary>
        /// Method for retreaving data records based on a standard store procedure user-defined object. (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="sspObj">Standard store procedure user-defined object.</param>
        /// <returns>Returns an IDataReader. Be carefull to close the reader object and dispose it.</returns>
        public static IDataReader ExecuteDataReader(Database db, StandardStoreProc sspObj)
        {
            DbCommand cmd = db.GetStoredProcCommand(sspObj.Name, sspObj.Parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteReader(cmd);
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// Method for retreving one value converted to string from database based on a sql query string (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="query">Query string containing one sql select command.</param>
        /// <returns>Returns one single value converted to string.</returns>
        public static string ExecuteScalar(string query)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(query);
            SetCommandTimeout(cmd);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }

        /// <summary>
        /// Method for retreving one value converted to string from database based on a sql query string (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="query">Query string containing one sql select command.</param>
        /// <returns>Returns one single value converted to string.</returns>
        public static string ExecuteScalar(Database db, string query)
        {
            DbCommand cmd = db.GetSqlStringCommand(query);
            SetCommandTimeout(cmd);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }

        /// <summary>
        /// Method for retreving one value converted to string from database based on a store procedure (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="procName">Store procedure name containing one sql select command.</param>
        /// <param name="parameters">Store procedure parameters.</param>
        /// <returns>Returns one single value converted to string.</returns>
        public static string ExecuteScalar(string procName, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }

        /// <summary>
        /// Method for retreving one value converted to string from database based on a store procedure (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="procName">Store procedure name containing one sql select command.</param>
        /// <param name="parameters">Store procedure parameters.</param>
        /// <returns>Returns one single value converted to string.</returns>
        public static string ExecuteScalar(Database db, string procName, params object[] parameters)
        {
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }

        /// <summary>
        /// Method for retreving one value converted to string from database based on query user-defined object (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="queryObj">Query user-defined object containing one sql select command.</param>
        /// <returns>Returns one single value converted to string.</returns>
        public static string ExecuteScalar(Query queryObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(queryObj.QueryString);
            SetCommandTimeout(cmd);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }

        /// <summary>
        /// Method for retreving one value converted to string from database based on query user-defined object (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="queryObj">Query user-defined object containing one sql select command.</param>
        /// <returns>Returns one single value converted to string.</returns>
        public static string ExecuteScalar(Database db, Query queryObj)
        {
            DbCommand cmd = db.GetSqlStringCommand(queryObj.QueryString);
            SetCommandTimeout(cmd);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }

        /// <summary>
        /// Method for retreving one value converted to string from database based on standard store procedure user-defined object (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="sspObj">Standard store procedure user-defined object containing one sql select command.</param>
        /// <returns>Returns one single value converted to string.</returns>
        public static string ExecuteScalar(StandardStoreProc sspObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(sspObj.Name, sspObj.Parameters);
            SetCommandTimeout(cmd);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }

        /// <summary>
        /// Method for retreving one value converted to string from database based on standard store procedure user-defined object (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="sspObj">Standard store procedure user-defined object containing one sql select command.</param>
        /// <returns>Returns one single value converted to string.</returns>
        public static string ExecuteScalar(Database db, StandardStoreProc sspObj)
        {
            DbCommand cmd = db.GetStoredProcCommand(sspObj.Name, sspObj.Parameters);
            SetCommandTimeout(cmd);
            return Convert.ToString(db.ExecuteScalar(cmd));
        }

        public static object ExecuteScalar(bool resultObject, string procName, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteScalar(cmd);
        }

        #endregion

        #region LoadDataSet

        /// <summary>
        /// Method for loading DataTables data (inside dataset) based on sql query string (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="dataset">Dataset containing tables to be loaded.</param>
        /// <param name="tableNames">Tables inside dataset that receive data. </param>
        /// <param name="query">Query string to be executed.</param>
        public static void LoadDataSet(DataSet dataset, string[] tableNames, string query)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(query);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);
        }

        /// <summary>
        /// Method for loading DataTables data (inside dataset) based on sql query string (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="dataset">Dataset containing tables to be loaded.</param>
        /// <param name="tableNames">Tables inside dataset that receive data. </param>
        /// <param name="query">Query string to be executed.</param>
        public static void LoadDataSet(Database db, DataSet dataset, string[] tableNames, string query)
        {
            DbCommand cmd = db.GetSqlStringCommand(query);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);
        }

        /// <summary>
        /// Method for loading DataTables data (inside dataset) based on store procedure (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="dataset">Dataset containing tables to be loaded.</param>
        /// <param name="tableNames">Tables inside dataset that receive data. </param>
        /// <param name="procName">Store procedure name.</param>
        /// <param name="parameters">Store procedure parameters.</param>
        public static void LoadDataSet(DataSet dataset, string[] tableNames, string procName, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);
        }

        /// <summary>
        /// Method for loading DataTables data (inside dataset) based on store procedure (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="dataset">Dataset containing tables to be loaded.</param>
        /// <param name="tableNames">Tables inside dataset that receive data. </param>
        /// <param name="procName">Store procedure name.</param>
        /// <param name="parameters">Store procedure parameters.</param>
        public static int LoadDataSetWithReturnValue(DataSet dataset, string[] tableNames, string procName, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);

            return UtilsGeneral.ToInteger(cmd.Parameters["@RETURN_VALUE"].Value, 0);
        }

        /// <summary>
        /// Method for loading DataTables data (inside dataset) based on store procedure (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="dataset">Dataset containing tables to be loaded.</param>
        /// <param name="tableNames">Tables inside dataset that receive data. </param>
        /// <param name="procName">Store procedure name.</param>
        /// <param name="parameters">Store procedure parameters.</param>
        public static void LoadDataSet(Database db, DataSet dataset, string[] tableNames, string procName, params object[] parameters)
        {
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);
        }

        /// <summary>
        /// Method for loading DataTables data (inside dataset) based on user-defined query object (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="dataset">Dataset containing tables to be loaded.</param>
        /// <param name="tableNames">Tables inside dataset that receive data. </param>
        /// <param name="queryObj">User-defined query object.</param>
        public static void LoadDataSet(DataSet dataset, string[] tableNames, Query queryObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(queryObj.QueryString);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);
        }

        /// <summary>
        /// Method for loading DataTables data (inside dataset) based on user-defined query object (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="dataset">Dataset containing tables to be loaded.</param>
        /// <param name="tableNames">Tables inside dataset that receive data. </param>
        /// <param name="queryObj">User-defined query object.</param>
        public static void LoadDataSet(Database db, DataSet dataset, string[] tableNames, Query queryObj)
        {
            DbCommand cmd = db.GetSqlStringCommand(queryObj.QueryString);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);
        }

        /// <summary>
        /// Method for loading DataTables data (inside dataset) based on standard store procedure user-defined object (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="dataset">Dataset containing tables to be loaded.</param>
        /// <param name="tableNames">Tables inside dataset that receive data. </param>
        /// <param name="sspObj">Standard store procedure user-defined object.</param>
        public static void LoadDataSet(DataSet dataset, string[] tableNames, StandardStoreProc sspObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(sspObj.Name, sspObj.Parameters);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);
        }

        /// <summary>
        /// Method for loading DataTables data (inside dataset) based on standard store procedure user-defined object (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="dataset">Dataset containing tables to be loaded.</param>
        /// <param name="tableNames">Tables inside dataset that receive data. </param>
        /// <param name="sspObj">Standard store procedure user-defined object.</param>
        public static void LoadDataSet(Database db, DataSet dataset, string[] tableNames, StandardStoreProc sspObj)
        {
            DbCommand cmd = db.GetStoredProcCommand(sspObj.Name, sspObj.Parameters);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);
        }

        #endregion

        #region UpdateDataset

        /// <summary>
        /// Method for updating sql data based on data contained in dataTable inside the dataset (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="dataset">Dataset containing local tables with data to be updated on the sql server.</param>
        /// <param name="tableName">Local tables (DataTables) containing data to be updated on the sql server.</param>
        /// <param name="insertCommand">Command to be executed for insert operation.</param>
        /// <param name="updateCommand">Command to be executed for update operation.</param>
        /// <param name="deleteCommand">Command to be executed for delete operation.</param>
        public static void UpdateDataset(DataSet dataset, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand)
        {
            Database db = DatabaseFactory.CreateDatabase();
            db.UpdateDataSet(dataset, tableName, insertCommand, updateCommand, deleteCommand, UpdateBehavior.Standard);
        }

        public static void UpdateDataset(Database db, DataSet dataset, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand)
        {
            db.UpdateDataSet(dataset, tableName, insertCommand, updateCommand, deleteCommand, UpdateBehavior.Standard);
        }

        #endregion

        #region FillDataTable

        /// <summary>
        /// Method for filling dataTable using template store procedure user-defined object (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="tspObj">Template store procedure user-defined object.</param>
        /// <returns>Returns one datatable containing data retreived based on store procedure.</returns>
        public static DataTable FillDataTable(TemplateStoreProc tspObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet dataset = new DataSet();
            int j = 0;
            object[] parameters = new object[8];
            parameters[j++] = tspObj.ServerTableName;      // sourceName
            parameters[j++] = tspObj.Fields;                       // fields
            parameters[j++] = tspObj.Where;                     // filter
            parameters[j++] = tspObj.Group;                      // group
            parameters[j++] = tspObj.Sort;                         // sort

            DbCommand cmd = db.GetStoredProcCommand(TemplateStoreProcName.SingleSelect, parameters);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tspObj.LocalTableName);
            return dataset.Tables[0];
        }

        /// <summary>
        /// Method for filling dataTable using template store procedure user-defined object (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="tspObj">Template store procedure user-defined object.</param>
        /// <returns>Returns one datatable containing data retreived based on store procedure.</returns>
        public static DataTable FillDataTable(Database db, TemplateStoreProc tspObj)
        {
            DataSet dataset = new DataSet();
            int j = 0;
            object[] parameters = new object[8];
            parameters[j++] = tspObj.ServerTableName;      // sourceName
            parameters[j++] = tspObj.Fields;                       // fields
            parameters[j++] = tspObj.Where;                     // filter
            parameters[j++] = tspObj.Group;                      // group
            parameters[j++] = tspObj.Sort;                         // sort

            DbCommand cmd = db.GetStoredProcCommand(TemplateStoreProcName.SingleSelect, parameters);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tspObj.LocalTableName);
            return dataset.Tables[0];
        }

        /// <summary>
        /// Method for filling dataTable based on store procedure (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="tableName">Table that receive data. </param>
        /// <param name="procName">Store procedure name.</param>
        /// <param name="parameters">Store procedure parameters.</param>
        public static DataTable FillDataTable(string tableName, string procName, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet dataset = new DataSet();
            dataset.Tables.Add(tableName);
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters.ToArray());
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableName);
            return dataset.Tables[0];
        }


        /// <summary>
        /// Method for filling dataTable based on query(WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="tableName">Table that receive data. </param>
        /// <param name="sqlQuery">Sql Query.</param>
        public static DataTable FillDataTable(string tableName, string sqlQuery)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet dataset = new DataSet();
            dataset.Tables.Add(tableName);
            DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableName);
            return dataset.Tables[0];
        }

        /// <summary>
        /// Method for filling dataTable based on store procedure with table parameters (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="tableName">Table that receive data. </param>
        /// <param name="procName">Store procedure name.</param>
        /// <param name="parameters">Store procedure parameters.</param>
        public static DataTable FillDataTableEx(string tableName, string procName, params object[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet dataset = new DataSet();
            dataset.Tables.Add(tableName);
            DbCommand cmd = db.GetStoredProcCommand(procName, parameters.Select(x => (x is System.Data.SqlClient.SqlParameter) ? (x as System.Data.SqlClient.SqlParameter).Value : x).ToArray());
            foreach (System.Data.SqlClient.SqlParameter p in parameters.Where(x => (x is System.Data.SqlClient.SqlParameter)))
            {
                (cmd.Parameters[p.ParameterName] as System.Data.SqlClient.SqlParameter).TypeName = p.TypeName;
            }
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableName);
            return dataset.Tables[0];
        }
        #endregion

        #region FillDataSet

        /// <summary>
        /// Method for filling multiple datatable (using template store procedure objects) contained in one dataset based on general user-defined object (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="dataset">Dataset containing datatable to be updated (filled).</param>
        /// <param name="sqlObject">User-defined object containing multiple template stored procedure user-defined objects.</param>
        public static void FillDataSet(DataSet dataset, SQLObjects sqlObject)
        {
            Database db = DatabaseFactory.CreateDatabase();

            int j = 0;
            object[] parameters = new object[50];
            string[] tableNames = new string[sqlObject.Count];

            for (int i = 0; i < sqlObject.Count; i++)
            {
                TemplateStoreProc tempObj = (TemplateStoreProc)sqlObject[i];
                tableNames[i] = tempObj.LocalTableName;

                parameters[j++] = tempObj.ServerTableName;
                parameters[j++] = tempObj.Fields;
                parameters[j++] = tempObj.Where;
                parameters[j++] = tempObj.Group;
                parameters[j++] = tempObj.Sort;
            }

            DbCommand cmd = db.GetStoredProcCommand(TemplateStoreProcName.MultipleSelect, parameters);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);
        }

        /// <summary>
        /// Method for filling multiple datatable (using template store procedure objects) contained in one dataset based on general user-defined object (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="dataset">Dataset containing datatable to be updated (filled).</param>
        /// <param name="sqlObject">User-defined object containing multiple template stored procedure user-defined objects.</param>
        public static void FillDataSet(Database db, DataSet dataset, SQLObjects sqlObject)
        {
            int j = 0;
            object[] parameters = new object[50];
            string[] tableNames = new string[sqlObject.Count];

            for (int i = 0; i < sqlObject.Count; i++)
            {
                TemplateStoreProc tempObj = (TemplateStoreProc)sqlObject[i];
                tableNames[i] = tempObj.LocalTableName;

                parameters[j++] = tempObj.ServerTableName;
                parameters[j++] = tempObj.Fields;
                parameters[j++] = tempObj.Where;
                parameters[j++] = tempObj.Group;
                parameters[j++] = tempObj.Sort;
            }

            DbCommand cmd = db.GetStoredProcCommand(TemplateStoreProcName.MultipleSelect, parameters);
            SetCommandTimeout(cmd);
            db.LoadDataSet(cmd, dataset, tableNames);
        }

        #endregion

        #region SingleDelete & MultipleDelete

        /// <summary>
        /// Method for deleting single/multiple rows from one table based on template store procedure user-defined object (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="tspObj">Template store procedure user-defined object.</param>
        /// <returns>Returns the number of the affected rows.</returns>
        public static int Delete(TemplateStoreProc tspObj)
        {
            Database db = DatabaseFactory.CreateDatabase();
            int j = 0;
            object[] parameters = new object[3];
            parameters[j++] = tspObj.ServerTableName;
            parameters[j++] = tspObj.Where;
            parameters[j++] = Base.Configuration.LoginClass.userID;

            DbCommand cmd = db.GetStoredProcCommand(TemplateStoreProcName.SingleDelete, parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Method for deleting single/multiple rows from one table based on template store procedure user-defined object (WITH TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="tspObj">Template store procedure user-defined object.</param>
        /// <returns>Returns the number of the affected rows.</returns>
        public static int Delete(Database db, TemplateStoreProc tspObj)
        {
            int j = 0;
            object[] parameters = new object[3];
            parameters[j++] = tspObj.ServerTableName;
            parameters[j++] = tspObj.Where;
            parameters[j++] = Base.Configuration.LoginClass.userID;

            DbCommand cmd = db.GetStoredProcCommand(TemplateStoreProcName.SingleDelete, parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Method for deleting single/multiple rows from multiple tables based on template store procedure user-defined object (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="sqlObject">Collection of template stored procedure user-defined objects.</param>
        /// <returns>Returns the number of the affected rows.</returns>
        public static int MultipleDelete(SQLObjects sqlObject)
        {
            Database db = DatabaseFactory.CreateDatabase();

            int j = 0;
            object[] parameters = new object[20];
            string[] tableNames = new string[sqlObject.Count];

            for (int i = 0; i < sqlObject.Count; i++)
            {
                TemplateStoreProc tempObj = (TemplateStoreProc)sqlObject[i];
                parameters[j++] = tempObj.ServerTableName;
                parameters[j++] = tempObj.Where;
            }

            DbCommand cmd = db.GetStoredProcCommand(TemplateStoreProcName.MultipleSelect, parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Method for deleting single/multiple rows from multiple tables based on template store procedure user-defined object (WITHOUT TRANSACTION).
        /// </summary>
        /// <param name="db">Database object transmited by referrence in order to execute several sql command in one transaction.</param>
        /// <param name="sqlObject">Collection of template stored procedure user-defined objects.</param>
        /// <returns>Returns the number of the affected rows.</returns>
        public static int MultipleDelete(Database db, SQLObjects sqlObject)
        {
            int j = 0;
            object[] parameters = new object[20];
            string[] tableNames = new string[sqlObject.Count];

            for (int i = 0; i < sqlObject.Count; i++)
            {
                TemplateStoreProc tempObj = (TemplateStoreProc)sqlObject[i];
                parameters[j++] = tempObj.ServerTableName;
                parameters[j++] = tempObj.Where;
            }

            DbCommand cmd = db.GetStoredProcCommand(TemplateStoreProcName.MultipleSelect, parameters);
            SetCommandTimeout(cmd);
            return db.ExecuteNonQuery(cmd);
        }
        #endregion

        #region Set Timeout
        private static void SetCommandTimeout(DbCommand cmd)
        {
            cmd.CommandTimeout = Constants.C_DATABASE_TIMEOUT;
        }
        #endregion

        #region Insert / Update Database

        #region Insert
        private static int identityAfterInsert = 0;
        public static int GetIdentityAfterInsert()
        {
            return identityAfterInsert;
        }
        public static void ResetIdentityAfterInsert()
        {
            identityAfterInsert = 0;
        }
        public static void SetIdentityAfterInsert(int value)
        {
            identityAfterInsert = value;
        }

        public static void ExecuteDatabaseInsert(string tableName, DataRow dataRow, out int identityKey)
        {
            ExecuteDatabaseInsert(tableName, dataRow, out identityKey, null);
        }

        public static void ExecuteDatabaseInsert(string tableName, DataRow dataRow, out int identityKey, string[] skippedFields)
        {
            bool existValidValues;
            string timeStampField;
            string xmlString = BuildXmlString(dataRow, out existValidValues, true, true, out timeStampField, skippedFields);
            if (!existValidValues) throw new Exception("Trebuie sa completati cel putin un camp in formular");

            StandardStoreProc ssp = new StandardStoreProc("sp___XmlRowInsertWithTimestamp", new object[] { tableName, xmlString, Base.Configuration.LoginClass.userID });
            DataSet returnedDataSet = ExecuteDataSet(ssp);
            identityKey = UtilsGeneral.ToInteger(returnedDataSet.Tables[0].Rows[0][0], 0);

            if (identityAfterInsert == 0)
                identityAfterInsert = identityKey;
        }

        public static void ExecuteDatabaseInsert(string tableName, DataRow dataRow)
        {
            ExecuteDatabaseInsert(tableName, dataRow, null);
        }

        public static void ExecuteDatabaseInsert(string tableName, DataRow dataRow, string[] skippedFields)
        {
            bool existValidValues;
            string timeStampField;
            string xmlString = BuildXmlString(dataRow, out existValidValues, true, true, out timeStampField, skippedFields);
            if (existValidValues)
            {
                StandardStoreProc ssp = new StandardStoreProc("sp___XmlRowInsertWithTimestamp", new object[] { tableName, xmlString, Base.Configuration.LoginClass.userID });
                if (identityAfterInsert != 0)
                    ExecuteDataSet(ssp);
                else
                {
                    DataSet returnedDataSet = ExecuteDataSet(ssp);
                    try
                    {
                        identityAfterInsert = Convert.ToInt32(returnedDataSet.Tables[0].Rows[0][0]);
                    }
                    catch { identityAfterInsert = 0; }
                }
            }
        }

        public static void ExecuteDatabaseInsert(string tableName, DataRow dataRow, string[] skippedFields, bool withByte)
        {
            bool existValidValues;
            string timeStampField;
            string xmlString = BuildXmlString(dataRow, out existValidValues, true, false, out timeStampField, skippedFields);
            if (existValidValues)
            {
                StandardStoreProc ssp = new StandardStoreProc("sp___XmlRowInsertWithTimestamp", new object[] { tableName, xmlString, Base.Configuration.LoginClass.userID });
                if (identityAfterInsert != 0)
                    ExecuteDataSet(ssp);
                else
                {
                    DataSet returnedDataSet = ExecuteDataSet(ssp);
                    try
                    {
                        identityAfterInsert = Convert.ToInt32(returnedDataSet.Tables[0].Rows[0][0]);
                    }
                    catch { identityAfterInsert = 0; }
                }
            }
        }

        #endregion

        #region Update

        public static void ExecuteDatabaseUpdate(string tableName, DataRow dataRow, string idFieldNameWhereCondition)
        {
            //byte[] returnedTimeStamp;
            //ExecuteDatabaseUpdate(tableName, dataRow, idFieldNameWhereCondition, false, out returnedTimeStamp);
            ExecuteDatabaseUpdate(tableName, dataRow, idFieldNameWhereCondition, null);
        }

        public static void ExecuteDatabaseUpdate(string tableName, DataRow dataRow, string idFieldNameWhereCondition, string[] skippedFields)
        {
            byte[] returnedTimeStamp;
            ExecuteDatabaseUpdate(tableName, dataRow, idFieldNameWhereCondition, false, out returnedTimeStamp, skippedFields);
        }

        public static void ExecuteDatabaseUpdate(string tableName, DataRow dataRow, string idFieldNameWhereCondition, bool returnTimeStamp, out Byte[] returnedTimeStamp)
        {
            ExecuteDatabaseUpdate(tableName, dataRow, idFieldNameWhereCondition, returnTimeStamp, out returnedTimeStamp, null);
        }

        public static void ExecuteDatabaseUpdate(string tableName, DataRow dataRow, string idFieldNameWhereCondition, bool returnTimeStamp, out Byte[] returnedTimeStamp, string[] skippedFields)
        {
            returnedTimeStamp = new byte[] { };

            bool existValidValues;
            string timeStampField;
            string xmlString = BuildXmlString(dataRow, out existValidValues, false, false, out timeStampField, skippedFields);
            if (existValidValues)
            {
                if (timeStampField == "")
                {
                    StandardStoreProc ssp = new StandardStoreProc("sp___XmlRowUpdate", new object[] { tableName, idFieldNameWhereCondition, xmlString, LoginClass.userID });
                    ExecuteDataSet(ssp);
                }
                else
                {
                    StandardStoreProc ssp = new StandardStoreProc("sp___XmlRowUpdate_Concurrency", new object[] { tableName, idFieldNameWhereCondition, timeStampField, xmlString, Base.Configuration.LoginClass.userID });
                    DataSet returnedDataSet = ExecuteDataSet(ssp);
                    bool hasDeleted = false;
                    try
                    {
                        returnedTimeStamp = (byte[])returnedDataSet.Tables[0].Rows[0][0];
                        hasDeleted = Convert.ToBoolean(returnedDataSet.Tables[0].Rows[0][1]);
                    }
                    catch { }
                    if (hasDeleted)
                    {
                        throw new Exception(Constants.C_CONCURRENCY_UPDATE_FAILED_RECORD_DELETED);
                    }
                    else
                    {
                        if (returnedTimeStamp.Length != 0)
                        {
                            if (!returnTimeStamp)
                            {
                                try
                                {
                                    dataRow[timeStampField] = returnedTimeStamp;
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            //another user modified the row
                            throw new Exception(Constants.C_CONCURRENCY_UPDATE_FAILED);
                        }
                    }
                }
            }
        }

        #endregion

        public static void ExecuteDatabaseDelete(string tableName, string whereDeleteClause)
        {
            StandardStoreProc ssp = new StandardStoreProc("sp__result_delete", new object[] { tableName, whereDeleteClause });
            ExecuteDataSet(ssp);
        }

        #endregion

        #region Split DataTable into more tables
        public static void SplitTableInto2Tables(DataRow mainRow, int startPositionSecondTable, out DataRow firstRow, out DataRow secondRow)
        {
            DataTable mainTable = mainRow.Table;
            DataTable firstTable = new DataTable();
            DataTable secondTable = new DataTable();

            for (int i = 0; i < mainTable.Columns.Count; i++)
            {
                DataColumn dataColumn = new DataColumn(mainTable.Columns[i].ColumnName, mainTable.Columns[i].DataType, mainTable.Columns[i].Expression, mainTable.Columns[i].ColumnMapping);
                if (i < startPositionSecondTable - 1)
                {
                    firstTable.Columns.Add(dataColumn);
                }
                else
                {
                    secondTable.Columns.Add(dataColumn);
                }
            }

            firstTable.Clear();
            firstRow = firstTable.NewRow();
            firstTable.Rows.Add(firstRow);

            secondTable.Clear();
            secondRow = secondTable.NewRow();
            secondTable.Rows.Add(secondRow);

            for (int i = 0; i < mainTable.Columns.Count; i++)
            {
                if (i < startPositionSecondTable - 1)
                {
                    firstRow[firstTable.Columns[i]] = mainRow[mainTable.Columns[i]];
                }
                else
                {
                    secondRow[secondTable.Columns[i - startPositionSecondTable + 1]] = mainRow[mainTable.Columns[i]];
                }
            }
        }

        public static void SplitTableInto3Tables(DataRow mainRow, int startPositionSecondTable, int startPositionThirdTable, out DataRow firstRow, out DataRow secondRow, out DataRow thirdRow)
        {
            DataTable mainTable = mainRow.Table;
            DataTable firstTable = new DataTable();
            DataTable secondTable = new DataTable();
            DataTable thirdTable = new DataTable();

            for (int i = 0; i < mainTable.Columns.Count; i++)
            {
                DataColumn dataColumn = new DataColumn(mainTable.Columns[i].ColumnName, mainTable.Columns[i].DataType, mainTable.Columns[i].Expression, mainTable.Columns[i].ColumnMapping);
                if (i < startPositionSecondTable - 1)
                {
                    firstTable.Columns.Add(dataColumn);
                }
                else if (i < startPositionThirdTable - 1)
                {
                    secondTable.Columns.Add(dataColumn);
                }
                else
                {
                    thirdTable.Columns.Add(dataColumn);
                }
            }

            firstTable.Clear();
            firstRow = firstTable.NewRow();
            firstTable.Rows.Add(firstRow);

            secondTable.Clear();
            secondRow = secondTable.NewRow();
            secondTable.Rows.Add(secondRow);

            thirdTable.Clear();
            thirdRow = thirdTable.NewRow();
            thirdTable.Rows.Add(thirdRow);

            for (int i = 0; i < mainTable.Columns.Count; i++)
            {
                if (i < startPositionSecondTable - 1)
                {
                    firstRow[firstTable.Columns[i]] = mainRow[mainTable.Columns[i]];
                }
                else if (i < startPositionThirdTable - 1)
                {
                    secondRow[secondTable.Columns[i - startPositionSecondTable + 1]] = mainRow[mainTable.Columns[i]];
                }
                else
                {
                    thirdRow[thirdTable.Columns[i - startPositionThirdTable + 1]] = mainRow[mainTable.Columns[i]];
                }
            }
        }

        #endregion

        #region Build XML - Other functions

        private static string BuildXmlString(DataRow dataRow, out bool existValidValues, bool skipNullFields, bool skipTimeStampField, out string timeStampField, string[] skippedFields)
        {
            string xmlString;
            xmlString = "<ROOT>";
            xmlString += "<row ";

            DataTable dataTable = dataRow.Table;

            existValidValues = false;
            timeStampField = "";

            bool existsSkippedFields;
            if (skippedFields == null)
                existsSkippedFields = false;
            else if (skippedFields.Length == 0)
                existsSkippedFields = false;
            else
                existsSkippedFields = true;

            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                string columnName = dataColumn.ColumnName;
                if (existsSkippedFields)
                {
                    bool skipColumn = false;
                    foreach (string skippedField in skippedFields)
                    {
                        if (columnName.Trim().ToUpper() == skippedField.Trim().ToUpper())
                        {
                            skipColumn = true;
                            break;
                        }
                    }
                    if (skipColumn) continue;
                }

                if (dataRow[dataColumn] != DBNull.Value)
                {
                    if (!existValidValues) existValidValues = true;

                    string strDbValue;
                    if (dataColumn.DataType == typeof(bool))
                    {
                        bool dbValue = UtilsGeneral.ToBool(dataRow[dataColumn]);
                        if (dbValue)
                            strDbValue = "1";
                        else
                            strDbValue = "0";
                    }
                    else if (dataColumn.DataType == typeof(DateTime))
                    {
                        DateTime dbValue = Convert.ToDateTime(dataRow[dataColumn]);
                        strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (dataColumn.DataType == typeof(Decimal))
                    {
                        Decimal dbValue = Convert.ToDecimal(dataRow[dataColumn]);
                        strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (dataColumn.DataType == typeof(Double))
                    {
                        Double dbValue = Convert.ToDouble(dataRow[dataColumn]);
                        strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (dataColumn.DataType == typeof(Byte[]) || dataColumn.DataType == typeof(byte[]))
                    {
                        if (skipTimeStampField) continue;

                        byte[] dbValue = (byte[])dataRow[dataColumn];
                        strDbValue = FormatTimestamp(dbValue);
                        if (timeStampField == "") timeStampField = columnName;
                    }
                    else if (dataColumn.DataType == typeof(String) || dataColumn.DataType == typeof(string))
                    {
                        strDbValue = dataRow[dataColumn].ToString().Replace("'", "''");
                        strDbValue = HttpUtility.HtmlEncode(strDbValue);
                    }
                    else
                    {
                        strDbValue = dataRow[dataColumn].ToString();
                    }
                    xmlString += columnName + "=\"" + strDbValue + "\" ";
                }
                else
                {
                    if (skipNullFields)
                        continue;
                    else
                    {
                        if (dataColumn.DataType == typeof(int) || dataColumn.DataType == typeof(Int16) || dataColumn.DataType == typeof(Int32) || dataColumn.DataType == typeof(Int64) || dataColumn.DataType == typeof(DateTime))
                        {
                            xmlString += columnName + "=\"null\" ";
                            if (!existValidValues) existValidValues = true;
                        }
                        else
                            continue;
                    }
                }
            }

            xmlString += "/>";
            xmlString += "</ROOT>";

            return xmlString;
        }

        public static string FormatTimestamp(byte[] ts)
        {
           // string strOutput = "0x";
            //foreach (byte b in ts)
            //{
            //    strOutput += b.ToString("X2");
            //}

            string strOutput = "0x" + BitConverter.ToString(ts).Replace("-", "");
            return strOutput;
        }

        #endregion

        public static DataTable FillDataTable(DataSet dataSource)
        {
            throw new NotImplementedException();
        }
    }
}
