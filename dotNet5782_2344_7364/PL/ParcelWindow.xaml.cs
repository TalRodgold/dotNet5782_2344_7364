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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        private IBl bl = BlFactory.GetBl("BL");
        public BO.Parcel parcel;
        public ParcelWindow() // constructor for adding new drone
        {
            InitializeComponent();
            AddButton.Visibility = Visibility.Visible; // make butten visible
            CancelButton.Visibility = Visibility.Visible;

            //ExitButton.Visibility = Visibility.Hidden;
            // enable erelevent buttons and text boxes
            //UpdateButton.IsEnabled = false;

            Id.IsEnabled = true;
            SenderB.Visibility = Visibility.Hidden;//button
            ReciverB.Visibility = Visibility.Hidden;//button
            senderT.Visibility = Visibility.Visible;
            reciverT.Visibility = Visibility.Visible;
            SenderB.IsEnabled = false;
            ReciverB.IsEnabled = false;
            senderT.IsEnabled = true;
            reciverT.IsEnabled = true;
            WeightSelector.IsEnabled = true;
            PrioritieSelector.IsEnabled = true;
            CreatingTime.IsEnabled = false;//disable the textbox to change
            AssociationTime.IsEnabled = false;//disable the textbox to change
            PickupTime.IsEnabled = false;//disable the textbox to change
            DeliveryTime.IsEnabled = false;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.WeightCategories));
            PrioritieSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Priorities));

        }
        public ParcelWindow(int? chosenParcel)
        {
            
            this.parcel = bl.GetParcelById(chosenParcel);
            InitializeComponent();
            MainGrid.DataContext = parcel;
           // UpdateButton.Visibility = Visibility.Visible; // make butten visible
            //AddButton.IsEnabled = false; // enable add button
            // enable text boxes where we dont want user to change
            Id.IsEnabled = false;//disable the textbox to change
            WeightSelector.IsEnabled = false;//disable the combobox to change
            PrioritieSelector.IsEnabled = false;//disable the combobox to change
            CreatingTime.IsEnabled = false;//disable the textbox to change
            AssociationTime.IsEnabled = false;//disable the textbox to change
            PickupTime.IsEnabled = false;//disable the textbox to change
            DeliveryTime.IsEnabled = false;

            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.WeightCategories));
            PrioritieSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Priorities));
            //Id.Text = parcel.Id.ToString();
            //SenderB.Content = parcel.Sender.Id;
            //ReciverB.Content = parcel.Reciver.Id;
            //WeightSelector.SelectedItem = parcel.Weight;
            //PrioritieSelector.SelectedItem = parcel.Prioritie;
            //if(parcel.DroneInParcel!=null)
            //{
            //    Drone.Content = parcel.DroneInParcel.Id;
            //}
            //CreatingTime.Text = parcel.ParcelCreatingTime.ToString();
            //AssociationTime.Text = parcel.AssociationTime.ToString();
            //PickupTime.Text = parcel.PickupTime.ToString();
            //DeliveryTime.Text = parcel.DeliveryTime.ToString();
        }

        private void Sender_Click(object sender, RoutedEventArgs e)
        {
            if ((AssociationTime != null) && (DeliveryTime.Text.Length == 0))
            {
                new CustomerWindow(parcel.Sender.Id).Show();
            }
        }

        private void Reciver_Click(object sender, RoutedEventArgs e)
        {
            if ((AssociationTime != null) && (DeliveryTime.Text.Length == 0))
            {
                new CustomerWindow(parcel.Reciver.Id).Show();
            }
        }

        private void Drone_Click(object sender, RoutedEventArgs e)
        {
            if ((AssociationTime != null) && (DeliveryTime.Text.Length == 0))
            {
                new DroneWindow(bl.GetDroneById(parcel.DroneInParcel.Id).Id).Show();
            }
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WeightSelector.SelectedItem = (BO.Enums.WeightCategories)WeightSelector.SelectedItem;
        }

        private void PrioritieSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PrioritieSelector.SelectedItem = (BO.Enums.Priorities)PrioritieSelector.SelectedItem;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (parcel.AssociationTime != null && parcel.DeliveryTime == null)
                {
                    throw new InvalidOperationException("The parcel in delivery proces");
                }
                bl.DeleteParcel(parcel.Id);
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddParcel(new BO.CustomerInParcel(int.Parse(senderT.Text), bl.GetCustomerById(int.Parse(senderT.Text)).Name), new BO.CustomerInParcel(int.Parse(reciverT.Text), bl.GetCustomerById(int.Parse(reciverT.Text)).Name), (BO.Enums.WeightCategories)WeightSelector.SelectedItem, (BO.Enums.Priorities)PrioritieSelector.SelectedItem);
                MessageBox.Show("Drone added sucsecfully");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
