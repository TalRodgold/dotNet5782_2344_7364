﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    static public class DalFactory
    {
        public static IDal GetDal(string str)
        {
            switch (str)
            {
                case "DalObject":
                    return DalObjects.DalObjects.Instance;
                case "DalXml":
                default:
                    throw new InvalidOperationException() ;
            }
        }
    }
}