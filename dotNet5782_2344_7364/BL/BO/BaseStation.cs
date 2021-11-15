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
            public BaseStation() { }

            public BaseStation(int id, string name, Location location, int freeChargingSlots)//, List<DroneInCharging> droneInCharging) ?????????
            {
                Id = id;
                Name = name;
                Location = location;
                NumberOfFreeChargingSlots = freeChargingSlots;
                DroneInCharging droneInCharging = new DroneInCharging(-1, -1);
                for (int i = 0; i < NumberOfFreeChargingSlots; i++)
                {
                    ListOfDroneInCharging.Add(droneInCharging);
                }
                // ListOfDroneInCharging = droneInCharging;
            }
            public BaseStation(int id, string name, Location location, int freeChargingSlots, List<DroneInCharging> listOfDroneInCharging)//, List<DroneInCharging> droneInCharging) ?????????
            {
                Id = id;
                Name = name;
                Location = location;
                NumberOfFreeChargingSlots = freeChargingSlots;
                ListOfDroneInCharging = listOfDroneInCharging;
            }
        }
    }
    
}
