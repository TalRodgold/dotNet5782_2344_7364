using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
namespace BlApi
{
    /// <summary>
    /// Interface with all the functions of BL
    /// </summary>
    public interface IBl
    {
        //void SimulatorFunc(int? id, Action action, Func<bool> func);
        void AddBaseStation(BaseStation b); //Base station addition
        void AddDrone(Drone d, int? startingBaseStation); //Drone addition
        void AddCustomer(Customer c); //Customer addition
        void AddParcel(CustomerInParcel sender, CustomerInParcel reciver, Enums.WeightCategories weight, Enums.Priorities prioritie); // parcel addition
        Customer GetCustomerById(int? id); //Get customer from data-source by id
        CustomerToList GetCustomerToListById(int? id); //Get Customer to list by id
        Parcel GetParcelById(int? id, Predicate<DO.Parcel> predicate = null); //from data-source by id
        Drone GetDroneById(int? id); //Get drone from data-source by id
        DroneToList GetDroneToList(int? id); //Get drone from list that is held here by id
        BaseStation GetBaseStationById(int? id); //Get base station from data-source by id
        ParcelToList GetParcelToListById(int? id); //Get parceltolist with manipulation from data source by id
        ParcelInTransit GetParcelInTransitById(int? id); //Get parceltolist with manipulation from data source by id
        void UpdateDroneModel(int? id, string newModel);//Update drone model
        void UpdateBaseStation(int? id, string name = "", int numberOfChargingSlots = 0);//Update base station name/number of charging slots
        void DeleteBaseStation(int? id); // delete base station 
        public void DeleteParcel(int? id);//Delete Parcel
        void UpdateCustomer(int? id, string name = "", string phone = "");//Update customer name/phone
        int? UpdateSendDroneToCharge(int? id);//Update-send drone to charge
        int? UpdateReleseDrone(int? id);//Update-relese drone from charging slot
        int? UpdateAssosiateDrone(int? id); //Update-assosiate drone to parcel
        void PickupParcelByDrone(int? droneId); //Update-pick-up parcel by dron
        void DeliveryParcelByDrone(int? droneId); //Update-dilavery parcel by drone
        IEnumerable<BaseStation> GetListOfBaseStations(); //Convert from dal drone to drone to list
        IEnumerable<Drone> GetListOfDrones(); //Get list of drones
        IEnumerable<Customer> GetListOfCustomers(); //Get list of customers
        IEnumerable<CustomerToList> GetListOfCustomerToList(); //Get list of customers
        IEnumerable<Parcel> GetListOfParcels(); //Get list of parcels
        IEnumerable<ParcelToList> GetListOfParcelToList();//Get list of parcel to list
        List<ParcelToList> GetListOfNotAssigned(); //Get list of assosiated drones
        List<BaseStationToList> GetListOfFreeChargingStations(int num = 0); //Get list of free charging stations
        List<Customer> GetListOfCustomerDalivered(); //Get list of customers that dalivered
        IEnumerable<BaseStationToList> GetListOfBaseStationsToList(); //Get list of base stations
        IEnumerable<DroneToList> GetListOfDronesToList();//Get list of drones
        List<DroneToList> GetListOfDroneToListByPredicat(Predicate<DroneToList> predicate, Predicate<DroneToList> predicate1 = null); //Get list of drones by predicat
        DroneToList convertDroneBlToList(Drone blDrone);
        BaseStationToList convertBasestationToBasestationTolist(BaseStation station);
        // BaseStationToList GetBaseStationToListById(int? id);//Get Customer to list by id
        IEnumerable<IGrouping<Enums.WeightCategories, DroneToList>> GroupingWeight();
        IEnumerable<IGrouping<Enums.DroneStatuses, DroneToList>> GroupingStatuses();
        IEnumerable<IGrouping<int, BaseStationToList>> GroupingFreeChargingSlots();
        IEnumerable<IGrouping<string, ParcelToList>> GroupingSender();
        IEnumerable<IGrouping<string, ParcelToList>> GroupingReciver();
        double calculateBattery(DroneToList drone = null, double distance = 0.0); //Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        double calculateBattery(Drone drone = null, double distance = 0.0); //Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        double calculateDistance(Location x, Location y); //Calculate distance between 2 location return double
        double convertBatteryToDistance(DroneToList drone); //Convert battery to distance by state and weight 
        int? calculateMinDistance(Location y, Predicate<BaseStation> predicate = null, Predicate<BaseStation> predicate1 = null); //Calculate min distance between loction y and 2 option 1.the closer station 2.the closer station and more 2 terms
        bool CalculateWhetherTheDroneHaveEnoghBattery(double distance, DroneToList drone); //Calculate whether the drone have enogh battery
        Enums.ParcelStatus statusCalculate(DO.Parcel p);
        void StartSimulator(int droneId, Action func, Func<bool> checkStop);
        bool CheckAvailableParcels(Drone drone);


    }
}