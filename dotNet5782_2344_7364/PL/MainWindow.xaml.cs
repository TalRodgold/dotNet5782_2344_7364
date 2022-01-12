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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
  
    public partial class MainWindow : Window
    {
        
       
        private IBl bl =BlFactory.GetBl("BL");
        public MainWindow()
        {
            InitializeComponent();
            EnteringComboBox.Items.Add("Manager");
            EnteringComboBox.Items.Add("Exsisting customer");
            EnteringComboBox.Items.Add("New customer");
            EnteringComboBox.SelectedIndex = 0;
        }
      

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EnteringComboBox.SelectedIndex == 0)
                {
                    new ListWindow().Show();
                }
                else if (EnteringComboBox.SelectedIndex == 1)
                {
                    if(CustomerId.Text.Length==0)
                    {
                        throw new ArgumentException("NO ID ENTERED");
                    }
                    BO.CustomerToList customerBo = bl.GetCustomerToListById(Int32.Parse(CustomerId.Text));
                    PO.CustomerToList customer = new PO.CustomerToList();
                    customer.Id = customerBo.Id;
                    customer.Name = customerBo.Name;
                    customer.NumberOfParcelsThatSentAndArrived = customerBo.NumberOfParcelsThatSentAndArrived;
                    customer.ParcelsOnWayToClient = customerBo.ParcelsOnWayToClient;
                    customer.ParcelsRecived = customerBo.ParcelsRecived;
                    customer.ParcelsThatSentYetNotArrived = customerBo.ParcelsThatSentYetNotArrived;
                    customer.Phone = customerBo.Phone;
                    new CustomerWindow(Int32.Parse(CustomerId.Text)).Show();
                }
                else if (EnteringComboBox.SelectedIndex == 2)
                {
                    new CustomerWindow().Show();
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.ToString());

            }
        }

        private void EnterIdLabel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
