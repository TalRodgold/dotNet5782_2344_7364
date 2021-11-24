using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// class for drone in charging
        /// </summary>
        public class DroneInCharging
        {
            public int Id { set; get; }
            public double Battery { set; get; }
            public override string ToString()
            {
                return $" Drone #{Id}: \n Battery = {Battery * 100}% \n";
            }
            public DroneInCharging() { } // default
            public DroneInCharging(int id,double battery) // constructor
            {
                Id = id;
                Battery = battery;
            }
        }
    } 
}
