using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            int User_input = Console.Read();
            switch (User_input)
            {
                case (1):
                    Console.WriteLine("Please enter following data: \n 1) Id \n 2) Name \n 3) ChargeSlots \n 4) Longtitude \n 5) Latitude ");
                    int User_id = Console.Read();
                    string User_name = Console.ReadLine();
                    int User_chargeSlots = Console.Read();
                    double User_longtitude = Console.Read();
                    double User_latitude = Console.Read();
                    
                    break;
                default:
                    break;
            }

        }
    }
}
