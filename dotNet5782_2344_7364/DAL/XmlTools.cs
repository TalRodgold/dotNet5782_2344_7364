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


        #region// delete base station
        internal static bool RemoveBaseStation(int? id)
        {
            XElement baseStationElement;
            try
            {
                baseStationElement = (from station in BaseStationPathRoot.Elements()
                                      where int.Parse(station.Element("Id").Value) == id
                                      select station).FirstOrDefault();
                baseStationElement.Remove();
                BaseStationPathRoot.Save(BaseStationsPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region// delete customer
        internal static bool RemoveCustomer(int? id)
        {
            XElement customerElement;
            try
            {
                customerElement = (from customer in CustomerPathRoot.Elements()
                                      where int.Parse(customer.Element("Id").Value) == id
                                      select customer).FirstOrDefault();
                customerElement.Remove();
                CustomerPathRoot.Save(CustomersPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region// delete drone
        internal static bool RemoveDrone(int? id)
        {
            XElement DroneElement;
            try
            {
                DroneElement = (from drone in DronePathRoot.Elements()
                                where int.Parse(drone.Element("Id").Value) == id
                                select drone).FirstOrDefault();
                DroneElement.Remove();
                DronePathRoot.Save(DronesPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region// delete drone charge
        internal static bool RemoveDroneCharge(int? id)
        {
            XElement DroneChargeElement;
            try
            {
                DroneChargeElement = (from droneCharge in DroneChargePathRoot.Elements()
                                      where int.Parse(droneCharge.Element("DroneId").Value) == id
                                      select droneCharge).FirstOrDefault();
                DroneChargeElement.Remove();
                DroneChargePathRoot.Save(DroneChargesPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region// delete parcel
        internal static bool RemoveParcel(int? id)
        {
            XElement parcelElement;
            try
            {
                parcelElement = (from parcel in ParcelPathRoot.Elements()
                                   where int.Parse(parcel.Element("Id").Value) == id
                                   select parcel).FirstOrDefault();
                parcelElement.Remove();
                ParcelPathRoot.Save(ParcelsPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region// update parcel Pickup
        internal static void UpdateParcelPickup(int? id)
        {
            try
            {
                XElement parcelElement = (from parcel in ParcelPathRoot.Elements()
                                           where int.Parse(parcel.Element("Id").Value) == id
                                           select parcel).FirstOrDefault();
                parcelElement.Element("PickedUp").Value = DateTime.Now.ToString();
            }
            catch (Exception)
            {
                throw new IdNotExsistException("parcel", id);
            }
        }
        #endregion
        #region// update parcel delivery
        internal static void UpdateParcleDelivery(int? id)
        {
            try
            {
                XElement parcelElement = (from parcel in ParcelPathRoot.Elements()
                                          where int.Parse(parcel.Element("Id").Value) == id
                                          select parcel).FirstOrDefault();
                parcelElement.Element("Deliverd").Value = DateTime.Now.ToString();
            }
            catch (Exception)
            {
                throw new IdNotExsistException("parcel", id);
            }
        }
        #endregion
        #region//update drones charging
        internal static void UpdateDroneCharge(int? droneId, int? stationId, DateTime? CurrentTime)
        {
            try
            {
                DroneCharge newDroneCharge = new DroneCharge();
                newDroneCharge.DroneId = droneId;
                newDroneCharge.StationId = stationId;
                newDroneCharge.TimeOfStartCharging = CurrentTime;
                AddDroneCharge(newDroneCharge);
            }
            catch (Exception exception)
            {
                if (exception.Message == "base station")
                {
                    throw new IdNotExsistException("base station", stationId);

                }
                throw new IdNotExsistException("drone", droneId);
            }
        }
        #endregion
        #region// update drone model
        internal static void UpdateDroneModel(int? id, string newModel) // update drones model
        {
            try
            {
                XElement droneElement = (from drone in DronePathRoot.Elements()
                                           where int.Parse(drone.Element("Id").Value) == id
                                           select drone).FirstOrDefault();
                droneElement.Element("Name").Value = newModel;
            }
            catch (Exception)
            {
                throw new IdNotExsistException("drone", id);
            }
        }
        #endregion
        #region// update base station name
        internal static void UpdateBaseStationName(int? id, string name)
        {
            try
            {
                XElement stationElement = (from station in BaseStationPathRoot.Elements()
                                           where int.Parse(station.Element("Id").Value) == id
                                           select station).FirstOrDefault();
                stationElement.Element("Name").Value = name;
            }
            catch (Exception)
            {
                throw new IdNotExsistException("base station", id);
            }
        }
        #endregion
        #region// update base stations number of free charging slots
        internal static void UpdateBaseStationNumOfFreeDroneCharges(int? id, int newnum) // update base stations number of free charging slots
        {
            try
            {
                XElement stationElement = (from station in BaseStationPathRoot.Elements()
                                           where int.Parse(station.Element("Id").Value) == id
                                           select station).FirstOrDefault();
                stationElement.Element("ChargeSlots").Value = newnum.ToString();
            }
            catch (Exception)
            {
                throw new IdNotExsistException("base station", id);
            }
        }
        #endregion
        #region// update number of charging slots
        internal static void UpdateChargingSlotsNumber(int? id, int numberOfChargingSlots) // update number of charging slots
        {
            try
            {
                XElement stationElement = (from station in BaseStationPathRoot.Elements()
                                           where int.Parse(station.Element("Id").Value) == id
                                           select station).FirstOrDefault();
                int count = GetListOfDroneCharge(element => element.StationId == id).ToList().Count();
                if (count > numberOfChargingSlots) // if the new number is smaller than the number of drones charging currently
                {
                    throw new SizeProblemException("bigger", count); // throw exception
                }
                stationElement.Element("ChargeSlots").Value = (numberOfChargingSlots - count).ToString();
            }
            catch (Exception)
            {
                throw new IdNotExsistException("base station", id);
            }
        }
        #endregion
        #region// update customers name
        internal static void UpdateCustomerName(int? id, string name)
        {
            try
            {
                XElement customerElement = (from customer in CustomerPathRoot.Elements()
                                            where int.Parse(customer.Element("Id").Value) == id
                                            select customer).FirstOrDefault();
                customerElement.Element("Name").Value = name;
            }
            catch (Exception)
            {
                throw new IdNotExsistException("customer", id);
            }
        }
        #endregion
        #region//update customers phone number
        internal static void UpdateCustomerPhone(int? id, string phone) // update customers phone number
        {
            try
            {
                XElement customerElement = (from customer in CustomerPathRoot.Elements()
                                            where int.Parse(customer.Element("Id").Value) == id
                                            select customer).FirstOrDefault();
                customerElement.Element("Phone").Value = phone;
            }
            catch (Exception)
            {
                throw new IdNotExsistException("customer", id);
            }
        }
        #endregion
        #region//releas drone from charging
        internal static void ReleaseDroneCharge(int? droneId, int? stationId) // releas drone from charging
        {
            try
            {
                if (!RemoveDroneCharge(droneId))
                {
                    throw new IdNotExsistException("drone", droneId);
                }
                XElement stationElement = (from station in BaseStationPathRoot.Elements()
                                           where int.Parse(station.Element("Id").Value) == stationId
                                           select station).FirstOrDefault();
                stationElement.Element("ChargeSlots").Value = (int.Parse(stationElement.Element("ChargeSlots").Value) + 1).ToString();
                
            }
            catch (Exception )
            {
                throw new IdNotExsistException("base station", stationId);
            }
        }
        #endregion
        #region// associate a drone to a parcel
        internal static void AssociateDroneToParcel(int? droneId, int? parcleId) // associate a drone to a parcel 
        {
            try
            {
                Getdrone(droneId);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("drone", droneId);
            }
            try
            {
                XElement parcelElement = (from parcel in ParcelPathRoot.Elements()
                                          where int.Parse(parcel.Element("Id").Value) == parcleId
                                          select parcel).FirstOrDefault();
                parcelElement.Element("DroneId").Value = droneId.ToString();
                parcelElement.Element("AssociatedTime").Value = DateTime.Now.ToString();
            }
            catch (Exception)
            {
                throw new IdNotExsistException("parcel", parcleId);
            }
        }
        #endregion

        #region// get base station
        internal static BaseStation GetBaseStation(int? id)
        {
            try
            {
                XElement stationElement = (from station in BaseStationPathRoot.Elements()
                                           where int.Parse(station.Element("Id").Value) == id
                                           select station).FirstOrDefault();
                return new BaseStation()
                {
                    Id = int.Parse(stationElement.Element("Id").Value),
                    Name = stationElement.Element("Name").Value,
                    ChargeSlots = int.Parse(stationElement.Element("ChargeSlots").Value),
                    Longtitude = double.Parse(stationElement.Element("Longtitude").Value),
                    Latitude = double.Parse(stationElement.Element("Latitude").Value)

                };
            }
            catch (Exception)
            {
                throw new IdNotExsistException("base station", id);
            }

        }
        #endregion
        #region// get customer
        internal static Customer GetCustomer(int? id)
        {
            try
            {
                XElement customerElement = (from customer in CustomerPathRoot.Elements()
                                           where int.Parse(customer.Element("Id").Value) == id
                                           select customer).FirstOrDefault();
                return new Customer()
                {
                    Id = int.Parse(customerElement.Element("Id").Value),
                    Name = customerElement.Element("Name").Value,
                    Phone = customerElement.Element("Phone").Value,
                    Longtitude = double.Parse(customerElement.Element("Longtitude").Value),
                    Latitude = double.Parse(customerElement.Element("Latitude").Value),
                };
            }
            catch (Exception)
            {
                throw new IdNotExsistException("customer", id);
            }

        }
        #endregion
        #region// get drone
        internal static Drone Getdrone(int? id)
        {
            try
            {
                XElement droneElement = (from drone in DronePathRoot.Elements()
                                            where int.Parse(drone.Element("Id").Value) == id
                                            select drone).FirstOrDefault();
                return new Drone()
                {
                    Id = int.Parse(droneElement.Element("Id").Value),
                    Model = droneElement.Element("Name").Value,
                    MaxWeight = (WeightCategories)int.Parse(droneElement.Element("MaxWeight").Value)

                };
            }
            catch (Exception)
            {
                throw new IdNotExsistException("drone", id);
            }

        }
        #endregion
        #region// get drone charge
        internal static DroneCharge GetdroneCharge(int? id)
        {
            try
            {
                XElement droneChargeElement = (from droneCharge in DroneChargePathRoot.Elements()
                                         where int.Parse(droneCharge.Element("Id").Value) == id
                                         select droneCharge).FirstOrDefault();
                return new DroneCharge()
                {
                    DroneId = int.Parse(droneChargeElement.Element("DroneId").Value),
                    StationId = int.Parse(droneChargeElement.Element("StationId").Value),
                    TimeOfStartCharging = DateTime.Parse(droneChargeElement.Element("TimeOfStartCharging").Value)
                };
            }
            catch (Exception)
            {
                throw new IdNotExsistException("drone charge", id);
            }

        }
        #endregion
        #region// get parcel
        internal static Parcel GetParcel(int? id)
        {
            try
            {
                XElement parcelElement = (from parcel in ParcelPathRoot.Elements()
                                               where int.Parse(parcel.Element("Id").Value) == id
                                               select parcel).FirstOrDefault();
                return new Parcel()
                {
                    Id = int.Parse(parcelElement.Element("Id").Value),
                    SenderId = int.Parse(parcelElement.Element("SenderId").Value),
                    ReciverId = int.Parse(parcelElement.Element("ReciverId").Value),
                    Weight = (WeightCategories)int.Parse(parcelElement.Element("Weight").Value),
                    Priority = (Priorities)int.Parse(parcelElement.Element("Priority").Value),
                    CreatingTime = DateTime.Parse(parcelElement.Element("CreatingTime").Value),
                    DroneId = int.Parse(parcelElement.Element("DroneId").Value),
                    AssociatedTime = DateTime.Parse(parcelElement.Element("AssociatedTime").Value),
                    PickedUp = DateTime.Parse(parcelElement.Element("PickedUp").Value),
                    Deliverd = DateTime.Parse(parcelElement.Element("Deliverd").Value)
                };
            }
            catch (Exception)
            {
                throw new IdNotExsistException("parcel", id);
            }

        }
        #endregion

        #region// get list of base stations
        internal static IEnumerable<BaseStation> GetListOfBaseStation(Predicate<BaseStation> predicate = null)
        {
            List<BaseStation> baseStationList = GetBaseStations();
            if (predicate == null)
            {
                return baseStationList.ToList();
            }
            return baseStationList.FindAll(predicate).ToList();
        }
        #endregion
        #region// get list of customers
        internal static IEnumerable<Customer> GetListOfCustomer(Predicate<Customer> predicate = null) 
        {
            List<Customer> customersList = GetCustomers();
            if (predicate == null)
            {
                return customersList.ToList();
            }
            return customersList.FindAll(predicate).ToList(); 
        }
        #endregion
        #region// get list of drones
        internal static IEnumerable<Drone> GetListOfDrone(Predicate<Drone> predicate = null)
        {
            List<Drone> dronesList = GetDrones();
            if (predicate == null)
            {
                return dronesList.ToList();
            }
            return dronesList.FindAll(predicate).ToList(); 
        }
        #endregion
        #region// get list of drone charges
        internal static IEnumerable<DroneCharge> GetListOfDroneCharge(Predicate<DroneCharge> predicate = null)
        {
            List<DroneCharge> droneChargesList = GetDroneCharges();
            if (predicate == null)
            {
                return droneChargesList.ToList();
            }
            return droneChargesList.FindAll(predicate).ToList();
        }
        #endregion
        #region// get list of parcels
        internal static IEnumerable<Parcel> GetListOfParcel(Predicate<Parcel> predicate = null) 
        {
            List<Parcel> parcelsList = GetParcels();
            if (predicate == null)
            {
                return parcelsList.ToList();
            }
            return parcelsList.FindAll(predicate).ToList(); 
        } 
        #endregion
    }
}