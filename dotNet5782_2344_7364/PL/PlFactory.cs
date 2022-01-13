using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace PL
{
    static public class PlFactory
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Model GetModel(string str)
        {
            switch (str)
            {
                case "Model":
                    return PL.Model.Instance;
                case "DalXml":
                    //return DalXml.DalXml.Instance;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
