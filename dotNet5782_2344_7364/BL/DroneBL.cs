using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DroneBL
        {
            int Id { set; get; }
            string Model { set; get; }
            EnumsBL.WeightCategories Weight { set; get; }
            float Battery { set; get; }
            EnumsBL.DroneStatuses DroneStatuses { set; get; }
            ParcelInTransitBL ParcelInTransit { set; get; }
            LocationBL CurrentLocation { set; get; }
            public override string ToString()
            {
                return $" Drone #{Id}: \n Model = {Model} \n Weight = {Weight} \n Battery = {Battery} \n Statuses = {DroneStatuses} \n Parcel in transit {ParcelInTransit} \n  Current location = {CurrentLocation} \n";
            }
        }
    }
   
}
