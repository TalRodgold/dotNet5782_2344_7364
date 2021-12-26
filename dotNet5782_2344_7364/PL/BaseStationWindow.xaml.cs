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
namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationWindow.xaml
    /// </summary>
    public partial class BaseStationWindow : Window
    {
        private IBl bl = BlFactory.GetBl("BL");
        ObservableCollection<BO.BaseStationToList> baseStationCollection = new ObservableCollection<BO.BaseStationToList>();
        public BO.BaseStation baseStation;
        public int occupied;
        public BaseStationWindow(ref ObservableCollection<BO.BaseStationToList> baseStationToList)
        {
            InitializeComponent();
            baseStationCollection = baseStationToList;
        }
        public BaseStationWindow(int? id, int oc)
        {
            InitializeComponent();
            occupied = oc;
            baseStation = bl.GetBaseStationById(id);
            MainGrid.DataContext = baseStation;

            //Id.Text = baseStation.Id.ToString();
            //Name.Text = baseStation.Name;
            //FreeChargingSlots.Text = baseStation.NumberOfFreeChargingSlots.ToString();
            //Longtitude.Text = baseStation.Location.LongitudeInSexa();
            //Latitude.Text = baseStation.Location.LatitudeInSexa();
            List<int?> l = new List<int?>();
            foreach (var item in baseStation.ListOfDroneInCharging)
            {
                l.Add(item.Id);
            }
            DronesInCharging.ItemsSource = l;

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string newName = "";
            int newChargingSlots = 0;
            if (baseStation.Name != Name.Text)
            {
                newName = Name.Text;
            }
            if (baseStation.NumberOfFreeChargingSlots != int.Parse(FreeChargingSlots.Text))
            {
                newChargingSlots = int.Parse(FreeChargingSlots.Text);
            }
            bl.UpdateBaseStation(baseStation.Id, newName, newChargingSlots);
            baseStation.NumberOfFreeChargingSlots = bl.GetBaseStationById(baseStation.Id).NumberOfFreeChargingSlots;
            baseStation.Name = bl.GetBaseStationById(baseStation.Id).Name;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (occupied > 0)
                {
                    throw new InvalidOperationException("Base station not empty, there are drones charging at this base station.");
                }
                bl.DeleteBaseStation(baseStation.Id);
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.ToString());
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenDroneWindow(object sender, MouseButtonEventArgs e)
        {
            int id = (int)DronesInCharging.SelectedItem;
            DroneWindow droneWindow = new DroneWindow(id);
            //droneWindow.Closed += CloseWindow;
            droneWindow.Show();


        }
    }
}
