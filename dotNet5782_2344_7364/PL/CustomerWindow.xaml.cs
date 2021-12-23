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
        public BO.Customer customer;
        public CustomerWindow()
        {
            InitializeComponent();
        }
        public CustomerWindow(int? id)
        {
            InitializeComponent();
            customer = bl.GetCustomerById(id);
            Id.Text = customer.Id.ToString();
            Name.Text = customer.Name;
            PhoneNumber.Text = customer.Phone.ToString();
            Longtitude.Text = customer.Location.LongitudeInSexa();
            Latitude.Text = customer.Location.LatitudeInSexa();
            List<int?> l = new List<int?>();
            foreach (var item in customer.ParcelFromCustomer)
            {
                l.Add(item.Id);
            }
            ParcelFromCustomer.ItemsSource = l;
            l.Clear();
            foreach (var item in customer.ParcelToCustomer)
            {
                l.Add(item.Id);
            }
            ParcelToCustomer.ItemsSource = l;
        }
    }
}
