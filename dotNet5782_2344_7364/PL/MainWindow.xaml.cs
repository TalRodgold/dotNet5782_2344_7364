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
        private enum enteringOptions // 3 types of weight categories
        { Manager, Exsisting_client, new_client }
        private IBl bl =BlFactory.GetBl("BL");
        public MainWindow()
        {
            InitializeComponent();
            EnteringComboBox.ItemsSource = Enum.GetValues(typeof(enteringOptions));
        }
        private void EnteringOptions_comboBox(object sender, ContextMenuEventArgs e)
        {
            EnteringComboBox.SelectedItem = (enteringOptions)EnteringComboBox.SelectedItem;
            
        }
        private void enterinOption_change(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (EnteringComboBox.SelectedIndex == 0)
            {
                DroneListWindow droneListWindow = new DroneListWindow();
            }
            else if (EnteringComboBox.SelectedIndex == 1)
            {
               CustomerWindow customerWindow = new CustomerWindow(int.Parse(CustomerId.SelectedText));
            }
            else if (EnteringComboBox.SelectedIndex == 2)
            {
               CustomerWindow customerWindow = new CustomerWindow();
            }
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

                    }
                    new CustomerWindow(Int32.Parse(CustomerId.Text)).Show();
                }
                else if (EnteringComboBox.SelectedIndex == 2)
                {
                    new CustomerWindow().Show();
                }
            }
            catch(Exception exception)
            {

            }
        }

        private void EnterIdLabel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
