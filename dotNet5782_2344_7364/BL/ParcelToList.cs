using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelToList
        {
            public int Id { set; get; }
            public string SendersName { set; get; }
            public string ReciversName { set; get; }
            public Enums.WeightCategories weight { set; get; }
            public Enums.Priorities Prioritie { set; get; }
            public Enums.ParcelStatus ParcelStatus { set; get; }

            public override string ToString()
            {
                return $" Parcel #{Id}: \n Sender = {SendersName} \n Reciver = {ReciversName} \n weight = {weight} \n Prioritie = {Prioritie} \n Parcel status = {ParcelStatus} \n";
            }
        }
    }
}
