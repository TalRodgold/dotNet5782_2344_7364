using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct  DroneCharge
        {
         public int Id{ get; set; };
         public int Stationld{ get; set; };
        }
        void DroneChargeCreat(int id=0,int stationId=0)
        {
            Id=id;
            Stationld=stationId;
        }
    }
}
