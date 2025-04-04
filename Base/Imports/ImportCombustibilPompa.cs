using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data;
using Base.BaseUtils;
using System.Text.RegularExpressions;
using Base.DataBase;
using Base.Configuration;

namespace Base.Imports
{
    public class ImportCombustibilPompa
    {

        #region Enum Tip Furnizor

        public enum TipFurnizorImportat : int
        {
            Proprie = 1,
            Rompetrol = 2,           
            Shel = 3,            
            AlteTipuri = 4,
        }

        public enum FirmaCurenta : int
        {
            Alex = 1,
            Dunca = 2,
            Dianthus = 3,
            Default = 4,
            Vectra = 5,
            Tabita = 6,
            Filadelfia = 7,
            Viotrans = 8,
            Eliton = 9,
            RomVega = 10,
            BlueRiver = 11,
        }

        #endregion Enum Tip Furnizor

        #region Properties

        public int FurnizorImportat
        {
            get;
            set;
        }

        public int FilterFirmaCurenta
        {
            get;
            set;
        }

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

        public ImportCombustibilPompa(string fileName, DateTime dataImport, string userImport)
        {
            FileName = fileName;
            Error = string.Empty;
            DataOraImport = dataImport;           
            UserImport = userImport;
            DataSetDBFurnizor = new DataSet();
            DataSetDBFurnizor.Tables.Add("Pompe");
        }

        #endregion Constructors

        #region File Operations

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

            ExtractTipFurnizorImportat();
            switch (FurnizorImportat)
            {
                case (int)TipFurnizorImportat.Proprie:
                    ExtractProprie();
                    break;               
                default:
                    Error = "Eroare la import. Furnizorul acestui import nu a fost identificat.";
                    break;
            }

            if (Error != string.Empty) return false;
            return true;
        }

        private void ExtractTipFurnizorImportat()
        {
            if (FileName.ToLower().Trim().Contains("propr"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Proprie;
                SetFilterFirmaCurenta();
            }          

            else if (FileName.ToLower().Trim().Contains("rompet"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Rompetrol;
                SetFilterFirmaCurenta();
            }

            else if (FileName.ToLower().Trim().Contains("shel"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Shel;
                SetFilterFirmaCurenta();
            }          

            else
            {
                FurnizorImportat = (int)TipFurnizorImportat.AlteTipuri;
                SetFilterFirmaCurenta();
            }
            LoadDBWithFurnizor();
        }

        private void SetFilterFirmaCurenta()
        {
            if (FileName.ToLower().Trim().Contains("alex"))
                FilterFirmaCurenta = (int)FirmaCurenta.Alex;
            else if (FileName.ToLower().Trim().Contains("dunca"))
                FilterFirmaCurenta = (int)FirmaCurenta.Dunca;
            else if (FileName.ToLower().Trim().Contains("diant"))
                FilterFirmaCurenta = (int)FirmaCurenta.Dianthus;
            else if (FileName.ToLower().Trim().Contains("vectr"))
                FilterFirmaCurenta = (int)FirmaCurenta.Vectra;
            else if (FileName.ToLower().Trim().Contains("tabit"))
                FilterFirmaCurenta = (int)FirmaCurenta.Tabita;
            else if (FileName.ToLower().Trim().Contains("filad"))
                FilterFirmaCurenta = (int)FirmaCurenta.Filadelfia;
            else if (FileName.ToLower().Trim().Contains("vio"))
                FilterFirmaCurenta = (int)FirmaCurenta.Viotrans;
            else if (FileName.ToLower().Trim().Contains("eliton"))
                FilterFirmaCurenta = (int)FirmaCurenta.Eliton;
            else if (FileName.ToLower().Trim().Contains("vega"))
                FilterFirmaCurenta = (int)FirmaCurenta.RomVega;
            else if (FileName.ToLower().Trim().Contains("blue"))
                FilterFirmaCurenta = (int)FirmaCurenta.BlueRiver;
            else FilterFirmaCurenta = (int)FirmaCurenta.Default;
        }

        private void LoadDBWithFurnizor()
        {
            try
            {
                WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Pompe" },
                    "sp__ImportCombustibil_Furnizor_Load", new object[] { 2, FurnizorImportat, false });

                if (DataSetDBFurnizor.Tables[0].Rows.Count != 0) NoOfRowsFromDB = 1;
            }
            catch { }
        }


        private void ExtractProprie()
        {
            switch (FilterFirmaCurenta)
            {
                case (int)FirmaCurenta.Vectra:
                    ExtractProprieVectra();
                    break;      
                case (int)FirmaCurenta.Dianthus:
                    ExtractProprieDianthus();
                    break;
                case (int)FirmaCurenta.Eliton:
                    ExtractProprieEliton();
                    break;
                case (int)FirmaCurenta.Tabita:
                case (int)FirmaCurenta.Filadelfia:
                    ExtractProprieTabitaFiladelfia();
                    break;
                case (int)FirmaCurenta.Viotrans:
                    ExtractProprieViotrans();
                    break;
                case (int)FirmaCurenta.RomVega:
                    ExtractProprieRomVega();
                    break;
                case (int)FirmaCurenta.BlueRiver:
                    ExtractProprieBlueRiver();
                    break;   
                default:
                    Error = "Eroare la import. Firma dvs. nu a fost identificata.";
                    break;
            }
        }

        private void ExtractProprieBlueRiver()
        {
            try
            {
                var results = from row in MasterTable.AsEnumerable().TakeWhile(x => x[0].ToString().Length > 0)
                              select new
                              {
                                  Data = row[0],
                                  Ora = row[1],
                                  Kilometri = row[3],
                                  NrAuto = row[4],
                                  LitriiAlimentati = row[6],
                                  PretPerLitru = row[7]
                              };
                if (results == null)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    return;
                }

                foreach (var rowImport in results)
                {
                    try
                    {

                        CombustibilPompa pompa = new CombustibilPompa();

                        pompa.Data = UtilsGeneral.ToDateTime(rowImport.Data.ToString());
                        pompa.Ora = UtilsGeneral.ToDateTime(rowImport.Ora.ToString());
                        pompa.Kilometri = UtilsGeneral.ToDecimal(rowImport.Kilometri.ToString(), 0.0M);
                        pompa.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        pompa.LitriiAlimentati = UtilsGeneral.ToDecimal(rowImport.LitriiAlimentati.ToString(), 0.0M);
                        pompa.PretPerLitru = UtilsGeneral.ToDecimal(rowImport.PretPerLitru.ToString(), 0.0M);
                        pompa.Retea = "Proprie";
                        pompa.FurnizorImportat = FurnizorImportat;
                        pompa.NrAutoID = NrAutoIDFindAndMatch(pompa.NrAuto);
                        pompa.ComputeHashCode();
                        if (NoOfRowsFromDB == 0)
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (PompaNotFound(pompa))
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
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
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private void ExtractProprieVectra()
        {
            try
            {
                var results = from row in MasterTable.AsEnumerable().TakeWhile(x => x[0].ToString().Length > 0)                              
                              select new
                              {
                                 Data = row[1],
                                 NrAuto = row[2],
                                 LitriiAlimentati = row[3]
                              };
                if (results == null)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    return;
                }

                foreach (var rowImport in results)
                {
                    try
                    {

                        CombustibilPompa pompa = new CombustibilPompa();

                        pompa.Data = UtilsGeneral.ToDateTime(rowImport.Data.ToString());
                        pompa.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        pompa.LitriiAlimentati = UtilsGeneral.ToDecimal(rowImport.LitriiAlimentati.ToString(), 0.0M);
                        pompa.Retea = "Proprie";
                        pompa.FurnizorImportat = FurnizorImportat;
                        pompa.NrAutoID = NrAutoIDFindAndMatch(pompa.NrAuto);                        
                        pompa.ComputeHashCode();
                        if (NoOfRowsFromDB == 0)
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (PompaNotFound(pompa))
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
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
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private void ExtractProprieDianthus()
        {
            try
            {
                var results = from row in MasterTable.AsEnumerable().TakeWhile(x => x[0].ToString().Length > 0)
                              select new
                              {
                                  Identificator = row[0],
                                  NrAuto = row[1],
                                  Kilometri = row[2],
                                  LitriiAlimentati = row[3],
                                  Data = row[4],
                                  Ora = row[5],
                                  PretPerLitru = row[6],
                                  ValoareFaraTVA = row[7]
                              };
                if (results == null)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    return;
                }

                foreach (var rowImport in results)
                {
                    try
                    {

                        CombustibilPompa pompa = new CombustibilPompa();

                        pompa.Identificator = rowImport.Identificator.ToString();
                        pompa.Data = ExtractCorrectDatePompaProprieDianthus(rowImport.Data.ToString());
                        pompa.Ora = UtilsGeneral.ToDateTime(rowImport.Ora.ToString());
                        pompa.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        pompa.LitriiAlimentati = UtilsGeneral.ToDecimal(rowImport.LitriiAlimentati.ToString(), 0.0M);
                        pompa.PretPerLitru = UtilsGeneral.ToDecimal(rowImport.PretPerLitru.ToString(), 0.0M);
                        pompa.ValoareFaraTVA = UtilsGeneral.ToDecimal(rowImport.ValoareFaraTVA.ToString(), 0.0M);
                        pompa.Kilometri = UtilsGeneral.ToDecimal(rowImport.Kilometri.ToString(), 0.0M);
                        pompa.Retea = "Proprie";
                        pompa.NrAutoID = NrAutoIDFindAndMatch(pompa.NrAuto);
                        pompa.FurnizorImportat = FurnizorImportat;
                        pompa.ComputeHashCode();

                        if (NoOfRowsFromDB == 0)
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (PompaNotFound(pompa))
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
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
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private void ExtractProprieEliton()
        {
            try
            {
                var results = from row in MasterTable.AsEnumerable().TakeWhile(x => x[0].ToString().Length > 0)
                              select new
                              {
                                  Identificator = row[0],
                                  NrAuto = row[1],
                                  Kilometri = row[2],
                                  LitriiAlimentati = row[3],
                                  Data = row[4],
                                  Ora = row[5],
                                  PretPerLitru = 0,
                                  ValoareFaraTVA = 0
                                  //PretPerLitru = row[6],
                                  //ValoareFaraTVA = row[7]
                              };
                if (results == null)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    return;
                }

                foreach (var rowImport in results)
                {
                    try
                    {

                        CombustibilPompa pompa = new CombustibilPompa();

                        pompa.Identificator = rowImport.Identificator.ToString();
                        pompa.Data = ExtractCorrectDatePompaProprieDianthus(rowImport.Data.ToString());
                        pompa.Ora = UtilsGeneral.ToDateTime(rowImport.Ora.ToString());
                        pompa.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        pompa.LitriiAlimentati = UtilsGeneral.ToDecimal(rowImport.LitriiAlimentati.ToString(), 0.0M);
                        pompa.PretPerLitru = UtilsGeneral.ToDecimal(rowImport.PretPerLitru.ToString(), 0.0M);
                        pompa.ValoareFaraTVA = UtilsGeneral.ToDecimal(rowImport.ValoareFaraTVA.ToString(), 0.0M);
                        pompa.Kilometri = UtilsGeneral.ToDecimal(rowImport.Kilometri.ToString(), 0.0M);
                        pompa.Retea = "Proprie";
                        pompa.NrAutoID = NrAutoIDFindAndMatch(pompa.NrAuto);
                        pompa.FurnizorImportat = FurnizorImportat;
                        pompa.ComputeHashCode();

                        if (NoOfRowsFromDB == 0)
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (PompaNotFound(pompa))
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
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
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private void ExtractProprieTabitaFiladelfia()
        {
            try
            {
                var results = from row in MasterTable.AsEnumerable().Skip(3)
                              select new
                              {                
                                  Data = row[0],
                                  Ora = row[1],
                                  NrAuto = row[3],
                                  Kilometri = row[12],
                                  LitriiAlimentati = row[6],                                  
                                  PretPerLitru = row[7],
                                  ValoareFaraTVA = row[8]                                 
                              };
                if (results == null)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    return;
                }

                foreach (var rowImport in results)
                {
                    try
                    {

                        CombustibilPompa pompa = new CombustibilPompa();
                        
                        //pompa.Data = ExtractCorrectDatePompaProprieDianthus(rowImport.Data.ToString());
                        pompa.Data = UtilsGeneral.ToDateTime(rowImport.Data.ToString());
                        pompa.Ora = UtilsGeneral.ToDateTime(rowImport.Ora.ToString());
                        pompa.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        pompa.LitriiAlimentati = UtilsGeneral.ToDecimalInvariantCulture(rowImport.LitriiAlimentati.ToString(), 0.0M);
                        pompa.PretPerLitru = UtilsGeneral.ToDecimalInvariantCulture(rowImport.PretPerLitru.ToString(), 0.0M);
                        pompa.ValoareFaraTVA = UtilsGeneral.ToDecimalInvariantCulture(rowImport.ValoareFaraTVA.ToString(), 0.0M);
                        pompa.Kilometri = UtilsGeneral.ToDecimalInvariantCulture(rowImport.Kilometri.ToString(), 0.0M);
                        pompa.Retea = "Proprie";
                        pompa.NrAutoID = NrAutoIDFindAndMatch(pompa.NrAuto);
                        pompa.FurnizorImportat = FurnizorImportat;
                        pompa.ComputeHashCode();

                        if (NoOfRowsFromDB == 0)
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (PompaNotFound(pompa))
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
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
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }


        private void ExtractProprieViotrans()
        {
            try
            {
                var results = from row in MasterTable.AsEnumerable().Skip(3)
                              select new
                              {
                                  Identificator = row[0],
                                  NrAuto = row[2],
                                  Kilometri = row[3],
                                  LitriiAlimentati = row[4],
                                  Data = row[1],
                                  Ora = row[1],
                                  PretPerLitru = 0,
                                  ValoareFaraTVA = 0
                              };
                if (results == null)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    return;
                }

                foreach (var rowImport in results)
                {
                    try
                    {

                        CombustibilPompa pompa = new CombustibilPompa();

                        pompa.Identificator = rowImport.Identificator.ToString();
                        pompa.Data = ExtractCorrectDatePompaProprieDianthus(rowImport.Data.ToString());
                        pompa.Ora = UtilsGeneral.ToDateTime(rowImport.Ora.ToString());
                        pompa.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        pompa.LitriiAlimentati = UtilsGeneral.ToDecimal(rowImport.LitriiAlimentati.ToString(), 0.0M);
                        pompa.PretPerLitru = UtilsGeneral.ToDecimal(rowImport.PretPerLitru.ToString(), 0.0M);
                        pompa.ValoareFaraTVA = UtilsGeneral.ToDecimal(rowImport.ValoareFaraTVA.ToString(), 0.0M);
                        pompa.Kilometri = UtilsGeneral.ToDecimal(rowImport.Kilometri.ToString(), 0.0M);
                        pompa.Retea = "Proprie";
                        pompa.NrAutoID = NrAutoIDFindAndMatch(pompa.NrAuto);
                        //card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);
                        pompa.FurnizorImportat = FurnizorImportat;
                        pompa.ComputeHashCode();

                        if (NoOfRowsFromDB == 0)
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (PompaNotFound(pompa))
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
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
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }
        private void ExtractProprieRomVega()
        {
            try
            {
                var results = from row in MasterTable.AsEnumerable().TakeWhile(x => x[0].ToString().Length > 0)
                              select new
                              {
                                  Data = row[0],
                                  Ora = row[0],
                                  NrAuto = row[2],
                                  LitriiAlimentati = row[6],
                                  Kilometri = row[4]/*,
                                  PretPerLitru = 1,
                                  ValoareFaraTVA = 1*/
                              };
                if (results == null)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    return;
                }

                foreach (var rowImport in results)
                {
                    try
                    {

                        CombustibilPompa pompa = new CombustibilPompa();

                        pompa.Data = UtilsGeneral.ToDateTime(rowImport.Data.ToString());
                        pompa.Ora = UtilsGeneral.ToDateTime(rowImport.Ora.ToString());
                        pompa.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        pompa.LitriiAlimentati = UtilsGeneral.ToDecimal(rowImport.LitriiAlimentati.ToString(), 0.0M);
                        pompa.Retea = "Proprie";
                        pompa.FurnizorImportat = FurnizorImportat;
                        pompa.NrAutoID = NrAutoIDFindAndMatch(pompa.NrAuto);
                        pompa.Kilometri = UtilsGeneral.ToDecimal(rowImport.Kilometri.ToString(), 0.0M);
                        pompa.ComputeHashCode();
                        if (NoOfRowsFromDB == 0)
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (PompaNotFound(pompa))
                        {
                            pompa.ExecuteSave(pompa, DataOraImport, UserImport);
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
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        #endregion File Operations

        #region String Cleaners & Correctors

        private string CleanString(string strIN)
        {
            return Regex.Replace(strIN, @"[^\w\.@-]", "");
        }

        private string CleanStringMaster(string strIN)
        {
            string result = CleanString(strIN);
            return Regex.Replace(result, @"\\", "").Replace(@"""", "")
               .Replace("=", ".").Replace(" ", "").Replace("-", "");
        }

        private DateTime ExtractCorrectDatePompaProprieDianthus(string strIN)
        {
            try
            {
            var splitDate = strIN.Split('.');

            return new DateTime(Convert.ToInt32("20" + splitDate[2]), Convert.ToInt32(splitDate[1]), Convert.ToInt32(splitDate[0]));
            }
            catch { return DateTime.Now; }
        }

        #endregion String Cleaners & Correctors

        #region Nr Auto Matches

        private int? NrAutoIDFindAndMatch(string nrAutoToMatch)
        {
            if (nrAutoToMatch != string.Empty)
            {
                #region [1] Incarc datele
                DataSet ds = new DataSet();
                ds.Tables.Add("NrAuto");

                string nrAutoID = string.Empty;
                try
                {
                    if (Constants.client == Constants.Clients.VioTrans || Constants.client == Constants.Clients.Eliton || Constants.client == Constants.Clients.RomVega)
                        nrAutoID = WindDatabase.ExecuteScalar(new Query(String.Format("select MPD.NrInmatriculareFCT_ID from Masini_Proprii_Detalii as MPD where REPLACE(RTRIM(LTRIM(MPD.NrAutoMasiniProprii)),'-', '') LIKE '{0}'", nrAutoToMatch)));
                    else
                        nrAutoID = WindDatabase.ExecuteScalar(new Query(String.Format("select MPD.NrInmatriculareFCT_ID from Masini_Proprii_Detalii as MPD where MPD.NrAutoMasiniProprii NOT like '%rad%' AND REPLACE(RTRIM(LTRIM(MPD.NrAutoMasiniProprii)),' ', '') LIKE '{0}'", nrAutoToMatch)));
                }
                catch
                {
                    nrAutoID = string.Empty;
                }
                #endregion [1]


                #region [2] Parsez datele

                var rowFoundID = UtilsGeneral.ToInteger(nrAutoID, 0);
                return rowFoundID;

                #endregion [2]
            }
            return null;
        }
        #endregion Nr Auto Matches

        #region Remove Duplicate Items

        private bool PompaNotFound(CombustibilPompa currentPompa)
        {
            try
            {
                var objectFound = DataSetDBFurnizor.Tables[0].Select("HashCode = " + currentPompa.HashCode.ToString()).FirstOrDefault();
                if (objectFound != null) return false;
            }
            catch { }
            return true;
        }
        #endregion Remove Duplicate Items
    }
}
