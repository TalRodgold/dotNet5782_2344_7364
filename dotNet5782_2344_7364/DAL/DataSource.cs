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
        /// this class holds all the data of the program in arrays
        /// this class also creats random data to start program
        /// </summary>

        #region // 5 arrays for storing data
        internal static List<Drone> DroneList; // list of drones
        internal static List<Drone> BaseStationList; // list of base stations
        internal static List<Drone> CustomerList; // list of customers
        internal static List<Drone> ParcelList; // list of parcel
        internal static List<Drone> DroneChargeList; // list of drone charges
        #endregion
        internal class Config // hold index of next free index in array
        {
            /// <summary>
            /// hold index of next free index in array
            /// </summary>
            
            #region// index for arrays and parcel id
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
            BaseStationList[Config.BaseStationArrIndex] = newBaseStation; // add to array
            Config.BaseStationArrIndex++;// edvance index to next free cell
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
            newDrone.Model = $"modle_number{Config.DroneArrIndex + 1}";
            newDrone.MaxWeight = (WeightCategories)rnd.Next(0, 3);
           // newDrone.Status = (DroneStatuses)rnd.Next(0, 3);
           // newDrone.Battery = rnd.Next(0, 101);
            DroneList[Config.DroneArrIndex] = newDrone; // add to array
            Config.DroneArrIndex++;// edvance index to next free cell
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
            newParcel.SenderId = CustomerList[rnd.Next(0, Config.CustomerArrIndex)].Id;
            newParcel.TargetId = CustomerList[rnd.Next(0, Config.CustomerArrIndex)].Id;
            newParcel.Weight = (WeightCategories)rnd.Next(0, 3);
            newParcel.Priority = (Priorities)rnd.Next(0, 3);
            newParcel.Requsted = DateTime.Now;
            newParcel.DroneId = 0;
            newParcel.Scheduled = DateTime.Now.AddHours(rnd.Next(1, 23));
            newParcel.PickedUp = DateTime.MinValue;
            newParcel.Deliverd = DateTime.MinValue;
            ParcelList[Config.ParcelArrIndex] = newParcel; // add to array
            Config.ParcelArrIndex++; // edvance index to next free cell
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
            CustomerList[DataSource.Config.CustomerArrIndex] = newCustomer;
            Config.CustomerArrIndex++; // edvance index to next free cell
            #endregion
        }
        /// <summary>
        /// call construct drone charge
        /// </summary>
        internal static void CreatDroneCharge() // call construct drone charge
        {
            #region // call construct drone charge
            DroneCharge newDroneCharge = new DroneCharge // creat new drone with negative value
            {
                DroneId = -1,
                StationId = -1
            };
            for (int i = 0; i < Config.DroneChargeArrSize; i++) // set all cells in array to be free (-1)
            {             
                DroneChargeList[i] = newDroneCharge;                
            }
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
                CreatDroneCharge();
            }
            #endregion
        }
    }
}

