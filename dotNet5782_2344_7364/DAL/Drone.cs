using System;
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
            public int Id{ get; set; }
            public string Model{ get; set; }
            public WeightCategories MaxWeight{ get; set; }
            public DroneStatuses Status{ get; set; }
            public double Battery{ get; set; }

            public override string ToString()
            {
                return $"Drone #{Id}: model = {Model}, {Status}, {MaxWeight}, battery = {(int)Battery} \n";
            }
            void DroneCreat(int id = 0, string model = "No modle set", WeightCategories maxWeight = WeightCategories.Heavy, DroneStatuses status = DroneStatuses.Available, int battery = 100) 
            { 
                Id = id;
                Model = model;
                MaxWeight = maxWeight; 
                Status = status; 
                Battery = battery; 
            }
        }
    }
   
}
