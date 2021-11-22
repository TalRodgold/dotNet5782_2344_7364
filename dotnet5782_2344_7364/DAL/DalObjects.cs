using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using IDAL;

namespace DalObjects 
{
    /// <summary>
    /// This is the data layer
    /// </summary>
    public class DalObjects : IDal
    {
        /// <summary>
        /// contains functions that allow to acsses and change the data in DataSource and pass it on to BL
        /// </summary>
        public DalObjects() // constructor.
        {
            DataSource.DroneList = new List<Drone>(); 
            DataSource.BaseStationList = new List<BaseStation>(); 
            DataSource.CustomerList = new List<Customer>(); 
            DataSource.ParcelList = new List<Parcel>(); 
            DataSource.DroneChargeList = new List<DroneCharge>();
            DataSource.Initialize();
        }
        #region//All the construct functions

        #region//construct a new base station
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
            
            if (IfBaseStationExsists(id)) // if id already exsists
            {
                throw new IdAlreadyExsistsExceptions("base station", id);// throw exception
            }
            BaseStation newBaseStation = new BaseStation // construct
            {
                Id = id,
                Name = name,
                ChargeSlots = chargeSlots,
                Longtitude = longtitude,
                Latitude = latitude
            };
            DataSource.BaseStationList.Add(newBaseStation); // add to list 
        }
        #endregion
        #region//construct a new drone
        /// <summary>
        /// construct a new drone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        public void ConstructDrone(int id, string model, WeightCategories maxWeight) // construct a new drone
        {  
            if (IfDroneExsists(id)) // if id already exsists
            {
                throw new IdAlreadyExsistsExceptions("drone", id); // throw exception
            }
            Drone newDrone = new Drone // construct
            {
                Id = id,
                Model = model,
                MaxWeight = maxWeight,
                //Status = status,
                //Battery = battery,
            };
            DataSource.DroneList.Add(newDrone); // add to list
        }
        #endregion
        #region//construct a new parcel
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
        public int ConstructParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime request, int droneId, DateTime schedual, DateTime pickUp, DateTime deliverd) // construct a new parcel
        {
            if (senderId == targetId)
            {
                throw new SameIdException("senders ID and recivers ID ", senderId);
            }
            Parcel newParcel = new Parcel
            {
                Id = DataSource.Config.ParcelId++, // grow runing number by 1
                SenderId = senderId,
                ReciverId = targetId,
                Weight = weight,
                Priority = priority,
                CreatingTime = request,
                DroneId = droneId,
                AssociatedTime = schedual,
                PickedUp = pickUp,
                Deliverd = deliverd
            };
            DataSource.ParcelList.Add(newParcel); //  add to list
            return newParcel.Id;  
        }
        #endregion
        #region//construct a new customer
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
            if (IfCustomerExsists(id)) // if id already exsists
            {
                throw new IdAlreadyExsistsExceptions("customer", id); // throw exception
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
        }
        #endregion
        #region//construct a new drone charge 
        /// <summary>
        /// construct a new drone charge
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void ConstructDroneCharge(int droneId, int stationId) // construct a new drone charge
        {      
            DroneCharge newDroneCharge = new DroneCharge();
            newDroneCharge.DroneId = droneId;
            newDroneCharge.StationId = stationId;
            DataSource.DroneChargeList.Add(newDroneCharge);       
        }
        #endregion
        #endregion

        #region//All the retun by ID functions

        #region//find drone in array and return it  
        /// <summary>
        /// find drone in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>drone data</returns>
        public string ReturnDroneDataById(int id) // find drone in list and return it as string
        {
                  
            return DataSource.DroneList.Find(x => x.Id == id).ToString();
        }
        #endregion
        #region//find base station in array and return it
        /// <summary>
        /// find base station in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns> base station data</returns>
        public string ReturnBaseStationDataById(int id) // find base station in list and return it as string
        {
            return DataSource.BaseStationList.Find(x => x.Id == id).ToString();
        }
        #endregion
        #region//find customer in array and return it  
        /// <summary>
        /// find customer in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns> customer data</returns>
        public string ReturnCustomerDataById(int id) // find customer in list and return it as string
        {       
            return DataSource.CustomerList.Find(x => x.Id == id).ToString();
        }
        #endregion
        #region//find parcel in array and return it  
        /// <summary>
        /// find parcel in array and return it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>parcel data</returns>
        public string ReturnParcelDataById(int id)// find parcel in list and return it as string
        {   
            return DataSource.ParcelList.Find(x => x.Id == id).ToString();
        }
        #endregion
        #endregion

        #region// All the update functions

        #region//update parcel pickup  
        /// <summary>
        /// update parcel pickup
        /// </summary>
        /// <param name="id"></param>
        public void UpdateParclePickup(int id) // update parcel pickup 
        {
            if (!IfParcelExsists(id)) // if id dosent exsist
            {
                throw new IdNotExsistException("parcel", id); // throw exception
            }
            int i = DataSource.ParcelList.FindIndex(element => element.Id == id);
            Parcel newParcel = DataSource.ParcelList.Find(element => element.Id == id);
            newParcel.PickedUp = DateTime.Now;
            DataSource.ParcelList[i] = newParcel;
        }
        #endregion
        #region//update parcel delivery
        /// <summary>
        /// update parcel delivery
        /// </summary>
        /// <param name="id"></param>
        public void UpdateParcleDelivery(int id) // update parcel delivery
        {
            if (!IfParcelExsists(id)) // if id dosent exsist
            {
                throw new IdNotExsistException("parcel", id); // throw exception
            }
            int i = DataSource.ParcelList.FindIndex(element => element.Id == id);
            Parcel newParcel = DataSource.ParcelList.Find(element => element.Id == id);
            newParcel.Deliverd = DateTime.Now;
            DataSource.ParcelList[i] = newParcel;
        }
        #endregion
        #region//update drones charging
        /// <summary>
        /// update drones charging
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void UpdateDroneCharge(int droneId, int stationId) // update drones charging
        {
            if (!IfDroneExsists(droneId)) // if id dosent exsist
            {
                throw new IdNotExsistException("drone", droneId); // throw exception
            }

            if (!IfBaseStationExsists(stationId)) // if id dosent exsist
            {
                throw new IdNotExsistException("base station", stationId); // throw exception
            }
            int i = DataSource.BaseStationList.FindIndex(element => element.Id == stationId);
            ConstructDroneCharge(droneId, stationId); // call construct
            BaseStation newBaseStation = DataSource.BaseStationList.Find(element => element.Id == stationId);
            newBaseStation.ChargeSlots -= 1; // change number of free charge slots
            DataSource.BaseStationList[i] = newBaseStation;
        }
        #endregion
        #region// update drone model
        /// <summary>
        /// update drones model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newModel"></param>
        public void UpdateDroneModel(int id, string newModel) // update drones model
        {
            if(!IfDroneExsists(id)) // if id dosent exsist
            {
                throw new IdNotExsistException("drone", id); // throw exception
            }
            Drone newDrone = DataSource.DroneList.Find(element => element.Id == id);
            newDrone.Model = newModel;
            int index = DataSource.DroneList.FindIndex(element => element.Id == id);
            DataSource.DroneList[index] = newDrone;
        }
        #endregion
        #region// update base stations name
        /// <summary>
        /// update base stations name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void UpdateBaseStationName(int id, string name) // update base stations name
        {
            if (!IfBaseStationExsists(id)) // if id dosent exsist
            {
                throw new IdNotExsistException("base station", id); // throw exception
            }
            BaseStation newBaseStation = DataSource.BaseStationList.Find(element => element.Id == id);
            newBaseStation.Name = name;
            int index = DataSource.BaseStationList.FindIndex(element => element.Id == id);
            DataSource.BaseStationList[index] = newBaseStation;
        }
        #endregion
        #region// update base stations number of free charging slots
        /// <summary>
        /// update base stations number of free charging slots
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newnum"></param>
        public void UpdateBaseStationNumOfFreeDroneCharges(int id, int newnum) // update base stations number of free charging slots
        {
            if (!IfBaseStationExsists(id)) // if id dosent exsist
            {
                throw new IdNotExsistException("base station", id); // throw exception
            }
            BaseStation newBaseStation = DataSource.BaseStationList.Find(element => element.Id == id);
            newBaseStation.ChargeSlots = newnum;
            int index = DataSource.BaseStationList.FindIndex(element => element.Id == id);
            DataSource.BaseStationList[index] = newBaseStation;
        }
        #endregion
        #region// update number of charging slots
        /// <summary>
        /// update number of charging slots
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfChargingSlots"></param>
        public void UpdateChargingSlotsNumber(int id, int numberOfChargingSlots) // update number of charging slots
        {
            if (!IfBaseStationExsists(id)) // if id dosent exsist
            {
                throw new IdNotExsistException("base station", id); // throw exception
            }
            if (GetListOfDroneCharge(element => element.StationId == id).ToList().Count() > numberOfChargingSlots) // if the new number is smaller than the number of drones charging currently
            {
                throw new SizeProblemException("bigger", GetListOfDroneCharge(element => element.StationId == id).ToList().Count()); // throw exception
            }
            BaseStation newBaseStation = DataSource.BaseStationList.Find(element => element.Id == id);
            newBaseStation.ChargeSlots = numberOfChargingSlots;
            int index = DataSource.BaseStationList.FindIndex(element => element.Id == id);
            DataSource.BaseStationList[index] = newBaseStation;
        }
        #endregion
        #region// update customers name
        /// <summary>
        /// update customers name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void UpdateCustomerName(int id, string name) // update customers name
        {
            if (!IfCustomerExsists(id)) // if id dosent exsist
            {
                throw new IdNotExsistException("customer", id); // throw exception
            }
            Customer newCustomer = DataSource.CustomerList.Find(element => element.Id == id);
            newCustomer.Name = name;
            int index = DataSource.CustomerList.FindIndex(element => element.Id == id);
            DataSource.CustomerList[index] = newCustomer;
        }
        #endregion
        #region//update customers phone number
        /// <summary>
        /// update customers phone number
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phone"></param>
        public void UpdateCustomerPhone(int id, string phone) // update customers phone number
        {
            if (!IfCustomerExsists(id)) // if id dosent exsist
            {
                throw new IdNotExsistException("customer", id); // throw exception
            }
            Customer newCustomer = DataSource.CustomerList.Find(element => element.Id == id);
            newCustomer.Phone = phone;
            int index = DataSource.CustomerList.FindIndex(element => element.Id == id);
            DataSource.CustomerList[index] = newCustomer;
        }
        #endregion
        #region//releas drone from charging
        /// <summary>
        /// releas drone from charging
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void ReleaseDroneCharge(int droneId, int stationId) // releas drone from charging
        {   
            if (!IfDroneExsists(droneId)) // if id dosent exsist
            {
                throw new IdNotExsistException("drone", droneId); // throw exception
            }
            if (!IfBaseStationExsists(stationId)) // if id dosent exsist
            {
                throw new IdNotExsistException("base station", stationId); // throw exception
            }
            BaseStation newBaseStation = DataSource.BaseStationList.Find(x => x.Id == stationId);
            int index = DataSource.BaseStationList.FindIndex(x => x.Id == stationId);
            newBaseStation.ChargeSlots += 1;
            DataSource.BaseStationList[index] = newBaseStation;
            DataSource.DroneChargeList.RemoveAt(DataSource.DroneChargeList.FindIndex(x => x.DroneId == droneId));
        }
        #endregion
        #region//associate a drone to a parcel
        /// <summary>
        /// associate a drone to a parcel
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="parcleId"></param>
        public void AssociateDroneToParcel(int droneId, int parcleId) // associate a drone to a parcel 
        {
            if (!IfDroneExsists(droneId)) // if id dosent exsist
            {
                throw new IdNotExsistException("drone", droneId); // throw exception
            }

            if (!IfParcelExsists(parcleId)) // if id dosent exsist
            {
                throw new IdNotExsistException("parcel", parcleId); // throw exception
            }
            int i = DataSource.ParcelList.FindIndex(element => element.Id == parcleId);
            Parcel newParcel = DataSource.ParcelList.Find(element => element.Id == parcleId);
            newParcel.DroneId = droneId;
            newParcel.CreatingTime = DateTime.Now;
            DataSource.ParcelList[i] = newParcel;
        }
        #endregion
        #endregion

        #region//All the ToString as list functions

        #region//concatent strings that contain BaseStation data and return long string
        /// <summary>
        /// concatent strings that contain BaseStation data and return long string
        /// </summary>
        /// <returns></returns>
        public string BaseStationListToString()//the function return long string of all BaseStation
        {
            string s = "";
            foreach (BaseStation b in DataSource.BaseStationList)
            {
                s += b.ToString();
            }
            return s;
        }
        #endregion
        #region//concatent strings that contain Drone data and return long string
        /// <summary>
        /// the function return long string of all Drones
        /// </summary>
        /// <returns></returns>
        public string DroneListToString()//the function return long string of all Drones
        {
            string s = "";
            foreach (Drone d in DataSource.DroneList)
            {
                s += d.ToString();
            }
            return s;
        }
        #endregion
        #region//concatent strings that contain Customer data and return long string 
        /// <summary>
        /// the function return long string of all Customers
        /// </summary>
        /// <returns></returns>
        public string CustomerListToString()//the function return long string of all Customers
        {
            string s = "";
            foreach (Customer c in DataSource.CustomerList)
            {
                s += c.ToString();
            }
            return s;
        }
        #endregion
        #region//concatent strings that contain Parcel data and return long string
        /// <summary>
        /// the function return long string of all Parcels
        /// </summary>
        /// <returns></returns>
        public string ParcelListToString()//the function return long string of all Parcels
        {
            string s = "";
            foreach (Parcel p in DataSource.ParcelList)
            {
                s += p.ToString();
            }
            return s;
        }
        #endregion
        #region//return all free BaseStation
        /// <summary>
        /// return all free BaseStation
        /// </summary>
        /// <returns></returns>
        public string BaseStationfreeToString()//return all free BaseStation
        {
            bool flag = true;
            string s = "";
            foreach (BaseStation b in DataSource.BaseStationList)
            {
                if (b.ChargeSlots == 0) // if free
                {
                    s += b.ToString();
                    flag = false;
                }
            }
            if (flag) // if all base station charging slots are full
            {
                s += " All base station charging slots are full ";
            }
            return s;
        }
        #endregion
        #region//return all Drones that not Associated
        /// <summary>
        /// return all Drones that not Associated
        /// </summary>
        /// <returns></returns>
        public string ParcelsNotAssociatedToString() //return all Drones that not Associated
        {
            string s = "";
            bool flag = true;
            foreach (Parcel p in DataSource.ParcelList)// for all parcels in array
            {
                if (p.DroneId == 0) //search in spesific index in parcel list if drone id is equal 0
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
        }
        #endregion
        #endregion

        #region//All the exist functions
        /// <summary>
        /// return true if id exisists in list of drones
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IfDroneExsists(int id) { return DataSource.DroneList.Exists(element => element.Id == id); } // return true if id exisists in list of drones
        /// <summary>
        /// return  true if id exisists in list of base stations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IfBaseStationExsists(int id) { return DataSource.BaseStationList.Exists(element => element.Id == id); } // return  true if id exisists in list of base stations
        /// <summary>
        /// return  true if id exisists in list of customers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IfCustomerExsists(int id) { return DataSource.CustomerList.Exists(element => element.Id == id); } // return  true if id exisists in list of customers
        /// <summary>
        /// return  true if id exisists inlist of parcels
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IfParcelExsists(int id) { return DataSource.ParcelList.Exists(element => element.Id == id); } // return  true if id exisists inlist of parcels
        #endregion

        #region//All the get functions
        #region//find a customer by id and return all his data as customer class
        /// <summary>
        /// find a customer by id and return all his data as customer class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomer(int id) // find a customer by id and return all his data as customer class
        {
            if (!IfCustomerExsists(id)) // if id doesnt exsist
            {
                throw new IdNotExsistException("customer", id); // throw exception
            }
            return DataSource.CustomerList.Find(element => element.Id == id);
        }
        #endregion
        #region// find a Parcel by id and return all his data as Parcel class
        /// <summary>
        /// find a Parcel by id and return all his data as Parcel class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="predicate"></param>    
        /// <returns></returns>
        public Parcel GetParcel(int id, Predicate<Parcel> predicate = null) // find a Parcel by id and return all his data as Parcel class
        {
            if ((!IfParcelExsists(id)) && (id != 0)) // if id doesnt exsist
            {
                throw new IdNotExsistException("parcel", id); // throw exception
            }
            if (predicate==null)
            {
                return DataSource.ParcelList.Find(element => element.Id == id);
            }
            return DataSource.ParcelList.Find(predicate);
        }
        #endregion
        #region// find a Drone by id and return all his data as Drone class
        /// <summary>
        /// find a Drone by id and return all his data as Drone class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Drone GetDrone(int id) // find a Drone by id and return all his data as Drone class
        {
            if (!IfDroneExsists(id)) // if id doesnt exsist
            {
                throw new IdNotExsistException("drone", id); // throw exception
            }
            return DataSource.DroneList.Find(element => element.Id == id);
        }
        #endregion
        #region // find a Drone by id and return all his data as Drone class 
        /// <summary>
        /// find a Drone by id and return all his data as Drone class 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseStation getBaseStationByDroneId(int id)  // find a Drone by id and return all his data as Drone class 
        {
            if (!IfDroneExsists(id)) // if id doesnt exsist
            {
                throw new IdNotExsistException("drone", id); // throw exception
            }
            return GetBaseStation(DataSource.DroneChargeList.Find(element => element.DroneId == id).StationId);
        }
        #endregion
        #region// find a BaseStation by id and return all his data as BaseStation class
        /// <summary>
        /// find a BaseStation by id and return all his data as BaseStation class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseStation GetBaseStation(int id) // find a BaseStation by id and return all his data as BaseStation class
        {
            if (!IfBaseStationExsists(id)) // if id doesnt exsist
            {
                throw new IdNotExsistException("base station", id); // throw exception
            }
            return DataSource.BaseStationList.Find(element => element.Id == id);
        }
        #endregion
        #region// find a DroneCharge by id and return all his data as DroneCharge class
        /// <summary>
        /// find a DroneCharge by id and return all his data as DroneCharge class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DroneCharge GetDroneCharge(int id = 0, Predicate<DroneCharge> predicate = null)  // find a DroneCharge by id and return all his data as DroneCharge class
        {
            if (predicate == null)
            {
                if (!IfDroneExsists(id)) // if id doesnt exsist
                {
                    throw new IdNotExsistException("drone charge", id); // throw exception
                }
                return DataSource.DroneChargeList.Find(element => element.DroneId == id);
            }
            return DataSource.DroneChargeList.Find(predicate);
        }
        #endregion
        #region//return an array of electricity data
        /// <summary>
        /// return an array of electricity data
        /// </summary>
        /// <returns></returns>
        public double[] Electricity() // return an array of electricity data
        {
            double[] electricity = new double[5];
            electricity[0] = DataSource.Config.ElectricityUseAvailiblity;
            electricity[1] = DataSource.Config.ElectricityUseLightWeight;
            electricity[2] = DataSource.Config.ElectricityUseMediumWeight;
            electricity[3] = DataSource.Config.ElectricityUseHeavyWeight;
            electricity[4] = DataSource.Config.DroneChargingPaste;
            return electricity;
        }
        #endregion
        /// <summary>
        /// returns size of drone list
        /// </summary>
        /// <returns></returns>
        public int GetCountOfDroneList() { return DataSource.DroneList.Count(); } // returns size of drone list
        /// <summary>
        /// returns size of base station list
        /// </summary>
        /// <returns></returns>
        public int GetCountOfBaseStationList() { return DataSource.BaseStationList.Count(); } // returns size of base station list
        /// <summary>
        /// returns size of customer list
        /// </summary>
        /// <returns></returns>
        public int GetCountOfCustomerList() { return DataSource.CustomerList.Count(); } // returns size of customer list
        /// <summary>
        /// // returns size of parcel list
        /// </summary>
        /// <returns></returns>
        public int GetCountOfParcelList() { return DataSource.ParcelList.Count(); } // returns size of parcel list
        
        /// <summary>
        /// Get an IEnumerable of all customers
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Customer> GetListOfCustomer(Predicate<Customer> predicate=null) { if(predicate==null) return DataSource.CustomerList.ToList(); return DataSource.CustomerList.FindAll(predicate).ToList(); } // Get an IEnumerable of all customers
        /// <summary>
        ///Get an IEnumerable of all parcels
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Parcel> GetListOfParcel(Predicate<Parcel> predicate=null) { if (predicate == null) return DataSource.ParcelList.ToList(); return DataSource.ParcelList.FindAll(predicate).ToList(); } // Get an IEnumerable of all parcels
        /// <summary>
        /// Get an IEnumerable of all drones
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Drone> GetListOfDrone(Predicate<Drone> predicate=null) { if (predicate == null) return DataSource.DroneList.ToList(); return DataSource.DroneList.FindAll(predicate).ToList(); } // Get an IEnumerable of all drones
        /// <summary>
        /// Get an IEnumerable of all base stations
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<BaseStation> GetListOfBaseStation(Predicate<BaseStation> predicate=null) { if (predicate == null) return DataSource.BaseStationList.ToList(); return DataSource.BaseStationList.FindAll(predicate).ToList(); } // Get an IEnumerable of all base stations
        /// <summary>
        /// Get an IEnumerable of all base drone charge
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DroneCharge> GetListOfDroneCharge(Predicate<DroneCharge> predicate=null) { if (predicate == null) return DataSource.DroneChargeList.ToList(); return DataSource.DroneChargeList.FindAll(predicate).ToList(); } // Get an IEnumerable of all base drone charge
        #endregion

    }
}