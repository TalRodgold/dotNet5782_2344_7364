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

        internal class Config // hold index of next free index
        {
            internal static int Drone_arr_index = 0; // points to next free index in drone_arr
            internal static int BaseStation_arr_index = 0; // points to next free index in BaseStation_arr
            internal static int Customer_arr_index = 0; // points to next free index in Customer_arr
            internal static int Parcel_arr_index = 0; // points to next free index in Parcel_arr
            internal static int Parcel_id = 12345; // runing number for parcel Id
        }

        internal void Initialize()
        {
       
        Random rnd = new Random(); // generate randome number
         
            for (int i=0; i<2; i++) // set 2 base stations
            {
                BaseStation_arr[Config.BaseStation_arr_index] = new BaseStation
                {
                    Id = Convert.ToInt32(DateTime.Now.Ticks),
                    Name = Convert.ToString((RandomBases)rnd.Next(1, 11)),
                    ChargeSlots = rnd.Next(1,101),
                    Longitude= rnd.NextDouble(31.728959, 31.728959),
                    latitude = rnd.NextDouble(35.206714, 35.221416)
                };
                Config.BaseStation_arr_index++;
            }

            for (int i = 0; i < 5; i++) // set 5 randome drones
            {
                Drone_arr[Config.Drone_arr_index] = new Drone // set random data for new drone
                {
                    Id = Convert.ToInt32(DateTime.Now.Ticks),
                    Model = $"modle_number{i + 1}",
                    MaxWeight = (WeightCategories)rnd.Next(1, 4),
                    Battery = rnd.Next(0, 101),
                    Status = (DroneStatuses)rnd.Next(1, 4),
                };
                Config.Drone_arr_index++; // edvance free index by 1
            }

            for (int i = 0; i < 10; i++) // set 10 custemers and 10 parcels
            {
                Parcel_arr[Config.Parcel_arr_index] = new Parcel
                {
                    Id = Config.Parcel_id,
                    SenderId = Convert.ToInt32(DateTime.Now.Ticks),
                    TargetId = Convert.ToInt32(DateTime.Now.Ticks),
                    Weight = (WeightCategories)rnd.Next(1,4),
                    Priority = (Priorities)rnd.Next(1,4),
                    Requsted = DateTime.Now,
                    DroneId = 0,
                    Scheduled = DateTime.Now.AddMinutes(rnd.Next(1, 60)),
                    PickedUp = DateTime.Now.AddMinutes(rnd.Next(1, 60)),
                    Deliverd = DateTime.Now.AddMinutes(rnd.Next(1, 60)),
                };
                
                Customer_arr[Config.Customer_arr_index] = new Customer
                {
                    Id = Convert.ToInt32(DateTime.Now.Ticks),
                    Name = Convert.ToString((RandomNames)rnd.Next(1, 16)),
                    Phone = "05" + Convert.ToString(rnd.Next(10000000, 99999999)),
                    Longitude = rnd.NextDouble(31.728959, 31.728959),
                    latitude = rnd.NextDouble(35.206714, 35.221416)
                };
                Config.Parcel_id++; // grow runing id number by 1
                Config.Parcel_arr_index++; // edvance free index by 1
                Config.Customer_arr_index++; // edvance free index by 1
            }
           
        }
    }
}

