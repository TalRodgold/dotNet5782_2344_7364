using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct  DroneCharge
        {
            /// <summary>
            /// This struct contains data for a drone charging including:
            /// id of drone and id of station
            /// </summary>
            public int DroneId{ get; set; } // id of drone
            public int StationId{ get; set; } // id of station
            public DateTime? TimeOfStartCharging { get; set;}//time when he start charging 
        }
  
    }
}
