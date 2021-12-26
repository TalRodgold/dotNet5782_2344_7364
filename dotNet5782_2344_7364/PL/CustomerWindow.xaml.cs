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
    /// Interaction logic for Customer.xaml
    /// </summary>
    
    public partial class CustomerWindow : Window
    {
        private IBl bl = BlFactory.GetBl("BL");
        public BO.Customer customer;
        ObservableCollection<BO.CustomerToList> customerCollection = new ObservableCollection<BO.CustomerToList>();


        public CustomerWindow()
        {
            InitializeComponent();
            UpdateButton.Visibility = Visibility.Hidden;
            ParcelFromCustomer.IsEnabled = false;
            ParcelToCustomer.IsEnabled = false;
        }
        public CustomerWindow(ref ObservableCollection<BO.CustomerToList> customerToList)
        {
            InitializeComponent();
            UpdateButton.Visibility = Visibility.Hidden;
            ParcelFromCustomer.IsEnabled = false;
            ParcelToCustomer.IsEnabled = false;
            customerCollection = customerToList;
        }
        public CustomerWindow(int? id)
        {
            InitializeComponent();
            AddButton.Visibility = Visibility.Hidden;
            customer = bl.GetCustomerById(id);
            MainGrid.DataContext = customer;

            //Id.Text = customer.Id.ToString();
            //Name.Text = customer.Name;
            //PhoneNumber.Text = customer.Phone.ToString();
            //Longtitude.Text = customer.Location.LongitudeInSexa();
            //Latitude.Text = customer.Location.LatitudeInSexa();
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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateCustomer(customer.Id,Name.Text, PhoneNumber.Text);
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int? nullableID = int.Parse(Id.Text);
                BO.Location newLocation = new BO.Location(Convert.ToDouble(Longtitude.Text), Convert.ToDouble(Latitude.Text));
                customer = new BO.Customer(nullableID, Name.Text, PhoneNumber.Text, newLocation);
                bl.AddCustomer(customer);
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());

            }
           
        }
    }
}
