﻿using System;
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
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window,INotifyPropertyChanged
    {
        private IBl bl = BlFactory.GetBl("BL");
        private PL.Model model = PlFactory.GetModel("Model");

        private void updateDrone() => worker.ReportProgress(0);
        private bool checkStop() => worker.CancellationPending;
        BackgroundWorker worker;
        public PO.Drone drone = new PO.Drone();
        public event PropertyChangedEventHandler PropertyChanged;
        public DroneWindow() // constructor for adding new drone
        {
            InitializeComponent();
            AddButton.Visibility = Visibility.Visible; // make butten visible
            ExitButton.Visibility = Visibility.Hidden;
            StartSimulatorButton.Visibility = Visibility.Hidden;
            CancelSimulatorButton.Visibility = Visibility.Hidden;
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
        public DroneWindow(int? id) // constructor for Drone update
        {
            lock (bl)
            {
                BO.Drone newDrone = bl.GetDroneById(id);
                drone.Id = newDrone.Id;
                drone.Model = newDrone.Model;
                drone.Weight = newDrone.Weight;
                drone.Battery = newDrone.Battery;

                if (newDrone.ParcelInTransit.Id != null)
                {
                    drone.ParcelInTransit.Id = newDrone.ParcelInTransit.Id;
                    drone.ParcelInTransit.PickupLocation = new PO.Location();
                    drone.ParcelInTransit.PickupLocation.Latitude = newDrone.ParcelInTransit.PickupLocation.Latitude;//
                    drone.ParcelInTransit.PickupLocation.Longitude = newDrone.ParcelInTransit.PickupLocation.Longitude;//
                    drone.ParcelInTransit.Priorities = newDrone.ParcelInTransit.Priorities;
                    drone.ParcelInTransit.DeliveryLocation = new PO.Location();
                    drone.ParcelInTransit.DeliveryLocation.Latitude = newDrone.ParcelInTransit.DeliveryLocation.Latitude;
                    drone.ParcelInTransit.DeliveryLocation.Longitude = newDrone.ParcelInTransit.DeliveryLocation.Longitude;
                    drone.ParcelInTransit.Distance = newDrone.ParcelInTransit.Distance;
                    drone.ParcelInTransit.Status = newDrone.ParcelInTransit.Status;
                    drone.ParcelInTransit.Weight = newDrone.ParcelInTransit.Weight;
                    if (!object.Equals(drone.ParcelInTransit.CustomerInParcelReciver, null))
                    {
                        drone.ParcelInTransit.CustomerInParcelReciver = new PO.CustomerInParcel();
                        drone.ParcelInTransit.CustomerInParcelReciver.Id = newDrone.ParcelInTransit.CustomerInParcelReciver.Id;
                        drone.ParcelInTransit.CustomerInParcelReciver.Name = newDrone.ParcelInTransit.CustomerInParcelReciver.Name;
                    }
                    if (!object.Equals(drone.ParcelInTransit.CustomerInParcelSender, null))
                    {
                        drone.ParcelInTransit.CustomerInParcelSender = new PO.CustomerInParcel();
                        drone.ParcelInTransit.CustomerInParcelSender.Id = newDrone.ParcelInTransit.CustomerInParcelSender.Id;
                        drone.ParcelInTransit.CustomerInParcelSender.Name = newDrone.ParcelInTransit.CustomerInParcelSender.Name;
                    }
                }
                else
                {
                    drone.ParcelInTransit = new PO.ParcelInTransit();
                }

                drone.DroneStatuses = newDrone.DroneStatuses;
                drone.CurrentLocation = new PO.Location();
                drone.CurrentLocation.Latitude = newDrone.CurrentLocation.Latitude;
                drone.CurrentLocation.Longitude = newDrone.CurrentLocation.Longitude;
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
                    drone.CurrentLocation = new PO.Location();
                    dronePo.CurrentLocation.Longitude = droneBo.CurrentLocation.Longitude;
                    dronePo.CurrentLocation.Latitude = droneBo.CurrentLocation.Latitude;

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
                    int stationId = (int)bl.UpdateSendDroneToCharge(drone.Id);
                    RefreshAll(); 
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
                    int stationId = (int)bl.UpdateReleseDrone(drone.Id);
                   RefreshAll(); 
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
                    int parcelId = (int)bl.UpdateAssosiateDrone(drone.Id);
                      RefreshAll(); 
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
                    RefreshAll(); 
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
                    RefreshAll(); 
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
                Model.Text = drone.Model;
                StatusSelector.SelectedItem = drone.DroneStatuses;
                ParcelInTransit.Text = newDrone.ParcelInTransit.Id.ToString();
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
        private void RefreshAll()

        {
            lock (bl)
            {
                model.drones.Clear();
                model.baseStations.Clear();
                model.customers.Clear();
                model.parcels.Clear();
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
                    model.drones.Add(drone);
                }
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
                BO.Drone newDrone = bl.GetDroneById(drone.Id);
                drone.Id = newDrone.Id;
                drone.Model = newDrone.Model;
                drone.Weight = newDrone.Weight;
                drone.Battery = newDrone.Battery;

                if (newDrone.ParcelInTransit.Id != null)
                {
                    drone.ParcelInTransit.Id = newDrone.ParcelInTransit.Id;
                    drone.ParcelInTransit.PickupLocation = new PO.Location();
                    drone.ParcelInTransit.PickupLocation.Longitude = newDrone.ParcelInTransit.PickupLocation.Longitude;
                    drone.ParcelInTransit.PickupLocation.Latitude = newDrone.ParcelInTransit.PickupLocation.Latitude;
                    drone.ParcelInTransit.Priorities = newDrone.ParcelInTransit.Priorities;
                    drone.ParcelInTransit.DeliveryLocation = new PO.Location();
                    drone.ParcelInTransit.DeliveryLocation.Latitude = newDrone.ParcelInTransit.DeliveryLocation.Latitude;
                    drone.ParcelInTransit.DeliveryLocation.Longitude = newDrone.ParcelInTransit.DeliveryLocation.Longitude;
                    drone.ParcelInTransit.Distance = newDrone.ParcelInTransit.Distance;
                    drone.ParcelInTransit.Status = newDrone.ParcelInTransit.Status;
                    drone.ParcelInTransit.Weight = newDrone.ParcelInTransit.Weight;
                    if (!object.Equals(drone.ParcelInTransit.CustomerInParcelReciver, null))
                    {
                        drone.ParcelInTransit.CustomerInParcelReciver = new PO.CustomerInParcel();
                        drone.ParcelInTransit.CustomerInParcelReciver.Id = newDrone.ParcelInTransit.CustomerInParcelReciver.Id;
                        drone.ParcelInTransit.CustomerInParcelReciver.Name = newDrone.ParcelInTransit.CustomerInParcelReciver.Name;
                    }
                    if (!object.Equals(drone.ParcelInTransit.CustomerInParcelSender, null))
                    {
                        drone.ParcelInTransit.CustomerInParcelSender = new PO.CustomerInParcel();
                        drone.ParcelInTransit.CustomerInParcelSender.Id = newDrone.ParcelInTransit.CustomerInParcelSender.Id;
                        drone.ParcelInTransit.CustomerInParcelSender.Name = newDrone.ParcelInTransit.CustomerInParcelSender.Name;
                    }
                }
                else
                {
                    drone.ParcelInTransit = new PO.ParcelInTransit();
                }
                drone.DroneStatuses = newDrone.DroneStatuses;
                drone.CurrentLocation = new PO.Location();
                drone.CurrentLocation.Latitude = newDrone.CurrentLocation.Latitude;
                drone.CurrentLocation.Longitude = newDrone.CurrentLocation.Longitude;
                Model.Text = drone.Model;
                StatusSelector.SelectedItem = drone.DroneStatuses;
                ParcelInTransit.Text = newDrone.ParcelInTransit.Id.ToString();
                Longitude.Text = drone.CurrentLocation.LongitudeInSexa();
                Latitude.Text = drone.CurrentLocation.LatitudeInSexa();

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


                drone.DroneStatuses = newDrone.DroneStatuses;
                drone.CurrentLocation = new PO.Location();
                drone.CurrentLocation.Latitude = newDrone.CurrentLocation.Latitude;
                drone.CurrentLocation.Longitude = newDrone.CurrentLocation.Longitude;

            }
        }

        private void StartSimulator_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {

                DeliverParcelButton.Visibility = Visibility.Hidden;
                SendDroneToChargeButton.Visibility = Visibility.Hidden;
                AccociateDroneToParcelButton.Visibility = Visibility.Hidden;
                RealesDroneFromChargingButton.Visibility = Visibility.Hidden;
                PickUpParcelButton.Visibility = Visibility.Hidden;
                UpdateButton.Visibility = Visibility.Hidden;

                worker = new()
                { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
                worker.DoWork += (sender, args) => bl.StartSimulator((int)args.Argument, updateDrone, checkStop);
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.ProgressChanged += (sender, args) => updateDroneView();
                worker.RunWorkerAsync(drone.Id); 
            }
        }
        private void updateDroneView()
        {
            lock (bl)
            {
                BO.Drone newDrone = bl.GetDroneById(drone.Id);
                drone.Id = newDrone.Id;
                drone.Model = newDrone.Model;
                drone.Weight = newDrone.Weight;
                drone.Battery = newDrone.Battery;

                if (newDrone.ParcelInTransit.Id != null)
                {
                    drone.ParcelInTransit.Id = newDrone.ParcelInTransit.Id;
                    drone.ParcelInTransit.PickupLocation = new PO.Location();
                    drone.ParcelInTransit.PickupLocation.Longitude = newDrone.ParcelInTransit.PickupLocation.Longitude;
                    drone.ParcelInTransit.PickupLocation.Latitude = newDrone.ParcelInTransit.PickupLocation.Latitude;
                    drone.ParcelInTransit.Priorities = newDrone.ParcelInTransit.Priorities;
                    drone.ParcelInTransit.DeliveryLocation = new PO.Location();
                    drone.ParcelInTransit.DeliveryLocation.Latitude = newDrone.ParcelInTransit.DeliveryLocation.Latitude;
                    drone.ParcelInTransit.DeliveryLocation.Longitude = newDrone.ParcelInTransit.DeliveryLocation.Longitude;
                    drone.ParcelInTransit.Distance = newDrone.ParcelInTransit.Distance;
                    drone.ParcelInTransit.Status = newDrone.ParcelInTransit.Status;
                    drone.ParcelInTransit.Weight = newDrone.ParcelInTransit.Weight;
                    if (!object.Equals(newDrone.ParcelInTransit.CustomerInParcelReciver, null))
                    {
                        drone.ParcelInTransit.CustomerInParcelReciver = new PO.CustomerInParcel();
                        drone.ParcelInTransit.CustomerInParcelReciver.Id = newDrone.ParcelInTransit.CustomerInParcelReciver.Id;
                        drone.ParcelInTransit.CustomerInParcelReciver.Name = newDrone.ParcelInTransit.CustomerInParcelReciver.Name;
                    }
                    if (!object.Equals(newDrone.ParcelInTransit.CustomerInParcelSender, null))
                    {
                        drone.ParcelInTransit.CustomerInParcelSender = new PO.CustomerInParcel();
                        drone.ParcelInTransit.CustomerInParcelSender.Id = newDrone.ParcelInTransit.CustomerInParcelSender.Id;
                        drone.ParcelInTransit.CustomerInParcelSender.Name = newDrone.ParcelInTransit.CustomerInParcelSender.Name;
                    }
                }
                else
                {
                    drone.ParcelInTransit = new PO.ParcelInTransit();
                }
                drone.DroneStatuses = newDrone.DroneStatuses;
                drone.CurrentLocation.Longitude = newDrone.CurrentLocation.Longitude;
                drone.CurrentLocation.Latitude = newDrone.CurrentLocation.Latitude;

                if (drone.ParcelInTransit.Id != null)
                {
                    PO.ParcelInTransit parcelInTransit = drone.ParcelInTransit;
                    ParcelHeadLabel.Visibility = Visibility.Visible;
                    ParcelIdLabel.Visibility = Visibility.Visible;
                    ParcelPriorityLabel.Visibility = Visibility.Visible;
                    ParcelWeightLabel.Visibility = Visibility.Visible;
                    ParcelSenderLabel.Visibility = Visibility.Visible;
                    ParcelReciverLabel.Visibility = Visibility.Visible;
                    ParcelDistanceLabel.Visibility = Visibility.Visible;
                    ParcelId.Visibility = Visibility.Visible;
                    ParcelPriority.Visibility = Visibility.Visible;
                    ParcelWeight.Visibility = Visibility.Visible;
                    ParcelSender.Visibility = Visibility.Visible;
                    ParcelReciver.Visibility = Visibility.Visible;
                    ParcelDistance.Visibility = Visibility.Visible;
                    ParcelId.Text = parcelInTransit.Id.ToString();
                    ParcelPriority.Text = parcelInTransit.Priorities.ToString();
                    ParcelWeight.Text = parcelInTransit.Weight.ToString();
                    ParcelSender.Text = parcelInTransit.CustomerInParcelSender.ToString();
                    ParcelReciver.Text = parcelInTransit.CustomerInParcelReciver.ToString();
                    ParcelDistance.Text = parcelInTransit.Distance.ToString();
                }
                else
                {
                    ParcelHeadLabel.Visibility = Visibility.Hidden;
                    ParcelIdLabel.Visibility = Visibility.Hidden;
                    ParcelPriorityLabel.Visibility = Visibility.Hidden;
                    ParcelWeightLabel.Visibility = Visibility.Hidden;
                    ParcelSenderLabel.Visibility = Visibility.Hidden;
                    ParcelReciverLabel.Visibility = Visibility.Hidden;
                    ParcelDistanceLabel.Visibility = Visibility.Hidden;
                    ParcelId.Visibility = Visibility.Hidden;
                    ParcelPriority.Visibility = Visibility.Hidden;
                    ParcelWeight.Visibility = Visibility.Hidden;
                    ParcelSender.Visibility = Visibility.Hidden;
                    ParcelReciver.Visibility = Visibility.Hidden;
                    ParcelDistance.Visibility = Visibility.Hidden;
                }
            }
   
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Simulation terminated sucssefully");

        }
        private void CancelSimulator_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }
    }
}