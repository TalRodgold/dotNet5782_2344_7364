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
        #region//Convert from dal drone to drone to list
        /// <summary>
        /// Convert from dal drone to drone to list
        /// </summary>
        /// <param name="idalDrone"></param>
        /// <returns></returns>
        public DroneToList ConvertDroneDalToList(IDAL.DO.Drone idalDrone)//Convert from dal drone to drone to list
        {

            Random rnd = new Random();
            DroneToList newDrone = new DroneToList();
            newDrone.Id = idalDrone.Id;
            newDrone.Model = idalDrone.Model;
            newDrone.Weight = (Enums.WeightCategories)idalDrone.MaxWeight;


            var listOfParcels = dal.GetListOfParcel().ToList();
            IDAL.DO.Parcel newParcel = listOfParcels.Find(element => element.DroneId == idalDrone.Id);
            if (newParcel.Id != 0)//(!newParcel.Equals(null))//if there have a parcel with this drone id
            {
                newDrone.DroneStatuses = Enums.DroneStatuses.Delivery;
                if (newParcel.Scheduled <= DateTime.MinValue)//the parcel didn't pick-upp
                {
                    Location newLocation = new Location(dal.GetCustomer(newParcel.SenderId).Longtitude, dal.GetCustomer(newParcel.SenderId).Latitude);
                    int baseStationId = CalculateMinDistance(newLocation);
                    IDAL.DO.BaseStation newBaseStation = dal.GetBaseStation(baseStationId);
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
                newDrone.DroneStatuses = (Enums.DroneStatuses)myValues[rnd.Next(0, 1)];
                if (newDrone.DroneStatuses == Enums.DroneStatuses.Maintenance)//if the drone in maintence status
                {
                    List<IDAL.DO.BaseStation> baseStationsList = dal.GetListOfBaseStation().ToList();
                    int index = rnd.Next(baseStationsList.Count);
                    newDrone.CurrentLocation = new Location(baseStationsList[index].Longtitude, baseStationsList[index].Latitude);
                    newDrone.Battery = CalculateBattery(newDrone);
                }
                else//if the drone is in avilible status
                {
                    Predicate<IDAL.DO.Parcel> predicate1 = element => element.Deliverd > DateTime.MinValue;
                    List<IDAL.DO.Parcel> listOfDeliveredParcel = dal.GetListOfParcel(predicate1).ToList();//all the parcel that delivered
                                                                                                          // listOfDeliveredParcel = listOfDeliveredParcel.FindAll(element => element.DroneId == newDrone.Id);
                    if (listOfDeliveredParcel.Count != 0)//if there have pacel that has been delivered
                    {
                        IDAL.DO.Parcel newParcel_ = listOfDeliveredParcel[rnd.Next(listOfDeliveredParcel.Count)];
                        IDAL.DO.Customer newCustomer = dal.GetCustomer(newParcel_.TargetId);
                        newDrone.CurrentLocation = new Location(newCustomer.Longtitude, newCustomer.Latitude);
                    }
                    else//if there no have delivered parcel 
                    {
                        List<BaseStation> baseTationList = GetListOfBaseStations();
                        newDrone.CurrentLocation = baseTationList[rnd.Next(0, baseTationList.Count)].Location;
                    }
                }
                newDrone.NumberOfParcelInTransit = -1;
            }
            newDrone.Battery = CalculateBattery(newDrone);

            return newDrone;
        }
        #endregion
        #region//Convert from bl drone to drone to list
        /// <summary>
        /// Convert from bl drone to drone to list
        /// </summary>
        /// <param name="blDrone"></param>
        /// <returns></returns>
        public DroneToList ConvertDroneBlToList(Drone blDrone)
        {
            DroneToList newDrone = new DroneToList(blDrone.Id, blDrone.Model, blDrone.Weight, blDrone.Battery, blDrone.DroneStatuses, blDrone.CurrentLocation, blDrone.ParcelInTransit.Id);
            return newDrone;
        }
        #endregion
        #region//Convert from dal customer to bl customer
        /// <summary>
        /// //Convert from dal customer to bl customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public Customer convertCustomerDalToBl(IDAL.DO.Customer customer)////Convert from dal customer to bl customer
        {
            List<IDAL.DO.Parcel> parcelList = dal.GetListOfParcel(element => (element.SenderId != -1)).ToList();
            List<IDAL.DO.Parcel> parcelListEnder = parcelList.FindAll(element => element.SenderId == customer.Id);
            List<IDAL.DO.Parcel> parcelListReciver = parcelList.FindAll(element => element.TargetId == customer.Id);
            if (parcelList.Count != 0)
            {
                List<ParcelAtCustomer> parcelAtCustomersListSender = null;
                foreach (var item in parcelListEnder)
                {
                    parcelAtCustomersListSender.Add(new ParcelAtCustomer(item.Id, (Enums.WeightCategories)item.Weight, (Enums.Priorities)item.Priority, StatusCalculate(dal.GetParcel(item.Id)), new CustomerInParcel(item.SenderId, GetCustomerById(item.SenderId).Name)));
                }
                List<ParcelAtCustomer> parcelAtCustomersListReciver = null;
                foreach (var item in parcelListReciver)
                {
                    parcelAtCustomersListReciver.Add(new ParcelAtCustomer(item.Id, (Enums.WeightCategories)item.Weight, (Enums.Priorities)item.Priority, StatusCalculate(dal.GetParcel(item.Id)), new CustomerInParcel(item.TargetId, GetCustomerById(item.TargetId).Name)));
                }
                return new Customer(customer.Id, customer.Name, customer.Phone, new Location(customer.Longtitude, customer.Latitude), parcelAtCustomersListSender, parcelAtCustomersListReciver);
            }
            else return null;//maybe exption
        }
        #endregion
    }
}
