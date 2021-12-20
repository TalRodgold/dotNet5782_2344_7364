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
            //AddButton.Visibility = Visibility.Visible; // make butten visible
            //ExitButton.Visibility = Visibility.Hidden;
            // enable erelevent buttons and text boxes
            //UpdateButton.IsEnabled = false;
            Id.IsEnabled = false;
            WeightSelector.IsEnabled = false;
            PrioritieSelector.IsEnabled = false;
            CreatingTime.IsEnabled = false;//disable the textbox to change
            AssociationTime.IsEnabled = false;//disable the textbox to change
            PickupTime.IsEnabled = false;//disable the textbox to change
            DeliveryTime.IsEnabled = false;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.WeightCategories));
            PrioritieSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Priorities));

        }
        public ParcelWindow(BO.Parcel chosenParcel)
        {
            this.parcel = chosenParcel;
            InitializeComponent();
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
            Id.Text = chosenParcel.Id.ToString();
            Sender.Content = chosenParcel.Sender.ToString();
            Reciver.Content = chosenParcel.Reciver.ToString();
            WeightSelector.SelectedItem = chosenParcel.Weight;
            PrioritieSelector.SelectedItem = chosenParcel.Prioritie;
            Drone.Content = chosenParcel.DroneInParcel.ToString();
            CreatingTime.Text = chosenParcel.ParcelCreatingTime.ToString();
            AssociationTime.Text = chosenParcel.AssociationTime.ToString();
            PickupTime.Text = chosenParcel.PickupTime.ToString();
        }

        private void Sender_Click(object sender, RoutedEventArgs e)
        {
            if ((AssociationTime != null) && (DeliveryTime == null))
            {
                new CustomerWindow(parcel.Sender.Id);
            }
        }

        private void Reciver_Click(object sender, RoutedEventArgs e)
        {
            if ((AssociationTime != null) && (DeliveryTime == null))
            {
                new CustomerWindow(parcel.Reciver.Id);
            }
        }

        private void Drone_Click(object sender, RoutedEventArgs e)
        {
            if ((AssociationTime != null) && (DeliveryTime == null))
            {
                new DroneWindow(bl.GetDroneToList(parcel.DroneInParcel.Id));
            }
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PrioritieSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (parcel.AssociationTime != null || parcel.DeliveryTime == null)
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
    }
}
