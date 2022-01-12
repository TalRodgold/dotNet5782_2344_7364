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

    public class Parcel : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("id"); } }
        private CustomerInParcel sender;
        public CustomerInParcel Sender { get { return sender; }  set { sender = value; OnPropertyChanged("sender"); } }
        private CustomerInParcel reciver;
        public CustomerInParcel Reciver { get { return reciver; }  set { reciver = value; OnPropertyChanged("reciver"); } }
        private Enums.WeightCategories weight;
        public Enums.WeightCategories Weight { get { return weight; } set { weight = value; OnPropertyChanged("weight"); } }
        private Enums.Priorities prioritie;
        public Enums.Priorities Prioritie { get { return prioritie; } set { prioritie = value; OnPropertyChanged("prioritie"); }  }
        private DroneInParcel droneInParcel;
        public DroneInParcel DroneInParcel { get { return droneInParcel; } set { droneInParcel = value; OnPropertyChanged("droneInParcel"); }  }
        private DateTime? parcelCreatingTime = null;
        public DateTime? ParcelCreatingTime { get { return parcelCreatingTime; } set { parcelCreatingTime = value; OnPropertyChanged("parcelCreatingTime"); } }
        private DateTime? associationTime = null;
        public DateTime? AssociationTime { get { return associationTime; } set { associationTime = value; OnPropertyChanged("associationTime"); } }
        private DateTime? pickupTime = null;
        public DateTime? PickupTime { get { return  pickupTime; } set { pickupTime = value; OnPropertyChanged("pickupTime"); } }
        private DateTime? deliveryTime = null;
        public DateTime? DeliveryTime { get { return deliveryTime; } set { deliveryTime = value; OnPropertyChanged("deliveryTime"); } } 
        public override string ToString()
        {
            return $" Parcel #{Id}: \n Sender = \n{Sender} \n Reciver = \n{Reciver} \n weight = {Weight} \n Prioritie = {Prioritie} \n Drone in parcel = {DroneInParcel} \n Parcel creating time {ParcelCreatingTime} \n Association time = {AssociationTime} \n Pickup time = {PickupTime} \n Delivery time = {DeliveryTime} \n";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prpertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prpertyName));
            }
        }

    }
}
