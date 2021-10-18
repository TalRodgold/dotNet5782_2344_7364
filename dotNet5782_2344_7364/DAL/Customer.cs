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
            public double Longtitude { get; set; } // Longitude 
            public double Latitude { get; set; } // Latitude
        }

        void CustomerCreat(int id=0,string name="no name set",string phone="no phone set",double longtitude=0,double latitude=0)
        {
            Id=id;
            Name=name;
            Phone=phone;
            Longtitude=longtitude;
            Latitude=latitude;
        }
    }

}
