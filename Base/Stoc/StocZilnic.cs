using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Base.BaseUtils;
using Base.Configuration;
using Base.DataBase;
using Base.FreezeTables;

namespace Base.Stoc
{
    public class StocZilnic
    {
        #region Properties

        public DataSet DataSetFromDB
        {
            get;
            set;
        }

        public DataSet DataSetCurrent
        {
            get;
            set;
        }

        /// <summary>
        /// Toate produsele aflate pe stoc.
        /// </summary>
        private StocObject[] StocObjectsFromDB
        {
            get;
            set;
        }

        /// <summary>
        /// Produsele asupra carora se exercita actiunea de inserare/modificare/stergere.
        /// </summary>
        private StocObject[] StocObjectsCurrent
        {
            get;
            set;
        }

        /// <summary>
        /// DataRowObject - folosit pentru alcatuirea stringului XML la salvarea in BD.
        /// </summary>
        private DataRow DataRowObject
        {
            get;
            set;
        }

        /// <summary>
        /// Colectie cu toate produsele din stoc ce au suferit modificari (insert, update, delete).
        /// </summary>
        private ArrayList StocuriFinaleDB
        {
            get;
            set;
        }

        /// <summary>
        /// Error = Variabila in care pastrez starea erorii -> ex: eroare citire date, stoc negativ etc.
        /// In caz de eroare la actualizarea/refacerea stocului aici pastrez eroarea.
        /// </summary>
        public string Error
        {
            get;
            set;
        }

        /// <summary>
        /// StocuriNegative - in aceasta variabila pastrez toate produsele care
        /// nu au putut fi actualizate (ex. -> cantitate negativa dupa stergere).
        /// </summary>
        public ArrayList StcouriNegative
        {
            get;
            set;
        }
        /// <summary>
        /// StocuriNeidentificate - in aceasta variabila pastrez toate produsele care
        /// nu au putut fi gasite in stoc.
        /// </summary>
        public ArrayList StocuriNeidentificate
        {
            get;
            set;
        }

        public DateTime DocumentDate
        {
            get;
            set;
        }      

        /// <summary>
        /// Aici pastram stocurile stornate
        /// </summary>
        private StocObject[] StocuriStorno
        { get; set; }

        #endregion Properties

        #region Enums

        /// <summary>
        /// Tipul de operatiune asupra stocului: 0 = Insert/Update; 1 = Delete
        /// </summary>
        public enum TipOperatiuneStoc : int
        {
            InsertUpdate = 0,
            Delete = 1,
            InsertUpdate_OnDelete_Bon_Aviz = 2
        }

        #endregion Enums

        #region Constructors

        /// <summary>
        /// Constructor implicit.
        /// </summary>
        public StocZilnic()
        {
        }

        /// <summary>
        /// Constructor cu parametru.
        /// </summary>
        /// <param name="currentDataSet">Dataset-ul cu toate produsele ce urmeaza sa fie adaugate/modificate/scazute din stoc.</param>
        public StocZilnic(DataSet currentDataSet, DateTime currentDate)
        {
            InitValues(currentDataSet);
            LoadStoc();
            BuildDBObjects();
            DocumentDate = currentDate;
        }

        #endregion Constructors

        #region Init

        /// <summary>
        /// Metoda apelata din constructorul cu parametru. Folosit pentru a seta variabilele default.
        /// </summary>
        /// <param name="currentDataSet"></param>
        private void InitValues(DataSet currentDataSet)
        {
            DataSetCurrent = new DataSet();
            DataSetCurrent.Tables.Add("Stoc_Zilnic");
            DataSetCurrent = currentDataSet;
            StocuriFinaleDB = new ArrayList();
            StcouriNegative = new ArrayList();
            StocuriNeidentificate = new ArrayList();
            DataRowObject = null;
            Error = string.Empty;
        }

        #endregion Init

        #region Load Operations

        /// <summary>
        /// Metoda folosita pentru a incarca stocul din baza de date in DataSetFromDB.
        /// </summary>
        private void LoadStoc()
        {
            try
            {
                DataSetFromDB = new DataSet();
                DataSetFromDB.Tables.Add("Stoc_Zilnic");
                WindDatabase.LoadDataSet(DataSetFromDB, new string[] { "Stoc_Zilnic" }, "sp__Stoc_Zilnic_Load",
                    new object[3] { null, null, null });
 
                    if (FreezeUtil.IsTableLocked("Stoc_Zilnic")) // table is freezed
                        Error = "Momentan nu se pot executa operatii asupra stocului!" +
                                " Utilizatorul: " + FreezeUtil.UserName + " face modificari asupra stocului." +
                                " Data: " + FreezeUtil.FreezeTime.Value.Date.ToString("dd-MM-yyyy") + " Ora: " + FreezeUtil.FreezeTime.Value.ToString("HH:mm") + "." +
                                " Observatii: " + FreezeUtil.Observations;
                    else if (!FreezeUtil.LockUnlockTable("Stoc_Zilnic", FreezeUtil.FreezeMode.Freeze, DateTime.Now, "Actualizare stocuri."))
                        Error = "Momentan nu se pot executa operatii asupra stocului." + " Va rugam incercati din nou.";
            }
            catch
            {
                Error = "Eroare la citirea datelor din stoc.";
                if (DataSetFromDB != null) DataSetFromDB.Dispose();
            }
        }



        /// <summary>
        /// Metoda folosita pentru a incarca stocul din baza de date in DataSetFromDB inclusiv cantitatile egale cu 0.
        /// </summary>
        private void LoadStocPlusZero()
        {
            try
            {
                DataSetFromDB = new DataSet();
                DataSetFromDB.Tables.Add("Stoc_Zilnic");
                WindDatabase.LoadDataSet(DataSetFromDB, new string[] { "Stoc_Zilnic" }, "sp__Stoc_Zilnic_IncludeZero_Load",
                    new object[3] { null, null, null });
            }
            catch
            {
                Error = "Eroare la citirea datelor din stoc.";
                if (DataSetFromDB != null) DataSetFromDB.Dispose();
            }
        }

        /// <summary>
        /// Metoda folosita pentru a incarca obiectele de tip stoc. 
        /// Metoda incarca atat obiectele din baza de date cat si obiectele noi aduse.
        /// </summary>
        private void BuildDBObjects()
        {
            try
            {
                StocObjectsFromDB = new StocObject[DataSetFromDB.Tables["Stoc_Zilnic"].Rows.Count];
                int idx = 0;

                foreach (DataRow row in DataSetFromDB.Tables["Stoc_Zilnic"].Rows)
                {
                    StocObject stocObject = new StocObject();
                    stocObject.StocID = (int)row["Stoc_ID"]; // nu se poate sa avem aici Stoc_ID NULL
                    stocObject.GestiuneID = row["Gestiune_ID"] == DBNull.Value ? 0 : (int)row["Gestiune_ID"];
                    stocObject.ProdusID = row["Produs_ID"] == DBNull.Value ? 0 : (int)row["Produs_ID"];
                    stocObject.FilialaID = row["Filiala_ID"] == DBNull.Value ? 0 : (int)row["Filiala_ID"];
                    stocObject.PretUnitar = row["PretUnitar"] == DBNull.Value ? 0.0M : (decimal)row["PretUnitar"];
                    stocObject.Cantitate = row["Cantitate"] == DBNull.Value ? 0.0M : (decimal)row["Cantitate"];
                    stocObject.DataStoc = row["DataStoc"] == DBNull.Value ? DateTime.Now.Date : (DateTime)row["DataStoc"];
                    stocObject.ComandaClientID = row["ComandaClient_ID"] == DBNull.Value ? null : (int?)row["ComandaClient_ID"];
                    //stocObject.CodProdusEchivalentID = row["CodProdusEchivalent_ID"] == DBNull.Value ? null : (int?)row["CodProdusEchivalent_ID"];

                    stocObject.Modificat = false;
                    StocObjectsFromDB[idx] = stocObject;
                    idx++;
                }

                StocObjectsCurrent = new StocObject[DataSetCurrent.Tables["Stoc_Zilnic"].Rows.Count];

                if (DataRowObject == null) DataRowObject = DataSetCurrent.Tables["Stoc_Zilnic"].Rows[0];
                idx = 0;

                foreach (DataRow row in DataSetCurrent.Tables["Stoc_Zilnic"].Rows)
                {
                    StocObject stocObject = new StocObject();

                    stocObject.StocID = (int)row["Stoc_ID"]; // automat vine pe 0 => conversia are loc safe
                    stocObject.GestiuneID = row["Gestiune_ID"] == DBNull.Value ? 0 : (int)row["Gestiune_ID"];
                    stocObject.ProdusID = row["Produs_ID"] == DBNull.Value ? 0 : (int)row["Produs_ID"];
                    stocObject.FilialaID = row["Filiala_ID"] == DBNull.Value ? 0 : (int)row["Filiala_ID"];
                    stocObject.PretUnitar = row["PretUnitar"] == DBNull.Value ? 0.0M : (decimal)row["PretUnitar"];
                    stocObject.Cantitate = row["Cantitate"] == DBNull.Value ? 0.0M : (decimal)row["Cantitate"];
                    stocObject.DataStoc = row["DataStoc"] == DBNull.Value ? DateTime.Now.Date : (DateTime)row["DataStoc"];
                    stocObject.ComandaClientID = row["ComandaClient_ID"] == DBNull.Value ? null : (int?)row["ComandaClient_ID"];
                    //stocObject.CodProdusEchivalentID = row["CodProdusEchivalent_ID"] == DBNull.Value ? null : (int?)row["CodProdusEchivalent_ID"];
                    stocObject.Modificat = true;
                    StocObjectsCurrent[idx] = stocObject;
                    idx++;
                }
            }
            catch
            {
                Error = "Stocul a fost modificat intre timp de catre alt utilizator.";
            }
        }

        #endregion Load Operations

        #region Execution Operations

        /// <summary>
        /// Executa diferite operatii asupra stocului
        /// </summary>
        /// <param name="tipOperatie">tipOperatie = InsertUpdate - inserare / modificare din stoc folosit de NIR;
        /// Delete - stergere din stoc folosit in Bon Consum/Transfer si alte documente ce scad stocul
        /// </param>
        /// <returns></returns>
        public bool ExecuteStoc(TipOperatiuneStoc tipOperatie)
        {
            if (Error != string.Empty)
                return false;

            if (DataSetCurrent == null || DataSetFromDB == null)
            {
                Error = "Eroare la citirea datelor din stoc.";
                return false;
            }

            if (StocObjectsCurrent.Any(x => x.Cantitate < 0))
            {
                var noOfStornoObjects = StocObjectsCurrent.Count(y => y.Cantitate < 0);                
                StocuriStorno = new StocObject[noOfStornoObjects];

                var oldCollectionOfStorno = StocObjectsCurrent.Where(z => z.Cantitate > 0).ToList();

                var newCollectionOfStorno = StocObjectsCurrent.Where(z => z.Cantitate < 0).ToList();

                var idx = 0;
                foreach (var storno in newCollectionOfStorno)
                {
                    storno.Cantitate *= -1;
                    StocuriStorno[idx] = storno;
                    idx++;                    
                }

                if (oldCollectionOfStorno != null && oldCollectionOfStorno.Count() > 0)
                {
                    StocObjectsCurrent = new StocObject[oldCollectionOfStorno.Count()];
                    idx = 0;
                    foreach (var stoc in oldCollectionOfStorno)
                    {
                        StocObjectsCurrent[idx] = stoc;
                        idx++;
                    }
                }
                else StocObjectsCurrent = null;
            }

            var stornoResult = false;
            // verificam daca avem storno pe stocuri
            if (StocuriStorno != null && StocuriStorno.Count() > 0)
            {
                switch (tipOperatie) // operatia devine inversa la storno => insert = scadere stoc / delete = aduagare stoc
                {
                    case TipOperatiuneStoc.InsertUpdate:
                         stornoResult = DeleteStorno();
                         break;
                    case TipOperatiuneStoc.Delete:
                         stornoResult = InsertUpdateStorno();
                         break;
                    
                }
            }

            if (!stornoResult && StocuriStorno != null && StocuriStorno.Count() > 0)
                return false;

            if (StocObjectsCurrent == null || StocObjectsCurrent.Count() == 0)
                return Error == string.Empty; // toate obiectele erau de tip storno!
            

            switch (tipOperatie)
            {
                case TipOperatiuneStoc.InsertUpdate:
                    return InsertUpdateStoc();
                case TipOperatiuneStoc.Delete:
                    return DeleteStoc();
                case TipOperatiuneStoc.InsertUpdate_OnDelete_Bon_Aviz:
                    return InsertUpdateStocFromOtherDocuments();
                default:
                    return false;
            }
        }


        #endregion Execution Operations

        #region Insert / Update Operations

        /// <summary>
        /// Metoda folosita pentru inserarea / modificarea si actualizarea stocului.
        /// </summary>
        /// <returns></returns>
        private bool InsertUpdateStoc()
        {

            // verific daca am produse in stocul existent din DB => StocObjectsFromDB
            if (StocObjectsFromDB.Count() == 0) // stocul este gol
            {
                UpdateCantitati(StocObjectsCurrent);
                ExecuteSaveDB();
                if (Error != string.Empty) return false;
            }
            else
            {
                UpdateCantitati(StocObjectsCurrent);
                SyncronizeStocuriDB(); // sincronizare stocuri vechi cu stocuri noi aduse
                ExecuteSaveDB();
                if (Error != string.Empty) return false;
            }


            return true;
        }

        private bool InsertUpdateStocFromOtherDocuments()
        {
            // Load Stoc again
            LoadStocPlusZero();


            // verific daca am produse in stocul existent din DB => StocObjectsFromDB
            if (StocObjectsFromDB.Count() == 0) // stocul este gol -> nu ma intereseaza Data la care se face inserarea STOCULUI
            {
                UpdateCantitati(StocObjectsCurrent);
                ExecuteSaveDB();
                if (Error != string.Empty) return false;
            }
            else
            {
                UpdateCantitatiFromOtherDocuments(StocObjectsCurrent);
                SyncronizeStocuriDBFromDeletedDocuments(); // sincronizare stocuri vechi cu stocuri noi aduse aici gasim si ultima data
                ExecuteSaveDB();
                if (Error != string.Empty) return false;
            }


            return true;
        }


        /// <summary>
        /// Metoda folosita pentru actualizarea stocurilor storno.
        /// </summary>
        /// <returns></returns>
        private bool InsertUpdateStorno()
        {
            // Load Stoc again
            LoadStocPlusZero();


            // verific daca am produse in stocul existent din DB => StocObjectsFromDB
            if (StocObjectsFromDB.Count() == 0) // stocul este gol -> nu ma intereseaza Data la care se face inserarea STOCULUI
            {
                UpdateCantitati(StocuriStorno); 
                ExecuteSaveDB();
                StocuriFinaleDB = new ArrayList(); // reinitializare
                if (Error != string.Empty) return false;
            }
            else
            {
                UpdateCantitatiFromOtherDocuments(StocuriStorno); 
                SyncronizeStocuriDBFromDeletedDocuments(); // sincronizare stocuri vechi cu stocuri noi aduse aici gasim si ultima data
                ExecuteSaveDB();
                StocuriFinaleDB = new ArrayList(); // reinitializare
                if (Error != string.Empty) return false;
            }


            return true;
        }


        /// <summary>
        /// Metoda folosita pentru actualizarea cantitatilor.
        /// </summary>
        /// <param name="stocObjects"></param>
        private void UpdateCantitati(StocObject[] stocObjects)
        {
            var groupOfObjects = from StocObject stocN in stocObjects
                                 orderby stocN.DataStoc
                                 group stocN by new { stocN.GestiuneID, stocN.ProdusID, stocN.FilialaID, stocN.PretUnitar, stocN.ComandaClientID }
                                     //, stocN.CodProdusEchivalentID }
                                     into stocGroup
                                     select stocGroup;

            foreach (var groupObject in groupOfObjects)
            {

                var xList = from StocObject rowC in groupObject
                            orderby rowC.DataStoc
                            group rowC by rowC.DataStoc.Date into newGroup
                            select newGroup;

                foreach (var xObject in xList)
                {
                    if (xObject.Count() > 1)
                    {
                        var cantitateNoua = xObject.Sum(x => x.Cantitate);
                        foreach (StocObject stocModificat in xObject)
                            stocModificat.Cantitate = cantitateNoua;
                    }
                }

                for (int i = 0; i < groupObject.Count(); i++)
                {
                    var newObject = groupObject.ElementAt(i);
                    if (i == 0)
                    {
                        StocuriFinaleDB.Add(newObject);
                        continue;
                    }
                    if (!StocuriFinaleDB.Contains(newObject))
                    {
                        newObject.Cantitate += groupObject.ElementAt(i - 1).Cantitate;
                        StocuriFinaleDB.Add(newObject);
                    }
                }
            }

            StocuriFinaleDB.Sort();
        }

        /// <summary>
        /// Metoda folosita pentru actualizarea cantitatilor din stergerea de documente doar (Bon Consum / Transfer / Aviz.
        /// </summary>
        /// <param name="stocObjects"></param>
        private void UpdateCantitatiFromOtherDocuments(StocObject[] stocObjects)
        {
            var groupOfObjects = from StocObject stocN in stocObjects
                                 orderby stocN.DataStoc
                                 group stocN by new { stocN.GestiuneID, stocN.ProdusID, stocN.FilialaID, stocN.PretUnitar, stocN.ComandaClientID, stocN.CodProdusEchivalentID }
                                     into stocGroup
                                     select stocGroup;

            foreach (var groupObject in groupOfObjects)
            {

                var xList = from StocObject rowC in groupObject
                            orderby rowC.DataStoc
                            group rowC by rowC.DataStoc.Date into newGroup
                            select newGroup;

                foreach (var xObject in xList)
                {
                    if (xObject.Count() > 1)
                    {
                        var cantitateNoua = xObject.Sum(x => x.Cantitate);
                        foreach (StocObject stocModificat in xObject)
                            stocModificat.Cantitate = cantitateNoua;
                    }
                }

                for (int i = 0; i < groupObject.Count(); i++)
                {
                    var newObject = groupObject.ElementAt(i);
                    if (i == 0)
                    {
                        StocuriFinaleDB.Add(newObject);
                        continue;
                    }
                    if (!StocuriFinaleDB.Contains(newObject))
                    {
                        newObject.Cantitate += groupObject.ElementAt(i - 1).Cantitate;
                        StocuriFinaleDB.Add(newObject);
                    }
                }
            }

            StocuriFinaleDB.Sort();
        }


        #endregion Insert / Update Operations

        #region Delete Operations

        /// <summary>
        /// Metoda folosita pentru stergerea si actualizarea stocului.
        /// </summary>
        /// <returns></returns>
        private bool DeleteStoc()
        {

            foreach (StocObject stocObject2Delete in StocObjectsCurrent)
            {
                StocObject ultimulStocDisponibil = StocObjectsFromDB.Where(x => x.IsEqual(x, stocObject2Delete) &&
                                    x.DataStoc.Date <= stocObject2Delete.DataStoc.Date).LastOrDefault();

                if (ultimulStocDisponibil == null)
                {
                    StocuriNeidentificate.Add(stocObject2Delete);
                    // unfreeze stoc
                    FreezeUtil.LockUnlockTable("Stoc_Zilnic", FreezeUtil.FreezeMode.UnFreeze, null, string.Empty);
                    return false;
                }

                var cantitate = stocObject2Delete.Cantitate; // scad cantitatea
                if (cantitate > ultimulStocDisponibil.Cantitate)
                {
                    Error = "Cantitatea selectata pentru stergere este mai mare decat cea disponibila." +
                            "Va rugam selectati o cantitate mai mica sau egala decat: " +
                            ultimulStocDisponibil.Cantitate + " pentru data: " + ultimulStocDisponibil.DataStoc.Date;

                    StcouriNegative.Add(stocObject2Delete);
                    // unfreeze stoc
                    FreezeUtil.LockUnlockTable("Stoc_Zilnic", FreezeUtil.FreezeMode.UnFreeze, null, string.Empty);
                    return false;
                }

                var goldenNumber = ultimulStocDisponibil.Cantitate - cantitate;

                IEnumerable<StocObject> stocuriDeModificat = StocObjectsFromDB.Where(x => x.IsEqual(x, stocObject2Delete));
                IEnumerable<StocObject> stocuriDupa = stocuriDeModificat.SkipWhile(x => x != ultimulStocDisponibil);
                IEnumerable<StocObject> stocuriInainte = stocuriDeModificat.Except(stocuriDupa).Where(x => x.Cantitate > goldenNumber);
                foreach (StocObject stoc in stocuriDupa)
                {
                    stoc.Cantitate -= cantitate;
                    stoc.Modificat = true;
                    StocuriFinaleDB.Add(stoc);
                }


                foreach (StocObject stoc in stocuriInainte)
                {
                    stoc.Cantitate = goldenNumber;
                    stoc.Modificat = true;
                    StocuriFinaleDB.Add(stoc);
                }

            }
            StocuriFinaleDB.Sort();

            ExecuteSaveDB();
            if (Error != string.Empty) return false;
            return true;
        }



        /// <summary>
        /// Metoda folosita pentru STORNAREA si actualizarea stocului.
        /// </summary>
        /// <returns></returns>
        private bool DeleteStorno()
        {

            foreach (StocObject stocObject2Delete in StocuriStorno)
            {
                StocObject ultimulStocDisponibil = StocObjectsFromDB.Where(x => x.IsEqual(x, stocObject2Delete) &&
                                    x.DataStoc.Date <= stocObject2Delete.DataStoc.Date).LastOrDefault();

                if (ultimulStocDisponibil == null)
                {
                    StocuriNeidentificate.Add(stocObject2Delete);
                    return false;
                }

                var cantitate = stocObject2Delete.Cantitate; // scad cantitatea
                if (cantitate > ultimulStocDisponibil.Cantitate)
                {
                    Error = "Cantitatea selectata pentru stergere este mai mare decat cea disponibila." +
                            "Va rugam selectati o cantitate mai mica sau egala decat: " +
                            ultimulStocDisponibil.Cantitate + " pentru data: " + ultimulStocDisponibil.DataStoc.Date;

                    StcouriNegative.Add(stocObject2Delete);
                    return false;
                }

                var goldenNumber = ultimulStocDisponibil.Cantitate - cantitate;

                IEnumerable<StocObject> stocuriDeModificat = StocObjectsFromDB.Where(x => x.IsEqual(x, stocObject2Delete));
                IEnumerable<StocObject> stocuriDupa = stocuriDeModificat.SkipWhile(x => x != ultimulStocDisponibil);
                IEnumerable<StocObject> stocuriInainte = stocuriDeModificat.Except(stocuriDupa).Where(x => x.Cantitate > goldenNumber);
                foreach (StocObject stoc in stocuriDupa)
                {
                    stoc.Cantitate -= cantitate;
                    stoc.Modificat = true;
                    StocuriFinaleDB.Add(stoc);
                }


                foreach (StocObject stoc in stocuriInainte)
                {
                    stoc.Cantitate = goldenNumber;
                    stoc.Modificat = true;
                    StocuriFinaleDB.Add(stoc);
                }

            }
            StocuriFinaleDB.Sort();

            ExecuteSaveDB();
            StocuriFinaleDB = new ArrayList(); // reinitializare
            if (Error != string.Empty) return false;
            return true;
        }


        #endregion Delete Operations


        #region Syncronize Operations

        /// <summary>
        /// Metoda folosita pentru sincronizarea tuturor cantitatilor din stoc.
        /// De asemenea se definesc aici doar stocurile finale care vor fi salvate in baza de date.
        /// </summary>
        private void SyncronizeStocuriDB()
        {
            var stocGroups = from StocObject stocC in StocuriFinaleDB
                             group stocC by new
                             {
                                 stocC.ProdusID,
                                 stocC.FilialaID,
                                 stocC.GestiuneID,
                                 stocC.PretUnitar,
                                 stocC.ComandaClientID
                                 //stocC.CodProdusEchivalentID
                             } into stocGrupat
                             select stocGrupat;

            foreach (var groupOfObjects in stocGroups)
            {
                foreach (StocObject stocObj in groupOfObjects)
                {
                    var stocuriComune = StocObjectsFromDB.Where(x => x.IsEqual(x, stocObj));
                    StocObject ultimulStocGasit = new StocObject();
                    bool foundExactDate = false;
                    if (stocuriComune.Count() > 0)
                    {
                        foreach (var stocGasit in stocuriComune)
                        {
                            if (stocObj.Modificat)
                            {
                                if (stocGasit.DataStoc.Date < stocObj.DataStoc.Date)
                                {
                                    ultimulStocGasit = stocGasit;
                                    continue;
                                }
                                else if (stocGasit.DataStoc.Date >= stocObj.DataStoc.Date)
                                {
                                    stocGasit.Cantitate += stocObj.Cantitate;
                                    stocGasit.Modificat = true;
                                    if (groupOfObjects.Count() > 1) stocObj.Modificat = false;
                                    if (stocGasit.DataStoc.Date == stocObj.DataStoc.Date)
                                    {
                                        foundExactDate = true;
                                        //stocObj.Modificat = false;                                
                                    }
                                    continue;
                                }
                            }
                        }

                        if (!foundExactDate && stocObj.Cantitate > 0)
                        {
                            stocObj.Cantitate += ultimulStocGasit.Cantitate;
                            stocObj.Modificat = true;
                        }
                        else if (!foundExactDate && stocObj.Cantitate < 0) stocObj.Modificat = false;
                        else if (foundExactDate)
                        {
                            stocObj.Modificat = false;
                        }

                    }
                }
            }

            // sincronizam BD-ul si sortam              
            StocuriFinaleDB.AddRange(StocObjectsFromDB);
            StocuriFinaleDB.Sort();
        }

        /// <summary>
        /// Metoda folosita pentru sincronizarea tuturor cantitatilor din stoc.
        /// Metoda este folosita doar in cazul stergerii unui document cu totul - Bon / Aviz.
        /// De asemenea se definesc aici doar stocurile finale care vor fi salvate in baza de date.
        /// </summary>
        private void SyncronizeStocuriDBFromDeletedDocuments()
        {
            var stocGroups = from StocObject stocC in StocuriFinaleDB
                             group stocC by new
                             {
                                 stocC.ProdusID,
                                 stocC.FilialaID,
                                 stocC.GestiuneID,
                                 stocC.PretUnitar,
                                 stocC.ComandaClientID
                                 //stocC.CodProdusEchivalentID
                             } into stocGrupat
                             select stocGrupat;

            foreach (var groupOfObjects in stocGroups)
            {
                foreach (StocObject stocObj in groupOfObjects)
                {
                    var stocuriComune = StocObjectsFromDB.Where(x => x.IsEqual(x, stocObj));
                    StocObject ultimulStocGasit = new StocObject();
                    bool foundExactDate = false;
                    if (stocuriComune.Count() > 0)
                    {
                        foreach (var stocGasit in stocuriComune)
                        {
                            if (stocObj.Modificat)
                            {
                                if (stocGasit.DataStoc.Date < stocObj.DataStoc.Date)
                                {
                                    ultimulStocGasit = stocGasit;
                                    continue;
                                }
                                else if (stocGasit.DataStoc.Date >= stocObj.DataStoc.Date)
                                {
                                    stocGasit.Cantitate += stocObj.Cantitate;
                                    stocGasit.Modificat = true;
                                    if (groupOfObjects.Count() > 1) stocObj.Modificat = false;
                                    if (stocGasit.DataStoc.Date == stocObj.DataStoc.Date)
                                    {
                                        foundExactDate = true;
                                        //stocObj.Modificat = false;                                
                                    }
                                    continue;
                                }
                            }
                        }

                        if (!foundExactDate && stocObj.Cantitate > 0)
                        {
                            stocObj.Cantitate += ultimulStocGasit.Cantitate;
                            stocObj.DataStoc = ultimulStocGasit.DataStoc; // setam ultimul ID de stoc
                            stocObj.StocID = ultimulStocGasit.StocID; // setam ultimul ID de stoc
                            stocObj.Modificat = true;
                        }
                        else if (!foundExactDate && stocObj.Cantitate < 0) stocObj.Modificat = false;
                        else if (foundExactDate)
                        {
                            stocObj.Modificat = false;
                        }

                    }
                }
            }

            // sincronizam BD-ul si sortam              
            StocuriFinaleDB.AddRange(StocObjectsFromDB);
            StocuriFinaleDB.Sort();
        }



        #endregion Syncronize Operations


        #region Save DB Operations

        /// <summary>
        /// Metoda salveaza ultimile modificari in baza de date.
        /// </summary>
        private void ExecuteSaveDB()
        {
            if (StocuriFinaleDB.Count != 0)
            {
                try
                {

                    //var stocuriNegative = (from StocObject stocNeg in StocuriFinaleDB
                    //                   where stocNeg.Cantitate < 0 && stocNeg.Modificat == true
                    //                  select stocNeg);

                    var stocuriNegative = StocuriFinaleDB.ToArray().Where(x => ((StocObject)x).Cantitate < 0 && ((StocObject)x).Modificat == true);

                    if (stocuriNegative != null && stocuriNegative.Count() != 0)
                    {
                        Error = "Eroare. A fost depistata o cantitate negativa." +
                                Environment.NewLine + "Operatiunea a fost anulata.";

                        // unfreeze stoc
                        FreezeUtil.LockUnlockTable("Stoc_Zilnic", FreezeUtil.FreezeMode.UnFreeze, null, string.Empty);
                        foreach (StocObject stocGasitNegativ in stocuriNegative)
                            StcouriNegative.Add(stocGasitNegativ);
                        return;
                    }

                    foreach (StocObject stocObj in StocuriFinaleDB)
                    {
                        if (stocObj.Modificat)
                        {

                            FillDataRowObject(stocObj); // fill data row with stoc object

                            System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
                            xmlString.Append("<root><Stoc_Zilnic ");
                            xmlString.Append(UtilsXML.RowToXML(DataRowObject, false));
                            xmlString.Append("></Stoc_Zilnic></root>");

                            List<object> DbList = new List<object>();

                            DbList.Add(xmlString.ToString());
                            DbList.Add(LoginClass.userID);
                            DbList.Add(stocObj.StocID);
                            // start saving...
                            int returnValue = 0;

                            using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp__Stoc_Zilnic_Insert_Update", DbList.ToArray()))
                            {
                                if (reader.Read())
                                {
                                    returnValue = UtilsGeneral.ToInteger(reader["ReturnValue"], 0);
                                }
                                else returnValue = 3;
                            }
                            DbList.Clear();

                            if (returnValue == 1)
                            {
                                Error = Constants.C_CONCURRENCY_UPDATE_FAILED;
                            }
                            else if (returnValue == 2)
                            {
                                Error = Constants.C_CONCURRENCY_UPDATE_FAILED_RECORD_DELETED;
                            }
                            else if (returnValue == 3) //eroare in procedura stocata
                                Error = "A aparut o eroare la salvare. Operatiune esuata !";
                        } // end if modificat
                    } // end foreach
                } // end try
                catch
                {
                    Error = "Eroare la salvarea stocurilor in baza de date.";
                }
                finally
                {
                    FreezeUtil.LockUnlockTable("Stoc_Zilnic", FreezeUtil.FreezeMode.UnFreeze, null, string.Empty);
                }
            }
        }

        /// <summary>
        /// Metoda folosita pentru construirea obiectului DataRowObject.
        /// Utilitate: construirea XMLString-ului.
        /// </summary>
        /// <param name="stocObj"></param>
        private void FillDataRowObject(StocObject stocObj)
        {
            DataRowObject["Stoc_ID"] = stocObj.StocID;
            DataRowObject["Produs_ID"] = stocObj.ProdusID;
            DataRowObject["Gestiune_ID"] = stocObj.GestiuneID;
            DataRowObject["DataStoc"] = UtilsGeneral.ToDateTime(stocObj.DataStoc) == DateTime.MinValue ? DocumentDate.Date : stocObj.DataStoc.Date;
            DataRowObject["Cantitate"] = stocObj.Cantitate;
            DataRowObject["PretUnitar"] = stocObj.PretUnitar;
            DataRowObject["Filiala_ID"] = stocObj.FilialaID;
            if (stocObj.ComandaClientID == null) DataRowObject["ComandaClient_ID"] = DBNull.Value;//System.Data.SqlTypes.SqlInt32.Null;
            else DataRowObject["ComandaClient_ID"] = stocObj.ComandaClientID;

            //if (stocObj.CodProdusEchivalentID == null) DataRowObject["CodProdusEchivalent_ID"] = DBNull.Value;//System.Data.SqlTypes.SqlInt32.Null;
            //else DataRowObject["CodProdusEchivalent_ID"] = stocObj.CodProdusEchivalentID;
        }

        #endregion Save DB Operations
    }
}