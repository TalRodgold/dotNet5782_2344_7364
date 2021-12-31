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
        string BaseStationPath = @"seStations.xml";
        internal static string CustomersPath = @"Data.Customers.xml";
        string DronesPath = @"Drones";
        string DroneChargesPath = @"DroneCharges";
        string ParcelsPath = @"Parcels";

        internal static XElement BaseStationPathRoot;
        internal static XElement CustomerPathRoot;
        internal static XElement DronePathRoot;
        internal static XElement DroneChargePathRoot;
        internal static XElement ParcelPathRoot;

        #region// base station loader
        private void LoadBaseStations()
        {
            try
            {
                BaseStationPathRoot = XElement.Load(BaseStationPath);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BaseStation> GetBaseStations()
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
        private void LoadCustomers()
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
        public List<Customer> GetCustomers()
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
        private void LoadDrones()
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
        public List<Drone> GetDrones()
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
        private void LoadDroneCharges()
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
        public List<DroneCharge> DroneCharges()
        {
            LoadBaseStations();
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
        private void LoadParcels()
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
        public List<Parcel> Parcels()
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
        internal static void SaveCustomer(List<Customer> customers)
        {
            XElement CustomerPathRoot = new XElement("Customer");
            foreach (Customer customer in customers)
            {
                XElement id = new XElement("Id", customer.Id);
                XElement name = new XElement("Name", customer.Name);
                XElement phone = new XElement("Phone", customer.Phone);
                XElement longtitude = new XElement("Longtitude", customer.Longtitude);
                XElement latitude = new XElement("Latitude", customer.Latitude);
            }
            CustomerPathRoot.Save(CustomersPath);
        }
        public void SaveBaseStations(IEnumerable<BaseStation> baseStations)
        {
            BaseStationPathRoot = new XElement("BaseStations",
              from station in baseStations
              select new XElement("BaseStations"
                   , new XElement("Id", station.Id)
                   , new XElement("Name", station.Name)
                   , new XElement("ChargeSlots", station.ChargeSlots)
                   , new XElement("Longtitude", station.Longtitude)
                   , new XElement("Latitude", station.Latitude)));

            BaseStationPathRoot.Save(BaseStationPath);
        }
        public void SaveDrones(List<Drone> drones)
        {
            DronePathRoot = new XElement("Drones",
              from drone in drones 
              select new XElement("Drones" 
                  , new XElement("Id", drone.Id)
                  , new XElement("Model", drone.Model)
                  ,new XElement("MaxWeight", drone.MaxWeight)));
            
            DronePathRoot.Save(DronesPath);
        }
        public  void SaveDroneCharges(List<DroneCharge> droneCharges)
        {
             DroneChargePathRoot = new XElement("DroneCharges",
               from droneCharge in droneCharges
               select new XElement("DroneCharges"
                  , new XElement("DroneId", droneCharge.DroneId)
                  , new XElement("StationId", droneCharge.StationId)
                  , new XElement("TimeOfStartCharging", droneCharge.TimeOfStartCharging)));
    
            DroneChargePathRoot.Save(DroneChargesPath);
        }
        public  void SaveParcel(List<Parcel> parcels)
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
    }
}
