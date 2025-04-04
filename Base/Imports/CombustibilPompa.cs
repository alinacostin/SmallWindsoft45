using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.BaseUtils;
using Base.Configuration;
using System.Data.SqlClient;
using Base.DataBase;
using System.Globalization;

namespace Base.Imports
{
    public class CombustibilPompa
    {
        #region Properties

        public string Identificator
        {
            get;
            set;
        }

        public DateTime? Data
        {
            get;
            set;
        }

        public DateTime? Ora
        {
            get;
            set;
        }

        public string NrAuto
        {
            get;
            set;
        }

        public int? NrAutoID
        {
            get;
            set;
        }

        public decimal? Kilometri
        {
            get;
            set;
        }

        public decimal? LitriiAlimentati
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

        public string Retea
        {
            get;
            set;
        }


        public int? FurnizorImportat
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

            HashCode = (FurnizorImportat == null ? 0 : FurnizorImportat).GetHashCode() ^
                       (Data == null ? 0 : Data.Value.Date.Ticks).GetHashCode() ^
                       (Ora == null ? 0 : Ora.Value.TimeOfDay.Ticks).GetHashCode() ^ LitriiAlimentati.GetHashCode() ^
                       (NrAuto == null ? string.Empty : NrAuto).GetHashCode() ^
                       (Kilometri == null ? 0 : Kilometri).GetHashCode();                     
        }

        #endregion Compute HashCodes

        #region DataBase Operations

        public void ExecuteSave(object pompaObject, DateTime DataOraImport, string UserImport)
        {
            if (pompaObject == null)
                return;

            if (pompaObject is CombustibilPompa)
            {
                CombustibilPompa tempPompa = (CombustibilPompa)pompaObject;
                StringBuilder xmlString = new StringBuilder();

                xmlString.Append("<root><ImportCombustibilPompa ");
                xmlString.Append(@"Identificator= """ + tempPompa.Identificator + @""" ");
                xmlString.Append(@"NrAuto= """ + tempPompa.NrAuto + @""" ");
                xmlString.Append(@"NrAuto_ID= """ + UtilsGeneral.ToInteger(tempPompa.NrAutoID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Kilometri= """ + UtilsGeneral.ToDecimal(tempPompa.Kilometri, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"LitriiAlimentati= """ + UtilsGeneral.ToDecimal(tempPompa.LitriiAlimentati, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"PretPerLitru= """ + UtilsGeneral.ToDecimal(tempPompa.PretPerLitru, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"ValoareFaraTVA= """ + UtilsGeneral.ToDecimal(tempPompa.ValoareFaraTVA, 0.0M).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Data= """ + UtilsGeneral.ToDateTime(tempPompa.Data).ToString("MM-dd-yyyy").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Ora= """ + UtilsGeneral.ToDateTime(tempPompa.Ora).ToString("H:mm:ss").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"TipFurnizorImportat= """ + UtilsGeneral.ToInteger(tempPompa.FurnizorImportat, 0).ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Retea= """ + tempPompa.Retea + @""" ");
                xmlString.Append(@"DataImport= """ + UtilsGeneral.ToDateTime(DataOraImport).ToString("MM-dd-yyyy").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"OraImport= """ + UtilsGeneral.ToDateTime(DataOraImport).ToString("H:mm:ss").ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"UserImport= """ + UserImport + @""" ");
                xmlString.Append(@"HashCode= """ + tempPompa.HashCode.ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append("></ImportCombustibilPompa></root>");

                List<object> DbList = new List<object>();
                DbList.Add(xmlString.ToString());
                DbList.Add(LoginClass.userID);
                DbList.Add(0);
                // start saving...
                int returnValue = 0;
                int cID = 0;
                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__ImportCombustibilPompa_Insert_Update", DbList.ToArray()))
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

        public void ExecuteSave(object pompaObject, object operatiuneID)
        {
            if (pompaObject == null)
                return;

            if (pompaObject is CombustibilPompa)
            {
                CombustibilPompa tempPompa = (CombustibilPompa)pompaObject;
                StringBuilder xmlString = new StringBuilder();


                xmlString.Append("<root><ImportCombustibilPompa ");
                xmlString.Append(@"NrAuto_ID= """ + UtilsGeneral.ToInteger(tempPompa.NrAutoID, 0).ToString(CultureInfo.InvariantCulture) + @""" ");                
                xmlString.Append("></ImportCombustibilPompa></root>");

                List<object> DbList = new List<object>();
                DbList.Add(xmlString.ToString());
                DbList.Add(LoginClass.userID);
                DbList.Add(UtilsGeneral.ToInteger(operatiuneID, 0));
                // start saving...
                int returnValue = 0;
                int cID = 0;
                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__ImportCombustibilPompa_Insert_Update", DbList.ToArray()))
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

        #endregion DataBase Operations
    }
}
