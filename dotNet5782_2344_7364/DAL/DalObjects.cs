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
        public int GetDroneArrIndex() // returns index of next free cell in drone arr
        {
            return DataSource.Config.DroneArrIndex;
        }
        public int GetBaseStationArrIndex() // returns index of next free cell in base station arr
        {
            return DataSource.Config.BaseStationArrIndex;
        } 
        public int GetCustomerArrIndex() // returns index of next free cell in customer arr
        {
            return DataSource.Config.CustomerArrIndex;
        }  
        public int GetParcelArrIndex() // returns index of next free cell in parcel arr
        {
            return DataSource.Config.ParcelArrIndex;
        }
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
        public string ReturnDroneDataById(int id) // find drone in array and return it
        {
            #region//find drone in array and return it        
            return DataSource.DroneArr[FindDroneById(id)].ToString(); 
            #endregion
        }
        public string ReturnBaseStationDataById(int id) // find base station in array and return it
        {
            #region//find base station in array and return it
           return DataSource.BaseStationArr[FindBaseStationById(id)].ToString(); 
            #endregion
        }
        public string ReturnCustomerDataById(int id) // find customer in array and return it
        {
            #region//find customer in array and return it         
            return DataSource.CustomerArr[FindCustomerById(id)].ToString(); 
            #endregion
        }
        public string ReturnParcelDataById(int id)// find parcel in array and return it
        {
            #region//find parcel in array and return it     
            return DataSource.ParcelArr[FindParcelById(id)].ToString(); 
            #endregion
        }


        public string ReturnDroneDataByIndex(int index) // find drone in array and return it
        {
            #region//find drone in array and return it        
            return DataSource.DroneArr[index].ToString(); 
            #endregion
        }
        public string ReturnBaseStationDataByIndex(int index) // find base station in array and return it
        {
            #region//find base station in array and return it
            return DataSource.BaseStationArr[index].ToString(); 
            #endregion
        }
        public string ReturnCustomerDataByIndex(int index) // find customer in array and return it
        {
            #region//find customer in array and return it         
            return DataSource.CustomerArr[index].ToString(); 
            #endregion
        }
        public string ReturnParcelDataByIndex(int index)// find parcel in array and return it
        {
            #region//find parcel in array and return it     
            return DataSource.ParcelArr[index].ToString(); 
            #endregion
        }

        public bool CheckDroneIdInParcel(int i)
        {
            if (DataSource.ParcelArr[i].DroneId == 0)
            {
                return true;
            }
            return false;
        }

        public bool CheckChargeSlotsInBaseStation(int i)
        {
            if (DataSource.BaseStationArr[i].ChargeSlots != 0)
            {
                return true;
            }
            return false;
        }



        public void AssociateDroneToParcel(int droneId, int parcleId) // associate a drone to a parcel
        {
            #region//associate a drone to a parcel
            DataSource.ParcelArr[FindParcelById(parcleId)].DroneId = droneId;
            DataSource.DroneArr[FindDroneById(droneId)].Status = DroneStatuses.Delivery;
            #endregion
        }
      
      
        public void UpdateParclePickup(int id) // update parcel pickup
        {
            #region//update parcel pickup           
             DataSource.ParcelArr[FindParcelById(id)].PickedUp = DateTime.Now; // currnt time  
            #endregion
        }
        public void UpdateParcleDelivery(int id) // update parcel delivery
        {
            #region//update parcel delivery
            DataSource.ParcelArr[FindParcelById(id)].Deliverd = DateTime.Now;  // currnt time
            DataSource.DroneArr[FindDroneById(DataSource.ParcelArr[FindParcelById(id)].DroneId)].Status = DroneStatuses.Available; // change drone status to avilable

            #endregion
        }
        public void UpdateDroneCharge(int droneId, int stationId) // update drones charging
        {
            #region//update drones charging
            ConstructDroneCharge(droneId, stationId); // call construct
            DataSource.DroneArr[FindDroneById(droneId)].Status = DroneStatuses.Maintenance; // update drone status
            DataSource.BaseStationArr[FindBaseStationById(stationId)].ChargeSlots -= 1;   // change number of free charge slots
            #endregion
        }
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
        public void StartProgram() { DataSource.Initialize(); } // call initialize to start program with random data
    }
}
