using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class CustomerBL
        {
            int Id { set; get; }
            string Name { set; get; }
            int Phone { set; get; }
            LocationBL Location { set; get; }
            // רשימת משלוחיםחבילות אצל לקוח - מהלקוח
           // רשימת משלוחיםחבילות אצל לקוח - אל הלקוח
        }
    }
}
    
