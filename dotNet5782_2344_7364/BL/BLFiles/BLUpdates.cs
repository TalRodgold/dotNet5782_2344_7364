using System;
using IDAL;
using IBL.BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace IBL
{
    /// <summary>
    /// All the updates functions for BL
    /// </summary>
    public partial class BL : IBl
    {
        #region//Update drone model
        /// <summary>
        /// Update drone model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newModel"></param>
        public void UpdateDroneModel(int id, string newModel)//Update drone model
        {
            try
            {
                if (id < 0)// if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                DroneToList newDroneToList = GetDroneToList(id);
                newDroneToList.Model = newModel;
                int index = ListOfDronsBL.FindIndex(element => element.Id == id);
                ListOfDronsBL[index] = newDroneToList;
                dal.UpdateDroneModel(id, newModel);
            }
            catch (IDAL.DO.IdNotExsistException exception)
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception);
            }
        }
        #endregion
        #region//Update base station name/number of charging slots
        /// <summary>
        /// Update base station name/number of charging slots
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="numberOfChargingSlots"></param>
        public void UpdateBaseStation(int id, string name = "", int numberOfChargingSlots = 0)//Update base station name/number of charging slots
        {
            try
            {
                if (numberOfChargingSlots < 0)
                {
                    throw new SizeProblemException("bigger", 0);
                }
                if (id < 0) // if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                if (name != "")
                {
                    dal.UpdateBaseStationName(id, name);
                }
                if (numberOfChargingSlots != 0)
                {
                    dal.UpdateChargingSlotsNumber(id, numberOfChargingSlots);
                }
            }
            catch (IDAL.DO.IdNotExsistException exception)
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception);
            }
            catch (IDAL.DO.SizeProblemException exception)
            {
                throw new SizeProblemException(exception.Text, exception.Number, exception);
            }
        }
        #endregion
        #region//Update customer name/phone
        /// <summary>
        /// Update customer name/phone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void UpdateCustomer(int id, string name = "", string phone = "")//Update customer name/phone
        {
            try
            {
                if (id < 0) // if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                if (name != "")
                {
                    dal.UpdateCustomerName(id, name);
                }
                if (phone != "")
                {
                    dal.UpdateCustomerPhone(id, phone);
                }
            }
            catch (IDAL.DO.IdNotExsistException exception)
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception);
            }
        }
        #endregion
        #region//Update-send drone to charge
        /// <summary>
        /// Update-send drone to charge
        /// </summary>
        /// <param name="id"></param>
        public void UpdateSendDroneToCharge(int id)//Update-send drone to charge
        {
            try
            {
                if (id < 0) // if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                GetDroneById(id);// check if drone exsists in dal
                DroneToList newDrone = ListOfDronsBL.Find(element => element.Id == id);
                if (newDrone.DroneStatuses != Enums.DroneStatuses.Available) // if drone is unavalible
                {
                    throw new UnavailableExeption("drone", id);
                }
                int stationId = CalculateMinDistance(GetDroneById(id).CurrentLocation, element => element.NumberOfFreeChargingSlots > 0, element => CalculateDistance(element.Location, newDrone.CurrentLocation) <= ConvertBatteryToDistance(newDrone));
                if (stationId == 0) // if there are no free base stations
                {
                    throw new UnavailableExeption("base station", stationId);
                }
                dal.ConstructDroneCharge(id, stationId);
                BaseStation station = GetBaseStationById(stationId);
                newDrone.Battery = CalculateBattery(newDrone);
                newDrone.CurrentLocation = station.Location;
                newDrone.DroneStatuses = Enums.DroneStatuses.Maintenance;
                int index = ListOfDronsBL.FindIndex(element => element.Id == id);
                ListOfDronsBL[index] = newDrone;
                station.NumberOfFreeChargingSlots -= 1;
                dal.UpdateBaseStationNumOfFreeDroneCharges(station.Id, station.NumberOfFreeChargingSlots);
            }
            catch (IDAL.DO.IdNotExsistException exception) // if base station id does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Update-relese drone from charging slot
        /// <summary>
        /// Update-relese drone from charging slot
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        public void UpdateReleseDrone(int id, double time)//Update-relese drone from charging slot
        {
            if (id < 0) // if id is negative
            {
                throw new InvalidIdException("negative", id);
            }
            DroneToList drone = GetDroneToList(id);
            if (drone.DroneStatuses != Enums.DroneStatuses.Maintenance)
            {
                throw new UnavailableExeption("drone", id);
            }
            if (drone.Battery * time * DroneChargingPaste >= 1) // if charged more than 100 %
            {
                drone.Battery = 1;
            }
            else
            {
                drone.Battery = (drone.Battery * time * DroneChargingPaste) / 100;
            }
            drone.DroneStatuses = Enums.DroneStatuses.Available;
            IDAL.DO.DroneCharge droneCharge = dal.GetDroneCharge(id, element => element.DroneId == id);
            IDAL.DO.BaseStation station = dal.getBaseStationByDroneId(droneCharge.DroneId);//
            station.ChargeSlots -= 1;
            dal.UpdateBaseStationNumOfFreeDroneCharges(station.Id, station.ChargeSlots);
            dal.ReleaseDroneCharge(id, station.Id);
        }
        #endregion
        public void UpdateAssosiateDrone(int id)//Update-assosiate drone to parcel
        {
            {
                try
                {
                    if (id < 0) // if id is negative
                    {
                        throw new InvalidIdException("negative", id);
                    }
                    DroneToList drone = ListOfDronsBL.Find(element => element.Id == id);
                    int index = ListOfDronsBL.FindIndex(element => element.Id == id);
                    if (drone.DroneStatuses != Enums.DroneStatuses.Available) // if drone is unavalible
                    {
                        throw new UnavailableExeption("drone", id);
                    }
                    Enums.Priorities maxPriorities = Enums.Priorities.Regular;
                    double minDistance = double.MaxValue;
                    int properParcelID = -1;
                    Customer senderCustomer = new Customer();
                    Customer reciverCustomer = new Customer();
                    double  distanceDroneToPickup, distancePickupToDelivery, distanceDeliveryToClothestBaseStation, distance = 0;
                    foreach (var item in dal.GetListOfParcel())
                    {
                        Customer newSenderCustomer = GetCustomerById(item.SenderId);
                        Customer newReciverCustomer = GetCustomerById(item.ReciverId);
                        distanceDroneToPickup = CalculateDistance(drone.CurrentLocation, newSenderCustomer.Location);// CalculateDistance(drone1.CurrentLocation, drone1.ParcelInTransit.DeliveryLocation);
                        distancePickupToDelivery = CalculateDistance(newSenderCustomer.Location, newReciverCustomer.Location);
                        distanceDeliveryToClothestBaseStation = CalculateDistance(newReciverCustomer.Location, GetBaseStationById(CalculateMinDistance(newReciverCustomer.Location)).Location);
                        distance = distanceDroneToPickup + distancePickupToDelivery + distanceDeliveryToClothestBaseStation;
                        if (((Enums.WeightCategories)item.Weight <= drone.Weight) && ((Enums.Priorities)item.Priority > maxPriorities) && (distanceDroneToPickup <= minDistance) && (CalculateWhetherTheDroneHaveEnoghBattery(distance, drone)));
                        {
                            maxPriorities = (Enums.Priorities)item.Priority;
                            minDistance = distanceDroneToPickup;
                            properParcelID = item.Id;
                            senderCustomer = newSenderCustomer;
                            reciverCustomer = newReciverCustomer;
                        }
                    }
                    dal.AssociateDroneToParcel(id, properParcelID);

                    drone.DroneStatuses = Enums.DroneStatuses.Delivery;
                    drone.NumberOfParcelInTransit = properParcelID;
                    ListOfDronsBL[index] = drone;
                }
                catch (IDAL.DO.IdNotExsistException exception) // if droneid does not exsists and was thrown from dal objects
                {

                    throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
                }
            }
        }
        
        #region//Update-pick-up parcel by drone
        /// <summary>
        /// Update-pick-up parcel by drone
        /// </summary>
        /// <param name="droneId"></param>
        public void PickupParcelByDrone(int droneId)//Update-pick-up parcel by drone
        {
            //DroneToList droneInList = ListOfDronsBL.Find(element => element.Id == droneId);

            try
            {

                if (droneId < 0) // if id is negative
                {
                    throw new InvalidIdException("negative", droneId);
                }
                Drone drone = GetDroneById(droneId);
                if (drone.DroneStatuses != Enums.DroneStatuses.Delivery || !drone.ParcelInTransit.Status)
                {
                    throw new UnavailableExeption("drone", droneId);
                }
                if (drone.ParcelInTransit.Id == -1 ||  GetParcelById(drone.ParcelInTransit.Id).AssociationTime == DateTime.MinValue)
                {
                    throw new NotAssociatedException("drone", droneId);
                }
                drone.Battery = (drone.Battery * ElectricityUseAvailiblity * CalculateDistance(drone.CurrentLocation, drone.ParcelInTransit.PickupLocation)) / 100;
                drone.CurrentLocation = drone.ParcelInTransit.PickupLocation;
                drone.ParcelInTransit.Status = false;
                DroneToList newDrone = ConvertDroneBlToList(drone);
                int index = ListOfDronsBL.FindIndex(element => element.Id == droneId);
                ListOfDronsBL[index] = newDrone;
                dal.UpdateParclePickup(drone.ParcelInTransit.Id);

            }
            catch (IDAL.DO.IdNotExsistException exception) // if droneid does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region//Update-dilavery parcel by drone
        /// <summary>
        /// Update-dilavery parcel by drone
        /// </summary>
        /// <param name="droneId"></param>
        public void DeliveryParcelByDrone(int droneId)//Update-dilavery parcel by drone
        {

            try
            {
                if (droneId < 0) // if id is negative
                {
                    throw new InvalidIdException("negative", droneId);
                }
                Drone drone = GetDroneById(droneId);
                if (drone.DroneStatuses != Enums.DroneStatuses.Delivery || drone.ParcelInTransit.Status)
                {
                    throw new UnavailableExeption("drone", droneId);
                }
                if (GetParcelById(drone.ParcelInTransit.Id).PickupTime != DateTime.MinValue)
                {
                    throw new NotAssociatedException("drone", droneId);
                }
                drone.Battery = CalculateBattery(null, drone);
                drone.CurrentLocation = drone.ParcelInTransit.DeliveryLocation;
                drone.DroneStatuses = Enums.DroneStatuses.Available;
                DroneToList newDrone = ConvertDroneBlToList(drone);
                int index = ListOfDronsBL.FindIndex(element => element.Id == droneId);
                ListOfDronsBL[index] = newDrone;
                dal.UpdateParcleDelivery(drone.ParcelInTransit.Id);
            }
            catch (IDAL.DO.IdNotExsistException exception) // if droneid does not exsists and was thrown from dal objects
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }

        }
        #endregion
    }



}

