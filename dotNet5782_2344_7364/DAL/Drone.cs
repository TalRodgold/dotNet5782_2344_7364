﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    { 
        public struct Drone 
         {
            /// <summary>
            /// This struct contains data for a drone including:
            /// drone id, drone model, maximum weight drone can carry,
            /// drones status and Battery left
            /// </summary>
            public int Id{ get; set; } // drone id
            public string Model{ get; set; } // drone model
            public WeightCategories MaxWeight{ get; set; } // maximum weight drone can carry
            public DroneStatuses Status{ get; set; } // drones status
            public double Battery{ get; set; } // Battery left
            public override string ToString() // return string with all the data
            {
                return $"Drone #{Id}: \n Model = {Model} \n Status = {Status} \n Max weight = {MaxWeight} \n battery = {(int)Battery} \n";
            }
        }
    } 
}
