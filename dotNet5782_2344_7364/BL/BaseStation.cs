using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class BaseStation
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public Location Location { set; get; }
            public int NumberOfFreeChargingSlots { set; get; }
            public List<DroneInCharging> ListOfDroneInCharging { set; get; }
            public override string ToString()
            {
                return $" Base station #{Id}: \n Name = {Name} \n Location = {Location} \n Number of free charging slots = {NumberOfFreeChargingSlots} \n All drones in charging = {ListOfDroneInCharging} \n ";
            }
            public BaseStation(int id, string name, Location location, int freeChargingSlots, List<DroneInCharging> droneInCharging) 
            {
                Id = id;
                Name = name;
                Location = location;
                NumberOfFreeChargingSlots = freeChargingSlots;
                ListOfDroneInCharging = droneInCharging;
            }
        }
    }
    
}
