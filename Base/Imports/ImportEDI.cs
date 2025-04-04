using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using Base.BaseUtils;
using Base.DataBase;

namespace Base.Imports
{
    public class ImportEDI
    {
        #region Properties

        public string FileName
        {
            get;
            set;
        }

        public DataTable MasterTable
        {
            get;
            set;
        }

        public string MasterSheetName
        {
            get;
            set;
        }

        public int MasterIndex
        {
            get;
            set;
        }

        public DateTime DataOraImport
        {
            get;
            set;
        }

        public string UserImport
        {
            get;
            set;
        }

        public string Error
        {
            get;
            set;
        }

        public DataSet DataSetDBFurnizor
        {
            get;
            set;
        }

        public bool ImportRealizat;

        #endregion Properties

        #region Constructors

        public ImportEDI(string fileName, DateTime dataImport, string userImport)
        {
            FileName = fileName;
            Error = string.Empty;
            DataOraImport = dataImport;
            UserImport = userImport;
            DataSetDBFurnizor = new DataSet();
            DataSetDBFurnizor.Tables.Add("Impachetare");
            DataSetDBFurnizor.Tables.Add("Valute");
            DataSetDBFurnizor.Tables.Add("CoteTVA");
            DataSetDBFurnizor.Tables.Add("Orase");
            DataSetDBFurnizor.Tables.Add("Tari");
        }

        public ImportEDI()
        {
        }

        #endregion Constructors

        #region File Operations

        public bool PrepareFile()
        {
            try
            {
                MasterSheetName = GetExcelSheetName();
                if (Error == string.Empty)
                {
                    string connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";");
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + MasterSheetName + "]", connString);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    if (adapter != null) adapter.Dispose();
                    MasterTable = ds.Tables[0];
                    if (ds != null) ds.Dispose();
                    if (MasterTable == null) return false;
                    MasterTable.TableName = MasterSheetName;
                    return true;
                }
                return false;
            }
            catch
            {
                Error = "Formatul fisierului selectat este invalid.";
                return false;
            }
        }

        public bool ParseFile()
        {
            if (!PrepareFile())
                return false;

            LoadDBWithFurnizor();
            try
            {
                ExtractComenziEDI();
            }
            catch
            {
                Error = "Eroare la import. Furnizorul acestui import nu a fost identificat.";
            }

            if (Error != string.Empty) return false;
            return true;
        }

        private void LoadDBWithFurnizor()
        {
            try
            {
                WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Impachetare" }, new Query(string.Format(@"SELECT NI.* FROM Nomenclator_ImpachetareMarfa AS NI WHERE NI.Categorie LIKE '%pal%'")));

                WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Valute" }, new Query(string.Format(@"SELECT * FROM Lista_Valute")));

                WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "CoteTVA" }, new Query(string.Format(@"SELECT * FROM Lista_Cota_TVA  where CotaTVA like '%24'")));

                WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Orase" }, new Query(string.Format(@"SELECT * FROM Lista_Orase")));

                WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Tari" }, new Query(string.Format(@"SELECT * FROM Lista_Tari")));
            }
            catch { }
        }

        private bool ExtractComenziEDI()
        {
            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              where row[0].ToString() != string.Empty
                              select new
                              {
                                  LoadID = row[0]
                                  ,
                                  ClientID = row[1]
                                  ,
                                  DataComanda = row[2].ToString()
                                  ,
                                  AdresaIncarcare = row[3]
                                  ,
                                  OrasIncarcare = row[4]
                                  ,
                                  TaraIncarcare = row[5]
                                  ,
                                  GreutateIncarcare = row[6]
                                  ,
                                  PaletiIncarcare = row[7]
                                  ,
                                  DataDescarcare = row[8]
                                  ,
                                  AdresaDescarcare = row[9].ToString() + "," + row[10].ToString()
                                  ,
                                  OrasDescarcare = row[11]
                                  ,
                                  TaraDescarcare = row[12]
                                  ,
                                  ObservatiiComanda = row[13].ToString() + "," + row[14].ToString()
                                  ,
                                  PretUnitar = row[15]
                                  ,
                                  Valuta = row[16]
                              };
                if (results == null)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    return false;
                }

                List<EDI> ediListOfObjects = new List<EDI>();
                foreach (var rowImport in results)
                {
                    try
                    {
                        EDI ediObject = new EDI();

                        ediObject.LoadID = ConvertToInt32IvariantCulture(rowImport.LoadID.ToString());
                        ediObject.ClientID = ConvertToInt32IvariantCulture(rowImport.ClientID.ToString());
                        ediObject.DataComanda = UtilsGeneral.ToDateTime(rowImport.DataComanda.ToString());
                        ediObject.AdresaIncarcare = rowImport.AdresaIncarcare.ToString();
                        ediObject.OrasIncarcare = rowImport.OrasIncarcare.ToString();
                        ediObject.OrasIncarcare_ID = FindOras(ediObject.OrasIncarcare);
                        ediObject.TaraIncarcare = rowImport.TaraIncarcare.ToString();
                        ediObject.TaraIncarcare_ID = FindTara(ediObject.TaraIncarcare);
                        ediObject.GreutateIncarcare = ConvertToDecimalIvariantCulture(rowImport.GreutateIncarcare.ToString());
                        ediObject.TipImpachetareIncarcare_ID = FindImpachetare();
                        ediObject.PaletiIncarcare = ConvertToDecimalIvariantCulture(rowImport.PaletiIncarcare.ToString());
                        ediObject.DataDescarcare = UtilsGeneral.ToDateTime(rowImport.DataDescarcare.ToString());
                        ediObject.AdresaDescarcare = rowImport.AdresaDescarcare.ToString();
                        ediObject.OrasDescarcare = rowImport.OrasDescarcare.ToString();
                        ediObject.OrasDescarcare_ID = FindOras(ediObject.OrasDescarcare.ToString());
                        ediObject.TaraDescarcare = rowImport.TaraDescarcare.ToString();
                        ediObject.TaraDescarcare_ID = FindTara(ediObject.TaraDescarcare);
                        ediObject.ObservatiiComanda = rowImport.ObservatiiComanda.ToString();
                        ediObject.PretUnitar = ConvertToDecimalIvariantCulture(rowImport.PretUnitar.ToString());
                        ediObject.Valuta = rowImport.Valuta.ToString();
                        ediObject.Valuta_ID = FindValuta(ediObject.Valuta);
                        ediObject.CotaTVA_ID = FindCotaTVA();
                        ediObject.DataOraImport = DataOraImport;
                        ediObject.UserImport = UserImport;

                        ediListOfObjects.Add(ediObject);
                    }
                    catch
                    {
                        Error = "Anumite importuri nu au fost realizate. Eroare la parsarea fisierului.";
                        continue;
                    }
                }

                EDI finalEDI = new EDI();
                finalEDI.CollectionOfEdi = ediListOfObjects;

                finalEDI.ExecuteSave();
                ImportRealizat = true;

                Error = string.Empty;
                return true;
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Formatul fisierului este invalid";
                return false;
            }
        }

        private string GetExcelSheetName()
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                // Connection String.
                String connString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                  "Data Source=" + FileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(connString);
                // Open connection with the database.
                objConn.Open();
                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }
                return dt.Rows[0]["TABLE_NAME"].ToString();
            }
            catch
            {
                Error = "Eroare la deschiderea fisierului.";
                return string.Empty;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        #endregion File Operations

        #region Find & Matches

        private int? FindOras(string oras)
        {
            if (oras == string.Empty)
                return null;
            try
            {
                var orasFound = DataSetDBFurnizor.Tables["Orase"].Select("Oras like '%" + oras.Trim().ToLower() + "%'").FirstOrDefault();

                return ((int?)orasFound["Oras_ID"]);
            }

            catch { return null; }
        }

        private int? FindTara(string tara)
        {
            if (tara == string.Empty)
                return null;
            try
            {
                var taraFound = DataSetDBFurnizor.Tables["Tari"].Select("Tara like '%" + tara.Trim().ToLower() + "%'").FirstOrDefault();

                return ((int?)taraFound["Tara_ID"]);
            }

            catch { return null; }
        }

        private int? FindImpachetare()
        {
            try
            {
                var impachetareFound = DataSetDBFurnizor.Tables["Impachetare"].Select(null, null, DataViewRowState.CurrentRows).FirstOrDefault();

                return ((int?)impachetareFound["ID"]);
            }

            catch { return null; }
        }

        private int? FindValuta(string valuta)
        {
            if (valuta == string.Empty)
                return null;
            try
            {
                var valuteRows = DataSetDBFurnizor.Tables["Valute"].Select(null, null, DataViewRowState.CurrentRows).ToArray();

                var findValuta = valuteRows.Where(x => x["CodValuta"].ToString().Trim().ToLower() == valuta.Trim().ToLower() ||
                                                       x["DenumireValuta"].ToString().Trim().ToLower() == valuta.Trim().ToLower() ||
                                                       x["Valuta"].ToString().Trim().ToLower() == valuta.Trim().ToLower()).FirstOrDefault();

                return ((int?)findValuta["Valuta_ID"]);
            }

            catch { return null; }
        }

        private int? FindCotaTVA()
        {
            try
            {
                var impachetareFound = DataSetDBFurnizor.Tables["CoteTVA"].Select(null, null, DataViewRowState.CurrentRows).FirstOrDefault();

                return ((int?)impachetareFound["CotaTVA_ID"]);
            }

            catch { return null; }
        }

        #endregion Find & Matches

        #region Find & Matches

        private decimal ConvertToDecimalIvariantCulture(string strIN)
        {
            try
            {
                int minusOperator = 1;
                if (strIN.Contains("-"))
                {
                    strIN = strIN.Replace("-", "");
                    minusOperator = -1;
                }
                return Decimal.Parse(strIN.Contains(",") ? strIN.Replace(",", ".") : strIN, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * minusOperator;
            }
            catch { return 0.0M; }
        }

        private int? ConvertToInt32IvariantCulture(string strIN)
        {
            try
            {
                int minusOperator = 1;
                if (strIN.Contains("-"))
                {
                    strIN = strIN.Replace("-", "");
                    minusOperator = -1;
                }
                return Int32.Parse(strIN.Contains(",") ? strIN.Replace(",", ".") : strIN, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * minusOperator;
            }
            catch { return null; }
        }

        #endregion Find & Matches
    }
}