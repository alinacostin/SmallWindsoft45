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
using System.ComponentModel;
using System.Collections;
using System.Globalization;
using Base.Configuration;
using System.Data.SqlClient;

namespace Base.Imports
{
    public class ImportCombustibil
    {
        #region Enum Tip Furnizor

        public enum TipFurnizorImportat : int
        {     
            [Description("Rompetrol")]
            Rompetrol = 1,
            [Description("FDE")]
            Fde = 2,
            [Description("Shel")]
            Shel = 3,
            [Description("SmartDiesel")]
            SmartDiesel = 4,
            [Description("EuroShell")]
            EuroShell = 5,
            [Description("Daars")]
            Daars = 6,
            [Description("Mol")]
            Mol = 7,
            [Description("ToolColect")] // TollVerag
            ToolColect = 8,
            [Description("IDS")]
            IDS = 9,
            [Description("OMV")]
            OMV = 10,
            [Description("Proprie")]
            AlteTipuri = 11,
            [Description("EliTrans")]
            EliTrans = 12,
            [Description("Axess")]
            Axess = 13,
            [Description("Telepass")]
            Telepass = 14,
            [Description("As24")]
            As24 = 15,
            [Description("EUROWAG")]
            EuroWag = 16,
            [Description("TollVerag")]
            ToolVerag = 17,
            [Description("TSV")]
            TSV = 18,
            [Description("DKV")]
            DKV = 19,
            [Description("LUKOIL")]
            Lukoil = 20,
            [Description("Hugo")]
            Hugo = 21,
            [Description("UTA")]
            UTA = 22,
            [Description("OSCAR")]
            Oscar = 23,
            [Description("KARACAN")]
            KARACAN = 24,
            [Description("ASFINAG")]
            ASFINAG = 25,
            [Description("UNTRR")]
            UNTRR = 26,
        }

        #endregion Enum Tip Furnizor

        #region Properties

        public int FurnizorImportat { get; set; }

        public int FilterFirmaCurenta { get; set; }

        public string FileName { get; set; }

        public DataTable MasterTable { get; set; }

        public string MasterSheetName { get; set; }

        public int MasterIndex { get; set; }

        public DateTime DataOraImport { get; set; }

        public string UserImport { get; set; }

        public string Error { get; set; }

        public DataSet DataSetDBFurnizor { get; set; }

        private bool IsFromFinanciarOpened { get; set; }

        private bool IsAnexaFactura { get; set; }

        private int IDAnexaFactura { get; set; }

        private string ValutaAnexaFactura { get; set; }

        private string NrFacturaAnexa { get; set; }

        private DateTime DataFacturaAnexa { get; set; }

        public bool ImportRealizat;

        public string ImportMessage { get; set; }

        private int NoOfRowsFromDB = 0;

        private bool IsFrantaSpania = false;

        private bool IsShellAutostradaItalia5 { get; set; }

        private DateTime DataFacturaFisier { get; set; }

        private int cotaTVARonDefault;

        #endregion Properties

        #region Constructors

        public ImportCombustibil(string fileName, DateTime dataImport, string userImport, bool isFinanciar, bool isAnexaFactura, int idAnexaFactura, string valutaAnexaFactura, string nrFacturaAnexa, DateTime dataFacturaAnexa)
        {
            FileName = fileName;
            Error = string.Empty;
            DataOraImport = dataImport;           
            UserImport = userImport;
            DataSetDBFurnizor = new DataSet();
            DataSetDBFurnizor.Tables.Add("Carduri");
            DataSetDBFurnizor.Tables.Add("Masini");
            //DataSetDBFurnizor.Tables.Add("MasiniTerti");
            DataSetDBFurnizor.Tables.Add("Masini_Terti_Carduri");
            DataSetDBFurnizor.Tables.Add("Produse");
            DataSetDBFurnizor.Tables.Add("Masini_Proprii_Carduri");
            DataSetDBFurnizor.Tables.Add("Perioada_Blocare_Balanta");
            IsFromFinanciarOpened = isFinanciar;
            IsAnexaFactura = isAnexaFactura;
            IDAnexaFactura = idAnexaFactura;
            ValutaAnexaFactura = valutaAnexaFactura;
            NrFacturaAnexa = nrFacturaAnexa;
            DataFacturaAnexa = dataFacturaAnexa;
            ImportMessage = "";
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
                    string connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"");
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
                Error = "Formatul fisierului selectat este invalid. Fisierul trebuie sa fie de tipul XCEL.";
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
                case (int)TipFurnizorImportat.Fde:
                    ExtractFde();
                    break;
                case (int)TipFurnizorImportat.Rompetrol:
                    ExtractRompetrol();
                    break;
                case (int)TipFurnizorImportat.EuroShell:
                    ExtractEuroShell();
                    break;
                case (int)TipFurnizorImportat.Shel:
                    ExtractShell();
                    break;
                case (int)TipFurnizorImportat.Telepass:
                    ExtractTelepass();
                    break;
                case (int)TipFurnizorImportat.SmartDiesel:
                    ExtractSmartDiesel();
                    break;
                case (int)TipFurnizorImportat.Mol:
                    ExtractMol();
                    break;
                case (int)TipFurnizorImportat.Daars:
                    ExtractDaars();
                    break;
                case (int)TipFurnizorImportat.ToolColect:
                    ExtractToolColect();
                    break;
                case (int)TipFurnizorImportat.ToolVerag:
                    ExtractToolVerag();
                    break;
                case (int)TipFurnizorImportat.IDS:
                    ExtractIDS();
                    break;
                case (int)TipFurnizorImportat.OMV:
                    ExtractOMV();
                    break;
                case (int)TipFurnizorImportat.EliTrans:
                    ExtractEliTrans();
                    break;
                case (int)TipFurnizorImportat.Axess:
                    ExtractAxxes();
                    break;
                case (int)TipFurnizorImportat.As24:
                    ExtractAs24();
                    break;
                case (int)TipFurnizorImportat.EuroWag:
                    ExtractEuroWag();
                    break;
                case (int)TipFurnizorImportat.TSV:
                    ExtractTSV();
                    break;
                case (int)TipFurnizorImportat.DKV:
                    ExtractDKV();
                    break;
                case (int)TipFurnizorImportat.Lukoil:
                    ExtractLukoil();
                    break;
                case (int)TipFurnizorImportat.Hugo:
                    ExtractHugo();
                    break;
                case (int)TipFurnizorImportat.UTA:
                    ExtractUTA();
                    break;
                case (int)TipFurnizorImportat.Oscar:
                    ExtractOscar();
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
            string filename = string.Empty;
            if (FileName.LastIndexOf("\\") > 1)
                filename = FileName.Substring(FileName.LastIndexOf("\\") + 1).ToLower().Trim().Replace(" ", string.Empty);
            else
                filename = FileName.ToLower().Trim().Replace(" ", string.Empty);

            if (filename.Contains("fde"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Fde;
            }

            else if (filename.Contains("euroshell"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.EuroShell;
            }

            else if (filename.Contains("telepass"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Telepass;
            }

            else if (filename.Contains("shel"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Shel;
            }

            else if (filename.Contains("rompetrol"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Rompetrol;
            }

            else if (filename.Contains("axes") || filename.Contains("axxe"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Axess;
            }

            else if (filename.Contains("daar"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Daars;
            }

            else if (filename.Contains("diesel"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.SmartDiesel;
            }

            else if (filename.Contains("mol"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Mol;
            }

            else if (filename.Contains("tool") || filename.Contains("toll"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.ToolColect;
            }

            else if (filename.Contains("verag") || filename.Contains("verrag"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.ToolVerag;
            }
            else if (filename.Contains("ids"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.IDS;
            }

            else if (filename.Contains("omv"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.OMV;
            }

            else if (filename.Contains("eli"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.EliTrans;
            }

            else if (filename.Contains("cardas"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.As24;
            }

            else if (filename.Contains("eurowag") || filename.Contains("euro wag"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.EuroWag;
            }
            else if (filename.Contains("tsv"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.TSV;
            }
            else if (filename.Contains("dkv"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.DKV;
            }
            else if (filename.Contains("lukoil"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Lukoil;
            }
            else if (FileName.ToLower().Trim().Contains("hugo") || FileName.ToLower().Trim().Contains("hu-go"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Hugo;
            }
            else if (filename.ToLower().Trim().Contains("uta"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.UTA;
            }
            else if (filename.Contains("oscar"))
            {
                FurnizorImportat = (int)TipFurnizorImportat.Oscar;
            }
            else
            {
                FurnizorImportat = (int)TipFurnizorImportat.AlteTipuri;
            }

            LoadDBWithFurnizor();
        }

        private void LoadDBWithFurnizor()
        {
            try
            {                
                WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Carduri" },
                    "sp__ImportCombustibil_Furnizor_Load", new object[] { 1, FurnizorImportat, IsFromFinanciarOpened });

                if (DataSetDBFurnizor.Tables[0].Rows.Count != 0) NoOfRowsFromDB = 1;

                try
                {
                    WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Masini" }, new Query(string.Format(@"select MPD.NrInmatriculareFCT_ID, MPD.NrAutoMasiniProprii, MPD.NumeCodFCT, MPD.AnulareNrAutoMasiniProprii, MPD.DataRadiere from Masini_Proprii_Detalii as MPD ")));
                    //WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "MasiniTerti" }, new Query(@"select MT.ID_MasinaMT, MT.NumarInmatriculareMT from Masini_Terti AS MT WHERE MasinaCaraus=1 AND ISNULL(MT.Anulata,0)=0 "));
                    WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Masini_Terti_Carduri" }, new Query(@"SELECT Card_ID,NrAutoTerti_ID,DataOraStart,DataOraEnd FROM Masini_Terti_Carduri"));
                    WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Produse" }, new Query(string.Format(@"select * from Nomenclator_Card_Produse as NCP ")));
                    WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Carduri" }, new Query(string.Format(@"select * from Nomenclator_CarduriMasini as NCM ")));
                    //WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Masini_Proprii_Carduri" }, new Query(string.Format(@"select * from Masini_Proprii_Carduri as MPC ")));
                    //Modificat pentru Tichet Intern Id 2783 - Import Telepass model ComDivers
                    WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Lista_Tari" }, new Query(string.Format(@"select * from Lista_Tari")));
                    WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Masini_Proprii_Carduri" }, new Query(string.Format(@"select DATEADD(DAY, DATEDIFF(DAY, CAST('19000101' AS DATETIME), CAST(MPC.DataStart AS DATETIME)), CAST(CAST(ISNULL(MPC.OraStart,'') AS TIME) AS DATETIME)) as DataOraStart, DATEADD(DAY, DATEDIFF(DAY, CAST('19000101' AS DATETIME), CAST(MPC.DataEnd AS DATETIME)), CAST(CAST(ISNULL(MPC.OraEnd,'') AS TIME) AS DATETIME)) AS DataOraEnd, * FROM Masini_Proprii_Carduri AS MPC WITH (NOLOCK)")));
                    // WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Masini_Proprii_Carduri" }, new Query(string.Format(@"SELECT CAST(CAST(MPC.DataStart AS DATE) AS DATETIME) + CAST(ISNULL(MPC.OraStart,'') AS TIME) AS DataOraStart, CAST(CAST(MPC.DataEnd AS DATE) AS DATETIME) + CAST(ISNULL(MPC.OraEnd,'') AS TIME) AS DataOraEnd, * FROM Masini_Proprii_Carduri AS MPC WITH (NOLOCK) ")));
                    WindDatabase.LoadDataSet(DataSetDBFurnizor, new string[] { "Perioada_Blocare_Balanta" }, new Query(string.Format(@"select * from Perioada_Blocare_Balanta where Modul_ID = 11")));
                   
                    cotaTVARonDefault = UtilsGeneral.ToInteger(WindDatabase.ExecuteScalar(string.Format("SELECT C.CotaTVA from Parametri P JOIN Lista_Cota_TVA C On C.CotaTVA_ID=P.Numeric where P.Parametru_ID=5")), 0);

                }
                catch { }
            }
            catch { }
        }

        private bool ValidateImportBalantaEmisa(int pozitieDataFactura)
        {
            if(DataSetDBFurnizor.Tables["Perioada_Blocare_Balanta"] == null || DataSetDBFurnizor.Tables["Perioada_Blocare_Balanta"].Rows.Count == 0)
                return true;

            var listDateFacturare = MasterTable.AsEnumerable().GroupBy(x => UtilsGeneral.ToDateTime(x[pozitieDataFactura])).Select(x => x.Key);
            if (listDateFacturare == null || listDateFacturare.Count() == 0)
                return false;

            foreach (DateTime dataFacturare in listDateFacturare)
            {
                DateTime dataFactura = dataFacturare.Date;
                if (DataSetDBFurnizor.Tables["Perioada_Blocare_Balanta"].AsEnumerable().Any(x => (UtilsGeneral.ToDateTime(x["DataInceput"]).Date <= dataFactura) && (UtilsGeneral.ToDateTime(x["DataSfarsit"]).Date >= dataFactura)))
                    return false;
            }

            return true;
        }


        private bool ValidateImportBalantaEmisa(string numeColoana)
        {

            if (DataSetDBFurnizor.Tables["Perioada_Blocare_Balanta"] == null || DataSetDBFurnizor.Tables["Perioada_Blocare_Balanta"].Rows.Count == 0)
                return true;

            var listDateFacturare = MasterTable.AsEnumerable().GroupBy(x => UtilsGeneral.ToDateTime(x[numeColoana])).Select(x => x.Key);
            if (listDateFacturare == null || listDateFacturare.Count() == 0)
                return false;

            foreach (DateTime dataFacturare in listDateFacturare)
            {
                DateTime dataFactura = dataFacturare.Date;
                if (DataSetDBFurnizor.Tables["Perioada_Blocare_Balanta"].AsEnumerable().Any(x => (UtilsGeneral.ToDateTime(x["DataInceput"]).Date <= dataFactura) && (UtilsGeneral.ToDateTime(x["DataSfarsit"]).Date >= dataFactura)))
                    return false;
            }

            return true;
        }

        private void ExtractOscar()
        {
            DataSet dataSet = CreateDataSetCarduri();

            try
            {
                int NoOfRowsFromDB = 0;

                string numarFactura = string.Empty;
                DateTime dataFactura = new DateTime(1900, 1, 1);
                if (FileName.Contains("-") && FileName.Contains("\\"))
                {
                    if (FileName.Length > FileName.LastIndexOf("\\") + 2)
                    {
                        string[] numarDataFactura = FileName.Substring(FileName.LastIndexOf("\\") + 1).Replace(".xlsx", string.Empty).Replace(".xls", string.Empty).Split('-');
                        if (numarDataFactura.Length > 2)
                        {
                            dataFactura = CorrectDateTime(numarDataFactura[2].ToString().Trim(), false);
                            numarFactura = numarDataFactura[1];
                        }
                    }
                }
                // Comdivers
                foreach (DataRow dataRow in MasterTable.Rows)
                {
                    DataRow card = dataSet.Tables["Carduri"].NewRow();
                    card["NrFactura"] = numarFactura;
                    card["CodTara"] = 38;
                    card["Tara"] = "ROMANIA";
                    card["LocatieStatie"] = dataRow["Statie"].ToString();
                    card["IntrareLocatie"] = dataRow["Statie"].ToString();
                    card["DataFacturare"] = CorrectDateTime(dataFactura.ToString().Trim(), false);
                    card["DataLivrare"] = (UtilsGeneral.ToDateTime(dataRow["Business Day"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["Business Day"]).Date);
                    card["OraLivrare"] = (UtilsGeneral.ToDateTime(dataRow["Ora"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["Ora"]));
                    card["Kilometri"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow["Mileage"].ToString()), 0.0M);
                    card["CardPan"] = dataRow[4].ToString().Substring(2, 18);
                    card["Card_ID"] = CardIDFindAndMatch(card["CardPan"].ToString(), UtilsGeneral.ToDateTime(card["DataLivrare"]).Date, UtilsGeneral.ToDateTime(card["OraLivrare"]));
                    card["NrAuto"] = dataRow[5].ToString();
                    card["NrAuto_ID"] = ((object)NrAutoIDFindAndMatchFromCardPan_mutareCard(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) ?? DBNull.Value);
                    card["PretPerLitru"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow["Pret"].ToString()), 0.0M);
                    card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow["Volum"].ToString()), 0.0M);

                    card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow["Valoare"].ToString()), 0.0M);
                    card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M) * 100 / (100 + cotaTVARonDefault);
                    card["ValoareTVA"] = UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M) - UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M);
                    card["VATAmount"] = 0.0M;
                    card["Valuta"] = "RON";

                    card["Produs"] = dataRow["Nume Carburant"].ToString();
                    card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(CleanString(card["Produs"].ToString()), string.Empty, FurnizorImportat);

                    card["Retea"] = "OSCAR";
                    card["FurnizorImportat"] = FurnizorImportat;
                    card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["UserImport"] = UserImport;

                    Hashtable parametersForComputeHashCode = new Hashtable();
                    parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                    parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                    parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                    parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                    parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                    parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                    parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                    parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                    parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                    parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                    parametersForComputeHashCode.Add("NrBon", card["NrBon"].ToString());
                    parametersForComputeHashCode.Add("NrAuto", card["NrAuto"].ToString());
                    parametersForComputeHashCode.Add("Tara", card["Tara"].ToString());
                    parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                    card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                    dataSet.Tables["Carduri"].Rows.Add(card);

                    NoOfRowsFromDB++;
                }

                if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    ImportMessage = Error;
                    return;
                }

                if (NoOfRowsFromDB == 0)
                {
                    Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                    ImportRealizat = false;
                    ImportMessage = Error;
                    return;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                ImportRealizat = false;
                ImportMessage = ex.Message;
                return;
            }

            SaveImports(dataSet.Tables["Carduri"]);
        }

        private void ExtractFdeDuncaFinanciar()
        {
            try
            {
                DataSet dataSet = CreateDataSetCarduri();
                try
                {
                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        card["NrFactura"] = NrFacturaAnexa;
                        card["DataLivrare"] = UtilsGeneral.ToDateTime(dataRow[8]);
                        card["OraLivrare"] = UtilsGeneral.ToDateTime(dataRow[8]);
                        card["DataFacturare"] = (DataFacturaAnexa == DateTime.MinValue ? new DateTime(1900, 1, 1) : DataFacturaAnexa);
                        card["LocatieStatie"] = dataRow[2].ToString();
                        card["IntrareLocatie"] = dataRow[1].ToString();
                        card["IesireLocatie"] = dataRow[2].ToString();
                        card["NrAuto"] = dataRow[0].ToString();
                        card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[4].ToString()), 0.0M);
                        card["Valuta"] = "EUR";
                        card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[5].ToString()), 0.0M);
                        card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[6].ToString()), 0.0M);
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[7].ToString()), 0.0M);
                        card["VATAmount"] = UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        card["Retea"] = "FDE";
                        card["FurnizorImportat"] = FurnizorImportat;
                        card["NrAuto_ID"] = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true, UtilsGeneral.ToDateTime(dataRow[8])), 0);
                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi("", CleanString(dataRow[3].ToString()), FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                        parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                        parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private bool ExtractFdeDunca()
        {
            if (!ValidateImportBalantaEmisa(0))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return false;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  DataFacturare =  row[0],
                                  DataTranzactie =   row[1],
                                  Ora = row[2],
                                  ValoareCuTVA =  row[3],
                                  ValoareTVA = row[4],
                                  ValoareFaraTVA = row[5],
                                  CodPan =  row[6],
                                  Produs = row[7],
                                  IntrareLocatie = row[8],
                                  IesireLocatie = row[9],
                                  CodRuta = row[10],
                                  ClasaVehicul = row[11],
                                  Tara = row[12]
                              };
                if (results == null)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine 
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine 
                        + "este in formatul corect. Probabil au fost scoase sau adaugate coloane in Xcel care nu au fost identificate.";
                    return false;
                }

                foreach (var rowImport in results)
                {
                    try
                    {
                        CombustibilCard card = new CombustibilCard();

                        card.Tara = rowImport.Tara.ToString();
                        card.DataFacturare = UtilsGeneral.ToDateTime(rowImport.DataFacturare);
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataTranzactie);
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.Ora);
                        card.ValoareCuTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString()),0);
                        card.ValoareTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString()), 0);
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0);
                        card.CardPan = rowImport.CodPan.ToString();
                        card.IntrareLocatie = rowImport.IntrareLocatie.ToString();
                        card.IesireLocatie = rowImport.IesireLocatie.ToString();
                        card.Produs = rowImport.Produs.ToString();
                        card.CodRuta = rowImport.CodRuta.ToString();
                        card.ClasaVehicul = UtilsGeneral.ToInteger(rowImport.ClasaVehicul,0);                        
                        card.FurnizorImportat = FurnizorImportat;
                        card.Valuta = "EUR";
                        card.Retea = "FDE";

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return false;
            }
        }

        private bool ExtractRompetrolDuncaFleet()
        {
            if (!ValidateImportBalantaEmisa(0))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return false;
            }

            try
            {

                var results = from row in MasterTable.AsEnumerable().Skip(3)
                              select new
                              {
                                  DataLivrare = row[0],
                                  OraLivrare = row[1],
                                  CodStatie = row[2],
                                  NrAuto = row[3],
                                  Produs = row[5],
                                  Cantitate = row[6],
                                  PretPerLitru = row[7],
                                  ValoareCuTVA = row[9],
                                  ValoareTVA = row[10],
                                  ValoareFaraTVA = row[11],
                                  LocatieStatie = row[12],
                                  CardPan = row[13],
                                  Kilometri = row[14],
                                  NrFactura = row[15],
                                  Tara = row[16]
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
                        if (rowImport.Cantitate.ToString().Contains("Tranzactii")
                                    || rowImport.DataLivrare.ToString() == string.Empty
                                        || rowImport.Produs.ToString().Contains("Tip"))
                            continue;

                        CombustibilCard card = new CombustibilCard();
                        // card.Tara = rowImport.Tara.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare);
                        card.DataFacturare = card.DataLivrare;
                        var ora = card.DataLivrare.ToString().Split(' ');
                        card.OraLivrare = UtilsGeneral.ToDateTime(ora[1]);
                        card.CodStatie = rowImport.CodStatie.ToString() != string.Empty ? rowImport.CodStatie.ToString().Replace("&", " ") : rowImport.CodStatie.ToString();
                        card.NrAuto = rowImport.NrAuto.ToString() != string.Empty ? rowImport.NrAuto.ToString().Replace("&", " ") : rowImport.NrAuto.ToString();
                        card.Produs = rowImport.Produs.ToString() != string.Empty ? rowImport.Produs.ToString().Replace("&", " ") : rowImport.Produs.ToString();


                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                        //card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                        card.ValoareFaraTVA = card.ValoareCuTVA - (card.ValoareCuTVA * 24 / 124);
                        // card.ValoareTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString());
                        card.PretPerLitru = ConvertToDecimalIvariantCulture(rowImport.PretPerLitru.ToString());

                        card.LocatieStatie = rowImport.LocatieStatie.ToString() != string.Empty ? rowImport.LocatieStatie.ToString().Replace("&", " ") : rowImport.LocatieStatie.ToString();
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = UtilsGeneral.ToDecimal(rowImport.Kilometri, 0.0M);
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.FurnizorImportat = FurnizorImportat;

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);

                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.Valuta = "RON";
                        card.Retea = "ROMPETROL";
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return false;
            }
        }

        private bool ExtractRompetrolDunca()
        {
            if (!ValidateImportBalantaEmisa(0))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return false;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  DataLivrare = row[0],
                                  OraLivrare = row[1],
                                  CodStatie = "ROMPETROL",//row[2],
                                  NrAuto = row[3],
                                  Produs = row[4],
                                  Cantitate = row[5],
                                  PretPerLitru = row[6],
                                  ValoareCuTVA = row[7],
                                  LocatieStatie = row[8],
                                  CardPan = row[9],
                                  Kilometri = row[10],
                                  NrFactura = row[11]
                              };
                //var results = from row in MasterTable.AsEnumerable().Skip(3)                              
                //              select new
                //              {
                //                  DataLivrare = row[0],
                //                  OraLivrare = row[1],
                //                  CodStatie = row[2],
                //                  NrAuto = row[3],
                //                  Produs = row[5],
                //                  Cantitate = row[6],
                //                  PretPerLitru = row[7],
                //                  ValoareCuTVA = row[9],
                //                  ValoareTVA = row[10],
                //                  ValoareFaraTVA = row[11],
                //                  LocatieStatie = row[12],
                //                  CardPan = row[13],
                //                  Kilometri = row[14],
                //                  NrFactura = row[15],
                //                  Tara = row[16]
                //              };
                if (results == null)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    return false;
                }

                DateTime dataFactura = new DateTime(1900, 1, 1);
                if (FileName.Contains('-'))
                {
                    if (FileName.Length > FileName.LastIndexOf('-') + 2)
                    {
                        string strDataFactura = FileName.Substring(FileName.LastIndexOf('-') + 1).Replace(".xlsx", string.Empty).Replace(".xls", string.Empty).Trim();
                        if (strDataFactura.Length > 0)
                            dataFactura = UtilsGeneral.ToDateTime(strDataFactura, new string[] { "dd.MM.yyyy", "yyyyMMdd" });
                    }
                }

                foreach (var rowImport in results)
                {
                    try
                    {
                        if (rowImport.Cantitate.ToString().Contains("Tranzactii")
                                    || rowImport.DataLivrare.ToString() == string.Empty
                                        || rowImport.Produs.ToString().Contains("Tip"))
                            continue;

                        CombustibilCard card = new CombustibilCard();
                        card.Tara = "RO";
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare);
                        card.DataFacturare = dataFactura;
                        var ora = card.DataLivrare.ToString().Split(' ');
                        card.OraLivrare = UtilsGeneral.ToDateTime(ora[1]);
                        card.CodStatie = rowImport.CodStatie.ToString() != string.Empty ? rowImport.CodStatie.ToString().Replace("&", " ") : rowImport.CodStatie.ToString();
                        card.NrAuto = rowImport.NrAuto.ToString() != string.Empty ? rowImport.NrAuto.ToString().Replace("&", " ") : rowImport.NrAuto.ToString();
                        card.Produs = rowImport.Produs.ToString() != string.Empty ? rowImport.Produs.ToString().Replace("&", " ") : rowImport.Produs.ToString();
                        

                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                        //card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                        card.ValoareFaraTVA = card.ValoareCuTVA - (card.ValoareCuTVA * cotaTVARonDefault / (100 + cotaTVARonDefault));
                        card.ValoareTVA = card.ValoareCuTVA - card.ValoareFaraTVA;
                        //card.PretPerLitru = ConvertToDecimalIvariantCulture(rowImport.PretPerLitru.ToString());
                        if (card.Cantitate != 0)
                            card.PretPerLitru = card.ValoareFaraTVA / card.Cantitate;
                        else
                            card.PretPerLitru = card.ValoareFaraTVA;

                        card.LocatieStatie = rowImport.LocatieStatie.ToString() != string.Empty ? rowImport.LocatieStatie.ToString().Replace("&", " ") : rowImport.LocatieStatie.ToString();
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = UtilsGeneral.ToDecimal(rowImport.Kilometri, 0.0M);
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.FurnizorImportat = FurnizorImportat;

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);

                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.Valuta = "RON";
                        card.Retea = "ROMPETROL";
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return false;
            }
        }
        
        private bool ExtractRompetrolFiladelfia()
        {
            if (!ValidateImportBalantaEmisa(0))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return false;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  Tara = "RO",
                                  DataLivrare = row[0],
                                  OraLivrare = row[1],
                                  CodStatie = "ROMPETROL",
                                  NrAuto = row[3],
                                  Produs = row[5],
                                  Cantitate = row[6],
                                  PretPerLitru = row[7],
                                  ValoareCuTVA = row[9],
                                  LocatieStatie = row[10],
                                  CardPan = row[11],
                                  Kilometri = row[12],
                                  NrFactura = row[13]
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
                        if (rowImport.Cantitate.ToString().Contains("Tranzactii")
                                    || rowImport.DataLivrare.ToString() == string.Empty
                                        || rowImport.Produs.ToString().Contains("Tip"))
                            continue;

                        CombustibilCard card = new CombustibilCard();
                        card.Tara = rowImport.Tara.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare);
                        card.DataFacturare = card.DataLivrare;
                        var ora = card.DataLivrare.ToString().Split(' ');
                        card.OraLivrare = UtilsGeneral.ToDateTime(ora[1]);
                        card.CodStatie = rowImport.CodStatie.ToString() != string.Empty ? rowImport.CodStatie.ToString().Replace("&", " ") : rowImport.CodStatie.ToString();
                        card.NrAuto = rowImport.NrAuto.ToString() != string.Empty ? rowImport.NrAuto.ToString().Replace("&", " ") : rowImport.NrAuto.ToString();
                        card.Produs = rowImport.Produs.ToString() != string.Empty ? rowImport.Produs.ToString().Replace("&", " ") : rowImport.Produs.ToString();


                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                        //card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                        card.ValoareFaraTVA = card.ValoareCuTVA - (card.ValoareCuTVA * cotaTVARonDefault / (100 + cotaTVARonDefault));
                        card.ValoareTVA = card.ValoareCuTVA - card.ValoareFaraTVA;
                        //card.PretPerLitru = ConvertToDecimalIvariantCulture(rowImport.PretPerLitru.ToString());
                        if (card.Cantitate != 0)
                            card.PretPerLitru = card.ValoareFaraTVA / card.Cantitate;
                        else
                            card.PretPerLitru = card.ValoareFaraTVA;

                        card.LocatieStatie = rowImport.LocatieStatie.ToString() != string.Empty ? rowImport.LocatieStatie.ToString().Replace("&", " ") : rowImport.LocatieStatie.ToString();
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = UtilsGeneral.ToDecimal(rowImport.Kilometri, 0.0M);
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.FurnizorImportat = FurnizorImportat;

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto,card.DataLivrare.Value);

                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.Valuta = "RON";
                        card.Retea = "ROMPETROL";
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return false;
            }
        }
        
        private bool ExtractEliTransDunca()
        {
            if (!ValidateImportBalantaEmisa(4))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return false;
            }

            try
            {          
                var results = from row in MasterTable.AsEnumerable().Where(x => x[1].ToString() != string.Empty)
                              select new
                              {
                                  Produs = row[1],
                                  DataLivrare = row[4],                                 
                                  OraLivrare = row[5],
                                  ValaoreCuTVA = row[8],
                                  ValoareTVA = row[9],
                                  ValoareFaraTVA = row[10],                                  
                                  CardPan = row[12],
                                  NrAuto = row[13],
                                  CodRuta = row[14] 
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

                        CombustibilCard card = new CombustibilCard();
                        card.Tara = "ITALIA";
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare);
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.OraLivrare);
                        card.DataFacturare = card.DataLivrare;

                        card.NrAuto = rowImport.NrAuto.ToString() != string.Empty ? rowImport.NrAuto.ToString().Replace("&", " ") : rowImport.NrAuto.ToString();
                        card.CardPan = rowImport.CardPan.ToString() != string.Empty ? rowImport.CardPan.ToString().Replace("&", " ") : rowImport.CardPan.ToString();
                        card.Produs = rowImport.Produs.ToString() != string.Empty ? rowImport.Produs.ToString().Replace("&", " ") : rowImport.Produs.ToString();
                        card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                        card.ValoareTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString());
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValaoreCuTVA.ToString());   
                        
                        card.CodRuta = rowImport.CodRuta.ToString();
                        card.FurnizorImportat = FurnizorImportat;
                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(card.CardPan, card.DataLivrare, card.OraLivrare);
                        if(0 == card.NrAutoID)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);

                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.Valuta = "EUR";
                        card.Retea = "ELI TRANS";
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return false;
            }
        }

        private bool ExtractEuroShellDianthus()
        {
            if (!ValidateImportBalantaEmisa(5))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return false;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  Tara = row[1],
                                  CodStatie = row[3],
                                  LocatieStatie = row[4],
                                  DataLivrare = row[5],
                                  OraLivrare = row[6],
                                  NrFactura = row[8],
                                  NrAuto = row[16],
                                  Kilometri = row[17],
                                  Produs = row[19],
                                  Cantitate = row[20],
                                  Valuta = row[21],
                                  PretPerLitru = row[22],
                                  ValoareCuTVA = row[24],
                                  ValoareTVA = row[26],
                                  ValoareFaraTVA = row[30],
                                  Retea = row[53],
                                  CardPan = row[54],
                                  ValoareCuTVAMoneda = row[24],
                                  ValoareTVAMoneda = row[26],
                                  ValoareFaraTVAMoneda = row[30]
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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = rowImport.Tara.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare);
                        card.DataFacturare = card.DataLivrare;
                        card.LocatieStatie = rowImport.LocatieStatie.ToString();
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.OraLivrare);
                        card.CodStatie = rowImport.CodStatie.ToString();
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        card.Produs = CleanStringMaster(rowImport.Produs.ToString());
                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                        card.Valuta = rowImport.Valuta.ToString();
                        card.PretPerLitru = ConvertToDecimalIvariantCulture(rowImport.PretPerLitru.ToString());                       
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                        card.ValoareTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString());
                        card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                        if (card.ValoareFaraTVA == 0)
                            card.ValoareFaraTVA = card.PretPerLitru * card.Cantitate;
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString());
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Retea = rowImport.Retea.ToString();
                        card.FurnizorImportat = FurnizorImportat;
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return false;
            }
        }

        private bool ExtractAxessDianthus()
        {
            if (!ValidateImportBalantaEmisa(15))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return false;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  Tara = row[9],             
                                  IntrareLocatie = row[14],
                                  IesireLocatie = row[17],                                  
                                  DataLivrare = row[15],
                                  DataFacturare = row[15],
                                  OraLivrare = row[16],
                                  NrFactura = row[0],
                                  NrAuto = row[3],
                                  Kilometri = row[19],
                                  Valuta = row[20],                                  
                                  ValoareCuTVA = row[26],                                  
                                  ValoareFaraTVA = row[26],
                                  Retea = "EuroShell",
                                  CardPan = row[1]
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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = rowImport.Tara.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare);
                        card.DataFacturare = card.DataLivrare;
                        card.IntrareLocatie = rowImport.IntrareLocatie.ToString();
                        card.IesireLocatie = rowImport.IesireLocatie.ToString();
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.OraLivrare);                        
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());                                                
                        card.Valuta = rowImport.Valuta.ToString();                        
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());                        
                        card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());                        
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString());
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Retea = rowImport.Retea.ToString();
                        card.FurnizorImportat = FurnizorImportat;
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);
                        card.ProdusID = ProdusIDFrantaSpaniaFindAndMatch(FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return false;
            }
        }
        
        private bool ExtractAs24Tabita()
        {
            if (!ValidateImportBalantaEmisa(6))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return false;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  Tara = row[8],
                                  CodStatie = row [9],
                                  LocatieStatie = row[10],
                                  DataLivrare = row[6],
                                  DataFacturare = row[11],
                                  Produs = row[4],
                                  Cantitate = row[5],
                                  OraLivrare = row[7],
                                  NrFactura = row[12],
                                  NrAuto = row[23],                                  
                                  Valuta = row[14],
                                  ValoareCuTVA = row[17],
                                  ValoareFaraTVA = row[15],
                                  ValoareTVA = row[16],
                                  Retea = "AS24",
                                  CardPan = row[1],
                                  Kilometri = row[22]

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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = UtilsGeneral.TruncateStringDeLaPanaLa(rowImport.Tara.ToString(),2,0);
                        card.CodStatie = rowImport.CodStatie.ToString();
                        DateTime newDateTime = DateTime.ParseExact(rowImport.DataLivrare.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        card.DataLivrare = newDateTime;
                        newDateTime = DateTime.ParseExact(rowImport.DataFacturare.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        card.DataFacturare = newDateTime;

                        card.LocatieStatie = rowImport.LocatieStatie.ToString();

                        var timeOfDelivery = rowImport.OraLivrare.ToString().ToCharArray(0, rowImport.OraLivrare.ToString().Length);

                        newDateTime = DateTime.ParseExact(rowImport.DataLivrare.ToString() + " " + timeOfDelivery[0].ToString() + timeOfDelivery[1].ToString() + ":" + timeOfDelivery[2].ToString() + timeOfDelivery[3], "yyyyMMdd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        card.OraLivrare = newDateTime;
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        card.Valuta = rowImport.Valuta.ToString();
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                        card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                        card.ValoareTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString());
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString());
                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Retea = rowImport.Retea.ToString();
                        card.FurnizorImportat = FurnizorImportat;
                        card.Produs = rowImport.Produs.ToString().Replace(" ","");
                      
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);

                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return false;
            }
        }
      
        private bool ExtractEuroWagTabita()
        {
            if (!ValidateImportBalantaEmisa(0))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return false;
            }

            try
            {

                

                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  Tara = row[7],
                                  CodStatie = row [6],
                                  LocatieStatie = row[6],
                                  DataLivrare = row[0],
                                  DataFacturare = row[0],
                                  Produs = row[5],
                                  Cantitate = row[4],
                                  OraLivrare = row[1],
                                  NrAuto = row[2],                                  
                                  Valuta = "RON",
                                  ValoareCuTVA = 1,
                                  ValoareFaraTVA = 1,
                                  ValoareTVA = 1,
                                  Retea = "EUROWAG",
                                  CardPan = row[3],
                

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
                            CombustibilCard card = new CombustibilCard();

                            card.Tara = rowImport.Tara.ToString();
                            card.CodStatie = rowImport.CodStatie.ToString();
                            DateTime newDateTime = DateTime.ParseExact(rowImport.DataLivrare.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            card.DataLivrare = newDateTime;
                            card.DataFacturare = card.DataLivrare;

                            card.LocatieStatie = rowImport.LocatieStatie.ToString();

                            card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.OraLivrare);
                            card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                            card.Valuta = rowImport.Valuta.ToString();
                            card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                            card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                            card.ValoareTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString());
                            card.CardPan = rowImport.CardPan.ToString();
                            card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                            card.Retea = rowImport.Retea.ToString();
                            card.FurnizorImportat = FurnizorImportat;
                            card.Produs = rowImport.Produs.ToString();

                            card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);

                            card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                            if (card.NrAutoID == null || card.NrAutoID == 0)
                                card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);

                            card.ComputeHashCode(IsFromFinanciarOpened);
                            if (NoOfRowsFromDB == 0)
                            {
                                if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                                else card.ExecuteSave(card, DataOraImport, UserImport);
                                ImportRealizat = true;
                            }
                            else if (CardNotFound(card))
                            {
                                if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                                else card.ExecuteSave(card, DataOraImport, UserImport);
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
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return false;
            }
        }
        /*
        private bool ExtractEuroWagMarvi()
        {
            if (!ValidateImportBalantaEmisa(1))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return false;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  Tara = row[17],
                                  CodStatie = row[3],
                                  LocatieStatie = row[9],
                                  DataLivrare = row[1],
                                  DataFacturare = row[1],
                                  Produs = row[8],
                                  Cantitate = row[7],
                                  OraLivrare = row[2],
                                  NrAuto = row[5],
                                  Valuta = row[16],
                                  ValoareCuTVA = row[13],
                                  ValoareFaraTVA = row[13],
                                  ValoareTVA = 0,
                                  Retea = "EUROWAG",
                                  CardPan = row[6],

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
                        if (rowImport.Produs.ToString().Contains("Action price"))
                            continue;

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = rowImport.Tara.ToString();
                        card.CodStatie = rowImport.CodStatie.ToString();
                        DateTime newDateTime = DateTime.ParseExact(rowImport.DataLivrare.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        card.DataLivrare = newDateTime;
                        card.DataFacturare = card.DataLivrare;

                        card.LocatieStatie = rowImport.LocatieStatie.ToString();

                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.OraLivrare);
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        card.Valuta = rowImport.Valuta.ToString();
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                        card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                        card.ValoareTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString());
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                        card.Retea = rowImport.Retea.ToString();
                        card.FurnizorImportat = FurnizorImportat;
                        card.Produs = rowImport.Produs.ToString();

                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);

                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return false;
            }
        }
        */
        private bool ExtractEuroShellFrantaSpaniaDianthus()
        {
            if (!ValidateImportBalantaEmisa(15))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return false;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  Tara = row[9],
                                  IntrareLocatie = row[14],
                                  IesireLocatie = row[17],
                                  DataLivrare = row[15],
                                  DataFacturare = row[15],
                                  OraLivrare = row[16],
                                  NrFactura = row[0],
                                  NrAuto = row[3],
                                  Kilometri = row[19],
                                  Valuta = row[20],
                                  ValoareCuTVA = row[26],
                                  ValoareFaraTVA = row[26],
                                  Retea = "EuroShell",
                                  CardPan = row[1]
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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = rowImport.Tara.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare);
                        card.DataFacturare = card.DataLivrare;
                        card.IntrareLocatie = rowImport.IntrareLocatie.ToString();
                        card.IesireLocatie = rowImport.IesireLocatie.ToString();
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.OraLivrare);
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        card.Valuta = rowImport.Valuta.ToString();
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                        card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString());
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Retea = rowImport.Retea.ToString();
                        card.FurnizorImportat = FurnizorImportat;
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);
                        card.ProdusID = ProdusIDFrantaSpaniaFindAndMatch(FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return false;
            }
        }
/*
        private void ExtractShellAlex()
        {
            if (!ValidateImportBalantaEmisa(4))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {   
                                  NrFactura = row[0],
                                  DataFactura = row[1],
                                  Tara = row[3],
                                  DataLivrare = row[4],
                                  CardPan = row[7],
                                  NrAuto = row[8],
                                  Kilometri = row[9],
                                  CodStatie = row[11],
                                  Produs = row[13],
                                  Valuta = row[14],
                                  Cantitate = row[15],
                                  PretPerLitru = row[16],
                                  ValoareFaraTVA = row[20],
                                  ValoareTVA = row[21],
                                  ValoareCuTVA = row[22]                                  
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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = rowImport.Tara.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare);
                        card.DataFacturare = card.DataLivrare;
                        card.CodStatie = rowImport.CodStatie.ToString();
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.Produs = rowImport.Produs.ToString();
                        card.Cantitate = UtilsGeneral.ToDecimal(CleanString(rowImport.Cantitate.ToString()), 0.0M);
                        card.Valuta = rowImport.Valuta.ToString();
                        card.PretPerLitru = UtilsGeneral.ToDecimal(CleanString(rowImport.PretPerLitru.ToString()), 0.0M);
                        card.ValoareCuTVA = UtilsGeneral.ToDecimal(CleanString(rowImport.ValoareCuTVA.ToString()), 0.0M);
                        card.ValoareTVA = UtilsGeneral.ToDecimal(rowImport.ValoareTVA, 0.0M);
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(CleanString(rowImport.ValoareFaraTVA.ToString()), 0.0M);
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = UtilsGeneral.ToDecimal(CleanString(rowImport.Kilometri.ToString()), 0.0M);
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Retea = "SHELL";
                        card.FurnizorImportat = FurnizorImportat;
                        if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                        else card.ExecuteSave(card, DataOraImport, UserImport);
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
*/        
        public DataSet CreateDataSetCarduri()
        {
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add("Carduri");
            dataSet.Tables["Carduri"].Columns.Add("Tara", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("CodStatie", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("LocatieStatie", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("DataLivrare", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("OraLivrare", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("DataFacturare", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("IntrareLocatie", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("IesireLocatie", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("NrAuto", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("NrFactura", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Kilometri", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("Produs", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Cantitate", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("Valuta", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("PretPerLitru", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("ValoareFaraTVA", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("ValoareTVA", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("ValoareCuTVA", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("VATAmount", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("Kilometraj", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Retea", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("CardPan", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("CodRuta", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("ClasaVehicul", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("FurnizorImportat", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("NrAuto_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("NrAutoTerti_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("Furnizor_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("Produs_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("Card_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("FacturaPrimita_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("CodTara", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("DataImport", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("OraImport", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("UserImport", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("HashCode", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("ImportatInFoaieParcurs", typeof(Boolean));
            dataSet.Tables["Carduri"].Columns.Add("ImportatInDecontCursa", typeof(Boolean));
            dataSet.Tables["Carduri"].Columns.Add("TipStatie", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("ValoareFaraTVAEUR", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("NomenclatorEbidta_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("Observatii", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("NrBon", typeof(String));
            return dataSet;
        }

        private void ExtractShellCardFacturi()
        {
            if (MasterTable.Columns[0].ColumnName == "Order Number")
            {
                ExtractShellCardFacturiTip1();
                return;
            }
            ExtractShellCardFacturiDefault();
        }

        private void ExtractShellCardFacturiDefault()
        {
            if (!ValidateImportBalantaEmisa("Data"))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            foreach (DataColumn col in MasterTable.Columns)
            {
                col.ColumnName = col.ColumnName.RemoveDiacriticsFromString();
            }

            try
            {
                DataSet dataSet = CreateDataSetCarduri();
                try
                {
                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        //card["NrFactura"] = dataRow["Număr factură"].ToString();
                        card["NrFactura"] = dataRow["Numar factura"].ToString();
                        //card["CodTara"] = UtilsGeneral.ToInteger(dataRow["Cod țară"].ToString(), 0) == 38 ? 38 : 0; // ROMANIA = 38
                        card["CodTara"] = UtilsGeneral.ToInteger(dataRow["Cod tara"].ToString(), 0) == 38 ? 38 : 0; // ROMANIA = 38
                        //card["Tara"] = dataRow["Țara tranzacţiei"].ToString();
                        //card["Tara"] = dataRow["Tara tranzactiei"].ToString();
                        card["Tara"] = dataRow["Tara"].ToString();
                        DateTime dataLivrare = (UtilsGeneral.ToDateTime(dataRow["Data"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["Data"]));
                        //DateTime oraLivrare = (UtilsGeneral.ToDateTime(dataRow["Card utilizat între orele#"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["Card utilizat între orele#"]));
                        //DateTime oraLivrare = (UtilsGeneral.ToDateTime(dataRow["Card utilizat intre orele#"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["Card utilizat intre orele#"]));
                        DateTime oraLivrare = (UtilsGeneral.ToDateTime(dataRow["Ora"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["Ora"]));
                        card["DataLivrare"] = dataLivrare;
                        card["OraLivrare"] = oraLivrare;
                        card["DataFacturare"] = (UtilsGeneral.ToDateTime(dataRow["Data facturii "]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["Data facturii "]));
                        //card["LocatieStatie"] = dataRow["Nume staţie"].ToString();
                        card["LocatieStatie"] = dataRow["Nume statie"].ToString();
                        //card["CodStatie"] = dataRow["Cod staţie"].ToString();
                        card["CodStatie"] = dataRow["Cod statie"].ToString();
                        //card["NrAuto"] = NrAutoFind(dataRow["Număr vehicul"].ToString(), false, dataLivrare.Date + oraLivrare.TimeOfDay);
                        //card["NrAuto"] = NrAutoFind(dataRow["Numar vehicul"].ToString(), false, dataLivrare.Date + oraLivrare.TimeOfDay);
                        card["NrAuto"] = NrAutoFind(dataRow["Vehicul"].ToString(), false, dataLivrare.Date + oraLivrare.TimeOfDay);
                        card["Produs"] = Base.Imports.RemoveDiacritics.RemoveDiacriticsFromString((dataRow["Nume produs"].ToString().IndexOf('-') > 0 ? dataRow["Nume produs"].ToString().Substring(dataRow["Nume produs"].ToString().IndexOf('-') + 2) : dataRow["Nume produs"].ToString()));
                        card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Cantitate"].ToString()), 0.0M);
                        card["Valuta"] = "EUR";
                        //card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Valoare netă euro"].ToString()), 0.0M);
                        //card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Valoare neta in moneda clientului"].ToString()), 0.0M);
                        card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Valoare neta in EURO"].ToString()), 0.0M);
                        card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Valoarea TVA (EURO)"].ToString()), 0.0M);
                        //card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Valoare netă euro"].ToString()), 0.0M) + UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Valoarea TVA (EURO)"].ToString()), 0.0M);
                        //card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Valoare neta in moneda clientului"].ToString()), 0.0M) + UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Valoarea TVA (EURO)"].ToString()), 0.0M);
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Valoare neta in EURO"].ToString()), 0.0M) + UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Valoarea TVA (EURO)"].ToString()), 0.0M);
                        card["PretPerLitru"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) / (UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M) == 0.0M ? 1 : UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        card["VATAmount"] = UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        //card["CardPan"] = dataRow["Număr complet card"].ToString().Replace("'", string.Empty);
                        card["CardPan"] = dataRow["Numar complet card"].ToString().Replace("'", string.Empty);
                        //card["Kilometraj"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Citire kilometraj"].ToString()), 0.0M);
                        card["Kilometraj"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Kilometraj"].ToString()), 0.0M);
                        card["Retea"] = "SHELL";
                        card["FurnizorImportat"] = FurnizorImportat;
                        //card["NrAuto_ID"] = (NrAutoIDFindAndMatchFromCardPan(CleanString(card["NrAuto"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataLivrare.Date + oraLivrare.TimeOfDay));
                        int nrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        card["NrAuto_ID"] = nrAutoID == 0 ? NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataLivrare.Date + oraLivrare.TimeOfDay) : nrAutoID;
                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi("", CleanString(dataRow["Cod produs"].ToString()), FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);


                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                        parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                        parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));


                        try
                        {
                            DataTable dt = WindDatabase.ExecuteDataSet("spGetParameters ", new object[] { "ImpShellDolo" }).Tables[0];
                            if (null != dt && dt.Rows.Count > 0 && UtilsGeneral.ToBool(dt.Rows[0]["Vizibil"]))
                            {
                                if (UtilsGeneral.ToDateTime(card["DataLivrare"]) >= UtilsGeneral.ToDateTime("2016-10-19"))
                                    parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());

                                if (UtilsGeneral.ToDateTime(card["DataLivrare"]) < UtilsGeneral.ToDateTime("2016-10-19"))
                                    parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());

                            }
                            else
                            {
                                parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                            }
                        }
                        catch
                        {
                            parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        }

                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        //folosit de Dolotrans
        private void ExtractShellCardFacturiTip1()
        {
            if (!ValidateImportBalantaEmisa("Delivery Date"))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                DataSet dataSet = CreateDataSetCarduri();
                try
                {
                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        card["NrFactura"] = dataRow["Order Number"].ToString();
                        card["CodTara"] = dataRow["Country Id"].ToString().EndsWith("38") ? 38 : 0; // ROMANIA = 38
                        card["Tara"] = dataRow["Country Name"].ToString();
                        DateTime dataOraLivrare = (UtilsGeneral.ToDateTime(dataRow["Delivery Date"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["Delivery Date"]));
                        card["DataLivrare"] = dataOraLivrare;
                        card["OraLivrare"] = dataOraLivrare;
                        card["DataFacturare"] = (UtilsGeneral.ToDateTime(dataRow["Order Date"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["Order Date"]));
                        card["LocatieStatie"] = dataRow["Station Name"].ToString();
                        card["CodStatie"] = dataRow["Station Id"].ToString();
                        card["NrAuto"] = NrAutoFind(dataRow["Vehicle"].ToString(), false, dataOraLivrare);
                        card["Produs"] = Base.Imports.RemoveDiacritics.RemoveDiacriticsFromString((dataRow["Product Name"].ToString().IndexOf('-') > 0 ? dataRow["Product Name"].ToString().Substring(dataRow["Product Name"].ToString().IndexOf('-') + 2) : dataRow["Product Name"].ToString()));
                        card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Local Quantity"].ToString()), 0.0M);
                        card["Valuta"] = "EUR";
                        card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Net Amount"].ToString()), 0.0M);
                        card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Vat Amount"].ToString()), 0.0M);
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Net Amount"].ToString()), 0.0M) + UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Vat Amount"].ToString()), 0.0M);
                        card["PretPerLitru"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) / (UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M) == 0.0M ? 1 : UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        card["VATAmount"] = UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        card["CardPan"] = dataRow["Card"].ToString().Replace("'", string.Empty);
                        card["Kilometraj"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Kilometrage"].ToString()), 0.0M);
                        card["Retea"] = "SHELL";
                        card["FurnizorImportat"] = FurnizorImportat;
                        //card["NrAuto_ID"] = (object)(NrAutoIDFindAndMatchFromCardPan(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) == null ? 0 : NrAutoIDFindAndMatch(ClearITR(card["NrAuto"].ToString()), dataOraLivrare)) ?? DBNull.Value;
                        int nrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        card["NrAuto_ID"] = nrAutoID == 0 ? NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataOraLivrare.Date + dataOraLivrare.TimeOfDay) : nrAutoID;
                        //card["NrAuto_ID"] = (NrAutoIDFindAndMatchFromCardPan(CleanString(card["NrAuto"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString()));
                        //card["NrAuto_ID"] = (NrAutoIDFindAndMatch(card["NrAuto"].ToString()) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString()));
                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi("", CleanString(dataRow["Product Id"].ToString().TrimStart('0')), FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        //a fost adaugat in HashCode ulterior si pentru tranzactiile de dinainte de 19.10.2016 nu trebuie luat in calcul 
                        //in momentul in care nu se mai importa tranzactii de dinainte de 19.10.2016 poate fi comentat
                        if (UtilsGeneral.ToDateTime(card["DataLivrare"]) >= UtilsGeneral.ToDateTime("2016-10-19"))
                            parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        //end
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                        parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                        parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        //A existat un bug, in "CodStatie" se aducea "NrAuto"
                        //Pentru a nu se importa duplicate, pentru importurile pana in 19.10.2016 nu trebuie luat in calcul "CodStatie ci "NrAuto"
                        //in momentul in care nu se mai importa tranzactii de dinainte de 19.10.2016 poate fi comentat
                        if (UtilsGeneral.ToDateTime(card["DataLivrare"]) < UtilsGeneral.ToDateTime("2016-10-19"))
                            parametersForComputeHashCode.Add("CodStatie", card["NrAuto"].ToString());
                        //end
                        //parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private void ExtractMolCard()
        {
            if (!ValidateImportBalantaEmisa("TRX Date"))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                DataSet dataSet = CreateDataSetCarduri();

                try
                {
                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        DateTime dataOraLivrare = (UtilsGeneral.ToDateTime(dataRow["TRX Date"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["TRX Date"]));
                        card["DataLivrare"] = dataOraLivrare;
                        card["OraLivrare"] = dataOraLivrare;
                        card["LocatieStatie"] = dataRow["Station Address"].ToString();
                        card["NrAuto"] = NrAutoFind(dataRow["Car Plate no#"].ToString().Replace(" ",""), false, dataOraLivrare);
                        card["Produs"] = Base.Imports.RemoveDiacritics.RemoveDiacriticsFromString((dataRow["Product Name"].ToString().IndexOf('-') > 0 ? dataRow["Product Name"].ToString().Substring(dataRow["Product Name"].ToString().IndexOf('-') + 2) : dataRow["Product Name"].ToString()));
                        card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Quantity"].ToString()), 0.0M);
                        card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["TRX Net Amount"].ToString()), 0.0M);
                        card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["VAT"].ToString()), 0.0M);
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["TRX Value"].ToString()), 0.0M);
                        card["PretPerLitru"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) / (UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M) == 0.0M ? 1 : UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        card["VATAmount"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["VAT"].ToString()), 0.0M);
                        card["CardPan"] = dataRow["PAN/LPI"].ToString().Replace("'", string.Empty);
                        card["Kilometraj"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow["Mileage"].ToString()), 0.0M);
                        card["Retea"] = "MOL";
                        card["FurnizorImportat"] = FurnizorImportat;
                        //card["NrAuto_ID"] = (NrAutoIDFindAndMatchFromCardPan(CleanString(card["NrAuto"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataOraLivrare));
                        int nrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        card["NrAuto_ID"] = nrAutoID == 0 ? NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataOraLivrare.Date + dataOraLivrare.TimeOfDay) : nrAutoID;
                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(CleanString(card["Produs"].ToString()), CleanString(dataRow["CodProdus"].ToString()), FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }
        /*
        private void ExtractShellTelepassDianthus()
        {
            if (!ValidateImportBalantaEmisa(1))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()                              
                              select new
                              {                                  
                                  DataFactura = row[0],                                  
                                  DataLivrare = row[1],
                                  OraLivrare = row[2],
                                  ValoareCuTVA = row[3],
                                  NrAuto = row[4],
                                  CardPan = row[5],
                                  Produs = row[6],
                                  LocatieIntrare = row[7],
                                  LocatieIesire = row[8],
                                  CodRuta = row[9],
                                  Tara = "Italia",
                                  Valuta = "EUR", //row[14], // aici va fi implicit EUR pt SHELL
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

                        CombustibilCard card = new CombustibilCard();

                        
                        card.Tara = rowImport.Tara.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime((rowImport.OraLivrare.ToString()));
                        card.DataFacturare = UtilsGeneral.ToDateTime((rowImport.DataFactura.ToString()));
                        
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        card.Produs = rowImport.Produs.ToString();
                        
                        card.Valuta = rowImport.Valuta.ToString();
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());                        
                        
                        card.CardPan = CleanString(rowImport.CardPan.ToString());
                        card.IntrareLocatie = rowImport.LocatieIntrare.ToString();
                        card.IesireLocatie = rowImport.LocatieIesire.ToString();
                        card.Retea = "TELEPASS";
                        card.FurnizorImportat = FurnizorImportat;
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        /*
        private void ExtractShellDuncaCarduri()
        {
            if (!ValidateImportBalantaEmisa(3))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              where ((row[0].ToString() != string.Empty || row[0].ToString().Length != 0) && row[12].ToString() != "098") // fara VAT REFUND
                              select new
                              {
                                  Tara = row[0],
                                  CodStatie = row[1],
                                  LocatieStatie = row[2],
                                  DataLivrare = row[3],
                                  OraLivrare = row[4],
                                  NrAuto = row[5],
                                  Kilometri = row[6],
                                  Produs = row[7],
                                  Cantitate = row[8],
                                  Valuta = "EUR", //row[9], // aici va fi implicit EUR pt SHELL                                  
                                  DataFactura = row[10],
                                  NrFactura = row[11],
                                  ValoareCuTVA = row[12],
                                  ValoareFaraTVA = row[13],
                                  VATAmount = row[15],
                                  ValoareTVA = row[15],
                                  CardPan = row[16],
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

                        CombustibilCard card = new CombustibilCard();
                        
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Tara = rowImport.Tara.ToString();
                        card.CodStatie = rowImport.CodStatie.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime((rowImport.OraLivrare.ToString()));
                        card.DataFacturare = UtilsGeneral.ToDateTime((rowImport.DataFactura.ToString()));
                        card.LocatieStatie = rowImport.LocatieStatie.ToString();
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        card.Produs = rowImport.Produs.ToString();
                        card.Cantitate = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString()), 0.0M);
                        card.Valuta = rowImport.Valuta.ToString();
                        card.ValoareCuTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString()), 0.0M);
                        card.ValoareTVA = 0;
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0.0M);
                        card.ValoareTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString()), 0.0M);
                        card.PretPerLitru = card.ValoareFaraTVA / (card.Cantitate == 0 ? 1 : card.Cantitate); //UtilsGeneral.ToDecimal(CleanString(rowImport.PretPerLitru.ToString()), 0.0M);
                        card.VATAmount = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.VATAmount.ToString()), 0.0M);
                        card.CardPan = CleanString(rowImport.CardPan.ToString());
                        card.Kilometri = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString()), 0.0M);
                        card.Retea = "SHELL";
                        card.FurnizorImportat = FurnizorImportat;

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);

                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        if (card.DataLivrare <= new DateTime(2013, 7, 27))
                            card.ComputeHashCodeWithoutStationCode(IsFromFinanciarOpened);
                        else
                            card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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

        private void ExtractShellElitonCarduri()
        {
            if (!ValidateImportBalantaEmisa(5))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              where ((row[0].ToString() != string.Empty || row[0].ToString().Length != 0) && row[12].ToString() != "098") // fara VAT REFUND
                              select new
                              {
                                  Tara = row[1],
                                  CodStatie = row[3],
                                  LocatieStatie = row[4],
                                  DataLivrare = row[5],
                                  OraLivrare = row[6],
                                  NrAuto = row[16],
                                  Kilometri = row[17],
                                  Produs = row[19],
                                  Cantitate = row[20],
                                  Valuta = "EUR", //row[9], // aici va fi implicit EUR pt SHELL                                                                    
                                  PretPerLitru = row[22],                                  
                                  ValoareFaraTVA = row[50],
                                  VATAmount = row[52],                                  
                                  CardPan = row[54],
                                  NrFactura = row[42],
                                  DataFactura = row[41]
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

                        CombustibilCard card = new CombustibilCard();

                        
                        card.Tara = rowImport.Tara.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime((rowImport.OraLivrare.ToString()));
                        card.DataFacturare = UtilsGeneral.ToDateTime(rowImport.DataFactura.ToString());
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.LocatieStatie = rowImport.LocatieStatie.ToString().Replace("&"," ");
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        if (card.NrAuto.ToUpper().Contains("ITR"))
                            card.NrAuto = card.NrAuto.Replace("ITR", "");
                        card.Produs = rowImport.Produs.ToString();
                        card.Cantitate = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString()), 0.0M);
                        card.Valuta = rowImport.Valuta.ToString();
                        card.ValoareTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.VATAmount.ToString()), 0.0M);
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0.0M);                        
                        card.PretPerLitru = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.PretPerLitru.ToString()), 0.0M);
                        card.VATAmount = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.VATAmount.ToString()), 0.0M);
                        card.ValoareCuTVA = card.ValoareFaraTVA; // UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()) + card.VATAmount, 0.0M);
                        card.CardPan = CleanString(rowImport.CardPan.ToString());
                        card.Kilometri = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString()), 0.0M);
                        card.Retea = "SHELL";
                        card.FurnizorImportat = FurnizorImportat;

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);

                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
/*
        private void ExtractShellAlexCarduri()
        {
            if (!ValidateImportBalantaEmisa(5))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                                where ((row[0].ToString() != string.Empty || row[0].ToString().Length != 0) && row[12].ToString() != "098") // fara VAT REFUND
                                select new
                                {
                                    Tara = row[1],
                                    CodStatie = row[3],
                                    LocatieStatie = row[4],
                                    DataLivrare = row[5],
                                    OraLivrare = row[6],
                                    NrAuto = row[16],
                                    Kilometri = row[17],
                                    Produs = row[19],
                                    Cantitate = row[20],
                                    Valuta = "EUR", //row[21],                                
                                    //DataFactura = row[10],
                                    NrFactura = row[48],
                                    //ValoareCuTVA = row[24],
                                    ValoareFaraTVA = row[43],
                                    VATAmount = row[45],
                                    ValoareTVA = row[45],
                                    Reteaua = row[53],
                                    CardPan = row[54]                                  
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

                        CombustibilCard card = new CombustibilCard();

                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.CodStatie = rowImport.CodStatie.ToString();
                        card.Tara = rowImport.Tara.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime((rowImport.OraLivrare.ToString()));
                        card.DataFacturare = UtilsGeneral.ToDateTime((rowImport.DataLivrare.ToString()));
                        card.LocatieStatie = rowImport.LocatieStatie.ToString();
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        card.Produs = rowImport.Produs.ToString();
                        card.Cantitate = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString()), 0.0M);
                        card.Valuta = rowImport.Valuta.ToString();
                        //card.ValoareCuTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString()), 0.0M);
                        card.ValoareTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString()), 0.0M);
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0.0M);
                        card.ValoareCuTVA = card.ValoareFaraTVA + card.ValoareTVA;                        
                        card.PretPerLitru = card.ValoareFaraTVA / (card.Cantitate == 0 ? 1 : card.Cantitate); //UtilsGeneral.ToDecimal(CleanString(rowImport.PretPerLitru.ToString()), 0.0M);
                        card.VATAmount = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.VATAmount.ToString()), 0.0M);
                        card.CardPan = CleanString(rowImport.CardPan.ToString());
                        card.Kilometri = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString()), 0.0M);
                        card.Retea = rowImport.Reteaua.ToString().Trim().ToUpper();
                        card.FurnizorImportat = FurnizorImportat;

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);

                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
*/
        /*
        private void ExtractShellViotransCarduri()
        {
            if (!ValidateImportBalantaEmisa(5))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              where ((row[0].ToString() != string.Empty || row[0].ToString().Length != 0) && row[12].ToString() != "098") // fara VAT REFUND
                              select new
                              {
                                  Tara = row[1],
                                  CodStatie = row[3],
                                  LocatieStatie = row[4],
                                  DataLivrare = row[5],
                                  OraLivrare = row[6],
                                  NrAuto = row[16],
                                  Kilometri = row[17],
                                  Produs = row[19],
                                  Cantitate = row[20],
                                  Valuta = row[21], //row[9], // aici va fi implicit EUR pt SHELL
                                  DataFactura = row[5],
                                  NrFactura = row[8], // nr bon
                                  ValoareCuTVA = row[24],
                                  ValoareFaraTVA = row[30],
                                  VATAmount = row[26],
                                  ValoareTVA = row[26],
                                  CardPan = row[54],
                                  Retea = row[53]
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

                        CombustibilCard card = new CombustibilCard();

                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Tara = rowImport.Tara.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime((rowImport.OraLivrare.ToString()));
                        card.DataFacturare = UtilsGeneral.ToDateTime((rowImport.DataFactura.ToString()));
                        card.LocatieStatie = rowImport.LocatieStatie.ToString();
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        card.Produs = rowImport.Produs.ToString();
                        card.Cantitate = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString()), 0.0M);
                        card.Valuta = rowImport.Valuta.ToString();
                        card.ValoareCuTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString()), 0.0M);
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0.0M);
                        card.ValoareTVA = card.ValoareCuTVA - card.ValoareFaraTVA;
                        card.ValoareTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString()), 0.0M);
                        card.PretPerLitru = card.ValoareFaraTVA / (card.Cantitate == 0 ? 1 : card.Cantitate); //UtilsGeneral.ToDecimal(CleanString(rowImport.PretPerLitru.ToString()), 0.0M);
                        card.VATAmount = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.VATAmount.ToString()), 0.0M);
                        card.CardPan = CleanString(rowImport.CardPan.ToString());
                        card.Kilometri = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString()), 0.0M);
                        card.Retea = CleanString(rowImport.Retea.ToString());
                        //card.Retea = "SHELL";
                        card.FurnizorImportat = FurnizorImportat;

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);

                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        /*
        private void ExtractShellTabitaCarduri()
        {
            if (!ValidateImportBalantaEmisa(5))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              where (row[0].ToString() != string.Empty || row[0].ToString().Length != 0)
                              //where ((row[0].ToString() != string.Empty || row[0].ToString().Length != 0) && row[12].ToString() != "098") // fara VAT REFUND
                              select new
                              {
                                  Tara = row[1],
                                  CodStatie = row[3],
                                  LocatieStatie = row[4],
                                  DataLivrare = row[5],
                                  OraLivrare = row[6],
                                  NrFactura = row[8],
                                  NrAuto = row[16],
                                  Kilometri = row[17],
                                  Produs = row[19],
                                  Cantitate = row[20],
                                  Valuta = row[21], // aici va fi implicit EUR pt SHELL                                                                                                      
                                  //ValoareCuTVA = row[24],
                                  ValoareFaraTVA = row[30],
                                  Discount = row[29],
                                  ValoareTVA = row[26],
                                  CardPan = row[48],
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

                        CombustibilCard card = new CombustibilCard();
                        
                        card.Tara = rowImport.Tara.ToString();
                        card.CodStatie = rowImport.CodStatie.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime((rowImport.OraLivrare.ToString()));
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.DataFacturare = UtilsGeneral.ToDateTime((rowImport.DataLivrare.ToString()));
                        card.LocatieStatie = rowImport.LocatieStatie.ToString();
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());
                        card.Produs = rowImport.Produs.ToString();
                        card.Cantitate = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString()), 0.0M);
                        card.Valuta = rowImport.Valuta.ToString();
                        card.ValoareCuTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0.0M) + UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString()), 0.0M);
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0.0M);
                        card.ValoareTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString()), 0.0M);
                        card.PretPerLitru = card.ValoareFaraTVA / (card.Cantitate == 0 ? 1 : card.Cantitate); //UtilsGeneral.ToDecimal(CleanString(rowImport.PretPerLitru.ToString()), 0.0M);
                        card.VATAmount = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString()), 0.0M);
                        card.CardPan = CleanString(rowImport.CardPan.ToString());
                        card.Kilometri = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString()), 0.0M);
                        card.Retea = "SHELL";
                        card.FurnizorImportat = FurnizorImportat;

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);

                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        //if (FilterFirmaCurenta != (int)FirmaCurenta.Marvi)
                            card.ComputeHashCodeWithoutStationCode(IsFromFinanciarOpened);
                        //else
                        //    card.ComputeHashCodeWithValoareCuTVA(IsFromFinanciarOpened);

                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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

        private void ExtractShellVectra()
        {
            if (!ValidateImportBalantaEmisa(2))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                //var results = from row in MasterTable.AsEnumerable()
                //              select new
                //              {                                 
                //                  LocatieStatie = row[0],
                //                  DataLivrare = row[2],
                //                  OraLivrare = row[2],
                //                  CardPan = row[3],
                //                  NrAuto = row[4],
                //                  Produs = row[5],
                //                  Cantitate = row[6],
                //                  Valuta = row[7],
                //                  PretPerLitru = row[8],
                //                  ValoareCuTVA = row[9],
                //                  ValoareTVA = row[11],                                  
                //                  DataFactura = row[12],
                //                  NrFactura = row[13]
                //              };
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  Tara = row[0],
                                  LocatieStatie = row[1],
                                  DataLivrare = row[2],
                                  OraLivrare = row[2],
                                  CardPan = row[3],
                                  NrAuto = row[4],
                                  Produs = row[5],
                                  Cantitate = row[6],
                                  Valuta = row[7],
                                  PretPerLitru = row[8],
                                  A = row[14],
                                  B = row[15],
                                  C = row[16],
                                  DataFactura = row[12],
                                  NrFactura = row[13]
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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = rowImport.Tara.ToString();
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime((rowImport.OraLivrare.ToString()));
                        card.DataFacturare = UtilsGeneral.ToDateTime((rowImport.DataFactura.ToString()));
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.LocatieStatie = rowImport.LocatieStatie.ToString();
                        card.Produs = rowImport.Produs.ToString();
                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                        card.PretPerLitru = ConvertToDecimalIvariantCulture(rowImport.PretPerLitru.ToString());
                        //card.Valuta = rowImport.Valuta.ToString();
                        card.Valuta = "EUR";
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.A.ToString()) +
                                            ConvertToDecimalIvariantCulture(rowImport.B.ToString()) +
                                            ConvertToDecimalIvariantCulture(rowImport.C.ToString());
                             
                        
                        card.CardPan = CleanString(rowImport.CardPan.ToString());                        
                        card.Retea = "SHELL";
                        card.FurnizorImportat = FurnizorImportat;
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        /*
        private void ExtractMolAlex()
        {
            if (!ValidateImportBalantaEmisa(2))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                 NrAuto = row[0],
                                 CardPan = row[1],
                                 DataLivrare = row[2],
                                 OraLivrare = row[3],
                                 Produs = row[6],
                                 Cantitate = row[9],
                                 PretPerLitru = row[10],
                                 Valuta = row[11],
                                 ValoareFaraTVA = row[12],
                                 ValoareTVA = row[13],
                                 ValoareCuTVA = row[14],
                                 CodStatie = row[20],
                                 LocatieStatie = row[21],
                                 Kilometri = row[24],
                                 NrFactura = row[25],
                                 ValoareFaraTVA2 = row[17],
                                 ValoareTVA2 = row[18],
                                 ValoareCuTVA2 = row[19],

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

                        CombustibilCard card = new CombustibilCard();

                        card.DataLivrare = CorrectAlexMolDataLivrare(CleanString(rowImport.DataLivrare.ToString()).Trim());
                        card.OraLivrare =  CorrectAlexMolOraLivrare(CleanString(rowImport.OraLivrare.ToString()).Trim());
                        card.DataFacturare = card.DataLivrare;
                        card.CodStatie = rowImport.CodStatie.ToString();
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.Produs = rowImport.Produs.ToString();
                        card.Cantitate = UtilsGeneral.ToDecimalInvariantCulture(rowImport.Cantitate.ToString(), 0.0M);
                        card.Valuta = rowImport.Valuta.ToString();
                        card.PretPerLitru = UtilsGeneral.ToDecimalInvariantCulture(rowImport.PretPerLitru.ToString(), 0.0M);
                        if (card.Valuta.ToLower().Trim().Contains("ron"))
                        {
                            card.ValoareCuTVA = UtilsGeneral.ToDecimalInvariantCulture(rowImport.ValoareCuTVA.ToString(), 0.0M);
                            card.ValoareTVA = UtilsGeneral.ToDecimalInvariantCulture(rowImport.ValoareTVA.ToString(), 0.0M);
                            card.ValoareFaraTVA = UtilsGeneral.ToDecimalInvariantCulture(rowImport.ValoareFaraTVA.ToString(), 0.0M);
                        }
                        else
                        {
                            card.ValoareCuTVA = UtilsGeneral.ToDecimalInvariantCulture(rowImport.ValoareCuTVA2.ToString(), 0.0M);
                            card.ValoareTVA = UtilsGeneral.ToDecimalInvariantCulture(rowImport.ValoareTVA2.ToString(), 0.0M);
                            card.ValoareFaraTVA = UtilsGeneral.ToDecimalInvariantCulture(rowImport.ValoareFaraTVA2.ToString(), 0.0M);
                        }
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = UtilsGeneral.ToDecimalInvariantCulture(rowImport.Kilometri.ToString(), 0.0M);
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Retea = "MOL";
                        card.FurnizorImportat = FurnizorImportat;
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        /*
        private void ExtractMolAlexFleet()
        {
            if (!ValidateImportBalantaEmisa(0))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {                                  
                                  DataLivrare = row[0],
                                  OraLivrare = row[1],
                                  NrAuto = row[2],
                                  CardPan = row[3],
                                  LocatieStatie = row[6],
                                  Produs = row[8],
                                  Cantitate = row[9],
                                  PretPerLitru = row[10],
                                  ValoareFaraTVA = row[12],
                                  Valuta = row[13],
                                  Kilometraj = row[14]
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

                        CombustibilCard card = new CombustibilCard();
                        card.Tara = "RO"; // implicit => nu exista in fisier ?!?!
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.DataFacturare = card.DataLivrare;
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.OraLivrare.ToString());
                        
                        card.LocatieStatie = rowImport.LocatieStatie.ToString();
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.Produs = rowImport.Produs.ToString();
                        card.Kilometraj = rowImport.Kilometraj.ToString();
                        card.Cantitate = UtilsGeneral.ToDecimalInvariantCulture(rowImport.Cantitate.ToString(), 0.0M);
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimalInvariantCulture(rowImport.ValoareFaraTVA.ToString(), 0.0M);
                        card.Valuta = rowImport.Valuta.ToString();
                        card.PretPerLitru = UtilsGeneral.ToDecimalInvariantCulture(rowImport.PretPerLitru.ToString(), 0.0M);                       
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Retea = "MOL";
                        card.FurnizorImportat = FurnizorImportat;
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);

                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        /*
        private void ExtractSmartDieselDianthus()
        {
            if (!ValidateImportBalantaEmisa(0))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                //var results = from row in MasterTable.AsEnumerable().Skip(18)
                //              where !row[0].ToString().Contains("SUBTOTAL")                              
                //              select new
                //              {
                //                  AllLinesInOne = row[0].ToString() + " = " + row[1].ToString() + " = " +
                //                                  row[2].ToString() + " = " + row[2].ToString() + " = " +
                //                                  row[3].ToString() + " = " + row[4].ToString() + " = " +
                //                                  row[5].ToString() + " = " + row[6].ToString() + " = " +
                //                                  row[7].ToString() + " = " + row[8].ToString() + " = " + 
                //                                  row[9].ToString() + " = " + row[10].ToString() 
                //              };

                var results = from row in MasterTable.AsEnumerable()
                              where !row[0].ToString().ToUpper().Contains("TOTAL")
                              select new
                              {
                                  AllLinesInOne = row[0].ToString() + " = " + row[1].ToString() + " = " +
                                                  row[2].ToString() + " = " + row[2].ToString() + " = " +
                                                  row[3].ToString() + " = " + row[4].ToString() + " = " +
                                                  row[5].ToString() + " = " + row[6].ToString() + " = " +
                                                  row[7].ToString() + " = " + row[8].ToString() + " = " +
                                                  row[9].ToString() + " = " + row[12].ToString()+ " = " +
                                                  row[13].ToString() + " = " + row[16].ToString() + " = " + row[17].ToString()
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

                        CombustibilCard card = new CombustibilCard();
                        var strRow = rowImport.AllLinesInOne.Split('=');


                        card.DataLivrare = UtilsGeneral.ToDateTime(strRow[0]);
                        card.OraLivrare = card.DataLivrare;
                        card.DataFacturare = card.DataLivrare;
                        card.Tara = CleanStringDianthusSmartDiesel(strRow[2]);
                        if (card.Tara.ToLower().Contains("roma"))
                        {
                            card.Valuta = "RON"; // pretul egal = 1.138 = 14                            
                            card.PretPerLitru = UtilsGeneral.ToDecimal(CleanStringMaster(strRow[11]), 0.0M);
                            card.ValoareFaraTVA = UtilsGeneral.ToDecimal(CleanStringMaster(strRow[12]), 0.0M);
                        }
                        else
                        {
                            card.Valuta = "EUR";
                            card.PretPerLitru = UtilsGeneral.ToDecimal(CleanStringMaster(strRow[13].ToString()), 0.0M);
                            card.ValoareFaraTVA = UtilsGeneral.ToDecimal(CleanStringMaster(strRow[14].ToString()), 0.0M);
                        }

                        //card.Valuta = "RON"; // pretul egal = 1.138 = 14                            
                        //card.PretPerLitru = UtilsGeneral.ToDecimal(CleanStringMaster(strRow[11]), 0.0M);
                        //card.ValoareFaraTVA = UtilsGeneral.ToDecimal(CleanStringMaster(strRow[12]), 0.0M);

                        card.LocatieStatie = CleanStringDianthusSmartDiesel(strRow[4]);                        
                        card.CardPan = CleanStringDianthusSmartDiesel(strRow[6]);
                        card.NrAuto = CleanStringMaster(strRow[7]);
                        card.Kilometri = UtilsGeneral.ToDecimal(CleanStringDianthusSmartDiesel(strRow[8]), 0.0M);
                        card.Produs = CleanStringMaster(strRow[9]);
                        card.Cantitate = UtilsGeneral.ToDecimal(CleanStringDianthusSmartDiesel(strRow[10]), 0.0M);                        
                        card.Retea = "SMARTDIESEL";
                        card.FurnizorImportat = FurnizorImportat;                        
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        /*
        private void ExtractSmartDieselDianthus2()
        {
            if (!ValidateImportBalantaEmisa(2))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  NrAuto = row[0],
                                  CardPan = row[1],
                                  DataLivrare = row[2],
                                  OraLivrare = row[3],
                                  Produs = row[6],
                                  Cantitate = row[9],
                                  PretPerLitru = row[10],
                                  Valuta = row[11],
                                  ValoareFaraTVA = row[12],
                                  ValoareTVA = row[13],
                                  ValoareCuTVA = row[14],
                                  CodStatie = row[20],
                                  LocatieStatie = row[21],
                                  Kilometri = row[24],
                                  NrFactura = row[25]
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

                        CombustibilCard card = new CombustibilCard();

                        card.DataLivrare = CorrectAlexMolDataLivrare(CleanString(rowImport.DataLivrare.ToString()).Trim());
                        card.OraLivrare = CorrectAlexMolOraLivrare(CleanString(rowImport.OraLivrare.ToString()).Trim());
                        card.DataFacturare = card.DataLivrare;
                        card.CodStatie = rowImport.CodStatie.ToString();
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.Produs = rowImport.Produs.ToString();
                        card.Cantitate = UtilsGeneral.ToDecimal(CleanString(rowImport.Cantitate.ToString()), 0.0M);
                        card.Valuta = rowImport.Valuta.ToString();
                        card.PretPerLitru = UtilsGeneral.ToDecimal(CleanString(rowImport.PretPerLitru.ToString()), 0.0M);
                        card.ValoareCuTVA = UtilsGeneral.ToDecimal(CleanString(rowImport.ValoareCuTVA.ToString()), 0.0M);
                        card.ValoareTVA = UtilsGeneral.ToDecimal(rowImport.ValoareTVA, 0.0M);
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(CleanString(rowImport.ValoareFaraTVA.ToString()), 0.0M);
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = UtilsGeneral.ToDecimal(CleanString(rowImport.Kilometri.ToString()), 0.0M);
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Retea = "MOL";
                        card.FurnizorImportat = FurnizorImportat;
                        if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                        else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        /*
        private void ExtractSmartDieselDunca()
        {
            if (!ValidateImportBalantaEmisa(0))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  Data = row[0],
                                  LocatieStatie = row[1],
                                  CardPan = row[2],
                                  NrAuto = row[3],
                                  Kilometri = row[4],
                                  Produs = row[5],
                                  Cantitate = row[6],
                                  PretPerLitru = row[9],
                                  ValoareCuTVA = row[10],
                                  ValoareTVA = row[11],
                                  ValoareFaraTVA = row[12],
                                  Tara = row[13]
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

                        CombustibilCard card = new CombustibilCard();
                        card.Tara = rowImport.Tara.ToString();
                        card.Valuta = "RON";
                        card.DataFacturare = UtilsGeneral.ToDateTime(rowImport.Data.ToString());
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.Data.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.Data.ToString());
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.Produs = rowImport.Produs.ToString();
                        card.Cantitate = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString()), 0.0M);

                        card.PretPerLitru = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.PretPerLitru.ToString()), 0.0M);

                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0.0M);
                        card.ValoareTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString()), 0.0M);
                        card.ValoareCuTVA = card.ValoareFaraTVA + card.ValoareTVA;
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString()), 0.0M);
                        
                        card.Retea = "SMARTDIESEL";
                        card.FurnizorImportat = FurnizorImportat;
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);

                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        private void ExtractSmartDieselF4F()
        {
            if (!ValidateImportBalantaEmisa(1))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                DataSet dataSet = CreateDataSetCarduri();

                try
                {
                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        if (dataRow[1] == DBNull.Value) // se sare peste liniile de subtotaluri
                            continue;

                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        card["Tara"] = dataRow[3];
                        DateTime dataOraLivrare = (UtilsGeneral.ToDateTime(dataRow[1]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow[1]));
                        card["DataLivrare"] = dataOraLivrare;
                        card["OraLivrare"] = dataOraLivrare;
                        card["LocatieStatie"] = dataRow[4].ToString();
                        card["NrAuto"] = NrAutoFind(dataRow[7].ToString().Replace(" ", ""), false, dataOraLivrare);
                        card["Produs"] = dataRow[9];
                        card["Valuta"] = dataRow[5];
                        card["Cantitate"] = ConvertToDecimalIvariantCulture(dataRow[10].ToString());
                        decimal valoareCuTVA = ConvertToDecimalIvariantCulture(dataRow[14].ToString());
                        card["ValoareCuTVA"] = valoareCuTVA;
                        decimal valoareFaraTVA;
                        if (card["Valuta"].ToString().Trim().ToUpper() == "RON")
                        { 
                            valoareFaraTVA = Math.Round(valoareCuTVA - (valoareCuTVA * cotaTVARonDefault / (100 + cotaTVARonDefault)), 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            valoareFaraTVA = valoareCuTVA;
                        }
                        card["ValoareFaraTVA"] = valoareFaraTVA;
                        card["ValoareTVA"] = valoareCuTVA - valoareFaraTVA;
                        card["PretPerLitru"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) / (UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M) == 0.0M ? 1 : UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        
                        card["CardPan"] = dataRow[6].ToString().Replace("'", string.Empty);
                        card["Kilometraj"] = ConvertToDecimalIvariantCulture(dataRow[8].ToString());
                        card["Retea"] = "SMARTDIESEL";
                        card["FurnizorImportat"] = FurnizorImportat;
                        card["NrAuto_ID"] = ((object)NrAutoIDFindAndMatchFromCardPan(CleanString(card["NrAuto"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) ?? DBNull.Value);
                        if (UtilsGeneral.ToInteger(card["NrAuto_ID"], 0) == 0)
                            card["NrAuto_ID"] = ((object)NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataOraLivrare) ?? DBNull.Value);
                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(CleanString(card["Produs"].ToString()), string.Empty, FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private void ExtractDaarsDunca()
        {
            if (!ValidateImportBalantaEmisa(1))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  CardPan = row[0],                                  
                                  DataLivrare = row[1], 
                                  OraLivrare = row[2],
                                  IntrareLocatie = row[3],
                                  IesireLocatie = row[4],
                                  ValoareFaraTVA = row[5],
                                  ValoareTVA = row[6],
                                  ValoareCuTVA = row[7],
                                  Tara = row[10]                                
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

                        CombustibilCard card = new CombustibilCard();

                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = card.DataLivrare;
                        card.DataFacturare = card.DataLivrare;
                        card.Tara = rowImport.Tara.ToString();

                        card.IntrareLocatie = CleanString(rowImport.IntrareLocatie.ToString());
                        card.IesireLocatie = CleanString(rowImport.IesireLocatie.ToString());

                        card.ValoareTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString()), 0.0M);
                        card.ValoareCuTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString()), 0.0M);
                        //card.ValoareTVA = UtilsGeneral.ToDecimal(rowImport.ValoareTVA, 0.0M);
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0.0M);
                        card.Produs = "Taxa drum";
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Valuta = "EUR";
                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value);

                        card.ProdusID = ProdusIDFindAndMatch(CleanString(card.Produs), FurnizorImportat);
                        card.Retea = "DAARS";
                        card.FurnizorImportat = FurnizorImportat;
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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

        private void ExtractToolColectDuncaFinanciar()
        {
            if (!ValidateImportBalantaEmisa(2))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                DataSet dataSet = CreateDataSetCarduri();

                try
                {
                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        card["NrFactura"] = NrFacturaAnexa;
                        card["Tara"] = "GERMANIA";
                        DateTime dataLivrare = (UtilsGeneral.ToDateTime(dataRow[2]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow[2]));
                        DateTime oraLivrare = (UtilsGeneral.ToDateTime(dataRow[3]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow[3]));
                        card["DataLivrare"] = dataLivrare;
                        card["OraLivrare"] = oraLivrare;
                        card["DataFacturare"] = (DataFacturaAnexa == DateTime.MinValue ? new DateTime(1900, 1, 1) : DataFacturaAnexa);
                        card["LocatieStatie"] = dataRow[7].ToString();
                        card["IntrareLocatie"] = dataRow[8].ToString();
                        card["IesireLocatie"] = dataRow[9].ToString();
                        card["NrAuto"] = NrAutoFind(dataRow[1].ToString(), false, dataLivrare.Date + oraLivrare.TimeOfDay);
                        card["Produs"] = dataRow[5].ToString();
                        card["Cantitate"] = 1;
                        card["Valuta"] = "EUR";
                        card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[20].ToString()), 0.0M);
                        card["ValoareTVA"] = 0.0M;
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M);
                        card["VATAmount"] = UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        card["CardPan"] = dataRow[4].ToString().Replace("'", string.Empty);
                        card["Kilometraj"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[19].ToString()), 0.0M);
                        card["Retea"] = "Toll Collect DE";
                        card["FurnizorImportat"] = FurnizorImportat;
                        //card["NrAuto_ID"] = (NrAutoIDFindAndMatchFromCardPan(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataLivrare.Date + oraLivrare.TimeOfDay));
                        int nrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        card["NrAuto_ID"] = nrAutoID == 0 ? NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataLivrare.Date + oraLivrare.TimeOfDay) : nrAutoID;
                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(CleanString(card["Produs"].ToString()), string.Empty, FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                        parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                        parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private void ExtractToolColectDunca()
        {
            if (!ValidateImportBalantaEmisa(2))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  NrAuto = row[1],
                                  DataLivrare = row[2],
                                  OraLivrare = row[3],
                                  CardPan = row[4],
                                  Produs = row[5],
                                  IntrareLocatie = row[7],
                                  CodRuta = row[8],
                                  IesireLocatie = row[9],                                  
                                  ValoareCuTVA = row[20],                                  
                                  ValoareTVA = row[21],
                                  ValoareFaraTVA = row[22],
                                  Tara = row[23]
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

                        CombustibilCard card = new CombustibilCard();
                        card.Tara = rowImport.Tara.ToString();
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.OraLivrare.ToString());
                        card.DataFacturare = card.DataLivrare;
                        card.CardPan = rowImport.CardPan.ToString();                        
                        card.IntrareLocatie = CleanString(rowImport.IntrareLocatie.ToString());
                        card.IesireLocatie = CleanString(rowImport.IesireLocatie.ToString());
                        card.Produs = CleanString(rowImport.Produs.ToString());
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0.0M);
                        card.ValoareTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString()), 0.0M);
                        card.ValoareCuTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString()), 0.0M);
                        card.Valuta = "EUR";
                        card.Retea = "Tool Colect";
                        card.FurnizorImportat = FurnizorImportat;
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);

                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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

        private void ExtractToolColectATVectra()
        {
            if (!ValidateImportBalantaEmisa(3))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  CardPan = row[1],
                                  NrAuto = row[2],
                                  DataLivrare = row[3],
                                  //OraLivrare = row[2],
                                  ValoareFaraTVA = row[5],
                                  //Produs = row[4]

                                  
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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = "AT";
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.DataFacturare = card.DataLivrare;
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0.0M);
                        card.Valuta = "EUR";
                        card.Retea = "Toll Collect AT";
                        card.FurnizorImportat = FurnizorImportat;
                        //card.Produs = "Taxa autostrada";
                        //card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ProdusID = 26;
                        card.CardPan = CleanString(rowImport.CardPan.ToString());
                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(card.CardPan, (DateTime?)card.DataLivrare, (DateTime?)card.DataLivrare);
                        card.Cantitate = 1;
                        if(null == card.NrAutoID || 0 == card.NrAutoID.Value)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date);                        
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        /*
        private void ExtractToolColectDEVectra()
        {
            if (!ValidateImportBalantaEmisa(1))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  NrAuto = row[0],
                                  DataLivrare = row[1],
                                  OraLivrare = row[2],
                                  ValoareFaraTVA = row[3],
                                  //Produs = row[4]


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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = "DE";
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.OraLivrare.ToString());
                        card.DataFacturare = card.DataLivrare;
                        card.ValoareFaraTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString()), 0.0M);
                        card.Valuta = "EUR";
                        card.Retea = "Toll Collect DE";
                        card.FurnizorImportat = FurnizorImportat;
                        //card.Produs = "Taxa autostrada";
                        //card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ProdusID = 26;
                        card.Cantitate = 1;
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        /*
        private void ExtractToolColectDianthus() 
        {
            if (!ValidateImportBalantaEmisa(2))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  NrAuto = row[1],
                                  DataLivrare = row[2],
                                  OraLivrare = row[3],
                                  CardPan = row[4],
                                  Produs = row[5],
                                  IntrareLocatie = row[7],
                                  CodRuta = row[8],
                                  IesireLocatie = row[9],
                                  ValoareCuTVA = row[20]
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

                        CombustibilCard card = new CombustibilCard();
                        card.Tara = "Germania";
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.OraLivrare.ToString());
                        card.DataFacturare = card.DataLivrare;
                        card.CardPan = rowImport.CardPan.ToString();
                        card.IntrareLocatie = CleanString(rowImport.IntrareLocatie.ToString());
                        card.IesireLocatie = CleanString(rowImport.IesireLocatie.ToString());
                        if (Constants.client == Constants.Clients.Dianthus)
                            card.Produs = "ROAD TAX";
                        else
                            card.Produs = rowImport.Produs.ToString();
                        card.ValoareFaraTVA = card.ValoareCuTVA = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString()), 0.0M);
                        card.Valuta = "EUR";
                        card.Retea = "Tool Colect";
                        card.FurnizorImportat = FurnizorImportat;
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);

                        card.NrAutoID = NrAutoIDFindAndMatchFromCardPan(CleanString(card.CardPan), card.DataLivrare, card.OraLivrare);
                        if (card.NrAutoID == null || card.NrAutoID == 0)
                            card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);

                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        private void ExtractIDSVectra()
        {
            if (!ValidateImportBalantaEmisa(4))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  CardPan = row[2],
                                  Tara = row[3],
                                  LocatieStatie = row[4],
                                  DataLivrare = row[5],
                                  OraLivrare = row[6],
                                  Produs = row[7],
                                  Cantitate = row[8],
                                  ValoareFaraTVA = row[9],
                                  //PretPerLitru = row[],
                                  NrFactura = row[23],
                                  Kilometri = row[15],
                                  NrAuto = row[20]
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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = rowImport.Tara.ToString();
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        //card.OraLivrare = UtilsGeneral.ToDateTime(CorrectVectraIDSOraLivrare(rowImport.OraLivrare.ToString()));
                        card.OraLivrare = UtilsGeneral.ToDateTime(rowImport.OraLivrare.ToString());
                        card.DataFacturare = card.DataLivrare;
                        card.LocatieStatie = CleanString(rowImport.LocatieStatie.ToString());
                        card.CardPan = rowImport.CardPan.ToString();                      
                        card.Kilometri = ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString());
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Produs = CleanString(rowImport.Produs.ToString());
                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                        //card.PretPerLitru = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                        card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                        card.Valuta = "EUR";
                        card.Retea = "IDS";
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);
                        card.FurnizorImportat = FurnizorImportat;
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        /*
        private void ExtractOMVVectra()
        {
            if (!ValidateImportBalantaEmisa(4))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  CardPan = row[2],
                                  NrAuto = row[3],
                                  DataLivrare = row[4],
                                  Produs = row[5],
                                  Cantitate = row[6],
                                  ValoareCuTVA = row[7],
                                  ValoareTVA = row[8],                                  
                                  Kilometri = row[9],
                                  Tara = row[13],
                                  IntrareLocatie = row[14],
                                  PretPerLitru  = row[16],
                                  Valuta = row[21],
                                  NrFactura = row[22],
                                  DataFactura = row[23],
                                  Retea = row[27]
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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = CleanString(rowImport.Tara.ToString());
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = card.DataLivrare;
                        card.DataFacturare = UtilsGeneral.ToDateTime(rowImport.DataFactura.ToString());
                        if (card.DataFacturare == DateTime.MinValue) card.DataFacturare = card.DataLivrare;
                        card.IntrareLocatie = rowImport.IntrareLocatie.ToString();
                        card.PretPerLitru = ConvertToDecimalIvariantCulture(rowImport.PretPerLitru.ToString());
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString());
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Produs = CleanString(rowImport.Produs.ToString());
                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                        card.ValoareTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString());
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                        card.ValoareFaraTVA = card.ValoareCuTVA - card.ValoareTVA;
                        card.Valuta = CleanString(rowImport.Valuta.ToString());
                        card.Retea = CleanString(rowImport.Retea.ToString());
                        card.FurnizorImportat = FurnizorImportat;
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        /*
        private void ExtractOMVEliton()
        {
            if (!ValidateImportBalantaEmisa(4))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  CardPan = row[2],
                                  NrAuto = row[3],
                                  DataLivrare = row[4],
                                  Produs = row[5],
                                  Cantitate = row[6],
                                  ValoareCuTVA = row[7],
                                  ValoareTVA = row[8],
                                  Kilometri = row[9],
                                  Tara = row[13],
                                  IntrareLocatie = row[14],
                                  PretPerLitru = row[16],
                                  Valuta = row[21],
                                  NrFactura = row[22],
                                  DataFactura = row[23],
                                  Retea = row[27]
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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = CleanString(rowImport.Tara.ToString());
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = card.DataLivrare;
                        card.DataFacturare = UtilsGeneral.ToDateTime(rowImport.DataFactura.ToString());
                        if (card.DataFacturare == DateTime.MinValue) card.DataFacturare = card.DataLivrare;
                        card.IntrareLocatie = rowImport.IntrareLocatie.ToString();
                        card.PretPerLitru = ConvertToDecimalIvariantCulture(rowImport.PretPerLitru.ToString());
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString());
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Produs = CleanString(rowImport.Produs.ToString());
                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                        card.ValoareTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString());
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                        card.ValoareCuTVA = card.ValoareCuTVA - card.ValoareTVA;
                        card.Valuta = CleanString(rowImport.Valuta.ToString());
                        card.Retea = CleanString(rowImport.Retea.ToString());
                        card.FurnizorImportat = FurnizorImportat;
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        /*
        private void ExtractOMVViotrans()
        {
            if (!ValidateImportBalantaEmisa(4))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  CardPan = row[2],
                                  NrAuto = row[3],
                                  DataLivrare = row[4],
                                  Produs = row[5],
                                  Cantitate = row[6],
                                  ValoareCuTVA = row[7],
                                  ValoareTVA = row[8],
                                  Kilometri = row[10],
                                  Tara = row[13],
                                  IntrareLocatie = row[14],
                                  PretPerLitru = row[16],
                                  Valuta = row[21],
                                  NrFactura = row[9],// nr bon
                                  DataFactura = row[4],
                                  Retea = row[27]
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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = CleanString(rowImport.Tara.ToString());
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = card.DataLivrare;
                        card.DataFacturare = UtilsGeneral.ToDateTime(rowImport.DataFactura.ToString());
                        if (card.DataFacturare == DateTime.MinValue) card.DataFacturare = card.DataLivrare;
                        card.IntrareLocatie = rowImport.IntrareLocatie.ToString();
                        card.PretPerLitru = ConvertToDecimalIvariantCulture(rowImport.PretPerLitru.ToString());
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString());
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Produs = CleanString(rowImport.Produs.ToString());
                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString());
                        card.ValoareTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString());
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                        card.ValoareFaraTVA = card.ValoareCuTVA - card.ValoareTVA;
                        card.Valuta = CleanString(rowImport.Valuta.ToString());
                        card.Retea = CleanString(rowImport.Retea.ToString());
                        card.FurnizorImportat = FurnizorImportat;
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        /*
        private void ExtractOMVRomVega()
        {
            if (!ValidateImportBalantaEmisa(4))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              select new
                              {
                                  CardPan = row[2],
                                  NrAuto = row[3],
                                  DataLivrare = row[4],
                                  Produs = row[5],
                                  Cantitate = row[6],
                                  ValoareCuTVA = row[17],
                                  ValoareTVA = row[20],
                                  Kilometri = row[10],
                                  Tara = row[13],
                                  IntrareLocatie = row[14],
                                  PretPerLitru = row[16],
                                  Valuta = row[21],
                                  NrFactura = row[9],// nr bon
                                  DataFactura = row[4],
                                  Retea = row[27]
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

                        CombustibilCard card = new CombustibilCard();

                        card.Tara = CleanString(rowImport.Tara.ToString());
                        card.NrAuto = CleanString(rowImport.NrAuto.ToString());
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = card.DataLivrare;
                        card.DataFacturare = UtilsGeneral.ToDateTime(rowImport.DataFactura.ToString());
                        if (card.DataFacturare == DateTime.MinValue) card.DataFacturare = card.DataLivrare;
                        card.IntrareLocatie = rowImport.IntrareLocatie.ToString();
                        card.PretPerLitru = ConvertToDecimalIvariantCulture(rowImport.PretPerLitru.ToString().Replace(".", ""));
                        //card.PretPerLitru = UtilsGeneral.ToDecimal(rowImport.PretPerLitru, 0.0M);
                        card.CardPan = rowImport.CardPan.ToString();
                        card.Kilometri = ConvertToDecimalIvariantCulture(rowImport.Kilometri.ToString());
                        card.NrFactura = rowImport.NrFactura.ToString();
                        card.Produs = CleanString(rowImport.Produs.ToString());
                        //card.Cantitate = UtilsGeneral.ToDecimal(rowImport.Cantitate, 0.0M);
                        card.Cantitate = ConvertToDecimalIvariantCulture(rowImport.Cantitate.ToString().Replace(".", ""));
                        card.ValoareTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString().Replace(".", ""));
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString().Replace(".", ""));
                        card.ValoareFaraTVA = card.ValoareCuTVA - card.ValoareTVA;
                        card.Valuta = CleanString(rowImport.Valuta.ToString());
                        card.Retea = CleanString(rowImport.Retea.ToString());
                        card.FurnizorImportat = FurnizorImportat;
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto);
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            if (IsFromFinanciarOpened) card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
                            else card.ExecuteSave(card, DataOraImport, UserImport);
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
        */
        private void ExtractShellAnexaFactura()
        {
            if (!ValidateImportBalantaEmisa(1))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                var results = from row in MasterTable.AsEnumerable()
                              where ((row[0].ToString() != string.Empty || row[0].ToString().Length != 0) && !row[0].ToString().Trim().ToLower().Contains("total")) // fara ultima linie
                              select new
                              {
                                  
                                  DataFactura = row[0],
                                  DataLivrare = row[1],
                                  OraLivrare = row[2],
                                  ValoareFaraTVA = row[3],
                                  ValoareTVA = row[4],
                                  VATAmount = row[4],
                                  ValoareCuTVA = row[5],
                                  NrAuto = row[6],                                 
                                  CardPan = row[7],
                                  ProdusID = row[8],
                                  IntrareLocatie = row[9],
                                  IesireLocatie = row[10],
                                  Ruta = row[11]                                                                    
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

                        CombustibilCard card = new CombustibilCard();

                        //card.NrFactura = NrFacturaAnexa;
                        //card.Valuta = ValutaAnexaFactura;
                        //card.FacturaPrimita_ID = IDAnexaFactura; // ATASAM ID-UL DE FACTURA
                        card.DataLivrare = UtilsGeneral.ToDateTime(rowImport.DataLivrare.ToString());
                        card.OraLivrare = UtilsGeneral.ToDateTime((rowImport.OraLivrare.ToString()));
                        card.DataFacturare = UtilsGeneral.ToDateTime((rowImport.DataFactura.ToString()));                        
                        card.NrAuto = CleanStringMaster(rowImport.NrAuto.ToString());                        
                        card.ValoareCuTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareCuTVA.ToString());
                        card.ValoareTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareTVA.ToString());
                        card.ValoareFaraTVA = ConvertToDecimalIvariantCulture(rowImport.ValoareFaraTVA.ToString());
                        card.VATAmount = ConvertToDecimalIvariantCulture(rowImport.VATAmount.ToString());
                        card.CodRuta = rowImport.Ruta.ToString();
                        card.CardPan = CleanString(rowImport.CardPan.ToString());
                        card.Produs = CleanString(rowImport.ProdusID.ToString());
                        card.Retea = "SHELL";
                        card.FurnizorImportat = FurnizorImportat;
                        card.NrAutoID = NrAutoIDFindAndMatch(card.NrAuto, card.DataLivrare.Value.Date + card.OraLivrare.Value.TimeOfDay);
                        card.ProdusID = ProdusIDFindAndMatch(card.Produs, FurnizorImportat);// ProdusIDFindAndMatch(card.Produs, FurnizorImportat);
                        card.ComputeHashCode(IsFromFinanciarOpened);
                        if (NoOfRowsFromDB == 0)
                        {
                            card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);                            
                            ImportRealizat = true;
                        }
                        else if (CardNotFound(card))
                        {
                            card.ExecuteSaveFinanciar(card, DataOraImport, UserImport);
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
        
        private void ExtractTSVMarvi()
        {
            if (!ValidateImportBalantaEmisa("DeliveryDate"))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add("Carduri");
            dataSet.Tables["Carduri"].Columns.Add("Tara", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("DataLivrare", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("NrAuto", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("NrFactura", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Valuta", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Produs", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("ValoareCuTVA", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("CardPan", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Retea", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("FurnizorImportat", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("NrAuto_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("Produs_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("HashCode", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("DataImport", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("OraImport", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("UserImport", typeof(String));

            try
            {
                int NoOfRowsFromDB = 0;

                foreach (DataRow dataRow in MasterTable.Rows)
                {
                    DataRow card = dataSet.Tables["Carduri"].NewRow();
                    card["Tara"] = dataRow["CountryName"].ToString();
                    DateTime dataLivrare;
                    card["DataLivrare"] = dataLivrare = (UtilsGeneral.ToDateTime(dataRow["DeliveryDate"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["DeliveryDate"]));
                    card["NrAuto"] = CleanStringMaster(dataRow["Vehicle"].ToString());
                    card["Produs"] = dataRow["ProductName"].ToString();
                    card["Valuta"] = "RON";
                    card["NrFactura"] = dataRow["InvNumber"].ToString();
                    card["CardPan"] = dataRow["Card"].ToString();
                    card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(dataRow["val cu tva Ron"], 0.0M);
                    card["Retea"] = "TSV";
                    card["FurnizorImportat"] = FurnizorImportat;
                    card["NrAuto_ID"] = (NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataLivrare) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataLivrare));
                    card["Produs_ID"] = (ProdusIDFindAndMatch(card["Produs"].ToString(), FurnizorImportat) == null ? 0 : ProdusIDFindAndMatch(card["Produs"].ToString(), FurnizorImportat));
                    Hashtable parametersForComputeHashCode = new Hashtable();
                    parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                    parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                    parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                    parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                    parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                    parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFactura"]));
                    parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                    parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                    card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                    card["DataImport"] = UtilsGeneral.ToDateTime(DataOraImport);
                    card["OraImport"] = UtilsGeneral.ToDateTime(DataOraImport);
                    card["UserImport"] = UserImport;         
                    dataSet.Tables["Carduri"].Rows.Add(card);

                    NoOfRowsFromDB++;
                }

                if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    ImportMessage = Error;
                    return;
                }

                if (NoOfRowsFromDB == 0)
                {
                    Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                    ImportRealizat = false;
                    ImportMessage = Error;
                    return;
                }
            }
            catch(Exception ex)
            {
                Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                ImportRealizat = false;
                ImportMessage = ex.Message;
                return;
            }

            SaveImports(dataSet.Tables["Carduri"]);
        }
        
        /*
        private void ExtractDKVRomVega()
        {
            if (!ValidateImportBalantaEmisa("Date"))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add("Carduri");
            dataSet.Tables["Carduri"].Columns.Add("Tara", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("DataLivrare", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("OraLivrare", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("DataFacturare", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("NrAuto", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("NrFactura", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Valuta", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Produs", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Cantitate", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("ValoareFaraTVA", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("ValoareTVA", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("ValoareCuTVA", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("PretPerLitru", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("CardPan", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Retea", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("CodStatie", typeof(String));//
            dataSet.Tables["Carduri"].Columns.Add("LocatieStatie", typeof(String));//
            dataSet.Tables["Carduri"].Columns.Add("Observatii", typeof(String));//
            dataSet.Tables["Carduri"].Columns.Add("FurnizorImportat", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("NrAuto_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("Produs_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("HashCode", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("DataImport", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("OraImport", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("UserImport", typeof(String));

            try
            {
                int NoOfRowsFromDB = 0;

                foreach (DataRow dataRow in MasterTable.Rows)
                {
                    DataRow card = dataSet.Tables["Carduri"].NewRow();
                    card["Tara"] = dataRow["Service country"].ToString();
                    card["DataLivrare"] = CorrectRomVegaData(UtilsGeneral.ToString(dataRow["Date"]));
                    card["OraLivrare"] = (UtilsGeneral.ToDateTime(dataRow["Date"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["Date"]));
                    card["DataFacturare"] = CorrectRomVegaData(UtilsGeneral.ToString(dataRow["Date"]));
                    card["NrAuto"] = CleanStringMaster(dataRow["Vehicle registration number"].ToString());
                    card["Produs"] = dataRow["Product"].ToString();
                    card["Valuta"] = "EUR";
                    card["NrFactura"] = dataRow["Customer number"].ToString();
                    card["CardPan"] = dataRow["Number of card or box"].ToString();
                    card["Cantitate"] = UtilsGeneral.ToDecimal(dataRow["Sales"], 0.0M);
                    card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(dataRow["Value of purchases net"], 0.0M);
                    card["ValoareTVA"] = UtilsGeneral.ToDecimal(dataRow["VAT"], 0.0M);
                    card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(dataRow["Value of purchases gross"], 0.0M);
                    card["PretPerLitru"] = UtilsGeneral.ToDecimal(dataRow["Price per unit"], 0.0M);
                    card["Retea"] = "DKV";
                    card["CodStatie"] = dataRow["Postcode"].ToString();
                    card["LocatieStatie"] = dataRow["Town"].ToString();
                    card["Observatii"] = dataRow["Product group"].ToString() + " / " + dataRow["Brand"].ToString();
                    card["FurnizorImportat"] = FurnizorImportat;
                    card["NrAuto_ID"] = (NrAutoIDFindAndMatch(card["NrAuto"].ToString()) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString()));
                    card["Produs_ID"] = (ProdusIDFindAndMatch(card["Produs"].ToString(), FurnizorImportat) == null ? 0 : ProdusIDFindAndMatch(card["Produs"].ToString(), FurnizorImportat));
                    Hashtable parametersForComputeHashCode = new Hashtable();
                    parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                    parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                    parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                    parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                    parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                    parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFactura"]));
                    parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                    parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                    card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                    card["DataImport"] = UtilsGeneral.ToDateTime(DataOraImport).ToString("MM-dd-yyyy").ToString(CultureInfo.InvariantCulture);
                    card["OraImport"] = UtilsGeneral.ToDateTime(DataOraImport).ToString("H:mm:ss").ToString(CultureInfo.InvariantCulture);
                    card["UserImport"] = UserImport;
                    dataSet.Tables["Carduri"].Rows.Add(card);

                    NoOfRowsFromDB++;
                }

                if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    ImportMessage = Error;
                    return;
                }

                if (NoOfRowsFromDB == 0)
                {
                    Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                    ImportRealizat = false;
                    ImportMessage = Error;
                    return;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                ImportRealizat = false;
                ImportMessage = ex.Message;
                return;
            }

            SaveImports(dataSet.Tables["Carduri"]);
        }
        */
   
        private void ExtractDKVCarduri()
        {
            string tip = null;
            try
            {
                tip = WindDatabase.ExecuteScalar(string.Format("SELECT dbo.fn_GetTipImportCombustibil({0},{1})", (int)TipFurnizorImportat.DKV, 0));
            }
            catch { } //daca nu s-a facut sincronizare si nu s-a adaugat functia fn_GetTipImportCombustibil se va face importul implicit

            if (tip == "1")
            {
                ExtractDKVCarduriTip1();
            }
            else if(tip == "2")
            {
                ExtractDKVCarduriTip2();
            }
            else
            {
                ExtractDKVCarduriDefault();
            }
        }

        private void ExtractDKVCarduriTip2()
        {
            DateTime excludeStationCodeFrom;
            try
            {
                Query textQuery = new Query("SELECT String FROM Parametri WHERE Parametru_ID = 116");
                excludeStationCodeFrom = UtilsGeneral.ToDateTime(WindDatabase.ExecuteScalar(textQuery));
            }
            catch
            {
                excludeStationCodeFrom = DateTime.MinValue;
            }

            try
            {
                DataSet dataSet = CreateDataSetCarduri();
                try
                {
                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        if (dataRow[6].ToString() == string.Empty)
                            continue;
                        DataRow card = dataSet.Tables["Carduri"].NewRow();

                        card["NrAuto"] = dataRow[1].ToString();
                        card["CardPan"] = dataRow[2].ToString().Replace("'", string.Empty);
                        DateTime dataLivrare = CorrectDKV(dataRow[3].ToString(), false);
                        DateTime oraLivrare = CorrectDKV(dataRow[3].ToString(), true);
                        card["DataLivrare"] = dataLivrare;
                        card["OraLivrare"] = oraLivrare;
                        card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[7].ToString()), 0.0M);
                        card["Produs"] = dataRow[12].ToString();
                        card["Tara"] = GetCountryName(CorrectTaraDKV(dataRow[16].ToString()));
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[9].ToString()), 0.0M);
                        card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[9].ToString()), 0.0M);
                        //card["PretPerLitru"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[16].ToString()), 0.0M); -> este pretul cu TVA
                        card["PretPerLitru"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) / (UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M) == 0.0M ? 1 : UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        card["VATAmount"] = UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        card["LocatieStatie"] = dataRow[24].ToString();
                        card["CodStatie"] = dataRow[25].ToString();
                        card["Retea"] = "DKV";
                        card["Kilometri"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[29].ToString()), 0.0M);
                        card["Valuta"] = "EUR";
                        card["FurnizorImportat"] = FurnizorImportat;

                        card["Card_ID"] = CardIDFindAndMatch(card["CardPan"].ToString(), UtilsGeneral.ToDateTime(card["DataLivrare"]).Date, UtilsGeneral.ToDateTime(card["OraLivrare"]));

                        int nrAuto_ID = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true, dataLivrare.Date + oraLivrare.TimeOfDay), 0);
                        card["NrAuto_ID"] = nrAuto_ID;
                        if (nrAuto_ID == 0)
                        {
                            card["NrAutoTerti_ID"] = NrAutoTertiFind(UtilsGeneral.ToInteger(card["Card_ID"], 0), dataLivrare.Date + oraLivrare.TimeOfDay);
                        }
                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(card["Produs"].ToString(), CleanString(dataRow[12].ToString()), FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));

                        if (excludeStationCodeFrom == DateTime.MinValue || excludeStationCodeFrom > dataLivrare)
                            parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());

                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private void ExtractDKVCarduriDefault()
        {
            DateTime excludeStationCodeFrom;
            try
            {
                Query textQuery = new Query("SELECT String FROM Parametri WHERE Parametru_ID = 116");
                excludeStationCodeFrom = UtilsGeneral.ToDateTime(WindDatabase.ExecuteScalar(textQuery));
            }
            catch
            {
                excludeStationCodeFrom = DateTime.MinValue;
            }

            try
            {
                DataSet dataSet = CreateDataSetCarduri();
                try
                {
                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        if (dataRow[6].ToString() == string.Empty)
                            continue;
                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        card["NrAuto"] = dataRow[1].ToString();
                        card["CardPan"] = dataRow[2].ToString().Replace("'", string.Empty);
                        //card["NrFactura"] = dataRow[0].ToString();
                        //card["DataFacturare"] = CorrectDKV(dataRow[3].ToString(), false);
                        DateTime dataLivrare = /*CorrectDateTime(dataRow[6].ToString(), false); //*/CorrectDKV(dataRow[6].ToString(), false);
                        DateTime oraLivrare = /*CorrectDateTime(dataRow[6].ToString(), true); //*/CorrectDKV(dataRow[6].ToString(), true);
                        card["DataLivrare"] = dataLivrare;
                        card["OraLivrare"] = oraLivrare;
                        //card["DataLivrare"] = CorrectDKV(dataRow[3].ToString(), false);
                        //card["OraLivrare"] = CorrectDKV(dataRow[3].ToString(), true);
                        card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[8].ToString()), 0.0M);
                        card["Produs"] = dataRow[13].ToString();//Base.Imports.RemoveDiacritics.RemoveDiacriticsFromString(dataRow[13].ToString());
                        card["Tara"] = GetCountryName(CorrectTaraDKV(dataRow[17].ToString()));
                        //if (dataRow[17].ToString().Trim().ToUpper().StartsWith("RO"))
                        //    card["CodTara"] = 28;
                        //card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[9].ToString()), 0.0M);
                        //card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[17].ToString()), 0.0M);
                        //card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) + UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[10].ToString()), 0.0M);
                        card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[10].ToString()), 0.0M) - UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[17].ToString()), 0.0M);
                        card["PretPerLitru"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) / (UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M) == 0.0M ? 1 : UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        card["VATAmount"] = UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        card["LocatieStatie"] = dataRow[25].ToString();
                        card["CodStatie"] = dataRow[26].ToString();
                        card["Retea"] = "DKV";
                        card["Kilometri"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[30].ToString()), 0.0M);
                        card["Valuta"] = "EUR";
                        card["FurnizorImportat"] = FurnizorImportat;
                        
                        card["Card_ID"] = CardIDFindAndMatch(card["CardPan"].ToString(), UtilsGeneral.ToDateTime(card["DataLivrare"]).Date, UtilsGeneral.ToDateTime(card["OraLivrare"]));

                        int nrAuto_ID = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true, dataLivrare.Date + oraLivrare.TimeOfDay), 0); //(NrAutoIDFindAndMatchFromCardPan(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString()));
                        card["NrAuto_ID"] = nrAuto_ID;
                        if (nrAuto_ID == 0)
                        {
                            //card["NrAutoTerti_ID"] = NrAutoTertiFind(card["NrAuto"].ToString());
                            card["NrAutoTerti_ID"] = NrAutoTertiFind(UtilsGeneral.ToInteger(card["Card_ID"], 0), dataLivrare.Date + oraLivrare.TimeOfDay);
                        }
                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(card["Produs"].ToString(), CleanString(dataRow[12].ToString()), FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        //parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                        //parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                        //parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        //parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));

                        if(excludeStationCodeFrom == DateTime.MinValue || excludeStationCodeFrom > dataLivrare)
                            parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());

                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private void ExtractDKVCarduriTip1()
        {
            DateTime excludeStationCodeFrom;
            try
            {
                Query textQuery = new Query("SELECT String FROM Parametri WHERE Parametru_ID = 116");
                excludeStationCodeFrom = UtilsGeneral.ToDateTime(WindDatabase.ExecuteScalar(textQuery));
            }
            catch
            {
                excludeStationCodeFrom = DateTime.MinValue;
            }
            try
            {
                DataSet dataSet = CreateDataSetCarduri();
                try
                {
                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        if (dataRow[5].ToString() == string.Empty)
                            continue;
                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        card["NrAuto"] = dataRow[1].ToString();
                        card["CardPan"] = dataRow[2].ToString().Replace("'", string.Empty);
                        //card["NrFactura"] = dataRow[0].ToString();
                        //card["DataFacturare"] = CorrectDKV(dataRow[3].ToString(), false);
                        //DateTime dataLivrare = CorrectDateTime(dataRow[5].ToString(), false); 
                        //DateTime oraLivrare = CorrectDateTime(dataRow[5].ToString(), true); 
                        DateTime dataLivrare = CorrectDKV(dataRow[5].ToString(), false);
                        DateTime oraLivrare = CorrectDKV(dataRow[5].ToString(), true);
                        card["DataLivrare"] = dataLivrare;
                        card["OraLivrare"] = oraLivrare;
                        //card["DataLivrare"] = CorrectDKV(dataRow[3].ToString(), false);
                        //card["OraLivrare"] = CorrectDKV(dataRow[3].ToString(), true);
                        card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[7].ToString()), 0.0M);
                        card["Produs"] = dataRow[12].ToString();//Base.Imports.RemoveDiacritics.RemoveDiacriticsFromString(dataRow[13].ToString());
                        card["Tara"] = GetCountryName(CorrectTaraDKV(dataRow[16].ToString()));
                        //if (dataRow[17].ToString().Trim().ToUpper().StartsWith("RO"))
                        //    card["CodTara"] = 28;
                        //card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[9].ToString()), 0.0M);
                        //card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[18].ToString()), 0.0M);
                        //card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) + UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[10].ToString()), 0.0M) + UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[22].ToString()), 0.0M);
                        card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[18].ToString()), 0.0M);
                        card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[10].ToString()), 0.0M) + UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[22].ToString()), 0.0M) - UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[18].ToString()), 0.0M);
                        card["PretPerLitru"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) / (UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M) == 0.0M ? 1 : UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        //card["VATAmount"] = UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        card["LocatieStatie"] = dataRow[24].ToString();
                        card["CodStatie"] = dataRow[25].ToString();
                        card["Retea"] = "DKV";
                        card["Kilometri"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[29].ToString()), 0.0M);
                        card["Valuta"] = "EUR";
                        card["FurnizorImportat"] = FurnizorImportat;
                        card["Card_ID"] = CardIDFindAndMatch(card["CardPan"].ToString(), UtilsGeneral.ToDateTime(card["DataLivrare"]).Date, UtilsGeneral.ToDateTime(card["OraLivrare"]));

                        int nrAuto_ID = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true, dataLivrare.Date + oraLivrare.TimeOfDay), 0); //(NrAutoIDFindAndMatchFromCardPan(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString()));
                        card["NrAuto_ID"] = nrAuto_ID;
                        if (nrAuto_ID == 0)
                        {
                            //card["NrAutoTerti_ID"] = NrAutoTertiFind(card["NrAuto"].ToString());
                            card["NrAutoTerti_ID"] = NrAutoTertiFind(UtilsGeneral.ToInteger(card["Card_ID"], 0), dataLivrare.Date + oraLivrare.TimeOfDay);
                        }

                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(card["Produs"].ToString(), CleanString(dataRow[11].ToString()), FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        //parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                        //parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                        //parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        //parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));

                        if (excludeStationCodeFrom == DateTime.MinValue || excludeStationCodeFrom > dataLivrare)
                            parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());

                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private void ExtractDKVFacturi()
        {
            DateTime excludeStationCodeFrom;
            try
            {
                Query textQuery = new Query("SELECT String FROM Parametri WHERE Parametru_ID = 116");
                excludeStationCodeFrom = UtilsGeneral.ToDateTime(WindDatabase.ExecuteScalar(textQuery));
            }
            catch
            {
                excludeStationCodeFrom = DateTime.MinValue;
            }
            try
            {
                DataSet dataSet = CreateDataSetCarduri();
                try
                {
                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        if (dataRow[6].ToString() == string.Empty)
                            continue;
                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        card["NrAuto"] = dataRow[1].ToString();
                        card["CardPan"] = dataRow[2].ToString().Replace("'", string.Empty);
                        card["NrFactura"] = dataRow[0].ToString();
                        card["DataFacturare"] = CorrectDateTime(dataRow[2].ToString(), false);
                        //card["DataLivrare"] = CorrectDateTime(dataRow[6].ToString(), false); //CorrectDKV(dataRow[6].ToString(), false);
                        //card["OraLivrare"] = CorrectDateTime(dataRow[6].ToString(), true); //CorrectDKV(dataRow[6].ToString(), true);
                        DateTime dataLivrare = CorrectDateTime(dataRow[6].ToString(), false);
                        DateTime oraLivrare = CorrectDateTime(dataRow[6].ToString(), true);
                        card["DataLivrare"] = dataLivrare;
                        card["OraLivrare"] = oraLivrare;
                        card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[8].ToString()), 0.0M);
                        card["Produs"] = dataRow[13].ToString();//Base.Imports.RemoveDiacritics.RemoveDiacriticsFromString(dataRow[13].ToString());
                        card["Tara"] = GetCountryName(CorrectTaraDKV(dataRow[17].ToString()));
                        if (dataRow[17].ToString().Trim().ToUpper().StartsWith("RO"))
                            card["CodTara"] = 38;
                        card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[10].ToString()), 0.0M) + UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[22].ToString()), 0.0M) - UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[20].ToString()), 0.0M);
                        card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[19].ToString()), 0.0M);
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) + UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        card["PretPerLitru"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) / (UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M) == 0.0M ? 1 : UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        card["VATAmount"] = UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        card["LocatieStatie"] = dataRow[25].ToString();
                        card["CodStatie"] = dataRow[26].ToString();
                        card["Retea"] = "DKV";
                        card["Kilometri"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[14].ToString()), 0.0M);
                        card["Valuta"] = "EUR";
                        card["FurnizorImportat"] = FurnizorImportat;
                        card["NrAuto_ID"] = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true, dataLivrare.Date + oraLivrare.TimeOfDay), 0); //(NrAutoIDFindAndMatchFromCardPan(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString()));
                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(card["Produs"].ToString(), CleanString(dataRow[12].ToString()), FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        //parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                        //parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                        parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));

                        if (excludeStationCodeFrom == DateTime.MinValue || excludeStationCodeFrom > dataLivrare)
                            parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());

                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }
        /*
        private void ExtractEuroShellRozattiKSC()
        {
            if (!ValidateImportBalantaEmisa("DeliveryDate"))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add("Carduri");
            dataSet.Tables["Carduri"].Columns.Add("NrFactura", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("DataFactura", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("Tara", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("DataLivrare", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("CardPan", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("NrAuto", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("NrAuto_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("Kilometri", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("LocatieStatie", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Produs", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Produs_ID", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("Valuta", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("Cantitate", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("Pret", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("ValoareFaraTVA", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("ValoareTVA", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("ValoareCuTVA", typeof(Decimal));
            dataSet.Tables["Carduri"].Columns.Add("Retea", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("FurnizorImportat", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("DataImport", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("OraImport", typeof(DateTime));
            dataSet.Tables["Carduri"].Columns.Add("UserImport", typeof(String));
            dataSet.Tables["Carduri"].Columns.Add("HashCode", typeof(Int32));
            dataSet.Tables["Carduri"].Columns.Add("Observatii", typeof(String));

            try
            {
                int NoOfRowsFromDB = 0;

                foreach (DataRow dataRow in MasterTable.Rows)
                {
                    DataRow card = dataSet.Tables["Carduri"].NewRow();
                    card["NrFactura"] = dataRow["OrderNumber"].ToString();
                    card["DataFactura"] = (UtilsGeneral.ToDateTime(dataRow["OrderDate"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["OrderDate"]));
                    card["Tara"] = dataRow["CountryName"].ToString();
                    card["DataLivrare"] = (UtilsGeneral.ToDateTime(dataRow["DeliveryDate"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["DeliveryDate"]));
                    card["CardPan"] = dataRow["Card"].ToString();
                    card["NrAuto"] = CleanStringMaster(dataRow["Vehicle"].ToString());
                    card["NrAuto_ID"] = (NrAutoIDFindAndMatch(card["NrAuto"].ToString()) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString()));
                    card["Kilometri"] = UtilsGeneral.ToDecimal(dataRow["Kilometrage"], 0.0M);
                    card["LocatieStatie"] = dataRow["StationName"].ToString();
                    card["Produs"] = dataRow["ProductName"].ToString();
                    card["Produs_ID"] = (ProdusIDFindAndMatch(card["Produs"].ToString(), FurnizorImportat) == null ? 0 : ProdusIDFindAndMatch(card["Produs"].ToString(), FurnizorImportat));
                    card["Valuta"] = "EUR";
                    card["Cantitate"] = UtilsGeneral.ToDecimal(dataRow["LocalQuantity"], 0.0M);
                    card["Pret"] = UtilsGeneral.ToDecimal(dataRow["Price"], 0.0M);
                    card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(dataRow["NetAmount"], 0.0M);
                    card["ValoareTVA"] = UtilsGeneral.ToDecimal(dataRow["VatAmount"], 0.0M);
                    card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(dataRow["TotalAmount"], 0.0M);
                    card["Retea"] = "EUROSHELL";
                    card["FurnizorImportat"] = FurnizorImportat;
                    card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["UserImport"] = UserImport;
                    card["Observatii"] = dataRow["LinkHeader"].ToString();

                    Hashtable parametersForComputeHashCode = new Hashtable();
                    parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                    parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                    parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                    parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                    parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                    parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFactura"]));
                    parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                    parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                    card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                    dataSet.Tables["Carduri"].Rows.Add(card);

                    NoOfRowsFromDB++;
                }

                if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    ImportMessage = Error;
                    return;
                }

                if (NoOfRowsFromDB == 0)
                {
                    Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                    ImportRealizat = false;
                    ImportMessage = Error;
                    return;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                ImportRealizat = false;
                ImportMessage = ex.Message;
                return;
            }
            
            SaveImports(dataSet.Tables["Carduri"]);
        }
        */
        private void ExtractHugo()
        {

            if (!IsFromFinanciarOpened)
                ExtractHugoCard();
            else
            {
                Error = "Eroare la import. Acest tip de import nu este disponibil.";
            }
        }

        private void ExtractHugoCard()
        {
            try
            {
                DataSet dataSet = CreateDataSetCarduri();
                try
                {
                    int NoOfRowsFromDB = 0;

                    //----------------------------------------------------------------------------------------------------------------------------------
                    //Grupare inregistrari in functie de campurile [Numãr de înmatriculare], [Monedã], [Data cumpãrãrii] si suma pe coloana [Cheltuiala]

                    for(int i=0; i < MasterTable.Columns.Count; i++)
                    {
                        MasterTable.Columns[i].ColumnName = "C" + i.ToString();
                    }

                    List<object> DbList = new List<object>();
                    SqlParameter p = new SqlParameter("@tableImport", MasterTable);
                    p.SqlDbType = SqlDbType.Structured;
                    p.TypeName = "dbo.ImportCombustibilCardHUGOType";
                    DbList.Add(p);
                    DataTable dtTabelaImport = WindDatabase.FillDataTableEx(MasterTable.TableName, "usp__ImportCombustibilCardHUGO_GroupBy", DbList.ToArray());
                    DbList.Clear();

                    if (dtTabelaImport.Rows.Count != 0)
                    {
                        MasterTable.Clear();

                        foreach (DataRow dataRow in dtTabelaImport.Rows)
                        {
                            if (null != dataRow)
                                MasterTable.Rows.Add(dataRow.ItemArray);
                        }
                    }
                    //----------------------------------------------------------------------------------------------------------------------------------

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        //card["CodStatie"] = dataRow[10].ToString();
                        //card["NrAuto"] = NrAutoFind(dataRow[2].ToString(), false);
                        card["NrAuto"] = dataRow[2].ToString();
                        //card["Tara"] = dataRow[3].ToString();
                        card["Tara"] = "UNGARIA";
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[8].ToString()), 0.0M);
                        card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[8].ToString()), 0.0M);
                        card["ValoareTVA"] = 0;
                        card["Produs"] = "VIGNETA";
                        card["Valuta"] = dataRow[9].ToString(); ;
                        //card["DataLivrare"] = (UtilsGeneral.ToDateTime(dataRow[11]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow[10]).Date);
                        //card["OraLivrare"] = (UtilsGeneral.ToDateTime(dataRow[11]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow[10]));
                        //card["DataFacturare"] = (UtilsGeneral.ToDateTime(dataRow[11]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow[10]).Date);

                        DateTime dataOraLivrare;
                        if (UtilsGeneral.ToDateTime(dataRow[11]) == DateTime.MinValue)
                        {
                            if (UtilsGeneral.ToDateTime(dataRow[10]) == DateTime.MinValue)
                            {
                                card["DataLivrare"] =
                                card["OraLivrare"] = dataOraLivrare = new DateTime(1900, 1, 1);
                                //card["DataFacturare"] = new DateTime(1900, 1, 1);
                            }
                            else
                            {
                                card["DataLivrare"] = UtilsGeneral.ToDateTime(dataRow[10]).Date;
                                card["OraLivrare"] = dataOraLivrare = UtilsGeneral.ToDateTime(dataRow[10]);
                                //card["DataFacturare"] = UtilsGeneral.ToDateTime(dataRow[10]).Date;
                            }
                        }
                        else
                        {
                            card["DataLivrare"] = UtilsGeneral.ToDateTime(dataRow[11]).Date;
                            card["OraLivrare"] = dataOraLivrare = UtilsGeneral.ToDateTime(dataRow[11]);
                            //card["DataFacturare"] = UtilsGeneral.ToDateTime(dataRow[11]).Date;
                        }

                        card["Cantitate"] = 1;
                        card["PretPerLitru"] = card["ValoareFaraTVA"];
                        card["FurnizorImportat"] = FurnizorImportat;
                        card["NrAuto_ID"] = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true, dataOraLivrare), 0);
                        card["Produs_ID"] = UtilsGeneral.ToInteger(ProdusIDFindAndMatchCardFacturi(card["Produs"].ToString(), "", FurnizorImportat), 0);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;
                        card["Retea"] = "HU-GO";

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("NrAuto", card["NrAuto"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        //parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                        //parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                        parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }

        private void ExtractUTA()
        {

            if (!IsFromFinanciarOpened)
                ExtractUTACard();
            else
            {
                Error = "Eroare la import. Acest tip de import nu este disponibil.";
            }
        }
        private void ExtractUTACard()
        {
            DataSet dataSet = CreateDataSetCarduri();

            try
            {
                int NoOfRowsFromDB = 0;
                MasterTable.Rows[0].Delete();
                foreach (DataRow dataRow in MasterTable.Select())
                {
                    DataRow card = dataSet.Tables["Carduri"].NewRow();
                    //card["NrFactura"] = dataRow["factura nr."].ToString();
                    //card["DataFacturare"] = (UtilsGeneral.ToDateTime(dataRow["data factura"]) == DateTime.MinValue ? new DateTime(1900, 1, 1) : UtilsGeneral.ToDateTime(dataRow["data factura"]));
                    DateTime dataLivrare, oraLivrare;
                    decimal valoareCuTVA, cantitate;
                    card["DataLivrare"] = dataLivrare = CorrectDKV(dataRow[1].ToString(), false);
                    card["OraLivrare"] = oraLivrare = CorrectDKV(dataRow[2].ToString(), true);
                    card["DataFacturare"] = CorrectDKV(dataRow[0].ToString(), false);
                    //try
                    //{
                    card["LocatieStatie"] = dataRow[10].ToString();
                    card["IntrareLocatie"] = dataRow[10].ToString();
                    //}
                    //catch
                    //{
                    //    card["LocatieStatie"] = dataRow[5].ToString();
                    //    card["IntrareLocatie"] = dataRow[5].ToString();
                    //}
                    card["CodStatie"] = dataRow[9].ToString();
                    card["NrAuto"] = dataRow[3].ToString();
                    card["Produs"] = Base.Imports.RemoveDiacritics.RemoveDiacriticsFromString((dataRow[13].ToString()));
                    card["Cantitate"] = cantitate = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[14].ToString()), 0.0M);
                    card["Valuta"] = dataRow[16].ToString();
                    if (card["Valuta"].ToString().ToUpper() == "RON")
                        card["CodTara"] = 38;
                    card["Tara"] = GetCountryName(dataRow[8].ToString());
                    card["Produs_ID"] = ProdusIDFindAndMatch(CleanString(card["Produs"].ToString()), FurnizorImportat);
                    card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[17].ToString()), 0.0M);
                    card["ValoareCuTVA"] = valoareCuTVA = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[19].ToString()), 0.0M);
                    card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[18].ToString()), 0.0M);
                    //card["PretPerLitru"] = UtilsGeneral.ToDecimal(dataRow[14], 0.0M);
                    card["PretPerLitru"] = cantitate == 0 ? 0m : valoareCuTVA / cantitate;
                    card["VATAmount"] = 0.0M;
                    card["CardPan"] = dataRow[4].ToString(); //Serie virtuala card
                    card["Kilometri"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[5].ToString()), 0.0M);
                    card["Retea"] = "UTA";
                    card["FurnizorImportat"] = FurnizorImportat;
                    card["NrAuto_ID"] = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true, dataLivrare.Date + oraLivrare.TimeOfDay), 0);
                    card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["UserImport"] = UserImport;

                    Hashtable parametersForComputeHashCode = new Hashtable();
                    parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                    parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                    parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                    parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                    parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                    parametersForComputeHashCode.Add("NrFacturare", card["NrFactura"].ToString());
                    parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                    parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                    parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                    parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                    parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                    card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                    dataSet.Tables["Carduri"].Rows.Add(card);

                    NoOfRowsFromDB++;
                }

                if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    ImportMessage = Error;
                    return;
                }

                if (NoOfRowsFromDB == 0)
                {
                    Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                    ImportRealizat = false;
                    ImportMessage = Error;
                    return;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                ImportRealizat = false;
                ImportMessage = ex.Message;
                return;
            }

            SaveImports(dataSet.Tables["Carduri"]);
        }


        private void SaveImports(DataTable carduri)
        {
            try
            {
                StringBuilder xmlString = new StringBuilder();
                xmlString.Append("<root>");

                foreach (DataRow dataRow in carduri.Rows)
                {
                    xmlString.Append("<ImportCombustibilCard " + UtilsXML.RowToXML(dataRow, true) + "/>");
                }

                xmlString.Append("</root>");

                List<object> DbList = new List<object>();
                DbList.Add(xmlString.ToString());
                DbList.Add(Base.Configuration.LoginClass.userID);
                DbList.Add(0);

                int returnValue = 0;

                string spProcedure = string.Empty;
                if (!IsFromFinanciarOpened)
                {
                    spProcedure = "sp__ImportCombustibilCard_Insert_Update_TP";
                }
                else
                {
                    spProcedure = "sp__ImportCombustibilCardFacturi_Insert_Update_TP";
                }
                
                using (System.Data.SqlClient.SqlDataReader reader = (System.Data.SqlClient.SqlDataReader)WindDatabase.ExecuteDataReader(spProcedure, DbList.ToArray()))
                {
                    if (reader.Read())
                    {
                        returnValue = UtilsGeneral.ToInteger(reader["ReturnValue"], 0);
                    }
                    else
                    {
                        returnValue = 3;
                    }
                }

                DbList.Clear();

                if (returnValue == 2)
                    ImportRealizat = false;
                else if (returnValue == 3) //eroare in procedura stocata
                {
                    ImportRealizat = false;
                    throw new Exception("A aparut o eroare la salvare. Operatiune esuata !");
                }
                else
                    ImportRealizat = true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return;
            }
        }

        private void ExtractShell()
        {
            if (!IsAnexaFactura)
            {
                try
                {
                    ExtractShellCardFacturi();
                }
                catch
                {
                    Error = "Eroare la import.";
                }
            }
            else // vin de pe facturi primite (anexa la factura)
            {
                try
                {
                    ExtractShellAnexaFactura();
                }
                catch
                {
                    Error = "Eroare la import.";
                }
            }
        }

        private void ExtractTelepass()
        {
            //try
            //{
            //    ExtractShellTelepassDianthus();
            //}
            //catch
            //{
            //    Error = "Eroare la import.";
            //}
            //Modificat pentru Tichet Intern Id 2783 - Import Telepass model ComDivers
            if (!IsFromFinanciarOpened)
            {
                try
                {
                    ExtractTelepassCarduri();
                }
                catch
                {
                    Error = "Eroare la import.";
                }
            }
            else // vin de pe facturi primite (anexa la factura)
            {
                try
                {
                    ExtractTelepassFacturi();
                }
                catch
                {
                    Error = "Eroare la import.";
                }
            }
        }

        public static bool IsDateTime(string txtDate)
        {
            DateTime tempDate;
            return DateTime.TryParse(txtDate, out tempDate);
        }

        private void ExtractTelepassFacturi()
        {
            DataSet dataSet = CreateDataSetCarduri();

            try
            {
                int NoOfRowsFromDB = 0;

                string numarFactura = string.Empty;
                DateTime dataFactura = new DateTime(1900, 1, 1);

                if (FileName.Contains("-") && FileName.Contains("\\") && FileName.Length > FileName.LastIndexOf("\\") + 2)
                {
                    string[] elemente_fisier = new string[3];
                    char[] caractere_delimitare = new char[] { '-' };
                    elemente_fisier = FileName.Substring(FileName.LastIndexOf("\\") + 1).Replace(".xlsx", string.Empty).Replace(".xls", string.Empty).Split(caractere_delimitare);

                    if (elemente_fisier.Length == 3)
                    {
                        if (elemente_fisier[0].ToLower() == "telepass" && elemente_fisier[1].All(char.IsDigit) && IsDateTime(elemente_fisier[2]))
                        {
                            dataFactura = CorrectDateTime(elemente_fisier[2].Trim(), false);
                            numarFactura = elemente_fisier[1];
                        }
                        else
                        {
                            Error = "Numele fisierului nu este in formatul corect: TELEPASS-NrFactura-zz.ll.anul.xls sau TELEPASS-NrFactura-zz.ll.anul.xlsx";
                            ImportMessage = Error;
                            return;
                        }
                    }
                    else
                    {
                        Error = "Numele fisierului nu este in formatul corect: TELEPASS-NrFactura-zz.ll.anul.xls sau TELEPASS-NrFactura-zz.ll.anul.xlsx";
                        ImportMessage = Error;
                        return;
                    }
                }
                else
                {
                    Error = "Numele fisierului nu este in formatul corect: TELEPASS-NrFactura-zz.ll.anul.xls sau TELEPASS-NrFactura-zz.ll.anul.xlsx";
                    ImportMessage = Error;
                    return;
                }


                foreach (DataRow dataRow in MasterTable.Rows)
                {
                    DataRow card = dataSet.Tables["Carduri"].NewRow();
                    card["NrFactura"] = numarFactura;
                    card["Tara"] = "ITALIA";
                    DateTime dataLivrare, oraLivrare;
                    card["DataLivrare"] = dataLivrare = CorrectDateTime(dataRow[0].ToString(), false);
                    card["OraLivrare"] = oraLivrare = CorrectDateTime(dataRow[1].ToString(), true);
                    card["CardPan"] = dataRow[2].ToString();
                    card["Card_ID"] = CardIDFindAndMatch(card["CardPan"].ToString(), UtilsGeneral.ToDateTime(card["DataLivrare"]).Date, UtilsGeneral.ToDateTime(card["OraLivrare"]));
                    card["DataFacturare"] = (dataFactura == DateTime.MinValue ? new DateTime(1900, 1, 1) : dataFactura);

                    card["NrAuto"] = dataRow[3].ToString();
                    //card["NrAuto_ID"] = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true), 0);
                    card["NrAuto_ID"] = ((object)NrAutoIDFindAndMatchFromCardPan_mutareCard(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) ?? DBNull.Value);
                    //Sinziana 07.11.2016 T 2783
                    if (UtilsGeneral.ToInteger(card["NrAuto_ID"], 0) == 0)
                        card["NrAuto_ID"] = NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataLivrare.Date + oraLivrare.TimeOfDay);
                    //end T 2783
                    card["Produs"] = "Taxa drum";
                    card["Cantitate"] = 1;
                    card["Valuta"] = "EUR";
                    card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalTelePass(dataRow[4].ToString()), 0.0M);
                    card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalTelePass(dataRow[5].ToString()), 0.0M);
                    card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) + UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                    card["PretPerLitru"] = (decimal)card["ValoareFaraTVA"];
                    card["VATAmount"] = (decimal)card["ValoareTVA"];
                    card["Retea"] = "TELEPASS";
                    card["FurnizorImportat"] = FurnizorImportat;

                    card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(card["Produs"].ToString(), string.Empty, FurnizorImportat);
                    card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["UserImport"] = UserImport;
                    //string locatie = dataRow[5].ToString();
                    //string[] separator = new string[] { "=>" };
                    //string[] result = locatie.Split(separator, StringSplitOptions.None);
                    //card["IntrareLocatie"] = result[0].ToString();
                    //card["IesireLocatie"] = result[1].ToString();

                    Hashtable parametersForComputeHashCode = new Hashtable();
                    parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                    parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                    parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                    parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                    parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                    parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                    parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                    parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                    parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                    parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                    parametersForComputeHashCode.Add("NrBon", card["NrBon"].ToString());
                    parametersForComputeHashCode.Add("NrAuto", card["NrAuto"].ToString());
                    parametersForComputeHashCode.Add("Tara", card["Tara"].ToString());
                    parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                    card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                    dataSet.Tables["Carduri"].Rows.Add(card);

                    NoOfRowsFromDB++;
                }

                if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    ImportMessage = Error;
                    return;
                }

                if (NoOfRowsFromDB == 0)
                {
                    Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                    ImportRealizat = false;
                    ImportMessage = Error;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch (Exception ex)
            {
                Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                ImportRealizat = false;
                ImportMessage = ex.Message;
                return;
            }
        }

        private void ExtractTelepassCarduri()
        {
            DataSet dataSet = CreateDataSetCarduri();

            try
            {
                int NoOfRowsFromDB = 0;

                string numarFactura = string.Empty;
                DateTime dataFactura = new DateTime(1900, 1, 1);
                if (FileName.Contains("-") && FileName.Contains("\\"))
                {
                    if (FileName.Length > FileName.LastIndexOf("\\") + 2)
                    {
                        string[] numarDataFactura = FileName.Substring(FileName.LastIndexOf("\\") + 1).Replace(".xlsx", string.Empty).Replace(".xls", string.Empty).Split('-');
                        if (numarDataFactura.Length > 2)
                        {
                            dataFactura = CorrectDateTime(numarDataFactura[2].ToString().Trim(), false);
                            numarFactura = numarDataFactura[1];
                        }
                    }
                }

                foreach (DataRow dataRow in MasterTable.Rows)
                {
                    DataRow card = dataSet.Tables["Carduri"].NewRow();
                    //card["NrFactura"] = numarFactura;
                    card["Tara"] = "ITALIA";
                    DateTime dataLivrare, oraLivrare;
                    card["DataLivrare"] = dataLivrare = CorrectDateTime(dataRow[0].ToString(), false);
                    card["OraLivrare"] = oraLivrare = CorrectDateTime(dataRow[1].ToString(), true);
                    card["CardPan"] = dataRow[2].ToString();
                    card["Card_ID"] = CardIDFindAndMatch(card["CardPan"].ToString(), UtilsGeneral.ToDateTime(card["DataLivrare"]).Date, UtilsGeneral.ToDateTime(card["OraLivrare"]));
                    card["NrAuto"] = dataRow[3].ToString();
                    card["NrAuto_ID"] = ((object)NrAutoIDFindAndMatchFromCardPan_mutareCard(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) ?? DBNull.Value);
                    //Sinziana 07.11.2016 T 2783
                    if (UtilsGeneral.ToInteger(card["NrAuto_ID"], 0) == 0)
                        card["NrAuto_ID"] = NrAutoIDFindAndMatch(card["NrAuto"].ToString(), dataLivrare.Date + oraLivrare.TimeOfDay);
                    //end T 2783
                    //card["NrAuto_ID"] = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true), 0);
                    card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalTelePass(dataRow[4].ToString()), 0.0M);
                    card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalTelePass(dataRow[5].ToString()), 0.0M);
                    card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) + UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                    string locatie = dataRow[7].ToString();
                    string[] separator = new string[] { "=>" };
                    string[] result = locatie.Split(separator, StringSplitOptions.None);
                    if (result.Length == 2)
                    {
                        card["IntrareLocatie"] = result[0].ToString();
                        card["IesireLocatie"] = result[1].ToString();
                    }
                    else
                    {
                        card["IntrareLocatie"] = locatie;
                        card["IesireLocatie"] = "";
                    }
                    card["PretPerLitru"] = (decimal)card["ValoareFaraTVA"];
                    //card["LocatieStatie"] = dataRow[5].ToString();//CleanString(dataRow[5].ToString());
                    card["DataFacturare"] = (dataFactura == DateTime.MinValue ? new DateTime(1900, 1, 1) : dataFactura);
                    card["Produs"] = "Taxa drum";
                    card["Cantitate"] = 1;
                    card["Valuta"] = "EUR";
                    card["VATAmount"] = (decimal)card["ValoareTVA"];
                    card["Retea"] = "TELEPASS";
                    card["FurnizorImportat"] = FurnizorImportat;
                    card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(card["Produs"].ToString(), string.Empty, FurnizorImportat);
                    card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["UserImport"] = UserImport;

                    Hashtable parametersForComputeHashCode = new Hashtable();
                    parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                    parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                    parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                    parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                    parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                    parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                    parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                    parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                    parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                    parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                    parametersForComputeHashCode.Add("NrBon", card["NrBon"].ToString());
                    parametersForComputeHashCode.Add("NrAuto", card["NrAuto"].ToString());
                    parametersForComputeHashCode.Add("Tara", card["Tara"].ToString());
                    parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                    card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                    dataSet.Tables["Carduri"].Rows.Add(card);

                    NoOfRowsFromDB++;
                }

                if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    ImportMessage = Error;
                    return;
                }

                if (NoOfRowsFromDB == 0)
                {
                    Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                    ImportRealizat = false;
                    ImportMessage = Error;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch (Exception ex)
            {
                Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                ImportRealizat = false;
                ImportMessage = ex.Message;
                return;
            }
        }

        private void ExtractMol()
        {
            try
            {
                ExtractMolCard();
                //if (IsFromFinanciarOpened) ExtractMolAlex();
                //else ExtractMolAlexFleet();
            }
            catch
            {
                Error = "Eroare la import.";
            }
        }

        private void ExtractDaars()
        {
            try
            {
                ExtractDaarsDunca();
            }
            catch
            {
                Error = "Eroare la import.";
            }
        }

        private void ExtractToolColect()
        {
            try
            {
                if (!IsFromFinanciarOpened) ExtractToolColectDunca();
                else ExtractToolColectDuncaFinanciar();
            }
            catch
            {
                Error = "Eroare la import.";
            }
        }

        private void ExtractToolVerag()
        {
            try
            {
                ExtractToolColectATVectra();
            }
            catch
            {
                Error = "Eroare la import.";
            }
        }

        private void ExtractIDS()
        {
            try
            {
                ExtractIDSVectra();
            }
            catch
            {
                Error = "Eroare la import.";
            }
        }
        
        private void ExtractOMV()
        {
            if (!ValidateImportBalantaEmisa(4))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                DataSet dataSet = CreateDataSetCarduri();

                try
                {
                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        card["NrFactura"] = CleanString(dataRow[22].ToString());
                        card["DataFacturare"] = CorrectDateTime(dataRow[23].ToString(), false);
                        card["Tara"] = GetCountryName(dataRow[13].ToString());
                        card["CodTara"] = 0;
                        DateTime dataOraLivrare;
                        card["DataLivrare"] = CorrectDateTime(dataRow[4].ToString(), false);
                        card["OraLivrare"] = dataOraLivrare = CorrectDateTime(dataRow[4].ToString(), true);
                        card["LocatieStatie"] = dataRow[14].ToString();//CleanString(dataRow[14].ToString());
                        //card["IntrareLocatie"] = card["LocatieStatie"].ToString();
                        //card["IesireLocatie"] = card["LocatieStatie"].ToString();
                        card["NrAuto"] = CleanString(dataRow[3].ToString());
                        card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[6].ToString()), 0.0M);
                        card["Valuta"] = "RON";
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[7].ToString()), 0.0M);
                        card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[8].ToString()), 0.0M);
                        card["ValoareFaraTVA"] = (decimal)card["ValoareCuTVA"] - (decimal)card["ValoareTVA"];
                        card["PretPerLitru"] = (decimal)card["ValoareFaraTVA"] / ((decimal)card["Cantitate"] == 0.0M ? 1 : (decimal)card["Cantitate"]);
                        card["VATAmount"] = card["ValoareTVA"];
                        card["CardPan"] = dataRow[0].ToString() + dataRow[1].ToString() + dataRow[2].ToString().Trim().PadLeft(6,'0');
                        card["Card_ID"] = CardIDFindAndMatch(card["CardPan"].ToString(), UtilsGeneral.ToDateTime(card["DataLivrare"]).Date, UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        card["Produs"] = CleanString(dataRow[5].ToString());
                        card["Retea"] = "OMV";
                        card["FurnizorImportat"] = FurnizorImportat;
                        //card["NrAuto_ID"] = ((object)NrAutoIDFindAndMatchFromCardPan_mutareCard(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) ?? DBNull.Value);
                        card["NrAuto_ID"] = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true, dataOraLivrare), 0);
                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(card["Produs"].ToString(), "", FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;
                        card["Kilometri"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[10].ToString()), 0.0M);
                        card["NrBon"] = dataRow[9].ToString();

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                        parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                        parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                        parametersForComputeHashCode.Add("NrBon", card["NrBon"].ToString());
                        parametersForComputeHashCode.Add("NrAuto", card["NrAuto"].ToString());
                        parametersForComputeHashCode.Add("Tara", card["Tara"].ToString());
                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }
        /*
        private void ExtractOMV()
        {
            if (!ValidateImportBalantaEmisa(8))
            {
                Error = "Exista importuri cu data livrarii in luna blocata. Importul nu se poate realiza.";
                ImportRealizat = false;
                ImportMessage = Error;
                return;
            }

            try
            {
                DataSet dataSet = CreateDataSetCarduri();

                try
                {
                    string numarFactura = string.Empty;
                    DateTime dataFactura = new DateTime(1900, 1, 1);
                    if (FileName.Contains("-") && FileName.Contains("\\"))
                    {
                        if (FileName.Length > FileName.LastIndexOf("\\") + 2)
                        {
                            string[] numarDataFactura = FileName.Substring(FileName.LastIndexOf("\\") + 1).Replace(".xlsx", string.Empty).Replace(".xls", string.Empty).Split('-');
                            numarFactura = numarDataFactura[0];
                            if (numarDataFactura.Count() > 1)
                                dataFactura = UtilsGeneral.ToDateTime(numarDataFactura[1].ToString().Trim());
                        }
                    }

                    int NoOfRowsFromDB = 0;

                    foreach (DataRow dataRow in MasterTable.Rows)
                    {
                        DataRow card = dataSet.Tables["Carduri"].NewRow();
                        card["NrFactura"] = numarFactura;
                        card["Tara"] = "ROMANIA";
                        card["CodTara"] = 38;
                        card["DataLivrare"] = UtilsGeneral.ToDateTime(dataRow[8]);
                        card["OraLivrare"] = UtilsGeneral.ToDateTime(dataRow[8]);
                        card["DataFacturare"] = (dataFactura == DateTime.MinValue ? new DateTime(1900, 1, 1) : dataFactura);
                        card["LocatieStatie"] = dataRow[1].ToString();
                        card["IntrareLocatie"] = dataRow[1].ToString();
                        card["IesireLocatie"] = dataRow[2].ToString();
                        card["NrAuto"] = dataRow[0].ToString();
                        card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[4].ToString()), 0.0M);
                        card["Valuta"] = "RON";
                        card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[5].ToString()), 0.0M);
                        card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[6].ToString()), 0.0M);
                        card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalIvariantCulture(dataRow[7].ToString()), 0.0M);
                        card["VATAmount"] = UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M);
                        card["PretPerLitru"] = UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M) / (UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M) == 0.0M ? 1 : UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        card["Retea"] = "OMV";
                        card["FurnizorImportat"] = FurnizorImportat;
                        card["NrAuto_ID"] = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true), 0);
                        //card["NrAuto_ID"] = UtilsGeneral.ToInteger(NrAutoFindMasiniNeanulate(card["NrAuto"].ToString(), true), 0);
                        card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi("", CleanString(dataRow[3].ToString()), FurnizorImportat);
                        card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                        card["UserImport"] = UserImport;

                        Hashtable parametersForComputeHashCode = new Hashtable();
                        parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                        parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                        parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                        parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                        parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                        parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                        parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                        parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                        parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                        parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                        parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                        parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                        card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                        dataSet.Tables["Carduri"].Rows.Add(card);

                        NoOfRowsFromDB++;
                    }

                    if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                    {
                        Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                            + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                            + "este in formatul corect.";
                        ImportMessage = Error;
                        return;
                    }

                    if (NoOfRowsFromDB == 0)
                    {
                        Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                        ImportRealizat = false;
                        ImportMessage = Error;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportRealizat = false;
                    ImportMessage = ex.Message;
                    return;
                }

                SaveImports(dataSet.Tables["Carduri"]);
            }
            catch
            {
                Error = "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                return;
            }
        }
        */
        private void ExtractLukoilCarduri()
        {
            DataSet dataSet = CreateDataSetCarduri();

            try
            {
                int NoOfRowsFromDB = 0;

                string numarFactura = string.Empty;
                DateTime dataFactura = new DateTime(1900, 1, 1);
                if (FileName.Contains("-") && FileName.Contains("\\"))
                {
                    if (FileName.Length > FileName.LastIndexOf("\\") + 2)
                    {
                        string[] numarDataFactura = FileName.Substring(FileName.LastIndexOf("\\") + 1).Replace(".xlsx", string.Empty).Replace(".xls", string.Empty).Split('-');
                        if (numarDataFactura.Length > 2)
                        {
                            dataFactura = CorrectDateTime(numarDataFactura[2].ToString().Trim(), false);
                            numarFactura = numarDataFactura[1];
                        }
                    }
                }

                foreach (DataRow dataRow in MasterTable.Rows)
                {
                    if (dataRow[0].ToString().ToLower().Contains("total"))
                        continue;

                    DataRow card = dataSet.Tables["Carduri"].NewRow();

                    string card_string = dataRow[0].ToString();

                    string filtered = (card_string.StartsWith(":")) ? card_string.Substring(1) : card_string;
                    card["CardPan"] = filtered;
                    DateTime dataOraLivrare;
                    card["DataLivrare"] = CorrectDateTime(dataRow[4].ToString(), false).Date;
                    card["OraLivrare"] = dataOraLivrare = CorrectDateTime(dataRow[4].ToString(), true);
                    card["Card_ID"] = CardIDFindAndMatch(card["CardPan"].ToString(), UtilsGeneral.ToDateTime(card["DataLivrare"]).Date, UtilsGeneral.ToDateTime(card["OraLivrare"]));
                    card["NrAuto"] = dataRow[2].ToString();
                    //card["NrAuto_ID"] = ((object)NrAutoIDFindAndMatchFromCardPan_mutareCard(CleanString(card["CardPan"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) ?? DBNull.Value);
                    int nrAuto_ID = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true, dataOraLivrare), 0);
                    card["NrAuto_ID"] = nrAuto_ID;
                    if (nrAuto_ID == 0)
                    {
                        //card["NrAutoTerti_ID"] = NrAutoTertiFind(card["NrAuto"].ToString());
                        card["NrAutoTerti_ID"] = NrAutoTertiFind(UtilsGeneral.ToInteger(card["Card_ID"], 0), dataOraLivrare);
                    }
                    card["Kilometri"] = ConvertToDecimalInvariantCulture(dataRow[3].ToString()).ToString(CultureInfo.InvariantCulture);
                    card["LocatieStatie"] = dataRow[5].ToString();
                    card["IntrareLocatie"] = dataRow[5].ToString();
                    card["Produs"] = Base.Imports.RemoveDiacritics.RemoveDiacriticsFromString((dataRow[6].ToString())).Replace(" ", string.Empty); // Produs DEL
                    card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(CleanString(card["Produs"].ToString()), string.Empty, FurnizorImportat);
                    //card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[7].ToString()), 0.0M);
                    card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[7].ToString()), 0.0M);

                    card["ValoareFaraTVA"] = UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M) * 100 / (100 + cotaTVARonDefault);
                    card["ValoareTVA"] = UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M) - UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M);


                    card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[8].ToString()), 0.0M);
                    card["NrFactura"] = numarFactura;
                    card["DataFacturare"] = (dataFactura == DateTime.MinValue ? new DateTime(1900, 1, 1) : dataFactura);
                    card["Tara"] = "ROMANIA";
                    card["Valuta"] = "RON";
                    //card["ValoareTVA"] = 0;
                    card["PretPerLitru"] = (decimal)card["ValoareFaraTVA"] / ((decimal)card["Cantitate"] == 0.0M ? 1 : (decimal)card["Cantitate"]);
                    card["VATAmount"] = 0;
                    card["Retea"] = "LUKOIL";
                    card["FurnizorImportat"] = FurnizorImportat;
                    card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["UserImport"] = UserImport;

                    Hashtable parametersForComputeHashCode = new Hashtable();
                    parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                    parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                    parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                    parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                    parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                    parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                    parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                    parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                    parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                    parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                    parametersForComputeHashCode.Add("NrBon", card["NrBon"].ToString());
                    parametersForComputeHashCode.Add("NrAuto", card["NrAuto"].ToString());
                    parametersForComputeHashCode.Add("Tara", card["Tara"].ToString());
                    parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                    card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                    dataSet.Tables["Carduri"].Rows.Add(card);

                    NoOfRowsFromDB++;
                }

                if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    ImportMessage = Error;
                    return;
                }

                if (NoOfRowsFromDB == 0)
                {
                    Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                    ImportRealizat = false;
                    ImportMessage = Error;
                    return;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                ImportRealizat = false;
                ImportMessage = ex.Message;
                return;
            }

            SaveImports(dataSet.Tables["Carduri"]);
        }

        private void ExtractLukoilFacturi()
        {
            DataSet dataSet = CreateDataSetCarduri();

            try
            {
                int NoOfRowsFromDB = 0;

                string numarFactura = string.Empty;
                DateTime dataFactura = new DateTime(1900, 1, 1);
                if (FileName.Contains("-") && FileName.Contains("\\"))
                {
                    if (FileName.Length > FileName.LastIndexOf("\\") + 2)
                    {
                        string[] numarDataFactura = FileName.Substring(FileName.LastIndexOf("\\") + 1).Replace(".xlsx", string.Empty).Replace(".xls", string.Empty).Split('-');
                        if (numarDataFactura.Length > 2)
                        {
                            dataFactura = CorrectDateTime(numarDataFactura[2].ToString().Trim(), false);
                            numarFactura = numarDataFactura[1];
                        }
                    }
                }

                foreach (DataRow dataRow in MasterTable.Rows)
                {
                    if (dataRow[0].ToString().ToLower().Contains("total"))
                        continue;

                    DataRow card = dataSet.Tables["Carduri"].NewRow();
                    card["NrFactura"] = numarFactura;
                    card["DataFacturare"] = (dataFactura == DateTime.MinValue ? new DateTime(1900, 1, 1) : dataFactura);
                    card["Tara"] = "ROMANIA";
                    DateTime dataOraLivrare;
                    card["DataLivrare"] = CorrectDateTime(dataRow[4].ToString(), false).Date;
                    card["OraLivrare"] = dataOraLivrare = CorrectDateTime(dataRow[4].ToString(), true);
                    card["LocatieStatie"] = dataRow[5].ToString();
                    card["IntrareLocatie"] = dataRow[5].ToString();
                    card["NrAuto"] = dataRow[1].ToString();
                    card["Produs"] = Base.Imports.RemoveDiacritics.RemoveDiacriticsFromString((dataRow[3].ToString())).Replace(" ", string.Empty); // Produs DEL
                    card["Cantitate"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[7].ToString()), 0.0M);
                    card["Valuta"] = "RON";
                    card["ValoareCuTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[8].ToString()), 0.0M);
                    card["ValoareTVA"] = UtilsGeneral.ToDecimal(ConvertToDecimalInvariantCulture(dataRow[9].ToString()), 0.0M);
                    card["ValoareFaraTVA"] = (decimal)card["ValoareCuTVA"] - (decimal)card["ValoareTVA"];
                    card["PretPerLitru"] = (decimal)card["ValoareFaraTVA"] / ((decimal)card["Cantitate"] == 0.0M ? 1 : (decimal)card["Cantitate"]);
                    card["CardPan"] = dataRow[0].ToString().Trim();
                    card["Kilometri"] = ConvertToDecimalInvariantCulture(dataRow[6].ToString()).ToString(CultureInfo.InvariantCulture);
                    card["Retea"] = "LUKOIL";
                    card["Card_ID"] = CardIDFindAndMatch(card["CardPan"].ToString(), UtilsGeneral.ToDateTime(card["DataLivrare"]).Date, UtilsGeneral.ToDateTime(card["OraLivrare"]));
                    card["FurnizorImportat"] = FurnizorImportat;
                    card["NrAuto_ID"] = UtilsGeneral.ToInteger(NrAutoFind(card["NrAuto"].ToString(), true, dataOraLivrare), 0);
                    //card["NrAuto_ID"] = (NrAutoIDFindAndMatchFromCardPan(CleanString(card["NrAuto"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString()));
                    //card["NrAuto_ID"] = (NrAutoIDFindAndMatchFromCardPan(CleanString(card["NrAuto"].ToString()), UtilsGeneral.ToDateTime(card["DataLivrare"]), UtilsGeneral.ToDateTime(card["OraLivrare"])) == null ? 0 : NrAutoIDFindAndMatch(card["NrAuto"].ToString()));
                    card["Produs_ID"] = ProdusIDFindAndMatchCardFacturi(CleanString(card["Produs"].ToString()), string.Empty, FurnizorImportat);
                    card["DataImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["OraImport"] = UtilsGeneral.ToDateTime(DateTime.Now);
                    card["UserImport"] = UserImport;

                    Hashtable parametersForComputeHashCode = new Hashtable();
                    parametersForComputeHashCode.Add("CardPan", card["CardPan"].ToString());
                    parametersForComputeHashCode.Add("FurnizorImportat", FurnizorImportat);
                    parametersForComputeHashCode.Add("Produs", card["Produs"].ToString());
                    parametersForComputeHashCode.Add("DataLivrare", UtilsGeneral.ToDateTime(card["DataLivrare"]));
                    parametersForComputeHashCode.Add("OraLivrare", UtilsGeneral.ToDateTime(card["OraLivrare"]));
                    parametersForComputeHashCode.Add("NrFactura", card["NrFactura"].ToString());
                    parametersForComputeHashCode.Add("DataFactura", UtilsGeneral.ToDateTime(card["DataFacturare"]));
                    parametersForComputeHashCode.Add("ValoareCuTVA", UtilsGeneral.ToDecimal(card["ValoareCuTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareFaraTVA", UtilsGeneral.ToDecimal(card["ValoareFaraTVA"], 0.0M));
                    parametersForComputeHashCode.Add("ValoareTVA", UtilsGeneral.ToDecimal(card["ValoareTVA"], 0.0M));
                    parametersForComputeHashCode.Add("Cantitate", UtilsGeneral.ToDecimal(card["Cantitate"], 0.0M));
                    parametersForComputeHashCode.Add("CodStatie", card["CodStatie"].ToString());
                    parametersForComputeHashCode.Add("NrBon", card["NrBon"].ToString());
                    parametersForComputeHashCode.Add("NrAuto", card["NrAuto"].ToString());
                    parametersForComputeHashCode.Add("Tara", card["Tara"].ToString());
                    parametersForComputeHashCode.Add("IsFromFinanciar", IsFromFinanciarOpened);
                    card["HashCode"] = Base.Imports.CombustibilCard.ComputeHashCode(parametersForComputeHashCode);
                    dataSet.Tables["Carduri"].Rows.Add(card);

                    NoOfRowsFromDB++;
                }

                if (dataSet.Tables["Carduri"] == null || dataSet.Tables["Carduri"].Rows.Count == 0)
                {
                    Error = "Eroare la parsarea fisierului. " + System.Environment.NewLine
                        + "Verificati daca fisierul: " + FileName + System.Environment.NewLine
                        + "este in formatul corect.";
                    ImportMessage = Error;
                    return;
                }

                if (NoOfRowsFromDB == 0)
                {
                    Error = "Nu au fost gasite cheltuieli noi pentru a fi importate.";
                    ImportRealizat = false;
                    ImportMessage = Error;
                    return;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                ImportRealizat = false;
                ImportMessage = ex.Message;
                return;
            }

            SaveImports(dataSet.Tables["Carduri"]);
        }


        private void ExtractEliTrans()
        {
            try
            {
                ExtractEliTransDunca();
            }
            catch
            {
                Error = "Eroare la import.";
            }
        }

        private void ExtractFde()
        {
            try
            {
                if (!IsFromFinanciarOpened) ExtractFdeDunca();
                else ExtractFdeDuncaFinanciar();
            }
            catch
            {
                Error = "Eroare la import.";
            }
        }

        private void ExtractRompetrol()
        {
            try
            {
                //if (IsFromFinanciarOpened)
                //    ExtractRompetrolDunca();
                //else ExtractRompetrolDuncaFleet();
                if (IsFromFinanciarOpened)
                    ExtractRompetrolDunca();
                else ExtractRompetrolFiladelfia();
            }
            catch
            {
                Error = "Eroare la import.";
            }
        }

        private void ExtractEuroShell()
        {
            try
            {
                if (!IsFrantaSpania) ExtractEuroShellDianthus();
                else ExtractEuroShellFrantaSpaniaDianthus();
                //ExtractEuroShellRozattiKSC();
            }
            catch
            {
                Error = "Eroare la import.";
            }
        }

        private void ExtractAxxes()
        {
            try
            {
                ExtractAxessDianthus();
            }
            catch
            {
                Error = "Eroare la import.";
            } 
        }

        private void ExtractAs24()
        {
            try
            {
                ExtractAs24Tabita();
            }
            catch
            {
                Error = "Eroare la import.";
            } 
        }

        private void ExtractEuroWag()
        {
            try
            {
                ExtractEuroWagTabita();
                //ExtractEuroWagMarvi();
            }
            catch
            {
                Error = "Eroare la import.";
            } 
        }

        private void ExtractSmartDiesel()
        {
            try
            {
                ExtractSmartDieselF4F();
                //ExtractSmartDieselDunca();
                //ExtractSmartDieselDianthus();
            }
            catch
            {
                Error = "Eroare la import.";
            }
        }

        private void ExtractTSV()
        {
            try
            {
                ExtractTSVMarvi();
            }
            catch
            {
                Error = "Eroare la import.";
            } 
        }

        private void ExtractDKV()
        {
            if (!IsFromFinanciarOpened)
            {
                try
                {
                    ExtractDKVCarduri();
                }
                catch
                {
                    Error = "Eroare la import.";
                }
            }
            else // vin de pe facturi primite (anexa la factura)
            {
                try
                {
                    //ExtractDKVRomVega();
                    ExtractDKVFacturi();
                }
                catch
                {
                    Error = "Eroare la import.";
                }
            }
        }

        private void ExtractLukoil()
        {
            if (!IsFromFinanciarOpened)
            {
                try
                {
                    ExtractLukoilCarduri();
                }
                catch
                {
                    Error = "Eroare la import.";
                }
            }
            else // vin de pe facturi primite (anexa la factura)
            {
                try
                {
                    ExtractLukoilFacturi();
                }
                catch
                {
                    Error = "Eroare la import.";
                }
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
            catch(Exception ex) 
            {
                Error = "Eroare la deschiderea fisierului. " + Environment.NewLine + ex.Message;
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

        #region String Cleaners & Correctors

        private string CleanString(string strIN)
        {
            return Regex.Replace(strIN, @"[^\w\.@-]", "");             
        }

        private string CleanStringDianthusSmartDiesel(string strIN)
        {
            return Regex.Replace(strIN, @"\\", "").Replace(@"""", "")
                .Replace("=", ".").Replace(" ", "").Replace("-","").Replace("&","");
        }

        private string CleanStringMaster(string strIN)
        {
            string result = CleanString(strIN);
            return CleanStringDianthusSmartDiesel(result);
        }

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
                if(strIN.Contains(',') && strIN.Contains('.'))
                {
                    if(strIN.LastIndexOf('.') > strIN.LastIndexOf(',')) //format 1,000,000.00
                    {
                        return Decimal.Parse(strIN.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * minusOperator;
                    }
                    else //format 1.000.000,00
                    {
                        return Decimal.Parse(strIN.Replace(".", "").Replace(",", "."), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * minusOperator;
                    }
                }
                return Decimal.Parse(strIN.Contains(",") ? strIN.Replace(",", ".") : strIN, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * minusOperator;                
            }
            catch { return 0.0M; }
        }

        private DateTime CorrectRomVegaData(string strIN)
        {
            ////try
            ////{
            ////    var result = strIN;
            ////    var spliter = result.Split('/');
            ////    return new DateTime(DateTime.Now.Year, Int32.Parse(spliter[0]), Int32.Parse(spliter[1]));
            ////}
            ////catch { return DateTime.Now; }
            DateTime customDate;
            //string[] formats = { "MM/dd/yy H:mm tt", "M/dd/yy H:mm tt", "MM/d/yy  H:mm tt", "M/d/yy  H:mm tt",
            //                       "MM/dd/yy HH:mm tt", "M/dd/yy HH:mm tt", "MM/d/yy  HH:mm tt", "M/d/yy  HH:mm tt",
            //                         "MM/dd/yyyy  H:mm tt", "dd/MM/yyyy H:mm tt", "M/d/yyyy", "dd/MM/yyyy"};

            string[] formats = {"M/d/yy h:mm:ss tt", "M/d/yy h:mm tt", 
                   "MM/dd/yy hh:mm:ss", "M/d/yy h:mm:ss", 
                   "M/d/yy hh:mm tt", "M/d/yy hh tt", 
                   "M/d/yy h:mm", "M/d/yy h:mm", 
                   "MM/dd/yy hh:mm", "M/dd/yy hh:mm"};

            if (DateTime.TryParseExact(strIN, formats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out customDate))
                return new DateTime(customDate.Year, customDate.Month, customDate.Day);
            else
            {
                return DateTime.Now;
            }
        }


        private DateTime CorrectDateTime(string stringDate, bool isReturnTime)
        {
            DateTime customDate;

            string[] formats = {"M/d/yy h:mm:ss tt", "M/d/yy h:mm tt", 
                   "MM/dd/yy hh:mm:ss", "M/d/yy h:mm:ss", 
                   "M/d/yy hh:mm tt", "M/d/yy hh tt", 
                   "M/d/yy h:mm", "M/d/yy h:mm", 
                   "MM/dd/yy hh:mm", "M/dd/yy hh:mm",
                   "dd/MM/yyyy hh:mm", "dd/MM/yyyy hh:mm tt", "dd/MM/yyyy HH:mm",
                   "dd/MM/yyyy hh:mm:ss", "dd/MM/yyyy", "dd.MM.yyyy", "dd.MM.yyyy h:mm:ss",
                   "dd.MM.yyyy HH:mm:ss", "dd.MM.yyyy hh:mm:ss", "dd.MM.yyyy", "dd/MM/yyyy HH:mm:ss",
                   "HH:mm:ss", "hh:mm:ss",
                   "HH:mm", "hh:mm", "hh:mm:ss", "HH:mm:ss",
                   "yyyyMMdd", "HHmm"};

            if (!isReturnTime)
            {
                if (DateTime.TryParseExact(stringDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out customDate))
                    return new DateTime(customDate.Year, customDate.Month, customDate.Day);
                else
                {
                    return DateTime.Now;
                }
            }
            else
            {
                if (DateTime.TryParseExact(stringDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out customDate))
                    return new DateTime(customDate.Year, customDate.Month, customDate.Day, customDate.Hour, customDate.Minute, customDate.Second);
                else
                {
                    return DateTime.Now;
                }
            }
        }

        private DateTime CorrectAlexMolDataLivrare(string strIN)
        {
            try
            {
                var result = CleanString(strIN).Insert(4, ":").Insert(7, ":");
                var spliter = result.Split(':');
                return new DateTime(Int32.Parse(spliter[0]), Int32.Parse(spliter[1]), Int32.Parse(spliter[2]));                
            }
            catch { return DateTime.Now; }
        }

        private DateTime CorrectAlexMolOraLivrare(string strIN)
        {
            try
            {
                var result = CleanString(strIN).Insert(2, ":");
                var spliter = result.Split(':');
                DateTime tempDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                tempDate.AddMinutes((Double.Parse(spliter[0])));
                return tempDate.AddMinutes(Double.Parse((spliter[1])));
            }
            catch { return DateTime.Now; }
        }

        private DateTime CorrectVectraIDSOraLivrare(string strIN)
        {
            try
            {
                var spliter = strIN.Split(':');
                DateTime tempDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                tempDate.AddMinutes((Double.Parse(spliter[0])));
                return tempDate.AddMinutes(Double.Parse((spliter[1])));
            }
            catch { return DateTime.Now; }
        }

        private DateTime CorrectDKV(string strIN, bool isReturnTime)
        {
            DateTime customDate = DateTime.Parse(strIN, new CultureInfo("nl-NL"), DateTimeStyles.AdjustToUniversal);
            if (!isReturnTime)
            {
                if (customDate != DateTime.MinValue)
                    return new DateTime(customDate.Year, customDate.Month, customDate.Day);
                else
                {
                    return DateTime.Now;
                }
            }
            else
            {
                if (customDate != DateTime.MinValue)
                    return new DateTime(customDate.Year, customDate.Month, customDate.Day, customDate.Hour, customDate.Minute, customDate.Second);
                else
                {
                    return DateTime.Now;
                }
            }
        }

        private string CorrectTaraDKV(string codTara)
        {
            if (codTara.Trim().ToUpper() == "SI")
                return "SLO";
            else
                return codTara;
        }

        private decimal ConvertToDecimalInvariantCulture(string strIN)
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

        private decimal ConvertToDecimalTelePass(string strIN)
        {
            try
            {
                return UtilsGeneral.ToDecimal(strIN, 0.0M);
            }
            catch
            {
                try
                {
                    int minusOperator = 1;
                    if (strIN.Contains("-"))
                    {
                        strIN = strIN.Replace("-", "");
                        minusOperator = -1;
                    }
                    return Decimal.Parse(strIN, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * minusOperator;
                }
                catch { return 0.0M; }
            }
        }
        #endregion String Cleaners & Correctors

        #region Nr Auto Matches
        private string ClearITR(string nrAuto)
        {
            if (nrAuto.Length > 7 && nrAuto.EndsWith("ITR"))
                return nrAuto.Substring(0, nrAuto.Length - 3);

            return nrAuto;
        }

        private int NrAutoIDFindAndMatch(string nrAutoToMatch, DateTime dataOraLivrare)
        {
            if (nrAutoToMatch != string.Empty)
            {
                nrAutoToMatch = nrAutoToMatch.Trim().Replace("-", "").Replace(" ", "");
                //if ((Base.Configuration.Constants.client == Base.Configuration.Constants.Clients.Marvi))
                //    nrAutoToMatch = nrAutoToMatch.Substring(0, (nrAutoToMatch.Length >= 7 ? 7 : nrAutoToMatch.Length));

                #region [1] Incarc datele             

                var currentMasini = DataSetDBFurnizor.Tables["Masini"].Select(null, null, DataViewRowState.CurrentRows);

                string nrAutoID = string.Empty;
                try
                {                   
                    nrAutoID = currentMasini.Where(x => (x["NrAutoMasiniProprii"].ToString().Trim().Replace(" ", "").Replace("-","").ToString() == nrAutoToMatch ||
                                    x["NumeCodFCT"].ToString().Trim().Replace(" ", "").ToString() == nrAutoToMatch) &&
                                    (x["DataRadiere"] == DBNull.Value || UtilsGeneral.ToDateTime(x["DataRadiere"]) > dataOraLivrare))
                                    .OrderBy(x => UtilsGeneral.ToDateTime(x["DataRadiere"], "20991231"))
                                    .ThenBy(x => UtilsGeneral.ToBool(x["AnulareNrAutoMasiniProprii"])).FirstOrDefault()["NrInmatriculareFCT_ID"].ToString() ?? string.Empty;                  
                }
                catch
                {
                    nrAutoID = string.Empty;
                }
                #endregion [1]

                
                #region [2] Parsez datele
                /*
                if ((Base.Configuration.Constants.client == Base.Configuration.Constants.Clients.Marvi) && (nrAutoID == string.Empty))
                {
                    nrAutoToMatch = nrAutoToMatch.Substring(0, (nrAutoToMatch.Length >= 6 ? 6 : nrAutoToMatch.Length));
                    try
                    {
                        nrAutoID = currentMasini.Where(x => (x["NrAutoMasiniProprii"]).ToString().Trim().Replace(" ", "").Replace("-", "").ToString()
                                        == nrAutoToMatch ||
                                        (x["NumeCodFCT"]).ToString().Trim().Replace(" ", "").ToString()
                                        == nrAutoToMatch).FirstOrDefault()["NrInmatriculareFCT_ID"].ToString() ?? string.Empty;
                    }
                    catch
                    {
                        nrAutoID = string.Empty;
                    }
                }
                */
                var rowFoundID = UtilsGeneral.ToInteger(nrAutoID, 0);
                return rowFoundID;

                #endregion [2]
            }
            return 0;//return null;
        }


        private string NrAutoFind(string nrAutoToMatch, bool isReturnID, DateTime dataOraLivrare)
        {
            if (nrAutoToMatch != string.Empty)
            {
                nrAutoToMatch = nrAutoToMatch.Trim().Replace("-", "").Replace(" ", "");
                //nrAutoToMatch = nrAutoToMatch.Substring(0, (nrAutoToMatch.Length >= 7 ? 7 : nrAutoToMatch.Length));

                #region [1] Incarc datele

                var currentMasini = DataSetDBFurnizor.Tables["Masini"].Select(null, null, DataViewRowState.CurrentRows);

                string nrAutoID = string.Empty;
                try
                {
                    nrAutoID = currentMasini.Where(x => (x["NrAutoMasiniProprii"].ToString().Trim().Replace(" ", "").Replace("-", "").ToString() == nrAutoToMatch ||
                                    x["NumeCodFCT"].ToString().Trim().Replace(" ", "").ToString() == nrAutoToMatch) &&
                                    (x["DataRadiere"] == DBNull.Value || UtilsGeneral.ToDateTime(x["DataRadiere"]) > dataOraLivrare))
                                    .OrderBy(x => UtilsGeneral.ToDateTime(x["DataRadiere"], "20991231"))
                                    .FirstOrDefault()["NrInmatriculareFCT_ID"].ToString() ?? string.Empty;
                                    
                                    //.OrderBy(x => UtilsGeneral.ToBool(x["AnulareNrAutoMasiniProprii"])).FirstOrDefault()["NrInmatriculareFCT_ID"].ToString() ?? string.Empty;
                }
                catch
                {
                    nrAutoID = string.Empty;
                }
                #endregion [1]


                #region [2] Parsez datele
                /*
                //Comentata deoarece nrAutoID nu poate fi niciodata null, daca nu se gasesc date nrAutoID este string.Empty 
                if (nrAutoID == null)
                {
                    nrAutoToMatch = nrAutoToMatch.Substring(0, (nrAutoToMatch.Length >= 6 ? 6 : nrAutoToMatch.Length));
                    try
                    {
                        nrAutoID = currentMasini.Where(x => (x["NrAutoMasiniProprii"]).ToString().Trim().Replace(" ", "").Replace("-", "").ToString()
                                        == nrAutoToMatch ||
                                        (x["NumeCodFCT"]).ToString().Trim().Replace(" ", "").ToString()
                                        == nrAutoToMatch).FirstOrDefault()["NrInmatriculareFCT_ID"].ToString() ?? string.Empty;
                    }
                    catch
                    {
                        nrAutoID = string.Empty;
                    }
                }
                */
                return (isReturnID ? nrAutoID : nrAutoToMatch);

                #endregion [2]
            }
            return "";
        }

        private int NrAutoTertiFind(int cardID, DateTime dataOraLivrare)
        {
            if (cardID > 0)
            {
                var currentCarduri = DataSetDBFurnizor.Tables["Masini_Terti_Carduri"].Select(null, null, DataViewRowState.CurrentRows)
                                                          .Where(x => UtilsGeneral.ToInteger(x["Card_ID"], 0) == cardID
                                                                        && x["DataOraStart"] != DBNull.Value && dataOraLivrare >= UtilsGeneral.ToDateTime(x["DataOraStart"])
                                                                        && (x["DataOraEnd"] == DBNull.Value || dataOraLivrare <= UtilsGeneral.ToDateTime(x["DataOraEnd"]))
                                                                      )
                                                          .OrderBy(x => UtilsGeneral.ToDateTime(x["DataOraStart"]).Date)
                                                          .LastOrDefault();

                if (currentCarduri != null)
                    return UtilsGeneral.ToInteger(currentCarduri["AutoID"], 0);
                else
                    return 0;
            }
            return 0;
        }

        /*
        private int NrAutoTertiFind(string nrAutoToMatch)
        {
            if (nrAutoToMatch != string.Empty)
            {
                nrAutoToMatch = nrAutoToMatch.Trim().Replace("-", "").Replace(" ", "");
                //nrAutoToMatch = nrAutoToMatch.Substring(0, (nrAutoToMatch.Length >= 7 ? 7 : nrAutoToMatch.Length));

                #region [1] Incarc datele

                var currentMasini = DataSetDBFurnizor.Tables["MasiniTerti"].Select(null, null, DataViewRowState.CurrentRows);

                DataRow rowAuto = currentMasini.Where(x => (x["NumarInmatriculareMT"].ToString().Trim().Replace(" ", "").Replace("-", "").ToString() == nrAutoToMatch))
                                    .OrderByDescending(x => UtilsGeneral.ToInteger(x["ID_MasinaMT"], 0))
                                    .FirstOrDefault();
                #endregion [1]


                #region [2] Parsez datele

                if (rowAuto != null)
                    return UtilsGeneral.ToInteger(rowAuto["ID_MasinaMT"], 0);
                else
                    return 0;
                #endregion [2]
            }
            return 0;
        }
        */

        /*
        private string NrAutoFindMasiniNeanulate(string nrAutoToMatch, bool isReturnID)
        {
            if (nrAutoToMatch != string.Empty)
            {
                nrAutoToMatch = nrAutoToMatch.Trim().Replace("-", "");
                nrAutoToMatch = nrAutoToMatch.Substring(0, (nrAutoToMatch.Length >= 7 ? 7 : nrAutoToMatch.Length));

                #region [1] Incarc datele

                var currentMasini = DataSetDBFurnizor.Tables["Masini"].Select(null, null, DataViewRowState.CurrentRows);

                string nrAutoID = string.Empty;
                try
                {
                    nrAutoID = currentMasini.Where(x => (x["NrAutoMasiniProprii"]).ToString().Trim().Replace(" ", "").Replace("-", "").ToString()
                                    == nrAutoToMatch ||
                                    (x["NumeCodFCT"]).ToString().Trim().Replace(" ", "").ToString()
                                    == nrAutoToMatch && UtilsGeneral.ToBool(x["AnulareNrAutoMasiniProprii"]) == false).FirstOrDefault()["NrInmatriculareFCT_ID"].ToString() ?? string.Empty;
                    if (nrAutoID == null)
                    {
                        nrAutoID = currentMasini.Where(x => (x["NrAutoMasiniProprii"]).ToString().Trim().Replace(" ", "").Replace("-", "").ToString()
                                        == nrAutoToMatch ||
                                        (x["NumeCodFCT"]).ToString().Trim().Replace(" ", "").ToString()
                                        == nrAutoToMatch).FirstOrDefault()["NrInmatriculareFCT_ID"].ToString() ?? string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    Error = ex.Message + Environment.NewLine + "Eroare la citirea fisierului. Structura fisierului a fost modificata. Au fost adaugate coloane noi. Contactati echipa WindSoft.";
                    ImportMessage = ex.Message;
                    nrAutoID = string.Empty;
                }

                //catch 
                //{
                //    nrAutoID = string.Empty;
                //}
                #endregion [1]


                #region [2] Parsez datele

                if (nrAutoID == null)
                {
                    nrAutoToMatch = nrAutoToMatch.Substring(0, (nrAutoToMatch.Length >= 6 ? 6 : nrAutoToMatch.Length));
                    try
                    {
                        nrAutoID = currentMasini.Where(x => (x["NrAutoMasiniProprii"]).ToString().Trim().Replace(" ", "").Replace("-", "").ToString()
                                       == nrAutoToMatch ||
                                       (x["NumeCodFCT"]).ToString().Trim().Replace(" ", "").ToString()
                                       == nrAutoToMatch).Where(x => UtilsGeneral.ToBool(x["AnulareNrAutoMasiniProprii"]) == false).FirstOrDefault()["NrInmatriculareFCT_ID"].ToString() ?? string.Empty;
                        if (null == nrAutoID)
                        {
                            nrAutoID = currentMasini.Where(x => (x["NrAutoMasiniProprii"]).ToString().Trim().Replace(" ", "").Replace("-", "").ToString()
                                            == nrAutoToMatch ||
                                            (x["NumeCodFCT"]).ToString().Trim().Replace(" ", "").ToString()
                                            == nrAutoToMatch).FirstOrDefault()["NrInmatriculareFCT_ID"].ToString() ?? string.Empty;
                        }
                    }
                    catch
                    {
                        nrAutoID = string.Empty;
                    }
                }

                return (isReturnID ? nrAutoID : nrAutoToMatch);

                #endregion [2]
            }
            return "";
        }
        */
        private int? NrAutoIDFindAndMatchFromCardPan_mutareCard(string serieCard, DateTime? dataLivrare, DateTime? oraLivrare)
        {
            if (serieCard != string.Empty)
            {
                serieCard = serieCard.Trim().Replace("-", "").Replace(" ", "").Replace("'", "");
                #region [1] Incarc datele

                var currentCards = DataSetDBFurnizor.Tables["Carduri"].Select(null, null, DataViewRowState.CurrentRows);
                string nrAutoID = string.Empty;
                int serieCardID = 0;
                try
                {
                    var findSerie = currentCards.Where(x => ((x["SerieCard"]).ToString().Trim().Replace(" ", "").Replace("-", "").Replace("'", "").ToString()
                                                                        == serieCard) && (!UtilsGeneral.ToBool(x["Anulat"]))).FirstOrDefault();
                    //string serie = WindDatabase.ExecuteScalar(new Query(""));
                    nrAutoID = findSerie["NrAuto_ID"].ToString() ?? string.Empty;
                    serieCardID = UtilsGeneral.ToInteger(findSerie["ID"], 0);
                }
                catch
                {
                    nrAutoID = string.Empty;
                }
                #endregion [1]

                #region [2] Parsez datele

                DateTime dataOraLivrare;
                if (oraLivrare.HasValue)
                    dataOraLivrare = dataLivrare.Value.Date + oraLivrare.Value.TimeOfDay;
                else
                    dataOraLivrare = dataLivrare.Value.Date;

                // verificam daca nu a fost mutat cardul de pe o masina pe alta
                if (dataLivrare.HasValue && serieCardID != 0)
                {
                    var currentCarduri = DataSetDBFurnizor.Tables["Masini_Proprii_Carduri"].Select(null, null, DataViewRowState.CurrentRows)
                                                          .Where(x => UtilsGeneral.ToInteger(x["SerieCardID"], 0) == serieCardID
                                                                        && !UtilsGeneral.ToBool(x["Predat"])
                                                                        && x["DataOraStart"] != DBNull.Value && dataOraLivrare >= UtilsGeneral.ToDateTime(x["DataOraStart"])
                                                                        && (x["DataOraEnd"] == DBNull.Value || dataOraLivrare <= UtilsGeneral.ToDateTime(x["DataOraEnd"]))

                                                                      )
                                                          .OrderBy(x => UtilsGeneral.ToDateTime(x["DataOraStart"]).Date)
                                                          .LastOrDefault();

                    if (currentCarduri != null)
                        return UtilsGeneral.ToInteger(currentCarduri["AutoID"], 0);

                    //DateTime data_Livrare = UtilsGeneral.ToDateTime(dataLivrare);
                    //DateTime ora_Livrare = UtilsGeneral.ToDateTime(oraLivrare);

                    //List<object> DbList = new List<object>();
                    //DbList.Add(serieCardID);
                    //DbList.Add(data_Livrare.ToString("yyyyMMdd"));
                    //DbList.Add(ora_Livrare.ToString("yyyyMMdd HH:mm:ss"));

                    //int returnValue = 0;
                    //using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__ImportCombustibilCard_Get_AutoID_Load", DbList.ToArray()))
                    //{
                    //    if (reader.Read())
                    //    {
                    //        returnValue = UtilsGeneral.ToInteger(reader["AutoID"], 0);
                    //    }
                    //    else
                    //    {
                    //        returnValue = 0;
                    //    }
                    //}
                    //DbList.Clear();
                    //if (returnValue != 0)
                    //{
                    //    return UtilsGeneral.ToInteger(returnValue, 0);
                    //}
                }

                var rowFoundID = UtilsGeneral.ToInteger(nrAutoID, 0);
                return rowFoundID;
                #endregion [2]
            }
            return null;
        }

        private int NrAutoIDFindAndMatchFromCardPan(string serieCard, DateTime? dataStart, DateTime? oraStart)
        {
            if (serieCard != string.Empty)
            {
                serieCard = serieCard.Trim().Replace("-", "").Replace(" ", "").Replace("'", "");
                #region [1] Incarc datele

                var currentCards = DataSetDBFurnizor.Tables["Carduri"].Select(null, null, DataViewRowState.CurrentRows);
                string nrAutoID = string.Empty;
                int serieCardID = 0;
                try
                {
                    var findSerie = currentCards.Where(x => ((x["SerieCard"]).ToString().Trim().Replace(" ", "").Replace("-", "").Replace("'", "").ToString()
                                                                        == serieCard) && (!UtilsGeneral.ToBool(x["Anulat"]))).FirstOrDefault();
                    //string serie = WindDatabase.ExecuteScalar(new Query(""));
                    nrAutoID = findSerie["NrAuto_ID"].ToString() ?? string.Empty;
                    serieCardID = UtilsGeneral.ToInteger(findSerie["ID"], 0);
                }
                catch
                {
                    nrAutoID = string.Empty;
                }
                #endregion [1]


                #region [2] Parsez datele


                // verificam daca nu a fost mutat cardul de pe o masina pe alta
                if (dataStart.HasValue && serieCardID != 0)
                {
                    var currentCarduri = DataSetDBFurnizor.Tables["Masini_Proprii_Carduri"].Select(null, null, DataViewRowState.CurrentRows)
                                                          .Where(x => UtilsGeneral.ToInteger(x["SerieCardID"], 0) == serieCardID)
                                                          .Where(x => !UtilsGeneral.ToBool(x["Predat"]))
                                                          .OrderBy(x => UtilsGeneral.ToDateTime(x["DataStart"]).Date)
                                                          .LastOrDefault();
                        if (currentCarduri != null && UtilsGeneral.ToDateTime(currentCarduri["DataStart"]).Date != DateTime.MinValue.Date)
                        {
                            if ((UtilsGeneral.ToDateTime(currentCarduri["DataStart"]).Date == dataStart.Value.Date
                                && UtilsGeneral.ToDateTime(currentCarduri["OraStart"]).TimeOfDay < oraStart.Value.TimeOfDay) ||
                                (UtilsGeneral.ToDateTime(currentCarduri["DataStart"]).Date < dataStart.Value.Date))
                                return UtilsGeneral.ToInteger(currentCarduri["AutoID"], 0);
                        }
                }

                var rowFoundID = UtilsGeneral.ToInteger(nrAutoID, 0);
                return rowFoundID;

                #endregion [2]
            }
            return 0;//return null;
        }

        #endregion Nr Auto Matches

        #region Card Matches

        private int? CardIDFindAndMatch(string serieCard, DateTime dataStart, DateTime oraStart)
        {
            if (serieCard != string.Empty)
            {
                if (dataStart != DateTime.MinValue && oraStart != DateTime.MinValue)
                {
                    var currentCards = DataSetDBFurnizor.Tables["Carduri"].Select(null, null, DataViewRowState.CurrentRows);
                    int serieCardID = 0;
                    try
                    {
                        var findSerie = currentCards.Where(x => ((x["SerieCard"]).ToString().Trim().Replace(" ", "").Replace("-", "").Replace("'", "").ToString()
                                                                            == serieCard) && (!UtilsGeneral.ToBool(x["Anulat"]))).FirstOrDefault();
                        serieCardID = UtilsGeneral.ToInteger(findSerie["ID"], 0);
                    }
                    catch
                    {
                    }

                    if (serieCardID == 0)
                        return 0;

                    var currentCarduri = DataSetDBFurnizor.Tables["Masini_Proprii_Carduri"].Select(null, null, DataViewRowState.CurrentRows)
                                                          .Where(x => UtilsGeneral.ToInteger(x["SerieCardID"], 0) == serieCardID)
                                                          .Where(x => !UtilsGeneral.ToBool(x["Predat"]))
                                                          .OrderBy(x => UtilsGeneral.ToDateTime(x["DataStart"]).Date)
                                                          .LastOrDefault();

                    if (currentCarduri != null && UtilsGeneral.ToDateTime(currentCarduri["DataStart"]).Date != DateTime.MinValue.Date)
                    {
                        if ((UtilsGeneral.ToDateTime(currentCarduri["DataStart"]).Date == dataStart.Date
                            && UtilsGeneral.ToDateTime(currentCarduri["OraStart"]).TimeOfDay < oraStart.TimeOfDay) ||
                            (UtilsGeneral.ToDateTime(currentCarduri["DataStart"]).Date < dataStart.Date))
                            return serieCardID;
                    }
                }
            }

            return 0;
        }

        #endregion

        #region Produs ID Matches

        private int? ProdusIDFindAndMatch(string produsToMatch, int idFurnizorImportat)
        {
            if (produsToMatch != string.Empty)
            {
                #region [1] Incarc datele              

                string produsIDFound = string.Empty;
                try
                {                    
                    string furnizorName = Enum.GetName(typeof(TipFurnizorImportat), idFurnizorImportat);
                    var currentProduse = DataSetDBFurnizor.Tables["Produse"].Select(null, null, DataViewRowState.CurrentRows);

                    produsIDFound = currentProduse.Where(x => ((x["DenumireProdus"]).ToString().Trim().Replace(" ", "").ToUpper().ToString() == produsToMatch.Replace(" ", "").ToUpper() ||
                                   (x["CodProdus"]).ToString().Trim().Replace(" ", "").ToUpper().ToString() == produsToMatch.Replace(" ", "").ToUpper())
                                   && (x["ProdusFurnizor_ID"]).ToString()
                                   == idFurnizorImportat.ToString())
                                   .FirstOrDefault()["ID"].ToString() ?? string.Empty;                 
                  
                }
                catch
                {
                    produsIDFound = string.Empty;
                }
                #endregion [1]              

                #region [2] Parsez datele 

                var rowFoundID = UtilsGeneral.ToInteger(produsIDFound, 0);
                return rowFoundID;

                #endregion [3]
            }
            return null;
        }

        private int? ProdusIDFindAndMatchCardFacturi(string produsToMatch, string codProdusToMatch, int idFurnizorImportat)
        {
            #region [1] Incarc datele

            string produsIDFound = string.Empty;
            try
            {
                string furnizorName = Enum.GetName(typeof(TipFurnizorImportat), idFurnizorImportat);
                var currentProduse = DataSetDBFurnizor.Tables["Produse"].Select(null, null, DataViewRowState.CurrentRows);

                DataRow productsList = null;

                if (!string.IsNullOrEmpty(codProdusToMatch))
                {
                    productsList = currentProduse.Where(x => ((x["CodProdus"]).ToString().Trim().Replace(" ", "").ToUpper().ToString()
                                                            == codProdusToMatch.Replace(" ", "").ToUpper()) && (x["ProdusFurnizor_ID"]).ToString()
                                                            == idFurnizorImportat.ToString()).FirstOrDefault();
                }

                if (productsList != null)
                    produsIDFound = productsList["ID"].ToString() ?? string.Empty;
                else
                {
                    if (!string.IsNullOrEmpty(produsToMatch))
                    {
                        productsList = currentProduse.Where(x => ((x["DenumireProdus"]).ToString().Trim().Replace(" ", "").ToUpper().ToString()
                                                               == produsToMatch.Replace(" ", "").ToUpper()) &&
                                                               x["ProdusFurnizor_ID"].ToString() == idFurnizorImportat.ToString()).FirstOrDefault();
                    }
                    if (productsList != null)
                        produsIDFound = productsList["ID"].ToString() ?? string.Empty;
                }
            }
            catch
            {
                produsIDFound = string.Empty;
            }
            #endregion [1]

            #region [2] Parsez datele

            var rowFoundID = UtilsGeneral.ToInteger(produsIDFound, 0);
            return rowFoundID;

            #endregion [3]
        }

        private int? ProdusIDFrantaSpaniaFindAndMatch(int idFurnizorImportat)
        {
          
                #region [1] Incarc datele

                string produsIDFound = string.Empty;
                try
                {
                    string furnizorName = Enum.GetName(typeof(TipFurnizorImportat), idFurnizorImportat);
                    var currentProduse = DataSetDBFurnizor.Tables["Produse"].Select(null, null, DataViewRowState.CurrentRows);

                    var produse = currentProduse.ToList();
                    produsIDFound = produse.Where(x => (x["DenumireProdus"].ToString().Trim().ToLower() == "motorway" ||
                                                        x["DenumireProdus"].ToString().Trim().ToLower().Contains("motorway"))
                                     && (x["Furnizor"].ToString().Trim().ToLower() == "euroshell" ||
                                         x["Furnizor"].ToString().Trim().ToLower() == "shell"))
                                    .FirstOrDefault()["ID"].ToString();

                }
                catch
                {
                    produsIDFound = string.Empty;
                }
                #endregion [1]

                #region [2] Parsez datele

                var rowFoundID = UtilsGeneral.ToInteger(produsIDFound, 0);
                return rowFoundID;

                #endregion [2]                      
        }

        #endregion Produs ID Matches

        #region Remove Duplicate Items

        private bool CardNotFound(CombustibilCard currentCard)
        {          
            try
            {
                var objectFound = DataSetDBFurnizor.Tables[0].Select("HashCode = " + currentCard.HashCode.ToString()).FirstOrDefault();
                if (objectFound != null)
                {
                    if(currentCard.DataLivrare <= new DateTime(2013, 7, 27))
                    {
                        try
                        {
                            WindDatabase.ExecuteNonQuery(new Query(string.Format("UPDATE ImportCombustibilCard SET CodStatie = {0} where HashCode = '{1}'",  currentCard.CodStatie, currentCard.HashCode )));
                        }
                        catch { }
                    }
                    return false;
                }
            }
            catch { }
            return true;
        }

        #endregion Remove Duplicate Items

        #region Find Country

        public string GetCountryName(string countryCode)
        {
            if (countryCode == string.Empty)
                return string.Empty;

            //foreach (DataRow row in DataSetDBFurnizor.Tables["Lista_Tari"].Rows)
            //{
            //    if (row["PrescurtareTara"].ToString().ToUpper().Trim() == countryCode.ToUpper().Trim())
            //        return row["Tara"].ToString().ToUpper().Trim();
            //}
            //return string.Empty;

            //corectie Austria pentru facturi AS24
            if (countryCode == "AUT")
                countryCode = "AT";
            DataRow rowTara = DataSetDBFurnizor.Tables["Lista_Tari"].Select(string.Format("'{0}' LIKE PrescurtareTara + '%'", countryCode.ToUpper().Trim())).FirstOrDefault();
            if (rowTara != null)
                return rowTara["Tara"].ToString().ToUpper().Trim();
            return string.Empty;
        }

        #endregion
    }
}
