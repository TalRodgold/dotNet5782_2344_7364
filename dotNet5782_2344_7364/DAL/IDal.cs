using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    /// <summary>
    /// Interface with all the functions of DalObjects
    /// </summary>
    public interface IDal
    {
        void ConstructBaseStation(int? id, string name, int chargeSlots, double longtitude, double latitude); // construct a new base station
        void ConstructDrone(int? id, string model, WeightCategories maxWeight); // construct a new drone
        int? ConstructParcel(int? senderId, int? targetId, WeightCategories weight, Priorities priority, DateTime? request, int? droneId, DateTime? schedual, DateTime? pickUp, DateTime? deliverd);// construct a new parcel
        void ConstructCustomer(int? id, string name, string phone, double longtitude, double latitude); // construct a new customer
        void ConstructDroneCharge(int? droneId, int? stationId,DateTime? cuerrentTime); // construct a new drone charge
        string ReturnDroneDataById(int? id); // find drone in list and return it as string
        string ReturnBaseStationDataById(int? id); // find base station in list and return it as string
        string ReturnCustomerDataById(int? id); // find customer in list and return it as string
        string ReturnParcelDataById(int? id); // find parcel in list and return it as string
        void AssociateDroneToParcel(int? droneId, int? parcleId); // associate a drone to a parcel 
        void UpdateParclePickup(int? id); // update parcel pickup 
        void UpdateParcleDelivery(int? id); // update parcel delivery
        void UpdateDroneCharge(int? droneId, int? stationId,DateTime? currentTime); // update drones charging
        void UpdateDroneModel(int? id, string newModel); // update drone model
        void UpdateBaseStationName(int? id, string name); // update base stations name
        void UpdateChargingSlotsNumber(int? id, int numberOfChargingSlots);
        void UpdateCustomerName(int? id, string name); // update customers name
        void UpdateCustomerPhone(int? id, string phone); //update customers phone number
        void UpdateBaseStationNumOfFreeDroneCharges(int? id, int newnum); // update base stations number of free charging slots
        void ReleaseDroneCharge(int? droneId, int? stationId); // releas drone from charging 
        string BaseStationListToString();//the function return long string of all BaseStation
        string DroneListToString();//the function return long string of all Drones
        string CustomerListToString();//the function return long string of all Customers
        string ParcelListToString();//the function return long string of all Parcels
        string BaseStationfreeToString();//return all free BaseStation
        string ParcelsNotAssociatedToString(); //return all Drones that not Associated
        int GetCountOfDroneList(); // returns size of drone list
        int GetCountOfBaseStationList(); // returns size of base station list
        int GetCountOfCustomerList(); // returns size of customer list
        int GetCountOfParcelList(); // returns size of parcel list
        bool IfDroneExsists(int? id);  // return true if id exisists in list of drones
        bool IfBaseStationExsists(int? id);  // return  true if id exisists in list of base stations
        bool IfCustomerExsists(int? id); // return  true if id exisists in list of customers
        bool IfParcelExsists(int? id);  // return  true if id exisists inlist of parcels
        Customer GetCustomer(int? id); // find a customer by id and return all his data as customer class
        Parcel GetParcel(int? id,Predicate<Parcel> predicate = null); // find a Parcel by id and return all his data as Parcel class
        Drone GetDrone(int? id); // find a Drone by id and return all his data as Drone class
        BaseStation GetBaseStation(int? id); // find a BaseStation by id and return all his data as BaseStation class
        BaseStation getBaseStationByDroneId(int? id); // find a Drone by id and return all his data as Drone class 
        DroneCharge GetDroneCharge(int? id = null, Predicate<DroneCharge> predicate = null); // find a DroneCharge by id and return all his data as DroneCharge class
        IEnumerable<DroneCharge> GetListOfDroneCharge(Predicate<DroneCharge> predicate = null); // Get an IEnumerable of all base drone charge
        IEnumerable<Customer> GetListOfCustomer(Predicate<Customer> predicate=null); // Get an IEnumerable of all customers
        IEnumerable<Parcel> GetListOfParcel(Predicate<Parcel> predicate=null); // Get an IEnumerable of all parcels
        IEnumerable<Drone> GetListOfDrone(Predicate<Drone> predicate=null); // Get an IEnumerable of all drones
        IEnumerable<BaseStation> GetListOfBaseStation(Predicate<BaseStation> predicate=null); // Get an IEnumerable of all base stations
        double[] Electricity(); //return an array of electricity data
    }
}
