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
    /// Interaction logic for Customer.xaml
    /// </summary>
    
    public partial class CustomerWindow : Window
    {
        private IBl bl = BlFactory.GetBl("BL");
        public BO.CustomerToList customer;
        public CustomerWindow()
        {
            InitializeComponent();
        }
        public CustomerWindow(int id)
        {
            InitializeComponent();
            customer = bl.GetCustomerToListById(id);
            Id.Text = customer.Id.ToString();
            Name.Text = customer.Name;
            PhoneNumber.Text = customer.Phone.ToString();
            ParcelsThatWhereSentAndArrived.Text = customer.NumberOfParcelsThatSentAndArrived.ToString();
            ParcelsThatWhereSentYetNotArrived.Text = customer.ParcelsThatSentYetNotArrived.ToString();
            ParcelsRecived.Text = customer.ParcelsRecived.ToString();
            ParcelsOnWayToClient.Text = customer.ParcelsOnWayToClient.ToString();
        }
    }
}
