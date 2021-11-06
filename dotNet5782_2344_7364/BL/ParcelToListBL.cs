using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class ParcelToListBL
    {
        int Id { set; get; }
        string SendersName { set; get; }
        string ReciversName { set; get; }
        IDAL.DO.WeightCategories weight { set; get; }
        IDAL.DO.Priorities Prioritie { set; get; }
        IDAL.DO.ParcelStatus ParcelStatus { set; get; }
    }
}
