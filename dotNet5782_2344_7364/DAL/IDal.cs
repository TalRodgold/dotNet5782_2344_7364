using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Runtime.CompilerServices;
namespace DalApi
{
    /// <summary>
    /// Interface with all the functions of DalObjects
    /// </summary>
    public interface IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        void AddBaseStation(int? id, string name, int chargeSlots, double longtitude, double latitude); // construct a new base station
        [MethodImpl(MethodImplOptions.Synchronized)]
        void AddDrone(int? id, string model, WeightCategories maxWeight); // construct a new drone
        [MethodImpl(MethodImplOptions.Synchronized)]
        int? AddParcel(int? senderId, int? targetId, WeightCategories weight, Priorities priority, DateTime? request, int? droneId, DateTime? schedual, DateTime? pickUp, DateTime? deliverd);// construct a new parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        void AddCustomer(int? id, string name, string phone, double longtitude, double latitude); // construct a new customer
        [MethodImpl(MethodImplOptions.Synchronized)]
        void AddDroneCharge(int? droneId, int? stationId,DateTime? cuerrentTime); // construct a new drone charge
        [MethodImpl(MethodImplOptions.Synchronized)]
        void AssociateDroneToParcel(int? droneId, int? parcleId); // associate a drone to a parcel 
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateParclePickup(int? id); // update parcel pickup 
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateParcleDelivery(int? id); // update parcel delivery
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateDroneCharge(int? droneId, int? stationId,DateTime? currentTime); // update drones charging
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateDroneModel(int? id, string newModel); // update drone model
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateBaseStationName(int? id, string name); // update base stations name
        [MethodImpl(MethodImplOptions.Synchronized)]
        void DeleteBaseStation(int? id); // delete base station
        [MethodImpl(MethodImplOptions.Synchronized)]
        void DeleteParcel(int? id);//Delete Parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateChargingSlotsNumber(int? id, int numberOfChargingSlots); // update number of charging slots
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateCustomerName(int? id, string name); // update customers name
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateCustomerPhone(int? id, string phone); //update customers phone number
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateBaseStationNumOfFreeDroneCharges(int? id, int newnum); // update base stations number of free charging slots
        [MethodImpl(MethodImplOptions.Synchronized)]
        void ReleaseDroneCharge(int? droneId, int? stationId); // releas drone from charging 
        [MethodImpl(MethodImplOptions.Synchronized)]
        bool IfDroneExsists(int? id);  // return true if id exisists in list of drones
        [MethodImpl(MethodImplOptions.Synchronized)]
        bool IfBaseStationExsists(int? id);  // return  true if id exisists in list of base stations
        [MethodImpl(MethodImplOptions.Synchronized)]
        bool IfCustomerExsists(int? id); // return  true if id exisists in list of customers
        [MethodImpl(MethodImplOptions.Synchronized)]
        bool IfParcelExsists(int? id);  // return  true if id exisists inlist of parcels
        [MethodImpl(MethodImplOptions.Synchronized)]
        Customer GetCustomer(int? id); // find a customer by id and return all his data as customer class
        [MethodImpl(MethodImplOptions.Synchronized)]
        Parcel GetParcel(int? id,Predicate<Parcel> predicate = null); // find a Parcel by id and return all his data as Parcel class
        [MethodImpl(MethodImplOptions.Synchronized)]
        Drone GetDrone(int? id); // find a Drone by id and return all his data as Drone class
        [MethodImpl(MethodImplOptions.Synchronized)]
        BaseStation GetBaseStation(int? id); // find a BaseStation by id and return all his data as BaseStation class
        [MethodImpl(MethodImplOptions.Synchronized)]
        BaseStation getBaseStationByDroneId(int? id); // find a Drone by id and return all his data as Drone class 
        [MethodImpl(MethodImplOptions.Synchronized)]
        DroneCharge GetDroneCharge(int? id = null, Predicate<DroneCharge> predicate = null); // find a DroneCharge by id and return all his data as DroneCharge class
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<DroneCharge> GetListOfDroneCharge(Predicate<DroneCharge> predicate = null); // Get an IEnumerable of all base drone charge
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<Customer> GetListOfCustomer(Predicate<Customer> predicate=null); // Get an IEnumerable of all customers
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<Parcel> GetListOfParcel(Predicate<Parcel> predicate=null); // Get an IEnumerable of all parcels
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<Drone> GetListOfDrone(Predicate<Drone> predicate=null); // Get an IEnumerable of all drones
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<BaseStation> GetListOfBaseStation(Predicate<BaseStation> predicate=null); // Get an IEnumerable of all base stations
        [MethodImpl(MethodImplOptions.Synchronized)]
        double[] Electricity(); //return an array of electricity data
    }

}
