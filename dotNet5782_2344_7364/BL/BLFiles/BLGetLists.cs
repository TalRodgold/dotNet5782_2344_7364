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
    /// All the Get Lists functions for BL
    /// </summary>
    public partial class BL : IBl
    {
        #region//Get list of base stations
        /// <summary>
        /// //Get list of base stations
        /// </summary>
        /// <returns></returns>
        public List<BaseStation> GetListOfBaseStations()////Get list of base stations
        {
            List<BaseStation> list = new List<BaseStation>();
            foreach (var item in dal.GetListOfBaseStation())
            {
                list.Add(GetBaseStationById(item.Id));
            }
            return list;
        }
        #endregion
        #region//Get list of base station to list
        /// <summary>
        /// //Get list of base station to list
        /// </summary>
        /// <returns></returns>
        public List<BaseStationToList> GetListOfBaseStationsToList()//Get list of base station to list
        {
            List<BaseStationToList> list = new List<BaseStationToList>();
            foreach (var item in dal.GetListOfBaseStation())
            {
                list.Add(convertBasestationToBasestationTolist(GetBaseStationById(item.Id)));
            }
            return list;
        }
        #endregion
        #region//Get list of drones
        /// <summary>
        /// Get list of drones
        /// </summary>
        /// <returns></returns>
        public List<Drone> GetListOfDrones()//Get list of drones
        {
            List<Drone> list = new List<Drone>();
            foreach (var item in dal.GetListOfDrone())
            {
                list.Add(GetDroneById(item.Id));
            }
            return list;
        }
        #endregion
        #region//Get list of drone to list
        /// <summary>
        /// Get list of drone to list
        /// </summary>
        /// <returns></returns>
        public List<DroneToList> GetListOfDronesToList()//Get list of drone to list
        {
            List<DroneToList> list = new List<DroneToList>();
            foreach (var item in dal.GetListOfDrone())
            {
                list.Add(convertDroneBlToList(GetDroneById(item.Id)));
            }
            return list;
        }
        #endregion
        #region//Get list of drones by predicat
        /// <summary>
        /// Get list of drones by predicat
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<DroneToList> GetListOfDroneToListByPredicat(Predicate<DroneToList> predicate) //Get list of drones by predicat
        {
            List<DroneToList> list1 = new List<DroneToList>();
            foreach (var item in ListOfDronsBL)
            {
                list1.Add(convertDroneBlToList(GetDroneById(item.Id)));
            }
            return list1.FindAll(predicate).ToList();
        }
        #endregion
        #region//Get list of customers
        /// <summary>
        /// //Get list of customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetListOfCustomers()//Get list of customers
        {
            List<Customer> list = new List<Customer>();
            foreach (var item in dal.GetListOfCustomer())
            {
                list.Add(GetCustomerById(item.Id));
            }
            return list;
        }
        #endregion
        #region//Get list of customer to list
        /// <summary>
        /// //Get list of customers
        /// </summary>
        /// <returns></returns>
        public List<CustomerToList> GetListOfCustomerToList()//Get list of customers
        {
            List<CustomerToList> list = new List<CustomerToList>();
            foreach (var item in dal.GetListOfCustomer())
            {
                list.Add(convertCustomerToCustomerTolist(GetCustomerById(item.Id)));
            }
            return list;
        }
        #endregion
        #region//Get list of parcel to list
        /// <summary>
        /// Get list of parcel to list
        /// </summary>
        /// <returns></returns>
        public List<ParcelToList> GetListOfParcelToList()//Get list of parcel to list
        {
            List<ParcelToList> list = new List<ParcelToList>();
            foreach (var item in dal.GetListOfParcel())
            {
                list.Add(convertParcelToParcelTolist(GetParcelById(item.Id)));
            }
            return list;
        }
        #endregion
        #region//Get list of parcels
        /// <summary>
        /// Get list of parcels
        /// </summary>
        /// <returns></returns>
        public List<Parcel> GetListOfParcels()//Get list of parcels
        {
            List<Parcel> list = new List<Parcel>();
            foreach (var item in dal.GetListOfParcel())
            {
                list.Add(GetParcelById(item.Id));
            }
            return list;
        }
        #endregion
        #region//Get list of assosiated drones
        /// <summary>
        /// Get list of assosiated drones
        /// </summary>
        /// <returns></returns>
        public List<ParcelToList> GetListOfNotAssigned()//Get list of assosiated drones
        {
            List<ParcelToList> list = new List<ParcelToList>();
            Predicate<IDAL.DO.Parcel> predicate = element => element.DroneId == null;
            List<IDAL.DO.Parcel> listIdal = dal.GetListOfParcel(predicate).ToList();
            foreach (var item in listIdal)
            {
                list.Add(convertParcelToParcelTolist(GetParcelById(item.Id)));  
            }
            return list;
        }
        #endregion
        #region//Get list of free charging stations
        /// <summary>
        /// Get list of free charging stations
        /// </summary>
        /// <returns></returns>
        public List<BaseStationToList> GetListOfFreeChargingStations()//Get list of free charging stations
        {
            List<BaseStationToList> list = new List<BaseStationToList>();
            Predicate<IDAL.DO.BaseStation> predicate = element => element.ChargeSlots > 0;
            List<IDAL.DO.BaseStation> listIdal = dal.GetListOfBaseStation(predicate).ToList();
            foreach (var item in listIdal)
            {
                list.Add(convertBasestationToBasestationTolist(GetBaseStationById(item.Id)));
            }
            return list;
        }
        #endregion
        #region//Get list of customers that dalivered
        /// <summary>
        /// Get list of customers that dalivered
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetListOfCustomerDalivered() //Get list of customers that dalivered
        {
            var customerDalList = dal.GetListOfCustomer().ToList();
            List<Customer> customerBlList = null;
            foreach (var item in customerDalList)
            {
                customerBlList.Add(convertCustomerDalToBl(item));
            }
            return customerBlList.FindAll(element => element.ParcelToCustomer.Find(elementi => elementi.ParcelStatus == Enums.ParcelStatus.Supplied).Equals(null));
        }
        #endregion
    }
}
