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
        [MethodImpl(MethodImplOptions.Synchronized)]
        void AddBaseStation(BaseStation b); //Base station addition
        [MethodImpl(MethodImplOptions.Synchronized)]
        void AddDrone(Drone d, int? startingBaseStation); //Drone addition
        [MethodImpl(MethodImplOptions.Synchronized)]
        void AddCustomer(Customer c); //Customer addition
        [MethodImpl(MethodImplOptions.Synchronized)]
        void AddParcel(CustomerInParcel sender, CustomerInParcel reciver, Enums.WeightCategories weight, Enums.Priorities prioritie); // parcel addition
        [MethodImpl(MethodImplOptions.Synchronized)]
        Customer GetCustomerById(int? id); //Get customer from data-source by id
        [MethodImpl(MethodImplOptions.Synchronized)]
        CustomerToList GetCustomerToListById(int? id); //Get Customer to list by id
        [MethodImpl(MethodImplOptions.Synchronized)]
        Parcel GetParcelById(int? id, Predicate<DO.Parcel> predicate = null); //from data-source by id
        [MethodImpl(MethodImplOptions.Synchronized)]
        Drone GetDroneById(int? id); //Get drone from data-source by id
        [MethodImpl(MethodImplOptions.Synchronized)]
        DroneToList GetDroneToList(int? id); //Get drone from list that is held here by id
        [MethodImpl(MethodImplOptions.Synchronized)]
        BaseStation GetBaseStationById(int? id); //Get base station from data-source by id
        [MethodImpl(MethodImplOptions.Synchronized)]
        ParcelToList GetParcelToListById(int? id); //Get parceltolist with manipulation from data source by id
        [MethodImpl(MethodImplOptions.Synchronized)]
        ParcelInTransit GetParcelInTransitById(int? id); //Get parceltolist with manipulation from data source by id
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateDroneModel(int? id, string newModel);//Update drone model
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateBaseStation(int? id, string name = "", int numberOfChargingSlots = 0);//Update base station name/number of charging slots
        [MethodImpl(MethodImplOptions.Synchronized)]
        void DeleteBaseStation(int? id); // delete base station 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int? id);//Delete Parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateCustomer(int? id, string name = "", string phone = "");//Update customer name/phone
        [MethodImpl(MethodImplOptions.Synchronized)]
        int? UpdateSendDroneToCharge(int? id);//Update-send drone to charge
        [MethodImpl(MethodImplOptions.Synchronized)]
        int? UpdateReleseDrone(int? id);//Update-relese drone from charging slot
        [MethodImpl(MethodImplOptions.Synchronized)]
        int? UpdateAssosiateDrone(int? id); //Update-assosiate drone to parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        void PickupParcelByDrone(int? droneId); //Update-pick-up parcel by dron
        [MethodImpl(MethodImplOptions.Synchronized)]
        void DeliveryParcelByDrone(int? droneId); //Update-dilavery parcel by drone
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<BaseStation> GetListOfBaseStations(); //Convert from dal drone to drone to list
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<Drone> GetListOfDrones(); //Get list of drones
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<Customer> GetListOfCustomers(); //Get list of customers
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<CustomerToList> GetListOfCustomerToList(); //Get list of customers
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<Parcel> GetListOfParcels(); //Get list of parcels
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<ParcelToList> GetListOfParcelToList();//Get list of parcel to list
        [MethodImpl(MethodImplOptions.Synchronized)]
        List<ParcelToList> GetListOfNotAssigned(); //Get list of assosiated drones
        [MethodImpl(MethodImplOptions.Synchronized)]
        List<BaseStationToList> GetListOfFreeChargingStations(int num = 0); //Get list of free charging stations
        [MethodImpl(MethodImplOptions.Synchronized)]
        List<Customer> GetListOfCustomerDalivered(); //Get list of customers that dalivered
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<BaseStationToList> GetListOfBaseStationsToList(); //Get list of base stations
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<DroneToList> GetListOfDronesToList();//Get list of drones
        [MethodImpl(MethodImplOptions.Synchronized)]
        List<DroneToList> GetListOfDroneToListByPredicat(Predicate<DroneToList> predicate, Predicate<DroneToList> predicate1 = null); //Get list of drones by predicat
        [MethodImpl(MethodImplOptions.Synchronized)]
        DroneToList convertDroneBlToList(Drone blDrone);// convert drone in bl to list
        [MethodImpl(MethodImplOptions.Synchronized)]
        BaseStationToList convertBasestationToBasestationTolist(BaseStation station); // convert base station to list of base stations
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<IGrouping<Enums.WeightCategories, DroneToList>> GroupingWeight(); // group by wight
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<IGrouping<Enums.DroneStatuses, DroneToList>> GroupingStatuses(); // group by status
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<IGrouping<int, BaseStationToList>> GroupingFreeChargingSlots(); // group by free charging slots
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<IGrouping<string, ParcelToList>> GroupingSender(); // group by sender
        [MethodImpl(MethodImplOptions.Synchronized)]
        IEnumerable<IGrouping<string, ParcelToList>> GroupingReciver(); // group by reciver
        [MethodImpl(MethodImplOptions.Synchronized)]
        double calculateBattery(DroneToList drone = null, double distance = 0.0); //Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        [MethodImpl(MethodImplOptions.Synchronized)]
        double calculateBattery(Drone drone = null, double distance = 0.0); //Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        [MethodImpl(MethodImplOptions.Synchronized)]
        double calculateDistance(Location x, Location y); //Calculate distance between 2 location return double
        [MethodImpl(MethodImplOptions.Synchronized)]
        double convertBatteryToDistance(DroneToList drone); //Convert battery to distance by state and weight 
        [MethodImpl(MethodImplOptions.Synchronized)]
        int? calculateMinDistance(Location y, Predicate<BaseStation> predicate = null, Predicate<BaseStation> predicate1 = null); //Calculate min distance between loction y and 2 option 1.the closer station 2.the closer station and more 2 terms
        [MethodImpl(MethodImplOptions.Synchronized)]
        bool CalculateWhetherTheDroneHaveEnoghBattery(double distance, DroneToList drone); //Calculate whether the drone have enogh battery
        [MethodImpl(MethodImplOptions.Synchronized)]
        Enums.ParcelStatus statusCalculate(DO.Parcel p); // calculate status
        [MethodImpl(MethodImplOptions.Synchronized)]
        void StartSimulator(int droneId, Action func, Func<bool> checkStop); // start simulator
        [MethodImpl(MethodImplOptions.Synchronized)]
        bool CheckAvailableParcels(Drone drone); // check for any available parcels
    }
}