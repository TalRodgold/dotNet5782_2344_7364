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
using System.ComponentModel;

using BlApi;
namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationWindow.xaml
    /// </summary>
    public partial class BaseStationWindow : Window ,INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyCanged;
        private IBl bl = BlFactory.GetBl("BL");
        private PL.Model model = PlFactory.GetModel("Model");
        //public static Model model { get; } = Model.Instance;
        public PO.BaseStation baseStation=new PO.BaseStation();
        public int occupied;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseStationWindow( ListWindow list)
        {
            InitializeComponent();
            UpdateButton.Visibility = Visibility.Hidden;
        }
        public BaseStationWindow(int? id, int oc)//, PO.BaseStationToList baseStationT
        {
            lock (bl)
            {
                InitializeComponent();
                occupied = oc;
                BO.BaseStation newBaseStation = bl.GetBaseStationById(id);
                baseStation.Id = newBaseStation.Id;
                baseStation.Name = newBaseStation.Name;
                baseStation.Location.Longitude = newBaseStation.Location.Longitude ;
                baseStation.Location.Latitude = newBaseStation.Location.Latitude;
                baseStation.NumberOfFreeChargingSlots = newBaseStation.NumberOfFreeChargingSlots;
                baseStation.ListOfDroneInCharging = newBaseStation.ListOfDroneInCharging;
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
                try
                {
                    bl.UpdateBaseStation(baseStation.Id, Name.Text, int.Parse(FreeChargingSlots.Text));
                    PO.BaseStationToList baseStationToList = new PO.BaseStationToList();
                    baseStationToList = (from station in model.baseStations where station.Id == baseStation.Id select station).FirstOrDefault();
                    if (Name.Text != "")
                    {
                        baseStationToList.Name = Name.Text;
                        baseStation.Name = Name.Text;
                    }
                    if (int.Parse(FreeChargingSlots.Text) != 0)
                    {
                        baseStationToList.FreeChargingSlots = int.Parse(FreeChargingSlots.Text);
                        baseStation.NumberOfFreeChargingSlots = int.Parse(FreeChargingSlots.Text);
                    }
                    Close();
                }
                catch (Exception exception)
                {

                    MessageBox.Show(exception.ToString());
                }
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
                    model.baseStations.Remove((from station in model.baseStations where station.Id == baseStation.Id select station).FirstOrDefault());
                    
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
                    BO.BaseStation newBaseStation = new BO.BaseStation(nullableID, Name.Text, newLocation, int.Parse(FreeChargingSlots.Text));
                    BO.BaseStationToList baseStation1= bl.convertBasestationToBasestationTolist(newBaseStation);
                        PO.BaseStationToList baseStation = new PO.BaseStationToList();
                        baseStation.Id = baseStation1.Id;
                        baseStation.Name = baseStation1.Name;
                        baseStation.OccupiedChargingSlots = baseStation1.OccupiedChargingSlots;
                        baseStation.FreeChargingSlots = baseStation1.FreeChargingSlots;
                        model.BaseStations.Add(baseStation);
                    bl.AddBaseStation(newBaseStation);
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
