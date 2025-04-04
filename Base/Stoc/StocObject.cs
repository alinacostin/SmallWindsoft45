using System;

namespace Base.Stoc
{
    public class StocObject : Object, IComparable
    {
        #region Properties

        public int StocID
        {
            get;
            set;
        }

        public object NirDetaliiID
        {
            get;
            set;
        }

        public object AvizInsotireMarfaDetaliiIntrareID
        {
            get;
            set;
        }

        public object AlteDocumenteGestiuneDetaliiIntrareID
        {
            get;
            set;
        }

        public object StocInitialID
        {
            get;
            set;
        }

        public object BonTransferDetaliiIntrareID
        {
            get;
            set;
        }

        public object OriginalNirDetalii_ID
        {
            get;
            set;
        }

        public int ProdusID
        {
            get;
            set;
        }

        public int GestiuneID
        {
            get;
            set;
        }

        public int FilialaID
        {
            get;
            set;
        }

        public int? ComandaClientID
        {
            get;
            set;
        }

        public int? CodProdusEchivalentID
        {
            get;
            set;
        }

        public DateTime DataStoc
        {
            get;
            set;
        }

        public decimal PretUnitar
        {
            get;
            set;
        }

        public decimal Cantitate
        {
            get;
            set;
        }

        public bool Modificat
        {
            get;
            set;
        }

        #endregion Properties

        #region Compare Objects

        public bool CompareTo(StocObject s1, StocObject s2)
        {
            return (s1.GestiuneID == s2.GestiuneID && s1.ProdusID == s2.ProdusID
                && s1.FilialaID == s2.FilialaID && s1.PretUnitar == s2.PretUnitar
                && s1.ComandaClientID == s2.ComandaClientID);
            //&& s1.CodProdusEchivalentID == s2.CodProdusEchivalentID);
        }

        public bool IsEqual(StocObject s1, StocObject s2)
        {
            return (s1.GestiuneID == s2.GestiuneID && s1.ProdusID == s2.ProdusID
                && s1.FilialaID == s2.FilialaID && s1.PretUnitar == s2.PretUnitar
                && s1.ComandaClientID == s2.ComandaClientID);
            //&& s1.CodProdusEchivalentID == s2.CodProdusEchivalentID);
        }

        public override int GetHashCode()
        {
            return FilialaID ^ GestiuneID ^ ProdusID ^ ComandaClientID.GetHashCode() ^
                PretUnitar.GetHashCode() ^ DataStoc.Date.GetHashCode();
            //return FilialaID ^ GestiuneID ^ ProdusID ^ ComandaClientID.GetHashCode() ^ CodProdusEchivalentID.GetHashCode() ^
            //    PretUnitar.GetHashCode() ^ DataStoc.Date.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is StocObject)
            {
                StocObject temp = obj as StocObject;
                if (temp == null) return false;
                return (GestiuneID == temp.GestiuneID && ProdusID == temp.ProdusID && FilialaID == temp.FilialaID
                    && PretUnitar == temp.PretUnitar && ComandaClientID == temp.ComandaClientID
                    //&& CodProdusEchivalentID == temp.CodProdusEchivalentID
                    && DataStoc.Date == temp.DataStoc.Date);
            }
            else return false;
        }

        #endregion Compare Objects

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return this.DataStoc.Date.CompareTo((obj as StocObject).DataStoc.Date);
        }

        #endregion IComparable Members
    }
}