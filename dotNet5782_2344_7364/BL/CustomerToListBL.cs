using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class CustomerToListBL
        {
            int Id { set; get; } 
            string Name { set; get; }
            int Phone { set; get; }
            int NumberOfParcelsThatSentAndArrived { set; get; }
            int ParcelsThatSentYetNotArrived { set; get; }
            int ParcelsRecived { set; get; }
            int ParcelsOnWayToClient { set; get; }


        }
    }
    
}
