using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ParcelToListBL
        {
            int Id { set; get; }
            string SendersName { set; get; }
            string ReciversName { set; get; }
            EnumsBL.WeightCategories weight { set; get; }
            EnumsBL.Priorities Prioritie { set; get; }
            EnumsBL.ParcelStatus ParcelStatus { set; get; }

            public override string ToString()
            {
                return $" Parcel #{Id}: \n Sender = {SendersName} \n Reciver = {ReciversName} \n weight = {weight} \n Prioritie = {Prioritie} \n Parcel status = {ParcelStatus} \n";
            }
        }
    }
}
