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
    /// Interaction logic for ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        private IBl bl = BlFactory.GetBl("BL");
        public ListWindow()
        {
            InitializeComponent();
            ListBaseStation.ItemsSource = bl.GetListOfBaseStationsToList().ToList();
            ListParcel.ItemsSource = bl.GetListOfParcelToList().ToList();
            ListCustomer.ItemsSource = bl.GetListOfCustomerToList().ToList();
            ListDrone.ItemsSource = bl.GetListOfDronesToList().ToList();
        }


        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow().Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddBaseStation_Click(object sender, RoutedEventArgs e)
        {
            new BaseStationWindow().Show();
        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow().Show();
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e) //
        {
            new CustomerWindow().Show();
        }

        private void BaseStation_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            BO.BaseStationToList baseStation = (BO.BaseStationToList)ListBaseStation.SelectedItem;
            BaseStationWindow baseStationWindow = new BaseStationWindow(baseStation);
            //droneWindow.Closed += CloseWindow;
            baseStationWindow.Show();
        }

        private void Parcel_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            BO.ParcelToList parcel = (BO.ParcelToList)ListParcel.SelectedItem;
            ParcelWindow parcelWindow = new ParcelWindow(parcel);
            //droneWindow.Closed += CloseWindow;
            parcelWindow.Show();
        }

        private void Drone_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneToList drone = (BO.DroneToList)ListDrone.SelectedItem;
            DroneWindow droneWindow = new DroneWindow(drone);
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
    }
}
