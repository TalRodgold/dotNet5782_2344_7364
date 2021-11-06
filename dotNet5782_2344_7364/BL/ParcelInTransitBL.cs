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
            IDAL.DO.Priorities Priorities { set; get; }
            IDAL.DO.WeightCategories WeightCategories { set; get; }
            CustomerInParcelBL CustomerInParcelSender { set; get; }
            CustomerInParcelBL CustomerInParcelReciver { set; get; }
            LocationBL PickupLocation { set; get; }
            LocationBL DeliveryLocation { set; get; }
            double Distance { set; get; }

        }
    }
    
}
