using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
            public int Id { set; get; }
            public CustomerInParcel Sender { set; get; }
            public CustomerInParcel Reciver { set; get; }
            public Enums.WeightCategories Weight { set; get; }
            public Enums.Priorities Prioritie { set; get; }
            public DroneInParcel DroneInParcel { set; get; }
            public DateTime ParcelCreatingTime { set; get; }
            public DateTime AssociationTime { set; get; }
            public DateTime PickupTime { set; get; }
            public DateTime DeliveryTime { set; get; }
            public override string ToString()
            {
                return $" Parcel #{Id}: \n Sender = \n{Sender} \n Reciver = \n{Reciver} \n weight = {Weight} \n Prioritie = {Prioritie} \n Drone in parcel = {DroneInParcel} \n Parcel creating time {ParcelCreatingTime} \n Association time = {AssociationTime} \n Pickup time = {PickupTime} \n Delivery time = {DeliveryTime} \n";
            }
            public Parcel() { }
           
            public Parcel(int id, CustomerInParcel sender, CustomerInParcel reciver, Enums.WeightCategories weight, Enums.Priorities prioritie, DroneInParcel droneInParcel, DateTime parcelCreatingTime, DateTime associationTime, DateTime pickupTime, DateTime deliveryTime)
            {
                Id = id;
                Sender = sender;
                Reciver = reciver;
                Weight = weight;
                Prioritie = prioritie;
                DroneInParcel = droneInParcel;
                ParcelCreatingTime = parcelCreatingTime;
                AssociationTime = associationTime;
                PickupTime = pickupTime;
                DeliveryTime = deliveryTime;
            }

        }
    }
    
}
