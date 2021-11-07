using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class Drone
        {
            public int Id { set; get; }
            public string Model { set; get; }
            public Enums.WeightCategories Weight { set; get; }
            public float Battery { set; get; }
            public Enums.DroneStatuses DroneStatuses { set; get; }
            public ParcelInTransit ParcelInTransit { set; get; }
            public Location CurrentLocation { set; get; }
            public override string ToString()
            {
                return $" Drone #{Id}: \n Model = {Model} \n Weight = {Weight} \n Battery = {Battery} \n Statuses = {DroneStatuses} \n Parcel in transit {ParcelInTransit} \n  Current location = {CurrentLocation} \n";
            }
        }
    }
   
}
