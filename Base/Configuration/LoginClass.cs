using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using Base.BaseUtils;
using System.Linq;

namespace Base.Configuration
{
    public static class LoginClass
    {
        public static int userID = -1;
        public static int groupID = -1;
        public static DataSet globalDataset;
        public static int filialaID = 0;
        public static string filtruFiliala = "1 = 1";
        public static int filialaOrasID = 0;
        public static string filialaPrescurtare = "";
        public static string filialaAdresa = "";
        public static int persoanaID = -1;
        public static UserLoggedIn CurrentUser { get; set; }
        public static bool IsLogginOn = false;
        public static string gpsKey = "";
        public static UserLoggedIn CurrentUserNew { get; set; }
        public enum LoginMode
        {
            Insert,
            Validate
        }
        public static int gestiuneImplicitaID = -1;
        public static int casierieImplcicitaID = -1;
        public static int serieAvizImplicitaId = -1;
        public static int serieFacturaImplicitaID = -1;
        public static int centruProfitImplicitId = -1;
        public static int subcentruProfitImplicitID = -1;
        public static int cashFlowImplicitID = -1;

        public static int DoLogin(string user, string passwd, LoginMode lm, int PC_ID, int groupID)
        {
            String strConn = ConfigurationManager.ConnectionStrings["WindNet.Properties.Settings.WindConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(passwd));

            cmd.Parameters.AddWithValue("@Username", user);

            SqlParameter param1;
            param1 = new SqlParameter("@Password", SqlDbType.Binary, 16);
            param1.Value = hashedBytes;
            cmd.Parameters.Add(param1);

            SqlParameter param3;
            param3 = new SqlParameter("@LoginMode", SqlDbType.Int, 4);
            param3.Value = (int)lm;
            cmd.Parameters.Add(param3);

            cmd.Parameters.AddWithValue("@PersoaneContact_ID", PC_ID);
            cmd.Parameters.AddWithValue("@Group_ID", groupID);

            cmd.Parameters.AddWithValue("@IsNew", 1);

            SqlParameter param2;
            param2 = new SqlParameter("@Result", SqlDbType.Int, 4);
            param2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param2);

            cmd.CommandText = "sp__do_Login";
            try
            {
                conn.Open();

                SqlCommand proc = new SqlCommand("sp__do_Login", conn);
                proc.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(proc);
                if (proc.Parameters.Count != cmd.Parameters.Count + 1)// + 1 deoarece procedura returneaza in plus @RETURN_VALUE
                {
                    return -2;
                }

                if (lm == LoginMode.Insert)
                    cmd.ExecuteNonQuery();
                else
                {
                    globalDataset.Tables.Clear();
                    da.Fill(globalDataset);
                    globalDataset.Tables[0].TableName = "UserInfo";
                    globalDataset.Tables[1].TableName = "PermsPanou";
                    globalDataset.Tables[2].TableName = "PermsTopMenu";
                    globalDataset.Tables[3].TableName = "PermsSideMenu";
                    globalDataset.Tables[4].TableName = "PermsForm";
                    globalDataset.Tables[5].TableName = "PermsNomenclator";
                }
            }
            catch
            {
                MessageBox.Show("Eroare la conectarea cu baza de date !", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }

            return Base.BaseUtils.UtilsGeneral.ToInteger(cmd.Parameters["@Result"].Value, 0);
        }

        public static int DoLogin(string user, string passwd)
        {
            return DoLogin(user, passwd, LoginMode.Validate, -1, -1);
        }

        public static int UpdateUsername(string Username, int PersoaneContact_ID)
        {
            String strConn = ConfigurationManager.ConnectionStrings["WindNet.Properties.Settings.WindConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            int rezultat = -1;
            cmd.CommandText = "sp__Users_update_Name";
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@PersoaneContact_ID", PersoaneContact_ID);
            SqlParameter p1 = new SqlParameter("@Result", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);
            cmd.ExecuteNonQuery();
            rezultat = Base.BaseUtils.UtilsGeneral.ToInteger(cmd.Parameters["@Result"].Value, -1);
            conn.Close();
            return rezultat;
        }

        public static int UpdateGroupName(int GroupID, int PersoaneContact_ID)
        {
            String strConn = ConfigurationManager.ConnectionStrings["WindNet.Properties.Settings.WindConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            int rezultat = -1;
            cmd.CommandText = "sp__Users_update_Group";
            cmd.Parameters.AddWithValue("@GroupID", GroupID);
            cmd.Parameters.AddWithValue("@PersoaneContact_ID", PersoaneContact_ID);
            SqlParameter p1 = new SqlParameter("@Result", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);
            cmd.ExecuteNonQuery();
            rezultat = Base.BaseUtils.UtilsGeneral.ToInteger(cmd.Parameters["@Result"].Value, -1);
            conn.Close();
            return rezultat;
        }

        public static int UpdateActiv(bool Activ, int PersoaneContact_ID)
        {
            String strConn = ConfigurationManager.ConnectionStrings["WindNet.Properties.Settings.WindConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            int rezultat = -1;
            cmd.CommandText = "sp__Users_update_Activ";
            cmd.Parameters.AddWithValue("@Activ", Activ);
            cmd.Parameters.AddWithValue("@PersoaneContact_ID", PersoaneContact_ID);
            SqlParameter p1 = new SqlParameter("@Result", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);
            cmd.ExecuteNonQuery();
            rezultat = Base.BaseUtils.UtilsGeneral.ToInteger(cmd.Parameters["@Result"].Value, -1);
            conn.Close();
            return rezultat;
        }

        public static int AddGroup(string GroupName)
        {
            String strConn = ConfigurationManager.ConnectionStrings["WindNet.Properties.Settings.WindConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            int rezultat = -1;
            cmd.CommandText = "sp__Users_insert_Group";
            cmd.Parameters.AddWithValue("@GroupName", GroupName);
            SqlParameter p1 = new SqlParameter("@Result", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);
            cmd.ExecuteNonQuery();
            rezultat = Base.BaseUtils.UtilsGeneral.ToInteger(cmd.Parameters["@Result"].Value, -1);
            conn.Close();
            return rezultat;
        }

        public static int UpdatePassword(string Password, int PersoaneContact_ID)
        {
            String strConn = ConfigurationManager.ConnectionStrings["WindNet.Properties.Settings.WindConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            int rezultat = -1;

            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(Password));

            cmd.CommandText = "sp__Users_update_Password";
            cmd.Parameters.AddWithValue("@Password", hashedBytes);
            cmd.Parameters.AddWithValue("@PersoaneContact_ID", PersoaneContact_ID);
            SqlParameter p1 = new SqlParameter("@Result", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);
            cmd.ExecuteNonQuery();
            rezultat = Base.BaseUtils.UtilsGeneral.ToInteger(cmd.Parameters["@Result"].Value, -1);
            conn.Close();
            return rezultat;
        }

        public static bool CheckPermission(string tableName, string camp)
        {
            //if (LoginClass.groupID == 1) return true; //pentru administratori

            //if (globalDataset.Tables.Contains(tableName))
            //    foreach (DataRow row in globalDataset.Tables[tableName].Rows)
            //        if (row["PermsName"].ToString() == camp)
            //            return true;
            //return false;
            return true;
        }


        public static bool HasRightsToOpenForm(string formType)
        {
            //if (!CheckPermission("PermsForm", formType))
            //    return false;

            return true;
        }

        public static void GetAlertPlaces()
        {
            try
            {
                DataSet ds = new DataSet();
                String strConn = ConfigurationManager.ConnectionStrings["WindNet.Properties.Settings.WindConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(strConn);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetPlacesAlert";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (Base.BaseUtils.UtilsGeneral.ToBool(ds.Tables[1].Rows[0].ItemArray[0]))
                {
                    List<String> sb = new List<String>();
                    foreach (DataRow detail in ds.Tables[0].Rows)
                    {
                        sb.Add(detail.ItemArray[0].ToString());
                    }

                    List<string> emails = new List<string>();

                    UserLoggedIn.GetConfigurableEmails(ref emails, "PlaceMails");

                    if (emails.Count == 0) return;

                    UtilsGeneral.SendCustomEmail(emails, "Place-uri nealocate", sb.Aggregate((a, b) => a + ", " + b));
                }
            }
            catch 
            {
                
            }
        }

        public static int UpdatePasswordNew(string Password, int userId)
        {
            String strConn = ConfigurationManager.ConnectionStrings["WindNet.Properties.Settings.WindConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            int rezultat = -1;

            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(Password));

            cmd.CommandText = "sp__Users_Update_Password_New";
            cmd.Parameters.AddWithValue("@password", hashedBytes);
            cmd.Parameters.AddWithValue("@userId", userId);
            SqlParameter p1 = new SqlParameter("@result", SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p1);
            cmd.ExecuteNonQuery();
            rezultat = Base.BaseUtils.UtilsGeneral.ToInteger(cmd.Parameters["@result"].Value, -1);
            conn.Close();
            return rezultat;
        }
    }
}
