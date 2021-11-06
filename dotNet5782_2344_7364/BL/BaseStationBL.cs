using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class BaseStationBL
        {
            int Id { set; get; }
            string Name { set; get; }
            LocationBL Location { set; get; }
            int NumberOfFreeChargingSlots { set; get; }
            List<DroneInChargingBL> ListOfDroneInCharging { set; get; }

        }
    }
    
}
