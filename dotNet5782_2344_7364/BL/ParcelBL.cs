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
            IDAL.DO.WeightCategories weight { set; get; }
            IDAL.DO.Priorities Prioritie { set; get; }
            DroneInParcelBL DroneInParcel { set; get; }
            DateTime ParcelCreatingTime { set; get; }
            DateTime AssociationTime { set; get; }
            DateTime PickupTime { set; get; }
            DateTime DeliveryTime { set; get; }

        }
    }
    
}
