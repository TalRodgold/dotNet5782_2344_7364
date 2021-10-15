using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public enum WeightCategories
        {
            [Description("Light weight")]
            Light,
            [Description("Medium weight")]
            Medium,
            [Description("Heavy weight")]
            Heavy
        }

        public enum Priorities { Regular,Express,Urgent}

         public enum DroneStatuses
        {
            [Description("Free for delivery")]
            Available,
            [Description("Doing dilivery")]
            Delivery,
            [Description("Being charged")]
            Maintenance
        }
    }
}
