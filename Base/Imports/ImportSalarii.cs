using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Base.DataBase;
using Base.BaseUtils;

namespace Base.Imports
{
    public class ImportSalarii
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

        private int NoOfRowsFromDB = 0;

        #endregion Properties

        #region Constructors

        public ImportSalarii(string fileName, DateTime dataImport, string userImport, bool isFinanciar)
        {
            FileName = fileName;
            Error = string.Empty;
            DataOraImport = dataImport;           
            UserImport = userImport;
            DataSetDBFurnizor = new DataSet();
            DataSetDBFurnizor.Tables.Add("Salarii");
            DataSetDBFurnizor.Tables.Add("Angajati");   
        }

        public ImportSalarii()
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
                ExtractSalarii();
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
                WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Salarii" }, new Query(string.Format(@"SELECT ISAL.HashCode FROM ImportSalarii AS ISAL")));
                if (DataSetDBFurnizor.Tables[0].Rows.Count != 0) NoOfRowsFromDB = 1;

                WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Angajati" }, new Query(string.Format(@"SELECT AD.NumeAngajati_ID, AD.CNPAngajati FROM Angajati_Detalii AS AD")));
            }
            catch { }
        }

        private bool ExtractSalarii()
        {
            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  Data = row[0]
                                  ,CNP = row[1].ToString()
                                  ,Nume = row[2].ToString()
                                  ,st = row[3]
                                  ,ValoareTichete = row[4]
                                  ,NrTichete = row[5]
                                  ,venit_brut = row[6]
                                  ,suma_zile_co = row[7]
                                  ,suma_zile_cm_firma = row[8]
                                  ,suma_zile_cm_FNUASS = row[9]
                                  ,sanatate = row[10]
                                  ,cas = row[11]
                                  ,somaj = row[12]
                                  ,impozit = row[13]
                                  ,somaj_firma = row[14]
                                  ,sanatate_firma = row[15]
                                  ,cas_firma = row[16]
                                  ,cas_concedii = row[17]
                                  ,asig_pt_accidente_si_boli_prof = row[18]
                                  ,contrib_pt_garantarea_creantelor_salariale = row[19]                                  
                                  ,contrib_pt_concedii_si_indemnizatii = row[20]
                                  //,comision_itm = row[0]
                                  //,fd_handicapati = row[0]
                              };
                if (results == null)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    return false;
                }

                foreach (var rowImport in results)
                {
                    try
                    {                      

                        Salarii salar = new Salarii();

                        salar.Data = UtilsGeneral.ToDateTime(rowImport.Data);
                        salar.CNP = rowImport.CNP;
                        salar.AngajatID = FindAngajatWithCNP(salar.CNP);
                        salar.Nume = rowImport.Nume;
                        salar.St =  UtilsGeneral.ToDecimal(rowImport.st, 0.0M);
                        salar.Venit_Brut = UtilsGeneral.ToDecimal(rowImport.venit_brut, 0.0M);
                        salar.Suma_Zile_CO = UtilsGeneral.ToDecimal(rowImport.suma_zile_co, 0.0M);
                        salar.Suma_Zile_Cm_Firma = UtilsGeneral.ToDecimal(rowImport.suma_zile_cm_firma, 0.0M);
                        salar.Suma_Zile_Cm_FNUASS = UtilsGeneral.ToDecimal(rowImport.suma_zile_cm_FNUASS, 0.0M);
                        salar.Sanatate_Angajat = UtilsGeneral.ToDecimal(rowImport.sanatate, 0.0M);
                        salar.CAS_Angajat = UtilsGeneral.ToDecimal(rowImport.cas, 0.0M);
                        salar.Somaj_Angajat = UtilsGeneral.ToDecimal(rowImport.somaj, 0.0M);
                        salar.Impozit = UtilsGeneral.ToDecimal(rowImport.impozit, 0.0M);
                        salar.Somaj_Firma = UtilsGeneral.ToDecimal(rowImport.somaj_firma, 0.0M);
                        salar.Sanatate_Firma = UtilsGeneral.ToDecimal(rowImport.sanatate_firma, 0.0M);
                        salar.CAS_Firma = UtilsGeneral.ToDecimal(rowImport.cas_firma, 0.0M);
                        salar.CAS_Concedii = UtilsGeneral.ToDecimal(rowImport.cas_concedii, 0.0M);
                        salar.Asig_Pt_Accidente_Si_Boli_Prof = UtilsGeneral.ToDecimal(rowImport.asig_pt_accidente_si_boli_prof, 0.0M);
                        salar.Contrib_Pt_Concedii_Si_Indemnizatii = UtilsGeneral.ToDecimal(rowImport.contrib_pt_concedii_si_indemnizatii, 0.0M);
                        salar.Contrib_Pt_Garantarea_Creantelor_Salariale = UtilsGeneral.ToDecimal(rowImport.contrib_pt_garantarea_creantelor_salariale, 0.0M);
                        salar.ValoareTichete = UtilsGeneral.ToDecimal(rowImport.ValoareTichete, 0.0M);
                        salar.NrTichete = UtilsGeneral.ToInteger(rowImport.NrTichete, 0);
                        salar.ComputeHashCode();

                        if (NoOfRowsFromDB == 0)
                        {
                            
                            salar.ExecuteSave(salar, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (SalarNotFound(salar))
                        {
                            salar.ExecuteSave(salar, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                    }
                    catch
                    {
                        Error = "Anumite importuri nu au fost realizate. Eroare la parsarea fisierului.";
                        continue;
                    }
                }

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

        #region Find Angajat ID

        private int FindAngajatWithCNP(string _cnp)
        {
            try
            {
                var angajatFound = DataSetDBFurnizor.Tables["Angajati"].Select("CNPAngajati like '%" + _cnp.Trim() + "%'").FirstOrDefault();
                return (angajatFound["NumeAngajati_ID"] == DBNull.Value ? 0 : (int)angajatFound["NumeAngajati_ID"]); 
            }

            catch { return 0; }
        }

        #endregion Find Angajat ID

        #region Remove Duplicate Items

        private bool SalarNotFound(Salarii currentSalar)
        {
            try
            {
                var objectFound = DataSetDBFurnizor.Tables[0].Select("HashCode = " + currentSalar.HashCode.ToString()).FirstOrDefault();
                if (objectFound != null) return false;
            }
            catch { }
            return true;
        }
        #endregion Remove Duplicate Items
    }
}
