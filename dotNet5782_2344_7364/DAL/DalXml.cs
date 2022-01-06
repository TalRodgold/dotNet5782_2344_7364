using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Linq;
using DalObjects;

namespace DalXml
{
    public sealed class DalXml : IDal
    {
        private static readonly Lazy<DalXml> instance = new Lazy<DalXml>(() => new DalXml());// using lazy to improve performance and avoid wasteful computation, and reduce program memory requirements.  
        static DalXml() { }// Explicit static constructor to ensure instance initialization
        public static IDal Instance { get => instance.Value; } // The public Instance property to use
        public DalXml() // constructor.
        {
            DataSource.Initialize();
        }
        internal static string BaseStationsPath = @"BaseStation.xml";
        internal static string CustomersPath = @"Customers.xml";
        internal static string DronesPath = @"Drones.xml";
        internal static string DroneChargesPath = @"DroneCharges.xml";
        internal static string ParcelsPath = @"Parcels.xml";
        internal static string ConfigPath = @"Config.xml";

        #region// add base station
        public void AddBaseStation(int? id, string name, int chargeSlots, double longtitude, double latitude)
        {

            XElement Root = XmlTools.LoadListFromXmlElement(BaseStationsPath);
            XElement newBaseStation = new XElement("BaseStation"
                   , new XElement("Id", id)
                   , new XElement("Name", name)
                   , new XElement("ChargeSlots", chargeSlots)
                   , new XElement("Longtitude", longtitude)
                   , new XElement("Latitude", latitude));
            Root.Add(newBaseStation);
            XmlTools.SaveListToXmlElement(Root, BaseStationsPath);
        }
        #endregion
        #region// add customer
        public void AddCustomer(int? id, string name, string phone, double longtitude, double latitude)
        {
            XElement Root = XmlTools.LoadListFromXmlElement(CustomersPath);
            XElement newCustomer = new XElement("Customer"
                   , new XElement("Id", id)
                  , new XElement("Name", name)
                  , new XElement("Phone", phone)
                  , new XElement("Longtitude", longtitude)
                  , new XElement("Latitude", latitude));
            Root.Add(newCustomer);
            XmlTools.SaveListToXmlElement(Root, CustomersPath);
        }
        #endregion
        #region// add drone
        public void AddDrone(int? id, string model, WeightCategories maxWeight)
        {
            XElement Root = XmlTools.LoadListFromXmlElement(DronesPath);
            XElement newDrone = new XElement("Drone"
                   , new XElement("Id", id)
                   , new XElement("Model", model)
                   , new XElement("MaxWeight", maxWeight));
            Root.Add(newDrone);
            XmlTools.SaveListToXmlElement(Root, DronesPath);
        }
        #endregion
        #region// add drone charge
        public void AddDroneCharge(int? droneId, int? stationId, DateTime? cuerrentTime)
        {
            XElement Root = XmlTools.LoadListFromXmlElement(DroneChargesPath);
            XElement newDroneCharge = new XElement("DroneCharge"
                   , new XElement("DroneId", droneId)
                   , new XElement("StationId", stationId)
                   , new XElement("TimeOfStartCharging", cuerrentTime));
            Root.Add(newDroneCharge);
            XmlTools.SaveListToXmlElement(Root, DroneChargesPath);
        }
        #endregion
        #region// add parcel
        public int? AddParcel(int? senderId, int? targetId, WeightCategories weight, Priorities priority, DateTime? request, int? droneId, DateTime? schedual, DateTime? pickUp, DateTime? deliverd)
        {
            XElement configRoot = XmlTools.LoadListFromXmlElement(ConfigPath);
            int parcelId = int.Parse(configRoot.Element("ParcelId").Value);
            XElement Root = XmlTools.LoadListFromXmlElement(ParcelsPath);
            XElement newParcel = new XElement("Parcel"
                   , new XElement("Id", parcelId)
                   , new XElement("SenderId", senderId)
                   , new XElement("ReciverId", targetId)
                   , new XElement("Weight", weight)
                   , new XElement("Priority", priority)
                   , new XElement("CreatingTime", request)
                   , new XElement("DroneId", droneId)
                   , new XElement("AssociatedTime", schedual)
                   , new XElement("PickedUp", pickUp)
                   , new XElement("Deliverd", deliverd));
            Root.Add(newParcel);
            XmlTools.SaveListToXmlElement(Root, ParcelsPath);
            configRoot.Element("ParcelId").Value = (++parcelId).ToString();
            XmlTools.SaveListToXmlElement(configRoot, ConfigPath);
            return --parcelId;
        }
        #endregion

        #region// delete base station
        public void DeleteBaseStation(int? id)
        {
            XElement baseStationElement;
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(BaseStationsPath);
                baseStationElement = (from basestation in Root.Elements()
                                      where int.Parse(basestation.Element("Id").Value) == id
                                      select basestation).FirstOrDefault();
                if (baseStationElement.Equals(null))
                    throw new IdNotExsistException("base station", id);
                baseStationElement.Remove();
                XmlTools.SaveListToXmlElement(Root, BaseStationsPath);
                //return true;
            }
            catch (Exception)
            {
                //return false;
            }
        }
        #endregion
        #region// delete customer
        public bool DeleteCustomer(int? id)
        {
            XElement CustomerElement;
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(CustomersPath);
                CustomerElement = (from customer in Root.Elements()
                                   where int.Parse(customer.Element("Id").Value) == id
                                   select customer).FirstOrDefault();
                if (CustomerElement.Equals(null))
                    throw new IdNotExsistException("customer", id);
                CustomerElement.Remove();
                XmlTools.SaveListToXmlElement(Root, CustomersPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region// delete drone
        public bool DeleteDrone(int? id)
        {
            XElement DroneElement;
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(DronesPath);
                DroneElement = (from drone in Root.Elements()
                                where int.Parse(drone.Element("Id").Value) == id
                                select drone).FirstOrDefault();
                if (DroneElement.Equals(null))
                {
                    throw new IdNotExsistException("drone", id);
                }
                DroneElement.Remove();
                XmlTools.SaveListToXmlElement(Root, DronesPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region// delete drone charge
        public bool DeleteDroneCharge(int? id)
        {
            XElement DroneChargeElement;
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(DroneChargesPath);
                DroneChargeElement = (from droneCharge in Root.Elements()
                                      where XmlTools.ToNullableInt(droneCharge.Element("Id").Value) == id
                                      select droneCharge).FirstOrDefault();
                if (DroneChargeElement.Equals(null))
                    throw new IdNotExsistException("drone charge", id);
                DroneChargeElement.Remove();
                XmlTools.SaveListToXmlElement(Root, DroneChargesPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region// delete parcel
        public void DeleteParcel(int? id)
        {
            XElement parcelElement;
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(ParcelsPath);
                parcelElement = (from parcel in Root.Elements()
                                 where int.Parse(parcel.Element("Id").Value) == id
                                 select parcel).FirstOrDefault();
                if (parcelElement.Equals(null))
                {
                    throw new IdNotExsistException("parcel", id);
                }
                parcelElement.Remove();
                XmlTools.SaveListToXmlElement(Root, ParcelsPath);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region// update parcel Pickup
        public void UpdateParclePickup(int? id)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(ParcelsPath);
                XElement parcelElement = (from parcel in Root.Elements()
                                          where int.Parse(parcel.Element("Id").Value) == id
                                          select parcel).FirstOrDefault();
                if (parcelElement.Equals(null))
                {
                    throw new IdNotExsistException("parcel", id);
                }
                parcelElement.Element("PickedUp").Value = DateTime.Now.ToString();
                XmlTools.SaveListToXmlElement(Root, ParcelsPath);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("parcel", id);
            }
        }
        #endregion
        #region// update parcel delivery
        public void UpdateParcleDelivery(int? id)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(ParcelsPath);//
                XElement parcelElement = (from parcel in Root.Elements()
                                          where int.Parse(parcel.Element("Id").Value) == id
                                          select parcel).FirstOrDefault();
                if (parcelElement.Equals(null))
                {
                    throw new IdNotExsistException("parcel", id);
                }
                parcelElement.Element("Deliverd").Value = DateTime.Now.ToString();
                XmlTools.SaveListToXmlElement(Root, ParcelsPath);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("parcel", id);
            }
        }
        #endregion
        #region//update drones charging 
        public void UpdateDroneCharge(int? droneId, int? stationId, DateTime? CurrentTime)
        {
            try
            {
                AddDroneCharge(droneId, stationId, CurrentTime);
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
        public void UpdateDroneModel(int? id, string newModel) // update drones model
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(DronesPath);//
                XElement droneElement = (from drone in Root.Elements()
                                         where int.Parse(drone.Element("Id").Value) == id
                                         select drone).FirstOrDefault();
                droneElement.Element("Name").Value = newModel;
                XmlTools.SaveListToXmlElement(Root, DronesPath);

            }
            catch (Exception)
            {
                throw new IdNotExsistException("drone", id);
            }
        }
        #endregion
        #region// update base station name
        public void UpdateBaseStationName(int? id, string name)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(BaseStationsPath);//
                XElement stationElement = (from station in Root.Elements()
                                           where int.Parse(station.Element("Id").Value) == id
                                           select station).FirstOrDefault();
                stationElement.Element("Name").Value = name;
                XmlTools.SaveListToXmlElement(Root, BaseStationsPath);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("base station", id);
            }
        }
        #endregion
        #region// update base stations number of free charging slots
        public void UpdateBaseStationNumOfFreeDroneCharges(int? id, int newnum) // update base stations number of free charging slots
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(BaseStationsPath);//
                XElement stationElement = (from station in Root.Elements()
                                           where int.Parse(station.Element("Id").Value) == id
                                           select station).FirstOrDefault();
                stationElement.Element("ChargeSlots").Value = newnum.ToString();
                XmlTools.SaveListToXmlElement(Root, BaseStationsPath);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("base station", id);
            }
        }
        #endregion
        #region// update number of charging slots
        public void UpdateChargingSlotsNumber(int? id, int numberOfChargingSlots) // update number of charging slots
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(BaseStationsPath);//
                XElement stationElement = (from station in Root.Elements()
                                           where int.Parse(station.Element("Id").Value) == id
                                           select station).FirstOrDefault();
                int count = GetListOfDroneCharge(element => element.StationId == id).ToList().Count();
                if (count > numberOfChargingSlots) // if the new number is smaller than the number of drones charging currently
                {
                    throw new SizeProblemException("bigger", count); // throw exception
                }
                stationElement.Element("ChargeSlots").Value = (numberOfChargingSlots - count).ToString();
                XmlTools.SaveListToXmlElement(Root, BaseStationsPath);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("base station", id);
            }
        }
        #endregion
        #region// update customers name
        public void UpdateCustomerName(int? id, string name)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(CustomersPath);//
                XElement customerElement = (from customer in Root.Elements()
                                            where int.Parse(customer.Element("Id").Value) == id
                                            select customer).FirstOrDefault();
                customerElement.Element("Name").Value = name;
                XmlTools.SaveListToXmlElement(Root, CustomersPath);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("customer", id);
            }
        }
        #endregion
        #region//update customers phone number
        public void UpdateCustomerPhone(int? id, string phone) // update customers phone number
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(CustomersPath);//
                XElement customerElement = (from customer in Root.Elements()
                                            where int.Parse(customer.Element("Id").Value) == id
                                            select customer).FirstOrDefault();
                customerElement.Element("Phone").Value = phone;
                XmlTools.SaveListToXmlElement(Root, CustomersPath);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("customer", id);
            }
        }
        #endregion
        #region//releas drone from charging
        public void ReleaseDroneCharge(int? droneId, int? stationId) // releas drone from charging
        {
            try
            {
                if (!DeleteDroneCharge(droneId))
                {
                    throw new IdNotExsistException("drone", droneId);
                }
                XElement Root = XmlTools.LoadListFromXmlElement(BaseStationsPath);//
                XElement stationElement = (from station in Root.Elements()
                                           where int.Parse(station.Element("Id").Value) == stationId
                                           select station).FirstOrDefault();
                stationElement.Element("ChargeSlots").Value = (int.Parse(stationElement.Element("ChargeSlots").Value) + 1).ToString();
                XmlTools.SaveListToXmlElement(Root, BaseStationsPath);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("base station", stationId);
            }
        }
        #endregion
        #region// associate a drone to a parcel
        public void AssociateDroneToParcel(int? droneId, int? parcleId) // associate a drone to a parcel 
        {
            try
            {
                GetDrone(droneId);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("drone", droneId);
            }
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(ParcelsPath);//
                XElement parcelElement = (from parcel in Root.Elements()
                                          where int.Parse(parcel.Element("Id").Value) == parcleId
                                          select parcel).FirstOrDefault();
                parcelElement.Element("DroneId").Value = droneId.ToString();
                parcelElement.Element("AssociatedTime").Value = DateTime.Now.ToString();
                XmlTools.SaveListToXmlElement(Root, ParcelsPath);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("parcel", parcleId);
            }
        }
        #endregion

        #region// get base station
        public BaseStation GetBaseStation(int? id)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(BaseStationsPath);//
                XElement stationElement = (from station in Root.Elements()
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
        public Customer GetCustomer(int? id)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(CustomersPath);//
                XElement customerElement = (from customer in Root.Elements()
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
        public Drone GetDrone(int? id)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(DronesPath);
                XElement droneElement = (from drone in Root.Elements()
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
        public DroneCharge GetdroneCharge(int? id)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(DroneChargesPath);//
                XElement droneChargeElement = (from droneCharge in Root.Elements()
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
        public Parcel GetParcel(int? id, Predicate<Parcel> predicate = null)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(ParcelsPath);//
                if (predicate == null)
                {
                    XElement parcelElement = (from parcel in Root.Elements()
                                              where int.Parse(parcel.Element("Id").Value) == id
                                              select parcel).FirstOrDefault();
                    return new Parcel()
                    {
                        Id = int.Parse(parcelElement.Element("Id").Value),
                        SenderId = int.Parse(parcelElement.Element("SenderId").Value),
                        ReciverId = int.Parse(parcelElement.Element("ReciverId").Value),
                        Weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), parcelElement.Element("Weight").Value),
                        Priority = (Priorities)Enum.Parse(typeof(Priorities), parcelElement.Element("Priority").Value),
                        CreatingTime = XmlTools.ToNullableDateTime(parcelElement.Element("CreatingTime").Value),
                        DroneId = XmlTools.ToNullableInt(parcelElement.Element("DroneId").Value),
                        AssociatedTime = XmlTools.ToNullableDateTime(parcelElement.Element("AssociatedTime").Value),
                        PickedUp = XmlTools.ToNullableDateTime(parcelElement.Element("PickedUp").Value),
                        Deliverd = XmlTools.ToNullableDateTime(parcelElement.Element("Deliverd").Value)
                    };
                }
                Func<Parcel, bool> func = new Func<Parcel, bool>(predicate);
                return GetListOfParcel().FirstOrDefault(func);
            }
            catch (Exception)
            {
                throw new IdNotExsistException("parcel", id);
            }

        }
        #endregion

        #region// get list of base stations
        public IEnumerable<BaseStation> GetListOfBaseStation(Predicate<BaseStation> predicate = null)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(BaseStationsPath);//
                List<BaseStation> baseStationsList;
                baseStationsList = (from station in Root.Elements()
                                    select new BaseStation()
                                    {
                                        Id = int.Parse(station.Element("Id").Value),
                                        Name = station.Element("Name").Value,
                                        ChargeSlots = int.Parse(station.Element("ChargeSlots").Value),
                                        Longtitude = double.Parse(station.Element("Longtitude").Value),
                                        Latitude = double.Parse(station.Element("Latitude").Value)

                                    }).ToList();
                if (predicate == null)
                {
                    return baseStationsList.ToList();
                }
                return baseStationsList.FindAll(predicate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
        #region// get list of customers
        public IEnumerable<Customer> GetListOfCustomer(Predicate<Customer> predicate = null)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(CustomersPath);//
                List<Customer> customersList;
                customersList = (from customer in Root.Elements()
                                 select new Customer()
                                 {
                                     Id = int.Parse(customer.Element("Id").Value),
                                     Name = customer.Element("Name").Value,
                                     Phone = (customer.Element("Phone").Value),
                                     Longtitude = double.Parse(customer.Element("Longtitude").Value),
                                     Latitude = double.Parse(customer.Element("Latitude").Value)

                                 }).ToList();
                if (predicate == null)
                {
                    return customersList.ToList();
                }
                return customersList.FindAll(predicate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
        #region// get list of drones
        public IEnumerable<Drone> GetListOfDrone(Predicate<Drone> predicate = null)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(DronesPath);
                List<Drone> dronesList = new List<Drone>();
                dronesList = (from drone in Root.Elements()
                              select new Drone()
                              {
                                  Id = int.Parse(drone.Element("Id").Value),
                                  Model = drone.Element("Model").Value,
                                  MaxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories), drone.Element("MaxWeight").Value)
                              }).ToList();
                if (predicate == null)
                {
                    return dronesList.ToList();
                }
                return dronesList.FindAll(predicate).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion
        #region// get list of drone charges
        public IEnumerable<DroneCharge> GetListOfDroneCharge(Predicate<DroneCharge> predicate = null)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(DroneChargesPath);//
                List<DroneCharge> droneChargesList;
                droneChargesList = (from droneCharge in Root.Elements()
                                    select new DroneCharge()
                                    {
                                        DroneId = int.Parse(droneCharge.Element("DroneId").Value),
                                        StationId = int.Parse(droneCharge.Element("StationId").Value),
                                        TimeOfStartCharging = DateTime.Parse(droneCharge.Element("TimeOfStartCharging").Value)
                                    }).ToList();
                if (predicate == null)
                {
                    return droneChargesList.ToList();
                }
                return droneChargesList.FindAll(predicate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region// get list of parcels
        public IEnumerable<Parcel> GetListOfParcel(Predicate<Parcel> predicate = null)
        {

            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(ParcelsPath);//
                List<Parcel> parcelsList;
                parcelsList = (from parcel in Root.Elements()
                               select new Parcel()
                               {
                                   Id = int.Parse(parcel.Element("Id").Value),
                                   SenderId = int.Parse(parcel.Element("SenderId").Value),
                                   ReciverId = int.Parse(parcel.Element("ReciverId").Value),
                                   Weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), parcel.Element("Weight").Value),
                                   Priority = (Priorities)Enum.Parse(typeof(Priorities), parcel.Element("Priority").Value),
                                   CreatingTime = DateTime.Parse(parcel.Element("CreatingTime").Value),
                                   DroneId = XmlTools.ToNullableInt(parcel.Element("DroneId").Value),
                                   AssociatedTime = XmlTools.ToNullableDateTime(parcel.Element("AssociatedTime").Value),
                                   PickedUp = XmlTools.ToNullableDateTime(parcel.Element("PickedUp").Value),
                                   Deliverd = XmlTools.ToNullableDateTime(parcel.Element("Deliverd").Value)
                               }).ToList();
                if (predicate == null)
                {
                    return parcelsList.ToList();
                }
                return parcelsList.FindAll(predicate).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
     
        #region// if drone exsists 
        public bool IfDroneExsists(int? id)  // return true if id exisists in list of drones
        {
            XElement Root = XmlTools.LoadListFromXmlElement(DronesPath);
            XElement droneElement = (from drone in Root.Elements()
                                      where int.Parse(drone.Element("Id").Value) == id
                                      select drone).FirstOrDefault();
            if (droneElement.Equals(null))
            {
                return false;
            }
            return true;
        }
        #endregion
        #region// if base station exsists 
        public bool IfBaseStationExsists(int? id)  // return  true if id exisists in list of base stations
        {
            XElement Root = XmlTools.LoadListFromXmlElement(BaseStationsPath);
            XElement baseStationElement = (from baseStation in Root.Elements()
                                     where int.Parse(baseStation.Element("Id").Value) == id
                                     select baseStation).FirstOrDefault();
            if (baseStationElement.Equals(null))
            {
                return false;
            }
            return true;
        }
        #endregion
        #region// if customer exsists
        public bool IfCustomerExsists(int? id) // return  true if id exisists in list of customers
        {
            XElement Root = XmlTools.LoadListFromXmlElement(CustomersPath);
            XElement customerElement = (from customer in Root.Elements()
                                      where int.Parse(customer.Element("Id").Value) == id
                                      select customer).FirstOrDefault();
            if (customerElement.Equals(null))
            {
                return false;
            }
            return true;
        }
        #endregion
        #region// if parcel exsists
        public bool IfParcelExsists(int? id)  // return  true if id exisists inlist of parcels
        {
            XElement Root = XmlTools.LoadListFromXmlElement(ParcelsPath);
            XElement parcelElement = (from parcel in Root.Elements()
                                      where int.Parse(parcel.Element("Id").Value) == id
                                      select parcel).FirstOrDefault();
            if (parcelElement.Equals(null))
            {
                return false;
            }
            return true;
        }
        #endregion

        #region // find a Drone by id and return all his data as Drone class 
        public BaseStation getBaseStationByDroneId(int? id)  // find a Drone by id and return all his data as Drone class 
        {
            if (!IfDroneExsists(id)) // if id doesnt exsist
            {
                throw new IdNotExsistException("drone", id); // throw exception
            }
            Func<DroneCharge, bool> func = new Func<DroneCharge, bool>(element => element.DroneId == id);
            return GetBaseStation(GetListOfDroneCharge().FirstOrDefault(func).StationId);
        }
        #endregion

        #region// find a DroneCharge by id and return all his data as DroneCharge class
        public DroneCharge GetDroneCharge(int? id = null, Predicate<DroneCharge> predicate = null)  // find a DroneCharge by id and return all his data as DroneCharge class
        {
            if (predicate == null)
            {
                if (!IfDroneExsists(id)) // if id doesnt exsist
                {
                    throw new IdNotExsistException("drone charge", id); // throw exception
                }
                Func<DroneCharge, bool> func = new Func<DroneCharge, bool>(element => element.DroneId == id);
                return GetListOfDroneCharge().FirstOrDefault(func);
            }
            Func<DroneCharge, bool> func1 = new Func<DroneCharge, bool>(predicate);
            return GetListOfDroneCharge().FirstOrDefault(func1);
        }
        #endregion

        #region//return an array of electricity data
        public double[] Electricity() // return an array of electricity data
        {
            XElement configRoot = XmlTools.LoadListFromXmlElement(ConfigPath);
            double[] array = new double[5];
            array[0] = double.Parse(configRoot.Element("ElectricityUseAvailiblity").Value);
            array[1] = double.Parse(configRoot.Element("ElectricityUseLightWeight").Value);
            array[2] = double.Parse(configRoot.Element("ElectricityUseMediumWeight").Value);
            array[3] = double.Parse(configRoot.Element("ElectricityUseHeavyWeight").Value);
            array[4] = double.Parse(configRoot.Element("DroneChargingPaste").Value);
            return array;
        }
        #endregion
    }
}


