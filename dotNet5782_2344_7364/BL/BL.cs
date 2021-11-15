﻿using System;
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
                if (!dal.IfBaseStationExsists(startingBaseStation)) // check if start station exisists!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!ץ!! kadosh wants to chang
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
        public Customer GetCustomerById(int id)
        {
            try
            {
                IDAL.DO.Customer idalCustomer = dal.GetCustomer(id); // get a customer of dal type
                Customer newCustomer = new Customer(idalCustomer.Id, idalCustomer.Name, idalCustomer.Phone, new Location(idalCustomer.Longtitude, idalCustomer.Latitude)); // creat and add to a customer of bl type


                Predicate<IDAL.DO.Parcel> predicate = element => element.SenderId == id; // predicat to find parcel based on senders id
                Predicate<IDAL.DO.Parcel> predicate1 = element => element.TargetId == id; // predicat to find parcel based on targets id

                IDAL.DO.Parcel newParcel = dal.GetParcel(id, predicate); // get parcel of dal type based on senders id

                CustomerInParcel newCustomerInParcel = new CustomerInParcel(idalCustomer.Id, idalCustomer.Name); // creat a customer in parcel based on current customer
                ParcelAtCustomer newparcelAtCustomer = new ParcelAtCustomer(newParcel.Id, (Enums.WeightCategories)newParcel.Weight, (Enums.Priorities)newParcel.Priority, newCustomerInParcel);

                newParcel = dal.GetParcel(id, predicate1); // get parcel of dal type based on target id

                ParcelAtCustomer newparcelAtCustomer1 = new ParcelAtCustomer(newParcel.Id, (Enums.WeightCategories)newParcel.Weight, (Enums.Priorities)newParcel.Priority, newCustomerInParcel);

                newCustomer.ParcelFromCustomer = newparcelAtCustomer; // add to new customer the parcel from customer
                newCustomer.parcelToCustomer = newparcelAtCustomer1; // add to new customer the parcel to customer

                return newCustomer;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Parcel GetParcelById(int id, Predicate<IDAL.DO.Parcel> predicate)
        {

            IDAL.DO.Parcel idalParcel = dal.GetParcel(id, predicate);
            Customer senderCustomer = GetCustomerById(idalParcel.SenderId); // creat a new customer based on the sender of the parcel
            Customer reciverCustomer = GetCustomerById(idalParcel.TargetId); // creat a new customer based on the reciver of the parcel

            CustomerInParcel senderCustomerInParcel = new CustomerInParcel(senderCustomer.Id, senderCustomer.Name); // creat a customer in parcel based on current customer
            CustomerInParcel reciverCustomerInParcel = new CustomerInParcel(senderCustomer.Id, senderCustomer.Name); // creat a customer in parcel based on current customer

            Drone newDrone = GetDroneById(idalParcel.DroneId);
            DroneInParcel newDroneInParcel = new DroneInParcel(newDrone.Id, newDrone.Battery, newDrone.CurrentLocation);


            Parcel newParcel = new Parcel(senderCustomerInParcel, reciverCustomerInParcel, (Enums.WeightCategories)idalParcel.Weight, (Enums.Priorities)idalParcel.Priority, newDroneInParcel, idalParcel.Requsted, idalParcel.Scheduled, idalParcel.PickedUp, idalParcel.Deliverd);
            return newParcel;
        }
        public Drone GetDroneById(int id)
        {
            Predicate<IDAL.DO.Parcel> predicate = element => element.DroneId == id;
            Parcel newParcel = GetParcelById(0, predicate);
            ParcelInTransit newParcelInTransit = GetParcelInTransitById(GetDroneToList(id).NumberOfParcelInTransit);
            Drone newDrone = new Drone(GetDroneToList(id).Id, GetDroneToList(id).Model, GetDroneToList(id).Weight, GetDroneToList(id).Battery, GetDroneToList(id).DroneStatuses, newParcelInTransit, GetDroneToList(id).CurrentLocation);
            return newDrone;
        }
        public DroneToList GetDroneToList(int id)
        {
            return ListOfDronsBL.Find(element => element.Id == id);
        }
        public BaseStation GetBaseStationById(int id)
        {
            if (!dal.IfBaseStationExsists(id))
            {
                throw new IdAlreadyExsistsExceptions("error");
            }
            IDAL.DO.BaseStation idalBaseStation = dal.GetBaseStation(id);
            BaseStation newBaseStation = new BaseStation(idalBaseStation.Id, idalBaseStation.Name, new Location(idalBaseStation.Longtitude, idalBaseStation.Latitude), idalBaseStation.ChargeSlots, dal.GetListOfDroneCharge());
            return newBaseStation;
        }
        public ParcelToList GetParcelToListById(int id)
        {
            Predicate<IDAL.DO.Parcel> predicate = element => element.Id == id; // predicat to find parcel based on senders id

            IDAL.DO.Parcel newParcel = dal.GetParcel(0, predicate);
            Customer senderCustomer = GetCustomerById(newParcel.SenderId); // creat a new customer based on the sender of the parcel
            Customer reciverCustomer = GetCustomerById(newParcel.TargetId); // creat a new customer based on the reciver of the parcel
            ParcelToList newParcelToList = new ParcelToList(newParcel.Id, senderCustomer.Name, reciverCustomer.Name, (Enums.WeightCategories)newParcel.Weight, (Enums.Priorities)newParcel.Priority, (Enums.ParcelStatus));
            return newParcelToList;
        }
        public ParcelInTransit GetParcelInTransitById(int id)
        {
            Predicate<IDAL.DO.Parcel> predicate = element => element.Id == id; // predicat to find parcel based on senders id
            IDAL.DO.Parcel newParcel = dal.GetParcel(0, predicate);
            Customer senderCustomer = GetCustomerById(newParcel.SenderId); // creat a new customer based on the sender of the parcel
            Customer reciverCustomer = GetCustomerById(newParcel.TargetId); // creat a new customer based on the reciver of the parcel
            CustomerInParcel sCustomer = new CustomerInParcel(senderCustomer.Id, senderCustomer.Name);
            CustomerInParcel rCustomer = new CustomerInParcel(reciverCustomer.Id, reciverCustomer.Name);
            return new ParcelInTransit(id, (Enums.Priorities)newParcel.Priority, (Enums.WeightCategories)newParcel.Weight, sCustomer, rCustomer, senderCustomer.Location, reciverCustomer.Location, CalculateDistance());

        }

    }


    public partial class BL : IBl
    {
        public double CalculateBattery(int id)
        {
            double banan = ListOfDronsBL.Find(element => element.Id == id).Battery);
            return;
        }

        public void CalculateLocation(DroneToList drone)
        {
            switch (drone.DroneStatuses)
            {
                case Enums.DroneStatuses.Available:
                    return;
                case Enums.DroneStatuses.Delivery:

                    double a = CalculateDistance(drone.CurrentLocation, GetBaseStationById(dal.GetDroneCharge(drone.Id).StationId).Location);
                    double b = CalculateDistance(drone.CurrentLocation, GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).SenderId).Location);
                    double c = CalculateDistance(drone.CurrentLocation, GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).TargetId).Location);
                    if (a >= b && a >= c)
                    {
                        ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetBaseStationById(dal.GetDroneCharge(drone.Id).StationId).Location;
                        return;
                    }
                    if (b >= a && b >= c)
                    {
                        ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).SenderId).Location;
                        return;
                    }
                    else
                    {
                        ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).TargetId).Location;
                        return;
                    }
                case Enums.DroneStatuses.Maintenance:
                    ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetBaseStationById(dal.GetDroneCharge(drone.Id).StationId).Location;
                    return;

            }
        }

        public double CalculateDistance(Location x, Location y)
        {
            double ConvertToRadians(double angle)
            {
                return (Math.PI / 180) * angle;
            }
            int R = 6371;

            double f1 = ConvertToRadians(x.Latitude);
            double f2 = ConvertToRadians(y.Latitude);

            double df = ConvertToRadians(x.Latitude - y.Latitude);
            double dl = ConvertToRadians(x.Longitude - y.Longitude);

            double a = Math.Sin(df / 2) * Math.Sin(df / 2) +
            Math.Cos(f1) * Math.Cos(f2) *
            Math.Sin(dl / 2) * Math.Sin(dl / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Calculate the distance.
            double d = R * c;

            return d;


        }
        public int ReciveParcelId(Parcel parcel)
        {
            Predicate<IDAL.DO.Parcel> predicate = element => element.SenderId == parcel.Sender.Id; // predicat to find parcel based on senders id

            IDAL.DO.Parcel newParcel = dal.GetParcel(parcel.Sender.Id, predicate); // get parcel of dal type based on senders id
            return newParcel.Id;

        }
    }
    public partial class BL : IBl
    {
        public void UpdateDroneModel(int id,string newMOdel)
        {

        }
    }
}

