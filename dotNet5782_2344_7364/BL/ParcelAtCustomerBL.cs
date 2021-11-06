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
            EnumsBL.WeightCategories Weight { set; get; }
            EnumsBL.Priorities Prioritie { set; get; }
            EnumsBL.ParcelStatus ParcelStatus { set; get; }
            CustomerInParcelBL customerInParcel { set; get; }
            public override string ToString()
            {
                return $" Parcel #{Id}: \n Weight = {Weight} \n Prioritie = {Prioritie} \n Parcel status = {ParcelStatus} \n Customer in parcel = {customerInParcel} \n ";
            }

        }
    }
   
}
