using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// class for customer in parcel
    /// </summary>
    public class CustomerInParcel
    {
        public int? Id { set; get; } = null;
        public string Name { set; get; }
        public override string ToString()
        {
            return $"Customer #{Id}: \n\t Name = {Name}";
        }
        public CustomerInParcel(int? id, string name) // constructor
        {
            Id = id;
            Name = name;
        }
    }
}
