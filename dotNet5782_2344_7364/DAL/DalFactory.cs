using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DalApi
{
    static public class DalFactory
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IDal GetDal(string str)
        {
            switch (str)
            {
                case "DalObject":
                    return DalObjects.DalObjects.Instance;
                case "DalXml":
                    return DalXml.DalXml.Instance;
                default:
                    throw new InvalidOperationException() ;
            }
        }
    }
}
