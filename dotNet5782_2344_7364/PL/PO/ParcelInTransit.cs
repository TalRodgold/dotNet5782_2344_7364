using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BO;

namespace PO
{

    public class ParcelInTransit : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("id"); } }
        private bool status;
        public bool Status { get { return status; } set { status = value; OnPropertyChanged("status"); } } // true = waiting for pickup | false = on transit to location
        private Enums.Priorities priorities;
        public Enums.Priorities Priorities { get { return priorities; } set { priorities = value; OnPropertyChanged("priorities"); } }
        private Enums.WeightCategories weight;
        public Enums.WeightCategories Weight { get { return weight; } set { weight = value; OnPropertyChanged("weight"); } }
        private CustomerInParcel customerInParcelSender;
        public CustomerInParcel CustomerInParcelSender { get { return customerInParcelSender; } set { customerInParcelSender = value; OnPropertyChanged("customerInParcelSender"); }  }
        private CustomerInParcel customerInParcelReciver;
        public CustomerInParcel CustomerInParcelReciver { get { return customerInParcelReciver; } set { customerInParcelReciver = value; OnPropertyChanged("customerInParcelReciver"); }  }
        private Location pickupLocation;
        public Location PickupLocation { get { return pickupLocation; } set { pickupLocation = value; OnPropertyChanged("pickupLocation"); }  }
        private Location deliveryLocation;
        public Location DeliveryLocation { get { return deliveryLocation; } set { deliveryLocation = value; OnPropertyChanged("deliveryLocation"); }  }
        private double distance;
        public double Distance { get { return distance; } set { distance = value; OnPropertyChanged("distance"); }  }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prpertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prpertyName));
            }
        }
        public override string ToString()
        {
            if (Id == null) // if not inisilaized
            {
                return $" Parcel # : \n Prioritie = {Priorities} \n Weight = {Weight} \n Customer in parcel sender = {CustomerInParcelSender} \n Customer in parcel reciver = {CustomerInParcelReciver} \n Pickup location: {PickupLocation.ToString()} \n Delivery location: {DeliveryLocation.ToString()} \n Distance = {Distance} ";

            }
            return $" Parcel #{Id}: \n Prioritie = {Priorities} \n Weight = {Weight} \n Customer in parcel sender = {CustomerInParcelSender} \n Customer in parcel reciver = {CustomerInParcelReciver} \n Pickup location: {PickupLocation.ToString()} \n Delivery location: {DeliveryLocation.ToString()} \n Distance = {Distance} ";
        }
    }
}