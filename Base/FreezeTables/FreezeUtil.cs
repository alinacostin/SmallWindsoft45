using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.DataBase;
using System.Data.SqlClient;
using Base.BaseUtils;
using Base.Configuration;
using System.Data;

namespace Base.FreezeTables
{
    public class FreezeUtil
    {
        #region Enums

        public enum FreezeMode : byte
        {
            UnFreeze = 0,
            Freeze = 1
        }

        public enum FreezeType : byte
        {
            LockMode = 0,
            OpenMode = 1
        }

        #endregion Enums

        #region Variables

        public static int UserID { get; set; }

        public static string TableName { get; set; }

        public static bool Lock { get; set; }

        public static DateTime? FreezeTime { get; set; }

        public static string Observations { get; set; }

        public static string ResultLock { get; set; }

        public static string UserName { get; set; }

        #endregion Variables

        #region Check Lock

        public static bool IsTableLocked(string tableName)
        {
            try
            {
                List<Object> parameters = new List<object>();

                parameters.Add((byte)FreezeType.OpenMode);
                parameters.Add(LoginClass.userID);
                parameters.Add(tableName);
                parameters.Add(DBNull.Value);
                parameters.Add(DBNull.Value);
                parameters.Add(DBNull.Value);
                parameters.Add(DBNull.Value);
                parameters.Add(DBNull.Value);
                parameters.Add(DBNull.Value);
                parameters.Add(DBNull.Value);

                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__FreezeTable", parameters.ToArray()))
                {
                    while (reader.Read())
                    {
                        ReadSingleRow((IDataReader)reader);                        
                    }                   
                }
                parameters.Clear();

                return Lock;
            }
            catch
            {
                return false;
            }
        }

        private static void ReadSingleRow(IDataRecord reader)
        {
            Lock = UtilsGeneral.ToBool(reader["OutputResult"]);
            UserName = reader["UserName"].ToString();
            FreezeTime = UtilsGeneral.ToDateTime(reader["FreezeTime"]);
            Observations = reader["Observations"].ToString();
        }

        #endregion Check Lock

        #region Lock & Unlock ( Freeze / UnFreeze)

        public static bool LockUnlockTable(string tableName, FreezeMode freezeMode, DateTime? freezeTime, string observations)
        {
            try
            {
                List<object> parameters = new List<object>();

                parameters.Add((byte)FreezeType.LockMode);
                parameters.Add(LoginClass.userID);
                parameters.Add(tableName);
                parameters.Add((byte)freezeMode);
                parameters.Add(freezeTime);
                parameters.Add(observations);
                parameters.Add(false);
                parameters.Add("");
                parameters.Add(DateTime.Now);
                parameters.Add("");

                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__FreezeTable", parameters.ToArray()))
                {
                    if (reader.Read())
                        Lock = UtilsGeneral.ToBool(reader["OutputResult"]);
                    else
                        Lock = false;
                }
                parameters.Clear();


                return Lock;
            }
            catch
            {
                return false;
            }
        }

        #endregion Lock & Unlock ( Freeze / UnFreeze)
    }
}
