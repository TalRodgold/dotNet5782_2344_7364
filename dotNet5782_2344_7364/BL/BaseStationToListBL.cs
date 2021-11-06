using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class BaseStationToListBL
        {
            int Id { set; get; }
            string Name { set; get; }
            int FreeChargingSlots { set; get; }
            int OccupiedChargingSlots { set; get; }
        }
    }
  
}
