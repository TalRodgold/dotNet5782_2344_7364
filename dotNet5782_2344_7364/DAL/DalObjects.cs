using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObjects
{
    public class DataSource
    {
        internal static Drone[] Drone_arr = new Drone[10]; // array of drones
        internal static BaseStation[] BaseStation_arr = new BaseStation[5]; // array of base stations
        internal static Customer[] Customer_arr = new Customer[100]; // array of customers
        internal static Parcel[] Parcel_arr = new Parcel[1000]; // array of parcel

        internal class Config
        {
            internal static int Drone_arr_index = 0; // points to next free index in drone_arr
            internal static int BaseStation_arr_index = 0; // points to next free index in BaseStation_arr
            internal static int Customer_arr_index = 0; // points to next free index in Customer_arr
            internal static int Parcel_arr_index = 0; // points to next free index in Parcel_arr
            internal static int Parcel_id = 0; // ???????????????????????????????????
        }

        internal void Initialize()
        {
           
            Random rnd = new Random(); // generate randome number
            for (int i=0; i<2; i++) // set 
            {
               
                Drone_arr[Config.Drone_arr_index] = new Drone { Id = rnd.Next(1, 100), Model = $"modle_number{i +1}", MaxWeight = WeightCategories.Heavy, Battery = rnd.Next(1, 100), Status = ;
            }
                

            }

            for (int i = 0; i < 5; i++)
            {

            }

            for (int i = 0; i < 10; i++)
            {

            }
           
        }
    }
}

