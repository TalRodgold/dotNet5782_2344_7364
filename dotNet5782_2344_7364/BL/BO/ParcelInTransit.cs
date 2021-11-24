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
        /// class for parcel in transit
        /// </summary>
        public class ParcelInTransit
        {
            public int Id { set; get; }
            public bool Status { set; get; } // true = waiting for pickup | false = on transit to location
            public Enums.Priorities Priorities { set; get; }
            public Enums.WeightCategories Weight { set; get; }
            public CustomerInParcel CustomerInParcelSender { set; get; }
            public CustomerInParcel CustomerInParcelReciver { set; get; }
            public Location PickupLocation { set; get; }
            public Location DeliveryLocation { set; get; }
            public double Distance { set; get; }
            public ParcelInTransit() { Id = -1; }
            public ParcelInTransit(int id, bool status, Enums.Priorities priorities, Enums.WeightCategories weight, CustomerInParcel customerInParcelSender, CustomerInParcel customerInParcelReciver, Location pickupLocation, Location deliveryLocation, double distance) // constructor
            {
                Id = id;
                Status = status;
                Priorities = priorities;
                Weight = weight;
                CustomerInParcelSender = customerInParcelSender;
                CustomerInParcelReciver = customerInParcelReciver;
                PickupLocation = pickupLocation;
                DeliveryLocation = deliveryLocation;
                Distance = distance;
            }
            public override string ToString()
            {
                if (Id == -1) // if not inisilaized
                {
                    return $" Parcel # : \n Prioritie = {Priorities} \n Weight = {Weight} \n Customer in parcel sender = {CustomerInParcelSender} \n Customer in parcel reciver = {CustomerInParcelReciver} \n Pickup location: {PickupLocation.ToString()} \n Delivery location: {DeliveryLocation.ToString()} \n Distance = {Distance} ";

                }
                return $" Parcel #{Id}: \n Prioritie = {Priorities} \n Weight = {Weight} \n Customer in parcel sender = {CustomerInParcelSender} \n Customer in parcel reciver = {CustomerInParcelReciver} \n Pickup location: {PickupLocation.ToString()} \n Delivery location: {DeliveryLocation.ToString()} \n Distance = {Distance} ";
            }

        }
    }
    
}
