using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// class for parcel to list
        /// </summary>
        public class ParcelToList
        {
            public int Id { set; get; }
            public string SendersName { set; get; }
            public string ReciversName { set; get; }
            public Enums.WeightCategories Weight { set; get; }
            public Enums.Priorities Prioritie { set; get; }
            public Enums.ParcelStatus ParcelStatus { set; get; }

            public ParcelToList(int id, string sendersName, string reciversName, Enums.WeightCategories weight, Enums.Priorities prioritie, Enums.ParcelStatus parcelStatus) // constructor
            {
                Id = id;
                SendersName = sendersName;
                ReciversName = reciversName;
                Weight = weight;
                Prioritie = prioritie;
                ParcelStatus = parcelStatus;
            }
            public override string ToString()
            {
                return $" Parcel #{Id}: \n Sender = {SendersName} \n Reciver = {ReciversName} \n weight = {Weight} \n Prioritie = {Prioritie} \n Parcel status = {ParcelStatus} \n";
            } 
        }
    }
}
