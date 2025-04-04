using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Base.DataBase
{
    public class DapperDB : IDbConnection
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DataAccessQuickStart"].ConnectionString;

        private static DapperDB _windDb;

        public static IDbConnection DbConnection => _windDb ?? (_windDb = new DapperDB());

        private DapperDB()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public T ExecuteScalar<T>(string sql)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.ExecuteScalar<T>(sql);
            }
        }

        public T ExecuteScalar<T>(string storedProc, IWindDParameters parameters)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.ExecuteScalar<T>(storedProc, parameters.Parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> QueryDB<T>(string sql)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<T>(sql);
            }
        }

        public T QueryDBFirstOrDefault<T>(string sql, T anonymousReturnType)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<T>(sql);
            }
        }

        public T QueryDBFirstOrDefault<T>(string sql)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<T>(sql);
            }
        }

        public int ExecuteNonQuery(string storedProc, IWindDParameters parameters)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Execute(storedProc, parameters.Parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public int ExecuteNonQuery(string sql)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Execute(sql);
            }
        }

        public IEnumerable<T> QueryDB<T>(string storedProc, IWindDParameters parameters)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<T>(storedProc, parameters.Parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> QueryDB<T>(string storedProc, IWindDParameters parameters, T anonymousReturnType)
        {
            return this.QueryDB<T>(storedProc, parameters);
        }

        public MultipleResultSet<T1, T2> QueryDbMultipleResultSet<T1, T2>(string sql)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                using (var multi = db.QueryMultiple(sql))
                {
                    return new MultipleResultSet<T1, T2>
                    {
                        ResultSet1 = multi.Read<T1>().ToList(),
                        ResultSet2 = multi.Read<T2>().ToList()
                    };
                }
            }
        }

        public MultipleResultSet<T1, T2> QueryDbMultipleResultSet<T1, T2>(string storedProc, IWindDParameters parameters)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                using (var multi = db.QueryMultiple(storedProc, parameters.Parameters, commandType: CommandType.StoredProcedure))
                {
                    return new MultipleResultSet<T1, T2>
                    {
                        ResultSet1 = multi.Read<T1>().ToList(),
                        ResultSet2 = multi.Read<T2>().ToList()
                    };
                }
            }
        }

        public MultipleResultSet<T1, T2, T3> QueryDbMultipleResultSet<T1, T2, T3>(string sql)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                using (var multi = db.QueryMultiple(sql))
                {
                    return new MultipleResultSet<T1, T2, T3>
                    {
                        ResultSet1 = multi.Read<T1>().ToList(),
                        ResultSet2 = multi.Read<T2>().ToList(),
                        ResultSet3 = multi.Read<T3>().ToList()
                    };
                }
            }
        }

        public MultipleResultSet<T1, T2, T3> QueryDbMultipleResultSet<T1, T2, T3>(string storedProc, IWindDParameters parameters)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                using (var multi = db.QueryMultiple(storedProc, parameters.Parameters, commandType: CommandType.StoredProcedure))
                {
                    return new MultipleResultSet<T1, T2, T3>
                    {
                        ResultSet1 = multi.Read<T1>().ToList(),
                        ResultSet2 = multi.Read<T2>().ToList(),
                        ResultSet3 = multi.Read<T3>().ToList()
                    };
                }
            }
        }

        public MultipleResultSet<T1, T2, T3, T4> QueryDbMultipleResultSet<T1, T2, T3, T4>(string sql)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                using (var multi = db.QueryMultiple(sql))
                {
                    return new MultipleResultSet<T1, T2, T3, T4>
                    {
                        ResultSet1 = multi.Read<T1>().ToList(),
                        ResultSet2 = multi.Read<T2>().ToList(),
                        ResultSet3 = multi.Read<T3>().ToList(),
                        ResultSet4 = multi.Read<T4>().ToList()
                    };
                }
            }
        }

        public MultipleResultSet<T1, T2, T3, T4> QueryDbMultipleResultSet<T1, T2, T3, T4>(string storedProc, IWindDParameters parameters)
        {
            using (System.Data.IDbConnection db = new SqlConnection(_connectionString))
            {
                using (var multi = db.QueryMultiple(storedProc, parameters.Parameters, commandType: CommandType.StoredProcedure))
                {
                    return new MultipleResultSet<T1, T2, T3, T4>
                    {
                        ResultSet1 = multi.Read<T1>().ToList(),
                        ResultSet2 = multi.Read<T2>().ToList(),
                        ResultSet3 = multi.Read<T3>().ToList(),
                        ResultSet4 = multi.Read<T4>().ToList()
                    };
                }
            }
        }
    }

    public class DapperParameters : IWindDParameters
    {
        private readonly DynamicParameters _parameters;

        public object Parameters => _parameters;

        public DapperParameters()
        {
            _parameters = new DynamicParameters();
        }

        public IWindDParameters AddParameter(string name, object value)
        {
            _parameters.Add(name, value);
            return this;
        }
    }

    public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
    {
        public override void SetValue(IDbDataParameter parameter, TimeSpan value)
        {
            parameter.Value = value.ToString();
        }

        public override TimeSpan Parse(object value)
        {
            return TimeSpan.Parse((string)value);
        }
    }
}
