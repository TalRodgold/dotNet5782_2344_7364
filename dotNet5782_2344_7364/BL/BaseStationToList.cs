using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class BaseStationToList
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public int FreeChargingSlots { set; get; }
            public int OccupiedChargingSlots { set; get; }
            public override string ToString()
            {
                return $" Base station #{Id}: \n Name = {Name} \n Free charging slots = {FreeChargingSlots} \n Occupied charging slots = {OccupiedChargingSlots} \n ";
            }
        }
    }
  
}
