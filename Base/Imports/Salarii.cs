using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.BaseUtils;
using System.Globalization;
using Base.Configuration;
using System.Data.SqlClient;
using Base.DataBase;

namespace Base.Imports
{
    public class Salarii
    {
        #region Properties

        public int? AngajatID
        { get; set; }

        public string CNP
        {
            get;
            set;
        }

        public DateTime? Data
        {
            get;
            set;
        }
        
        public decimal? St
        {
            get;
            set;
        }
        
        public decimal? Venit_Brut
        {
            get;
            set;
        }
        
        public decimal? Suma_Zile_CO
        {
            get;
            set;
        }

        public decimal? Suma_Zile_Cm_Firma
        {
            get;
            set;
        }

        public decimal? Suma_Zile_Cm_FNUASS
        {
            get;
            set;
        }

        public decimal? Sanatate_Angajat
        {
            get;
            set;
        }

        public decimal? CAS_Angajat
        {
            get;
            set;
        }

        public decimal? Somaj_Angajat
        {
            get;
            set;
        }

        public decimal? Impozit
        {
            get;
            set;
        }

        public decimal? Somaj_Firma
        {
            get;
            set;
        }

        public decimal? Sanatate_Firma
        {
            get;
            set;
        }

        public decimal? CAS_Firma
        {
            get;
            set;
        }


        public decimal? CAS_Concedii
        {
            get;
            set;
        }

        public decimal? Asig_Pt_Accidente_Si_Boli_Prof
        {
            get;
            set;
        }

        public decimal? Contrib_Pt_Garantarea_Creantelor_Salariale
        {
            get;
            set;
        }

        public decimal? Comision_ITM
        {
            get;
            set;
        }

        public decimal? Contrib_Pt_Concedii_Si_Indemnizatii
        {
            get;
            set;
        }

        public string Nume
        {
            get;
            set;
        }

        public decimal? ValoareTichete
        {
            get;
            set;
        }

        public int? NrTichete
        {
            get;
            set;
        }

        public decimal? Fd_Handicapati
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

        public void ComputeHashCode()
        {

            HashCode = (Nume == null ? string.Empty : Nume).GetHashCode() ^
                       (CNP == null ? string.Empty : CNP).GetHashCode() ^
                       (Data == null ? 0 : Data.Value.Ticks).GetHashCode() ^
                       (NrTichete == null ? 0 : (int)NrTichete).GetHashCode() ^
                       (AngajatID == null ? 0 : (int)AngajatID).GetHashCode() ^                       
                       (CAS_Angajat == null ? 0 : CAS_Angajat).GetHashCode() ^
                       (Impozit == null ? 0 : Impozit).GetHashCode() ^                       
                       (Suma_Zile_Cm_FNUASS == null ? 0 : Suma_Zile_Cm_FNUASS).GetHashCode() ^
                       (Suma_Zile_CO == null ? 0 : Suma_Zile_CO).GetHashCode() ^
                       (CAS_Concedii == null ? 0 : CAS_Concedii).GetHashCode();
        }

        #endregion Compute HashCodes

        #region DataBase Operations

        public void ExecuteSave(object salarObject, DateTime DataOraImport, string UserImport)
        {
            if (salarObject == null)
                return;

            if (salarObject is Salarii)
            {
                Salarii tempSalarii = (Salarii)salarObject;
                StringBuilder xmlString = new StringBuilder();


                DateTime correctMinimumSQLDT = new DateTime(1753, 1, 1);
                if (tempSalarii.Data == DateTime.MinValue)
                    tempSalarii.Data = correctMinimumSQLDT;              


                xmlString.Append("<root><ImportSalarii ");
                xmlString.Append(@"AngajatID= """ + tempSalarii.AngajatID + @""" ");
                xmlString.Append(@"CNP= """ + tempSalarii.CNP + @""" ");                
                xmlString.Append(@"Data= """ + UtilsGeneral.ToDateTime(tempSalarii.Data).ToString("MM-dd-yyyy").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"st= """ + (tempSalarii.St ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"venit_brut= """ + (tempSalarii.Venit_Brut ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"suma_zile_co= """ + (tempSalarii.Suma_Zile_CO ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"suma_zile_cm_firma= """ + (tempSalarii.Suma_Zile_Cm_Firma ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"suma_zile_cm_FNUASS= """ + (tempSalarii.Suma_Zile_Cm_FNUASS ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"sanatate_angajat= """ + (tempSalarii.Sanatate_Angajat ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"cas_angajat= """ + (tempSalarii.CAS_Angajat ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"somaj_angajat= """ + (tempSalarii.Somaj_Angajat ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"impozit= """ + (tempSalarii.Impozit ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"somaj_firma= """ + (tempSalarii.Somaj_Firma ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"sanatate_firma= """ + (tempSalarii.Sanatate_Firma ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"cas_firma= """ + (tempSalarii.CAS_Firma ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"cas_concedii= """ + (tempSalarii.CAS_Concedii ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"asig_pt_accidente_si_boli_prof= """ + (tempSalarii.Asig_Pt_Accidente_Si_Boli_Prof ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"contrib_pt_garantarea_creantelor_salariale= """ + (tempSalarii.Contrib_Pt_Garantarea_Creantelor_Salariale ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"comision_itm= """ + (tempSalarii.Comision_ITM ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"contrib_pt_concedii_si_indemnizatii= """ + (tempSalarii.Contrib_Pt_Concedii_Si_Indemnizatii ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Nume= """ + tempSalarii.Nume + @""" ");
                xmlString.Append(@"ValoareTichete= """ + (tempSalarii.ValoareTichete ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"NrTichete= """ + (tempSalarii.NrTichete ?? 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"fd_handicapati= """ + (tempSalarii.Fd_Handicapati ?? 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");

                xmlString.Append(@"HashCode= """ + tempSalarii.HashCode.ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append("></ImportSalarii></root>");

                List<object> DbList = new List<object>();
                DbList.Add(xmlString.ToString());
                DbList.Add(LoginClass.userID);
                DbList.Add(0);
                // start saving...
                int returnValue = 0;
                int cID = 0;
                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__ImportSalarii_Insert_Update", DbList.ToArray()))
                {
                    if (reader.Read())
                    {
                        returnValue = UtilsGeneral.ToInteger(reader["ReturnValue"], 0);
                        cID = UtilsGeneral.ToInteger(reader["ImportSalariiID"], 0);
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

        public void ExecuteSave(object salarObject, object operatiuneID)
        {
            if (salarObject == null)
                return;

            if (salarObject is Salarii)
            {
                Salarii tempCard = (Salarii)salarObject;
                StringBuilder xmlString = new StringBuilder();


                xmlString.Append("<root><ImportSalarii ");
                xmlString.Append(@"AngajatID= """ +  tempCard.AngajatID ?? 0 + @""" ");                                
                xmlString.Append("></ImportSalarii></root>");

                List<object> DbList = new List<object>();
                DbList.Add(xmlString.ToString());
                DbList.Add(LoginClass.userID);
                DbList.Add(UtilsGeneral.ToInteger(operatiuneID, 0));
                // start saving...
                int returnValue = 0;
                int cID = 0;
                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__ImportSalarii_Insert_Update", DbList.ToArray()))
                {
                    if (reader.Read())
                    {
                        returnValue = UtilsGeneral.ToInteger(reader["ReturnValue"], 0);
                        cID = UtilsGeneral.ToInteger(reader["ImportSalariiID"], 0);
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

        #endregion DataBase Operations
    }
}
