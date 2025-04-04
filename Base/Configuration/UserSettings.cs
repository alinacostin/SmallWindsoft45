using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Base.Configuration
{
    public static class UserSettings
    {
        public static Hashtable UserSetting { get; set; }

        public static void SetUserSetting()
        {
            try
            {
                string path = GetExecutingDirectoryName();

                if (File.Exists(path))
                {
                    UserSetting = new Hashtable();
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        XmlDocument doc = new XmlDocument();
                        string xmlIn = reader.ReadToEnd();
                        reader.Close();
                        doc.LoadXml(xmlIn);

                        TraverseNode(doc.ChildNodes);
                    }
                }
            }
            catch { }

        }

        private static void TraverseNode(XmlNodeList nodes)
        {
            foreach(XmlNode node in nodes)
            {
                if (node.Name.Equals("add"))
                    UserSetting.Add
                    (
                        node.Attributes["key"].Value,
                        node.Attributes["value"].Value
                    );
                TraverseNode(node.ChildNodes);
            }
        }

        private static string GetExecutingDirectoryName()
        {
            return (new System.IO.FileInfo(Assembly.GetEntryAssembly().Location)).Directory.FullName + "\\WindUserSettings.xml";
        }
    }
}
