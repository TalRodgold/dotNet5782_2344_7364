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
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private IBl bl = BlFactory.GetBl("BL");
        public BO.Drone drone;
        BackgroundWorker worker;
        private void updateDrone() => worker.ReportProgress(0);
        private bool checkStop() => worker.CancellationPending;
        public DroneWindow() // constructor for adding new drone
        {
            InitializeComponent();
            System.Diagnostics.Process.Start("explorer.exe", "http://google.com");

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
        public DroneWindow(int? id) // constructor for Drone update
        {
            lock (bl)
            {
                this.drone = bl.GetDroneById(id);
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
                if (drone.DroneStatuses == BO.Enums.DroneStatuses.Delivery && drone.ParcelInTransit.Status)
                {
                    PickUpParcelButton.Visibility = Visibility.Visible;
                }
                if (drone.DroneStatuses == BO.Enums.DroneStatuses.Delivery && bl.GetParcelById((int)drone.ParcelInTransit.Id).PickupTime != null && drone.ParcelInTransit.Status)
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
                    Refresh();  
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
                    bl.UpdateSendDroneToCharge(drone.Id);
                    Refresh(); 
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
                    bl.UpdateReleseDrone(drone.Id);
                    Refresh(); 
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
                    bl.UpdateAssosiateDrone(drone.Id);
                    Refresh(); 
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
                    Refresh(); 
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
                    Refresh(); 
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
                drone = bl.GetDroneById(drone.Id);
                Battery.Value = drone.Battery * 100;
                Model.Text = drone.Model.ToString();
                StatusSelector.SelectedItem = drone.DroneStatuses;
                ParcelInTransit.Text = drone.ParcelInTransit.Id.ToString();
                Longitude.Text = drone.CurrentLocation.LongitudeInSexa();
                Latitude.Text = drone.CurrentLocation.LatitudeInSexa();

                if (drone.DroneStatuses == BO.Enums.DroneStatuses.Available)
                {
                    SendDroneToChargeButton.Visibility = Visibility.Visible;
                    AccociateDroneToParcelButton.Visibility = Visibility.Visible;
                }
                else
                {
                    SendDroneToChargeButton.Visibility = Visibility.Hidden;
                    AccociateDroneToParcelButton.Visibility = Visibility.Hidden;
                }
                if (drone.DroneStatuses == BO.Enums.DroneStatuses.Maintenance)
                {
                    RealesDroneFromChargingButton.Visibility = Visibility.Visible;
                }
                else
                {
                    RealesDroneFromChargingButton.Visibility = Visibility.Hidden;
                }
                if (drone.DroneStatuses == BO.Enums.DroneStatuses.Delivery)
                {
                    if (bl.GetParcelById(drone.ParcelInTransit.Id).DeliveryTime != null && !drone.ParcelInTransit.Status)//(bl.GetParcelById(drone.NumberOfParcelInTransit).DeliveryTime != null && !bl.GetParcelInTransitById(drone.NumberOfParcelInTransit).Status)
                    {
                        DeliverParcelButton.Visibility = Visibility.Hidden;
                        PickUpParcelButton.Visibility = Visibility.Visible;
                    }
                    else if (bl.GetParcelById((int)drone.ParcelInTransit.Id).PickupTime != null && !drone.ParcelInTransit.Status)//true until pickup
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

        private void StartSimulator_Click(object sender, RoutedEventArgs e)
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
        private void updateDroneView()
        {
            drone = bl.GetDroneById(drone.Id);
            DeliverParcelButton.Visibility = Visibility.Hidden;
            SendDroneToChargeButton.Visibility = Visibility.Hidden;
            AccociateDroneToParcelButton.Visibility = Visibility.Hidden;
            RealesDroneFromChargingButton.Visibility = Visibility.Hidden;
            PickUpParcelButton.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;
            if (drone.ParcelInTransit.Id != null)
            {
                BO.ParcelInTransit parcelInTransit = drone.ParcelInTransit;
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
            Battery.Value = drone.Battery * 100;
            Model.Text = drone.Model.ToString();
            StatusSelector.SelectedItem = drone.DroneStatuses;
            ParcelInTransit.Text = drone.ParcelInTransit.Id.ToString();
            Longitude.Text = drone.CurrentLocation.LongitudeInSexa();
            Latitude.Text = drone.CurrentLocation.LatitudeInSexa();
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
