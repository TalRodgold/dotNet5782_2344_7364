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
        public DroneWindow(IBL.BL bl)
        {
            this.bl = bl;
            InitializeComponent();

           AddGrid.Visibility = Visibility.Visible;
        }
        public DroneWindow(IBL.BL bl, IBL.BO.DroneToList chosenDrone)
        {
            this.bl = bl;
            this.drone = chosenDrone;
            InitializeComponent();
            UpdateGrid.Visibility = Visibility.Visible;
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.Enums.WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.Enums.DroneStatuses));

            IdL.Content = chosenDrone.Id.ToString();
            BatteryL.Content = chosenDrone.Battery.ToString();
            MaxWeightSelector.SelectedItem = chosenDrone.Weight;
            ModelL.Text = chosenDrone.Model.ToString();
            StatusSelector.SelectedItem = chosenDrone.DroneStatuses;
            DeliveryL.Content = chosenDrone.NumberOfParcelInTransit.ToString();
            LongitudeL.Content = chosenDrone.CurrentLocation.LongitudeInSexa();
            LatitudeL.Content = chosenDrone.CurrentLocation.LatitudeInSexa();
            
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpdateModel(object sender, MouseButtonEventArgs e)
        {
            
            string model = ModelL.Text;
            ModelL.Text = model;

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateDroneModel(drone.Id, ModelL.Text.ToString());
            new DroneListWindow(bl).Show();
            this.Close();
        }
        private void AddId(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
