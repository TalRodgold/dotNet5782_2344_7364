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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private IBl bl = BlFactory.GetBl("BL");

        public DroneListWindow() // constructor
        {
            InitializeComponent();
            DroneListView.ItemsSource = bl.GetListOfDronesToList().ToList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.DroneStatuses));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.WeightCategories));
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) // combo box to select by weight categories
        {
            if (StatusSelector.SelectedIndex == -1)
            {
                DroneListView.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem).ToList();
            }
            else
            {
                DroneListView.ItemsSource= bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem, predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
            }
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) // combo box to select by status
        {
            if (MaxWeightSelector.SelectedIndex == -1)
            {
                DroneListView.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
            }
            else
            {
                DroneListView.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem, predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
            }
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e) // add a drone
        {
            ///DroneWindow droneWindow = new DroneWindow();
            //droneWindow.Closed += CloseWindow;
            //droneWindow.Show();

        }

        private void UpdateDrone_doubleClick(object sender, MouseButtonEventArgs e) // ypdate drone
        {

            BO.DroneToList drone = (BO.DroneToList)DroneListView.SelectedItem;
            DroneWindow droneWindow = new DroneWindow(drone.Id);
            droneWindow.Closed += CloseWindow;
            droneWindow.Show();
        }

        private void DroneListView_SelectionChanged(object sender, SelectionChangedEventArgs e) // open drone window
        {
           // new DroneWindow().Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) // exit
        {
            Close();
        }
        public void Refresh() // refresh
        {
            if (StatusSelector.SelectedIndex == -1 && MaxWeightSelector.SelectedIndex != -1)
            {
                DroneListView.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem).ToList();
            }
            else if (MaxWeightSelector.SelectedIndex == -1 && StatusSelector.SelectedIndex != -1)
            {
                DroneListView.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
            }
            else if(StatusSelector.SelectedIndex == -1 && MaxWeightSelector.SelectedIndex == -1)
            {
                DroneListView.ItemsSource = bl.GetListOfDronesToList().ToList();
            }
            else 
            {
                DroneListView.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem, predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
            }
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Refresh();
        }

        private void refreshButton_click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

      

      
    }
}
