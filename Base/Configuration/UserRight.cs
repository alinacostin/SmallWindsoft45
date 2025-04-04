using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.DataBase;
using System.Data.Linq;
using System.Transactions;
using System.Data;
using Base.BaseUtils;

namespace Base.Configuration
{
    public class UserRight
    {
        public string DepartmentRightPattern { get; set; }
        public string EntityName { get; set; }
        public int EntityType { get; set; }
        public string RightPattern { get; set; }

        public static UserLoggedIn LoginUser(int userID)
        {
            UserLoggedIn user=new UserLoggedIn();
            
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                using (RightsLinqDataContext context = new RightsLinqDataContext())
                {
                    DataLoadOptions options = new DataLoadOptions();

                    var loginData = (from u in context.Users.Where(u => u.User_ID == userID)
                                     from x in
                                         (from uer in context.UserEntityRights
                                          join e in context.Entities on uer.EntityId equals e.EntityId
                                          join r in context.Rights on uer.RightId equals r.RightId
                                          where u.Group_ID == uer.GroupId
                                          select new UserRight
                                          {
                                              EntityName = e.EntityName
                                              ,
                                              EntityType = e.EntityTypeId
                                              ,
                                              RightPattern = r.RightPattern
                                          }).DefaultIfEmpty()
                                     select new
                                     {
                                         u.Group_ID,
                                         u.User_ID
                                         ,
                                         u.Username
                                         ,
                                         Right = x.EntityType == null ? null : new UserRight
                                         {
                                             EntityName = x.EntityName
                                             ,
                                             EntityType = x.EntityType
                                             ,
                                             RightPattern = x.RightPattern
                                         }
                                     }).ToList();

                    user = (from x in loginData
                            group x by new { x.User_ID, x.Username, x.Group_ID }
                                into g
                                select new UserLoggedIn
                                {
                                    GroupId = Convert.ToInt32(g.Key.Group_ID),
                                    UserId = g.Key.User_ID
                                    ,
                                    UserName = g.Key.Username
                                    ,
                                    UserRights = g.Select(z => z.Right).Where(z => null != z).Distinct().ToList()
                                }).SingleOrDefault();
                }
            }
            

            DataSet ds = WindDatabase.ExecuteDataSet("SELECT User_ID, Username, Group_ID, ISNULL(LF.Functie,'') AS Functie from Users LEFT JOIN Firme_Persoane_Contact AS FPC WITH (NOLOCK) ON FPC.PersoaneContact_ID = Users.PersoaneContact_ID LEFT JOIN Lista_Functii AS LF WITH (NOLOCK) ON LF.Functie_ID = FPC.FunctiePC_ID WHERE User_ID = " + userID);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            user.GroupId = UtilsGeneral.ToInteger(ds.Tables[0].Rows[0]["Group_ID"], 0);
            user.UserName = ds.Tables[0].Rows[0]["Username"].ToString();
            user.UserId= UtilsGeneral.ToInteger(ds.Tables[0].Rows[0]["User_ID"], 0);
            user.Functie = ds.Tables[0].Rows[0]["Functie"].ToString();


            return user;
        }

        public static UserLoggedIn LoginUserNew(int userID)
        {
            UserLoggedIn user = new UserLoggedIn();
            user.UserRightsNew = new List<UserRight>();
            DataSet ds = WindDatabase.ExecuteDataSet("sp_Rights_User", new object[] { LoginClass.userID });
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                user.GroupId = UtilsGeneral.ToInteger(dr["GroupId"], 0);
                user.IsTopManagement = UtilsGeneral.ToBool(dr["IsTopManagement"]);
                user.UserId = UtilsGeneral.ToInteger(dr["UserId"], 0);
                user.UserName = UtilsGeneral.ToString(dr["UserName"]);
            }

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                UserRight us = new UserRight();
                us.EntityName = UtilsGeneral.ToString(dr["EntityName"]);
                us.EntityType = UtilsGeneral.ToInteger(dr["EntityType"], 0);
                us.RightPattern = UtilsGeneral.ToString(dr["RightPattern"]);
                user.UserRightsNew.Add(us);
            }
            return user;
        }

    }
}
