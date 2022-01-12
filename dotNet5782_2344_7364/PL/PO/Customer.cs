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

    public class Customer : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("id"); } }
        private string name;
        public string Name { get { return name; } set { name = value;OnPropertyChanged("name"); }  }
        private string phone;
        public string Phone { get { return phone; } set { phone = value; OnPropertyChanged("phone"); }  }
        private Location location;
        public Location Location { get { return location; } set { location = value; OnPropertyChanged("location"); } }
        private List<ParcelAtCustomer> parcelFromCustomer;
        public List<ParcelAtCustomer> ParcelFromCustomer { get { return parcelFromCustomer; } set { parcelFromCustomer = value; OnPropertyChanged("parcelFromCustomer"); } } // change to list
        List<ParcelAtCustomer> parcelToCustomer;
        public List<ParcelAtCustomer> ParcelToCustomer { get { return parcelToCustomer; } set { parcelToCustomer = value; OnPropertyChanged("parcelToCustomer"); } }// change to list

        public override string ToString()
        {
            return $"Customer #{Id}: \nName = {Name} \nPhone = {Phone} \n{Location.ToString()} \nList of Parcels From Customer: \n{printList(ParcelFromCustomer)} \nList of Parcels To Customer: \n{printList(ParcelToCustomer)}";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prpertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prpertyName));
            }
        }
        private string printList(List<ParcelAtCustomer> l) // make a string from list of parcels at customer
        {
            string s = "";
            foreach (var item in l)
            {
                s += item.ToString();
            }
            return s;
        }

    }
}
