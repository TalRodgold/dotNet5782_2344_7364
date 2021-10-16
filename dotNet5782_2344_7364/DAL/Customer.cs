using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; } // Id number for each costomer
            public string Name { get; set; } // costomers name
            public string Phone { get; set; } // customers phone number
            public double Longitude { get; set; } // Longitude 
            public double latitude { get; set; } // Latitude
        }
    }

}
