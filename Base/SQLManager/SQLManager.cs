using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Base.BaseUtils;
using Base.Configuration;
using Base.DataBase;
using System.Data.OleDb;
using System.Globalization;
using BaseUtils;

namespace Base.SQLManager
{
    public class SQLManager
    {
        #region Variables

        public string ProcedureName
        {
            get;
            set;
        }

        public string SqlString
        {
            get;
            set;
        }

        public int Result
        {
            get;
            set;
        }       

        #endregion Variables

        #region Enums

        public enum OperationType : short
        {
            CreateTable = 1,
            InsertColumnInTable = 2,
            InsertForeignKeyInTable = 3,
            InsertUpdateStoreProcedure = 4,
            DeleteStoreProcedure = 5,
            InsertUpdateView = 6,
            DeleteView = 7,
            DropDataFromTable = 8,
            ModifyCommonControl = 9
        }

        #endregion Enums

        #region Constructors

        /// <summary>
        /// Constructor implicit.
        /// </summary>
        public SQLManager() { }

        /// <summary>
        /// Constructor cu paramaetri.
        /// </summary>
        /// <param name="_procedureName">Numele procedurii (ex. nume tabel, view, procedura stocata)</param>
        /// <param name="_sqlString"></param>
        public SQLManager(string _procedureName, string _sqlString)
        {
            ProcedureName = _procedureName;
            SqlString = _sqlString;
            Result = 0;
        }

        #endregion Constructors

        #region Operations

        /// <summary>
        /// Executa SQL Manager.
        /// </summary>
        /// <param name="operationType">Tipul operatiei (creare tabel, stergere view etc.).</param>
        /// <returns></returns>
        public bool Execute(int operationType)
        {
            DataSet dataSetManager = new DataSet();
            dataSetManager.Tables.Add("SQLManager");

            WindDatabase.LoadDataSet(dataSetManager, new string[] { "SQLManager" },
                                  "sp__SQL_Manager", new object[] { operationType, ProcedureName, SqlString });

            if (dataSetManager.Tables[0].Rows.Count != 0)
            {
                var sqlManagerResult = dataSetManager.Tables[0].Select().FirstOrDefault();
                if (sqlManagerResult == null)
                {
                    dataSetManager.Dispose();
                    return false;
                }

                Result = UtilsGeneral.ToInteger(sqlManagerResult["Result"], 0);

                if (Result == 0)
                {
                    dataSetManager.Dispose();
                    return true;
                }
            }
            if (dataSetManager != null) dataSetManager.Dispose();
            return false;
        }

        #endregion Operations

        #region Generated Examples

        /// <summary>
        /// Genereaza exemple.
        /// </summary>
        /// <param name="operationType">Tipul parametrului (ex. inserare tabel, stergere view etc.)</param>
        /// <returns></returns>
        public string GenerateSQLExamples(int operationType)
        {
            StringBuilder sqlString = new StringBuilder();
            switch (operationType)
            {
                case (int)OperationType.CreateTable:
                    return sqlString.Append(@"CREATE TABLE [Inventar](
	                                            [Inventar_ID] [int] IDENTITY(1,1) NOT NULL,
	                                            [Masina_ID] [int] NULL,
	                                            [DataInventar] [datetime] NULL,
	                                            [Persoana_ID] [int] NULL,
	                                            [Filiala_ID] [int] NULL,
	                                            [TipInventar] [tinyint] NULL,
	                                            [NrPV] [varchar](15) NULL,
	                                            [Row_ID] [timestamp] NULL,
                                             CONSTRAINT [PK_Inventar_Pe_Masina] PRIMARY KEY CLUSTERED
                                            (
	                                            [Inventar_ID] ASC
                                            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                                            ) ON [PRIMARY]").ToString();
                case (int)OperationType.InsertColumnInTable:
                    return sqlString.Append(@"ALTER TABLE Inventar
                                              ADD TotalInventar decimal(18,2), BalantaEmisa bit, Observatii varchar(50)").ToString();
                case (int)OperationType.InsertForeignKeyInTable:
                    return sqlString.Append(@"ALTER TABLE Masini_Terti_Detalii
                                              ADD CONSTRAINT fk_MasiniTertiDetaliiFirme FOREIGN KEY (ProprietarTerti_ID) REFERENCES Firme(Firma_ID)").ToString();
                case (int)OperationType.InsertUpdateView:
                    return sqlString.Append(@"ALTER view vwa_CEC_Vizualizare
                                                                as
                                                                select C.CEC_ID ,
                                                                C.Data,
                                                                C.NrCEC,
                                                                NII.Categorie as Tip,
                                                                NDB.Categorie as TipDocument,
                                                                C.Suma,
                                                                LV.Valuta,
                                                                C.SumaInRON,
                                                                C.DataScadenta,
                                                                C.Partener_ID,
                                                                F.FirmaNume as Partener,
                                                                LB.Banca,
                                                                C.IncasatAchitat,
                                                                C.DataIncasareAchitare,
                                                                LC.Contul as ContDebit,
                                                                LC1.Contul as ContCredit,
                                                                C.CursValutar,
                                                                C.GirareCEC,
                                                                F1.FirmaNume as Furnizor,
                                                                LC2.Contul as ContDebitGirare,
                                                                LC3.Contul as ContCreditGirare,
                                                                C.BlocareCEC,
                                                                C.BalantaEmisa,
                                                                FIL.Filiala

                                                                from CEC as C
                                                                left join Nomenclator_Intrare_Iesire_Casa_Banca NII on C.IntrareIesire_ID=NII.ID
                                                                left join Nomenclator_DocBanca_TipDocJustificativ NDB on NDB.ID=C.TipDocument_ID
                                                                left join Lista_Valute LV on C.Valuta_ID=LV.Valuta_ID
                                                                left join Firme F on C.Partener_ID=F.Firma_ID
                                                                left join Lista_Banci LB on C.Banca_ID=LB.Banca_ID
                                                                left join Lista_Conturi LC on C.ContDebit_ID=LC.Cont_ID
                                                                left join Lista_Conturi LC1 on C.ContCredit_ID=LC1.Cont_ID
                                                                left join Firme F1 on C.Partener_ID=F1.Firma_ID
                                                                left join Lista_Conturi LC2 on C.ContDebit_ID=LC2.Cont_ID
                                                                left join Lista_Conturi LC3 on C.ContCredit_ID=LC3.Cont_ID
                                                                left join Filiale FIL on C.Filiala_ID = FIL.Filiala_ID").ToString();
                case (int)OperationType.DeleteView:
                    return sqlString.Append(@"DROP VIEW vwa_Curse_Documente_Predate").ToString();

                case (int)OperationType.InsertUpdateStoreProcedure:
                    return sqlString.Append(@"CREATE PROCEDURE  sp__Delete_Marfuri(
	                                                            @partidaID int,
	                                                            @userID int
                                                            )
                                                            AS

                                                            set nocount on

                                                            --local used
                                                            declare @HasError int, @InPlanificare bit
                                                            select @HasError = 0, @InPlanificare = 1

                                                            if not exists (select Partida_ID from Planificare where Partida_ID = @partidaID)
                                                            begin
	                                                            begin tran
		                                                            ----inserez	LOG
		                                                            insert into Logs ([User_ID], Data, TableName, Master_ID, Field, Field1, Field2, Field3, Field4, Operation)
		                                                            select @userID, getdate(), 'Partide', @partidaID, P.CodPartida, cast(OM.DataPrimiriiOM as varchar(50)), F.FirmaNume, cast(P.TarifFClientPartida as varchar(50)), LV.CodValuta, 1
		                                                            from Partide as P with(nolock)
		                                                            inner join Oferta_Marfa as OM with(nolock) on P.Partida_ID = OM.PartidaOM_ID
		                                                            left join Firme as F with(nolock) on OM.FirmaOM_ID = F.Firma_ID
		                                                            left join Lista_Valute as LV with(nolock) on P.MonedaFClientPartida_ID = LV.Valuta_ID
		                                                            where P.Partida_ID = @partidaID

	                                                            ----sterg Marfa
		                                                            delete from Partide where Partida_ID = @partidaID
		                                                            select @HasError = @@error, @InPlanificare = 0

	                                                            if @HasError <> 0
		                                                            rollback tran
	                                                            else
		                                                            commit tran
                                                            end

                                                            if @HasError > 0
	                                                            select -1 as returnValue
                                                            else if @InPlanificare = 1
		                                                            select 0 as returnValue
	                                                            else
		                                                            select 1 as returnValue").ToString();
                case (int)OperationType.DeleteStoreProcedure:
                    return sqlString.Append(@"DROP PROCEDURE sp__Delete_Marfuri").ToString();

                case (int)OperationType.DropDataFromTable:
                    return sqlString.Append(@"Doar numele tabelei aici: ex: ImportCombustibilCard").ToString();

                case (int)OperationType.ModifyCommonControl:
                    return sqlString.Append(@"UPDATE CommonControls
                                                                  SET
                                                                  [SelectString] = N''
                                                                  [VisibleFieldNames] = N''
                                                                  [Captions] = N''
                                                             WHERE CommonControl_ID = @fieldID").ToString();

                default:
                    return string.Empty;
            }
        }

        public void FixEntitiesTable()
        {
            DataSet mySet = new DataSet();
            mySet.Tables.Add("Entities");

            WindDatabase.LoadDataSet(mySet, new string[] { "Entities" }, new Query(string.Format(@"select * from Entities")));

            var currentRows = mySet.Tables[0].Select();

            for (int i = 0; i < mySet.Tables[0].Rows.Count; i++)
            {
                DataRow row = mySet.Tables[0].Rows[i];
                var entityName = row["EntityName"].ToString();

                entityName = Regex.Replace(entityName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
                var id = (int)row["EntityID"];
                try
                {
                    var returnValue = WindDatabase.ExecuteNonQuery(new Query(string.Format(@"UPDATE Entities  SET EntityName = '" + entityName + "' WHERE EntityId = " + id)));
                }
                catch (Exception ex)
                { System.Windows.Forms.MessageBox.Show(ex.Message); }
            }
        }

        public void FixConturiTable()
        {
            DataSet mySet = new DataSet();
            mySet.Tables.Add("Conturi");

            WindDatabase.LoadDataSet(mySet, new string[] { "Conturi" }, new Query(string.Format(@"select * from Lista_Conturi")));

            var currentRows = mySet.Tables[0].Select();

            for (int i = 0; i < mySet.Tables[0].Rows.Count; i++)
            {
                DataRow row = mySet.Tables[0].Rows[i];
                var entityName = row["Contul"].ToString();

                entityName = Regex.Replace(entityName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
                var id = (int)row["Cont_ID"];
                try
                {
                    var returnValue = WindDatabase.ExecuteNonQuery(new Query(string.Format(@"UPDATE Lista_Conturi  SET Contul = '" + entityName + "' WHERE Cont_ID = " + id)));
                }
                catch (Exception ex)
                { System.Windows.Forms.MessageBox.Show(ex.Message); }
            }
        }

        public bool GenerateAllBarCodes()
        {
            try
            {
                DataSet mySet = new DataSet();
                mySet.Tables.Add("Products");

                WindDatabase.LoadDataSet(mySet, new string[] { "Products" }, new Query(string.Format(@"select Produs_ID, CodBare from Lista_Produse where CodBare IS NULL")));

                var currentRows = mySet.Tables[0].Select();
                Base.BarCode.BarCode barCode;
                StringBuilder xmlString;
                List<object> DbList;
                foreach (var row in currentRows)
                {
                    barCode = new Base.BarCode.BarCode();
                    var prodID = (int)row["Produs_ID"];
                    var codBare = barCode.GenerateBarCode(prodID, Base.BarCode.BarCode.BarCodeType.Product);

                    xmlString = new StringBuilder();
                    xmlString.Append("<root><Lista_Produse ");
                    xmlString.Append(@"CodBare= """ + codBare.ToString() + @"""");
                    xmlString.Append("></Lista_Produse></root>");

                    DbList = new List<object>();
                    DbList.Add(xmlString.ToString());
                    DbList.Add(LoginClass.userID);
                    DbList.Add(prodID);
                    int returnValue = 0;
                    using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__Produse_Cod_Bare_Insert_Update", DbList.ToArray()))
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
                }

                return true;
            }
            catch { return false; }
        }

        public void ViewAllCifs()
        {
            DataSet mySet = new DataSet();
            mySet.Tables.Add("CIFS");

            WindDatabase.LoadDataSet(mySet, new string[] { "CIFS" }, new Query(string.Format(@"select Firma_ID, FirmaNume, FirmaCodFiscal, LT.Tara from Firme AS F left join Lista_Tari AS LT on F.FirmaTara_ID=LT.Tara_ID WHERE FirmaCodFiscalLitere like '%RO%' AND (ISNULL(F.FacturabilaFirma,0) <> 0 AND (LT.PrescurtareTara LIKE 'RO%'))")));

            var currentRows = mySet.Tables[0].Select();

            string listaFirme = string.Empty;

            for (int i = 0; i < mySet.Tables[0].Rows.Count; i++)
            {
                if (!IsCIFValid(mySet.Tables[0].Rows[i]["FirmaCodFiscal"].ToString()))
                    listaFirme += mySet.Tables[0].Rows[i]["FirmaNume"].ToString() + Environment.NewLine;
            }

            if (listaFirme != string.Empty)
            {
                File.WriteAllText(Environment.CurrentDirectory + @"\Firme Cod Fiscal Invalide - WindNet Software.txt", listaFirme);
                ProcessStartInfo processInfo = new ProcessStartInfo("notepad.exe", Environment.CurrentDirectory + @"\Firme Cod Fiscal Invalide - WindNet Software.txt");
                Process.Start(processInfo);
            }
        }

        private bool IsCIFValid(string cif)
        {
            try
            {
                if (cif.ToUpper().Contains("RO"))
                    cif = cif.Replace("RO", "");

                string mask = "753217532";
                var lastChar = cif.Substring(cif.Length - 1, 1);

                int controlDigit = Convert.ToInt32(lastChar);

                string cifCheck = cif.Substring(0, cif.Length - 1);

                if (cifCheck.Length <= 9)
                    cifCheck = cifCheck.PadLeft(9, '0');

                int sum = 0;
                for (int i = 0; i < cifCheck.Length; i++)
                {
                    var currentCheck = Convert.ToInt32(cifCheck.Substring(i, 1));
                    var currentMaskCheck = Convert.ToInt32(mask.Substring(i, 1));
                    sum += currentCheck * currentMaskCheck;
                }

                sum *= 10;

                int moduloSum = sum % 11;

                moduloSum %= 10;

                if (moduloSum == controlDigit) return true;
                return false;
            }
            catch { return false; }
        }

        public void ExecuteInchidereTVA(List<object> dbList)
        {
            try
            {
                Result = 0;
                Result = Base.DataBase.WindDatabase.ExecuteNonQuery("sp__Contabilitate_InchidereTVA", dbList.ToArray());
            }
            catch
            {
                Result = 1;
            }
        }

        public void ExecuteInchidereAn(List<object> dbList)
        {
            try
            {
                Result = 0;
                Result = Base.DataBase.WindDatabase.ExecuteNonQuery("sp__Contabilitate_Inchidere_De_AN", dbList.ToArray());
            }
            catch
            {
                Result = 1;
            }
        }

        public bool AlexStoc(string _fileName)
        {
            try
            {
                DataSet mySet = new DataSet();
                mySet.Tables.Add("Products");

                mySet.Tables.Add("Gestiuni");

                WindDatabase.LoadDataSet(mySet, new string[] { "Gestiuni" }, new Query(string.Format(@"select * from Lista_Gestiuni")));

                FileName = _fileName;
                var MasterSheetName = GetExcelSheetName();
                DataTable dtNew = new DataTable();
                try
                {
                    string connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";");
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + MasterSheetName + "]", connString);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    if (adapter != null) adapter.Dispose();
                    dtNew = ds.Tables[0];
                    if (ds != null) ds.Dispose();
                    if (dtNew == null) return false;
                    dtNew.TableName = MasterSheetName;
                }
                catch
                {

                }


                var results = from row in dtNew.AsEnumerable()
                              select new
                              {
                                  Produs = row[0],
                                  CodProdus = row[1],
                                  Produs_ID = row[2],
                                  PretIntrare = row[3],
                                  Stoc = row[4],
                                  ValoareStoc = row[5],
                                  Gestiunea = row[6]                                  
                              };
                if (results == null)
                {                   
                    return false;
                }

                List<Produs> productList = new List<Produs>();
                foreach (var rowImport in results)
                {
                    try
                    {
                        Produs pro = new Produs();
                        pro.ProdusID = UtilsGeneral.ToInteger(rowImport.Produs_ID, 0);
                        pro.PretUnitar = UtilsGeneral.ToDecimal(rowImport.PretIntrare, 0);
                        pro.Cantitate = UtilsGeneral.ToDecimal(rowImport.Stoc, 0);
                        pro.Gestiunea = rowImport.Gestiunea.ToString();
                        pro.ValoareStoc = UtilsGeneral.ToDecimal(rowImport.ValoareStoc, 0.0M);
                        pro.DenumireProdus = rowImport.Produs.ToString().Replace(@"""","");
                        pro.CodProdus = rowImport.CodProdus.ToString().Replace(@"""", "");
                        pro.GestiuneID = UtilsGeneral.ToInteger(mySet.Tables["Gestiuni"].Select(null, null, DataViewRowState.CurrentRows)
                                              .Where(x => x["Gestiune"].ToString().Trim() == pro.Gestiunea)
                                              .FirstOrDefault()["Gestiune_ID"], 0);

                        var xmlString = GetXmlRowProdus(pro);
                        List<object> DbList = new List<object>();
                        DbList.Add(xmlString);
                        int returnValue = 0;
                        using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__Lista_Produse_Insert", DbList.ToArray()))
                        {
                            if (reader.Read())
                            {
                                returnValue = UtilsGeneral.ToInteger(reader["ReturnValue"], 0);
                            }
                            else
                            {
                                returnValue = 3;
                                return false;
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                return true;
            }
            catch { return false; }
        }
        
        public string FileName { get; set; }

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

        private string GetXmlRowProdus(Produs prod)
        {
            try
            {
                StringBuilder xmlString = new StringBuilder();
                xmlString.Append("<root><Produse ");
                xmlString.Append(@"Produs_ID= """ + prod.ProdusID.ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"DenumireProdus= """ + prod.DenumireProdus.ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"CodProdus= """ + prod.CodProdus.ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"PretUnitar= """ +  prod.PretUnitar.ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Cantitate= """ + prod.Cantitate.ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"ValoareStoc= """ + prod.ValoareStoc.ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Gestiune_ID= """ + prod.GestiuneID.ToString(CultureInfo.InvariantCulture) + @""" ");
                xmlString.Append(@"Gestiunea= """ + prod.Gestiunea.ToString(CultureInfo.InvariantCulture) + @""" ");

                xmlString.Append("></Produse></root>");

                return xmlString.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool AlexBuildStocInitial()
        {
            try
            {
                DataSet mySet = new DataSet();
                mySet.Tables.Add("Lista_Produse_Import");
                mySet.Tables.Add("Stoc");

                WindDatabase.LoadDataSet(mySet, new string[] { "Lista_Produse_Import" }, new Query(string.Format(@"select * from Lista_Produse_Import")));
                WindDatabase.LoadDataSet(mySet, new string[] { "Stoc" }, new Query(string.Format(@"select TOP 1 * from Stoc_Initial_DAN")));

                var currentProduse = mySet.Tables["Lista_Produse_Import"].Select(null, null, DataViewRowState.CurrentRows)
                                                     .ToList();

                var groupOfProducts = currentProduse.GroupBy(x => x["Produs_ID"]);
             
             

                return true;
            }
            catch { return false; }
        }

        public bool FixUserInterfaces()
        {
            try
            {

                var myCollection = Enum.GetNames(typeof(UserInterfaceType));
                Dictionary<int, string> dict = new Dictionary<int, string>();
                foreach (var xName in myCollection)
                {
                    var findValue = (int)((UserInterfaceType)Enum.Parse(typeof(UserInterfaceType), xName));
                    var charArray = xName.Replace("_", " ").ToCharArray().ToList();
                    string member = "";

                    for (int i = 0; i < charArray.Count; i++)
                    {
                        if (i != 0 && char.IsLetter(charArray[i]) && char.IsUpper(charArray[i]))
                            member += " " + char.ToLower(charArray[i]);                        
                        else member += charArray[i];
                    }

                    dict.Add(findValue, member);                    
                }             

                var result = WindDatabase.ExecuteNonQuery(new Query(string.Format(@"delete from WindnetUserInterfaces")));
                if (result != -1)
                {
                    foreach (var key in dict.Keys)
                    {
                        if (key == 0) continue;

                        var insertResult = WindDatabase.ExecuteNonQuery(string.Format(@"INSERT INTO WindnetUserInterfaces (UserInterfaceTypeID, Name) VALUES ({0}, '{1}')",
                                                                key, dict[key]));
                        if (insertResult != 1)
                            return false;
                    }
                }               

                return true;
            }
            catch { return false; }
        }

        public void ExecuteInchidereLuna(List<object> dbList)
        {
            try
            {
                Result = 0;
                Result = Base.DataBase.WindDatabase.ExecuteNonQuery("sp_Contabilitate_InchidereLuna", dbList.ToArray());
            }
            catch
            {
                Result = 1;
            }
        }

        public static bool? VerificaDiferenteNoteStaticeVsNoteDinamice(List<object> dbList)
        {
            try
            {
                return UtilsGeneral.ToInteger(Base.DataBase.WindDatabase.ExecuteScalar("sp_DiferenteNoteStaticeVsNoteDinamice", dbList.ToArray()), 0) > 0 ? true : false;
            }
            catch
            {
                return null;
            }
        }

        public bool LunaInchisaYN(int luna, int anul)
        {
            bool result;
            try
            {
               result = (UtilsGeneral.ToInteger(Base.DataBase.WindDatabase.ExecuteScalar(String.Format("SELECT count(*) FROM InchideriLuna WHERE Anul={0} AND Luna = {1}",anul,luna)),0)>0)?true:false;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        #endregion Generated Examples
    }

    public class Produs
    {
        public int ProdusID { get; set; }
        public string DenumireProdus { get; set; }
        public string CodProdus { get; set; }
        public int GestiuneID { get; set; }
        public decimal PretUnitar { get; set; }
        public decimal Cantitate { get; set; }
        public decimal ValoareStoc { get; set; }
        public string Gestiunea { get; set; }
    }
}