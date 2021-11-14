﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneInCharging
        {
            public int Id { set; get; }
            public float Battery { set; get; }
            public override string ToString()
            {
                return $" Drone #{Id}: \n Battery = {Battery} \n";
            }
            public DroneInCharging(int id,float battery)
            {
                Id = id;
                Battery = battery;
            }
        }
       
    }
    
}