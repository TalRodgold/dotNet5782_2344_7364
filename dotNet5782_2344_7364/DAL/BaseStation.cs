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
            /// <summary>
            /// This struct contains data for a base station including:
            /// Id number for each base station, base station name, Number of free charging satstions,
            /// Longitude and Latitude 
            /// </summary>
            public int Id { get; set; } // Id number for each base station 
            public string Name { get; set; } // base station name
            public int ChargeSlots { get; set; } // Number of free charging satstions
            public string Longtitude { get; set; } // Longitude 
            public string Latitude { get; set; } // Latitude
            public override string ToString() // return string with all the data
            {
                return $" Base station #{Id}: \n Name = {Name} \n Charge slots = {ChargeSlots} \n Longtitude = {Longtitude} \n Latitude = {Latitude} \n ";
            }
        }
      
    }
   
}
