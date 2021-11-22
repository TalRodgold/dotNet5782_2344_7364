using System;
using IDAL;
using IBL.BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace IBL
{
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
        #region//Get list of customers
        /// <summary>
        /// //Get list of customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetListOfCustomers()////Get list of customers
        {
            List<Customer> list = new List<Customer>();
            foreach (var item in dal.GetListOfCustomer())
            {
                list.Add(GetCustomerById(item.Id));
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
        public List<Parcel> GetListOfNotAssigned()//Get list of assosiated drones
        {
            List<Parcel> list = new List<Parcel>();
            foreach (var item in dal.GetListOfParcel())
            {
                if (GetParcelById(item.Id, element => element.DroneId > 0) != null)
                {
                    list.Add(GetParcelById(item.Id, element => element.DroneId > 0));
                }
            }
            return list;
        }
        #endregion
        #region//Get list of free charging stations
        /// <summary>
        /// Get list of free charging stations
        /// </summary>
        /// <returns></returns>
        public List<BaseStation> GetListOfFreeChargingStations()//Get list of free charging stations
        {
            List<BaseStation> list = new List<BaseStation>();
            foreach (var item in dal.GetListOfBaseStation())
            {
                if (GetBaseStationById(item.Id).NumberOfFreeChargingSlots > 0)
                {
                    list.Add(GetBaseStationById(item.Id));
                }
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
