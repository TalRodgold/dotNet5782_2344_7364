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
            public override string ToString()
            {
                return $" Customer #{Id}: \n Name = {Name} \n Phone = {Phone} \n Number of parcels that where sent and arrived = {NumberOfParcelsThatSentAndArrived} \n Number of parcels that sent yet not arrived = {ParcelsThatSentYetNotArrived} \n Parcels recived = {ParcelsRecived} \n Parcels on the way to a client {ParcelsOnWayToClient} \n";
            }

        }
    }
    
}
