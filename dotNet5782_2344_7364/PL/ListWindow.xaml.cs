using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using System.Collections.ObjectModel;
namespace PL
{
    /// <summary>
    /// Interaction logic for ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        private IBl bl = BlFactory.GetBl("BL");
        //public ObservableCollection<BO.BaseStationToList> BsCollection = new ObservableCollection<BO.BaseStationToList>();
        //public ObservableCollection<BO.ParcelToList> PCollection = new ObservableCollection<BO.ParcelToList>();
        //public ObservableCollection<BO.CustomerToList> CCollection = new ObservableCollection<BO.CustomerToList>();
        //public ObservableCollection<BO.DroneToList> DCollection = new ObservableCollection<BO.DroneToList>();
        private PL.Model model=PlFactory.GetModel("Model");

        public ListWindow()
        {
            InitializeComponent();
            observe();
            DisplayChargingSlots.Items.Add("All");
            DisplayChargingSlots.Items.Add("Only free charging slots");
            DisplayChargingSlots.SelectedIndex = 0;

            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.DroneStatuses));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.WeightCategories));
            for (int i = 1; i < 11; i++)
            {
                NumOfFreeChargingSlots.Items.Add(i);
            }

        }
        private void observe()
        {
            lock (bl)
            {

                ListBaseStation.DataContext = model.BaseStations;
                ListParcel.DataContext = model.Parcels;
                ListDrone.DataContext = model.Drones;
                ListCustomer.DataContext = model.Customers;
            }
        }


        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                DroneWindow droneWindow = new DroneWindow();
                //droneWindow.AddButton.Click += updateDrones;
                droneWindow.Show();
            }

        }
        private void AddBaseStation_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                BaseStationWindow baseStationWindow = new BaseStationWindow(this);
                //baseStationWindow.AddButton.Click += updateStation;
                baseStationWindow.Show();
            }
        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                ParcelWindow parcelWindow = new ParcelWindow();
                //parcelWindow.AddButton.Click += updateParcel;
                parcelWindow.Show();
            }
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e) //aa
        {
            lock (bl)
            {
                CustomerWindow customerWindow = new CustomerWindow();
                //customerWindow.AddButton.Click += updateCustomer;
                customerWindow.Show();
            }
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void BaseStation_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            lock (bl)
            {
                PO.BaseStationToList baseStation = (PO.BaseStationToList)ListBaseStation.SelectedItem;
                BaseStationWindow baseStationWindow = new BaseStationWindow(baseStation.Id, baseStation.OccupiedChargingSlots);//, baseStation);

                //baseStationWindow.DeleteButton.Click += updateStation;

                //baseStationWindow.UpdateButton.Click += updateStation;
                baseStationWindow.Show();
            }

        }
        private void Parcel_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            lock (bl)
            {

                PO.ParcelToList parcel = (PO.ParcelToList)ListParcel.SelectedItem;
                ParcelWindow parcelWindow = new ParcelWindow(parcel.Id);//, parcel);
                //parcelWindow.DeleteButton.Click += updateParcel;
                parcelWindow.Show();
            }
        }
        private void Drone_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            lock (bl)
            {
                PO.DroneToList drone = (PO.DroneToList)ListDrone.SelectedItem;
                //   PO.DroneToList drone1 =(from dro in model.Drones where dro.Id == drone.Id select dro).FirstOrDefault();
                DroneWindow droneWindow = new DroneWindow(drone.Id);//,drone);
                //droneWindow.UpdateButton.Click += updateDrones;
                //droneWindow.SendDroneToChargeButton.Click += updateDrones;
                //droneWindow.RealesDroneFromChargingButton.Click += updateDrones;
                //droneWindow.AccociateDroneToParcelButton.Click += updateDrones;
                //droneWindow.PickUpParcelButton.Click += updateDrones;
                //droneWindow.DeliverParcelButton.Click += updateDrones;
                droneWindow.Show();
            }

        }
        private void Customer_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            lock (bl)
            {
                PO.CustomerToList customer = (PO.CustomerToList)ListCustomer.SelectedItem;
                CustomerWindow customerWindow = new CustomerWindow(customer.Id);//, customer);
                //customerWindow.UpdateButton.Click += updateCustomer;
                customerWindow.Show();
            }
        }
        private void updateStation(object sender, EventArgs e)
        {
            RefreshBaseStationFunc();
        }
        private void updateCustomer(object sender, EventArgs e)
        {
            RefreshCustomerFunc();
        }

        private void updateDrones(object sender, EventArgs e)
        {
            RefreshDroneFunc();
        }
        private void updateParcel(object sender, EventArgs e)
        {
            RefreshParcelFunc();
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) // combo box to select by weight categories
        {
            lock (bl)
            {
                if (StatusSelector.SelectedIndex == -1)
                {

                    // ListDrone.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem).ToList();
                    ObservableCollection < BO.DroneToList > droneToListsBo = new( bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem).ToList());
                    ObservableCollection<PO.DroneToList> droneToListsPo = new ObservableCollection<PO.DroneToList>();
                    foreach (var item in droneToListsBo)
                    {
                        PO.DroneToList drone = new PO.DroneToList();
                        drone.Id = item.Id;
                        drone.Model = item.Model;
                        drone.Weight = item.Weight;
                        drone.Battery = item.Battery;
                        drone.NumberOfParcelInTransit = item.NumberOfParcelInTransit;
                        drone.DroneStatuses = item.DroneStatuses;
                        drone.CurrentLocation = item.CurrentLocation;
                        droneToListsPo.Add(drone);
                    }
                    ListDrone.ItemsSource = droneToListsPo;
                }
                else
                {
                    //ListDrone.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem, predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
                    ObservableCollection<BO.DroneToList> droneToListsBo = new(bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem, predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList());
                    ObservableCollection<PO.DroneToList> droneToListsPo = new ObservableCollection<PO.DroneToList>();
                    foreach (var item in droneToListsBo)
                    {
                        PO.DroneToList drone = new PO.DroneToList();
                        drone.Id = item.Id;
                        drone.Model = item.Model;
                        drone.Weight = item.Weight;
                        drone.Battery = item.Battery;
                        drone.NumberOfParcelInTransit = item.NumberOfParcelInTransit;
                        drone.DroneStatuses = item.DroneStatuses;
                        drone.CurrentLocation = item.CurrentLocation;
                        droneToListsPo.Add(drone);
                    }
                    ListDrone.ItemsSource = droneToListsPo;
                }
            }
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) // combo box to select by status
        {

            lock (bl)
            {
                if (MaxWeightSelector.SelectedIndex == -1)
                {
                    //ListDrone.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
                    ObservableCollection<BO.DroneToList> droneToListsBo = new(bl.GetListOfDroneToListByPredicat(predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList());
                    ObservableCollection<PO.DroneToList> droneToListsPo = new ObservableCollection<PO.DroneToList>();
                    foreach (var item in droneToListsBo)
                    {
                        PO.DroneToList drone = new PO.DroneToList();
                        drone.Id = item.Id;
                        drone.Model = item.Model;
                        drone.Weight = item.Weight;
                        drone.Battery = item.Battery;
                        drone.NumberOfParcelInTransit = item.NumberOfParcelInTransit;
                        drone.DroneStatuses = item.DroneStatuses;
                        drone.CurrentLocation = item.CurrentLocation;
                        droneToListsPo.Add(drone);
                    }
                    ListDrone.ItemsSource = droneToListsPo;
                }
                else
                {
                    //ListDrone.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem, predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
                    ObservableCollection<BO.DroneToList> droneToListsBo = new(bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem, predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList());
                    ObservableCollection<PO.DroneToList> droneToListsPo = new ObservableCollection<PO.DroneToList>();
                    foreach (var item in droneToListsBo)
                    {
                        PO.DroneToList drone = new PO.DroneToList();
                        drone.Id = item.Id;
                        drone.Model = item.Model;
                        drone.Weight = item.Weight;
                        drone.Battery = item.Battery;
                        drone.NumberOfParcelInTransit = item.NumberOfParcelInTransit;
                        drone.DroneStatuses = item.DroneStatuses;
                        drone.CurrentLocation = item.CurrentLocation;
                        droneToListsPo.Add(drone);
                    }
                    ListDrone.ItemsSource = droneToListsPo;
                }
            }
        }

        private void DisplayChargingSlots_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            lock (bl)
            {
                if (DisplayChargingSlots.SelectedIndex == 0)
                {
                    ListBaseStation.ItemsSource = model.BaseStations;

                }
                else if (DisplayChargingSlots.SelectedIndex == 1)
                {
                    //ListBaseStation.ItemsSource = bl.GetListOfFreeChargingStations();
                    ObservableCollection<PO.BaseStationToList> baseStationToListsPO = new ObservableCollection<PO.BaseStationToList>();
                    ObservableCollection<BO.BaseStationToList> baseStationToLists = new(bl.GetListOfBaseStationsToList());
                    foreach (var item in baseStationToLists)
                    {
                        PO.BaseStationToList baseStation = new PO.BaseStationToList();
                        baseStation.Id = item.Id;
                        baseStation.Name = item.Name;
                        baseStation.OccupiedChargingSlots = item.OccupiedChargingSlots;
                        baseStation.FreeChargingSlots = item.FreeChargingSlots;
                        baseStationToListsPO.Add(baseStation);
                    }
                    ListBaseStation.ItemsSource = baseStationToListsPO;
                }
            }

        }


        private void NumOfFreeChargingSlots_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            lock (bl)
            {
                //ListBaseStation.ItemsSource = bl.GetListOfFreeChargingStations(NumOfFreeChargingSlots.SelectedIndex + 1);
                ObservableCollection<PO.BaseStationToList> baseStationToListsPO = new ObservableCollection<PO.BaseStationToList>();
                ObservableCollection<BO.BaseStationToList> baseStationToLists = new(bl.GetListOfFreeChargingStations(NumOfFreeChargingSlots.SelectedIndex + 1));
                foreach (var item in baseStationToLists)
                {
                    PO.BaseStationToList baseStation = new PO.BaseStationToList();
                    baseStation.Id = item.Id;
                    baseStation.Name = item.Name;
                    baseStation.OccupiedChargingSlots = item.OccupiedChargingSlots;
                    baseStation.FreeChargingSlots = item.FreeChargingSlots;
                    baseStationToListsPO.Add(baseStation);
                }
                ListBaseStation.ItemsSource = baseStationToListsPO;
            }
        }

        private void GroupByStatus_click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                var groups = bl.GroupingStatuses();
                {

                    ObservableCollection<PO.DroneToList> droneToLists = new();
                    foreach (var item in groups)
                    {
                        foreach (var innerItem in item)
                        {
                            PO.DroneToList drone = new PO.DroneToList();
                            drone.Id = innerItem.Id;
                            drone.Model = innerItem.Model;
                            drone.Weight = innerItem.Weight;
                            drone.Battery = innerItem.Battery;
                            drone.NumberOfParcelInTransit = innerItem.NumberOfParcelInTransit;
                            drone.DroneStatuses = innerItem.DroneStatuses;
                            drone.CurrentLocation = innerItem.CurrentLocation;
                            droneToLists.Add(drone);
                        }
                    }
                    ListDrone.ItemsSource = droneToLists;
                }
            }
        }

        private void GroupByWeight_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                var groups = bl.GroupingWeight();
                ObservableCollection<PO.DroneToList> droneToLists = new();
                foreach (var item in groups)
                {
                    foreach (var innerItem in item)
                    {
                        PO.DroneToList drone = new PO.DroneToList();
                        drone.Id = innerItem.Id;
                        drone.Model = innerItem.Model;
                        drone.Weight = innerItem.Weight;
                        drone.Battery = innerItem.Battery;
                        drone.NumberOfParcelInTransit = innerItem.NumberOfParcelInTransit;
                        drone.DroneStatuses = innerItem.DroneStatuses;
                        drone.CurrentLocation = innerItem.CurrentLocation;
                        droneToLists.Add(drone);
                    }
                }
                ListDrone.ItemsSource = droneToLists;
            }
        }

        private void refreshDrone_button(object sender, RoutedEventArgs e)
        {
            RefreshDroneFunc();

        }

        private void refreshBaseStation_button(object sender, RoutedEventArgs e)
        {
            RefreshBaseStationFunc();
            DisplayChargingSlots.SelectedIndex = 0;
            NumOfFreeChargingSlots.SelectedIndex = -1;
        }
        private void RefreshDroneFunc()
        {
            lock (bl)
            {
                model.Drones.Clear();
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
                    drone.CurrentLocation = item.CurrentLocation;
                    model.Drones.Add(drone);
                }
                ListDrone.ItemsSource = model.Drones;
            }

        }
        private void RefreshParcelFunc()
        {
            lock (bl)
            {
                model.Parcels.Clear();
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
                    model.parcels.Add(parcel);
                }
                ListParcel.ItemsSource = model.Parcels;
            }
        }
        private void RefreshBaseStationFunc()
        {
            lock (bl)
            {
                model.BaseStations.Clear();
                ObservableCollection<BO.BaseStationToList> baseStationToLists = new(bl.GetListOfBaseStationsToList());
                foreach (var item in baseStationToLists)
                {
                    PO.BaseStationToList baseStation = new PO.BaseStationToList();
                    baseStation.Id = item.Id;
                    baseStation.Name = item.Name;
                    baseStation.OccupiedChargingSlots = item.OccupiedChargingSlots;
                    baseStation.FreeChargingSlots = item.FreeChargingSlots;
                    model.baseStations.Add(baseStation);
                }
                ListBaseStation.ItemsSource = model.BaseStations;

            }

        }
        private void RefreshCustomerFunc()
        {
            lock (bl)
            {
                model.Customers.Clear();
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
                    model.customers.Add(customer);
                }
                ListCustomer.ItemsSource = model.Customers;
            }

        }

        private void refreshParcel_button(object sender, RoutedEventArgs e)
        {
            RefreshParcelFunc();
        }

        private void refreshCustomer_button(object sender, RoutedEventArgs e)
        {
            RefreshCustomerFunc();
        }

        private void GroupByFreeChargingSlots_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                var groups = bl.GroupingFreeChargingSlots();
                ObservableCollection<PO.BaseStationToList> baseStationToLists = new();
                foreach (var item in groups)
                {
                    foreach (var innerItem in item)
                    {
                        PO.BaseStationToList baseStation = new PO.BaseStationToList();
                        baseStation.Id = innerItem.Id;
                        baseStation.Name = innerItem.Name;
                        baseStation.OccupiedChargingSlots = innerItem.OccupiedChargingSlots;
                        baseStation.FreeChargingSlots = innerItem.FreeChargingSlots;
                        baseStationToLists.Add(baseStation);
                    }
                }
                ListBaseStation.ItemsSource = baseStationToLists;
            }
        }

        private void GroupBySender_click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                var groups = bl.GroupingSender();
                ObservableCollection<PO.ParcelToList> parcelToLists = new ();
                foreach (var item in groups)
                {
                    foreach (var innerItem in item)
                    {
                        PO.ParcelToList parcel = new PO.ParcelToList();
                        parcel.Id = innerItem.Id;
                        parcel.ParcelStatus = innerItem.ParcelStatus;
                        parcel.Prioritie = innerItem.Prioritie;
                        parcel.ReciversName = innerItem.ReciversName;
                        parcel.SendersName = innerItem.SendersName;
                        parcel.Weight = innerItem.Weight;
                        parcelToLists.Add(parcel);
                    }
                }
                ListParcel.ItemsSource = parcelToLists;
            }
        }

        private void GroupByReciver_click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                var groups = bl.GroupingReciver();
                ObservableCollection<PO.ParcelToList> parcelToLists = new();
                foreach (var item in groups)
                {
                    foreach (var innerItem in item)
                    {
                        PO.ParcelToList parcel = new PO.ParcelToList();
                        parcel.Id = innerItem.Id;
                        parcel.ParcelStatus = innerItem.ParcelStatus;
                        parcel.Prioritie = innerItem.Prioritie;
                        parcel.ReciversName = innerItem.ReciversName;
                        parcel.SendersName = innerItem.SendersName;
                        parcel.Weight = innerItem.Weight;
                        parcelToLists.Add(parcel);
                    }
                }
                ListParcel.ItemsSource = parcelToLists;
            }
        }

        private void listDrones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
