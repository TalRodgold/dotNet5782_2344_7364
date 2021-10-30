using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObjects
{
    /// <summary>
    /// Holds data in arrays
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
        internal class Config // hold runing number for id
        {
            /// <summary>
            /// hold runing number for id
            /// </summary>
            
            #region// runing number
            internal static int ParcelId = 12345; // runing number for parcel Id
            #endregion
        }
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
            newBaseStation.Id = (Byte)DateTime.Now.Ticks;
            newBaseStation.Name = Convert.ToString((RandomBases)rnd.Next(1, 11));
            newBaseStation.ChargeSlots= rnd.Next(0, 10);
            newBaseStation.Longtitude = sexagesimal(rnd.NextDouble() + 31.728959, 'N');
            newBaseStation.Latitude = sexagesimal(rnd.NextDouble() + 35.206714, 'E');
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
            newDrone.Id = (Byte)DateTime.Now.Ticks;
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
            newParcel.SenderId = CustomerList.ElementAt(rnd.Next(0, CustomerList.Count())).Id;
            newParcel.TargetId = CustomerList.ElementAt(rnd.Next(0, CustomerList.Count())).Id;
            newParcel.Weight = (WeightCategories)rnd.Next(0, 3);
            newParcel.Priority = (Priorities)rnd.Next(0, 3);
            newParcel.Requsted = DateTime.Now;
            newParcel.DroneId = 0;
            newParcel.Scheduled = DateTime.Now.AddHours(rnd.Next(1, 23));
            newParcel.PickedUp = DateTime.MinValue;
            newParcel.Deliverd = DateTime.MinValue;
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
            newCustomer.Id=(Byte)DateTime.Now.Ticks;
            newCustomer.Name = Convert.ToString((RandomNames)rnd.Next(0, 15));
            newCustomer.Phone = "05" + Convert.ToString(rnd.Next(10000000, 99999999));
            newCustomer.Longtitude = DataSource.sexagesimal(rnd.NextDouble() + 31.72895, 'N');
            newCustomer.Latitude = DataSource.sexagesimal(rnd.NextDouble() + 35.206714, 'E');
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

