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
            IDAL.DO.WeightCategories WeightCategories { set; get; }
            float Battery { set; get; }
            IDAL.DO.DroneStatuses DroneStatuses { set; get; }
            LocationBL CurrentLocation { set; get; }
            int NumberOfParcelInTransit { set; get; }
        }
    }
    
}
