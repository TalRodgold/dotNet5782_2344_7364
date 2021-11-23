using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneInParcel
        {
            public int Id { set; get; }
            public double Battery { set; get; }
            public Location CurrentLocation { set; get; }
            public DroneInParcel(int id, double battery, Location location)
            {
                Id = id;
                Battery = battery;
                CurrentLocation = location;
            }
            public override string ToString()
            {
                return $" Drone #{Id}: \n Battery = {Battery * 100}% \n Current location {CurrentLocation} \n";
            }
        }
    }
   
}
