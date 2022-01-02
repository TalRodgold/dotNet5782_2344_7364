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


        //internal static XElement BaseStationPathRoot;
        //internal static XElement CustomerPathRoot;
        //internal static XElement DronePathRoot;//
        //internal static XElement DroneChargePathRoot;
        //internal static XElement ParcelPathRoot;

        #region// base station loader
        //internal static void LoadBaseStations()
        //{
        //    try
        //    {
        //        BaseStationPathRoot = XElement.Load(BaseStationsPath);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //internal static List<BaseStation> GetBaseStations()
        //{
        //    LoadBaseStations();
        //    List<BaseStation> baseStations;
        //    try
        //    {
        //        baseStations = (from station in BaseStationPathRoot.Elements()
        //                        select new BaseStation()
        //                        {
        //                            Id = int.Parse(station.Element("Id").Value),
        //                            Name = station.Element("Name").Value,
        //                            ChargeSlots = int.Parse(station.Element("ChargeSlots").Value),
        //                            Longtitude = double.Parse(station.Element("Longtitude").Value),
        //                            Latitude = double.Parse(station.Element("Latitude").Value)

        //                        }).ToList();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return baseStations;
        //}
        #endregion
        static XmlTools()
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

        #region// save parcel
        //internal static void SaveParcel(List<Parcel> parcels)
        //{
        //    ParcelPathRoot = new XElement("Parcels",
        //        from parcel in parcels
        //        select new XElement("parcels"
        //           , new XElement("Id", parcel.Id)
        //           , new XElement("SenderId", parcel.SenderId)
        //           , new XElement("ReciverId", parcel.ReciverId)
        //           , new XElement("Weight", parcel.Weight)
        //           , new XElement("Priority", parcel.Priority)
        //           , new XElement("CreatingTime", parcel.CreatingTime)
        //           , new XElement("DroneId", parcel.DroneId)
        //           , new XElement("AssociatedTime", parcel.AssociatedTime)
        //           , new XElement("PickedUp", parcel.PickedUp)
        //           , new XElement("Deliverd", parcel.Deliverd)));
            
        //    ParcelPathRoot.Save(ParcelsPath);
        //}
        #endregion


   
}