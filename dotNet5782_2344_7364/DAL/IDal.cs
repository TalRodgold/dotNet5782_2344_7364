using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        void ConstructBaseStation(int id, string name, int chargeSlots, double longtitude, double latitude); // construct a new base station
        void ConstructDrone(int id, string model, WeightCategories maxWeight); // construct a new drone
        void ConstructParcel(int id, int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime request, int droneId, DateTime schedual, DateTime pickUp, DateTime deliverd);// construct a new parcel
        void ConstructCustomer(int id, string name, string phone, double longtitude, double latitude); // construct a new customer
        void ConstructDroneCharge(int droneId, int stationId); // construct a new drone charge
        string ReturnDroneDataById(int id); // find drone in list and return it
        string ReturnBaseStationDataById(int id); // find base station in list and return it
        string ReturnCustomerDataById(int id); // find customer in list and return it
        string ReturnParcelDataById(int id); // find parcel in list and return it
        //bool CheckDroneIdInParcel(int i); // search in spesific index in parcel list if drone id is equal 0
        //bool CheckChargeSlotsInBaseStation(int i); // search in spesific index in base station list if charge slots equals 0
        void AssociateDroneToParcel(int droneId, int parcleId); // associate a drone to a parcel CHAC!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
        void UpdateParclePickup(int id); // update parcel pickup CHAC!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!11
        void UpdateParcleDelivery(int id); // update parcel delivery
        void UpdateDroneCharge(int droneId, int stationId); // update drones charging
        void ReleaseDroneCharge(int droneId, int stationId); // releas drone from charging
        int GetCountOfDroneList(); // returns size of drone list
        int GetCountOfBaseStationList(); // returns size of base station list
        int GetCountOfCustomerList(); // returns size of customer list
        int GetCountOfParcelList(); // returns size of parcel list
        string BaseStationLisToString();//the function return long string of all BaseStation
        string DroneLisToString();//the function return long string of all Drones
        string CustomerListToString();//the function return long string of all Customers
        string ParcelLisToString();//the function return long string of all Parcels
        string BaseStationfreeToString();//return all free BaseStation
        string ParcelsNotAssociatedToString();
        bool IfDroneExsists(int id);  // return true if id exisists in list of drones
        bool IfBaseStationExsists(int id);  // return  true if id exisists in list of base stations
        bool IfCustomerExsists(int id); // return  true if id exisists in list of customers
        bool IfParcelExsists(int id);  // return  true if id exisists inlist of parcels
        public int GenerateParcelId();


        //double[] Electricity();
    }
}
