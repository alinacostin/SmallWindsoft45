using System.Collections.Generic;

namespace Base.DataBase
{
    public interface IDbConnection
    {
        IEnumerable<T> QueryDB<T>(string sql);

        T QueryDBFirstOrDefault<T>(string sql, T anonymousReturnType);

        T QueryDBFirstOrDefault<T>(string sql);

        IEnumerable<T> QueryDB<T>(string storedProc, IWindDParameters parameters);

        IEnumerable<T> QueryDB<T>(string storedProc, IWindDParameters parameters, T anonymousReturnType);

        int ExecuteNonQuery(string storedProc, IWindDParameters parameters);

        int ExecuteNonQuery(string sql);

        T ExecuteScalar<T>(string sql);

        T ExecuteScalar<T>(string storedProc, IWindDParameters parameters);

        MultipleResultSet<T1, T2> QueryDbMultipleResultSet<T1, T2>(string sql);

        MultipleResultSet<T1, T2> QueryDbMultipleResultSet<T1, T2>(string storedProc, IWindDParameters parameters);

        MultipleResultSet<T1, T2, T3> QueryDbMultipleResultSet<T1, T2, T3>(string sql);

        MultipleResultSet<T1, T2, T3> QueryDbMultipleResultSet<T1, T2, T3>(string storedProc, IWindDParameters parameters);

        MultipleResultSet<T1, T2, T3, T4> QueryDbMultipleResultSet<T1, T2, T3, T4>(string sql);

        MultipleResultSet<T1, T2, T3, T4> QueryDbMultipleResultSet<T1, T2, T3, T4>(string storedProc, IWindDParameters parameters);
    }

    public interface IWindDParameters
    {
        object Parameters { get; }
        IWindDParameters AddParameter(string name, object value);
    }

    public class MultipleResultSet<T1, T2>
    {
        public IEnumerable<T1> ResultSet1 { get; set; }
        public IEnumerable<T2> ResultSet2 { get; set; }
    }

    public class MultipleResultSet<T1, T2, T3> : MultipleResultSet<T1, T2>
    {
        public IEnumerable<T3> ResultSet3 { get; set; }
    }

    public class MultipleResultSet<T1, T2, T3, T4> : MultipleResultSet<T1, T2, T3>
    {
        public IEnumerable<T4> ResultSet4 { get; set; }
    }
}
