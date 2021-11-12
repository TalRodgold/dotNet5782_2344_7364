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
            public ParcelAtCustomer ParcelFromCustomer { set; get; }
            public ParcelAtCustomer parcelToCustomer { set; get; }

            public override string ToString()
            {
                return $"  ";
            }
            public Customer() { }
            public Customer(int id,string name,string phone,Location location)
            {
                Id = id;
                Name = name;
                Phone = phone;
                Location = location;
            }
        }
    }
}
    
