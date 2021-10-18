using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct BaseStation
        {
            public int Id { get; set; } // Id number for each drone
            public string Name { get; set; } // Drones name
            public int ChargeSlots { get; set; } // Number of free charging atstions
            public double Longtitude { get; set; } // Longitude 
            public double Latitude { get; set; } // Latitude
        }
      
    }
   
}
