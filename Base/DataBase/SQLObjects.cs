using System;
using System.Collections;
using System.Collections.Generic;

namespace Base.DataBase
{
    #region SQL Objects Collection

    public class SQLObjects : IDisposable
    {
        #region Members

        private SQLObjectsCollection sqlObjectCollection = new SQLObjectsCollection();

        #endregion

        #region Properties

        public int Count
        {
            get
            {
                return this.sqlObjectCollection.Count;
            }
        }

        public object this[int index]
        {
            get
            {
                return this.sqlObjectCollection[index];
            }
        }

        #endregion

        #region Add Overloaded Methods

        public void Add(TemplateStoreProc templateStoreProcObj)
        {
            this.sqlObjectCollection.Add(templateStoreProcObj);
        }

        public void Add(StandardStoreProc standardStoreProcObj)
        {
            this.sqlObjectCollection.Add(standardStoreProcObj);
        }

        public void Add(Query queryObj)
        {
            this.sqlObjectCollection.Add(queryObj);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.sqlObjectCollection.Clear();
            this.sqlObjectCollection = null;
        }

        #endregion
    }

    #endregion

    #region Template Store Procedure Class

    public class TemplateStoreProc
    {
        #region Members

        private string localTableName;
        private string serverTableName;
        private string fieldsNames = "*";
        private string whereCond = "";
        private string groupCond = "";
        private string sortCond = "";

        #endregion

        #region Properties

        public string LocalTableName
        {
            get { return this.localTableName; }
        }

        public string ServerTableName
        {
            get { return this.serverTableName; }
        }

        public string Fields
        {
            get { return this.fieldsNames; }
            set { this.fieldsNames = value; }
        }

        public string Where
        {
            get { return this.whereCond; }
            set { this.whereCond = value; }
        }

        public string Group
        {
            get { return this.groupCond; }
            set { this.groupCond = value; }
        }

        public string Sort
        {
            get { return this.sortCond; }
            set { this.sortCond = value; }
        }

        #endregion

        #region Constructors

        public TemplateStoreProc(string localAndServerTableName)
        {
            this.localTableName = localAndServerTableName;
            this.serverTableName = localAndServerTableName;
        }

        public TemplateStoreProc(string localTableName, string serverTableName)
        {
            this.localTableName = localTableName;
            this.serverTableName = serverTableName;
        }

        #endregion
    }

    #endregion

    #region Standard Store Procedure Class

    public class StandardStoreProc
    {
        #region Members

        private string spName;
        private object[] spParams;

        #endregion

        #region Properties

        public string Name
        {
            get { return this.spName; }
        }

        public object[] Parameters
        {
            get { return this.spParams; }
        }

        #endregion

        #region Overloaded constructors

        public StandardStoreProc(string spName)
        {
            this.spName = spName;
        }

        public StandardStoreProc(string spName, object[] spParams)
        {
            this.spName = spName;
            this.spParams = spParams;
        }

        #endregion
    }

    #endregion

    #region Query Class

    public class Query
    {
        #region Members

        private string queryString;

        #endregion

        #region Properties

        public string QueryString
        {
            get { return this.queryString; }
        }

        #endregion

        #region Overloaded constructors

        public Query(string queryString)
        {
            this.queryString = queryString;
        }

        #endregion
    }

    #endregion

    #region SQL Object Collection Class

    public class SQLObjectsCollection : CollectionBase
    {
        public object this[int index]
        {
            get
            {
                return List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        public void Add(object sqlObject)
        {
            List.Add(sqlObject);
        }
    }

    #endregion
}
