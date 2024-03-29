﻿using System;
using DalApi;
using BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace BlApi
{
    /// <summary>
    /// All the Get Lists functions for BL
    /// </summary>
    internal sealed partial class BL : IBl
    {
        #region//Get list of base stations
        /// <summary>
        /// //Get list of base stations
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetListOfBaseStations()////Get list of base stations
        {
            lock (dal)
            {
                List<BaseStation> list = new List<BaseStation>();
                foreach (var item in dal.GetListOfBaseStation())
                {
                    list.Add(GetBaseStationById(item.Id));
                }
                return list; 
            }
        }
        #endregion
        #region//Get list of base station to list
        /// <summary>
        /// //Get list of base station to list
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationToList> GetListOfBaseStationsToList()//Get list of base station to list
        {
            lock (dal)
            {
                ObservableCollection<BaseStationToList> list = new ObservableCollection<BaseStationToList>();
                foreach (var item in dal.GetListOfBaseStation())
                {
                    list.Add(convertBasestationToBasestationTolist(GetBaseStationById(item.Id)));
                }
                return list; 
            }
        }
        #endregion
        #region//Get list of drones
        /// <summary>
        /// Get list of drones
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetListOfDrones()//Get list of drones
        {
            lock (dal)
            {lock (dal)
                {
                    List<Drone> list = new List<Drone>();
                    foreach (var item in dal.GetListOfDrone())
                    {
                        list.Add(GetDroneById(item.Id));
                    }
                    return list;
                }
            }
        }
        #endregion
        #region//Get list of drone to list
        /// <summary>
        /// Get list of drone to list
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetListOfDronesToList()//Get list of drone to list
        {
            lock (dal)
            {
                List<DroneToList> list = new List<DroneToList>();
                foreach (var item in dal.GetListOfDrone())
                {
                    list.Add(convertDroneBlToList(GetDroneById(item.Id)));
                }
                return list; 
            }
        }
        #endregion
        #region//Get list of drones by predicat
        /// <summary>
        /// Get list of drones by predicat
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<DroneToList> GetListOfDroneToListByPredicat(Predicate<DroneToList> predicate , Predicate<DroneToList> predicate1=null) //Get list of drones by predicat
        {
            lock (dal)
            {
                List<DroneToList> list1 = new List<DroneToList>();
                foreach (var item in ListOfDronsBL)
                {
                    list1.Add(convertDroneBlToList(GetDroneById(item.Id)));
                }
                if (predicate1 == null)
                {
                    return list1.FindAll(predicate).ToList();
                }
                else
                {
                    return list1.FindAll(predicate).FindAll(predicate1).ToList();
                } 
            }
        }
        #endregion
        #region//Get list of customers
        /// <summary>
        /// //Get list of customers
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetListOfCustomers()//Get list of customers
        {
            lock (dal)
            {
                List<Customer> list = new List<Customer>();
                foreach (var item in dal.GetListOfCustomer())
                {
                    list.Add(GetCustomerById(item.Id));
                }
                return list; 
            }
        }
        #endregion
        #region//Get list of customer to list
        /// <summary>
        /// //Get list of customers
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetListOfCustomerToList()//Get list of customers
        {
            lock (dal)
            {
                List<CustomerToList> list = new List<CustomerToList>();
                foreach (var item in dal.GetListOfCustomer())
                {
                    list.Add(convertCustomerToCustomerTolist(GetCustomerById(item.Id)));
                }
                return list; 
            }
        }
        #endregion
        #region//Get list of parcel to list
        /// <summary>
        /// Get list of parcel to list
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetListOfParcelToList()//Get list of parcel to list
        {
            lock (dal)
            {
                List<ParcelToList> list = new List<ParcelToList>();
                foreach (var item in dal.GetListOfParcel())
                {
                    list.Add(convertParcelToParcelTolist(GetParcelById(item.Id)));
                }
                return list; 
            }
        }
        #endregion
        #region//Get list of parcels
        /// <summary>
        /// Get list of parcels
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetListOfParcels()//Get list of parcels
        {
            lock (dal)
            {
                List<Parcel> list = new List<Parcel>();
                foreach (var item in dal.GetListOfParcel())
                {
                    list.Add(GetParcelById(item.Id));
                }
                return list; 
            }
        }
        #endregion
        #region//Get list of assosiated drones
        /// <summary>
        /// Get list of assosiated drones
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<ParcelToList> GetListOfNotAssigned()//Get list of assosiated drones
        {
            lock (dal)
            {
                List<ParcelToList> list = new List<ParcelToList>();
                Predicate<DO.Parcel> predicate = element => element.DroneId == null;
                List<DO.Parcel> listIdal = dal.GetListOfParcel(predicate).ToList();
                foreach (var item in listIdal)
                {
                    list.Add(convertParcelToParcelTolist(GetParcelById(item.Id)));
                }
                return list; 
            }
        }
        #endregion
        #region//Get list of free charging stations
        /// <summary>
        /// Get list of free charging stations
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<BaseStationToList> GetListOfFreeChargingStations(int num = 0)//Get list of free charging stations
        {
            lock (dal)
            {
                List<BaseStationToList> list = new List<BaseStationToList>();
                Predicate<DO.BaseStation> predicate;
                if (num == 0)
                {
                    predicate = element => element.ChargeSlots > num;

                }
                else
                {
                    predicate = element => element.ChargeSlots == num;

                }
                List<DO.BaseStation> listIdal = dal.GetListOfBaseStation(predicate).ToList();
                foreach (var item in listIdal)
                {
                    list.Add(convertBasestationToBasestationTolist(GetBaseStationById(item.Id)));
                }
                return list; 
            }
        }
        #endregion
        #region//Get list of customers that dalivered
        /// <summary>
        /// Get list of customers that dalivered
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<Customer> GetListOfCustomerDalivered() //Get list of customers that dalivered
        {
            lock (dal)
            {
                var customerDalList = dal.GetListOfCustomer().ToList();
                List<Customer> customerBlList = null;
                foreach (var item in customerDalList)
                {
                    customerBlList.Add(convertCustomerDalToBl(item));
                }
                return customerBlList.FindAll(element => element.ParcelToCustomer.Find(elementi => elementi.ParcelStatus == Enums.ParcelStatus.Supplied).Equals(null)); 
            }
        }
        #endregion
    }
}
