using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DalApi
{
    /// <summary>
    /// 
    /// </summary>
    static public class DalFactory
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IDal GetDal(string str)
        {
            switch (str)
            {
                case "DalObject": // for dal object
                    return DalObjects.DalObjects.Instance;
                case "DalXml": // for dal Xml
                    return DalXml.DalXml.Instance;
                default:
                    throw new InvalidOperationException() ;
            }
        }
    }
}
