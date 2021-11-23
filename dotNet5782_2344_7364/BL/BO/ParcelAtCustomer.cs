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
        /// class for parcel at customer
        /// </summary>
        public class ParcelAtCustomer
        {
            public int Id { set; get; }
            public Enums.WeightCategories Weight { set; get; }
            public Enums.Priorities Prioritie { set; get; }
            public Enums.ParcelStatus ParcelStatus { set; get; }
            public CustomerInParcel CustomerInParcel { set; get; }
            public ParcelAtCustomer( int id, Enums.WeightCategories weight, Enums.Priorities prioritie, Enums.ParcelStatus parcelStatus, CustomerInParcel customerInParcel) // constructor
            {
                Id = id;
                Weight = weight;
                Prioritie = prioritie;
                ParcelStatus = parcelStatus;
                CustomerInParcel = customerInParcel;
            }
            public override string ToString()
            {
                return $"Parcel #{Id}: \n Weight = {Weight} \n Prioritie = {Prioritie} \n Parcel status = {ParcelStatus} \n Customer in parcel =\n \t {CustomerInParcel} \n ";
            }
        }
    }  
}
