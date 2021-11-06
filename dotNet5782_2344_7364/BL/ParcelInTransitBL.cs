using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ParcelInTransitBL
        {
            int Id { set; get; }
            EnumsBL.Priorities Priorities { set; get; }
            EnumsBL.WeightCategories Weight { set; get; }
            CustomerInParcelBL CustomerInParcelSender { set; get; }
            CustomerInParcelBL CustomerInParcelReciver { set; get; }
            LocationBL PickupLocation { set; get; }
            LocationBL DeliveryLocation { set; get; }
            double Distance { set; get; }
            public override string ToString()
            {
                return $" Parcel #{Id}: \n Prioritie = {Priorities} \n Weight = {Weight} \n Customer in parcel sender = {CustomerInParcelSender} \n Customer in parcel reciver = {CustomerInParcelReciver} \n Pickup location = {PickupLocation} \n Delivery location = {DeliveryLocation} \n Distance = {Distance} ";
            }

        }
    }
    
}
