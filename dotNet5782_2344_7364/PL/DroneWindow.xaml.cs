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
        public DroneListWindow droneListWindow;
        public DroneWindow(IBL.BL bl, DroneListWindow listWindow) // constructor for adding new drone
        {
            this.bl = bl;
            this.droneListWindow = listWindow;
            InitializeComponent();
            AddButton.Visibility = Visibility.Visible; // make butten visible
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
            
            this.droneListWindow = listWindow;
            InitializeComponent();
            UpdateButton.Visibility = Visibility.Visible; // make butten visible
            AddButton.IsEnabled = false; // enable add button
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
                bl.UpdateDroneModel(int.Parse(Id.Text), Model.Text.ToString());
                droneListWindow.Refresh();
                this.Close();
              
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
    }
}
