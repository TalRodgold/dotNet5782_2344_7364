using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BO;
using BlApi;
using System.Runtime.CompilerServices;

namespace PL
{

    public class Model : INotifyPropertyChanged
    {

        private static readonly Lazy<Model> instance = new Lazy<Model>(() => new Model());// using lazy to improve performance and avoid wasteful computation, and reduce program memory requirements.  
        public event PropertyChangedEventHandler PropertyChanged;
        static readonly IBl bl = BlFactory.GetBl("BL");
        public static Model Instance { get; } = new Model();
        public ObservableCollection<PO.DroneToList> drones=new ObservableCollection<PO.DroneToList>();// = new(bl.GetListOfDronesToList());
        public ObservableCollection<PO.BaseStationToList> baseStations= new ObservableCollection<PO.BaseStationToList>();//= new(bl.GetListOfBaseStationsToList());
        public ObservableCollection<PO.CustomerToList> customers= new ObservableCollection<PO.CustomerToList>();//= new(bl.GetListOfCustomerToList());
        public ObservableCollection<PO.ParcelToList> parcels=new ObservableCollection<PO.ParcelToList>();// = new(bl.GetListOfParcelToList());
        static Model() { }
        Model()
        {
            lock (bl)
            {
                ObservableCollection<BO.DroneToList> droneToLists = new(bl.GetListOfDronesToList());
                foreach (var item in droneToLists)
                {
                    PO.DroneToList drone = new PO.DroneToList();
                    drone.Id = item.Id;
                    drone.Model = item.Model;
                    drone.Weight = item.Weight;
                    drone.Battery = item.Battery;
                    drone.NumberOfParcelInTransit = item.NumberOfParcelInTransit;
                    drone.DroneStatuses = item.DroneStatuses;
                    drone.CurrentLocation = new PO.Location();
                    drone.CurrentLocation.Latitude = item.CurrentLocation.Latitude;
                    drone.CurrentLocation.Longitude = item.CurrentLocation.Longitude;
                    drones.Add(drone);
                }
                ObservableCollection<BO.BaseStationToList> baseStationToLists = new(bl.GetListOfBaseStationsToList());
                foreach (var item in baseStationToLists)
                {
                    PO.BaseStationToList baseStation = new PO.BaseStationToList();
                    baseStation.Id = item.Id;
                    baseStation.Name = item.Name;
                    baseStation.OccupiedChargingSlots = item.OccupiedChargingSlots;
                    baseStation.FreeChargingSlots = item.FreeChargingSlots;
                    baseStations.Add(baseStation);
                }
                ObservableCollection<BO.CustomerToList> customerToLists = new(bl.GetListOfCustomerToList());
                foreach (var item in customerToLists)
                {
                    PO.CustomerToList customer = new PO.CustomerToList();
                    customer.Id = item.Id;
                    customer.Name = item.Name;
                    customer.Phone = item.Phone;
                    customer.NumberOfParcelsThatSentAndArrived = item.NumberOfParcelsThatSentAndArrived;
                    customer.ParcelsOnWayToClient = item.ParcelsOnWayToClient;
                    customer.ParcelsRecived = item.ParcelsRecived;
                    customer.ParcelsThatSentYetNotArrived = item.ParcelsThatSentYetNotArrived;
                    customers.Add(customer);
                }
                ObservableCollection<BO.ParcelToList> parcelToLists = new(bl.GetListOfParcelToList());
                foreach (var item in parcelToLists)
                {
                    PO.ParcelToList parcel = new PO.ParcelToList();
                    parcel.Id = item.Id;
                    parcel.ParcelStatus = item.ParcelStatus;
                    parcel.Prioritie = item.Prioritie;
                    parcel.ReciversName = item.ReciversName;
                    parcel.SendersName = item.SendersName;
                    parcel.Weight = item.Weight;
                    parcels.Add(parcel);
                } 
            }
        }
        public ObservableCollection<PO.DroneToList> Drones
        {
            get => drones;
            private set
            {
                drones = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drones)));
            }
        }
        public ObservableCollection<PO.BaseStationToList> BaseStations
        {
            get => baseStations;
            private set
            {
                baseStations = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drones)));
            }
        }
        public ObservableCollection<PO.ParcelToList> Parcels
        {
            get => parcels;
            private set
            {
                parcels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drones)));
            }
        }
        public ObservableCollection<PO.CustomerToList> Customers
        {
            get => customers;
            private set
            {
                customers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drones)));
            }
        }

    }
}
