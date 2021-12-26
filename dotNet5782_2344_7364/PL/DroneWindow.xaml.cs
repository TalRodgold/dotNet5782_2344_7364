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
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private IBl bl = BlFactory.GetBl("BL");
        public BO.Drone drone;
        ObservableCollection<BO.DroneToList> drones = new ObservableCollection<BO.DroneToList>();
        public DroneWindow(ref ObservableCollection<BO.DroneToList>droneToLists ) // constructor for adding new drone
        {
            InitializeComponent();
            drones = droneToLists;
            AddButton.Visibility = Visibility.Visible; // make butten visible
            ExitButton.Visibility = Visibility.Hidden;
            // enable erelevent buttons and text boxes
            UpdateButton.IsEnabled = false; 
            Battery.IsEnabled = false;
            StatusSelector.IsEnabled = false;
          //  ParcelInTransit.IsEnabled = false;
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
            this.drone = bl.GetDroneById(id);
            InitializeComponent();

            MainGrid.DataContext = drone;

            UpdateButton.Visibility = Visibility.Visible; // make butten visible
            AddButton.IsEnabled = false; // enable add button
            if (drone.DroneStatuses ==BO.Enums.DroneStatuses.Available)
            {
                SendDroneToChargeButton.Visibility = Visibility.Visible;
                AccociateDroneToParcelButton.Visibility = Visibility.Visible;
            }
            if (drone.DroneStatuses == BO.Enums.DroneStatuses.Maintenance)
            {
                RealesDroneFromChargingButton.Visibility = Visibility.Visible;
            }
            if (drone.DroneStatuses == BO.Enums.DroneStatuses.Delivery && drone.ParcelInTransit.Status )
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
            // insert drone data to text box
            //Id.Text = chosenDrone.Id.ToString();
            //Battery.Value = drone.Battery;
           // MaxWeightSelector.SelectedItem = drone.Weight;
            //Model.Text = chosenDrone.Model.ToString();
            //StatusSelector.SelectedItem = drone.DroneStatuses;
            //ParcelInTransit.Text = drone.ParcelInTransit.Id.ToString();
            //Longitude.Text = drone.CurrentLocation.LongitudeInSexa();
            //Latitude.Text = drone.CurrentLocation.LatitudeInSexa();     
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
                BO.Drone newDrone = new BO.Drone(int.Parse(Id.Text), Model.Text, (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem);
                bl.AddDrone(newDrone, Convert.ToInt32(BaseStationTxtBox.Text));
                drones.Add(bl.convertDroneBlToList( newDrone));
                MessageBox.Show("Drone added sucsecfully");
                this.Close();
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
                bl.UpdateDroneModel(drone.Id, Model.Text.ToString());
                Refresh(); 
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
                bl.UpdateSendDroneToCharge(drone.Id);
                Refresh();
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

                bl.UpdateReleseDrone(drone.Id);
                Refresh();
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
                bl.UpdateAssosiateDrone(drone.Id);
                Refresh();
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
                bl.PickupParcelByDrone(drone.Id);
                Refresh();
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
                bl.DeliveryParcelByDrone(drone.Id);
                Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
        private void Refresh() // refresh
        {
            drone = bl.GetDroneById(drone.Id);
            //Battery.Value = drone.Battery;
            //Model.Text = drone.Model.ToString();
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
            if (drone.DroneStatuses ==BO.Enums.DroneStatuses.Maintenance)
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
}
