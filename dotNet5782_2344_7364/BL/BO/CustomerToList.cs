using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// class for customer in list
        /// </summary>
        public class CustomerToList
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public string Phone { set; get; }
            public int NumberOfParcelsThatSentAndArrived { set; get; }
            public int ParcelsThatSentYetNotArrived { set; get; }
            public int ParcelsRecived { set; get; }
            public int ParcelsOnWayToClient { set; get; }
            public override string ToString()
            {
                return $" Customer #{Id}: \n Name = {Name} \n Phone = {Phone} \n Number of parcels that where sent and arrived = {NumberOfParcelsThatSentAndArrived} \n Number of parcels that sent yet not arrived = {ParcelsThatSentYetNotArrived} \n Parcels recived = {ParcelsRecived} \n Parcels on the way to a client {ParcelsOnWayToClient} \n";
            }
            public CustomerToList(int id,string name,string phone,int numberOfParcelsThatSentAndArrived, int parcelsThatSentYetNotArrived,int parcelsRecived,int parcelsOnWayToClient)
            {
                Id = id;
                Name = name;
                Phone = phone;
                NumberOfParcelsThatSentAndArrived = numberOfParcelsThatSentAndArrived;
                ParcelsThatSentYetNotArrived = parcelsThatSentYetNotArrived;
                ParcelsRecived = parcelsRecived;
                ParcelsOnWayToClient = parcelsOnWayToClient;
            }
        }
    }
    
}
