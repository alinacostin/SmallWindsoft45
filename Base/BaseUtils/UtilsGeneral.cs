using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Reflection;
using System.Globalization;
using Base.Emails;
using Base.Configuration;
using System.Linq;
using System.ComponentModel;



namespace Base.BaseUtils
{
    public partial class UtilsGeneral
    {
        #region Metode pentru rotunjire
        public static double Round(double value, int decimals)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }

        public static decimal Round(decimal value, int decimals)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }

        #endregion

        #region Conversie
        
        public static double NZDouble(object value)
        {
            if (value == DBNull.Value || value == null)
                return 0;


            try
            {
                return Convert.ToDouble(value);
            }
            catch { return 0; }
            
        }

        public static decimal NZDecimal(object value)
        {
            if (value == DBNull.Value || value == null)
                return 0;

            try
            {
                return Convert.ToDecimal(value);
            }
            catch { return 0; }
        }

        public static int ToInteger(object value, int defValue)
        {
            if (value == DBNull.Value || value == null)
                return defValue;
            
            try
            {
                return Convert.ToInt32(value);
            }
            catch { return defValue; }
        }

        public static long ToInt64(object value, Int64 defValue)
        {
            if (value == DBNull.Value || value == null)
                return defValue;

            try
            {
                return Convert.ToInt64(value);
            }
            catch { return defValue; }
        }

        public static int? ToIntegerOrNull(object value)
        {
            if (value == DBNull.Value || value == null)
                    return null;
            try
            {
                return (int?)(Convert.ToInt32(value));
            }
            catch { return null; }

        }

        public static Decimal ToDecimal(object value, Decimal defValue)
        {
            if (value == DBNull.Value || value == null)
                return defValue;

            try
            {
                return Convert.ToDecimal(value);
            }
            catch { return defValue; }
        }

        public static Double ToDouble(object value, Double defValue)
        {
            if (value == DBNull.Value || value == null)
                return defValue;

            try
            {
                return Convert.ToDouble(value);
            }
            catch { return defValue; }

        }

        public static Decimal ToDecimalInvariantCulture(string value, Decimal defValue)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defValue;

            try
            {
                int minusOperator = 1;
                if (value.Contains("-"))
                {
                    value = value.Replace("-", "");
                    minusOperator = -1;
                }
                return Decimal.Parse(value.Contains(",") ? value.Replace(",", ".") : value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * minusOperator;
            }
            catch
            {
                return defValue;
            }
        }

        public static Double ToDoubleInvariantCulture(string value, Double defValue)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defValue;

            try
            {
                int minusOperator = 1;
                if (value.Contains("-"))
                {
                    value = value.Replace("-", "");
                    minusOperator = -1;
                }
                return Double.Parse(value.Contains(",") ? value.Replace(",", ".") : value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * minusOperator;
            }
            catch
            {
                return defValue;
            }
        }

        public static Int32 ToIntInvariantCulture(string value, int defValue)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defValue;

            try
            {
                string newVal = value.Contains(",") ? value.Replace(",", ".") : value;
                int minusOperator = 1;
                if (value.Contains("-"))
                {
                    value = value.Replace("-", "");
                    minusOperator = -1;
                }

                int num;
                if (int.TryParse(newVal, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out num))
                {
                    return num;
                }
                return int.Parse(newVal.Replace(".", "")) * minusOperator;
            }
            catch
            {
                return defValue;
            }
        }
        public static DateTime? ToDateTimeNull(object value)
        {
            if (value == null || value == DBNull.Value || Convert.ToDateTime(value) == Convert.ToDateTime("01.01.0001 00:00:00"))
            {
                return null;
            }

            try
            {
                return Convert.ToDateTime(value);
            }
            catch { return null; }

        }
        public static DateTime ToDateTime(object value)
        {
            if (value == null || value == DBNull.Value)
                return DateTime.MinValue;

            try
            {
                return Convert.ToDateTime(value);
            }
            catch { return DateTime.MinValue; }
        }

        public static DateTime ToDateTime(object value, string defValue)
        {
            if (value == null || value == DBNull.Value)
                return UtilsGeneral.ToDateTime(defValue);
            try
            {
                return Convert.ToDateTime(value);
            }
            catch { return UtilsGeneral.ToDateTime(defValue); }
        }

        public static DateTime ToDateTime(object value, string[] formats)
        {
            if (value == null || value == DBNull.Value)
                return DateTime.MinValue;
            try
            {
                return DateTime.ParseExact(value.ToString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch { return DateTime.MinValue; }
        }

        public static DateTime ToShortDateTime(DateTime value)
        {
            try
            {
                return Convert.ToDateTime(value.ToShortDateString());
            }
            catch { return DateTime.MinValue; }
        }

        public static string ToString(object value)
        {
            string myStr;
            try
            {
                myStr = value.ToString();
            }
            catch { myStr = ""; }
            return myStr;
        }

        public static string TruncateStringDeLaPanaLa(string str, int size, int start)//truncheaza un string de pozitia ceruta cu dimensiunea ceruta, daca se poate
        {
            int len = str.Length;
            if (len < size)
                return str.Substring(start, len);
            else
                return str.Substring(start, size);

        }

        public static bool ToBool(object value)
        {
            if (value == DBNull.Value || value == null)
                return false;

            try
            {
                return Convert.ToBoolean(value);
            }
            catch { return false; }

        }

        public static bool ToBool(object value, bool defValue)
        {
            if (value == DBNull.Value || value == null)
                return defValue;
            try
            {
                return Convert.ToBoolean(value);
            }
            catch { return defValue; }

        }

        #endregion

        #region Alte Metode

        public static int GetWeekOfYear(DateTime dt)
        {
            CultureInfo myCI = CultureInfo.CurrentCulture;
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = DayOfWeek.Monday;// myCI.DateTimeFormat.FirstDayOfWeek;

            return myCal.GetWeekOfYear(dt, myCWR, myFirstDOW);
        }

        public static string TruncateString(string str, int size)//truncheaza un string la dimensiunea ceruta, daca se poate
        {
            int len = str.Length;
            if (len < size)
                return str.Substring(0, len);
            else
                return str.Substring(0, size);

        }

        public static string[] GetStringArray(Type type)//returneaza un array de string cu elementele unei enumerari (sau alt tip)
        {
            FieldInfo[] fi = type.GetFields();
            ArrayList campuri = new ArrayList();

            for (int i = 1; i < fi.Length; i++)
                campuri.Add(fi[i].Name);

            return (string[])campuri.ToArray(typeof(String));
        }

        public static DataTable GetTable(Type type)//returneaza o tabela cu elementele unei enumerari (sau alt tip)
        {
            FieldInfo[] fi = type.GetFields();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("Value", typeof(string));
            DataRow dr;
            int i = -1;
            foreach (FieldInfo ff in fi)
            {
                if (i == -1)
                {
                    i++;
                    continue;
                }
                dr = dt.NewRow();
                dr["ID"] = i++;
                dr["Value"] = ff.Name;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable GetDistinctValues(DataTable dtable, string filter, string[] colName)
        {
            return (new DataView(dtable, filter, "", DataViewRowState.CurrentRows).ToTable(true, colName));
        }

        public static DataTable ToDataTable<T>(List<T> data)
        {
            Type tip = typeof(T);
            FieldInfo[] fieldInfo = tip.GetFields();
            DataTable table = new DataTable();
            if (null != fieldInfo)
            {
                foreach (var prop in fieldInfo)
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.FieldType) ?? prop.FieldType);
                }
                foreach (var ss in data)
                {
                    DataRow row = table.NewRow();
                    foreach (var prop in fieldInfo)
                    {
                        row[prop.Name] = prop.GetValue(ss) ?? DBNull.Value;
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static DataTable ToDataTable<T>(List<T> data, List<string> onlyFields)
        {
            Type tip = typeof(T);
            FieldInfo[] fieldInfo = tip.GetFields();
            DataTable table = new DataTable();
            if (null != fieldInfo)
            {
                foreach (var prop in fieldInfo)
                {
                    if (onlyFields.Contains(prop.Name))
                        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.FieldType) ?? prop.FieldType);
                }
                foreach (var ss in data)
                {
                    DataRow row = table.NewRow();
                    foreach (var prop in fieldInfo)
                    {
                        if (onlyFields.Contains(prop.Name))
                            row[prop.Name] = prop.GetValue(ss) ?? DBNull.Value;
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static int GetDistinctValuesCount(DataTable dtable, string filter, string colName)
        {
            DataTable dt;
            dt = new DataView(dtable, filter, "", DataViewRowState.OriginalRows).ToTable(true, colName);
            return dt.Rows.Count;
        }

        public static double GetDistinctValuesSum(DataTable dtable, string filter, string colName, string colIDName)
        {
            DataTable dt;
            double total = 0;
            dt = new DataView(dtable, filter, "", DataViewRowState.OriginalRows).ToTable(true, colIDName, colName);
            foreach (DataRow row in dt.Rows)
                total += UtilsGeneral.NZDouble(row[colName]);
            return total;
        }
        public static void SendCustomEmail(List<string> mailList, string subject, string body, bool withMessage)
        {
            MailServerParams mailServerParams = new MailServerParams();


            mailServerParams.Host = Constants.MailServerHostDelamode;
            mailServerParams.Port = Constants.MailServerPortDelamode;
            mailServerParams.User = Constants.MailServerUserDelamode;
            mailServerParams.Pass = Constants.MailServerPassDelamode;


            EMailParams mailParams = new EMailParams
            {

                To = mailList,
                Subject = subject,
                Body = body,
                SMTPSSL = Constants.UseSmtpSslDelamode,
                IsHtml = true,
                FromName = "",
                WithSmtp = Constants.WithSmtpDelamode,
                UseSmtpSsl = Constants.UseSmtpSslDelamode,
                SmtpDomain = Constants.SmtpDomain,
                UseDefaultCredentials = Constants.UseDefaultCredentials,
            };

            mailParams.From = Constants.MailParamsFromDelamode;

            try
            {
                if (SendMail.SendSimpleMail(mailParams, mailServerParams))
                {
                    if (withMessage)
                    {
                        System.Windows.Forms.MessageBox.Show(String.Format("{0}: {1}", "Emailul a fost trimis la",
                                                      mailList.Aggregate((a, b) => a + ", " + b)));
                    }
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("A aparut o eroare la trimiterea de mail. Emailul nu a fost trimis!");
            }
        }

        public static void SendCustomEmail(List<string> mailList, string subject, string body, bool withMessage, System.Net.Mail.Attachment[] attach)
        {
            MailServerParams mailServerParams = new MailServerParams();


            mailServerParams.Host = Constants.MailServerHostDelamode;
            mailServerParams.Port = Constants.MailServerPortDelamode;
            mailServerParams.User = Constants.MailServerUserDelamode;
            mailServerParams.Pass = Constants.MailServerPassDelamode;


            EMailParams mailParams = new EMailParams
            {
                To = mailList,
                Subject = subject,
                Body = body,
                SMTPSSL = Constants.UseSmtpSslDelamode,
                IsHtml = true,
                FromName = "",
                WithSmtp = Constants.WithSmtpDelamode,
                UseSmtpSsl = Constants.UseSmtpSslDelamode,
                SmtpDomain = Constants.SmtpDomain,
                UseDefaultCredentials = Constants.UseDefaultCredentials,
                AttachFiles = attach,
            };

            mailParams.From = Constants.MailParamsFromDelamode;

            try
            {
                if (SendMail.SendSimpleMail(mailParams, mailServerParams))
                {
                    if (withMessage)
                    {
                        System.Windows.Forms.MessageBox.Show(String.Format("{0}: {1}", "Emailul a fost trimis la",
                                                      mailList.Aggregate((a, b) => a + ", " + b)));
                    }
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("A aparut o eroare la trimiterea de mail. Emailul nu a fost trimis!");
            }
        }

        public static void SendCustomEmail(List<string> mailList, string subject, string body)
        {
            MailServerParams mailServerParams = new MailServerParams();

            if (Constants.client == Constants.Clients.Delamode)
            {
                mailServerParams.Host = Constants.MailServerHostDelamode;
                mailServerParams.Port = Constants.MailServerPortDelamode;
                mailServerParams.User = Constants.MailServerUserDelamode;
                mailServerParams.Pass = Constants.MailServerPassDelamode;
            }
            else
            {
                mailServerParams.Host = Constants.MailServerHost;
                mailServerParams.Port = Constants.MailServerPort;
                mailServerParams.User = Constants.MailServerUser;
                mailServerParams.Pass = Constants.MailServerPass;
            }

            EMailParams mailParams = new EMailParams
            {
                
                To = mailList,
                Subject = subject,
                Body = body,
                SMTPSSL = Constants.UseSmtpSsl,
                IsHtml = true,
                FromName = "",
                WithSmtp = Constants.WithSmtp,
                UseSmtpSsl = Constants.UseSmtpSsl,
                SmtpDomain = Constants.SmtpDomain,
                UseDefaultCredentials = Constants.UseDefaultCredentials,
            };
            if (Constants.client == Constants.Clients.Delamode)
            {
                mailParams.From = Constants.MailParamsFromDelamode;
            }
            else
            {
                mailParams.From = Constants.MailParamsFrom;
            }

            try
            {
                if (SendMail.SendSimpleMail(mailParams, mailServerParams))
                {
                    System.Windows.Forms.MessageBox.Show(String.Format("{0}: {1}", "Emailul a fost trimis la",
                                                  mailList.Aggregate((a, b) => a + ", " + b)));
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("A aparut o eroare la trimiterea de mail. Emailul nu a fost trimis!");
            }
        }

        public static void SendMailActiuniInformari(DataSet resultDataSet, int returnValue, System.Net.Mail.Attachment[] attach = null)
        {
            string subjectEmail = "";
            string bodyEmail = "";
            string emails = "";

            if (resultDataSet.Tables.Count > 1)
            {
                List<string> mailList = new List<string>();
                foreach (DataRow dr in resultDataSet.Tables[1].Rows)
                {
                    subjectEmail = UtilsGeneral.ToString(dr["SubjectEmail"]);
                    bodyEmail = UtilsGeneral.ToString(dr["BodyEmail"]);
                    emails = UtilsGeneral.ToString(dr["Email"]);

                    if (returnValue == 0 && !String.IsNullOrEmpty(emails))
                    {
                        List<string> emailsList = new List<string>();
                        foreach (string detail in emails.Split(';'))
                        {
                            if (String.IsNullOrEmpty(detail)) continue;
                            emailsList.Add(detail);
                            if (!mailList.Contains(detail))
                            {
                                mailList.Add(detail);
                            }
                        }
                        if (emailsList.Count > 0)
                        {
                            if (null == attach)
                                UtilsGeneral.SendCustomEmail(emailsList, subjectEmail, bodyEmail, false);
                            else
                                UtilsGeneral.SendCustomEmail(emailsList, subjectEmail, bodyEmail, false, attach);

                        }
                    }
                }

                if (mailList.Count > 0)
                {
                    System.Windows.Forms.MessageBox.Show(String.Format("{0}: {1}", "Emailul a fost trimis la",
                                    mailList.Aggregate((a, b) => a + ", " + b)));
                }
                mailList.Clear();
            }
        }

        public static int GetTipTara(int taraId)
        {
            int tipTara = 0;
            DataSet ds = Base.DataBase.WindDatabase.ExecuteDataSet("SELECT * FROM Lista_Tari WHERE Tara_ID = " + taraId);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Tara"].ToString().ToUpper().Contains("ROMANIA"))
                {
                    tipTara = (int)TipuriTara.ROMANIA;
                    return tipTara;
                }
                if (UtilsGeneral.ToBool(ds.Tables[0].Rows[0]["UE"], false))
                {
                    tipTara = (int)TipuriTara.UE;
                    return tipTara;
                }
                else
                {
                    tipTara = (int)TipuriTara.NONUE;
                    return tipTara;
                }
            }
            return tipTara;
        }

        public static DateTime GetFirstLastDay(int month, int year, bool firstDay)
        {
            DateTime result;
            try
            {
                if (firstDay)
                {
                    result = new DateTime(year, month, 1);
                }
                else
                {
                    result = new DateTime(year, month, GetLastDayFromMonth(month, year));
                }
            }
            catch { result = DateTime.Now; }
            return result;
        }

        public static int GetLastDayFromMonth(int month, int year)
        {
            return System.DateTime.DaysInMonth(year, month);
        }
        #region Enums
        public enum TipuriTara
        {
            UE = 1,
            NONUE = 2,
            ROMANIA = 3,
            UE_VIES = 4
        }
        #endregion

        #endregion
    }

    public class DataTableHelper : IEnumerable
    {
        #region Members

        private DataRowCollection _dataRowCollection = null;

        #endregion

        #region Constructors

        public DataTableHelper()
        {
        }

        private DataTableHelper(DataTable dataTable)
        {
            this._dataRowCollection = dataTable.Rows;
        }

        private DataTableHelper(DataRowCollection dataRowCollection)
        {
            this._dataRowCollection = dataRowCollection;
        }

        #endregion

        #region Casting Methods

        public static explicit operator DataTableHelper(DataTable dataTable)
        {
            return new DataTableHelper(dataTable);
        }

        public static explicit operator DataTableHelper(DataRowCollection dataRowCollection)
        {
            return new DataTableHelper(dataRowCollection);
        }

        public static void InsertClonedRow(DataTable dtDestination, DataRow drOriginal)
        {
            DataRow drDestination = dtDestination.NewRow();
            foreach (DataColumn column in dtDestination.Columns)
            {
                drDestination[column.ColumnName] = drOriginal[column.ColumnName];
            }
            dtDestination.Rows.Add(drDestination);
        }

        #endregion

        #region Methods

        public int RowsCount
        {
            get
            {
                int counter = 0;
                foreach (DataRow dataRow in this._dataRowCollection)
                {
                    if (dataRow.RowState != DataRowState.Deleted)
                        counter++;
                }
                return counter;
            }
        }

        public DataRow GetRow(int index)
        {
            int countNotDeleted = -1;
            int realCounter = -1;
            while (countNotDeleted != index)
            {
                realCounter++;
                if (!(this._dataRowCollection[realCounter].RowState == DataRowState.Deleted))
                    countNotDeleted++;
            }
            return this._dataRowCollection[realCounter];
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return new DataTableHelperEnumerator(this);
        }

        #endregion

        public class DataTableHelperEnumerator : IEnumerator
        {
            #region Members

            private DataTableHelper item;
            private int index = -1;

            #endregion

            #region Constructor

            public DataTableHelperEnumerator(DataTableHelper dataTableHelperObj)
            {
                this.item = dataTableHelperObj;
            }

            #endregion


            #region IEnumerator Members

            public void Reset()
            {
                index = -1;
            }

            public object Current
            {
                get
                {
                    return this.item.GetRow(index);
                }
            }

            public bool MoveNext()
            {
                if (index < this.item.RowsCount - 1)
                {
                    index++;
                    return true;
                }
                else
                    return false;
            }

            #endregion
        }
    }
}
