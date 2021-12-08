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
        public DroneWindow(IBL.BL bl)
        {
            this.bl = bl;
            InitializeComponent();
        }
        public DroneWindow(IBL.BL bl, IBL.BO.DroneToList drone)
        {
            this.bl = bl;
            InitializeComponent();
            Id.Content = drone.Id.ToString();
            Battery.Content = drone.Battery.ToString();
            Model.Content = drone.Model.ToString();
            Delivery.Content = drone.NumberOfParcelInTransit;
            Longitude.Content = drone.CurrentLocation.Longitude.ToString();
            Latitude.Content = drone.CurrentLocation.Latitude.ToString();

        }
    }
}
