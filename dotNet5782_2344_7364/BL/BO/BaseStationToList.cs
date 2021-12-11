using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// class for a base station in a list
        /// </summary>
        public class BaseStationToList
        {
            public int? Id { set; get; } = null;
            public string Name { set; get; }
            public int FreeChargingSlots { set; get; }
            public int OccupiedChargingSlots { set; get; }
            public override string ToString()
            {
                return $" Base station #{Id}: \n Name = {Name} \n Free charging slots = {FreeChargingSlots} \n Occupied charging slots = {OccupiedChargingSlots} \n ";
            }
            public BaseStationToList(int? id,string name,int freeChargingSlot,int  occupiedChargingSlots)
            {
                Id = id;
                Name = name;
                FreeChargingSlots = freeChargingSlot;
                OccupiedChargingSlots = occupiedChargingSlots;
            }
        }
    }
  
}
