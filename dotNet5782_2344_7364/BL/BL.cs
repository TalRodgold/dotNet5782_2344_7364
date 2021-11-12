using System;
using IDAL;
using IBL.BO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IBl
    {
        public List<DroneToList> ListOfDronsBL = new List<DroneToList>();
        BL bl;
        IDal dal;
        public BL()
        {
            dal = new DalObjects.DalObjects();
        }

        public void AddBaseStation(BaseStation b)
        {
            try
            {
                dal.ConstructBaseStation(b.Id, b.Name, b.NumberOfFreeChargingSlots, b.Location.Longitude, b.Location.Latitude);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AddDrone(Drone d, int startingBaseStation)
        {
            try
            {
                if (!dal.IfBaseStationExsists(startingBaseStation)) // check if start station exisists!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! kadosh wants to chang
                {
                    throw "?";
                }
                d.CurrentLocation = GetBaseStationById(startingBaseStation).Location;
                dal.ConstructDrone(d.Id, d.Model, (IDAL.DO.WeightCategories)d.Weight); // creat drone
                dal.UpdateDroneCharge(d.Id, startingBaseStation); // connect drone to charging base station
                ListOfDronsBL.Add(new DroneToList(d.Id, d.Model, d.Weight, d.Battery, d.DroneStatuses, d.CurrentLocation, d.ParcelInTransit.Id));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AddCustomer(Customer c)
        {
            try
            {
                dal.ConstructCustomer(c.Id, c.Name, c.Phone, c.Location.Longitude, c.Location.Latitude);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AddParcel(Parcel p)
        {

            p.Reciver.Name = dal.GetParcel(p.Reciver.Id);
            p.Sender.Name = dal.GetNameOfCustomerById(p.Sender.Id);
            try
            {
                dal.ConstructParcel(p.Sender.Id, p.Reciver.Id, (IDAL.DO.WeightCategories)p.Weight, (IDAL.DO.Priorities)p.Prioritie, DateTime.Now, p.DroneInParcel.Id, p.AssociationTime, p.PickupTime, p.DeliveryTime);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public partial class BL : IBl
    {
        Customer GetCustomerById(int id)
        {
            try
            {
                IDAL.DO.Customer idalCustomer = dal.GetCustomer(id); // get a customer of dal type
                Customer newCustomer = new Customer(idalCustomer.Id, idalCustomer.Name, idalCustomer.Phone, new Location(idalCustomer.Longtitude, idalCustomer.Latitude)); // creat and add to a customer of bl type

            
                Predicate<IDAL.DO.Parcel> predicate = element => element.SenderId == id; // predicat to find parcel based on senders id
                Predicate<IDAL.DO.Parcel> predicate1 = element => element.TargetId == id; // predicat to find parcel based on targets id
                IDAL.DO.Parcel idalParcel = dal.GetParcel(predicate); // get parcel of dal type based on senders id

                CustomerInParcel newCustomerInParcel = new CustomerInParcel(idalCustomer.Id, idalCustomer.Name); // creat a customer in parcel based on current customer
                ParcelAtCustomer newparcelAtCustomer = new ParcelAtCustomer(idalParcel.Id, (Enums.WeightCategories)idalParcel.Weight, (Enums.Priorities)idalParcel.Priority, newCustomerInParcel );

                idalParcel = dal.GetParcel(predicate1); // get parcel of dal type based on target id

                ParcelAtCustomer newparcelAtCustomer1 = new ParcelAtCustomer(idalParcel.Id, (Enums.WeightCategories)idalParcel.Weight, (Enums.Priorities)idalParcel.Priority, newCustomerInParcel);

                newCustomer.ParcelFromCustomer = newparcelAtCustomer; // add to new customer the parcel from customer
                newCustomer.parcelToCustomer = newparcelAtCustomer1; // add to new customer the parcel to customer

                return newCustomer;
            }
            catch (Exception)
            {

                throw;
            }
        }
        Parcel GetParcelById(int id)
        {
            if (!dal.IfParcelExsists(id))
            {
                throw "?";
            }
            IDAL.DO.Parcel idalParcel = dal.GetParcel(id);
            Parcel newParcel = new Parcel();
            newParcel.AssociationTime = idalParcel.
        }
        Drone GetDroneById(int id)
        {
            if (!dal.IfDroneExsists(id))
            {
                throw "?";
            }
            IDAL.DO.Drone idalDrone = dal.GetDrone(id);
            Drone newDrone = new Drone();
            newDrone.Id = idalDrone.Id;
            newDrone.Model = idalDrone.Model;
            newDrone.Weight = (Enums.WeightCategories)idalDrone.MaxWeight;
            newDrone.Battery = ListOfDronsBL.Find(element => element.Id == id).Battery;

        }
        BaseStation GetBaseStationById(int id)
        {
            if (!dal.IfBaseStationExsists(id))
            {
                throw "?";
            }
            IDAL.DO.BaseStation idalBaseStation = dal.GetBaseStation(id);
            BaseStation newBaseStation = new BaseStation();
            newBaseStation.Id = idalBaseStation.Id;
            newBaseStation.Name = idalBaseStation.Name;
            newBaseStation.Location = new Location(idalBaseStation.Longtitude, idalBaseStation.Latitude);
            newBaseStation.NumberOfFreeChargingSlots = idalBaseStation.
        }
    }
}
