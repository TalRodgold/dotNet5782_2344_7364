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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        public IBL.BL bl;
        public IBL.BO.DroneToList drone;
        public DroneListWindow droneListWindow;
        public DroneWindow(IBL.BL bl, DroneListWindow listWindow) // constructor for adding new drone
        {
            this.bl = bl;
            this.droneListWindow = listWindow;
            InitializeComponent();
            AddButton.Visibility = Visibility.Visible; // make butten visible
            ExitButton.Visibility = Visibility.Hidden;
            // enable erelevent buttons and text boxes
            UpdateButton.IsEnabled = false; 
            Battery.IsEnabled = false;
            StatusSelector.IsEnabled = false;
            Delivery.IsEnabled = false;
            Latitude.IsEnabled = false;
            Longitude.IsEnabled = false;

            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.Enums.WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.Enums.DroneStatuses));
            StartingBaseStation.Visibility = Visibility.Visible;
            BaseStationTxtBox.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
          
        }
        public DroneWindow(IBL.BL bl, DroneListWindow listWindow, IBL.BO.DroneToList chosenDrone) // constructor for Drone update
        {
            this.bl = bl;
            this.drone = chosenDrone;
            this.droneListWindow = listWindow;
            InitializeComponent();
            UpdateButton.Visibility = Visibility.Visible; // make butten visible
            AddButton.IsEnabled = false; // enable add button
            if (chosenDrone.DroneStatuses == IBL.BO.Enums.DroneStatuses.Available)
            {
                SendDroneToChargeButton.Visibility = Visibility.Visible;
                AccociateDroneToParcelButton.Visibility = Visibility.Visible;
            }
            if (chosenDrone.DroneStatuses == IBL.BO.Enums.DroneStatuses.Maintenance)
            {
                RealesDroneFromChargingButton.Visibility = Visibility.Visible;
            }
            if (chosenDrone.DroneStatuses == IBL.BO.Enums.DroneStatuses.Delivery && bl.GetParcelInTransitById(chosenDrone.NumberOfParcelInTransit).Status )
            {
                PickUpParcelButton.Visibility = Visibility.Visible;
            }
            if (chosenDrone.DroneStatuses == IBL.BO.Enums.DroneStatuses.Delivery && bl.GetParcelById(chosenDrone.NumberOfParcelInTransit).PickupTime != null && bl.GetParcelInTransitById(chosenDrone.NumberOfParcelInTransit).Status)
            {
                DeliverParcelButton.Visibility = Visibility.Visible;
            }
            // enable text boxes where we dont want user to change
            Id.IsEnabled = false;
            Battery.IsEnabled = false;
            MaxWeightSelector.IsEnabled = false;
            StatusSelector.IsEnabled = false;
            Delivery.IsEnabled = false;
            Latitude.IsEnabled = false;
            Longitude.IsEnabled = false;
            // set combo boxes
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.Enums.WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.Enums.DroneStatuses));
            // insert drone data to text box
            Id.Text = chosenDrone.Id.ToString();
            Battery.Text = chosenDrone.Battery.ToString();
            MaxWeightSelector.SelectedItem = chosenDrone.Weight;
            Model.Text = chosenDrone.Model.ToString();
            StatusSelector.SelectedItem = chosenDrone.DroneStatuses;
            Delivery.Text = chosenDrone.NumberOfParcelInTransit.ToString();
            Longitude.Text = chosenDrone.CurrentLocation.LongitudeInSexa();
            Latitude.Text = chosenDrone.CurrentLocation.LatitudeInSexa();     
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MaxWeightSelector.SelectedItem = (IBL.BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem;
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StatusSelector.SelectedItem = (IBL.BO.Enums.DroneStatuses)StatusSelector.SelectedItem;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IBL.BO.Drone newDrone = new IBL.BO.Drone(int.Parse(Id.Text), Model.Text, (IBL.BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem);
                bl.AddDrone(newDrone, Convert.ToInt32(BaseStationTxtBox.Text));
                MessageBox.Show("Drone added sucsecfully");
                droneListWindow.Refresh();
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            droneListWindow.Refresh();
            this.Close();
        }
        private void SendDroneToChargeButton_Click(object sender, RoutedEventArgs e)
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
        private void Refresh()
        {
            //bl.UpdateListOfDronsBL();
            drone = bl.GetDroneToList(drone.Id);
            Battery.Text = drone.Battery.ToString();
            Model.Text = drone.Model.ToString();
            StatusSelector.SelectedItem = drone.DroneStatuses;
            Delivery.Text = drone.NumberOfParcelInTransit.ToString();
            Longitude.Text = drone.CurrentLocation.LongitudeInSexa();
            Latitude.Text = drone.CurrentLocation.LatitudeInSexa();

            if (drone.DroneStatuses == IBL.BO.Enums.DroneStatuses.Available)
            {
                SendDroneToChargeButton.Visibility = Visibility.Visible;
                AccociateDroneToParcelButton.Visibility = Visibility.Visible;
            }
            else
            {
                SendDroneToChargeButton.Visibility = Visibility.Hidden;
                AccociateDroneToParcelButton.Visibility = Visibility.Hidden;
            }
            if (drone.DroneStatuses == IBL.BO.Enums.DroneStatuses.Maintenance)
            {
                RealesDroneFromChargingButton.Visibility = Visibility.Visible;
            }
            else
            {
                RealesDroneFromChargingButton.Visibility = Visibility.Hidden;
            }
            if (drone.DroneStatuses == IBL.BO.Enums.DroneStatuses.Delivery )
            {
                if(bl.GetParcelById(drone.NumberOfParcelInTransit).PickupTime != null && !bl.GetParcelInTransitById(drone.NumberOfParcelInTransit).Status)//true until pickup
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
            //if (drone.DroneStatuses == IBL.BO.Enums.DroneStatuses.Delivery && bl.GetParcelById(drone.NumberOfParcelInTransit).PickupTime != null && bl.GetParcelInTransitById(drone.NumberOfParcelInTransit).Status)
            //{
            //    DeliverParcelButton.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    DeliverParcelButton.Visibility = Visibility.Hidden;

            //}



        }

        private void RealesDroneFromChargingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                bl.UpdateReleseDrone(drone.Id);//_______________________________need to change time (100 is not good)________________________________
                Refresh();
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.ToString());
            }
        }

        private void AccociateDroneToParcelButton_Click(object sender, RoutedEventArgs e)
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

        private void PickUpParcelButton_click(object sender, RoutedEventArgs e)
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

        private void DeliverParcelButton_Click(object sender, RoutedEventArgs e)
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
    }
}
