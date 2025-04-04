using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Imports
{
    public class CardFactura : IEquatable<CardFactura>
    {
        #region Properties

        public string Descriere
        { get; set; }

        public decimal? SumaFaraTVA
        { get; set; }

        public int? CotaTVAID
        { get; set; }
        

        public decimal? SumaCuTVA
        { get; set; }

        public decimal? Cantitate
        { get; set; }

        public decimal? PretUnitar
        { get; set; }

        public decimal? VATAmount
        { get; set; }

        public string NrFactura
        { get; set; }

        public int? ContDebit_ID
        { get; set; }

        public int? ContCredit_ID
        { get; set; }

        public int? ContDebitImport_ID
        { get; set; }

        public int? ValutaID
        {
            get;
            set;
        }

        public int? ProdusCardID
        {
            get;
            set;
        }

        public DateTime DataFactura
        { get; set; }

        public DateTime DataLivrare
        { get; set; }

        public int CodTara
        { get; set; }

        public decimal? SumaCuTVATrx
        { get; set; }

        public decimal? SumaFaraTVATrx
        { get; set; }

        public decimal? TVATrx
        { get; set; }

        public int? CentruProfit_ID
        { get; set; }

        public int? SubcentruProfit1_ID
        { get; set; }

        public int? TipTVA_ID
        { get; set; }

        public decimal? CursValutar
        { get; set; }
        #endregion Properties

        #region Constructors

        public CardFactura()
        {

        }

        #endregion Constructors

        #region IEquatable<CardFactura> Members

        public bool Equals(CardFactura other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;

            //return (ContDebit_ID.Equals(other.ContDebit_ID) && ContCredit_ID.Equals(other.ContCredit_ID) 
            //        && Descriere.Equals(other.Descriere) && Cantitate.Equals(other.Cantitate) && PretUnitar.Equals(other.PretUnitar)
            //        && SumaFaraTVA.Equals(other.SumaFaraTVA) && SumaCuTVA.Equals(other.SumaCuTVA));         
            return (ContDebit_ID.Equals(other.ContDebit_ID) && ContCredit_ID.Equals(other.ContCredit_ID)
                    && Descriere.Equals(other.Descriere) && DataFactura.Date.Year.Equals(other.DataFactura.Date.Year) &&
                    DataFactura.Date.Month.Equals(other.DataFactura.Date.Month) && DataLivrare.Date.Year.Equals

(other.DataLivrare.Date.Year) &&
                    DataLivrare.Date.Month.Equals(other.DataLivrare.Date.Month)
                    && SumaFaraTVA.Equals(other.SumaFaraTVA));// && Cantitate.Equals(other.Cantitate) && PretUnitar.Equals
                                                              //(other.PretUnitar)
                                                              //&& SumaFaraTVA.Equals(other.SumaFaraTVA) && SumaCuTVA.Equals(other.SumaCuTVA));         
        }

        public override int GetHashCode()
        {
            int hashContDebit = ContDebit_ID == null ? 0 : ContDebit_ID.GetHashCode();
            int hashContCredit = ContCredit_ID == null ? 0 : ContCredit_ID.GetHashCode();
            int hashPretUnitar = PretUnitar == null ? 0 : PretUnitar.GetHashCode();
            int hashDescriere = Descriere.GetHashCode();
            int hashSumaFaraTVA = SumaFaraTVA == null ? 0 : SumaFaraTVA.GetHashCode();
            int hashSumaCuTVA = SumaCuTVA == null ? 0 : SumaCuTVA.GetHashCode();
            int hashCantitate = Cantitate == null ? 0 : Cantitate.GetHashCode();
            int hashDataFacturareAn = DataFactura.Date == null ? 0 : DataFactura.Date.Year.GetHashCode();
            int hashDataFacturareLuna = DataFactura.Date == null ? 0 : DataFactura.Date.Month.GetHashCode();
            int hashDataLivrareAn = DataLivrare.Date == null ? 0 : DataLivrare.Date.Year.GetHashCode();
            int hashDataLivrareLuna = DataLivrare.Date == null ? 0 : DataLivrare.Date.Month.GetHashCode();
            //return hashContDebit ^ hashContCredit ^ hashPretUnitar ^ hashDescriere ^ hashSumaFaraTVA ^ hashSumaCuTVA;
            //return hashContDebit ^ hashContCredit ^ hashDescriere; // ^ hashSumaFaraTVA ^ hashSumaCuTVA;
            return hashContDebit ^ hashContCredit ^ hashDescriere ^ hashDataFacturareLuna ^ hashDataFacturareAn ^ hashDataLivrareLuna ^ hashDataLivrareAn;
            //return hashContDebit ^ hashContCredit ^ hashPretUnitar ^ hashCantitate ^ hashDescriere ^ hashSumaFaraTVA ^ 

            //hashSumaCuTVA;
        }

        #endregion IEquatable<CardFactura> Members
    }

    public class VATAmountObject : IEquatable<VATAmountObject>
    {
        #region Properties

        public decimal? VatAmount
        { get; set; }

        public int? ContCredit
        { get; set; }

        public int? ContDebit
        { get; set; }

        public int? ContDebitImport_ID
        { get; set; }

        public int? ProdusCard_ID
        { get; set; }

        public int? CentruProfit_ID
        { get; set; }

        public int? SubcentruProfit1_ID
        { get; set; }

        public int? TipTVA_ID
        { get; set; }

        #endregion Properties

        #region Constructors

        public VATAmountObject() { }

        #endregion Constructors

        #region IEquatable<VATAmountObject> Members

        public bool Equals(VATAmountObject other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return (ContDebit.Equals(other.ContDebit) && ContCredit.Equals(other.ContCredit));
        }

        public override int GetHashCode()
        {
            int hashContDebit = ContDebit == null ? 0 : ContDebit.GetHashCode();
            int hashContCredit = ContCredit == null ? 0 : ContCredit.GetHashCode();
            return hashContDebit ^ hashContCredit;
        }

        #endregion IEquatable<VATAmountObject> Members
    }

    public class FacturaCardMultipla
    {
        #region Properties
        public IEnumerable<CardFactura> Factura
        { get; set; }

        public IEnumerable<VATAmountObject> VATAmountField
        { get; set; }

        public string NrFactura
        { get; set; }

        public int? FurnizorImportat
        { get; set; }

        public int? FurnizorID
        { get; set; }

        public int? TaraID
        { get; set; }

        public DateTime? DataFactura
        { get; set; }

        public int? ValutaID
        { get; set; }

        public int? CodTara
        { get; set; }

        public int? ContFurnizorFCaraus_ID
        { get; set; }

        public int? ClasaCashFlow
        { get; set; }

        public int? CotaTva
        { get; set; }

        public int? TipTVA
        { get; set; }

        public string MonedaTranzactiei
        { get; set; }

        #endregion Properties

        #region Constructors

        public FacturaCardMultipla()
        {

        }

        #endregion Constructors
    }
}
