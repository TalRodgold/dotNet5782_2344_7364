using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObjects
{
    /// <summary>
    /// Data source layer, holds all the data
    /// </summary>
    public class DataSource
    {
        /// <summary>
        /// this class holds all the data of the program in lists
        /// this class also creats random data to start program
        /// </summary>

        #region // 5 lists for storing data
        internal static List<Drone> DroneList; // list of drones
        internal static List<BaseStation> BaseStationList; // list of base stations
        internal static List<Customer> CustomerList; // list of customers
        internal static List<Parcel> ParcelList; // list of parcel
        internal static List<DroneCharge> DroneChargeList; // list of drone charges
        #endregion
        #region// hold runing number for id plus electricity usege 
        /// <summary>
        /// hold runing number for parcel id plus electricity usege 
        /// </summary>
        internal class Config // hold runing number for id plus electricity usege 
        {
            internal static int ParcelId = 12345; // runing number for parcel Id
            internal static double ElectricityUseAvailiblity { get; set; } = 0.1; //electricity usege for avilable
            internal static double ElectricityUseLightWeight { get; set; } = 0.2; // electricity usege for light weight
            internal static double ElectricityUseMediumWeight { get; set; } = 0.3; // electricity usege for  medium weight
            internal static double ElectricityUseHeavyWeight { get; set; } = 0.4; // electricity usege for heavy weight
            internal static double DroneChargingPaste { get; set; } = 100; // speed of drone charging per hour in %
        }
        #endregion
        /// <summary>
        /// convert double to sexagesimal
        /// </summary>
        /// <param name="decimalDegrees"></param>
        /// <param name="char"></param>
        /// <returns>cordinations in sexagesimal</returns>
        internal static string sexagesimal(double decimalDegrees, Char c) // convert double to sexagesimal
        {
            #region//convert double to sexagesimal
            // caluclate seconds
            double latDegrees = decimalDegrees;
            int latSeconds = (int)Math.Round(latDegrees * 60 * 60);  
            // calculate degrees
            double latDegreesWithFraction = decimalDegrees;
            int degrees = (int)latDegreesWithFraction;  
            // calculate minutes
            double fractionalDegrees = latDegrees - degrees; 
            double minutesWithFraction = 60 * fractionalDegrees; 
            int minutes = (int)minutesWithFraction;
            // caluclate seconds with fraction
            double fractionalMinutes = minutesWithFraction - minutes; 
            double secondsWithFraction = 60 * fractionalMinutes;
            return $"{degrees}°{minutes}'{string.Format("{0:F3}", secondsWithFraction)}\"{c}"; // return string of cordinents
            #endregion
        }
        /// <summary>
        /// this function creats random base station
        /// </summary>
        internal static void CreatBaseStation() // this function creats random base station
        {
            #region // call construct base station
            Random rnd = new Random(); // generate randome number
            BaseStation newBaseStation = new BaseStation();
            newBaseStation.Id = (Byte)DateTime.Now.Ticks + 1;
            newBaseStation.Name = Convert.ToString((RandomBases)rnd.Next(1, 11));
            newBaseStation.ChargeSlots= rnd.Next(5, 10);
            newBaseStation.Longtitude = rnd.NextDouble() + 31.728959;
            newBaseStation.Latitude = rnd.NextDouble() + 35.206714;
            BaseStationList.Add(newBaseStation); // add to list
            #endregion
        }
        /// <summary>
        /// this function creats random drone
        /// </summary>
        internal static void CreatDrone() // this function creats random drone
        {
            #region // call construct drone
            Random rnd = new Random(); // generate randome number
            Drone newDrone = new Drone();
            newDrone.Id = (Byte)DateTime.Now.Ticks + 1;
            newDrone.Model = $"modle_number{DroneList.Count() + 1}";
            newDrone.MaxWeight = (WeightCategories)rnd.Next(0, 3);
           // newDrone.Status = (DroneStatuses)rnd.Next(0, 3);
           // newDrone.Battery = rnd.Next(0, 101);
            DroneList.Add(newDrone); // add to list
            #endregion
        }
        /// <summary>
        /// this function creats random parcel
        /// </summary>
        internal static void CreatParcel() // this function creats random parcel
        {
            #region // call construct parcel
            Random rnd = new Random(); // generate randome number
            Parcel newParcel = new Parcel();
            newParcel.Id = Config.ParcelId;
            int size = CustomerList.Count();
            int num1 = rnd.Next(0, size);
            int num2 = rnd.Next(0, size);
            while (num1 == num2) // so we dont get same customer twice
            {
                num2 = rnd.Next(0, size);
            }
            newParcel.SenderId = CustomerList.ElementAt(num1).Id;
            newParcel.ReciverId = CustomerList.ElementAt(num2).Id;
            newParcel.Weight = (WeightCategories)rnd.Next(0, 3);
            newParcel.Priority = (Priorities)rnd.Next(0, 3);
            newParcel.CreatingTime = DateTime.Now;
            num1 = rnd.Next(0, DroneList.Count());
            newParcel.DroneId = DroneList[num1].Id;
            newParcel.DroneId = null;                    
            newParcel.AssociatedTime = DateTime.Now.AddHours(rnd.Next(1, 23));
            newParcel.PickedUp = null;
            newParcel.Deliverd =null;
            ParcelList.Add(newParcel); // add to list
            Config.ParcelId++; // grow runing number by 1
            #endregion
        }
        /// <summary>
        /// this function creats random customer
        /// </summary>
        internal static void CreatCustomer()  // this function creats random customer
        {
            #region // call construct customer
            Random rnd = new Random(); // generate randome number
            Customer newCustomer = new Customer();
            newCustomer.Id=(Byte)DateTime.Now.Ticks + 1;
            newCustomer.Name = Convert.ToString((RandomNames)rnd.Next(0, 15));
            newCustomer.Phone = "05" + Convert.ToString(rnd.Next(10000000, 99999999));
            newCustomer.Longtitude = rnd.NextDouble() + 31.72895;
            newCustomer.Latitude = rnd.NextDouble() + 35.206714;
            CustomerList.Add(newCustomer); // add to list
            #endregion
        }
        /// <summary>
        /// this function sets 2 randome base stations,  5 randome drones,  10 custemers and 10 parcels
        /// </summary>
        internal static void Initialize() // this function sets 2 randome base stations,  5 randome drones,  10 custemers and 10 parcels
        {
            #region// call 4 creat functions
            for (int i = 0; i < 2; i++) // set 2 base stations
            {
                CreatBaseStation();
            }

            for (int i = 0; i < 5; i++) // set 5 randome drones
            {
                CreatDrone();
            }

            for (int i = 0; i < 10; i++) // set 10 custemers 
            {
                CreatCustomer();
            }
            for (int i = 0; i < 10; i++) // set 10 parcels and 10 drone charges
            {
                CreatParcel();
            }
            #endregion
        }
    }
}

