using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// class for drone in charging
    /// </summary>
    public class DroneInCharging
    {
        public int? Id { set; get; } = null;
        public double Battery { set; get; }
        public DateTime? TimeOfStartCharging { set; get; } = null;
        public override string ToString()
        {
            return $" Drone #{Id}: \n Battery = {Battery * 100}% \n";
        }
        public DroneInCharging() { } // default
        public DroneInCharging(int? id, double battery, DateTime? currentTime) // constructor
        {
            Id = id;
            Battery = battery;
            TimeOfStartCharging = currentTime;
        }
    }
}
