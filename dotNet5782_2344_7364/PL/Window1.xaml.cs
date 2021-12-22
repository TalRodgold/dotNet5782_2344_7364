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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private IBl bl = BlFactory.GetBl("BL");
        public Window1()
        {
            InitializeComponent();
            ListBaseStation.ItemsSource = bl.GetListOfBaseStationsToList().ToList();
            ListParcel.ItemsSource = bl.GetListOfParcelToList().ToList();
            ListCustomer.ItemsSource = bl.GetListOfCustomerToList().ToList();
            ListDrone.ItemsSource = bl.GetListOfDronesToList().ToList();
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
