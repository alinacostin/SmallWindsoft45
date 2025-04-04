using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Web;
using System.Linq;
using Base.BaseUtils;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace Base.DataBase
{
    public class UtilsXML
    {

        public static string RowToXML(DataRow dataRow)
        {
            return RowToXML(dataRow, false);
        }

        public static string RowToXML(DataRow dataRow, bool useRowState)
        {
            return RowToXML(dataRow, useRowState, null);
        }

        public static string RowToXML(DataRow dataRow, bool useRowState, string[] SkippedFields)
        {
            string xmlString = "", strState = "-1";
            DataTable dataTable = dataRow.Table;
            DataRowVersion drv = DataRowVersion.Default;

            switch (dataRow.RowState)
            {
                case DataRowState.Added: strState = "0"; drv = DataRowVersion.Default;
                    break;
                case DataRowState.Deleted: strState = "1"; drv = DataRowVersion.Original;
                    break;
                case DataRowState.Modified: strState = "2"; drv = DataRowVersion.Default;
                    break;
                case DataRowState.Detached: strState = "0"; drv = DataRowVersion.Default;
                    break;
            }

            bool existsSkippedFields;
            if (SkippedFields == null) existsSkippedFields = false;
            else
            {
                if (SkippedFields.Length == 0) existsSkippedFields = false;
                else existsSkippedFields = true;
            }

            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                string columnName = dataColumn.ColumnName;
                if (existsSkippedFields)
                {
                    bool skipColumn = false;
                    foreach (string skippedField in SkippedFields)
                    {
                        if (columnName.Trim().ToUpper() == skippedField.Trim().ToUpper())
                        {
                            skipColumn = true;
                            break;
                        }
                    }
                    if (skipColumn) continue;
                }

                if (dataRow[dataColumn, drv] != DBNull.Value)
                {
                    string strDbValue;
                    if (dataColumn.DataType == typeof(bool) || dataColumn.DataType == typeof(Boolean))
                    {
                        bool dbValue = Base.BaseUtils.UtilsGeneral.ToBool(dataRow[dataColumn, drv]);
                        if (dbValue)
                            strDbValue = "1";
                        else
                            strDbValue = "0";
                    }
                    else if (dataColumn.DataType == typeof(DateTime))
                    {
                        DateTime dbValue = UtilsGeneral.ToDateTime(dataRow[dataColumn, drv]);
                        strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (dataColumn.DataType == typeof(Decimal))
                    {
                        Decimal dbValue = UtilsGeneral.NZDecimal(dataRow[dataColumn, drv]);
                        strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (dataColumn.DataType == typeof(Double))
                    {
                        Double dbValue = UtilsGeneral.NZDouble(dataRow[dataColumn, drv]);
                        strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (dataColumn.DataType == typeof(Byte[]) || dataColumn.DataType == typeof(byte[]))
                    {
                        byte[] dbValue = (byte[])dataRow[dataColumn, drv];
                        strDbValue = WindDatabase.FormatTimestamp(dbValue);
                    }
                    else if (dataColumn.DataType == typeof(String) || dataColumn.DataType == typeof(string))
                    {
                        strDbValue = dataRow[dataColumn, drv].ToString().Replace("'", "''");
                        strDbValue = HttpUtility.HtmlEncode(strDbValue);
                    }
                    else
                    {
                        strDbValue = dataRow[dataColumn, drv].ToString();
                    }
                    xmlString += columnName + "=\"" + strDbValue + "\" ";
                }
                else if (dataColumn.DataType == typeof(int) || dataColumn.DataType == typeof(Int16) || dataColumn.DataType == typeof(Int32) || dataColumn.DataType == typeof(Int64) || dataColumn.DataType == typeof(Decimal) || dataColumn.DataType == typeof(Double))
                {
                    xmlString += columnName + "=\"0\" ";
                }
                else
                    xmlString += columnName + "=\"\" ";
            }

            if (useRowState)
                xmlString += "RowState=\"" + strState + "\" "; //1 = deleted; 2 = modified; 0 = new
            else
                xmlString += "RowState=\"-1\" ";

            return xmlString;
        }

        public static string RowToXML(DataRow dataRow, bool useRowState, string[] Fields, bool onlyFields )
        {
            string xmlString = "", strState = "-1";
            DataTable dataTable = dataRow.Table;
            DataRowVersion drv = DataRowVersion.Default;

            switch (dataRow.RowState)
            {
                case DataRowState.Added: strState = "0"; drv = DataRowVersion.Default;
                    break;
                case DataRowState.Deleted: strState = "1"; drv = DataRowVersion.Original;
                    break;
                case DataRowState.Modified: strState = "2"; drv = DataRowVersion.Default;
                    break;
                case DataRowState.Detached: strState = "0"; drv = DataRowVersion.Default;
                    break;
            }

            bool existsFields;
            if (Fields == null) return xmlString;
            else
            {
                if (Fields.Length == 0) existsFields = false;
                else existsFields = true;
            }

            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                string columnName = dataColumn.ColumnName;
                if (existsFields)
                {
                    bool skipColumn = true;

                    foreach (string includeField in Fields)
                    {
                        if (columnName.Trim().ToUpper() == includeField.Trim().ToUpper())
                        {
                            skipColumn = false;
                            break;
                        }
                    }
                    if (skipColumn) continue;
                }

                if (dataRow[dataColumn, drv] != DBNull.Value)
                {
                    string strDbValue;
                    if (dataColumn.DataType == typeof(bool) || dataColumn.DataType == typeof(Boolean))
                    {
                        bool dbValue = Base.BaseUtils.UtilsGeneral.ToBool(dataRow[dataColumn, drv]);
                        if (dbValue)
                            strDbValue = "1";
                        else
                            strDbValue = "0";
                    }
                    else if (dataColumn.DataType == typeof(DateTime))
                    {
                        DateTime dbValue = UtilsGeneral.ToDateTime(dataRow[dataColumn, drv]);
                        strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (dataColumn.DataType == typeof(Decimal))
                    {
                        Decimal dbValue = UtilsGeneral.NZDecimal(dataRow[dataColumn, drv]);
                        strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (dataColumn.DataType == typeof(Double))
                    {
                        Double dbValue = UtilsGeneral.NZDouble(dataRow[dataColumn, drv]);
                        strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (dataColumn.DataType == typeof(Byte[]) || dataColumn.DataType == typeof(byte[]))
                    {
                        byte[] dbValue = (byte[])dataRow[dataColumn, drv];
                        strDbValue = WindDatabase.FormatTimestamp(dbValue);
                    }
                    else if (dataColumn.DataType == typeof(String) || dataColumn.DataType == typeof(string))
                    {
                        strDbValue = dataRow[dataColumn, drv].ToString().Replace("'", "''");
                        strDbValue = HttpUtility.HtmlEncode(strDbValue);
                    }
                    else
                    {
                        strDbValue = dataRow[dataColumn, drv].ToString();
                    }
                    xmlString += columnName + "=\"" + strDbValue + "\" ";
                }
                else if (dataColumn.DataType == typeof(int) || dataColumn.DataType == typeof(Int16) || dataColumn.DataType == typeof(Int32) || dataColumn.DataType == typeof(Int64) || dataColumn.DataType == typeof(Decimal) || dataColumn.DataType == typeof(Double))
                {
                    xmlString += columnName + "=\"0\" ";
                }
                else
                    xmlString += columnName + "=\"\" ";
            }

            if (useRowState)
                xmlString += "RowState=\"" + strState + "\" "; //1 = deleted; 2 = modified; 0 = new
            else
                xmlString += "RowState=\"-1\" ";

            return xmlString;
        }


        //public static string RowToXML(DataRow dataRow, bool useRowState, bool withoutTimeStamp)
        //{
        //    string xmlString = "", strState = "-1";
        //    DataTable dataTable = dataRow.Table;
        //    DataRowVersion drv = DataRowVersion.Default;

        //    switch (dataRow.RowState)
        //    {
        //        case DataRowState.Added: strState = "0"; drv = DataRowVersion.Default;
        //            break;
        //        case DataRowState.Deleted: strState = "1"; drv = DataRowVersion.Original;
        //            break;
        //        case DataRowState.Modified: strState = "2"; drv = DataRowVersion.Default;
        //            break;
        //        case DataRowState.Detached: strState = "0"; drv = DataRowVersion.Default;
        //            break;
        //    }

        //    foreach (DataColumn dataColumn in dataTable.Columns)
        //    {
        //        string columnName = dataColumn.ColumnName;
        //        if (dataRow[dataColumn, drv] != DBNull.Value)
        //        {
        //            string strDbValue;
        //            if (dataColumn.DataType == typeof(bool))
        //            {
        //                bool dbValue = UtilsGeneral.ToBool(dataRow[dataColumn, drv]);
        //                if (dbValue)
        //                    strDbValue = "1";
        //                else
        //                    strDbValue = "0";
        //            }
        //            else if (dataColumn.DataType == typeof(DateTime))
        //            {
        //                DateTime dbValue = UtilsGeneral.ToDateTime(dataRow[dataColumn, drv]);
        //                strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
        //            }
        //            else if (dataColumn.DataType == typeof(Decimal))
        //            {
        //                Decimal dbValue = UtilsGeneral.NZDecimal(dataRow[dataColumn, drv]);
        //                strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
        //            }
        //            else if (dataColumn.DataType == typeof(Double))
        //            {
        //                Double dbValue = UtilsGeneral.NZDouble(dataRow[dataColumn, drv]);
        //                strDbValue = dbValue.ToString(CultureInfo.InvariantCulture);
        //            }
        //            else if (dataColumn.DataType == typeof(Byte[]) || dataColumn.DataType == typeof(byte[]))
        //            {
        //                byte[] dbValue = (byte[])dataRow[dataColumn, drv];
        //                strDbValue = WindDatabase.FormatTimestamp(dbValue);
        //            }
        //            else if (dataColumn.DataType == typeof(String) || dataColumn.DataType == typeof(string))
        //            {
        //                strDbValue = dataRow[dataColumn, drv].ToString().Replace("'", "''");
        //                strDbValue = HttpUtility.HtmlEncode(strDbValue);
        //            }
        //            else
        //            {
        //                strDbValue = dataRow[dataColumn, drv].ToString();
        //            }
        //            //if (columnName == "FacturaRow_ID")
        //            //    xmlString += columnName + strDbValue;
        //            //else
        //            //    xmlString += columnName + "=\"" + strDbValue + "\" ";
        //            xmlString += columnName + "=\"" + strDbValue + "\" ";
        //        }
        //        else if (dataColumn.DataType == typeof(int) || dataColumn.DataType == typeof(Int16) || dataColumn.DataType == typeof(Int32) || dataColumn.DataType == typeof(Int64) || dataColumn.DataType == typeof(Decimal) || dataColumn.DataType == typeof(Double))
        //        {
        //            xmlString += columnName + "=\"0\" ";
        //        }
        //        else
        //            xmlString += columnName + "=\"\" ";
        //    }

        //    if (useRowState)
        //        xmlString += "RowState=\"" + strState + "\" "; //1 = deleted; 2 = modified; 0 = new
        //    else
        //        xmlString += "RowState=\"-1\" ";

        //    return xmlString;
        //}


    }
}
