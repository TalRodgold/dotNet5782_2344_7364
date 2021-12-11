using System;
using IDAL;
using IBL.BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace IBL
{
    /// <summary>
    /// All the get object by id functions
    /// </summary>
    public partial class BL : IBl
    {
        #region//Get customer from data-source by id
        /// <summary>
        /// //Get customer from data-source by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomerById(int? id)//Get customer from data-source by id
        {
            try
            {
                if (id < 0 || id == null) // if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                IDAL.DO.Customer idalCustomer = dal.GetCustomer(id); // get a customer of dal type
                Customer newCustomer = new Customer(idalCustomer.Id, idalCustomer.Name, idalCustomer.Phone, new Location(idalCustomer.Longtitude, idalCustomer.Latitude)); // creat and add to a customer of bl type


                Predicate<IDAL.DO.Parcel> predicate = element => element.SenderId == id; // predicat to find parcel based on senders id
                Predicate<IDAL.DO.Parcel> predicate1 = element => element.ReciverId == id; // predicat to find parcel based on targets id

                List<IDAL.DO.Parcel> newSenderParcel = dal.GetListOfParcel(predicate).ToList(); // get parcel of dal type based on senders id
                List<ParcelAtCustomer> list = new List<ParcelAtCustomer>();
                foreach (var item in newSenderParcel)
                {
                    list.Add(new ParcelAtCustomer(item.Id, (Enums.WeightCategories)item.Weight, (Enums.Priorities)item.Priority, statusCalculate(dal.GetParcel(item.Id)), new CustomerInParcel(newCustomer.Id, newCustomer.Name)));
                }
                newCustomer.ParcelFromCustomer = list; // add to new customer the parcel from customer

                List<IDAL.DO.Parcel> newReciveParcels = dal.GetListOfParcel(predicate1).ToList(); // get parcel of dal type based on reciver id
                List<ParcelAtCustomer> list1 = new List<ParcelAtCustomer>();
                foreach (var item in newReciveParcels)
                {
                    list1.Add(new ParcelAtCustomer(item.Id, (Enums.WeightCategories)item.Weight, (Enums.Priorities)item.Priority, statusCalculate(dal.GetParcel(item.Id)), new CustomerInParcel(newCustomer.Id, newCustomer.Name)));

                }
                newCustomer.ParcelToCustomer = list1; // add to new customer the parcel from customer
                return newCustomer;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if customer id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region //Get parcel from data-source by id
        /// <summary>
        /// from data-source by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Parcel GetParcelById(int? id, Predicate<IDAL.DO.Parcel> predicate = null)//from data-source by id
        {
            try
            {
                if (id < 0) // if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                if (id == null)
                {
                    throw new IdNotExsistException("parcel", id); // throw
                }
                IDAL.DO.Parcel idalParcel = dal.GetParcel(id, predicate);//Get parcel by id or by predicate
                Customer senderCustomer = GetCustomerById(idalParcel.SenderId); // creat a new customer based on the sender of the parcel
                Customer reciverCustomer = GetCustomerById(idalParcel.ReciverId); // creat a new customer based on the reciver of the parcel

                CustomerInParcel senderCustomerInParcel = new CustomerInParcel(senderCustomer.Id, senderCustomer.Name); // creat a customer in parcel based on current customer
                CustomerInParcel reciverCustomerInParcel = new CustomerInParcel(reciverCustomer.Id, reciverCustomer.Name); // creat a customer in parcel based on current customer
                DroneInParcel newDroneInParcel;
                if (idalParcel.DroneId == null)//if the parcel not assosiate
                {
                    newDroneInParcel = null;
                }
                else//if the parcel is assosiate
                {
                    Drone newDrone = GetDroneById(idalParcel.DroneId);//get drone by drone id
                    newDroneInParcel = new DroneInParcel(newDrone.Id, newDrone.Battery, newDrone.CurrentLocation);//creat new drone in parcel 

                }


                Parcel newParcel = new Parcel(idalParcel.Id, senderCustomerInParcel, reciverCustomerInParcel, (Enums.WeightCategories)idalParcel.Weight, (Enums.Priorities)idalParcel.Priority, newDroneInParcel, idalParcel.CreatingTime, idalParcel.AssociatedTime, idalParcel.PickedUp, idalParcel.Deliverd);//creat new parcel
                return newParcel;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if parcel id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region //Get drone from data-source by id
        /// <summary>
        /// Get drone from data-source by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Drone GetDroneById(int? id)//Get drone from data-source by id
        {
            try
            {
                if (id < 0)// if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                if (id == null)
                {
                    throw new IdNotExsistException("drone", id); // throw
                }
                Predicate<IDAL.DO.Parcel> predicate = element => element.DroneId == id;
                ParcelInTransit newParcelInTransit;
                if (GetDroneToList(id).NumberOfParcelInTransit == null)
                {
                    newParcelInTransit = new ParcelInTransit();
                }
                else
                {
                    newParcelInTransit = GetParcelInTransitById(GetDroneToList(id).NumberOfParcelInTransit);
                }
                DroneToList droneToList = GetDroneToList(id);
                Drone newDrone = new Drone(droneToList.Id, droneToList.Model, droneToList.Weight, droneToList.Battery, droneToList.DroneStatuses, newParcelInTransit, droneToList.CurrentLocation);
                return newDrone;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if drone id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Get drone from list that is held here by id
        /// <summary>
        /// Get drone from list that is held here by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DroneToList GetDroneToList(int? id)//Get drone from list that is held here by id
        {
            try
            {
                if (id < 0) // if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                if (id == null)
                {
                    throw new IdNotExsistException("drone", id); // throw
                }
                if (!dal.IfDroneExsists(id))
                {
                    throw new IdNotExsistException("drone", id, new IDAL.DO.IdNotExsistException("drone", id));
                }
                return ListOfDronsBL.Find(element => element.Id == id);

            }
            catch (IDAL.DO.IdNotExsistException exception) // if base station id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Get base station from data-source by id
        /// <summary>
        /// Get base station from data-source by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseStation GetBaseStationById(int? id)//Get base station from data-source by id
        {
            try
            {
                if (id < 0) // if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                if (id == null)
                {
                    throw new IdNotExsistException("base station", id); // throw
                }
                if (!dal.IfBaseStationExsists(id))
                {
                    throw new IdNotExsistException("base station", id, new IDAL.DO.IdNotExsistException("base station", id));
                }
                IDAL.DO.BaseStation idalBaseStation = dal.GetBaseStation(id);

                IEnumerable<IDAL.DO.DroneCharge> listOfDronesCharging = dal.GetListOfDroneCharge();
                List<DroneInCharging> listToAdd = new List<DroneInCharging>();
                foreach (var item in listOfDronesCharging)
                {
                    if (item.StationId == idalBaseStation.Id)
                    {
                        listToAdd.Add(new DroneInCharging(item.DroneId, ListOfDronsBL.Find(element => element.Id == item.DroneId).Battery, dal.GetDroneCharge(item.DroneId).TimeOfStartCharging));
                    }
                }
                BaseStation newBaseStation = new BaseStation(idalBaseStation.Id, idalBaseStation.Name, new Location(idalBaseStation.Longtitude, idalBaseStation.Latitude), idalBaseStation.ChargeSlots, listToAdd);
                return newBaseStation;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if base station id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Get parceltolist with manipulation from data source by id
        /// <summary>
        /// //Get parceltolist with manipulation from data source by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ParcelToList GetParcelToListById(int? id)//Get parceltolist with manipulation from data source by id
        {
            try
            {
                if (id < 0)// if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                if (id == null)
                {
                    throw new IdNotExsistException("parcel", id); // throw
                }
                Predicate<IDAL.DO.Parcel> predicate = element => element.Id == id; // predicat to find parcel based on senders id

                IDAL.DO.Parcel newParcel = dal.GetParcel(null, predicate);
                Customer senderCustomer = GetCustomerById(newParcel.SenderId); // creat a new customer based on the sender of the parcel
                Customer reciverCustomer = GetCustomerById(newParcel.ReciverId); // creat a new customer based on the reciver of the parcel
                ParcelToList newParcelToList = new ParcelToList(newParcel.Id, senderCustomer.Name, reciverCustomer.Name, (Enums.WeightCategories)newParcel.Weight, (Enums.Priorities)newParcel.Priority, statusCalculate(dal.GetParcel(GetParcelById(newParcel.Id, predicate).Id)));
                return newParcelToList;
            }
            catch (IDAL.DO.IdNotExsistException exception) // if parcel or customer id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Get parceltolist with manipulation from data source by id
        /// <summary>
        /// //Get parceltolist with manipulation from data source by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ParcelInTransit GetParcelInTransitById(int? id)//Get parceltolist with manipulation from data source by id
        {
            try
            {
                if (id < 0)// if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                if (id == null)
                {
                    throw new IdNotExsistException("parcel", id); // throw
                }
                //Predicate<IDAL.DO.Parcel> predicate = element => element.SenderId == id; // predicat to find parcel based on senders id
                IDAL.DO.Parcel newParcel = dal.GetParcel(id);
                bool status = true;
                if (newParcel.PickedUp != null)
                {
                    status = false;
                }
                Customer senderCustomer = GetCustomerById(newParcel.SenderId); // creat a new customer based on the sender of the parcel
                Customer reciverCustomer = GetCustomerById(newParcel.ReciverId); // creat a new customer based on the reciver of the parcel
                CustomerInParcel sCustomer = new CustomerInParcel(senderCustomer.Id, senderCustomer.Name);
                CustomerInParcel rCustomer = new CustomerInParcel(reciverCustomer.Id, reciverCustomer.Name);

                return new ParcelInTransit(id, status, (Enums.Priorities)newParcel.Priority, (Enums.WeightCategories)newParcel.Weight, sCustomer, rCustomer, senderCustomer.Location, reciverCustomer.Location, calculateDistance(senderCustomer.Location, reciverCustomer.Location));

            }
            catch (IDAL.DO.IdNotExsistException exception) // if customer or parcel id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
    }

}
