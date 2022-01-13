using System;
using DalApi;
using BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

namespace BlApi
{
    /// <summary>
    /// All the updates functions for BL
    /// </summary>
    internal sealed partial class BL : IBl
    {
        #region//Update drone model
        /// <summary>
        /// Update drone model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newModel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDroneModel(int? id, string newModel)//Update drone model
        {
            try
            {
                lock (dal)
                {
                    if (id < 0 || id == null)// if id is negative
                    {
                        throw new InvalidIdException("negative", id);
                    }
                    DroneToList newDroneToList = GetDroneToList(id);
                    newDroneToList.Model = newModel;
                    int index = ListOfDronsBL.FindIndex(element => element.Id == id);
                    ListOfDronsBL[index] = newDroneToList;
                    dal.UpdateDroneModel(id, newModel);
                }
            }
            catch (DO.IdNotExsistException exception)
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateBaseStation(int? id, string name = "", int numberOfChargingSlots = 0)//Update base station name/number of charging slots
        {
            try
            {
                lock (dal)
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
            }
            catch (DO.IdNotExsistException exception)
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception);
            }
            catch (DO.SizeProblemException exception)
            {
                throw new SizeProblemException(exception.Text, exception.Number, exception);
            }
        }
        #endregion
        #region//Delete base station
        /// <summary>
        /// Delete base station
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteBaseStation(int? id) // delete base station 
        {
            try
            {
                lock (dal)
                {
                    if (id < 0 || id == null)// if id is negative
                    {
                        throw new InvalidIdException("negative", id);
                    }
                    dal.DeleteBaseStation(id);
                }
            }
            catch (DO.IdNotExsistException exception)
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception);
            }
        }
        #endregion
        #region//Delete parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int? id)
        {
            try
            {
                lock (dal)
                {
                    if (id < 0 || id == null)// if id is negative
                    {
                        throw new InvalidIdException("negative", id);
                    }
                    dal.DeleteParcel(id);
                }
            }
            catch (DO.IdNotExsistException exception)
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception);
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int? id, string name = "", string phone = "")//Update customer name/phone
        {
            try
            {
                lock (dal)
                {

                    if (id < 0 || id == null) // if id is negative
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
            }
            catch (DO.IdNotExsistException exception)
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int? UpdateSendDroneToCharge(int? id)//Update-send drone to charge
        {
            try
            {
                lock (dal)
                {
                    if (id < 0 || id == null) // if id is negative
                    {
                        throw new InvalidIdException("negative", id);
                    }
                    GetDroneById(id);// check if drone exsists in dal
                    DroneToList newDrone = ListOfDronsBL.Find(element => element.Id == id);
                    if (newDrone.DroneStatuses != Enums.DroneStatuses.Available) // if drone is unavalible
                    {
                        throw new UnavailableExeption("drone", id);
                    }
                    int? stationId = calculateMinDistance(GetDroneById(id).CurrentLocation, element => element.NumberOfFreeChargingSlots > 0, element => calculateDistance(element.Location, newDrone.CurrentLocation) <= convertBatteryToDistance(newDrone));
                    if (stationId == null) // if there are no free base stations
                    {
                        throw new UnavailableExeption("base station", stationId);
                    }
                    DateTime? currentTime = DateTime.Now;
                    dal.AddDroneCharge(id, stationId, currentTime);
                    BaseStation station = GetBaseStationById(stationId);
                    double distance = calculateDistance(newDrone.CurrentLocation, station.Location);
                    if (distance != 0)
                    {
                        newDrone.Battery -= calculateBattery(newDrone, distance);
                    }
                    newDrone.CurrentLocation = station.Location;
                    newDrone.DroneStatuses = Enums.DroneStatuses.Maintenance;
                    int index = ListOfDronsBL.FindIndex(element => element.Id == id);
                    ListOfDronsBL[index] = newDrone;
                    station.NumberOfFreeChargingSlots -= 1;
                    dal.UpdateBaseStationNumOfFreeDroneCharges(station.Id, station.NumberOfFreeChargingSlots);
                    DroneInCharging droneInCharging = new DroneInCharging(newDrone.Id, newDrone.Battery, currentTime);
                    return stationId;
                }
            }
            catch (DO.IdNotExsistException exception) // if base station id does not exsists and was thrown from dal objects
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int? UpdateReleseDrone(int? id)//Update-relese drone from charging slot
        {
            lock (dal)
            {
                if (id < 0 || id == null) // if id is negative
                {
                    throw new InvalidIdException("negative", id);
                }
                DroneToList drone = GetDroneToList(id);
                if (dal.GetDroneCharge(drone.Id).TimeOfStartCharging == null)
                    throw new Exception();
                TimeSpan Diff = (TimeSpan)(DateTime.Now - dal.GetDroneCharge(drone.Id).TimeOfStartCharging);
                if (drone.DroneStatuses != Enums.DroneStatuses.Maintenance)
                {
                    throw new UnavailableExeption("drone", id);
                }
                if ((drone.Battery + (Diff.Hours + (double)Diff.Minutes / 60 + (double)Diff.Seconds / 3600) * DroneChargingPaste >= 1)) // if charged more than 100 %
                {
                    drone.Battery = 1;
                }
                else
                {
                    drone.Battery = drone.Battery + (Diff.Hours + (double)Diff.Minutes / 60 + (double)Diff.Seconds / 3600) * DroneChargingPaste / 100;
                }
                drone.DroneStatuses = Enums.DroneStatuses.Available;
                DO.DroneCharge droneCharge = dal.GetDroneCharge(id, element => element.DroneId == id);
                DO.BaseStation station = dal.getBaseStationByDroneId(droneCharge.DroneId);
                int index = ListOfDronsBL.FindIndex(element => element.Id == id);
                ListOfDronsBL[index] = drone;
                station.ChargeSlots -= 1;
                dal.UpdateBaseStationNumOfFreeDroneCharges(station.Id, station.ChargeSlots);
                dal.ReleaseDroneCharge(id, station.Id);
                return droneCharge.StationId;
            }
             
        }
        #endregion
        #region//update and associate a drone
        /// <summary>
        /// update and associate a drone
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int? UpdateAssosiateDrone(int? id)//Update-assosiate drone to parcel
        {
            {
                try
                {
                    lock (dal)
                    {
                        if (id < 0 || id == null) // if id is negative
                        {
                            throw new InvalidIdException("negative", id);
                        }
                        if (!dal.IfDroneExsists(id))
                        {
                            throw new IdNotExsistException("drone", id);
                        }
                        DroneToList drone = ListOfDronsBL.Find(element => element.Id == id);
                        int index = ListOfDronsBL.FindIndex(element => element.Id == id);
                        if (drone.DroneStatuses != Enums.DroneStatuses.Available) // if drone is unavalible
                        {
                            throw new UnavailableExeption("drone", id);
                        }
                        Enums.Priorities maxPriorities = Enums.Priorities.Regular;
                        double minDistance = double.MaxValue;
                        int? properParcelID = null;
                        Customer senderCustomer = new Customer();
                        Customer reciverCustomer = new Customer();
                        double distanceDroneToPickup, distancePickupToDelivery, distanceDeliveryToClothestBaseStation, distance = 0;
                        foreach (var item in dal.GetListOfParcel())
                        {
                            Customer newSenderCustomer = GetCustomerById(item.SenderId); // sender
                            Customer newReciverCustomer = GetCustomerById(item.ReciverId); // reciver
                            distanceDroneToPickup = calculateDistance(drone.CurrentLocation, newSenderCustomer.Location); // distance
                            distancePickupToDelivery = calculateDistance(newSenderCustomer.Location, newReciverCustomer.Location);
                            distanceDeliveryToClothestBaseStation = calculateDistance(newReciverCustomer.Location, GetBaseStationById(calculateMinDistance(newReciverCustomer.Location)).Location); // closest
                            distance = distanceDroneToPickup + distancePickupToDelivery + distanceDeliveryToClothestBaseStation;
                            if (item.Deliverd == null && (Enums.WeightCategories)item.Weight <= drone.Weight && (Enums.Priorities)item.Priority >= maxPriorities && distanceDroneToPickup <= minDistance && CalculateWhetherTheDroneHaveEnoghBattery(distance, drone))//&& item.AssociatedTime != null)//-----------------------
                            {
                                maxPriorities = (Enums.Priorities)item.Priority;
                                minDistance = distanceDroneToPickup;
                                properParcelID = item.Id;
                                senderCustomer = newSenderCustomer;
                                reciverCustomer = newReciverCustomer;
                            }
                        }
                        if (properParcelID == null)
                        {
                            throw new NotAssociatedException("drone", id);
                        }
                        dal.AssociateDroneToParcel(id, properParcelID); // associate

                        drone.DroneStatuses = Enums.DroneStatuses.Delivery;
                        drone.NumberOfParcelInTransit = properParcelID;
                        ListOfDronsBL[index] = drone;
                        return properParcelID;
                    }
                }
                catch (DO.IdNotExsistException exception) // if droneid does not exsists and was thrown from dal objects
                {

                    throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
                }
            }
        }
        #endregion
        #region//Update-pick-up parcel by drone
        /// <summary>
        /// Update-pick-up parcel by drone
        /// </summary>
        /// <param name="droneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickupParcelByDrone(int? droneId)//Update-pick-up parcel by drone
        {
            try
            {

                lock (dal)
                {
                    if (droneId < 0 || droneId == null) // if id is negative
                    {
                        throw new InvalidIdException("negative", droneId);
                    }
                    Drone drone = GetDroneById(droneId);
                    if (drone.DroneStatuses != Enums.DroneStatuses.Delivery || !drone.ParcelInTransit.Status) // if drone is not in delivery
                    {
                        throw new UnavailableExeption("drone", droneId); // throw
                    }
                    if (drone.ParcelInTransit.Id == null || GetParcelById(drone.ParcelInTransit.Id).AssociationTime == null) // if no parcel associated
                    {
                        throw new NotAssociatedException("drone", droneId); // throw
                    }
                    drone.Battery -= calculateBattery(drone, calculateDistance(drone.CurrentLocation, drone.ParcelInTransit.PickupLocation));//(drone.Battery * ElectricityUseAvailiblity * calculateDistance(drone.CurrentLocation, drone.ParcelInTransit.PickupLocation)) / 100;
                    drone.CurrentLocation = drone.ParcelInTransit.PickupLocation;
                    drone.ParcelInTransit.Status = false;
                    DroneToList newDrone = convertDroneBlToList(drone);
                    int index = ListOfDronsBL.FindIndex(element => element.Id == droneId);
                    ListOfDronsBL[index] = newDrone;
                    dal.UpdateParclePickup(drone.ParcelInTransit.Id);
                }
            }
            catch (DO.IdNotExsistException exception) // if droneid does not exsists and was thrown from dal objects
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliveryParcelByDrone(int? droneId)//Update-dilavery parcel by drone
        {

            try
            {
                lock (dal)
                {
                    if (droneId < 0 || droneId == null) // if id is negative
                    {
                        throw new InvalidIdException("negative", droneId);
                    }
                    Drone drone = GetDroneById(droneId);
                    if (drone.DroneStatuses != Enums.DroneStatuses.Delivery || drone.ParcelInTransit.Status) // if drone not in delivery
                    {
                        throw new UnavailableExeption("drone", droneId); // throw
                    }
                    Parcel newParcel = GetParcelById(drone.ParcelInTransit.Id);
                    if (newParcel.PickupTime == null) // if parcel not associated
                    {
                        throw new NotAssociatedException("drone", droneId); // throw
                    }
                    int? station = calculateMinDistance(drone.CurrentLocation);
                    drone.Battery -= calculateBattery(drone, calculateDistance(drone.CurrentLocation, GetBaseStationById(station).Location));//(drone.CurrentLocation, drone.ParcelInTransit.DeliveryLocation));
                    drone.CurrentLocation = GetBaseStationById(station).Location;//drone.ParcelInTransit.DeliveryLocation;
                    drone.DroneStatuses = Enums.DroneStatuses.Available;
                    DroneToList newDrone = convertDroneBlToList(drone);
                    newDrone.NumberOfParcelInTransit = null;
                    int index = ListOfDronsBL.FindIndex(element => element.Id == droneId);
                    ListOfDronsBL[index] = newDrone;
                    dal.UpdateParcleDelivery(drone.ParcelInTransit.Id);
                }
            }
            catch (DO.IdNotExsistException exception) // if droneid does not exsists and was thrown from dal objects.
            {

                throw new IdNotExsistException(exception.Text, exception.ID, exception); // throw
            }
        }
        #endregion
        #region// check for any available parcels
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool CheckAvailableParcels(Drone drone) // check for any available parcels
        {
            lock (dal)
            {
                Enums.Priorities maxPriorities = Enums.Priorities.Regular;
                double minDistance = double.MaxValue;
                Customer senderCustomer = new Customer();
                Customer reciverCustomer = new Customer();
                double distanceDroneToPickup, distancePickupToDelivery, distanceDeliveryToClothestBaseStation, distance = 0;

                foreach (var item in dal.GetListOfParcel())
                {
                    Customer newSenderCustomer = GetCustomerById(item.SenderId); // sender
                    Customer newReciverCustomer = GetCustomerById(item.ReciverId); // reciver
                    distanceDroneToPickup = calculateDistance(drone.CurrentLocation, newSenderCustomer.Location); // distance
                    distancePickupToDelivery = calculateDistance(newSenderCustomer.Location, newReciverCustomer.Location);
                    distanceDeliveryToClothestBaseStation = calculateDistance(newReciverCustomer.Location, GetBaseStationById(calculateMinDistance(newReciverCustomer.Location)).Location); // closest
                    distance = distanceDroneToPickup + distancePickupToDelivery + distanceDeliveryToClothestBaseStation;
                    if (item.Deliverd == null && (Enums.WeightCategories)item.Weight <= drone.Weight && (Enums.Priorities)item.Priority >= maxPriorities && distanceDroneToPickup <= minDistance && CalculateWhetherTheDroneHaveEnoghBattery(distance, convertDroneBlToList(drone)))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        #endregion
        #region// start simulator
        public void StartSimulator(int droneId, Action func, Func<bool> checkStop) => new Simulator(this, droneId, func, checkStop);
        #endregion
    }
}