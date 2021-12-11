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
        void AddDrone(Drone d, int? startingBaseStation); //Drone addition
        void AddCustomer(Customer c); //Customer addition
        void AddParcel(CustomerInParcel sender, CustomerInParcel reciver, Enums.WeightCategories weight, Enums.Priorities prioritie); // parcel addition
        Customer GetCustomerById(int? id); //Get customer from data-source by id
        Parcel GetParcelById(int? id, Predicate<IDAL.DO.Parcel> predicate = null); //from data-source by id
        Drone GetDroneById(int? id); //Get drone from data-source by id
        DroneToList GetDroneToList(int? id); //Get drone from list that is held here by id
        BaseStation GetBaseStationById(int? id); //Get base station from data-source by id
        ParcelToList GetParcelToListById(int? id); //Get parceltolist with manipulation from data source by id
        ParcelInTransit GetParcelInTransitById(int? id); //Get parceltolist with manipulation from data source by id
        void UpdateDroneModel(int? id, string newModel);//Update drone model
        void UpdateBaseStation(int? id, string name = "", int numberOfChargingSlots = 0);//Update base station name/number of charging slots
        void UpdateCustomer(int? id, string name = "", string phone = "");//Update customer name/phone
        void UpdateSendDroneToCharge(int? id);//Update-send drone to charge
        void UpdateReleseDrone(int? id);//Update-relese drone from charging slot
        void UpdateAssosiateDrone(int? id); //Update-assosiate drone to parcel
        void PickupParcelByDrone(int? droneId); //Update-pick-up parcel by dron
        void DeliveryParcelByDrone(int? droneId); //Update-dilavery parcel by drone
        List<BaseStation> GetListOfBaseStations(); //Convert from dal drone to drone to list
        List<Drone> GetListOfDrones(); //Get list of drones
        List<Customer> GetListOfCustomers(); //Get list of customers
        List<Parcel> GetListOfParcels(); //Get list of parcels
        List<ParcelToList> GetListOfNotAssigned(); //Get list of assosiated drones
        List<BaseStationToList> GetListOfFreeChargingStations(); //Get list of free charging stations
        List<Customer> GetListOfCustomerDalivered(); //Get list of customers that dalivered
        List<BaseStationToList> GetListOfBaseStationsToList(); //Get list of base stations
        List<DroneToList> GetListOfDronesToList();//Get list of drones
        List<DroneToList> GetListOfDroneToListByPredicat(Predicate<DroneToList> predicate); //Get list of drones by predicat
    }
}
