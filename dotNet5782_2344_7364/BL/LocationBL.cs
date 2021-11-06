using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class LocationBL
        {
            double longitude { set; get; }
            double Latitude { set; get; }
            public override string ToString()
            {
                return $" Longitude{longitude}: \n Latitude = {Latitude} \n";
            }
        }
    }
    
}
