using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace DalXml
{
    public class XmlTools
    {
        internal static string directory = @"..\..\..\..\Data\";
        public XmlTools()
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
        #region SaveLoadWithXElement
        public static void SaveListToXmlElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(directory + filePath);
            }
            catch (Exception ex)
            {
                throw;//new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        public  static XElement LoadListFromXmlElement(string filePath)
        {
            try
            {
                if (File.Exists(directory + filePath))
                {
                    return XElement.Load(directory + filePath);
                }
                else
                {
                    XElement rootElem = new XElement(directory + filePath);
                    rootElem.Save(directory + filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw;// new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
        #region SaveLoadWithXMLSerializer
        public static void SaveListToXmlSerializer<T>(IEnumerable<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(directory + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw;//new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static List<T> LoadListFromXmlSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(directory + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(directory + filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw;//new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
    }
}