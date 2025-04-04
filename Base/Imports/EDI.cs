using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.BaseUtils;
using System.Globalization;
using Base.Configuration;
using System.Data.SqlClient;
using Base.DataBase;
using System.Data;
using System.Collections;


namespace Base.Imports
{
    public class EDI
    {
        #region Properties

        public int? LoadID
        { get; set; }

        public int? ClientID
        { get; set; }

        public DateTime? DataComanda
        { get; set; }

        public string AdresaIncarcare
        { get; set; }

        public string OrasIncarcare
        { get; set; }

        public int? OrasIncarcare_ID
        { get; set; }

        public string TaraIncarcare
        { get; set; }

        public int? TaraIncarcare_ID
        { get; set; }

        public decimal? GreutateIncarcare
        { get; set; }

        public int? TipImpachetareIncarcare_ID
        { get; set; }

        public decimal? PaletiIncarcare
        { get; set; }

        public DateTime? DataDescarcare
        { get; set; }

        public string AdresaDescarcare
        { get; set; }

        public string OrasDescarcare
        { get; set; }

        public int? OrasDescarcare_ID
        { get; set; }

        public string TaraDescarcare
        { get; set; }

        public int? TaraDescarcare_ID
        { get; set; }

        public string ObservatiiComanda
        { get; set; }

        public decimal? PretUnitar
        { get; set; }

        public string Valuta
        { get; set; }

        public int? Valuta_ID
        { get; set; }

        public string CotaTVA // momentan nu exista
        { get; set; }

        public int? CotaTVA_ID // setat implicit pe cota tva = 24
        { get; set; }

        public DateTime? DataOraImport
        { get; set; }

        public string UserImport
        { get; set; }

        public List<EDI> CollectionOfEdi
        { get; set; }

        #endregion Properties

        #region Constructors

        public EDI()
        {
            CollectionOfEdi = new List<EDI>();            
        }

        #endregion Constructors

        #region DataBase Operations

        private string BuildXMLRow()
        {
            try
            {
                DataSet DataSetEDI = new DataSet();
                DataSetEDI.Tables.Add("ImportEDI");
                DataSetEDI.Tables[0].Columns.Add("EDI_ID", typeof(int));
                DataSetEDI.Tables[0].Columns.Add("LoadID", typeof(int));
                DataSetEDI.Tables[0].Columns.Add("ClientID", typeof(int));
                DataSetEDI.Tables[0].Columns.Add("DataComanda", typeof(DateTime));
                DataSetEDI.Tables[0].Columns.Add("AdresaIncarcare", typeof(string));
                DataSetEDI.Tables[0].Columns.Add("OrasIncarcare", typeof(string));
                DataSetEDI.Tables[0].Columns.Add("OrasIncarcare_ID", typeof(int));
                DataSetEDI.Tables[0].Columns.Add("TaraIncarcare", typeof(string));
                DataSetEDI.Tables[0].Columns.Add("TaraIncarcare_ID", typeof(int));
                DataSetEDI.Tables[0].Columns.Add("GreutateIncarcare", typeof(decimal));
                DataSetEDI.Tables[0].Columns.Add("TipImpachetareIncarcare_ID", typeof(int));
                DataSetEDI.Tables[0].Columns.Add("PaletiIncarcare", typeof(decimal));
                DataSetEDI.Tables[0].Columns.Add("DataDescarcare", typeof(DateTime));
                DataSetEDI.Tables[0].Columns.Add("AdresaDescarcare", typeof(string));
                DataSetEDI.Tables[0].Columns.Add("OrasDescarcare", typeof(string));
                DataSetEDI.Tables[0].Columns.Add("OrasDescarcare_ID", typeof(int));
                DataSetEDI.Tables[0].Columns.Add("TaraDescarcare", typeof(string));
                DataSetEDI.Tables[0].Columns.Add("TaraDescarcare_ID", typeof(int));
                DataSetEDI.Tables[0].Columns.Add("ObservatiiComanda", typeof(string));
                DataSetEDI.Tables[0].Columns.Add("PretUnitar", typeof(decimal));
                DataSetEDI.Tables[0].Columns.Add("Valuta", typeof(string));
                DataSetEDI.Tables[0].Columns.Add("Valuta_ID", typeof(int));
                DataSetEDI.Tables[0].Columns.Add("CotaTVA_ID", typeof(int));
                DataSetEDI.Tables[0].Columns.Add("DataOraImport", typeof(DateTime));
                DataSetEDI.Tables[0].Columns.Add("UserImport", typeof(string));

                foreach (EDI ediObject in CollectionOfEdi)
                {
                    var rowEDI = DataSetEDI.Tables[0].NewRow();

                    rowEDI["EDI_ID"] = 0;
                    rowEDI["LoadID"] = ediObject.LoadID ?? (object)DBNull.Value;
                    rowEDI["ClientID"] = ediObject.ClientID ?? (object)DBNull.Value;
                    rowEDI["DataComanda"] = ediObject.DataComanda ?? (object)DBNull.Value;
                    rowEDI["AdresaIncarcare"] = ediObject.AdresaIncarcare;
                    rowEDI["OrasIncarcare"] = ediObject.OrasIncarcare;
                    rowEDI["OrasIncarcare_ID"] = ediObject.OrasIncarcare_ID ?? (object)DBNull.Value;
                    rowEDI["TaraIncarcare"] = ediObject.TaraIncarcare;
                    rowEDI["TaraIncarcare_ID"] = ediObject.TaraIncarcare_ID ?? (object)DBNull.Value;
                    rowEDI["GreutateIncarcare"] = ediObject.GreutateIncarcare ?? (object)DBNull.Value;
                    rowEDI["TipImpachetareIncarcare_ID"] = ediObject.TipImpachetareIncarcare_ID ?? (object)DBNull.Value;
                    rowEDI["PaletiIncarcare"] = ediObject.PaletiIncarcare ?? (object)DBNull.Value;
                    rowEDI["DataDescarcare"] = ediObject.DataDescarcare ?? (object)DBNull.Value;
                    rowEDI["AdresaDescarcare"] = ediObject.AdresaDescarcare;
                    rowEDI["OrasDescarcare"] = ediObject.OrasDescarcare;
                    rowEDI["OrasDescarcare_ID"] = ediObject.OrasDescarcare_ID ?? (object)DBNull.Value;
                    rowEDI["TaraDescarcare"] = ediObject.TaraDescarcare;
                    rowEDI["TaraDescarcare_ID"] = ediObject.TaraDescarcare_ID ?? (object)DBNull.Value;
                    rowEDI["ObservatiiComanda"] = ediObject.ObservatiiComanda;
                    rowEDI["PretUnitar"] = ediObject.PretUnitar ?? (object)DBNull.Value;
                    rowEDI["Valuta"] = ediObject.Valuta;
                    rowEDI["Valuta_ID"] = ediObject.Valuta_ID ?? (object)DBNull.Value;
                    rowEDI["CotaTVA_ID"] = ediObject.CotaTVA_ID ?? (object)DBNull.Value;
                    rowEDI["DataOraImport"] = ediObject.DataOraImport ?? (object)DBNull.Value;
                    rowEDI["UserImport"] = ediObject.UserImport;

                    DataSetEDI.Tables[0].Rows.Add(rowEDI.ItemArray);
                }

                StringBuilder xmlString = new StringBuilder();

                xmlString.Append("<root>");
                foreach (DataRow dataRow in DataSetEDI.Tables[0].Rows)
                {
                    xmlString.Append("<ImportEDI " + UtilsXML.RowToXML(dataRow, true) + "/>");
                }
                xmlString.Append("</root>");

                return xmlString.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }


        public void ExecuteSave()
        {
            if (CollectionOfEdi.Count != 0)
            {
                var xmlString = BuildXMLRow();


                List<object> DbList = new List<object>();
                DbList.Add(xmlString.ToString());
                DbList.Add(LoginClass.userID);
                // start saving...
                int returnValue = 0;
                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__ImportEDI_Insert_Update", DbList.ToArray()))
                {
                    if (reader.Read())
                    {
                        returnValue = UtilsGeneral.ToInteger(reader["ReturnValue"], 0);
                    }
                    else
                        returnValue = 3;

                }
                DbList.Clear();

                if (returnValue != 0) //eroare in procedura stocata
                    throw new Exception("A aparut o eroare la salvare. Operatiune esuata !");
            }           
        }
      
        #endregion DataBase Operations
    }
}
