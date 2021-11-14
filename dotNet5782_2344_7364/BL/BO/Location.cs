using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Location
        {
            public double Longitude { set; get; }
            public double Latitude { set; get; }
            public override string ToString()
            {
                return $" Longitude = {Longitude}: \n Latitude = {Latitude} \n";
            }
            public Location(double longitude, double latitude)
            {
                Longitude = longitude;
                Latitude = latitude;
            }
        }
    }
    
}
