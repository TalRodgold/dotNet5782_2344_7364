using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DroneToListBL
        {
            int Id { set; get; }
            string Model { set; get; }
            EnumsBL.WeightCategories Weight { set; get; }
            float Battery { set; get; }
            EnumsBL.DroneStatuses DroneStatuses { set; get; }
            LocationBL CurrentLocation { set; get; }
            int NumberOfParcelInTransit { set; get; }
            public override string ToString()
            {
                return $" Drone #{Id}: \n Battery = {Battery} Weight = {Weight} \n Battery = {Battery} \n Drone Status = {DroneStatuses} \n Current location = {CurrentLocation} \n Number of parcel in transit = {NumberOfParcelInTransit} \n";
            }
        }
    }
    
}
