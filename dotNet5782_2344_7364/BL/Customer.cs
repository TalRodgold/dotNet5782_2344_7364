using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public int Phone { set; get; }
            public Location Location { set; get; }
            // רשימת משלוחיםחבילות אצל לקוח - מהלקוח
            // רשימת משלוחיםחבילות אצל לקוח - אל הלקוח

            public override string ToString()
            {
                return $"  ";
            }
        }
    }
}
    
