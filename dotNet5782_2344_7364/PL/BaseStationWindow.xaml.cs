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

        public BaseStationWindow()
        {
            InitializeComponent();
            BaseStationListView.ItemsSource = bl.GetListOfBaseStationsToList().ToList();
            for (int i = 0; i < 10; i++)
            {
                NumOfFreeBSSelector.Items.Add(i);
            }

        }

        private void refreshButton_click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void NumOfFreeBSSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void Refresh() // refresh
        {
        }
    }
}
