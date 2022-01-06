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
            foreach (var item in bl.GetListOfDronesToList())
            {
                DCollection.Add(item);
            }
            foreach (var item in bl.GetListOfParcelToList())
            {
                PCollection.Add(item);
            }
            foreach (var item in bl.GetListOfCustomerToList())
            {
                CCollection.Add(item);
            }
            foreach (var item in bl.GetListOfBaseStationsToList())
            {

                BsCollection.Add(item);
            }
            Base_station.DataContext = BsCollection;
            Parcel_Item.DataContext = PCollection;
            Customer_Item.DataContext = CCollection;
            Drone_Item.DataContext = DCollection;
        }

      
        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow droneWindow = new DroneWindow();
            droneWindow.AddButton.Click += updateDrones;
            droneWindow.Show();
            
        }
        private void AddBaseStation_Click(object sender, RoutedEventArgs e)
        {
            BaseStationWindow baseStationWindow = new BaseStationWindow();
            baseStationWindow.AddButton.Click += updateStation;
            baseStationWindow.Show();
        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            ParcelWindow parcelWindow = new ParcelWindow();
            parcelWindow.AddButton.Click += updateParcel;
            parcelWindow.Show();
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e) //aa
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.AddButton.Click += updateCustomer;
            customerWindow.Show();
        }
  

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       

        private void BaseStation_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            BO.BaseStationToList baseStation = (BO.BaseStationToList)ListBaseStation.SelectedItem;
            BaseStationWindow baseStationWindow = new BaseStationWindow(baseStation.Id, baseStation.OccupiedChargingSlots);

            baseStationWindow.DeleteButton.Click += updateStation;

            baseStationWindow.UpdateButton.Click  += updateStation;
            baseStationWindow.Show();

        }
        private void Parcel_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            BO.ParcelToList parcel = (BO.ParcelToList)ListParcel.SelectedItem;
            ParcelWindow parcelWindow = new ParcelWindow(parcel.Id);
            parcelWindow.DeleteButton.Click += updateParcel;
            parcelWindow.Show();
        }
        private void Drone_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneToList drone = (BO.DroneToList)ListDrone.SelectedItem;
            DroneWindow droneWindow = new DroneWindow(drone.Id);
            droneWindow.UpdateButton.Click += updateDrones;
            droneWindow.SendDroneToChargeButton.Click += updateDrones;
            droneWindow.RealesDroneFromChargingButton.Click += updateDrones;
            droneWindow.AccociateDroneToParcelButton.Click += updateDrones;
            droneWindow.PickUpParcelButton.Click += updateDrones;
            droneWindow.DeliverParcelButton.Click += updateDrones;
            droneWindow.Show();
           
        }
        private void Customer_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            BO.CustomerToList customer = (BO.CustomerToList)ListCustomer.SelectedItem;
            CustomerWindow customerWindow = new CustomerWindow(customer.Id);
            customerWindow.UpdateButton.Click += updateCustomer;
            customerWindow.Show();
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

        private void GroupByStatus_click(object sender, RoutedEventArgs e)
        {
            var groups = bl.GroupingStatuses();
            List<BO.DroneToList> list = new List<BO.DroneToList>();
            foreach (var item in groups)
            {
                foreach (var innerItem in item)
                {
                    list.Add(innerItem);
                }
            }
            ListDrone.ItemsSource = list.ToList();
        }

        private void GroupByWeight_Click(object sender, RoutedEventArgs e)
        {
            var groups = bl.GroupingWeight();
            List<BO.DroneToList> list = new List<BO.DroneToList>();
            foreach (var item in groups)
            {
                foreach (var innerItem in item)
                {
                    list.Add(innerItem);
                }
            }
            ListDrone.ItemsSource = list.ToList();
        }

        private void refreshDrone_button(object sender, RoutedEventArgs e)
        {
            RefreshDroneFunc();

        }

        private void refreshBaseStation_button(object sender, RoutedEventArgs e)
        {
            RefreshBaseStationFunc();
        }
        private void RefreshDroneFunc()
        {
            DCollection.Clear();
            foreach (var item in bl.GetListOfDronesToList())
            {
                DCollection.Add(item);
            }
            ListDrone.ItemsSource = DCollection.ToList();

        }
        private void RefreshParcelFunc()
        {
            PCollection.Clear();
            foreach (var item in bl.GetListOfParcelToList())
            {
                PCollection.Add(item);
            }
            ListParcel.ItemsSource = PCollection.ToList();
        }
        private void RefreshBaseStationFunc()
        {
            BsCollection.Clear();
            foreach (var item in bl.GetListOfBaseStationsToList())
            {
                BsCollection.Add(item);
            }
            ListBaseStation.ItemsSource = BsCollection.ToList();

        }
        private void RefreshCustomerFunc()
        {
            CCollection.Clear();
            foreach (var item in bl.GetListOfCustomerToList())
            {
                CCollection.Add(item);
            }
            ListCustomer.ItemsSource = CCollection.ToList();

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
            var groups = bl.GroupingFreeChargingSlots();
            List<BO.BaseStationToList> list = new List<BO.BaseStationToList>();
            foreach (var item in groups)
            {
                foreach (var innerItem in item)
                {
                    list.Add(innerItem);
                }
            }
            ListBaseStation.ItemsSource = list.ToList();
        }

        private void GroupBySender_click(object sender, RoutedEventArgs e)
        {
            var groups = bl.GroupingSender();
            List<BO.ParcelToList> list = new List<BO.ParcelToList>();
            foreach (var item in groups)
            {
                foreach (var innerItem in item)
                {
                    list.Add(innerItem);
                }
            }
            ListParcel.ItemsSource = list.ToList();
        }

        private void GroupByReciver_click(object sender, RoutedEventArgs e)
        {
            var groups = bl.GroupingReciver();
            List<BO.ParcelToList> list = new List<BO.ParcelToList>();
            foreach (var item in groups)
            {
                foreach (var innerItem in item)
                {
                    list.Add(innerItem);
                }
            }
            ListParcel.ItemsSource = list.ToList();
        }
    }

}
