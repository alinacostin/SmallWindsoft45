using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

using Base.DataBase;
using Base.Configuration;
using Base.Impersonator;

namespace Base.Attatchments
{
    public class Attatchments
    {
        // test share
        public enum AttatchmentType : byte
        {
            Firme = 0,
            Angajati = 1,
            Daune = 2,
            FirmeMarketing = 3,
            Oferte = 4,
            MasiniPropriiDetalii = 5
        }
        public enum FileType : byte
        {
            Undefined = 0,
            DOC = 1,
            XLS = 2,
            PDF = 3,
            JPG = 4,
            ZIP = 5,
            RAR = 6,
            JPEG = 7,
            BMP = 8,
            PNG = 9,
            GIF = 10,
            SFX = 11,
            DOCX = 12,
            XLSX = 13,
            PPTX = 14,
            PPT = 15

        }

        public static String FileServer;
        public static String UserName;
        public static String Password;

        private static FileType GetFileType(String fileName)
        {
            String extension = fileName.Substring(fileName.LastIndexOf(".") + 1);
            switch (extension.ToLower())
            {
                case "doc":
                    return FileType.DOC;
                case "xls":
                    return FileType.XLS;
                case "pdf":
                    return FileType.PDF;
                case "jpg":
                    return FileType.JPG;
                case "zip":
                    return FileType.ZIP;
                case "rar":
                    return FileType.RAR;
                case "jpeg":
                    return FileType.JPEG;
                case "bmp":
                    return FileType.BMP;
                case "png":
                    return FileType.PNG;
                case "gif":
                    return FileType.GIF;
                case "sfx":
                    return FileType.SFX;
                case "docx":
                    return FileType.DOCX;
                case "xlsx":
                    return FileType.XLSX;
                case "pptx":
                    return FileType.PPTX;
                case "ppt":
                    return FileType.PPT;
                default:
                    return FileType.Undefined;
            }

        }

        public static void LoadAttatchmentsConfiguration()
        {
            try
            {
                System.Configuration.AppSettingsReader asr = new System.Configuration.AppSettingsReader();

                FileServer = asr.GetValue("FILESERVER", typeof(string)).ToString();
                UserName = asr.GetValue("USERNAME", typeof(string)).ToString();
                Password = asr.GetValue("PASSWORD", typeof(string)).ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to read configuration data." + ex.Message);
            }

        }

        public static bool CheckFileSize(String filePath)
        {
            if (!Constants.C_SAVE_ATTATCHMENTS_IN_DB)
                return true;

            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.Length > Constants.C_MAXIMUM_ATTATCHMENT_SIZE)
                return false;
            else return true;
        }

        public static void StoreAttatchment(String fileName, AttatchmentType attatchmentType, Int32 AttatchedToID, String Remarks)
        {
            if (Constants.C_SAVE_ATTATCHMENTS_IN_DB)
            {

                Byte[] fileBytes;
                Byte tip = (byte)GetFileType(fileName);

                String shortFileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);

                using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    fileBytes = new Byte[file.Length];
                    int fileLength = Convert.ToInt32(file.Length);
                    file.Read(fileBytes, 0, fileLength);

                    file.Close();
                }

                StandardStoreProc sp = new StandardStoreProc("sp_InsertAttatchment", new object[] { (Byte)attatchmentType, AttatchedToID, tip, shortFileName, fileBytes, Remarks, LoginClass.userID });
                WindDatabase.ExecuteNonQuery(sp);

            }
            else
            {
                Byte tip = (byte)GetFileType(fileName);
                String shortFileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);


                Impersonate impersonator = new Impersonate();
                impersonator.ImpersonateUser(UserName, FileServer, Password);

                FileInfo file = new FileInfo(fileName);
                if (System.IO.File.Exists(System.IO.Path.Combine(FileServer, shortFileName)))
                    throw new Exception("Un fisier cu acelasi nume exista deja");
                file.CopyTo(System.IO.Path.Combine(FileServer, shortFileName), false);

                impersonator.UndoImpersonate();

                StandardStoreProc sp = new StandardStoreProc("sp_InsertAttatchment", new object[] { (Byte)attatchmentType, AttatchedToID, tip, shortFileName, null, Remarks, LoginClass.userID });
                WindDatabase.ExecuteNonQuery(sp);


            }
        }

        public static void DeleteAttatchment(Int32 AttatchmentID)
        {
            StandardStoreProc sp = new StandardStoreProc("sp_DeleteAttatchment", new object[] { AttatchmentID, LoginClass.userID });
            WindDatabase.ExecuteNonQuery(sp);
        }

        public static void RetrieveAttatchmentByID(String filePath, Int32 AttatchmentID)
        {
            if (Constants.C_SAVE_ATTATCHMENTS_IN_DB)
            {
                Byte[] fileBytes;

                #region Retrieve attatchment

                DataSet ds = new DataSet();
                ds.Tables.Add("Atasamente");

                WindDatabase.LoadDataSet(ds, new string[] { "Atasamente" }, "sp_RetrieveAttatchment", new object[] { AttatchmentID });

                fileBytes = (Byte[])ds.Tables["Atasamente"].Rows[0]["Continut"];

                #endregion

                #region Save to Disk
                using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    stream.Write(fileBytes, 0, fileBytes.Length);
                    stream.Flush();
                    stream.Close();
                }
                #endregion

                #region Open File


                try
                {
                    System.Diagnostics.Process.Start(filePath);
                }
                catch (System.ComponentModel.Win32Exception) // nu se poate deschide fisierul pt ca nu are default app pt ext aia / alta probl
                {
                    System.Diagnostics.ProcessStartInfo explorer = new System.Diagnostics.ProcessStartInfo();
                    explorer.FileName = "explorer.exe";
                    explorer.Arguments = filePath;
                    System.Diagnostics.Process.Start(explorer);
                }


                #endregion
            }
            else
            {

                #region Retrieve filename from DB
                DataSet ds = new DataSet();
                ds.Tables.Add("Atasamente");

                WindDatabase.LoadDataSet(ds, new string[] { "Atasamente" }, "sp_RetrieveAttatchment", new object[] { AttatchmentID });

                String fileName = ds.Tables["Atasamente"].Rows[0]["Nume"].ToString();

                #endregion

                #region Save to Disk

                Impersonate impersonator = new Impersonate();

                impersonator.ImpersonateUser(UserName, FileServer, Password);
                FileInfo file = new FileInfo(System.IO.Path.Combine(FileServer, fileName));

                file.CopyTo(filePath, true);

                impersonator.UndoImpersonate();

                #endregion

                #region Open File

                try
                {
                    System.Diagnostics.Process.Start(filePath);
                }
                catch (System.ComponentModel.Win32Exception) // nu se poate deschide fisierul pt ca nu are default app pt ext aia / alta probl
                {
                    System.Diagnostics.ProcessStartInfo explorer = new System.Diagnostics.ProcessStartInfo();
                    explorer.FileName = "explorer.exe";
                    explorer.Arguments = filePath;
                    System.Diagnostics.Process.Start(explorer);
                }
                #endregion

            }
        }

    }
}
