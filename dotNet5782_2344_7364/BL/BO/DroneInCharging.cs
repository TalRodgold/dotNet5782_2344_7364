using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneInCharging
        {
            public int Id { set; get; }
            public double Battery { set; get; }
            public override string ToString()
            {
                return $" Drone #{Id}: \n Battery = {Battery * 100}% \n";
            }
            public DroneInCharging() { }
            public DroneInCharging(int id,double battery)
            {
                Id = id;
                Battery = battery;
            }
        }
       
    }
    
}
