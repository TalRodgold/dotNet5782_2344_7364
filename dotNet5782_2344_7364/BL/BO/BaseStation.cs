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
                return $" Base station #{Id}: \n Name = {Name} \n {Location.ToString()} \n Number of free charging slots = {NumberOfFreeChargingSlots} \n All drones in charging: {printList(ListOfDroneInCharging)} \n ";
            }
            public BaseStation() { ListOfDroneInCharging = new List<DroneInCharging>(); }

            public BaseStation(int id, string name, Location location, int freeChargingSlots)//, List<DroneInCharging> droneInCharging) ?????????
            {
                Id = id;
                Name = name;
                Location = location;
                NumberOfFreeChargingSlots = freeChargingSlots;
                DroneInCharging droneInCharging = new DroneInCharging(-1, -1);
                ListOfDroneInCharging = new List<DroneInCharging>();
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
            private string printList(List<DroneInCharging> l)
            {
                string s = "";
                foreach (var item in l)
                {
                    s += item.ToString();
                }
                return s;
            }
        }
    }
    
}
