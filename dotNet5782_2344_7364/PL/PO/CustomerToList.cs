using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BO;

namespace PO
{

    public class CustomerToList : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; }  set { id = value; OnPropertyChanged("id"); } }
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("name"); } }
        private string phone;
        public string Phone { get { return phone; } set { phone = value; OnPropertyChanged("phone"); } }
        private int numberOfParcelsThatSentAndArrived;
        public int NumberOfParcelsThatSentAndArrived { get { return numberOfParcelsThatSentAndArrived; }  set { numberOfParcelsThatSentAndArrived = value; OnPropertyChanged("numberOfParcelsThatSentAndArrived"); } }
        private int parcelsThatSentYetNotArrived;
        public int ParcelsThatSentYetNotArrived { get { return parcelsThatSentYetNotArrived; } set { parcelsThatSentYetNotArrived = value; OnPropertyChanged("parcelsThatSentYetNotArrived"); }  }
        private int parcelsRecived;
        public int ParcelsRecived { get { return parcelsRecived; } set { parcelsRecived = value; OnPropertyChanged("parcelsRecived"); } }
        private int parcelsOnWayToClient;
        public int ParcelsOnWayToClient { get { return parcelsOnWayToClient; } set { parcelsOnWayToClient = value; OnPropertyChanged("parcelsOnWayToClient"); } }
        public override string ToString()
        {
            return $" Customer #{Id}: \n Name = {Name} \n Phone = {Phone} \n Number of parcels that where sent and arrived = {NumberOfParcelsThatSentAndArrived} \n Number of parcels that sent yet not arrived = {ParcelsThatSentYetNotArrived} \n Parcels recived = {ParcelsRecived} \n Parcels on the way to a client {ParcelsOnWayToClient} \n";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prpertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prpertyName));
            }
        }

    }
}