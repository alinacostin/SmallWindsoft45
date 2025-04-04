using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

using System.Xml.Schema;
using System.Data;
using System.Globalization;
using System.Reflection;
namespace Base.Utils
{
    public static class XmlUtil
    {
        public static void ValidateSchema(XDocument doc, string strFileName, string strXmlScemaEmbededResource, Assembly ass)
        {

           

            

            var names = ass.GetManifestResourceNames();
            var name = names.First(n => n.Contains(strXmlScemaEmbededResource));


            var lstErrs = new List<string>();
            using (XmlTextReader tr = new XmlTextReader(ass.GetManifestResourceStream(name)))
            {

                var schemas = new XmlSchemaSet();
                schemas.Add("", tr);
                doc.Validate(schemas, (o, e) => lstErrs.Add(e.Message), true);
            }
            if (lstErrs.Count > 0)
            {
                string[] args = 
                {
                    strFileName,
                    string.Join("\n",lstErrs.ToArray())
                };
                string strMsg = string.Format("Au fost semnalate urmatoarele erori privind validarea fisierului {0}: \n{1}", args);
                throw new Exception(strMsg);
            }
        }
        public static XElement GetElement(string strElementName, Dictionary<string, string> dic, DataRow dr)
        {
            XElement el = new XElement(XName.Get(strElementName));
            XElement[] elms = GetElements(dic, dr);
            el.Add(elms);

            return el;
        }
        public static XElement[] GetElements(Dictionary<string, string> dic, DataRow dr)
        {
            XElement[] elms = dic.Select(kp => GetElement(kp, dr)).ToArray();
            return elms;
        }
        public static XElement GetElement(KeyValuePair<string, string> kp, DataRow dr)
        {
            XElement el = new XElement(XName.Get(kp.Key));
            el.Value = dr[kp.Value].ToString();
            return el;
        }
        public static XElement GetElement(string strElementName)
        {
            XName name = XName.Get(strElementName);
            XElement element = new XElement(name);
            return element;
        }
        public static XElement GetElementFromNom(DataSet dataSupport, DataRow dr, string strElementName, string strIdName, string strIdNameInSuportTable, string strSuportTableName, string strSuportTableFieldName)
        {
            try
            {
                //string strField = dr[strIdName].ToString();
                string strId = dr[strIdName].ToString();
                //string strId = DataRowMaster[strIdName].ToString();
                string strCondition = string.Format("{0} = {1}", strIdNameInSuportTable, strId);
                DataRow rwProduct = dataSupport.Tables[strSuportTableName].Select(strCondition)[0];
                string strVal = rwProduct[strSuportTableFieldName].ToString();
                var el = GetElement(strElementName, strVal);
                return el;
            }
            catch
            {
                throw;
            }
        }
        public static XElement GetElement(string strName, object value)
        {
            XElement el = GetElement(strName);
            el.Value = value.ToString();
            return el;
        }
        public static XElement GetElementFromXml(string strXml)
        {
            var el = XElement.Parse(strXml);
            return el;
        }
    }
}
