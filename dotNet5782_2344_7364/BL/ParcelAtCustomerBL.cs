using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ParcelAtCustomerBL
        {
            int Id { set; get; }
            IDAL.DO.WeightCategories Weight { set; get; }
            IDAL.DO.Priorities Prioritie { set; get; }
            IDAL.DO.ParcelStatus ParcelStatus { set; get; }
            CustomerInParcelBL customerInParcel { set; get; }

        }
    }
   
}
