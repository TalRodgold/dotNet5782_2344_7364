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
    /// Interaction logic for BaseStationWindow.xaml
    /// </summary>
    public partial class BaseStationWindow : Window
    {
        private IBl bl = BlFactory.GetBl("BL");
        public BO.BaseStationToList baseStation;
        public BaseStationWindow()
        {
            InitializeComponent();
        }
        public BaseStationWindow(BO.BaseStationToList b)
        {
            InitializeComponent();
            baseStation = b;
            Id.Text = baseStation.Id.ToString();
            Name.Text = baseStation.Name;
            FreeChargingSlots.Text = baseStation.FreeChargingSlots.ToString();
            OccupiedChargingSlots.Text = baseStation.OccupiedChargingSlots.ToString();

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string newName = "";
            int newChargingSlots = 0;
            if (baseStation.Name != Name.Text)
            {
                newName = Name.Text;
            }
            if (baseStation.FreeChargingSlots != int.Parse(FreeChargingSlots.Text))
            {
                newChargingSlots = int.Parse(FreeChargingSlots.Text);
            }
            bl.UpdateBaseStation(baseStation.Id, newName, newChargingSlots);
            baseStation.FreeChargingSlots = bl.GetBaseStationById(baseStation.Id).NumberOfFreeChargingSlots;
            baseStation.Name = bl.GetBaseStationById(baseStation.Id).Name;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (baseStation.OccupiedChargingSlots > 0)
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
    }
}
