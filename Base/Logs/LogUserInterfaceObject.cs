namespace Base.Logs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LogUserInterfaceObject
    {
        #region Members
        
        /// <summary>
        /// Cheia primara din tabel.
        /// </summary>
        public string PrimaryKeyName { get; set; }

        /// <summary>
        /// Select-ul pentru aducerea datelor.
        /// </summary>
        public string SelectString { get; set; }

        /// <summary>
        /// VisibleFieldNames - campurile vizibile (acestea trebuie sa aiba denumirea exact la fel cu cele din baza de date).
        /// Cu virugla intre ele. Fara spatii dupa / inainte de virgule.
        /// </summary>
        public string VisibleFieldNames { get; set; }

        /// <summary>
        /// Captions - cum se doreste sa apara caption-ul pe fiecare camp in parte.
        /// Cu virugla intre ele. Fara spatii dupa / inainte de virgule.
        /// </summary>
        public string Captions { get; set; }    
      

        #endregion Members

        #region Constructors

        public LogUserInterfaceObject()
        {

        }

        #endregion Constructors
    }
}
