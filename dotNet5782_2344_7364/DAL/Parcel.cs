using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel 
        {
            public int Id { get; set; } // Id number for each parcel
            public int SenderId { get; set; } // Senders Id
            public int TargetId { get; set; } // Targets Id
            public WeightCategories Weight { get; set; } // The weight of the parcel
            public Priorities Priority { get; set; } // parcels priority
            public DateTime Requsted { get; set; } //requested time
            public int DroneId { get; set; } // Drones Id
            public DateTime Scheduled { get; set; } // Scheduled
            public DateTime PickedUp { get; set; }  // PickedUp
            public DateTime Deliverd { get; set; } // Deliverd

           
        }
    }

}