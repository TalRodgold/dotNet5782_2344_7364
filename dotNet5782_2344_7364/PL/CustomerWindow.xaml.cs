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
        private PL.Model model = PlFactory.GetModel("Model");
        //public BO.Customer customer;
        public PO.Customer customer = new PO.Customer();


        public CustomerWindow()
        {
            InitializeComponent();
            UpdateButton.Visibility = Visibility.Hidden;
            ParcelFromCustomer.IsEnabled = false;
            ParcelToCustomer.IsEnabled = false;
        }
        //public CustomerWindow(ref ObservableCollection<BO.CustomerToList> customerToList)
        //{
        //    InitializeComponent();
        //    UpdateButton.Visibility = Visibility.Hidden;
        //    ParcelFromCustomer.IsEnabled = false;
        //    ParcelToCustomer.IsEnabled = false;
        //    customerCollection = customerToList;
        //}
        public CustomerWindow(int? id)
        {
            lock (bl)
            {
                InitializeComponent();
                AddButton.Visibility = Visibility.Hidden;
                BO.Customer customerBo = bl.GetCustomerById(id);
                customer.Id = customerBo.Id;
                customer.Location.Longitude = customerBo.Location.Longitude;
                customer.Location.Latitude = customerBo.Location.Latitude;
                customer.Name = customerBo.Name;
                customer.ParcelFromCustomer = new List<PO.ParcelAtCustomer>();
                foreach (var item in customerBo.ParcelFromCustomer)
                {
                    PO.ParcelAtCustomer parcelAtCustomer= new PO.ParcelAtCustomer();
                    parcelAtCustomer.Id = item.Id;
                    parcelAtCustomer.ParcelStatus = item.ParcelStatus;
                    parcelAtCustomer.Prioritie = item.Prioritie;
                    parcelAtCustomer.Weight = item.Weight;
                    parcelAtCustomer.CustomerInParcel = new PO.CustomerInParcel();
                    parcelAtCustomer.CustomerInParcel.Id = item.CustomerInParcel.Id;
                    parcelAtCustomer.CustomerInParcel.Name = item.CustomerInParcel.Name;
                    customer.ParcelFromCustomer.Add(parcelAtCustomer);
                } 
                customer.ParcelToCustomer = new List<PO.ParcelAtCustomer>();
                foreach (var item in customer.ParcelToCustomer)
                {
                    PO.ParcelAtCustomer parcelAtCustomer = new PO.ParcelAtCustomer();
                    parcelAtCustomer.Id = item.Id;
                    parcelAtCustomer.ParcelStatus = item.ParcelStatus;
                    parcelAtCustomer.Prioritie = item.Prioritie;
                    parcelAtCustomer.Weight = item.Weight;
                    parcelAtCustomer.CustomerInParcel.Id = item.CustomerInParcel.Id;
                    parcelAtCustomer.CustomerInParcel.Name = item.CustomerInParcel.Name;
                    customer.ParcelToCustomer.Add(parcelAtCustomer);
                }
                customer.Phone = customerBo.Phone;
                MainGrid.DataContext = customer;

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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            lock (bl)
            {
                bl.UpdateCustomer(customer.Id, Name.Text, PhoneNumber.Text);
                PO.CustomerToList customerToList = new PO.CustomerToList();
                customerToList = (from customer_ in model.customers where customer_.Id == customer.Id select customer_).FirstOrDefault();
                if (Name.Text != "")
                {
                    customerToList.Name = Name.Text;
                    customer.Name = Name.Text;
                }
                if (PhoneNumber.Text != "")
                {
                    customerToList.Name = PhoneNumber.Text;
                    customer.Name = PhoneNumber.Text;
                }
                Close(); 
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lock (bl)
                {
                    int? nullableID = int.Parse(Id.Text);
                    BO.Location newLocation = new BO.Location(Convert.ToDouble(Longtitude.Text), Convert.ToDouble(Latitude.Text));
                    BO.Customer customer = new BO.Customer(nullableID, Name.Text, PhoneNumber.Text, newLocation);
                    bl.AddCustomer(customer);
                    BO.CustomerToList customerBo = bl.GetCustomerToListById(nullableID);
                    PO.CustomerToList customerPo = new PO.CustomerToList();
                    customerPo.Id = customerBo.Id;
                    customerPo.Name = customerBo.Name;
                    customerPo.Phone = customerBo.Phone;
                    customerPo.NumberOfParcelsThatSentAndArrived = customerBo.NumberOfParcelsThatSentAndArrived;
                    customerPo.ParcelsOnWayToClient = customerBo.ParcelsOnWayToClient;
                    customerPo.ParcelsRecived = customerBo.ParcelsRecived;
                    customerPo.ParcelsThatSentYetNotArrived = customerBo.ParcelsThatSentYetNotArrived;
                    model.customers.Add(customerPo);

                    MessageBox.Show("customer added sucsecfully");

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
