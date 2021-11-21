using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
namespace IBL
{
    /// <summary>
    /// Interface with all the functions of BL
    /// </summary>
    public interface IBl
    {
        void AddBaseStation(BaseStation b); //Base station addition
        public void AddDrone(Drone d, int startingBaseStation); //Drone addition
        void AddCustomer(Customer c); //Customer addition
        void AddParcel(CustomerInParcel sender, CustomerInParcel reciver, Enums.WeightCategories weight, Enums.Priorities prioritie); // parcel addition
        Customer GetCustomerById(int id); //Get customer from data-source by id
        Parcel GetParcelById(int id, Predicate<IDAL.DO.Parcel> predicate = null); //from data-source by id
        Drone GetDroneById(int id); //Get drone from data-source by id
        DroneToList GetDroneToList(int id); //Get drone from list that is held here by id
        BaseStation GetBaseStationById(int id); //Get base station from data-source by id
        ParcelToList GetParcelToListById(int id); //Get parceltolist with manipulation from data source by id
        ParcelInTransit GetParcelInTransitById(int id); //Get parceltolist with manipulation from data source by id
        double CalculateBattery(DroneToList drone = null, Drone drone1 = null); //Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        void CalculateLocation(DroneToList drone); // calculate location of drone
        double CalculateDistance(Location x, Location y); //Calculate distance between 2 location return double
        double ConvertBatteryToDistance(DroneToList drone); //Convert battery to distance by state and weight 
        int CalculateMinDistance(Location y, Predicate<BaseStation> predicate = null, Predicate<BaseStation> predicate1 = null); // calculate minimum distance between location and base station
        int ReciveParcelId(Parcel parcel);
        Enums.ParcelStatus StatusCalculate(IDAL.DO.Parcel p);
        void UpdateDroneModel(int id, string newModel);
        void UpdateBaseStation(int id, string name = "", int numberOfChargingSlots = -1);
        void UpdateCustomer(int id, string name = "", string phone = "");
        void UpdateSendDroneToCharge(int id);
        void UpdateReleseDrone(int id, double time);
        void UpdateAssosiateDrone(int id);
        void PickupParcelByDrone(int droneId);
        void DilaveryParcelByDrone(int droneId);
        DroneToList ConvertDroneDalToList(IDAL.DO.Drone idalDrone);
        DroneToList ConvertDroneBlToList(Drone blDrone);
        Customer convertCustomerDalToBl(IDAL.DO.Customer customer);
        List<BaseStation> GetListOfBaseStations();
        List<Drone> GetListOfDrones();
        List<Customer> GetListOfCustomers();
        List<Parcel> GetListOfParcels();
        List<Parcel> GetListOfNotAssigned();
        List<BaseStation> GetListOfFreeChargingStations();
        List<Customer> GetListOfCustomerDalivered();
    }
}
