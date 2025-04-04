using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.BaseUtils;
using Base.Configuration;
using System.Data.SqlClient;
using Base.DataBase;
using System.Globalization;
using System.Collections;

namespace Base.Imports
{
    public class CombustibilCard
    {

        #region Properties

        public string Tara
        {
            get;
            set;
        }

        public string CodStatie
        {
            get;
            set;
        }

        public string LocatieStatie
        {
            get;
            set;
        }

        public DateTime? DataLivrare
        {
            get;
            set;
        }

        public DateTime? OraLivrare
        {
            get;
            set;
        }

        public DateTime? DataFacturare
        {
            get;
            set;
        }

        public string IntrareLocatie
        {
            get;
            set;
        }

        public string IesireLocatie
        {
            get;
            set;
        }

        public string NrAuto
        {
            get;
            set;
        }

        public string NrFactura
        {
            get;
            set;
        }

        public decimal? Kilometri
        {
            get;
            set;
        }

        public string Produs
        {
            get;
            set;
        }

        public decimal? Cantitate
        {
            get;
            set;
        }

        public string Valuta
        {
            get;
            set;
        }

        public decimal? PretPerLitru
        {
            get;
            set;
        }

        public decimal? ValoareFaraTVA
        {
            get;
            set;
        }

        public decimal? ValoareTVA
        {
            get;
            set;
        }

        public decimal? ValoareCuTVA
        {
            get;
            set;
        }
        public decimal? ValoareFaraTVAMoneda
        {
            get;
            set;
        }

        public decimal? ValoareTVAMoneda
        {
            get;
            set;
        }

        public decimal? ValoareCuTVAMoneda
        {
            get;
            set;
        }
        public decimal? CursValutar
        {
            get;
            set;
        }

        public decimal? VATAmount
        {
            get;
            set;
        }

        public string Kilometraj
        {
            get;
            set;
        }

        public string Retea
        {
            get;
            set;
        }

        public string CardPan
        {
            get;
            set;
        }



        public string CodRuta
        {
            get;
            set;
        }


        public int? ClasaVehicul
        {
            get;
            set;
        }

        public int? FurnizorImportat
        {
            get;
            set;
        }

        public int? NrAutoID
        {
            get;
            set;
        }

        public int? NrAutoTertiID
        {
            get;
            set;
        }

        public int? ProdusID
        {
            get;
            set;
        }

        public int? CardID
        {
            get;
            set;
        }

        public int? FacturaPrimita_ID
        {
            get;
            set;
        }

        public int? CodTara
        {
            get;
            set;
        }

        public int HashCode
        {
            get;
            set;
        }       

        #endregion Properties

        #region Compute HashCodes

        public void ComputeHashCode(bool isFromFinanciar)
        {
            if (isFromFinanciar)
            {
                HashCode = (CardPan == null ? string.Empty : CardPan).GetHashCode() ^
                           (FurnizorImportat == null ? 0 : FurnizorImportat).GetHashCode() ^
                           (DataLivrare == null ? 0 : DataLivrare.Value.Date.Ticks).GetHashCode() ^
                           (NrFactura == null ? string.Empty : NrFactura).GetHashCode() ^
                           //(ProdusID == null ? 0 : (int)ProdusID).GetHashCode() ^
                           (ValoareCuTVA == null ? 0 : ValoareCuTVA).GetHashCode() ^
                           (ValoareFaraTVA == null ? 0 : ValoareFaraTVA).GetHashCode() ^
                           (FacturaPrimita_ID == null ? 0 : FacturaPrimita_ID).GetHashCode() ^
                           (ValoareTVA == null ? 0 : ValoareTVA).GetHashCode();
            }
            else
            {
                HashCode = (CardPan == null ? string.Empty : CardPan).GetHashCode() ^
                          (FurnizorImportat == null ? 0 : FurnizorImportat).GetHashCode() ^
                          (DataLivrare == null ? 0 : DataLivrare.Value.Date.Ticks).GetHashCode() ^
                          (CodStatie == null ? string.Empty : CodStatie).GetHashCode() ^
                          (OraLivrare == null ? 0 : OraLivrare.Value.TimeOfDay.Ticks).GetHashCode() ^
                          (Cantitate == null ? 0 : Cantitate.Value).GetHashCode();
                          //(NrFactura == null ? string.Empty : NrFactura).GetHashCode() ^
                          //(ProdusID == null ? 0 : (int)ProdusID).GetHashCode();
                          //(ValoareCuTVA == null ? 0 : ValoareCuTVA).GetHashCode() ^
                          //(ValoareFaraTVA == null ? 0 : ValoareFaraTVA).GetHashCode() ^
                          //(FacturaPrimita_ID == null ? 0 : FacturaPrimita_ID).GetHashCode() ^
                          //(ValoareTVA == null ? 0 : ValoareTVA).GetHashCode();
            }
        }

        public void ComputeHashCodeWithoutStationCode(bool isFromFinanciar)
        {
            if (isFromFinanciar)
            {
                HashCode = (CardPan == null ? string.Empty : CardPan).GetHashCode() ^
                           (FurnizorImportat == null ? 0 : FurnizorImportat).GetHashCode() ^
                           (DataLivrare == null ? 0 : DataLivrare.Value.Date.Ticks).GetHashCode() ^
                           (NrFactura == null ? string.Empty : NrFactura).GetHashCode() ^
                           //(ProdusID == null ? 0 : (int)ProdusID).GetHashCode() ^
                           (ValoareCuTVA == null ? 0 : ValoareCuTVA).GetHashCode() ^
                           (ValoareFaraTVA == null ? 0 : ValoareFaraTVA).GetHashCode() ^
                           (FacturaPrimita_ID == null ? 0 : FacturaPrimita_ID).GetHashCode() ^
                           (ValoareTVA == null ? 0 : ValoareTVA).GetHashCode();
            }
            else
            {
                HashCode = (CardPan == null ? string.Empty : CardPan).GetHashCode() ^
                          (FurnizorImportat == null ? 0 : FurnizorImportat).GetHashCode() ^
                          (DataLivrare == null ? 0 : DataLivrare.Value.Date.Ticks).GetHashCode() ^
                          (string.Empty).GetHashCode() ^
                          (OraLivrare == null ? 0 : OraLivrare.Value.TimeOfDay.Ticks).GetHashCode() ^
                          (Cantitate == null ? 0 : Cantitate.Value).GetHashCode();
                          //(NrFactura == null ? string.Empty : NrFactura).GetHashCode() ^
                          //(ProdusID == null ? 0 : (int)ProdusID).GetHashCode();
                          //(ValoareCuTVA == null ? 0 : ValoareCuTVA).GetHashCode() ^
                          //(ValoareFaraTVA == null ? 0 : ValoareFaraTVA).GetHashCode() ^
                          //(FacturaPrimita_ID == null ? 0 : FacturaPrimita_ID).GetHashCode() ^
                          //(ValoareTVA == null ? 0 : ValoareTVA).GetHashCode();
            }
        }

        public void ComputeHashCodeWithValoareCuTVA(bool isFromFinanciar)
        {
            if (isFromFinanciar)
            {
                HashCode = (CardPan == null ? string.Empty : CardPan).GetHashCode() ^
                           (FurnizorImportat == null ? 0 : FurnizorImportat).GetHashCode() ^
                           (DataLivrare == null ? 0 : DataLivrare.Value.Date.Ticks).GetHashCode() ^
                           (NrFactura == null ? string.Empty : NrFactura).GetHashCode() ^
                    //(ProdusID == null ? 0 : (int)ProdusID).GetHashCode() ^
                           (ValoareCuTVA == null ? 0 : ValoareCuTVA).GetHashCode() ^
                           (ValoareFaraTVA == null ? 0 : ValoareFaraTVA).GetHashCode() ^
                           (FacturaPrimita_ID == null ? 0 : FacturaPrimita_ID).GetHashCode() ^
                           (ValoareTVA == null ? 0 : ValoareTVA).GetHashCode();
            }
            else
            {
                HashCode = (CardPan == null ? string.Empty : CardPan).GetHashCode() ^
                          (FurnizorImportat == null ? 0 : FurnizorImportat).GetHashCode() ^
                          (DataLivrare == null ? 0 : DataLivrare.Value.Date.Ticks).GetHashCode() ^
                          (CodStatie == null ? string.Empty : CodStatie).GetHashCode() ^
                          (ValoareCuTVA == null ? 0 : ValoareCuTVA).GetHashCode() ^
                          (OraLivrare == null ? 0 : OraLivrare.Value.TimeOfDay.Ticks).GetHashCode() ^
                          (Cantitate == null ? 0 : Cantitate.Value).GetHashCode();
                //(NrFactura == null ? string.Empty : NrFactura).GetHashCode() ^
                //(ProdusID == null ? 0 : (int)ProdusID).GetHashCode();
                //(ValoareFaraTVA == null ? 0 : ValoareFaraTVA).GetHashCode() ^
                //(FacturaPrimita_ID == null ? 0 : FacturaPrimita_ID).GetHashCode() ^
                //(ValoareTVA == null ? 0 : ValoareTVA).GetHashCode();
            }
        }

        public static int ComputeHashCode(Hashtable parameters)
        {
            string cardPan = string.Empty;
            int furnizorImportat = 0;
            string produs = string.Empty;
            DateTime dataLivrare = new DateTime(1900, 1, 1);
            DateTime oraLivrare = new DateTime(1900, 1, 1);
            DateTime dataFactura = new DateTime(1900, 1, 1);
            string nrFactura = string.Empty;
            bool isFromFinanciar = false;
            decimal valoareCuTVA = 0.0M;
            decimal valoareFaraTVA = 0.0M;
            decimal valoareTVA = 0.0M;
            string codStatie = string.Empty;
            decimal cantitate = 0.0M;

            if (parameters.ContainsKey("CardPan"))
                cardPan = parameters["CardPan"].ToString();
            if (parameters.ContainsKey("FurnizorImportat"))
                furnizorImportat = UtilsGeneral.ToInteger(parameters["FurnizorImportat"], 0);
            if (parameters.ContainsKey("Produs"))
                produs = parameters["Produs"].ToString();
            if (parameters.ContainsKey("DataLivrare"))
                dataLivrare = UtilsGeneral.ToDateTime(parameters["DataLivrare"]);
            if (parameters.ContainsKey("OraLivrare"))
                oraLivrare = UtilsGeneral.ToDateTime(parameters["OraLivrare"]);
            if (parameters.ContainsKey("DataFactura"))
                dataFactura = UtilsGeneral.ToDateTime(parameters["DataFactura"]);
            if (parameters.ContainsKey("NrFactura"))
                nrFactura = parameters["NrFactura"].ToString();
            if (parameters.ContainsKey("CodStatie"))
                codStatie = parameters["CodStatie"].ToString();
            if (parameters.ContainsKey("IsFromFinanciar"))
                isFromFinanciar = UtilsGeneral.ToBool(parameters["IsFromFinanciar"]);
            if (parameters.ContainsKey("ValoareCuTVA"))
                valoareCuTVA = UtilsGeneral.ToDecimal(parameters["ValoareCuTVA"], 0.0M);
            if (parameters.ContainsKey("ValoareFaraTVA"))
                valoareFaraTVA = UtilsGeneral.ToDecimal(parameters["ValoareFaraTVA"], 0.0M);
            if (parameters.ContainsKey("ValoareTVA"))
                valoareTVA = UtilsGeneral.ToDecimal(parameters["ValoareTVA"], 0.0M);
            if (parameters.ContainsKey("Cantitate"))
                cantitate = UtilsGeneral.ToDecimal(parameters["Cantitate"], 0.0M);

            if (isFromFinanciar)
            {
                return (string.IsNullOrEmpty(cardPan) ? string.Empty : cardPan).GetHashCode() ^
                       furnizorImportat.GetHashCode() ^
                       (dataLivrare == new DateTime(1900, 1, 1) ? 0 : dataLivrare.Date.Ticks).GetHashCode() ^
                       (string.IsNullOrEmpty(nrFactura) ? string.Empty : nrFactura).GetHashCode() ^
                       (valoareCuTVA == 0.0M ? 0 : valoareCuTVA).GetHashCode() ^
                       (valoareFaraTVA == 0.0M ? 0 : valoareFaraTVA).GetHashCode() ^
                       0.GetHashCode() ^
                       (valoareTVA == 0.0M ? 0 : valoareTVA).GetHashCode();
            }
            else
            {
                return (string.IsNullOrEmpty(cardPan) ? string.Empty : cardPan).GetHashCode() ^
                       furnizorImportat.GetHashCode() ^
                       (dataLivrare == new DateTime(1900, 1, 1) ? 0 : dataLivrare.Date.Ticks).GetHashCode() ^
                       //se foloseste produs in Hash code doar pentru Shell
                       (furnizorImportat != (int)Base.Imports.ImportCombustibil.TipFurnizorImportat.Shel || string.IsNullOrEmpty(produs) ? 0 : produs.GetHashCode()) ^
                       (string.IsNullOrEmpty(codStatie) ? string.Empty : codStatie).GetHashCode() ^
                       (oraLivrare == new DateTime(1900, 1, 1) ? 0 : oraLivrare.TimeOfDay.Ticks).GetHashCode() ^
                       (cantitate == 0.0M ? 0 : cantitate).GetHashCode();
            }
        }

        #endregion Compute HashCodes

        #region DataBase Operations

        public void ExecuteSave(object cardObject, DateTime DataOraImport, string UserImport)
        {
            if (cardObject == null)
                return;

            if (cardObject is CombustibilCard)
            {
                CombustibilCard tempCard = (CombustibilCard)cardObject;                
                StringBuilder xmlString = new StringBuilder();


                DateTime correctMinimumSQLDT = new DateTime(1753, 1, 1);
                if (tempCard.DataFacturare == DateTime.MinValue)
                    tempCard.DataFacturare = correctMinimumSQLDT;
                if (tempCard.OraLivrare == DateTime.MinValue)
                {
                    var itms = new int[] { tempCard.OraLivrare.Value.Hour, tempCard.OraLivrare.Value.Minute, tempCard.OraLivrare.Value.Second };
                    tempCard.OraLivrare = correctMinimumSQLDT;
                    
                    tempCard.OraLivrare.Value.AddHours((double)itms[0]);
                    tempCard.OraLivrare.Value.AddMinutes((double)itms[1]);
                    tempCard.OraLivrare.Value.AddSeconds((double)itms[2]);

                }
                if (tempCard.DataLivrare == DateTime.MinValue)
                    tempCard.DataLivrare = correctMinimumSQLDT;



                xmlString.Append("<root><ImportCombustibilCard ");
                xmlString.Append(@"Tara= """ + tempCard.Tara + @""" ");
                xmlString.Append(@"CodStatie= """ + tempCard.CodStatie + @""" ");
                xmlString.Append(@"LocatieStatie= """ + tempCard.LocatieStatie + @""" ");
                xmlString.Append(@"DataLivrare= """ + UtilsGeneral.ToDateTime(tempCard.DataLivrare).ToString("MM-dd-yyyy").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"OraLivrare= """ + UtilsGeneral.ToDateTime(tempCard.OraLivrare).ToString("H:mm:ss").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"DataFacturare= """ + UtilsGeneral.ToDateTime(tempCard.DataFacturare).ToString("MM-dd-yyyy").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"IntrareLocatie= """ + tempCard.IntrareLocatie + @""" ");
                xmlString.Append(@"IesireLocatie= """ + tempCard.IesireLocatie + @""" ");
                xmlString.Append(@"NrAuto= """ + tempCard.NrAuto + @""" ");
                xmlString.Append(@"NrFactura= """ + tempCard.NrFactura + @""" ");
                xmlString.Append(@"Kilometri= """ + UtilsGeneral.ToDecimal(tempCard.Kilometri, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Produs= """ + tempCard.Produs + @""" ");
                xmlString.Append(@"Cantitate= """ + UtilsGeneral.ToDecimal(tempCard.Cantitate, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Valuta= """ + tempCard.Valuta + @""" ");
                xmlString.Append(@"PretPerLitru= """ + UtilsGeneral.ToDecimal(tempCard.PretPerLitru, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"ValoareFaraTVA= """ + UtilsGeneral.ToDecimal(tempCard.ValoareFaraTVA, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"ValoareTVA= """ + UtilsGeneral.ToDecimal(tempCard.ValoareTVA, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"ValoareCuTVA= """ + UtilsGeneral.ToDecimal(tempCard.ValoareCuTVA, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Kilometraj= """ + tempCard.Kilometraj + @""" ");
                xmlString.Append(@"Retea= """ + tempCard.Retea + @""" ");
                xmlString.Append(@"CardPan= """ + tempCard.CardPan + @""" ");
                xmlString.Append(@"CodRuta= """ + tempCard.CodRuta + @""" ");
                xmlString.Append(@"ClasaVehicul= """ + tempCard.ClasaVehicul.ToString() + @""" ");
                xmlString.Append(@"TipFurnizorImportat= """ + UtilsGeneral.ToInteger(tempCard.FurnizorImportat, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"NrAuto_ID= """ + UtilsGeneral.ToInteger(tempCard.NrAutoID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"NrAutoTerti_ID= """ + UtilsGeneral.ToInteger(tempCard.NrAutoTertiID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Produs_ID= """ + UtilsGeneral.ToInteger(tempCard.ProdusID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Card_ID= """ + UtilsGeneral.ToInteger(tempCard.CardID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"DataImport= """ + UtilsGeneral.ToDateTime(DataOraImport).ToString("MM-dd-yyyy").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"OraImport= """ + UtilsGeneral.ToDateTime(DataOraImport).ToString("H:mm:ss").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"UserImport= """ + UserImport + @""" ");
                xmlString.Append(@"HashCode= """ + tempCard.HashCode.ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append("></ImportCombustibilCard></root>");

                List<object> DbList = new List<object>();
                DbList.Add(xmlString.ToString());
                DbList.Add(LoginClass.userID);
                DbList.Add(0);
                // start saving...
                int returnValue = 0;
                int cID = 0;
                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__ImportCombustibilCard_Insert_Update", DbList.ToArray()))
                {
                    if (reader.Read())
                    {
                        returnValue = UtilsGeneral.ToInteger(reader["ReturnValue"], 0);
                        cID = UtilsGeneral.ToInteger(reader["ImportCombustibilID"], 0);
                    }
                    else
                    {
                        returnValue = 3;
                        cID = 0;
                    }
                }
                DbList.Clear();               

                if (returnValue == 1)
                {
                    throw new Exception(Constants.C_CONCURRENCY_UPDATE_FAILED);
                }
                else if (returnValue == 2)
                {
                   
                    throw new Exception(Constants.C_CONCURRENCY_UPDATE_FAILED_RECORD_DELETED);
                }
                else if (returnValue == 3) //eroare in procedura stocata
                    throw new Exception("A aparut o eroare la salvare. Operatiune esuata !");
            }
        }

        public void ExecuteSave(object cardObject, object operatiuneID)
        {
            if (cardObject == null)
                return;

            if (cardObject is CombustibilCard)
            {
                CombustibilCard tempCard = (CombustibilCard)cardObject;
                StringBuilder xmlString = new StringBuilder();

                
                xmlString.Append("<root><ImportCombustibilCard ");            
                xmlString.Append(@"NrAuto_ID= """ + UtilsGeneral.ToInteger(tempCard.NrAutoID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"NrAutoTerti_ID= """ + UtilsGeneral.ToInteger(tempCard.NrAutoTertiID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Produs_ID= """ + UtilsGeneral.ToInteger(tempCard.ProdusID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Card_ID= """ + UtilsGeneral.ToInteger(tempCard.CardID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");                
                xmlString.Append("></ImportCombustibilCard></root>");

                List<object> DbList = new List<object>();
                DbList.Add(xmlString.ToString());
                DbList.Add(LoginClass.userID);
                DbList.Add(UtilsGeneral.ToInteger(operatiuneID,0));
                // start saving...
                int returnValue = 0;
                int cID = 0;
                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__ImportCombustibilCard_Insert_Update", DbList.ToArray()))
                {
                    if (reader.Read())
                    {
                        returnValue = UtilsGeneral.ToInteger(reader["ReturnValue"], 0);
                        cID = UtilsGeneral.ToInteger(reader["ImportCombustibilID"], 0);
                    }
                    else
                    {
                        returnValue = 3;
                        cID = 0;
                    }
                }
                DbList.Clear();

                if (returnValue == 1)
                {
                    throw new Exception(Constants.C_CONCURRENCY_UPDATE_FAILED);
                }
                else if (returnValue == 2)
                {

                    throw new Exception(Constants.C_CONCURRENCY_UPDATE_FAILED_RECORD_DELETED);
                }
                else if (returnValue == 3) //eroare in procedura stocata
                    throw new Exception("A aparut o eroare la salvare. Operatiune esuata !");
            }
        }

        #region Financiar

        // *** FINANCIAR ***

        public void ExecuteSaveFinanciar(object cardObject, DateTime DataOraImport, string UserImport)
        {
            if (cardObject == null)
                return;

            if (cardObject is CombustibilCard)
            {
                CombustibilCard tempCard = (CombustibilCard)cardObject;
                StringBuilder xmlString = new StringBuilder();


                DateTime correctMinimumSQLDT = new DateTime(1753, 1, 1);
                if (tempCard.DataFacturare == DateTime.MinValue)
                    tempCard.DataFacturare = correctMinimumSQLDT;
                if (tempCard.OraLivrare == DateTime.MinValue)
                {
                    var itms = new int[] { tempCard.OraLivrare.Value.Hour, tempCard.OraLivrare.Value.Minute, tempCard.OraLivrare.Value.Second };
                    tempCard.OraLivrare = correctMinimumSQLDT;

                    tempCard.OraLivrare.Value.AddHours((double)itms[0]);
                    tempCard.OraLivrare.Value.AddMinutes((double)itms[1]);
                    tempCard.OraLivrare.Value.AddSeconds((double)itms[2]);

                }
                if (tempCard.DataLivrare == DateTime.MinValue)
                    tempCard.DataLivrare = correctMinimumSQLDT;



                xmlString.Append("<root><ImportCombustibilCardFacturi ");
                xmlString.Append(@"Tara= """ + tempCard.Tara + @""" ");
                xmlString.Append(@"CodStatie= """ + tempCard.CodStatie + @""" ");
                xmlString.Append(@"LocatieStatie= """ + tempCard.LocatieStatie + @""" ");
                xmlString.Append(@"DataLivrare= """ + UtilsGeneral.ToDateTime(tempCard.DataLivrare).ToString("MM-dd-yyyy").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"OraLivrare= """ + UtilsGeneral.ToDateTime(tempCard.OraLivrare).ToString("H:mm:ss").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"DataFacturare= """ + UtilsGeneral.ToDateTime(tempCard.DataFacturare).ToString("MM-dd-yyyy").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"IntrareLocatie= """ + tempCard.IntrareLocatie + @""" ");
                xmlString.Append(@"IesireLocatie= """ + tempCard.IesireLocatie + @""" ");
                xmlString.Append(@"NrAuto= """ + tempCard.NrAuto + @""" ");
                xmlString.Append(@"NrFactura= """ + tempCard.NrFactura + @""" ");
                xmlString.Append(@"Kilometri= """ + UtilsGeneral.ToDecimal(tempCard.Kilometri, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Produs= """ + tempCard.Produs + @""" ");
                xmlString.Append(@"Cantitate= """ + UtilsGeneral.ToDecimal(tempCard.Cantitate, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Valuta= """ + tempCard.Valuta + @""" ");
                xmlString.Append(@"PretPerLitru= """ + UtilsGeneral.ToDecimal(tempCard.PretPerLitru, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"ValoareFaraTVA= """ + UtilsGeneral.ToDecimal(tempCard.ValoareFaraTVA, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"ValoareTVA= """ + UtilsGeneral.ToDecimal(tempCard.ValoareTVA, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"ValoareCuTVA= """ + UtilsGeneral.ToDecimal(tempCard.ValoareCuTVA, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"VATAmount= """ + UtilsGeneral.ToDecimal(tempCard.VATAmount, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Retea= """ + tempCard.Retea + @""" ");
                xmlString.Append(@"CardPan= """ + tempCard.CardPan + @""" ");
                xmlString.Append(@"CodRuta= """ + tempCard.CodRuta + @""" ");
                xmlString.Append(@"ClasaVehicul= """ + tempCard.ClasaVehicul.ToString() + @""" ");
                xmlString.Append(@"TipFurnizorImportat= """ + UtilsGeneral.ToInteger(tempCard.FurnizorImportat, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"NrAuto_ID= """ + UtilsGeneral.ToInteger(tempCard.NrAutoID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Produs_ID= """ + UtilsGeneral.ToInteger(tempCard.ProdusID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Card_ID= """ + UtilsGeneral.ToInteger(tempCard.CardID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"DataImport= """ + UtilsGeneral.ToDateTime(DataOraImport).ToString("MM-dd-yyyy").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"OraImport= """ + UtilsGeneral.ToDateTime(DataOraImport).ToString("H:mm:ss").ToString(CultureInfo.InvariantCulture) + @""" ");

                if(FacturaPrimita_ID.HasValue && FacturaPrimita_ID.Value != 0)
                    xmlString.Append(@"FacturaPrimita_ID= """ + UtilsGeneral.ToInteger(FacturaPrimita_ID.Value, 0).ToString() + @""" ");
                else xmlString.Append(@"FacturaPrimita_ID= """ + DBNull.Value + @""" ");

                xmlString.Append(@"UserImport= """ + UserImport + @""" ");
                xmlString.Append(@"HashCode= """ + tempCard.HashCode.ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append("></ImportCombustibilCardFacturi></root>");

                List<object> DbList = new List<object>();
                DbList.Add(xmlString.ToString());
                DbList.Add(LoginClass.userID);
                DbList.Add(0);
                // start saving...
                int returnValue = 0;
                int cID = 0;
                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__ImportCombustibilCardFacturi_Insert_Update", DbList.ToArray()))
                {
                    if (reader.Read())
                    {
                        returnValue = UtilsGeneral.ToInteger(reader["ReturnValue"], 0);
                        cID = UtilsGeneral.ToInteger(reader["ImportCombustibilID"], 0);
                    }
                    else
                    {
                        returnValue = 3;
                        cID = 0;
                    }
                }
                DbList.Clear();

                if (returnValue == 1)
                {
                    throw new Exception(Constants.C_CONCURRENCY_UPDATE_FAILED);
                }
                else if (returnValue == 2)
                {

                    throw new Exception(Constants.C_CONCURRENCY_UPDATE_FAILED_RECORD_DELETED);
                }
                else if (returnValue == 3) //eroare in procedura stocata
                    throw new Exception("A aparut o eroare la salvare. Operatiune esuata !");
            }
        }

        public void ExecuteSaveFinanciar(object cardObject, object operatiuneID)
        {
            if (cardObject == null)
                return;

            if (cardObject is CombustibilCard)
            {
                CombustibilCard tempCard = (CombustibilCard)cardObject;
                StringBuilder xmlString = new StringBuilder();


                xmlString.Append("<root><ImportCombustibilCardFacturi ");
                xmlString.Append(@"NrAuto_ID= """ + UtilsGeneral.ToInteger(tempCard.NrAutoID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Produs_ID= """ + UtilsGeneral.ToInteger(tempCard.ProdusID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Card_ID= """ + UtilsGeneral.ToInteger(tempCard.CardID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append("></ImportCombustibilCardFacturi></root>");

                List<object> DbList = new List<object>();
                DbList.Add(xmlString.ToString());
                DbList.Add(LoginClass.userID);
                DbList.Add(UtilsGeneral.ToInteger(operatiuneID, 0));
                // start saving...
                int returnValue = 0;
                int cID = 0;
                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__ImportCombustibilCardFacturi_Insert_Update", DbList.ToArray()))
                {
                    if (reader.Read())
                    {
                        returnValue = UtilsGeneral.ToInteger(reader["ReturnValue"], 0);
                        cID = UtilsGeneral.ToInteger(reader["ImportCombustibilID"], 0);
                    }
                    else
                    {
                        returnValue = 3;
                        cID = 0;
                    }
                }
                DbList.Clear();

                if (returnValue == 1)
                {
                    throw new Exception(Constants.C_CONCURRENCY_UPDATE_FAILED);
                }
                else if (returnValue == 2)
                {

                    throw new Exception(Constants.C_CONCURRENCY_UPDATE_FAILED_RECORD_DELETED);
                }
                else if (returnValue == 3) //eroare in procedura stocata
                    throw new Exception("A aparut o eroare la salvare. Operatiune esuata !");
            }
        }

        #endregion Financiar

        #endregion DataBase Operations
    }

    public static class RemoveDiacritics
    {
        public static string RemoveDiacriticsFromString(this String s)
        {
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
