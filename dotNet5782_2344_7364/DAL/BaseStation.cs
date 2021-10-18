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
            public double Longitude { get; set; } // Longitude 
            public double Latitude { get; set; } // Latitude
        }
        void BaseStationCreat(int id=0,string name="no name set",int chargeSlots=0,double Longitude=0,double latitude=0)
        {
            Id=id;
            Name=name;
            ChargeSlots=chargeSlots;
            Longitude=Longitude;
            Latitude=latitude;
        }
    }
   
}
