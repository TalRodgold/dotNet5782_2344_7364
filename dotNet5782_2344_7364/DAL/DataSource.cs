using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
/// <summary>
/// hello <c> DataSource </c>v
/// </summary>
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
        internal static double sexagesimal(double decimal_degrees) // convert double to sexagesimal
        {
            double minutes = (decimal_degrees - Math.Floor(decimal_degrees)) * 60.0;
            double seconds = (minutes - Math.Floor(minutes)) * 60.0;
            double tenths = (seconds - Math.Floor(seconds)) * 10.0;
            // get rid of fractional part
            minutes = Math.Floor(minutes);
            seconds = Math.Floor(seconds);
            tenths = Math.Floor(tenths);
            return minutes + seconds + tenths;
        }

        internal  static void CreatBaseStation() // this function creats random base station
        {
            #region // call construct base station
            Random rnd = new Random(); // generate randome number
            DalObjects.ConstructBaseStation // construct
                (
                Convert.ToInt32(DateTime.Now.Ticks),
                Convert.ToString((RandomBases)rnd.Next(1, 11)),
                rnd.Next(0, 10),
                 sexagesimal(rnd.NextDouble() + 31.728959),
                 sexagesimal(rnd.NextDouble() + 35.206714)
                );
            #endregion
        }
        internal static void CreatDrone() // this function creats random drone
        {
            #region // call construct drone
            Random rnd = new Random(); // generate randome number
            DalObjects.ConstructDrone // construct
                (
                Convert.ToInt32(DateTime.Now.Ticks),
                $"modle_number{Config.Drone_arr_index + 1}",
                (WeightCategories)rnd.Next(1, 4),
                (DroneStatuses)rnd.Next(1, 4),
                 rnd.Next(0, 101)
                );
            #endregion
        }
        internal static void CreatParcel() // this function creats random parcel
        {
            #region // call construct parcel
            Random rnd = new Random(); // generate randome number
            DalObjects.ConstructParcel // construct
                (
                Config.Parcel_id,
                Convert.ToInt32(DateTime.Now.Ticks),
                Convert.ToInt32(DateTime.Now.Ticks),
                (WeightCategories)rnd.Next(1, 4),
                (Priorities)rnd.Next(1, 4),
                DateTime.Now,
                0,
                DateTime.Now.AddHours(rnd.Next(1, 23)),
                DateTime.MinValue,
                DateTime.MinValue
                );
            #endregion
        }
        internal static void CreatCustomer()  // this function creats random customer
        {
            #region // call construct customer
            Random rnd = new Random(); // generate randome number
            DalObjects.ConstructCustomer // construct
                (
                Convert.ToInt32(DateTime.Now.Ticks),
                Convert.ToString((RandomNames)rnd.Next(1, 16)),
                "05" + Convert.ToString(rnd.Next(10000000, 99999999)),
                sexagesimal(rnd.NextDouble() + 31.728959),
                sexagesimal(rnd.NextDouble() + 35.206714)
                );
            #endregion
        }
        internal static void Initialize() // this function sets 2 randome base stations,  5 randome drones,  10 custemers and 10 parcels
        {
            for (int i = 0; i < 2; i++) // set 2 base stations
            {
                CreatBaseStation();
            }

            for (int i = 0; i < 5; i++) // set 5 randome drones
            {
                CreatDrone();
            }

            for (int i = 0; i < 10; i++) // set 10 custemers and 10 parcels
            {
                CreatParcel();
                CreatCustomer();
            }
        }
    }
}

