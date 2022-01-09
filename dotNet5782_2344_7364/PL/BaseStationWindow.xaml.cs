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
        public BO.BaseStation baseStation;
        public int occupied;
        public ListWindow li;

        public BaseStationWindow()
        {
            InitializeComponent();
            UpdateButton.Visibility = Visibility.Hidden;
        }
        public BaseStationWindow(int? id, int oc)
        {
            lock (bl)
            {
                InitializeComponent();
                occupied = oc;
                baseStation = bl.GetBaseStationById(id);
                MainGrid.DataContext = baseStation;
                AddButton.Visibility = Visibility.Hidden;
                Id.IsEnabled = false;
                Longtitude.IsEnabled = false;
                Latitude.IsEnabled = false;
                DronesInCharging.IsEnabled = false;
                List<int?> l = new List<int?>();
                foreach (var item in baseStation.ListOfDroneInCharging)
                {
                    l.Add(item.Id);
                }
                DronesInCharging.ItemsSource = l; 
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                bl.UpdateBaseStation(baseStation.Id, Name.Text, int.Parse(FreeChargingSlots.Text));
                Close(); 
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock (bl)
                {
                    if (occupied > 0)
                    {
                        throw new InvalidOperationException("Base station not empty, there are drones charging at this base station.");
                    }
                    bl.DeleteBaseStation(baseStation.Id); 
                }
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
            droneWindow.Show();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock (bl)
                {
                    int? nullableID = int.Parse(Id.Text);
                    BO.Location newLocation = new BO.Location(Convert.ToDouble(Longtitude.Text), Convert.ToDouble(Latitude.Text));
                    baseStation = new BO.BaseStation(nullableID, Name.Text, newLocation, int.Parse(FreeChargingSlots.Text));
                    bl.AddBaseStation(baseStation);
                    MessageBox.Show("Base station added sucsecfully");
                    Close(); 
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());

            }
          
        }
    }
}
