using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Linq;

namespace DalObjects
{
    public class XmlTools
    {
        internal static string BaseStationsPath = @"\..\..\..\Data\BaseStation.xml";
        internal static string CustomersPath = @"\..\..\..\DAL\Data\Customers.xml";
        internal static string DronesPath = @"\..\..\..\Data\Drones.xml";
        internal static string DroneChargesPath = @"C:\..\..\..\Data\DroneCharges.xml";
        internal static string ParcelsPath = @"C:\..\..\..\Data\Parcels.xml";

        internal static XElement BaseStationPathRoot;
        internal static XElement CustomerPathRoot;
        internal static XElement DronePathRoot;//
        internal static XElement DroneChargePathRoot;
        internal static XElement ParcelPathRoot;

        #region// base station loader
        internal static void LoadBaseStations()
        {
            try
            {
                BaseStationPathRoot = XElement.Load(BaseStationsPath);
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal static List<BaseStation> GetBaseStations()
        {
            LoadBaseStations();
            List<BaseStation> baseStations;
            try
            {
                baseStations = (from station in BaseStationPathRoot.Elements()
                                select new BaseStation()
                                {
                                    Id = int.Parse(station.Element("Id").Value),
                                    Name = station.Element("Name").Value,
                                    ChargeSlots = int.Parse(station.Element("ChargeSlots").Value),
                                    Longtitude = double.Parse(station.Element("Longtitude").Value),
                                    Latitude = double.Parse(station.Element("Latitude").Value)

                                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return baseStations;
        }
        #endregion
        #region// customer loader
        internal static void LoadCustomers()
        {
            try
            {
                CustomerPathRoot = XElement.Load(CustomersPath);
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal static List<Customer> GetCustomers()
        {
            LoadCustomers();
            List<Customer> Customers;
            try
            {
                Customers = (from customer in CustomerPathRoot.Elements()
                             select new Customer()
                             {
                                 Id = int.Parse(customer.Element("Id").Value),
                                 Name = customer.Element("Name").Value,
                                 Phone = customer.Element("Phone").Value,
                                 Longtitude = double.Parse(customer.Element("Longtitude").Value),
                                 Latitude = double.Parse(customer.Element("Latitude").Value),
                             }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return Customers;
        }
        #endregion
        #region// drone loader
        internal static void LoadDrones()
        {
            try
            {
                DronePathRoot = XElement.Load(DronesPath);
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal static List<Drone> GetDrones()
        {
            LoadDrones();
            List<Drone> Drones;
            try
            {
                Drones = (from drone in DronePathRoot.Elements()
                                select new Drone()
                                {
                                    Id = int.Parse(drone.Element("Id").Value),
                                    Model = drone.Element("Name").Value,
                                    MaxWeight = (WeightCategories)int.Parse(drone.Element("MaxWeight").Value)
                                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return Drones;
        }
        #endregion
        #region// Drone charge loader
        internal static void LoadDroneCharges()
        {
            try
            {
                DroneChargePathRoot = XElement.Load(DroneChargesPath);
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal static List<DroneCharge> GetDroneCharges()
        {
            LoadDroneCharges();
            List<DroneCharge> droneCharges;
            try
            {
                droneCharges = (from droneCharge in DroneChargePathRoot.Elements()
                                select new DroneCharge()
                                {
                                    DroneId = int.Parse(droneCharge.Element("DroneId").Value),
                                    StationId = int.Parse(droneCharge.Element("StationId").Value),
                                    TimeOfStartCharging= DateTime.Parse(droneCharge.Element("TimeOfStartCharging").Value)
                                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return droneCharges;
        }
        #endregion
        #region// Parcel loader
        internal static void LoadParcels()
        {
            try
            {
                ParcelPathRoot = XElement.Load(ParcelsPath);
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal static List<Parcel> GetParcels()
        {
            LoadParcels();
            List<Parcel> parcels;
            try
            {
                parcels = (from parcel in ParcelPathRoot.Elements()
                                select new Parcel()
                                {
                                    Id = int.Parse(parcel.Element("Id").Value),
                                    SenderId=int.Parse(parcel.Element("SenderId").Value),
                                    ReciverId = int.Parse(parcel.Element("ReciverId").Value),
                                    Weight =(WeightCategories)int.Parse(parcel.Element("Weight").Value),
                                    Priority=(Priorities)int.Parse(parcel.Element("Priority").Value),
                                    CreatingTime = DateTime.Parse(parcel.Element("CreatingTime").Value),
                                    DroneId=int.Parse(parcel.Element("DroneId").Value),
                                    AssociatedTime = DateTime.Parse(parcel.Element("AssociatedTime").Value),
                                    PickedUp = DateTime.Parse(parcel.Element("PickedUp").Value),
                                    Deliverd = DateTime.Parse(parcel.Element("Deliverd").Value)

                                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return parcels;
        }
        #endregion

        #region// save base station
        internal static void SaveBaseStations(IEnumerable<BaseStation> baseStations)
        {
            BaseStationPathRoot = new XElement("BaseStations",
              from station in baseStations
              select new XElement("BaseStations"
                   , new XElement("Id", station.Id)
                   , new XElement("Name", station.Name)
                   , new XElement("ChargeSlots", station.ChargeSlots)
                   , new XElement("Longtitude", station.Longtitude)
                   , new XElement("Latitude", station.Latitude)));

            BaseStationPathRoot.Save(BaseStationsPath);
        }
        #endregion
        #region// save customer
        internal static void SaveCustomer(List<Customer> customers)
        {
            CustomerPathRoot = new XElement("Customer",
            from customer in customers
            select new XElement("Customers"
                  , new XElement("Id", customer.Id)
                  , new XElement("Name", customer.Name)
                  , new XElement("Phone", customer.Phone)
                  , new XElement("Longtitude", customer.Longtitude)
                  , new XElement("Latitude", customer.Latitude)));
            CustomerPathRoot.Save(CustomersPath);
        }
        #endregion   
        #region// save drone
        internal static void SaveDrones(List<Drone> drones)
        {
            DronePathRoot = new XElement("Drones",
              from drone in drones 
              select new XElement("Drones" 
                  , new XElement("Id", drone.Id)
                  , new XElement("Model", drone.Model)
                  ,new XElement("MaxWeight", drone.MaxWeight)));
            
            DronePathRoot.Save(DronesPath);
        }
        #endregion
        #region// save drone charge
        internal static void SaveDroneCharges(List<DroneCharge> droneCharges)
        {
             DroneChargePathRoot = new XElement("DroneCharges",
               from droneCharge in droneCharges
               select new XElement("DroneCharges"
                  , new XElement("DroneId", droneCharge.DroneId)
                  , new XElement("StationId", droneCharge.StationId)
                  , new XElement("TimeOfStartCharging", droneCharge.TimeOfStartCharging)));
    
            DroneChargePathRoot.Save(DroneChargesPath);
        }
        #endregion
        #region// save parcel
        internal static void SaveParcel(List<Parcel> parcels)
        {
            ParcelPathRoot = new XElement("Parcels",
                from parcel in parcels
                select new XElement("parcels"
                   , new XElement("Id", parcel.Id)
                   , new XElement("SenderId", parcel.SenderId)
                   , new XElement("ReciverId", parcel.ReciverId)
                   , new XElement("Weight", parcel.Weight)
                   , new XElement("Priority", parcel.Priority)
                   , new XElement("CreatingTime", parcel.CreatingTime)
                   , new XElement("DroneId", parcel.DroneId)
                   , new XElement("AssociatedTime", parcel.AssociatedTime)
                   , new XElement("PickedUp", parcel.PickedUp)
                   , new XElement("Deliverd", parcel.Deliverd)));
            
            ParcelPathRoot.Save(ParcelsPath);
        }
        #endregion

        #region// add base station
        internal static void AddBaseStation(BaseStation baseStations)
        {
            LoadBaseStations();
            XElement newBaseStation = new XElement("BaseStation"
                   , new XElement("Id", baseStations.Id)
                   , new XElement("Name", baseStations.Name)
                   , new XElement("ChargeSlots", baseStations.ChargeSlots)
                   , new XElement("Longtitude", baseStations.Longtitude)
                   , new XElement("Latitude", baseStations.Latitude));
            BaseStationPathRoot.Add(newBaseStation);
            BaseStationPathRoot.Save(BaseStationsPath);
        }
        #endregion
        #region// add customer
        internal static void Addcustomer(Customer customer)
        {
            LoadCustomers();
            XElement newCustomer = new XElement("Customer"
                   , new XElement("Id", customer.Id)
                  , new XElement("Name", customer.Name)
                  , new XElement("Phone", customer.Phone)
                  , new XElement("Longtitude", customer.Longtitude)
                  , new XElement("Latitude", customer.Latitude));
            CustomerPathRoot.Add(newCustomer);
            CustomerPathRoot.Save(CustomersPath);
        }
        #endregion
        #region// add drone
        internal static void AddDrone(Drone drone)
        {
            LoadDrones();
            XElement newDrone = new XElement("Drone"
                   , new XElement("Id", drone.Id)
                   , new XElement("Model", drone.Model)
                   , new XElement("MaxWeight", drone.MaxWeight));
            DronePathRoot.Add(newDrone);
            DronePathRoot.Save(DronesPath);
        }
        #endregion
        #region// add drone charge
        internal static void AddDroneCharge(DroneCharge droneCharge)
        {
            LoadDroneCharges();
            XElement newDroneCharge = new XElement("DroneCharge"
                   , new XElement("DroneId", droneCharge.DroneId)
                   , new XElement("StationId", droneCharge.StationId)
                   , new XElement("TimeOfStartCharging", droneCharge.TimeOfStartCharging));
            DroneChargePathRoot.Add(newDroneCharge);
            DroneChargePathRoot.Save(DroneChargesPath);
        }
        #endregion
        #region// add parcel
        internal static void AddParcel(Parcel parcel)
        {
            LoadParcels();
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
            ParcelPathRoot.Add(newParcel);
            DronePathRoot.Save(DronesPath);
        }
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