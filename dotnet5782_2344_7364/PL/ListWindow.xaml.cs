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
        public ObservableCollection<BO.BaseStationToList> BsCollection= new ObservableCollection<BO.BaseStationToList>();
        public ObservableCollection<BO.ParcelToList> PCollection = new ObservableCollection<BO.ParcelToList>();
        public ObservableCollection<BO.CustomerToList> CCollection = new ObservableCollection<BO.CustomerToList>();
        public ObservableCollection<BO.DroneToList> DCollection = new ObservableCollection<BO.DroneToList>();
        
        public ListWindow()
        {
            InitializeComponent();
            //ListBaseStation.ItemsSource = bl.GetListOfBaseStationsToList().ToList();
            //ListParcel.ItemsSource = bl.GetListOfParcelToList().ToList();
            //ListCustomer.ItemsSource = bl.GetListOfCustomerToList().ToList();
            //ListDrone.ItemsSource = bl.GetListOfDronesToList().ToList();
            Base_station.DataContext = BsCollection = bl.GetListOfBaseStationsToList();
            Parcel_Item.DataContext = PCollection = bl.GetListOfParcelToList();
            Customer_Item.DataContext = CCollection = bl.GetListOfCustomerToList();
            Drone_Item.DataContext = DCollection = bl.GetListOfDronesToList();
            DisplayChargingSlots.Items.Add("All");
            DisplayChargingSlots.Items.Add("Only free charging slots");
            DisplayChargingSlots.SelectedIndex = 0;
            SenderOrReciver.Items.Add("sender");
            SenderOrReciver.Items.Add("reciver");
            Filter1.ItemsSource = Enum.GetValues(typeof(BO.Enums.DroneStatuses));
            Filter2.ItemsSource = Enum.GetValues(typeof(BO.Enums.WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.DroneStatuses));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.WeightCategories));
            for (int i = 1; i < 11; i++)
            {
                NumOfFreeChargingSlots.Items.Add(i);
            }

        }

      
        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(ref DCollection).Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddBaseStation_Click(object sender, RoutedEventArgs e)
        {
            new BaseStationWindow(ref BsCollection).Show();
        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(ref PCollection).Show();
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e) //aa
        {
            new CustomerWindow(ref CCollection).Show();
        }

        private void BaseStation_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            BO.BaseStationToList baseStation = (BO.BaseStationToList)ListBaseStation.SelectedItem;
            BaseStationWindow baseStationWindow = new BaseStationWindow(baseStation.Id, baseStation.OccupiedChargingSlots);
            //droneWindow.Closed += CloseWindow;
            baseStationWindow.Show();
        }

        private void Parcel_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            BO.ParcelToList parcel = (BO.ParcelToList)ListParcel.SelectedItem;
            ParcelWindow parcelWindow = new ParcelWindow(parcel.Id);
            //droneWindow.Closed += CloseWindow;
            parcelWindow.Show();
        }

        private void Drone_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneToList drone = (BO.DroneToList)ListDrone.SelectedItem;
            DroneWindow droneWindow = new DroneWindow(drone.Id); //---------------------------------------
            //droneWindow.Closed += CloseWindow;
            droneWindow.Show();
        }

        private void Customer_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            BO.CustomerToList customer = (BO.CustomerToList)ListCustomer.SelectedItem;
            CustomerWindow customerWindow = new CustomerWindow(customer.Id);
            //droneWindow.Closed += CloseWindow;
            customerWindow.Show();
        }
        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) // combo box to select by weight categories
        {
            if (StatusSelector.SelectedIndex == -1)
            {
                
                ListDrone.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem).ToList();
            }
            else
            {
                ListDrone.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem, predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
            }
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) // combo box to select by status
        {
            if (MaxWeightSelector.SelectedIndex == -1)
            {
                ListDrone.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
            }
            else
            {
                ListDrone.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem, predicate => predicate.DroneStatuses == (BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
            }
        }

        private void DisplayChargingSlots_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
                if (DisplayChargingSlots.SelectedIndex == 0)
                {
                    ListBaseStation.ItemsSource = bl.GetListOfBaseStationsToList();

                }
                else if(DisplayChargingSlots.SelectedIndex == 1)
                {
                    ListBaseStation.ItemsSource = bl.GetListOfFreeChargingStations();

                }

        }

        private void NumOfFreeChargingSlots_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
                ListBaseStation.ItemsSource = bl.GetListOfFreeChargingStations(NumOfFreeChargingSlots.SelectedIndex + 1);
        }

        private void SenderOrReciver_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (SenderOrReciver.SelectedIndex == 0)
            {
                //ListParcel.ItemsSource = bl.GetListOfParcelToList()
            }
        }

        private void Filter2_SelectionChange(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Filter1_SelectionChange(object sender, SelectionChangedEventArgs e)
        {

        }

        private void refreshBaseStation_button(object sender, RoutedEventArgs e)
        {

        }
    }
}
