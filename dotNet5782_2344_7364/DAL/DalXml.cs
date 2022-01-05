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


    internal sealed class DalXml:IDal
    {
        private static readonly Lazy<DalXml> instance = new Lazy<DalXml>(() => new DalXml());// using lazy to improve performance and avoid wasteful computation, and reduce program memory requirements.  
        static DalXml() { }// Explicit static constructor to ensure instance initialization
                           // is done just before first usage
        DalXml() // constructor.
        {
            DataSource.Initialize();
            Config config = new Config();

            
        }
        internal static string BaseStationsPath = @"BaseStation.xml";
        internal static string CustomersPath = @"Customers.xml";
        internal static string DronesPath = @"Drones.xml";
        internal static string DroneChargesPath = @"DroneCharges.xml";
        internal static string ParcelsPath = @"Parcels.xml";
        internal static string ConfigPath = @"Config.xml";

        #region// add base station
        public void AddBaseStation(BaseStation baseStations)
        {

            XElement Root= XmlTools.LoadListFromXmlElement(BaseStationsPath);
            XElement newBaseStation = new XElement("BaseStation"
                   , new XElement("Id", baseStations.Id)
                   , new XElement("Name", baseStations.Name)
                   , new XElement("ChargeSlots", baseStations.ChargeSlots)
                   , new XElement("Longtitude", baseStations.Longtitude)
                   , new XElement("Latitude", baseStations.Latitude));
            Root.Add(newBaseStation);
            XmlTools.SaveListToXmlElement(Root,BaseStationsPath);
        }
        internal class Config // hold runing number for id plus electricity usege 
        {
            internal static int ParcelId = 12345; // runing number for parcel Id
            internal static double ElectricityUseAvailiblity { get; set; } = 0.1; //electricity usege for avilable
            internal static double ElectricityUseLightWeight { get; set; } = 0.2; // electricity usege for light weight
            internal static double ElectricityUseMediumWeight { get; set; } = 0.3; // electricity usege for  medium weight
            internal static double ElectricityUseHeavyWeight { get; set; } = 0.4; // electricity usege for heavy weight
            internal static double DroneChargingPaste { get; set; } = 100; // speed of drone charging per hour in %
            internal static double[] Electricity()
            {
                double[] electricity = new double[5];
                electricity[0] = DataSource.Config.ElectricityUseAvailiblity;
                electricity[1] = DataSource.Config.ElectricityUseLightWeight;
                electricity[2] = DataSource.Config.ElectricityUseMediumWeight;
                electricity[3] = DataSource.Config.ElectricityUseHeavyWeight;
                electricity[4] = DataSource.Config.DroneChargingPaste;
                return electricity;
            }
            internal static int GetParcelId()
            {
                return DataSource.Config.ParcelId ;
            }
        }
        #endregion
        #region// add parcel
        internal static void AddConfig(Config config)
        {
            List<double> list = Config.Electricity().ToList<double>();
            XmlTools.SaveListToXmlSerializer(list, ParcelsPath);
            XElement xElement=new XElement { }

        }
        #region// add customer
        internal static void Addcustomer(Customer customer)
        {
            XElement Root = XmlTools.LoadListFromXmlElement(CustomersPath);
            XElement newCustomer = new XElement("Customer"
                   , new XElement("Id", customer.Id)
                  , new XElement("Name", customer.Name)
                  , new XElement("Phone", customer.Phone)
                  , new XElement("Longtitude", customer.Longtitude)
                  , new XElement("Latitude", customer.Latitude));
            Root.Add(newCustomer);
            XmlTools.SaveListToXmlElement(Root, CustomersPath);
        }
        #endregion
        #region// add drone
        internal static void AddDrone(Drone drone)
        {
            XElement Root = XmlTools.LoadListFromXmlElement(DronesPath);
            XElement newDrone = new XElement("Drone"
                   , new XElement("Id", drone.Id)
                   , new XElement("Model", drone.Model)
                   , new XElement("MaxWeight", drone.MaxWeight));
            Root.Add(newDrone);
            XmlTools.SaveListToXmlElement(Root, DronesPath);
        }
        #endregion
        #region// add drone charge
        internal static void AddDroneCharge(DroneCharge droneCharge)
        {
            XElement Root = XmlTools.LoadListFromXmlElement(DroneChargesPath);
            XElement newDroneCharge = new XElement("DroneCharge"
                   , new XElement("DroneId", droneCharge.DroneId)
                   , new XElement("StationId", droneCharge.StationId)
                   , new XElement("TimeOfStartCharging", droneCharge.TimeOfStartCharging));
            Root.Add(newDroneCharge);
            XmlTools.SaveListToXmlElement(Root, DroneChargesPath);
        }
        #endregion
        #region// add parcel
        internal static void AddParcel(Parcel parcel)
        {
            XElement Root = XmlTools.LoadListFromXmlElement(ParcelsPath);
            XElement newParcel = new XElement("Parcel"
                   , new XElement("Id", parcel.Id)
                   , new XElement("SenderId", parcel.SenderId)
                   , new XElement("ReciverId", parcel.ReciverId)
                   , new XElement("Weight", parcel.Weight)
                   , new XElement("Priority", parcel.Priority)
                   , new XElement("CreatingTime", parcel.CreatingTime)
                   , new XElement("DroneId", parcel.DroneId)
                   , new XElement("AssociatedTime", parcel.AssociatedTime)
                   , new XElement("PickedUp", parcel.PickedUp)
                   , new XElement("Deliverd", parcel.Deliverd));
            Root.Add(newParcel);
            XmlTools.SaveListToXmlElement(Root, ParcelsPath);
        }
        #endregion

        #region// delete base station
        internal void DeleteBaseStation(int? id)
        {
            XElement baseStationElement;
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(BaseStationsPath);
                baseStationElement =(from basestation in Root.Elements()
                                           where int.Parse(basestation.Element("Id").Value) == id
                                           select basestation).FirstOrDefault();
                if (baseStationElement.Equals(null))
                    throw new IdNotExsistException("base station",id);
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
        internal static bool RemoveCustomer(int? id)
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
        internal static bool RemoveDrone(int? id)
        {
            XElement DroneElement;
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(DronesPath);
                DroneElement = (from drone in Root.Elements()
                                where int.Parse(drone.Element("Id").Value) == id
                                select drone).FirstOrDefault();
                if(DroneElement.Equals(null))
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
        internal static bool RemoveDroneCharge(int? id)
        {
            XElement DroneChargeElement;
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(DroneChargesPath);
                DroneChargeElement = (from droneCharge in Root.Elements()
                                      where int.Parse(droneCharge.Element("Id").Value) == id
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
        internal static bool RemoveParcel(int? id)
        {
            XElement parcelElement;
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(ParcelsPath);
                parcelElement = (from parcel in Root.Elements()
                                 where int.Parse(parcel.Element("Id").Value) == id
                                 select parcel).FirstOrDefault();
                if(parcelElement.Equals(null))
                {
                    throw new IdNotExsistException("parcel", id);
                }
                parcelElement.Remove();
                XmlTools.SaveListToXmlElement(Root, ParcelsPath);
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
        internal static void UpdateParcleDelivery(int? id)
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
        #region//update drones charging          //????????????????????????
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
        internal static void UpdateBaseStationName(int? id, string name)
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
        internal static void UpdateBaseStationNumOfFreeDroneCharges(int? id, int newnum) // update base stations number of free charging slots
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
        internal static void UpdateChargingSlotsNumber(int? id, int numberOfChargingSlots) // update number of charging slots
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
        internal static void UpdateCustomerName(int? id, string name)
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
        internal static void UpdateCustomerPhone(int? id, string phone) // update customers phone number
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
        internal static void ReleaseDroneCharge(int? droneId, int? stationId) // releas drone from charging
        {
            try
            {
                if (!RemoveDroneCharge(droneId))
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
        internal static BaseStation GetBaseStation(int? id)
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
        internal static Customer GetCustomer(int? id)
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
        internal static Drone Getdrone(int? id)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(DronesPath);//
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
        internal static DroneCharge GetdroneCharge(int? id)
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
        internal static Parcel GetParcel(int? id)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(ParcelsPath);//
                XElement parcelElement = (from parcel in Root.Elements()
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
        internal static IEnumerable<Customer> GetListOfCustomer(Predicate<Customer> predicate = null)
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
        internal static IEnumerable<Drone> GetListOfDrone(Predicate<Drone> predicate = null)
        {
            try
            {
                XElement Root = XmlTools.LoadListFromXmlElement(DronesPath);//
                List<Drone> dronesList;
                dronesList = (from drone in Root.Elements()
                                 select new Drone()
                                 {
                                     Id = int.Parse(drone.Element("Id").Value),
                                     Model = drone.Element("Model").Value,
                                     MaxWeight = (WeightCategories)int.Parse(drone.Element("MaxWeight").Value)
                                 }).ToList();
                if (predicate == null)
                {
                    return dronesList.ToList();
                }
                return dronesList.FindAll(predicate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
        #region// get list of drone charges
        internal static IEnumerable<DroneCharge> GetListOfDroneCharge(Predicate<DroneCharge> predicate = null)
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
        internal static IEnumerable<Parcel> GetListOfParcel(Predicate<Parcel> predicate = null)
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
                                        Weight = (WeightCategories)int.Parse(parcel.Element("Weight").Value),
                                        Priority = (Priorities)int.Parse(parcel.Element("Priority").Value),
                                        CreatingTime = DateTime.Parse(parcel.Element("CreatingTime").Value),
                                        DroneId = int.Parse(parcel.Element("DroneId").Value),
                                        AssociatedTime = DateTime.Parse(parcel.Element("AssociatedTime").Value),
                                        PickedUp = DateTime.Parse(parcel.Element("PickedUp").Value),
                                        Deliverd = DateTime.Parse(parcel.Element("Deliverd").Value)

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
        //public static IDal Instance { get => instance.Value; } // The public Instance property to use
    }
}

