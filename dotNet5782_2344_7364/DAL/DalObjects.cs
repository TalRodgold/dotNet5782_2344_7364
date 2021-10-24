using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObjects
{
    /// <summary>
    /// Holds functions that allow to acsses data
    /// </summary>
    public class DalObjects
    {
        /// <summary>
        /// contains functions that allow to acsses and change the data in DataSource
        /// </summary>
        public DalObjects() // constructor.
        {
            DataSource.Initialize();
        }
        /// <summary>
        /// construct a new base station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="chargeSlots"></param>
        /// <param name="longtitude"></param>
        /// <param name="latitude"></param>
        public void ConstructBaseStation(int id, string name, int chargeSlots, double longtitude, double latitude) // construct a new base station
        {
            #region//construct a new base station
            BaseStation newBaseStation = new BaseStation
            {
                Id = id,
                Name = name,
                ChargeSlots = chargeSlots,
                Longtitude = DataSource.sexagesimal(longtitude, 'N'),
                Latitude = DataSource.sexagesimal(latitude, 'E')
            };
            DataSource.BaseStationArr[DataSource.Config.BaseStationArrIndex] = newBaseStation; // add to array
            DataSource.Config.BaseStationArrIndex++; // edvance index to next free cell
            #endregion
        }
        /// <summary>
        /// construct a new drone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="status"></param>
        /// <param name="battery"></param>
        public void ConstructDrone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery) // construct a new drone
        {
            #region//construct a new drone
            Drone newDrone = new Drone
            {
                Id = id,
                Model = model,
                MaxWeight = maxWeight,
                Status = status,
                Battery = battery,
            };
            DataSource.DroneArr[DataSource.Config.DroneArrIndex] = newDrone; // add to array
            DataSource.Config.DroneArrIndex++; // edvance index to next free cell
            #endregion
        }
        /// <summary>
        /// construct a new parcel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        /// <param name="request"></param>
        /// <param name="droneId"></param>
        /// <param name="schedual"></param>
        /// <param name="pickUp"></param>
        /// <param name="deliverd"></param>
        public void ConstructParcel(int id, int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime request, int droneId, DateTime schedual, DateTime pickUp, DateTime deliverd) // construct a new parcel
        {
            #region//construct a new parcel
            Parcel newParcel = new Parcel
            {
                Id = id,
                SenderId = senderId,
                TargetId = targetId,
                Weight = weight,
                Priority = priority,
                Requsted = request,
                DroneId = droneId,
                Scheduled = schedual,
                PickedUp = pickUp,
                Deliverd = deliverd
            };
            DataSource.ParcelArr[DataSource.Config.ParcelArrIndex] = newParcel; // add to array
            DataSource.Config.ParcelArrIndex++; // edvance index to next free cell
            DataSource.Config.ParcelId++; // grow runing number by 1
            #endregion
        }
        /// <summary>
        /// construct a new customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="longtitude"></param>
        /// <param name="latitude"></param>
        public void ConstructCustomer(int id, string name, string phone, double longtitude, double latitude) // construct a new customer
        {
            #region//construct a new customer
            Customer newCustomer = new Customer
            {
                Id = id,
                Name = name,
                Phone = phone,
                Longtitude = DataSource.sexagesimal(longtitude, 'N'),
                Latitude = DataSource.sexagesimal(latitude, 'E')
            };
            DataSource.CustomerArr[DataSource.Config.CustomerArrIndex] = newCustomer; // add to array
            DataSource.Config.CustomerArrIndex++; // edvance index to next free cell
            #endregion
        }
        /// <summary>
        /// construct a new drone charge
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void ConstructDroneCharge(int droneId, int stationId) // construct a new drone charge
        {
            #region//construct a new drone charge           
            for (int i = 0; i < DataSource.Config.DroneChargeArrSize; i++)
            {
                if (DataSource.DroneChargeArr[i].DroneId == -1 && DataSource.DroneChargeArr[i].StationId == -1)
                {
                    DataSource.DroneChargeArr[i].DroneId = droneId; // add to array id
                    DataSource.DroneChargeArr[i].StationId = stationId; // add to array id
                    break;
                }
            }   
            #endregion
        }
        /// <summary>
        /// returns index of next free cell in drone arr
        /// </summary>
        /// <returns>index of next free cell in drone arr</returns>
        public int GetDroneArrIndex() // returns index of next free cell in drone arr
        {
            return DataSource.Config.DroneArrIndex;
        }
        /// <summary>
        /// returns index of next free cell in base station arr
        /// </summary>
        /// <returns>index of next free cell in base station arr</returns>
        public int GetBaseStationArrIndex() // returns index of next free cell in base station arr
        {
            return DataSource.Config.BaseStationArrIndex;
        }
        /// <summary>
        /// returns index of next free cell in customer arr
        /// </summary>
        /// <returns>index of next free cell in customer arr</returns>
        public int GetCustomerArrIndex() // returns index of next free cell in customer arr
        {
            return DataSource.Config.CustomerArrIndex;
        }
        /// <summary>
        /// returns index of next free cell in parcel arr
        /// </summary>
        /// <returns>index of next free cell in parcel arr</returns>
        public int GetParcelArrIndex() // returns index of next free cell in parcel arr
        {
            return DataSource.Config.ParcelArrIndex;
        }
        /// <summary>
        /// find drone in array 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>index of drone in array</returns>
        public int FindDroneById(int id) // find drone in array 
        {
            #region//find drone in array
            for (int index = 0; index < DataSource.Config.DroneArrIndex; index++) // search drone by id
            {
                if (DataSource.DroneArr[index].Id == id)
                {
                    return index; // return index
                }
            }
            return -1; // if not found
            #endregion
        }
        /// <summary>
        /// find BaseStation in array 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>index of base station in array</returns>
        public int FindBaseStationById(int id) // find BaseStation in array 
        {
            #region//find BaseStation in array
            for (int index = 0; index < DataSource.Config.BaseStationArrIndex; index++) // search BaseStation by id
            {
                if (DataSource.BaseStationArr[index].Id == id)
                {
                    return index; // return index
                }
            }
            return -1; // if not found
            #endregion
        }
        /// <summary>
        /// find Customer in array
        /// </summary>
        /// <param name="id"></param>
        /// <returns>index of customer in array</returns>
        public int FindCustomerById(int id) // find Customer in array 
        {
            #region//find Customer in array
            for (int index = 0; index < DataSource.Config.CustomerArrIndex; index++) // searchCustomer by id
            {
                if (DataSource.CustomerArr[index].Id == id)
                {
                    return index; // return index
                }
            }
            return -1; // if not found
            #endregion
        }
        /// <summary>
        /// find Parcel in array 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>index of parcel in array</returns>
        public int FindParcelById(int id) // find Parcel in array 
        {
            #region//find Parcelin array
            for (int index = 0; index < DataSource.Config.ParcelArrIndex; index++) // Parcel by id
            {
                if (DataSource.ParcelArr[index].Id == id)
                {
                    return index; // return index
                }
            }
            return -1; // if not found
            #endregion
        }
        /// <summary>
        /// find drone in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>drone data</returns>
        public string ReturnDroneDataById(int id) // find drone in array and return it
        {
            #region//find drone in array and return it        
            return DataSource.DroneArr[FindDroneById(id)].ToString(); 
            #endregion
        }
        /// <summary>
        /// find base station in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns> base station data</returns>
        public string ReturnBaseStationDataById(int id) // find base station in array and return it
        {
            #region//find base station in array and return it
           return DataSource.BaseStationArr[FindBaseStationById(id)].ToString(); 
            #endregion
        }
        /// <summary>
        /// find customer in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns> customer data</returns>
        public string ReturnCustomerDataById(int id) // find customer in array and return it
        {
            #region//find customer in array and return it         
            return DataSource.CustomerArr[FindCustomerById(id)].ToString(); 
            #endregion
        }
        /// <summary>
        /// find parcel in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>parcel data</returns>
        public string ReturnParcelDataById(int id)// find parcel in array and return it
        {
            #region//find parcel in array and return it     
            return DataSource.ParcelArr[FindParcelById(id)].ToString(); 
            #endregion
        }
        /// <summary>
        ///  find drone in array and return it
        /// </summary>
        /// <param name="index"></param>
        /// <returns>index</returns>
        public string ReturnDroneDataByIndex(int index) // find drone in array and return it
        {
            #region//find drone in array and return it        
            return DataSource.DroneArr[index].ToString(); 
            #endregion
        }
        /// <summary>
        /// find base station in array and return it
        /// </summary>
        /// <param name="index"></param>
        /// <returns>index</returns>
        public string ReturnBaseStationDataByIndex(int index) // find base station in array and return it
        {
            #region//find base station in array and return it
            return DataSource.BaseStationArr[index].ToString(); 
            #endregion
        }
        /// <summary>
        /// find customer in array and return it
        /// </summary>
        /// <param name="index"></param>
        /// <returns>index</returns>
        public string ReturnCustomerDataByIndex(int index) // find customer in array and return it
        {
            #region//find customer in array and return it         
            return DataSource.CustomerArr[index].ToString(); 
            #endregion
        }
        /// <summary>
        /// find parcel in array and return it
        /// </summary>
        /// <param name="index"></param>
        /// <returns>index</returns>
        public string ReturnParcelDataByIndex(int index)// find parcel in array and return it
        {
            #region//find parcel in array and return it     
            return DataSource.ParcelArr[index].ToString(); 
            #endregion
        }
        /// <summary>
        /// search in spesific index in parcel array if drone id is equal 0
        /// </summary>
        /// <param name="index"></param>
        /// <returns>if equal 0 return true, else return false</returns>
        public bool CheckDroneIdInParcel(int i) // search in spesific index in parcel array if drone id is equal 0
        {
            #region//search in spesific index in parcel array if drone id is equal 0
            if (DataSource.ParcelArr[i].DroneId == 0)
            {
                return true;
            }
            return false;
            #endregion
        }
        /// <summary>
        /// search in spesific index in base station array if charge slots equals 0
        /// </summary>
        /// <param name="index"></param>
        /// <returns>if charge slots equals 0 return false, else return true</returns>
        public bool CheckChargeSlotsInBaseStation(int i) // search in spesific index in base station array if charge slots equals 0
        {
            #region//search in spesific index in base station array if charge slots equals 0
            if (DataSource.BaseStationArr[i].ChargeSlots != 0)
            {
                return true;
            }
            return false;
            #endregion
        }
        /// <summary>
        /// associate a drone to a parcel
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="parcleId"></param>
        public void AssociateDroneToParcel(int droneId, int parcleId) // associate a drone to a parcel
        {
            #region//associate a drone to a parcel
            DataSource.ParcelArr[FindParcelById(parcleId)].DroneId = droneId;
            DataSource.DroneArr[FindDroneById(droneId)].Status = DroneStatuses.Delivery;
            #endregion
        }
        /// <summary>
        /// update parcel pickup
        /// </summary>
        /// <param name="id"></param>
        public void UpdateParclePickup(int id) // update parcel pickup
        {
            #region//update parcel pickup           
             DataSource.ParcelArr[FindParcelById(id)].PickedUp = DateTime.Now; // currnt time  
            #endregion
        }
        /// <summary>
        /// update parcel delivery
        /// </summary>
        /// <param name="id"></param>
        public void UpdateParcleDelivery(int id) // update parcel delivery
        {
            #region//update parcel delivery
            DataSource.ParcelArr[FindParcelById(id)].Deliverd = DateTime.Now;  // currnt time
            DataSource.DroneArr[FindDroneById(DataSource.ParcelArr[FindParcelById(id)].DroneId)].Status = DroneStatuses.Available; // change drone status to avilable
            #endregion
        }
        /// <summary>
        /// update drones charging
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void UpdateDroneCharge(int droneId, int stationId) // update drones charging
        {
            #region//update drones charging
            ConstructDroneCharge(droneId, stationId); // call construct
            DataSource.DroneArr[FindDroneById(droneId)].Status = DroneStatuses.Maintenance; // update drone status
            DataSource.BaseStationArr[FindBaseStationById(stationId)].ChargeSlots -= 1;   // change number of free charge slots
            #endregion
        }
        /// <summary>
        /// releas drone from charging
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void ReleaseDroneCharge(int droneId, int stationId) // releas drone from charging
        {
            #region//releas drone from charging
            DataSource.DroneArr[FindDroneById(droneId)].Status = DroneStatuses.Available; // make drone status: Available
            DataSource.BaseStationArr[FindBaseStationById(stationId)].ChargeSlots++;
            for (int index = 0; index < DataSource.Config.DroneChargeArrSize; index++) // search drone by id in drone charge
            {
                if (DataSource.DroneChargeArr[index].DroneId == droneId)
                {
                    DataSource.DroneChargeArr[index].DroneId = -1; // set to availeble
                    DataSource.DroneChargeArr[index].StationId = -1; // set to availeble
                    break;
                }
            }
            #endregion
        }
    }
}
