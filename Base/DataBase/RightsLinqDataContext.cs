using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Base.DataBase
{
    public partial class RightsLinqDataContext
    {
        static String strConn = ConfigurationManager.ConnectionStrings["WindNet.Properties.Settings.WindConnectionString"].ConnectionString;


        public RightsLinqDataContext() :
            base(strConn, mappingSource)
        {
            OnCreated();
        }

        public RightsLinqDataContext(System.Data.IDbConnection connection) :
            base(strConn, mappingSource)
        {
            OnCreated();
        }

        public RightsLinqDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(strConn, mappingSource)
        {
            OnCreated();
        }

        public RightsLinqDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(strConn, mappingSource)
        {
            OnCreated();
        }
    }
}
