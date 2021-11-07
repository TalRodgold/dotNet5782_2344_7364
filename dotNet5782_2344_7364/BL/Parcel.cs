using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
            public CustomerInParcel Sender { set; get; }
            public CustomerInParcel Reciver { set; get; }
            public Enums.WeightCategories weight { set; get; }
            public Enums.Priorities Prioritie { set; get; }
            public DroneInParcel DroneInParcel { set; get; }
            public DateTime ParcelCreatingTime { set; get; }
            public DateTime AssociationTime { set; get; }
            public DateTime PickupTime { set; get; }
            public DateTime DeliveryTime { set; get; }
            public override string ToString()
            {
                return $" Sender = {Sender} \n Reciver = {Reciver} \n weight = {weight} \n Prioritie = {Prioritie} \n Drone in parcel = {DroneInParcel} \n Parcel creating time {ParcelCreatingTime} \n Association time = {AssociationTime} \n Pickup time = {PickupTime} \n Delivery time = {DeliveryTime} \n";
            }

        }
    }
    
}
