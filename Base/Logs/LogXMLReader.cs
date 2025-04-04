using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Base.Logs
{
    public static class LogXMLReader
    {
        public static void XMLRead(StringBuilder xmlString, out StringBuilder outputXML, out List<string> impactTables)
        {
            outputXML = new StringBuilder();
            impactTables = new List<string>();
            xmlString = xmlString.Replace("\\\"", @"""");
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString.ToString())))
            {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(outputXML, ws))
                {

                    // Parse the file and display each of the nodes.
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                writer.WriteStartElement(reader.Name);
                                if (IsElementTable(reader.Name, impactTables))
                                        impactTables.Add(reader.Name);
                                if (reader.HasAttributes)
                                {
                                    for (int i = 0; i < reader.AttributeCount; i++)
                                    {
                                        reader.MoveToAttribute(i);
                                        writer.WriteAttributeString(reader.Name, reader.Value);
                                    }
                                }
                                break;
                            case XmlNodeType.Text:
                                writer.WriteString(reader.Value);
                                break;
                            case XmlNodeType.Attribute:                                
                                writer.WriteAttributeString(reader.Name, reader.Value);
                                break;
                            case XmlNodeType.XmlDeclaration:
                            case XmlNodeType.ProcessingInstruction:
                                writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                break;
                            case XmlNodeType.Comment:
                                writer.WriteComment(reader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                writer.WriteFullEndElement();
                                break;
                        }
                    }
                }
            }            
        }

        private static bool IsElementTable(string inputElement, List<string> containedTables)
        {
            if (inputElement.ToLower().Contains("root")) // no root elements
                return false;

            return (!containedTables.Contains(inputElement));
        }
    }
}
