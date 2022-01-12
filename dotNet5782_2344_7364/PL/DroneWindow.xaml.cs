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
using System.Collections.ObjectModel;
using BlApi;
namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private IBl bl = BlFactory.GetBl("BL");
        private PL.Model model = PlFactory.GetModel("Model");
        //public BO.Drone drone;
        public PO.Drone drone=new PO.Drone();
       // public PO.DroneToList droneToList=new PO.DroneToList();
        public DroneWindow() // constructor for adding new drone
        {
            InitializeComponent();
            AddButton.Visibility = Visibility.Visible; // make butten visible
            ExitButton.Visibility = Visibility.Hidden;
            UpdateButton.IsEnabled = false; 
            Battery.IsEnabled = false;
            StatusSelector.IsEnabled = false;
            Latitude.IsEnabled = false;
            Longitude.IsEnabled = false;

            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.DroneStatuses));
            StartingBaseStation.Visibility = Visibility.Visible;
            BaseStationTxtBox.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
          
        }
        public DroneWindow(int? id)//,PO.DroneToList newd) // constructor for Drone update
        {
            lock (bl)
            {
                //droneToList = newd;
                BO.Drone newDrone = bl.GetDroneById(id);
                drone.Id = newDrone.Id;
                drone.Model = newDrone.Model;
                drone.Weight = newDrone.Weight;
                drone.Battery = newDrone.Battery;
               
                if(!object.Equals(newDrone.ParcelInTransit,null))
                {
                    drone.ParcelInTransit = new PO.ParcelInTransit();
                    if(!object.Equals(drone.ParcelInTransit.CustomerInParcelReciver, null))
                    {
                        drone.ParcelInTransit.CustomerInParcelReciver = new PO.CustomerInParcel();
                        drone.ParcelInTransit.CustomerInParcelReciver.Id = newDrone.ParcelInTransit.CustomerInParcelReciver.Id;
                        drone.ParcelInTransit.CustomerInParcelReciver.Name = newDrone.ParcelInTransit.CustomerInParcelReciver.Name;
                    }
                    if (!object.Equals(drone.ParcelInTransit.CustomerInParcelReciver, null))
                    {
                        drone.ParcelInTransit.CustomerInParcelSender = new PO.CustomerInParcel();
                        drone.ParcelInTransit.CustomerInParcelSender.Id = newDrone.ParcelInTransit.CustomerInParcelSender.Id;
                        drone.ParcelInTransit.CustomerInParcelSender.Name = newDrone.ParcelInTransit.CustomerInParcelSender.Name;
                    }
                }
                
                drone.DroneStatuses = newDrone.DroneStatuses;
                drone.CurrentLocation = newDrone.CurrentLocation;

                InitializeComponent();

                MainGrid.DataContext = drone;

                UpdateButton.Visibility = Visibility.Visible; // make butten visible
                AddButton.IsEnabled = false; // enable add button
                if (drone.DroneStatuses == BO.Enums.DroneStatuses.Available)
                {
                    SendDroneToChargeButton.Visibility = Visibility.Visible;
                    AccociateDroneToParcelButton.Visibility = Visibility.Visible;
                }
                if (drone.DroneStatuses == BO.Enums.DroneStatuses.Maintenance)
                {
                    RealesDroneFromChargingButton.Visibility = Visibility.Visible;
                }
                if (drone.DroneStatuses == BO.Enums.DroneStatuses.Delivery && newDrone.ParcelInTransit.Status)
                {
                    PickUpParcelButton.Visibility = Visibility.Visible;
                }
                if (drone.DroneStatuses == BO.Enums.DroneStatuses.Delivery && bl.GetParcelById((int)newDrone.ParcelInTransit.Id).PickupTime != null && newDrone.ParcelInTransit.Status)
                {
                    DeliverParcelButton.Visibility = Visibility.Visible;
                }
                // enable text boxes where we dont want user to change
                Id.IsEnabled = false;
                Battery.IsEnabled = false;
                MaxWeightSelector.IsEnabled = false;
                StatusSelector.IsEnabled = false;
                ParcelInTransit.IsEnabled = false;
                Latitude.IsEnabled = false;
                Longitude.IsEnabled = false;
                // set combo boxes
                MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.WeightCategories));
                StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.DroneStatuses));
          
            }
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) // combo box for max weghit
        {
            MaxWeightSelector.SelectedItem = (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem;
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) // combo box for status
        {
            StatusSelector.SelectedItem = (BO.Enums.DroneStatuses)StatusSelector.SelectedItem;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e) // add a drone button
        {
            try
            {
                lock (bl)
                {
                    BO.Drone newDrone = new BO.Drone(int.Parse(Id.Text), Model.Text, (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem);
                    bl.AddDrone(newDrone, Convert.ToInt32(BaseStationTxtBox.Text));
                    BO.DroneToList droneBo = bl.convertDroneBlToList(newDrone);
                    PO.DroneToList dronePo = new PO.DroneToList();
                    dronePo.Id = droneBo.Id;
                    dronePo.Model = droneBo.Model;
                    dronePo.Weight = droneBo.Weight;
                    dronePo.Battery = droneBo.Battery;
                    dronePo.NumberOfParcelInTransit = droneBo.NumberOfParcelInTransit;
                    dronePo.DroneStatuses = droneBo.DroneStatuses;
                    dronePo.CurrentLocation = droneBo.CurrentLocation;
                    model.drones.Add(dronePo);
                    MessageBox.Show("Drone added sucsecfully");
                    this.Close(); 
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e) // update drone model button
        {
            try
            {
                lock (bl)
                {
                    bl.UpdateDroneModel(drone.Id, Model.Text.ToString());
                    PO.DroneToList droneToList = new PO.DroneToList();
                    droneToList = (from drone_ in model.drones where drone_.Id == drone.Id select drone_).FirstOrDefault();
                    if (Model.Text != "")
                    {
                        droneToList.Model = Model.Text;
                        drone.Model = Model.Text;
                    }
                    // Refresh();  
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString()); 
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) // cancel button
        {
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) // exit button
        {            
            this.Close();
        }
        private void SendDroneToChargeButton_Click(object sender, RoutedEventArgs e) // send drone to charge button
        {
            try
            {
                lock (bl)
                {
                    int stationId=bl.UpdateSendDroneToCharge(drone.Id);
                    BO.DroneToList droneToListBo= bl.GetDroneToList(drone.Id);
                    BO.BaseStation baseStationPo = bl.GetBaseStationById(stationId);
                    PO.DroneToList droneToList = new PO.DroneToList();
                    PO.BaseStationToList baseStationToList = new PO.BaseStationToList();
                    droneToList = (from drone_ in model.drones where drone_.Id == drone.Id select drone_).FirstOrDefault();
                    baseStationToList = (from station in model.baseStations where station.Id == stationId select station).FirstOrDefault();
                    drone.DroneStatuses = BO.Enums.DroneStatuses.Maintenance;
                    baseStationToList.FreeChargingSlots -= 1;
                    baseStationToList.OccupiedChargingSlots += 1;
                    droneToList.DroneStatuses = drone.DroneStatuses;
                    droneToList.CurrentLocation = baseStationPo.Location;
                    droneToList.Battery = droneToListBo.Battery;
                    //Refresh(); 
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void RealesDroneFromChargingButton_Click(object sender, RoutedEventArgs e) // releas drone from charging button
        {
            try
            {

                lock (bl)
                {
                    int stationId= bl.UpdateReleseDrone(drone.Id);
                    BO.DroneToList droneToListBo = bl.GetDroneToList(drone.Id);
                    PO.DroneToList droneToList = new PO.DroneToList();
                    BO.BaseStation baseStationPo = bl.GetBaseStationById(stationId);
                    PO.BaseStationToList baseStationToList = new PO.BaseStationToList();
                    baseStationToList = (from station in model.baseStations where station.Id == stationId select station).FirstOrDefault();
                    droneToList = (from drone_ in model.drones where drone_.Id == drone.Id select drone_).FirstOrDefault();
                    drone.DroneStatuses = BO.Enums.DroneStatuses.Available;
                    droneToList.DroneStatuses = BO.Enums.DroneStatuses.Available;
                    droneToList.Battery = droneToListBo.Battery;
                    baseStationToList.FreeChargingSlots -= 1;
                    baseStationToList.OccupiedChargingSlots += 1;
                    //Refresh(); 
                }
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.ToString());
            }
        }

        private void AccociateDroneToParcelButton_Click(object sender, RoutedEventArgs e) // accocate drone to parcel button
        {
            try
            {
                lock (bl)
                {
                    int parcelId= bl.UpdateAssosiateDrone(drone.Id);
                    //BO.ParcelToList parcelToListBo = bl.GetParcelToListById(parcelId);
                    //PO.DroneToList droneToListPo = new PO.DroneToList();
                    //PO.ParcelToList parcelToListPo = new PO.ParcelToList();
                    //PO.CustomerToList customerToListSenderPo = new PO.CustomerToList();
                    //PO.CustomerToList customerToListReciverPo = new PO.CustomerToList();
                    //droneToListPo = (from drone_ in model.drones where drone_.Id == drone.Id select drone_).FirstOrDefault();
                    //parcelToListPo = (from parcel_ in model.parcels where parcel_.Id == parcelId select parcel_).FirstOrDefault();
                    //drone.DroneStatuses = BO.Enums.DroneStatuses.Delivery;
                    //droneToList.DroneStatuses = drone.DroneStatuses;
                  //  RefreshAll(); 
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void PickUpParcelButton_click(object sender, RoutedEventArgs e) // pivk up parcel button
        {
            try
            {
                lock (bl)
                {
                    bl.PickupParcelByDrone(drone.Id);

                    //PO.DroneToList droneToList = new PO.DroneToList();
                    //droneToList = (from drone_ in model.drones where drone_.Id == drone.Id select drone_).FirstOrDefault();
                    //drone.DroneStatuses = BO.Enums.DroneStatuses.Delivery;
                    //droneToList.DroneStatuses = drone.DroneStatuses;
                   // RefreshAll(); 
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void DeliverParcelButton_Click(object sender, RoutedEventArgs e) // deliver parcel button
        {
            try
            {
                lock (bl)
                {
                    bl.DeliveryParcelByDrone(drone.Id);
                    //PO.DroneToList droneToList = new PO.DroneToList();
                    //droneToList = (from drone_ in model.drones where drone_.Id == drone.Id select drone_).FirstOrDefault();
                    //drone.DroneStatuses = BO.Enums.DroneStatuses.Available;
                    //droneToList.DroneStatuses = drone.DroneStatuses;
                    //RefreshAll(); 
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
        private void Refresh() // refresh
        {
            lock (bl)
            {
                BO.Drone newDrone = bl.GetDroneById(drone.Id);
//Battery.Value = drone.Battery * 100;
                Model.Text = drone.Model.ToString();
                StatusSelector.SelectedItem = drone.DroneStatuses;
                ParcelInTransit.Text = newDrone.ParcelInTransit.Id.ToString();
               // Longitude.Text = drone.CurrentLocation.LongitudeInSexa();
                //Latitude.Text = drone.CurrentLocation.LatitudeInSexa();

                if (newDrone.DroneStatuses == BO.Enums.DroneStatuses.Available)
                {
                    SendDroneToChargeButton.Visibility = Visibility.Visible;
                    AccociateDroneToParcelButton.Visibility = Visibility.Visible;
                }
                else
                {
                    SendDroneToChargeButton.Visibility = Visibility.Hidden;
                    AccociateDroneToParcelButton.Visibility = Visibility.Hidden;
                }
                if (newDrone.DroneStatuses == BO.Enums.DroneStatuses.Maintenance)
                {
                    RealesDroneFromChargingButton.Visibility = Visibility.Visible;
                }
                else
                {
                    RealesDroneFromChargingButton.Visibility = Visibility.Hidden;
                }
                if (newDrone.DroneStatuses == BO.Enums.DroneStatuses.Delivery)
                {
                    if (bl.GetParcelById(newDrone.ParcelInTransit.Id).DeliveryTime != null && !newDrone.ParcelInTransit.Status)//(bl.GetParcelById(drone.NumberOfParcelInTransit).DeliveryTime != null && !bl.GetParcelInTransitById(drone.NumberOfParcelInTransit).Status)
                    {
                        DeliverParcelButton.Visibility = Visibility.Hidden;
                        PickUpParcelButton.Visibility = Visibility.Visible;
                    }
                    else if (bl.GetParcelById((int)newDrone.ParcelInTransit.Id).PickupTime != null && !newDrone.ParcelInTransit.Status)//true until pickup
                    {
                        DeliverParcelButton.Visibility = Visibility.Visible;
                        PickUpParcelButton.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        DeliverParcelButton.Visibility = Visibility.Hidden;
                        PickUpParcelButton.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    PickUpParcelButton.Visibility = Visibility.Hidden;
                    DeliverParcelButton.Visibility = Visibility.Hidden;
                } 
            }
        }
        //private void RefreshAll()

        //{
        //    ObservableCollection<BO.DroneToList> droneToLists = new(bl.GetListOfDronesToList());
        //    foreach (var item in droneToLists)
        //    {
        //        PO.DroneToList drone = new PO.DroneToList();
        //        drone.Id = item.Id;
        //        drone.Model = item.Model;
        //        drone.Weight = item.Weight;
        //        drone.Battery = item.Battery;
        //        drone.NumberOfParcelInTransit = item.NumberOfParcelInTransit;
        //        drone.DroneStatuses = item.DroneStatuses;
        //        drone.CurrentLocation = item.CurrentLocation;
        //        model.drones.Add(drone);
        //    }
        //    ObservableCollection<BO.BaseStationToList> baseStationToLists = new(bl.GetListOfBaseStationsToList());
        //    foreach (var item in baseStationToLists)
        //    {
        //        PO.BaseStationToList baseStation = new PO.BaseStationToList();
        //        baseStation.Id = item.Id;
        //        baseStation.Name = item.Name;
        //        baseStation.OccupiedChargingSlots = item.OccupiedChargingSlots;
        //        baseStation.FreeChargingSlots = item.FreeChargingSlots;
        //        model.baseStations.Add(baseStation);
        //    }
        //    ObservableCollection<BO.CustomerToList> customerToLists = new(bl.GetListOfCustomerToList());
        //    foreach (var item in customerToLists)
        //    {
        //        PO.CustomerToList customer = new PO.CustomerToList();
        //        customer.Id = item.Id;
        //        customer.Name = item.Name;
        //        customer.Phone = item.Phone;
        //        customer.NumberOfParcelsThatSentAndArrived = item.NumberOfParcelsThatSentAndArrived;
        //        customer.ParcelsOnWayToClient = item.ParcelsOnWayToClient;
        //        customer.ParcelsRecived = item.ParcelsRecived;
        //        customer.ParcelsThatSentYetNotArrived = item.ParcelsThatSentYetNotArrived;
        //        model.customers.Add(customer);
        //    }
        //    ObservableCollection<BO.ParcelToList> parcelToLists = new(bl.GetListOfParcelToList());
        //    foreach (var item in parcelToLists)
        //    {
        //        PO.ParcelToList parcel = new PO.ParcelToList();
        //        parcel.Id = item.Id;
        //        parcel.ParcelStatus = item.ParcelStatus;
        //        parcel.Prioritie = item.Prioritie;
        //        parcel.ReciversName = item.ReciversName;
        //        parcel.SendersName = item.SendersName;
        //        parcel.Weight = item.Weight;
        //        model.parcels.Add(parcel);
        //    }
        //    BO.Drone newDrone = bl.GetDroneById(drone.Id);
        //    drone.Id = newDrone.Id;
        //    drone.Model = newDrone.Model;
        //    drone.Weight = newDrone.Weight;
        //    drone.Battery = newDrone.Battery;

        //    if (!object.Equals(newDrone.ParcelInTransit, null))
        //    {
        //        drone.ParcelInTransit = new PO.ParcelInTransit();
        //        if (!object.Equals(drone.ParcelInTransit.CustomerInParcelReciver, null))
        //        {
        //            drone.ParcelInTransit.CustomerInParcelReciver = new PO.CustomerInParcel();
        //            drone.ParcelInTransit.CustomerInParcelReciver.Id = newDrone.ParcelInTransit.CustomerInParcelReciver.Id;
        //            drone.ParcelInTransit.CustomerInParcelReciver.Name = newDrone.ParcelInTransit.CustomerInParcelReciver.Name;
        //        }
        //        if (!object.Equals(drone.ParcelInTransit.CustomerInParcelReciver, null))
        //        {
        //            drone.ParcelInTransit.CustomerInParcelSender = new PO.CustomerInParcel();
        //            drone.ParcelInTransit.CustomerInParcelSender.Id = newDrone.ParcelInTransit.CustomerInParcelSender.Id;
        //            drone.ParcelInTransit.CustomerInParcelSender.Name = newDrone.ParcelInTransit.CustomerInParcelSender.Name;
        //        }
        //    }

        //    drone.DroneStatuses = newDrone.DroneStatuses;
        //    drone.CurrentLocation = newDrone.CurrentLocation;
        //}
        private void StartSimulator_Click(object sender, RoutedEventArgs e)
        {
           // bl.SimulatorFunc(drone.Id,);
        }

        private void CancelSimulator_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
