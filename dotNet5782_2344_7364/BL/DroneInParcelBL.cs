using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DroneInParcelBL
        {
            int Id { set; get; }
            float Battery { set; get; }
            LocationBL CurrentLocation { set; get; }
            public override string ToString()
            {
                return $" Drone #{Id}: \n Battery = {Battery} \n Current location {CurrentLocation} \n";
            }
        }
    }
   
}
