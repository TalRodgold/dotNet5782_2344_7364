using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using IDAL;

namespace DalObjects // להתייחס לממשק
{
    /// <summary>
    /// Holds functions that allow to acsses data
    /// </summary>
    public class DalObjects : IDal
    {
        /// <summary>
        /// contains functions that allow to acsses and change the data in DataSource
        /// </summary>
        public DalObjects() // constructor.
        {
            DataSource.Initialize();
        }
        #region//all construct functions
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
            DataSource.BaseStationList.Add(newBaseStation); // add to list
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
        public void ConstructDrone(int id, string model, WeightCategories maxWeight) // construct a new drone
        {
            #region//construct a new drone
            Drone newDrone = new Drone
            {
                Id = id,
                Model = model,
                MaxWeight = maxWeight,
                //Status = status,
                //Battery = battery,
            };
            DataSource.DroneList.Add(newDrone); // add to list
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
            DataSource.ParcelList.Add(newParcel); //  add to list
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
            DataSource.CustomerList.Add(newCustomer); // add to list
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
            DroneCharge new_DroneCharge = new DroneCharge();
            new_DroneCharge.DroneId = droneId;
            new_DroneCharge.StationId = stationId;
            #endregion
        }
        #endregion
        #region//all Retun by value functions

        /// <summary>
        /// find drone in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>drone data</returns>
        public string ReturnDroneDataById(int id) // find drone in list and return it
        {
            #region//find drone in array and return it        
            return DataSource.DroneList.Find(x => x.Id == id).ToString();
            #endregion
        }
        /// <summary>
        /// find base station in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns> base station data</returns>
        public string ReturnBaseStationDataById(int id) // find base station in list and return it
        {
            #region//find base station in array and return it
            return DataSource.BaseStationList.Find(x => x.Id == id).ToString();
            #endregion
        }
        /// <summary>
        /// find customer in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns> customer data</returns>
        public string ReturnCustomerDataById(int id) // find customer in list and return it
        {
            #region//find customer in array and return it         
            return DataSource.CustomerList.Find(x => x.Id == id).ToString();
            #endregion
        }
        /// <summary>
        /// find parcel in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>parcel data</returns>
        public string ReturnParcelDataById(int id)// find parcel in list and return it
        {
            #region//find parcel in array and return it     
            return DataSource.ParcelList.Find(x => x.Id == id).ToString();
            #endregion
        }
        #endregion

        /// <summary>
        /// associate a drone to a parcel
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="parcleId"></param>
        public void AssociateDroneToParcel(int droneId, int parcleId) // associate a drone to a parcel 
        {
            #region//associate a drone to a parcel

            //int i= DataSource.ParcelList.FindIndex(x => x.Id == parcleId);
            Parcel newParcel = DataSource.ParcelList.Find(x => x.Id == parcleId);
            newParcel.DroneId = droneId;
            DataSource.ParcelList.RemoveAt(DataSource.ParcelList.FindIndex(x => x.Id == parcleId));
            DataSource.ParcelList.Add(newParcel);
            #endregion
        }
        /// <summary>
        /// update parcel pickup
        /// </summary>
        /// <param name="id"></param>
        public void UpdateParclePickup(int id) // update parcel pickup CHAC!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!11
        {
            #region//update parcel pickup  
            Parcel newParcel = DataSource.ParcelList.Find(x => x.Id == id);
            newParcel.PickedUp = DateTime.Now;
            DataSource.ParcelList.RemoveAt(DataSource.ParcelList.FindIndex(x => x.Id == id));
            DataSource.ParcelList.Add(newParcel);
            #endregion
        }
        /// <summary>
        /// update parcel delivery
        /// </summary>
        /// <param name="id"></param>
        public void UpdateParcleDelivery(int id) // update parcel delivery
        {
            #region//update parcel delivery
            Parcel newParcel = DataSource.ParcelList.Find(x => x.Id == id);
            newParcel.Deliverd = DateTime.Now;
            DataSource.ParcelList.RemoveAt(DataSource.ParcelList.FindIndex(x => x.Id == id));
            DataSource.ParcelList.Add(newParcel);
            // DataSource.DroneArr[FindDroneById(DataSource.ParcelArr[FindParcelById(id)].DroneId)].Status = DroneStatuses.Available; // change drone status to avilable
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
                                                      // DataSource.DroneArr[FindDroneById(droneId)].Status = DroneStatuses.Maintenance; // update drone status
            BaseStation newBaseStation = DataSource.BaseStationList.Find(x => x.Id == stationId);
            newBaseStation.ChargeSlots -= 1;
            DataSource.BaseStationList.RemoveAt(DataSource.BaseStationList.FindIndex(x => x.Id == stationId));
            DataSource.BaseStationList.Add(newBaseStation);
            // change number of free charge slots
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
            // DataSource.DroneArr[FindDroneById(droneId)].Status = DroneStatuses.Available; // make drone status: Available
            BaseStation newBaseStation = DataSource.BaseStationList.Find(x => x.Id == stationId);
            newBaseStation.ChargeSlots += 1;
            DataSource.BaseStationList.RemoveAt(DataSource.BaseStationList.FindIndex(x => x.Id == stationId));
            DataSource.BaseStationList.Add(newBaseStation);
            DataSource.DroneChargeList.RemoveAt(DataSource.DroneChargeList.FindIndex(x => x.DroneId == droneId));
            #endregion
        } 
        public double[] Electricity();//צריך לממש והיא תחזיר  5 ערכים פנוי קל בינוני כבד וקצב תעינה
        #region//all ToString Functions
        /// <summary>
        ///  return long string of all BaseStations
        /// </summary>
        /// <param name="s"></param>
        /// <param name="b"></param>
        public string BaseStationLisToString()//the function return long string of all BaseStation
        {
            #region//concatent strings that contain BaseStation data and return long string
            string s = "";
            foreach (BaseStation b in DataSource.BaseStationList)
            {
                s += b.ToString();
            }
            return s;
            #endregion
        }
        /// <summary>
        ///  return long string of all Drones
        /// </summary>
        /// <param name="s"></param>
        /// <param name="d"></param>
        public string DroneLisToString()//the function return long string of all Drones
        {
            #region//concatent strings that contain Drone data and return long string
            string s = "";
            foreach (Drone d in DataSource.DroneList)
            {
                s += d.ToString();
            }
            return s;
            #endregion
        }
        /// <summary>
        ///  return long string of all Customers
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        public string CustomerListToString()//the function return long string of all Customers
        {
            #region//concatent strings that contain Customer data and return long string 
            string s = "";
            foreach (Customer c in DataSource.CustomerList)
            {
                s += c.ToString();
            }
            return s;
            #endregion
        }
        /// <summary>
        ///  return long string of all Parcels
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        public string ParcelLisToString()//the function return long string of all Parcels
        {
            #region//concatent strings that contain Parcel data and return long string
            string s = "";
            foreach (Parcel p in DataSource.ParcelList)
            {
                s += p.ToString();
            }
            return s;
            #endregion
        }
        public string BaseStationfreeToString()//return all free BaseStation
        {
            bool flag=true;
            string s = "";
            foreach(BaseStation b in DataSource.BaseStationList)
            {
                if (b.ChargeSlots== 0) // if free
                {
                    s += b.ToString();
                    flag= false;
                }
            }
            if (flag) // if all base station charging slots are full
            {
                s+=" All base station charging slots are full ";
            }
            return s;
        }
        public string ParcelsNotAssociatedToString()
        {
            #region//return all Drones that not Associated
            string s = "";
            bool flag = true;
            foreach (Parcel p in DataSource.ParcelList)// for all parcels in array
            {
                if(p.DroneId==0) //search in spesific index in parcel list if drone id is equal 0
                {
                    s += p.ToString();
                    flag = false;
                }
            }
            if (flag) // if all parcels have been associated to a drone
            {
                s += " All parcels have been associated to a drone ";
            }
            return s;
            #endregion
        }
        #endregion
        //return list of parcels who are not associated to a drone
        public int GetCountOfDroneList() { return DataSource.DroneList.Count(); }
        public int GetCountOfBaseStationList() { return DataSource.BaseStationList.Count(); }
        public int GetCountOfCustomerList() { return DataSource.CustomerList.Count(); }
        public int GetCountOfParcelList() { return DataSource.ParcelList.Count(); }
    }
}
///// <summary>
///// search in spesific index in parcel list if drone id is equal 0
///// </summary>
///// <param name="index"></param>
///// <returns>if equal 0 return true, else return false</returns>
//public bool CheckDroneIdInParcel(int i) // search in spesific index in parcel list if drone id is equal 0
//{
//    #region//search in spesific index in parcel list if drone id is equal 0
//    if ((DataSource.ParcelList.ElementAt(i).Id == 0))
//    {
//        return true;
//    }
//    return false;
//    #endregion
//}
///// <summary>
///// search in spesific index in base station list if charge slots equals 0
///// </summary>
///// <param name="index"></param>
///// <returns>if charge slots equals 0 return false, else return true</returns>
//public bool CheckChargeSlotsInBaseStation(int i) // search in spesific index in base station list if charge slots equals 0
//{
//    #region//search in spesific index in base station array if charge slots equals 0
//    if ((DataSource.BaseStationList.ElementAt(i).Id == 0))
//    {
//        return true;
//    }
//    return false;
//    #endregion
//}
