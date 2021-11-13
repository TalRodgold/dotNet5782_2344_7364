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
            if (IfBaseStationExsists(id))
            {
                throw new DALException("error");// לתקן את כל הזריקות
            }
            BaseStation newBaseStation = new BaseStation
            {
                Id = id,
                Name = name,
                ChargeSlots = chargeSlots,
                Longtitude = longtitude,
                Latitude = latitude
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
            if (IfDroneExsists(id))
            {
                throw new DALException("error");// לתקן את כל הזריקות
            }
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
        public void ConstructParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime request, int droneId, DateTime schedual, DateTime pickUp, DateTime deliverd) // construct a new parcel
        {
            #region//construct a new parcel
            Parcel newParcel = new Parcel
            {
                Id = DataSource.Config.ParcelId++, // grow runing number by 1
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
            if (IfCustomerExsists(id))
            {
                throw new DALException("error");// לתקן את כל הזריקות
            }
            Customer newCustomer = new Customer
            {
                Id = id,
                Name = name,
                Phone = phone,
                Longtitude = longtitude,
                Latitude = latitude
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
            DroneCharge newDroneCharge = new DroneCharge();
            newDroneCharge.DroneId = droneId;
            newDroneCharge.StationId = stationId;
            DataSource.DroneChargeList.Add(newDroneCharge);
            #endregion
        }
        #endregion
        #region//all Retun by value functions

        /// <summary>
        /// find drone in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>drone data</returns>
        public string ReturnDroneDataById(int id) // find drone in list and return it as string
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
        public string ReturnBaseStationDataById(int id) // find base station in list and return it as string
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
        public string ReturnCustomerDataById(int id) // find customer in list and return it as string
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
        public string ReturnParcelDataById(int id)// find parcel in list and return it as string
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

            int i= DataSource.ParcelList.FindIndex(element => element.Id == parcleId);
            Parcel newParcel = DataSource.ParcelList.Find(element => element.Id == parcleId);
            newParcel.DroneId = droneId;
            DataSource.ParcelList[i] = newParcel;
            #endregion
        }
        /// <summary>
        /// update parcel pickup
        /// </summary>
        /// <param name="id"></param>
        public void UpdateParclePickup(int id) // update parcel pickup 
        {
            #region//update parcel pickup  
            int i= DataSource.ParcelList.FindIndex(element => element.Id == id);
            Parcel newParcel = DataSource.ParcelList.Find(element => element.Id == id);
            newParcel.PickedUp = DateTime.Now;
            DataSource.ParcelList[i] = newParcel;
           
            #endregion
        }
        /// <summary>
        /// update parcel delivery
        /// </summary>
        /// <param name="id"></param>
        public void UpdateParcleDelivery(int id) // update parcel delivery
        {
            #region//update parcel delivery
            int i = DataSource.ParcelList.FindIndex(element => element.Id == id);
            Parcel newParcel = DataSource.ParcelList.Find(element => element.Id == id);
            newParcel.Deliverd = DateTime.Now;
            DataSource.ParcelList[i] = newParcel;
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
            int i = DataSource.BaseStationList.FindIndex(element => element.Id == stationId);

            ConstructDroneCharge(droneId, stationId); // call construct
                                                      // DataSource.DroneArr[FindDroneById(droneId)].Status = DroneStatuses.Maintenance; // update drone status
            BaseStation newBaseStation = DataSource.BaseStationList.Find(element => element.Id == stationId);
            newBaseStation.ChargeSlots -= 1;
            DataSource.BaseStationList[i] = newBaseStation;
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
        #region//all ToString Functions
        /// <summary>
        ///  return long string of all BaseStations
        /// </summary>
        /// <param name="s"></param>
        /// <param name="b"></param>
        public string BaseStationListToString()//the function return long string of all BaseStation
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
        public string DroneListToString()//the function return long string of all Drones
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
        public string ParcelListToString()//the function return long string of all Parcels
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
        public string ParcelsNotAssociatedToString() //return all Drones that not Associated
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
        public int GetCountOfDroneList() { return DataSource.DroneList.Count(); } // returns size of drone list
        public int GetCountOfBaseStationList() { return DataSource.BaseStationList.Count(); } // returns size of base station list
        public int GetCountOfCustomerList() { return DataSource.CustomerList.Count(); } // returns size of customer list
        public int GetCountOfParcelList() { return DataSource.ParcelList.Count(); } // returns size of parcel list
        public bool IfDroneExsists(int id) { return DataSource.DroneList.Exists(element => element.Id == id); } // return true if id exisists in list of drones
        public bool IfBaseStationExsists(int id) { return DataSource.BaseStationList.Exists(element => element.Id == id); } // return  true if id exisists in list of base stations
        public bool IfCustomerExsists(int id) { return DataSource.CustomerList.Exists(element => element.Id == id); } // return  true if id exisists in list of customers
        public bool IfParcelExsists(int id) { return DataSource.ParcelList.Exists(element => element.Id == id); } // return  true if id exisists inlist of parcels
        public Customer GetCustomer(int id) {if(! IfCustomerExsists(id)) throw "?"; return DataSource.CustomerList.Find(element => element.Id == id);  } // find a customer by id and return all his data as customer class
        public Parcel GetParcel(int id=0,Predicate<Parcel> predicate){if ((!IfParcelExsists(id)) & (id != 0))  throw "?";  return DataSource.ParcelList.Find(predicate); } // find a Parcel by id and return all his data as Parcel class
        public Drone GetDrone(int id) { if (!IfDroneExsists(id)) throw "?"; return DataSource.DroneList.Find(element => element.Id == id); } // find a Drone by id and return all his data as Drone class
        public BaseStation GetBaseStation(int id) { if (!IfBaseStationExsists(id)) throw "?"; return DataSource.BaseStationList.Find(element => element.Id == id); } // find a BaseStation by id and return all his data as BaseStation class

        //public double[] Electricity();//צריך לממש והיא תחזיר  5 ערכים פנוי קל בינוני כבד וקצב תעינה


    }
}

