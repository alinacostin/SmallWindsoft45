using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.IO;
using System.Globalization;
using Base.Configuration;
using System.Linq;
using BaseUtils;
using System.Net;
using FluentFTP;

namespace Base.BaseUtils
{
    public static partial class UtilsFTP
    {
        public static string StatusCode;
        public static string StatusDescription;

        #region Statusuri

        /// <summary>
        /// Clear stats
        /// </summary>
        private static void ClearStats()
        {
            StatusCode = StatusDescription = string.Empty;
        }

        #endregion Statusuri

        #region [List FTP Directory]

        /// <summary>
        /// List files inside FTP Directory
        /// </summary>
        /// <param name="accountType">Account => Delamode, MM etc.</param>
        /// <param name="direction">Import / Export Direction</param>
        /// <returns>If Success => true & filesInsideDirectory = List of files inside directory; Else => False & filesInsideDirectory = Null</returns>
        public static bool ListDirectory(FTPAccountType accountType, FTPDirection direction, ref List<string> filesInsideDirectory)
        {
            try
            {
                ClearStats();

                FTPClient client = new FTPClient(accountType, direction);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(client.FtpServer);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(client.FtpUsername, client.FtpPassword);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                filesInsideDirectory = new List<string>();

                while (!reader.EndOfStream)
                {
                    filesInsideDirectory.Add(reader.ReadLine());
                }

                reader.Close();
                response.Close();

                return true;
            }
            catch (WebException ex)
            {
                if (null != ex.Response)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    StatusCode = GetStatusCode(response.StatusCode);
                    StatusDescription = response.StatusDescription;
                }
                filesInsideDirectory = null;
                return false;
            }
        }

        #endregion [List FTP Directory]

        #region [FTP Downloading]
        public static bool DownloadFileFromFTP(string user, string password, string ftpPath, string destinationfilepath, string fileName)
        {
            try
            {
                ClearStats();

                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential(user, password);
                    byte[] fileData = request.DownloadData(ftpPath);

                    using (FileStream file = File.Create(destinationfilepath))
                    {
                        file.Write(fileData, 0, fileData.Length);
                        file.Close();
                    }
                }

                return true;
            }
            catch (WebException ex)
            {
                if (null != ex.Response)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    StatusCode = GetStatusCode(response.StatusCode);
                    StatusDescription = response.StatusDescription;
                }
                return false;
            }
            catch (IOException ex)
            {
                StatusDescription = string.Format(@"Eroare la citirea fisierului ' {0} ' de la adresa ' {1} ' {2}{3}",
                                                Path.GetFileName(fileName), ftpPath,
                                                Environment.NewLine, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                               Environment.NewLine, ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Download file from FTP address.
        /// </summary>
        /// <param name="accountType">Account => Delamode, MM etc.</param>
        /// <param name="direction">Import / Export Direction</param>
        /// <param name="fileName">File name to download</param>
        /// <returns>True = file downloaded & deleted from FTP Account; False = otherwise</returns>
        public static bool DownloadFile(FTPAccountType accountType, FTPDirection direction, string fileName)
        {
            try
            {
                ClearStats();

                FTPClient client = new FTPClient(accountType, direction);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(client.FtpServer + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(client.FtpUsername, client.FtpPassword);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                if (!CreateDirectory())
                    return false;

                StreamWriter writer = new StreamWriter(Constants.EDI_FILE_LOCATIONS + fileName);
                writer.Write(reader.ReadToEnd());

                writer.Close();
                reader.Close();
                response.Close();

                if (!DeleteFile(accountType, direction, fileName))
                    return false;

                return true;
            }
            catch (WebException ex)
            {
                if (null != ex.Response)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    StatusCode = GetStatusCode(response.StatusCode);
                    StatusDescription = response.StatusDescription;
                }
                return false;
            }
            catch (IOException ex)
            {
                StatusDescription = string.Format(@"Eroare la scriererea fisierului ' {0} ' la adresa ' {1} ' {2}{3}",
                                                fileName, Constants.EDI_FILE_LOCATIONS + fileName,
                                                Environment.NewLine, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                               Environment.NewLine, ex.Message);
                return false;
            }
        }


        public static List<KeyValuePair<string, string>> GetFileListFromResources(FTPAccountType accountType, FTPDirection direction, string syncPath)
        {
            List<KeyValuePair<string, string>> fisiere = new List<KeyValuePair<string, string>>();

            ClearStats();

            FTPClient client = new FTPClient(accountType, direction);
            string FtpServer = client.FtpServer.EndsWith("/") ? client.FtpServer : client.FtpServer + "/";

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(FtpServer + "Resources"));
            request.UseBinary = true;
            request.Credentials = new NetworkCredential(client.FtpUsername, client.FtpPassword);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Proxy = null;
            request.KeepAlive = false;
            request.UsePassive = false;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string fileName = reader.ReadLine();
                    while (fileName != null)
                    {
                        fisiere.Add(new KeyValuePair<string, string>("Resources\\" + fileName, syncPath + "Resources\\" + fileName));
                        fileName = reader.ReadLine();
                    }

                    return fisiere;
                }
            }
        }

        public static bool DownloadFTPResources(FTPAccountType accountType, FTPDirection direction, string syncPath)
        {
            try
            {
                ClearStats();

                FTPClient client = new FTPClient(accountType, direction);
                string FtpServer = client.FtpServer.EndsWith("/") ? client.FtpServer : client.FtpServer + "/";

                var uri = new Uri(FtpServer);
                var credential = new NetworkCredential(client.FtpUsername, client.FtpPassword);

                using (var ftpClient = new FluentFTP.FtpClient(uri, credential))
                {
                    ftpClient.Connect();

                    ftpClient.DownloadDirectory(syncPath, "Resources", FtpFolderSyncMode.Update);

                    ftpClient.Disconnect();
                }

                return true;
            }
            catch(Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                              Environment.NewLine, ex.Message);
                return false;
            }
        }


        public static void EnsureFolder(string path)
        {
            try
            {
                string directoryName = Path.GetDirectoryName(path);
                if ((directoryName.Length > 0) && (!Directory.Exists(directoryName)))
                    Directory.CreateDirectory(directoryName);
            }
            catch { }
        }        
        public static bool DownloadFile(FTPAccountType accountType, FTPDirection direction, string fileName, string downloadPath)
        {
            try
            {
                ClearStats();

                FTPClient client = new FTPClient(accountType, direction);

                string FtpServer = client.FtpServer.EndsWith("/") ? client.FtpServer : client.FtpServer + "/";
                Uri uri = new Uri(FtpServer + fileName);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(client.FtpUsername, client.FtpPassword);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (FileStream writer = new FileStream(downloadPath, FileMode.Create))
                        {
                            byte[] buffer = new byte[2048];
                            int ReadCount = responseStream.Read(buffer, 0, buffer.Length);
                            while (ReadCount > 0)
                            {
                                writer.Write(buffer, 0, ReadCount);
                                ReadCount = responseStream.Read(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }

                return true;
            }
            catch (WebException ex)
            {
                if (null != ex.Response)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    StatusCode = GetStatusCode(response.StatusCode);
                    StatusDescription = response.StatusDescription;
                }
                return false;
            }
            catch (IOException ex)
            {
                StatusDescription = string.Format(@"Eroare la scriererea fisierului ' {0} ' la adresa ' {1} ' {2}{3}",
                                                fileName, downloadPath,
                                                Environment.NewLine, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                               Environment.NewLine, ex.Message);
                return false;
            }
        }

        public static bool DownloadFileFromFTP(FTPAccountType accountType, FTPDirection direction, string fileName, string downloadPath)
        {

            try
            {
                ClearStats();

                FTPClient client = new FTPClient(accountType, direction);
                string FtpServer = client.FtpServer.EndsWith("/") ? client.FtpServer : client.FtpServer + "/";

                var uri = new Uri(FtpServer);
                var credential = new NetworkCredential(client.FtpUsername, client.FtpPassword);

                using (var ftpClient = new FluentFTP.FtpClient(uri, credential))
                {
                    ftpClient.Connect();

                    var result = ftpClient.DownloadFile(downloadPath, fileName);

                    ftpClient.Disconnect();
                }

                return true;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                               Environment.NewLine, ex.Message);
                return false;
            }
        }

        public static bool CheckFluentFTPExist()
        {
            try
            {
                if (File.Exists(Environment.CurrentDirectory + "\\" + "FluentFTP.dll"))
                    return true;
            }
            catch
            {
                return false;
            }
            
            return false;
        }

        /// <summary>
        /// Download files from FTP address.
        /// </summary>
        /// <param name="accountType">Account => Delamode, MM etc.</param>
        /// <param name="direction">Import / Export Direction</param>
        /// <param name="files">File names to download</param>
        /// <returns>True = if each file downloaded & deleted from FTP Account; False = otherwise</returns>
        public static bool DownloadAllFiles(FTPAccountType accountType, FTPDirection direction, List<string> files)
        {
            try
            {
                ClearStats();
                foreach(var fileName in files)
                    if(!DownloadFile(accountType, direction, fileName))
                        return false;
                return true;
            }           
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Eroare la preluarea fisierelor EDI de la adresa FTP stabilita! {0} {1}",                                                
                                                Environment.NewLine, ex.Message);
                return false;
            }
        }

        public static bool DownloadAllFiles(FTPAccountType accountType, FTPDirection direction, List<KeyValuePair<string, string>> files)
        {
            try
            {
                ClearStats();

                foreach (KeyValuePair<string, string> fileName in files)
                    if (!DownloadFile(accountType, direction, fileName.Key, fileName.Value))
                        return false;

                return true;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Eroare la preluarea fisierelor de la adresa FTP stabilita! {0} {1}",
                                                Environment.NewLine, ex.Message);
                return false;
            }
        }

        public static bool DownloadAllFilesFromFTP(FTPAccountType accountType, FTPDirection direction, List<KeyValuePair<string, string>> files)
        {
            try
            {
                ClearStats();

                foreach (KeyValuePair<string, string> fileName in files)
                    if (!DownloadFileFromFTP(accountType, direction, fileName.Key, fileName.Value))
                        return false;

                return true;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Eroare la preluarea fisierelor de la adresa FTP stabilita! {0} {1}",
                                                Environment.NewLine, ex.Message);
                return false;
            }
        }
        #endregion [FTP Downloading]

        #region [FTP Deleting]
        public static bool DeleteFileFromFTP(string user, string password, string ftpPath, string fileName)
        {
            try
            {
                ClearStats();

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpPath);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(user, password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();

                return true;
            }
            catch (WebException ex)
            {
                if (null != ex.Response)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    StatusCode = GetStatusCode(response.StatusCode);
                    StatusDescription = response.StatusDescription;
                }
                return false;
            }
            catch (IOException ex)
            {
                StatusDescription = string.Format(@"Eroare la stergerea fisierului ' {0} ' la adresa ' {1} ' {2}{3}",
                                                fileName, ftpPath,
                                                Environment.NewLine, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                               Environment.NewLine, ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Delete file from FTP address.
        /// </summary>
        /// <param name="accountType">Account => Delamode, MM etc.</param>
        /// <param name="direction">Import / Export Direction</param>
        /// <param name="fileName">File to delete</param>
        /// <returns>True = file deleted with success from FTP Server; False = otherwise</returns>
        public static bool DeleteFile(FTPAccountType accountType, FTPDirection direction, string fileName)
        {
            try
            {
                ClearStats();

                FTPClient client = new FTPClient(accountType, direction);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(client.FtpServer + fileName);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(client.FtpUsername, client.FtpPassword);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();

                return true;
            }
            catch (WebException ex)
            {
                if (null != ex.Response)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    StatusCode = GetStatusCode(response.StatusCode);
                    StatusDescription = response.StatusDescription;
                }
                return false;
            }
            catch (IOException ex)
            {
                StatusDescription = string.Format(@"Eroare la scriererea fisierului ' {0} ' la adresa ' {1} ' {2}{3}",
                                                fileName, Constants.EDI_FILE_LOCATIONS + fileName,
                                                Environment.NewLine, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                               Environment.NewLine, ex.Message);
                return false;
            }
        }

        #endregion [FTP Deleting]

        #region [FTP Uploading]
        public static bool UploadFileToFTP(string user, string password, string ftpPath, string fileName)
        {
            try
            {
                ClearStats();

                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(user, password);
                    client.UploadFile(ftpPath + @"/" + fileName, fileName);
                }

                return true;
            }
            catch (WebException ex)
            {
                if (null != ex.Response)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    StatusCode = GetStatusCode(response.StatusCode);
                    StatusDescription = response.StatusDescription;
                }
                return false;
            }
            catch (IOException ex)
            {
                StatusDescription = string.Format(@"Eroare la UPLOAD-ul fisierului ' {0} ' de la adresa ' {1} ' {2}{3}",
                                                Path.GetFileName(fileName), ftpPath,
                                                Environment.NewLine, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                               Environment.NewLine, ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Upload file from FTP address.
        /// </summary>
        /// <param name="accountType">Account => Delamode, MM etc.</param>
        /// <param name="direction">Import / Export Direction</param>
        /// <param name="fileName">File name to upload</param>
        /// <returns>True = file uploaded to FTP Account; False = otherwise</returns>
        public static bool UploadFile(FTPAccountType accountType, FTPDirection direction, string fileName)
        {
            try
            {
                ClearStats();               

                FTPClient client = new FTPClient(accountType, direction);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(client.FtpServer + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(client.FtpUsername, client.FtpPassword);

                StreamReader sourceStream = new StreamReader(fileName);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());

                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);

                //FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                //response.Close();
                requestStream.Close();
                sourceStream.Close();
                
                return true;
            }
            catch (WebException ex)
            {
                if (null != ex.Response)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    StatusCode = GetStatusCode(response.StatusCode);
                    StatusDescription = response.StatusDescription;
                }
                return false;
            }
            catch (IOException ex)
            {
                StatusDescription = string.Format(@"Eroare la UPLOAD-ul fisierului ' {0} ' de la adresa ' {1} ' {2}{3}",
                                                Path.GetFileName(fileName), fileName,
                                                Environment.NewLine, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                               Environment.NewLine, ex.Message);
                return false;
            }
        }

        public static bool UploadFile(FTPAccountType accountType, FTPDirection direction, string sourcePath, string destinationPath, string fileName)
        {
            MakeFTPDir(accountType, direction, destinationPath);
            try
            {
                ClearStats();

                FTPClient client = new FTPClient(accountType, direction);

                string FtpServer = client.FtpServer.EndsWith("/") ? client.FtpServer : client.FtpServer + "/";
                Uri uri = new Uri(FtpServer + destinationPath + "/" + fileName);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(client.FtpUsername, client.FtpPassword);

                byte[] fileContents = File.ReadAllBytes(sourcePath);

                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);

                requestStream.Close();

                return true;
            }
            catch (WebException ex)
            {
                if (null != ex.Response)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    StatusCode = GetStatusCode(response.StatusCode);
                    StatusDescription = response.StatusDescription;
                }
                return false;
            }
            catch (IOException ex)
            {
                StatusDescription = string.Format(@"Eroare la UPLOAD-ul fisierului ' {0} ' de la adresa ' {1} ' {2}{3}",
                                                Path.GetFileName(sourcePath), sourcePath,
                                                Environment.NewLine, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                               Environment.NewLine, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Upload files to FTP address.
        /// </summary>
        /// <param name="accountType">Account => Delamode, MM etc.</param>
        /// <param name="direction">Import / Export Direction</param>
        /// <param name="files">File names to upload</param>
        /// <returns>True = if each file uploaded to FTP Account; False = otherwise</returns>
        public static bool UploadAllFiles(FTPAccountType accountType, FTPDirection direction, List<string> files)
        {
            try
            {
                ClearStats();
                foreach (var fileName in files)
                    if (!UploadFile(accountType, direction, fileName))
                        return false;
                return true;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Eroare la UPLOAD-ul fisierelor EDI la adresa FTP stabilita! {0} {1}",
                                                Environment.NewLine, ex.Message);
                return false;
            }
        }       

        #endregion [FTP Uploading]

        #region [Status Codes]

        /// <summary>
        /// Statusul curent pe FTP.
        /// </summary>
        /// <param name="code">Codul curent al obiectului FTP</param>
        /// <returns>Explained status code for current FtpWebResponse</returns>
        private static string GetStatusCode(FtpStatusCode code)
        {
            switch (code)
            {
                case FtpStatusCode.OpeningData:
                    return "Conexiunea a fost deschisa cu succes.";
                case FtpStatusCode.ClosingData:
                    return "Conexiunea a fost inchisa cu succes.";
                case FtpStatusCode.CommandOK:
                    return "Comanda a fost executata cu succes.";
                case FtpStatusCode.FileActionOK:
                    return "Operatia asupra fisierului a fost incheiata cu succes.";
                case FtpStatusCode.ConnectionClosed:
                    return "Conexiunea a fost inchisa.";
                case FtpStatusCode.ActionNotTakenFileUnavailable:
                    return "Fisierul selectat pentru operatiune nu mai este disponibil.";
                default:
                    return "Conexiune nedeterminata. + " + code.ToString();                    
            }
        }

        #endregion [Status Codes]

        #region [Files & Directories Operations]
        public static bool FileExistFTP(string user, string password, string ftpPath, string fileName)
        {
            try
            {
                ClearStats();

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpPath);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(user, password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();

                return true;
            }
            catch (WebException ex)
            {
                if (null != ex.Response)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    StatusCode = GetStatusCode(response.StatusCode);
                    StatusDescription = response.StatusDescription;
                }
                return false;
            }
            catch (IOException ex)
            {
                StatusDescription = string.Format(@"Eroare la cautarea fisierului ' {0} ' la adresa ' {1} ' {2}{3}",
                                                fileName, ftpPath,
                                                Environment.NewLine, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                               Environment.NewLine, ex.Message);
                return false;
            }
        }

        private static bool CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(Constants.EDI_FILE_LOCATIONS))
                    Directory.CreateDirectory(Constants.EDI_FILE_LOCATIONS);
                return true;
            }
            catch (IOException ex)
            {
                StatusDescription = string.Format(@"Eroare la crearea directorului {0}{1}{2}",
                                                  Constants.EDI_FILE_LOCATIONS, Environment.NewLine, ex.Message
                                                  );
                return false;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Eroare scriere. {0}{1}",
                                                  Environment.NewLine, ex.Message
                                                  );
                return false;
            }
        }

        public static void MakeFTPDir(FTPAccountType accountType, FTPDirection direction, string pathToCreate)
        {
            try
            {
                ClearStats();

                FTPClient client = new FTPClient(accountType, direction);

                FtpWebRequest reqFTP = null;
                Stream ftpStream = null;

                string[] subDirs = pathToCreate.Split('\\');

                string FtpServer = client.FtpServer.EndsWith("/") ? client.FtpServer : client.FtpServer + "/";

                foreach (string subDir in subDirs)
                {
                    try
                    {
                        FtpServer = FtpServer + "/" + subDir;
                        Uri uri = new Uri(FtpServer);
                        reqFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                        reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                        reqFTP.UseBinary = true;
                        reqFTP.Credentials = new NetworkCredential(client.FtpUsername, client.FtpPassword);
                        FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                        ftpStream = response.GetResponseStream();
                        ftpStream.Close();
                        response.Close();
                    }
                    catch (Exception ex)
                    {
                        //daca directorul exista oricum va arunca eroarea asa ca il las sa treaca mai departe
                    }
                }
            }
            catch (Exception) { }
        }

        public static bool MoveFile(FTPAccountType accountType, FTPDirection direction, string fileToMove, string origin, string destination)
        {
            try
            {
                ClearStats();

                FTPClient client = new FTPClient(accountType, direction);

                string FtpServer = client.FtpServer.EndsWith("/") ? client.FtpServer : client.FtpServer + "/";


                Uri uriSource = new Uri(FtpServer + origin + "/" + fileToMove, UriKind.Absolute);
                Uri uriDestination = new Uri(FtpServer + destination + "/" + fileToMove, UriKind.Absolute);

                MakeFTPDir(accountType, direction, destination);

                Uri targetUriRelative = uriSource.MakeRelativeUri(uriDestination);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uriSource.AbsoluteUri);
                request.Method = WebRequestMethods.Ftp.Rename;
                request.Credentials = new NetworkCredential(client.FtpUsername, client.FtpPassword);
                request.RenameTo = Uri.UnescapeDataString(targetUriRelative.OriginalString);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                StatusCode = GetStatusCode(response.StatusCode);
                StatusDescription = response.StatusDescription;

                return true;
            }
            catch (WebException ex)
            {
                if (null != ex.Response)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    StatusCode = GetStatusCode(response.StatusCode);
                    StatusDescription = response.StatusDescription;
                }
                return false;
            }
            catch (IOException ex)
            {
                StatusDescription = String.Format("Eroare la mutarea fisierului {0} din locatia {1} in locatia {2}!{3}{4}", fileToMove, origin, destination, Environment.NewLine, ex.Message); return false;
            }
            catch (Exception ex)
            {
                StatusDescription = string.Format(@"Operatia a esuat. {0}{1}",
                                               Environment.NewLine, ex.Message);
                return false;
            }
        }

        #endregion [Files & Directories Operations]
    }

    /// <summary>
    /// Helper Class => set server address, username, password.
    /// </summary>
    public class FTPClient
    {
        public string FtpServer { get; set; }
        public string FtpUsername { get; set; }
        public string FtpPassword { get; set; }

        public FTPClient(FTPAccountType accountType, FTPDirection direction)
        {
            switch (accountType)
            {
                case FTPAccountType.MM_ROMANIA:
                    switch(direction)
                    {
                        case FTPDirection.IMPORT:
                            FtpServer = Constants.FTP_MM_ADDRESS_IMPORT;                          
                            break;
                        default:
                            //FtpServer = Constants.FTP_MM_ADDRESS_EXPORT;                            
                            break;
                    }
                    FtpUsername = Constants.FTP_MM_USER;
                    FtpPassword = Constants.FTP_MM_PASS;
                    break;
                case FTPAccountType.MM_ROMANIA_CLUJ:
                    switch (direction)
                    {
                        case FTPDirection.IMPORT:
                            FtpServer = Constants.FTP_MM_ADDRESS_IMPORT_CLUJ;
                            break;
                        default:
                            //FtpServer = Constants.FTP_MM_ADDRESS_EXPORT_CLUJ;
                            break;
                    }
                    FtpUsername = Constants.FTP_MM_USER;
                    FtpPassword = Constants.FTP_MM_PASS;
                    break;
                case FTPAccountType.MM_ROMANIA_TIMISOARA:
                    switch (direction)
                    {
                        case FTPDirection.IMPORT:
                            FtpServer = Constants.FTP_MM_ADDRESS_IMPORT_TIMISOARA;
                            break;
                        default:
                            //FtpServer = Constants.FTP_MM_ADDRESS_EXPORT_TIMISOARA;
                            break;
                    }
                    FtpUsername = Constants.FTP_MM_USER;
                    FtpPassword = Constants.FTP_MM_PASS;
                    break;
                case FTPAccountType.DELAMODE_RHENUS:
                    switch (direction)
                    {
                        case FTPDirection.IMPORT:
                            FtpServer = Constants.FTP_DELAMODE_RHENUS_ADDRESS_IMPORT;
                            break;
                        default:
                            //FtpServer = Constants.FTP_DELAMODE_RHENUS_ADDRESS_EXPORT;
                            break;
                    }
                    FtpUsername = Constants.FTP_DELAMODE_RHENUS_USER;
                    FtpPassword = Constants.FTP_DELAMODE_RHENUS_PASS;
                    break;
                case FTPAccountType.DELAMODE_SEVSTA:
                    switch (direction)
                    {
                        case FTPDirection.IMPORT:
                            FtpServer = Constants.FTP_DELAMODE_SEVSTA_ADDRESS_IMPORT;
                            break;
                        default:
                            //FtpServer = Constants.FTP_DELAMODE_SEVSTA_ADDRESS_EXPORT;
                            break;
                    }
                    FtpUsername = Constants.FTP_DELAMODE_STAECO_SEVSTA_USER;
                    FtpPassword = Constants.FTP_DELAMODE_STAECO_SEVSTA_PASS;
                    break;
                case FTPAccountType.DELAMODE_SEVSTA_ORADEA:
                    switch (direction)
                    {
                        case FTPDirection.IMPORT:
                            FtpServer = Constants.FTP_DELAMODE_SEVSTA_ADDRESS_IMPORT_ORADEA;
                            break;
                        default:
                            //FtpServer = Constants.FTP_DELAMODE_SEVSTA_ADDRESS_EXPORT;
                            break;
                    }
                    FtpUsername = Constants.FTP_DELAMODE_STAECO_SEVSTA_USER;
                    FtpPassword = Constants.FTP_DELAMODE_STAECO_SEVSTA_PASS;
                    break;
                case FTPAccountType.DELAMODE_STAECO:
                    switch (direction)
                    {
                        case FTPDirection.IMPORT:
                            FtpServer = Constants.FTP_DELAMODE_STAECO_ADDRESS_IMPORT;
                            break;
                        default:
                            //FtpServer = Constants.FTP_DELAMODE_STAECO_ADDRESS_EXPORT;
                            break;
                    }
                    FtpUsername = Constants.FTP_DELAMODE_STAECO_SEVSTA_USER;
                    FtpPassword = Constants.FTP_DELAMODE_STAECO_SEVSTA_PASS;
                    break;
                case FTPAccountType.DELAMODE_STAECO_ORADEA:
                    switch (direction)
                    {
                        case FTPDirection.IMPORT:
                            FtpServer = Constants.FTP_DELAMODE_STAECO_ADDRESS_IMPORT_ORADEA;
                            break;
                        default:
                            //FtpServer = Constants.FTP_DELAMODE_STAECO_ADDRESS_EXPORT;
                            break;
                    }
                    FtpUsername = Constants.FTP_DELAMODE_STAECO_SEVSTA_USER;
                    FtpPassword = Constants.FTP_DELAMODE_STAECO_SEVSTA_PASS;
                    break;
                case FTPAccountType.DELAMODE_UK:
                    switch (direction)
                    {
                        case FTPDirection.IMPORT:
                            FtpServer = Constants.FTP_DELAMODE_UK_ADDRESS_IMPORT;
                            break;
                        default:
                            //FtpServer = Constants.FTP_DELAMODE_UK_ADDRESS_EXPORT;
                            break;
                    }
                    FtpUsername = Constants.FTP_DELAMODE_UK_USER;
                    FtpPassword = Constants.FTP_DELAMODE_UK_PASS;
                    break;
                case FTPAccountType.WINDSOFT:
                    FtpServer = Constants.FTP_WINDSOFT_ADDRESS;
                    FtpUsername = Constants.FTP_WINDSOFT_USER;
                    FtpPassword = Constants.FTP_WINDSOFT_PASS;
                    break;
                default:
                    break;
            }
        }
    }
}
