using System;
using DalApi;
using BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace BlApi
{
    /// <summary>
    /// All the addition functions for BL
    /// </summary>
    internal sealed partial class BL : IBl
    {
        #region//Base station addition
        /// <summary>
        /// Base station addition
        /// </summary>
        /// <param name="b"></param>
        public void AddBaseStation(BaseStation b)//Base station addition
        { 
            try
            {
                if (b.Location.Longitude < 31.728959 || b.Location.Longitude > 31.806477 || b.Location.Latitude > 35.221416 || b.Location.Latitude < 35.206714) // if out of range
                {
                    throw new LocationOutOfRangeException("base station");
                }
                if (b.Id < 0) // if id is negative
                {
                    throw new InvalidIdException("negative", b.Id);
                }
                dal.ConstructBaseStation(b.Id, b.Name, b.NumberOfFreeChargingSlots, b.Location.Longitude, b.Location.Latitude);//call for constructor
            }
            catch (DO.IdAlreadyExsistsExceptions exception) // if base station id already exsists and was thrown from dal objects
            {

                throw new IdAlreadyExsistsExceptions(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Drone addition
        /// <summary>
        /// Drone addition
        /// </summary>
        /// <param name="d"></param>
        /// <param name="startingBaseStation"></param>
        public void AddDrone(Drone d, int? startingBaseStation)//Drone addition
        {
            try
            {
                if (d.Id < 0)// if id is negative
                {
                    throw new InvalidIdException("negative", d.Id);
                }
                if (!dal.IfBaseStationExsists(startingBaseStation))//if base atstion already exist
                {
                    throw new IdNotExsistException("base station", startingBaseStation);
                }
                d.CurrentLocation = GetBaseStationById(startingBaseStation).Location;//update the location to be like his starting base station
                dal.ConstructDrone(d.Id, d.Model, (DO.WeightCategories)d.Weight); // creat drone
                dal.UpdateDroneCharge(d.Id, startingBaseStation,DateTime.Now); // connect drone to charging base station
                ListOfDronsBL.Add(new DroneToList(d.Id, d.Model, d.Weight, d.Battery, d.DroneStatuses, d.CurrentLocation, null));//add the drone to the local list of drones
            }
            catch (DO.IdAlreadyExsistsExceptions exception) // if drone id already exsists and was thrown from dal objects
            {
                throw new IdAlreadyExsistsExceptions(exception.Text, exception.ID, exception); // throw
            }
            catch(DO.NoFreeSpace exception) // if there is no free space
            {
                throw new NoFreeSpace(exception.Text, exception); // throw
            }
        }
        #endregion
        #region//Customer addition
        /// <summary>
        /// Customer addition
        /// </summary>
        /// <param name="c"></param>
        public void AddCustomer(Customer c)//Customer addition
        {
            try
            {
                if (c.Location.Longitude < 31.728959 || c.Location.Longitude > 31.806477 || c.Location.Latitude > 35.221416 || c.Location.Latitude < 35.206714) // if out of range
                {
                    throw new LocationOutOfRangeException("customr");
                }
                if (c.Id < 0)// if id is negative
                {
                    throw new InvalidIdException("negative", c.Id);
                }
                if (dal.IfCustomerExsists(c.Id))//if customer already exist 
                {
                    throw new IdAlreadyExsistsExceptions("Customer", c.Id);
                }
                dal.ConstructCustomer(c.Id, c.Name, c.Phone, c.Location.Longitude, c.Location.Latitude);//call to the constructor
            }
            catch (DO.IdAlreadyExsistsExceptions exception) // if customer id already exsists and was thrown from dal objects
            {

                throw new IdAlreadyExsistsExceptions(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Parcel addition
        /// <summary>
        /// Parcel addition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reciver"></param>
        /// <param name="weight"></param>
        /// <param name="prioritie"></param>
        public void AddParcel(CustomerInParcel sender, CustomerInParcel reciver, Enums.WeightCategories weight, Enums.Priorities prioritie) // Parcel addition
        {
            try
            {
                if (sender.Id < 0) // if id is negative
                {
                    throw new InvalidIdException("negative", sender.Id);
                }
                if (reciver.Id < 0) // if id is negative
                {
                    throw new InvalidIdException("negative", reciver.Id);
                }
                int? id = dal.ConstructParcel(sender.Id, reciver.Id, (DO.WeightCategories)weight, (DO.Priorities)prioritie, DateTime.Now, null, null, null, null) ;//call the constructor
                Parcel newParcel = new Parcel(id, sender, reciver, weight, prioritie, null, DateTime.Now, null, null, null);
            }
            catch (DO.SameIdException exception)
            {

                throw new SameIdException(exception.Text, exception.Id, exception);
            }    
        }
        #endregion
    }
}
