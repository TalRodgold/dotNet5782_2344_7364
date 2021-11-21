using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public string Phone { set; get; }
            public Location Location { set; get; }
            public List<ParcelAtCustomer> ParcelFromCustomer { set; get; } // change to list
            public List<ParcelAtCustomer> ParcelToCustomer { set; get; } // change to list

            public override string ToString()
            {
                return $"Customer #{Id}: \nName = {Name} \nPhone = {Phone} \n{Location.ToString()} \nList of Parcels From Customer: \n{printList(ParcelFromCustomer)} \nList of Parcels To Customer: \n{printList(ParcelToCustomer)}";
            }
            public Customer() { }
            public Customer(int id,string name,string phone,Location location)
            {
                Id = id;
                Name = name;
                Phone = phone;
                Location = location;
            }
            public Customer(int id, string name, string phone, Location location, List<ParcelAtCustomer> parcelFromCustomer, List<ParcelAtCustomer> parcelToCustomer)
            {
                Id = id;
                Name = name;
                Phone = phone;
                Location = location;
                ParcelFromCustomer = parcelFromCustomer;
                ParcelToCustomer = parcelToCustomer;
            }
            private string printList(List<ParcelAtCustomer> l)
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
}
    
