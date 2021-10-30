using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDal
{
    interface IDal
    {
        public void ConstructBaseStation(int id, string name, int chargeSlots, double longtitude, double latitude); // construct a new base station
        public void ConstructDrone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery); // construct a new drone
        public void ConstructParcel(int id, int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime request, int droneId, DateTime schedual, DateTime pickUp, DateTime deliverd);// construct a new parcel
        public void ConstructCustomer(int id, string name, string phone, double longtitude, double latitude); // construct a new customer
        public void ConstructDroneCharge(int droneId, int stationId); // construct a new drone charge
        public string ReturnDroneDataById(int id); // find drone in list and return it
        public string ReturnBaseStationDataById(int id); // find base station in list and return it
        public string ReturnCustomerDataById(int id); // find customer in list and return it
        public string ReturnParcelDataById(int id); // find parcel in list and return it
        public bool CheckDroneIdInParcel(int i); // search in spesific index in parcel list if drone id is equal 0
        public bool CheckChargeSlotsInBaseStation(int i); // search in spesific index in base station list if charge slots equals 0
        public void AssociateDroneToParcel(int droneId, int parcleId); // associate a drone to a parcel CHAC!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
        public void UpdateParclePickup(int id); // update parcel pickup CHAC!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!11
        public void UpdateParcleDelivery(int id); // update parcel delivery
        public void UpdateDroneCharge(int droneId, int stationId); // update drones charging
        public void ReleaseDroneCharge(int droneId, int stationId); // releas drone from charging
        public double[] Electricity();
    }
}
