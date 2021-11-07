using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerInParcel
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public override string ToString()
            {
                return $" Customer #{Id}: \n Name = {Name} \n ";
            }
        }
    }
    
}
