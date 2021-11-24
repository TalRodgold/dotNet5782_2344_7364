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
        /// class for base station
        /// </summary>
        public class BaseStation
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public Location Location { set; get; }
            public int NumberOfFreeChargingSlots { set; get; }
            public List<DroneInCharging> ListOfDroneInCharging { set; get; }
            public override string ToString()
            {
                return $" Base station #{Id}: \n Name = {Name} \n{Location.ToString()} Number of free charging slots = {NumberOfFreeChargingSlots} \n All drones in charging: {printList(ListOfDroneInCharging)} \n ";
            }
            public BaseStation() { ListOfDroneInCharging = new List<DroneInCharging>(); } // default
            public BaseStation(int id, string name, Location location, int freeChargingSlots) // constructor
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
            }
            public BaseStation(int id, string name, Location location, int freeChargingSlots, List<DroneInCharging> listOfDroneInCharging) // constructor
            {
                Id = id;
                Name = name;
                Location = location;
                NumberOfFreeChargingSlots = freeChargingSlots;
                ListOfDroneInCharging = listOfDroneInCharging;
            }
            private string printList(List<DroneInCharging> l) // make a string from list of drone in charging
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
