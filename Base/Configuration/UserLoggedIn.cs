using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.DataBase;

namespace Base.Configuration
{
    public class UserLoggedIn
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string UserName { get; set; }
        public bool IsTopManagement { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Functie { get; set; }
        public List<UserRight> UserRights { get; set; }
        public List<UserRight> UserRightsNew { get; set; }


        public static void GetConfigurableEmails(ref List<string> mails, string code)
        {
            using (RightsLinqDataContext context = new RightsLinqDataContext())
            {
                ApplicationParameter param = context.ApplicationParameters.FirstOrDefault(u => u.ParameterCode == code);
                if (null == param) return;
                string[] emails = param.ParameterValue.Split(';');
                mails.AddRange(emails);
            }
        }
    }
}
