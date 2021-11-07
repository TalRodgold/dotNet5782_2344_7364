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
            public float Battery { set; get; }
            public Location CurrentLocation { set; get; }
            public override string ToString()
            {
                return $" Drone #{Id}: \n Battery = {Battery} \n Current location {CurrentLocation} \n";
            }
        }
    }
   
}
