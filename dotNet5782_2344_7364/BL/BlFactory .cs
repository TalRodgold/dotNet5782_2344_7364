using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BlApi
{
    /// <summary>
    /// factory to work on same bl
    /// </summary>
    public class BlFactory
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IBl GetBl(string str)
        {
            switch (str)
            {
                case "BL":
                    return BL.Instance;
                case "XML":
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
