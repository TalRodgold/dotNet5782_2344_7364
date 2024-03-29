﻿using System;
using DalApi;
using BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BlApi
{
    /// <summary>
    /// All the convertion functions for BL
    /// </summary>
    internal sealed partial class BL : IBl
    {
        #region//Convert from dal drone to drone to list
        /// <summary>
        /// Convert from dal drone to drone to list
        /// </summary>
        /// <param name="idalDrone"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private DroneToList convertDroneDalToList(DO.Drone idalDrone)//Convert from dal drone to drone to list.
        {
            lock (dal)
            {
                Random rnd = new Random();
                DroneToList newDrone = new DroneToList();
                newDrone.Id = idalDrone.Id;
                newDrone.Model = idalDrone.Model;
                newDrone.Weight = (Enums.WeightCategories)idalDrone.MaxWeight;
                var listOfParcels = dal.GetListOfParcel().ToList();
                DO.Parcel newParcel = listOfParcels.Find(element => element.DroneId == idalDrone.Id);
                if (newParcel.Id != null)//if there have a parcel with this drone id
                {
                    newDrone.DroneStatuses = Enums.DroneStatuses.Delivery;
                    if (newParcel.AssociatedTime == null)//the parcel didn't pick-upp
                    {
                        Location newLocation = new Location(dal.GetCustomer(newParcel.SenderId).Longtitude, dal.GetCustomer(newParcel.SenderId).Latitude);
                        int? baseStationId = calculateMinDistance(newLocation);
                        DO.BaseStation newBaseStation = dal.GetBaseStation(baseStationId);
                        newDrone.CurrentLocation = new Location(newBaseStation.Longtitude, newBaseStation.Latitude);
                    }
                    else//the parcel was pick-upp
                    {
                        newDrone.CurrentLocation = new Location(dal.GetCustomer(newParcel.SenderId).Longtitude, dal.GetCustomer(newParcel.SenderId).Latitude);//he get the the sender customer address
                    }
                    newDrone.NumberOfParcelInTransit = newParcel.Id;
                }
                else //if there no have parcel with this drone id
                {
                    var myValues = new int[] { 0, 2 }; // Will work with array or list
                    newDrone.DroneStatuses = (Enums.DroneStatuses)myValues[rnd.Next(0, 2)];
                    if (newDrone.DroneStatuses == Enums.DroneStatuses.Maintenance)//if the drone in maintence status
                    {
                        List<DO.BaseStation> baseStationsList = dal.GetListOfBaseStation().ToList();
                        int index = rnd.Next(baseStationsList.Count);
                        dal.UpdateDroneCharge(newDrone.Id, baseStationsList[index].Id, DateTime.Now);
                        newDrone.CurrentLocation = new Location(baseStationsList[index].Longtitude, baseStationsList[index].Latitude);
                    }
                    else//if the drone is in avilible status
                    {
                        Predicate<DO.Parcel> predicate1 = element => element.Deliverd != null;
                        List<DO.Parcel> listOfDeliveredParcel = dal.GetListOfParcel(predicate1).ToList();//all the parcel that delivered
                        if (listOfDeliveredParcel.Count != 0)//if there have pacel that has been delivered
                        {
                            DO.Parcel newParcel_ = listOfDeliveredParcel[rnd.Next(listOfDeliveredParcel.Count)];
                            DO.Customer newCustomer = dal.GetCustomer(newParcel_.ReciverId);
                            newDrone.CurrentLocation = new Location(newCustomer.Longtitude, newCustomer.Latitude);
                        }
                        else//if there no have delivered parcel 
                        {
                            List<BaseStation> baseTationList = GetListOfBaseStations().ToList();
                            newDrone.CurrentLocation = baseTationList[rnd.Next(0, baseTationList.Count)].Location;
                            newDrone.Battery = calculateBattery(newDrone);
                            newDrone.NumberOfParcelInTransit = null;
                            return newDrone;
                        }
                    }
                    newDrone.NumberOfParcelInTransit = null;
                }
                newDrone.Battery = calculateBattery(newDrone);
                return newDrone; 
            }
        }
        #endregion
        #region//Convert from bl drone to drone to list
        /// <summary>
        /// Convert from bl drone to drone to list
        /// </summary>
        /// <param name="blDrone"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneToList convertDroneBlToList(Drone blDrone)//Convert drone from bl to list
        {
            lock (dal)
            {
                if (blDrone.ParcelInTransit == null)
                {
                    DroneToList newDrone = new DroneToList(blDrone.Id, blDrone.Model, blDrone.Weight, blDrone.Battery, blDrone.DroneStatuses, blDrone.CurrentLocation, null);
                    return newDrone;
                }
                else
                {
                    DroneToList newDrone = new DroneToList(blDrone.Id, blDrone.Model, blDrone.Weight, blDrone.Battery, blDrone.DroneStatuses, blDrone.CurrentLocation, blDrone.ParcelInTransit.Id);
                    return newDrone;
                } 
            }
            
        }
        #endregion
        #region//Convert from dal customer to bl customer
        /// <summary>
        /// //Convert from dal customer to bl customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private Customer convertCustomerDalToBl(DO.Customer customer)////Convert from dal customer to bl customer
        {
            lock (dal)
            {
                List<DO.Parcel> parcelList = dal.GetListOfParcel(element => (element.SenderId != null)).ToList();
                List<DO.Parcel> parcelListSender = parcelList.FindAll(element => element.SenderId == customer.Id);
                List<DO.Parcel> parcelListReciver = parcelList.FindAll(element => element.ReciverId == customer.Id);
                if (parcelList.Count != 0)
                {
                    List<ParcelAtCustomer> parcelAtCustomersListSender = null;
                    foreach (var item in parcelListSender)
                    {
                        parcelAtCustomersListSender.Add(new ParcelAtCustomer(item.Id, (Enums.WeightCategories)item.Weight, (Enums.Priorities)item.Priority, statusCalculate(dal.GetParcel(item.Id)), new CustomerInParcel(item.SenderId, GetCustomerById(item.SenderId).Name)));
                    }
                    List<ParcelAtCustomer> parcelAtCustomersListReciver = null;
                    foreach (var item in parcelListReciver)
                    {
                        parcelAtCustomersListReciver.Add(new ParcelAtCustomer(item.Id, (Enums.WeightCategories)item.Weight, (Enums.Priorities)item.Priority, statusCalculate(dal.GetParcel(item.Id)), new CustomerInParcel(item.ReciverId, GetCustomerById(item.ReciverId).Name)));
                    }
                    return new Customer(customer.Id, customer.Name, customer.Phone, new Location(customer.Longtitude, customer.Latitude), parcelAtCustomersListSender, parcelAtCustomersListReciver);
                }
                else return null; 
            }
        }
        #endregion
        #region//Convert basestation to basestation to list
        /// <summary>
        /// Convert basestation to basestation to list
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStationToList convertBasestationToBasestationTolist(BaseStation station)//Convert basestation to basestation to list
        {
            lock (dal)
            {
                return new BaseStationToList(station.Id, station.Name, station.NumberOfFreeChargingSlots, station.ListOfDroneInCharging.Count);

            }        
        }
        #endregion
        #region//Convert customer to customer to list
        /// <summary>
        /// Convert customer to customer to list
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private CustomerToList convertCustomerToCustomerTolist(Customer customer) //Convert customer to customer to list
        {
            lock (dal)
            {
                int numOfArraived, numOfRecived, numOfNotArraived, numOfOnWay; // 4 types of number of parcels
                numOfArraived = customer.ParcelFromCustomer.FindAll(element => element.ParcelStatus == Enums.ParcelStatus.Supplied).Count; // from customer and suplied
                numOfNotArraived = customer.ParcelFromCustomer.FindAll(element => element.ParcelStatus != Enums.ParcelStatus.Supplied).Count; // from customer and not suplied
                numOfRecived = customer.ParcelToCustomer.FindAll(element => element.ParcelStatus == Enums.ParcelStatus.Supplied).Count; // to customer and suplied
                numOfOnWay = customer.ParcelToCustomer.FindAll(element => element.ParcelStatus != Enums.ParcelStatus.Supplied).Count; // to customer and not suplied
                return new CustomerToList(customer.Id, customer.Name, customer.Phone, numOfArraived, numOfNotArraived, numOfRecived, numOfOnWay); 
            }
        }
        #endregion
        #region//Convert parcel to parcel to list
        /// <summary>
        /// Convert parcel to parcel to list
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private ParcelToList convertParcelToParcelTolist(Parcel parcel) //Convert parcel to parcel to list
        {
            lock (dal)
            {
                return new ParcelToList(parcel.Id, parcel.Sender.Name, parcel.Reciver.Name, parcel.Weight, parcel.Prioritie, statusCalculate(dal.GetParcel(parcel.Id)));

            }        
        }
        #endregion
    }
}
