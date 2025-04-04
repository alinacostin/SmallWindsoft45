using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BaseUtils;
using Base.Configuration;
using System.Configuration;
using System.IO;
using System.Xml;
using Base.DataBase;
using System.Data.SqlClient;
using Base.BaseUtils;

namespace Base.Logs
{
    public static class Logger
    {
        /// <summary>
        /// Salvare log-uri
        /// </summary>
        /// <param name="masterID">ID-ul master al documentului curent</param>
        /// <param name="masterDataSet">Sursa de date curenta</param>
        /// <param name="userInterfaceType">Tipul de user interface</param>
        /// <param name="operationType">Tipul operatiei (ex. save, update, delete etc.)</param>
        /// <param name="executionResult">Rezultatul executiei (ex. salvat cu succes, eroare etc.)</param>
        /// <param name="recordState">Status-ul curent al documentului (ex. activ, blocat, anulat etc.)</param>
        /// <param name="executionMessage">Mesajul curent al executiei</param>
        public static void SaveLog(int masterID, DataSet masterDataSet, UserInterfaceType userInterfaceType, OperationType operationType, ExecutionResult executionResult, RecordState recordState, string executionMessage)
        {
            if (Constants.client == Constants.Clients.Delamode) return;
            if (!LoginClass.IsLogginOn || LoginClass.userID == -1) // loging-ul dezactivat sau user-ul nu exista
                return;

            var fileName = string.Join("_", new string[] { Guid.NewGuid().ToString(), masterID.ToString() });
            var filePath = string.Empty;
            
            // Save to XML
            if (!SaveLogToXML(userInterfaceType, fileName, new StringBuilder(masterDataSet.GetXml()), out filePath)) // daca nu putem salva XML-ul oprim procesul
                return;

            // Create current operation description
            var logDescription = string.Empty;
            LogCreateDescription(masterID, userInterfaceType, out logDescription);

            // Save to DataBase
            SaveLogToDataBase(masterID, userInterfaceType, operationType, executionResult, recordState, filePath, executionMessage, logDescription);
        }

        /// <summary>
        /// Salvare log in format XML
        /// </summary>
        /// <param name="userInterfaceType">user interface-ul</param>
        /// <param name="fileName">numele fisierului generat</param>
        /// <param name="fileContent">continutul fisierului XML</param>
        /// <param name="filePath">calea unde se salveaza</param>
        /// <returns>True - daca fisierul a fost salvat; False - altfel</returns>
        public static bool SaveLogToXML(UserInterfaceType userInterfaceType, string fileName, StringBuilder fileContent, out string filePath)
        {
            var fileServer = string.Empty;
            var userName = string.Empty;
            var password = string.Empty;
            filePath = string.Empty;
            try
            {
                AppSettingsReader asr = new AppSettingsReader();

                fileServer = asr.GetValue("LoggingPath", typeof(string)).ToString();
                userName = asr.GetValue("USERNAME", typeof(string)).ToString();
                password = asr.GetValue("PASSWORD", typeof(string)).ToString();

                if (!System.IO.Directory.Exists(fileServer)) return false;
            }
            catch
            {
                return false;
            }
            
            Base.Impersonator.Impersonate impersonator = new Base.Impersonator.Impersonate();
            try
            {
                impersonator.ImpersonateUser(userName, fileServer, password);
                filePath = Path.Combine(fileServer, fileName + ".xml");

                // start to save XML
                XmlDocument xDoc = new XmlDocument();
                XmlAttribute att = xDoc.CreateAttribute("UserInterfaceType");
                att.Value = ((int)userInterfaceType).ToString();
                xDoc.LoadXml(fileContent.ToString());
                xDoc.FirstChild.Attributes.Append(att);
                xDoc.Save(filePath);
            }
            catch
            {
                return false;
            }
            finally
            {
                impersonator.UndoImpersonate();
            }
            return true;
        }

        /// <summary>
        /// Salvare log in baza de date
        /// </summary>
        /// <param name="masterID">ID-ul master</param>
        /// <param name="userInterfaceTypeID">user interface-ul</param>
        /// <param name="operationType">tipul operatiei (ex.: inregistrare noua, modificare, salvare etc.)</param>
        /// <param name="executionResult">daca a fost executata cu succes sau nu</param>
        /// <param name="recordState">status-ul documentului curent (ex. activ, blocat, anulat etc.)</param>
        /// <param name="filePath">calea unde se gaseste XML-ul salvat</param>
        /// <param name="executionMessage">Mesajul curent al executiei</param>
        public static void SaveLogToDataBase(int masterID, UserInterfaceType userInterfaceTypeID, OperationType operationType, ExecutionResult executionResult, RecordState recordState, string filePath, string executionMessage, string logDescription)
        {
            try
            {
                int returnValue = -1;
                using (SqlDataReader reader = (SqlDataReader)WindDatabase.ExecuteDataReader("sp_WindNet_Log_Insert",
                                        (new List<object>() 
                                        {
                                          LoginClass.userID,
                                          masterID,
                                          (int)userInterfaceTypeID,
                                          DateTime.Now,
                                          (int)operationType,
                                          (int)recordState,
                                          (int)executionResult, 
                                          filePath,
                                          executionMessage,
                                          logDescription
                                        })
                                        .ToArray())
                      )
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
            catch
            {
                return;
            }            
        }

        /// <summary>
        /// Crearea descrierii pentru log-ul curent
        /// </summary>
        /// <param name="masterID">Cheia master</param>
        /// <param name="userInterfaceTypeID">Userinterface-ul curent</param>
        /// <param name="logDescription">Out - descrierea</param>
        public static void LogCreateDescription(int masterID, UserInterfaceType userInterfaceTypeID, out string logDescription)
        {
            logDescription = string.Empty;

            LogUserInterfaceObject logObject = Interpreter.Fill(userInterfaceTypeID);
            if (null == logObject)
                return;

            var columns = logObject.VisibleFieldNames.Split(',');
            var captions = logObject.Captions.Split(',');
            if (columns.Length != captions.Length)
                return;

            var windLogSet = new DataSet();
            try
            {
                windLogSet.Tables.Add("WindLog");
                WindDatabase.LoadDataSet(windLogSet,
                        new string[] { "WindLog" },
                        new Query(string.Format(@"{0} WHERE {1} = {2}",
                                                logObject.SelectString,
                                                logObject.PrimaryKeyName,
                                                masterID)
                                  ));

                if (0 != windLogSet.Tables["WindLog"].Rows.Count)
                {
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (i != 0)
                            logDescription += ", " + captions[i] + " : " + windLogSet.Tables[0].Rows[0][columns[i]];
                        else logDescription = captions[i] + " : " + windLogSet.Tables[0].Rows[0][columns[i]];

                    }
                }
            }
            catch
            {
                logDescription = string.Empty;
            }
            finally
            {
                windLogSet.Dispose();
            }

        }
    }
}
