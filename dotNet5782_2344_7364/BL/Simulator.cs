using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using static BlApi.BL;
using static BlApi.BlFactory;

namespace BlApi
{
   internal class Simulator
    {
        static int DroneSpeed = 10;
        static int Timer = 1000;
        Thread thread;
        Simulator(BlApi.BL bl, int? id, Action action, Func<bool> func) 
        {
            //thread = new Thread();
            while (func())
            {
                //Thread.Sleep(DELAY);
            }
        }
    }
}
