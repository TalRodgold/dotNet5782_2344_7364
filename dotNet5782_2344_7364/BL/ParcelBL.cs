using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ParcelBL
        {
            CustomerInParcelBL Sender { set; get; }
            CustomerInParcelBL Reciver { set; get; }
            EnumsBL.WeightCategories weight { set; get; }
            EnumsBL.Priorities Prioritie { set; get; }
            DroneInParcelBL DroneInParcel { set; get; }
            DateTime ParcelCreatingTime { set; get; }
            DateTime AssociationTime { set; get; }
            DateTime PickupTime { set; get; }
            DateTime DeliveryTime { set; get; }
            public override string ToString()
            {
                return $" Sender = {Sender} \n Reciver = {Reciver} \n weight = {weight} \n Prioritie = {Prioritie} \n Drone in parcel = {DroneInParcel} \n Parcel creating time {ParcelCreatingTime} \n Association time = {AssociationTime} \n Pickup time = {PickupTime} \n Delivery time = {DeliveryTime} \n";
            }

        }
    }
    
}
