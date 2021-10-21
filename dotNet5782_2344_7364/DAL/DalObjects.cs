using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObjects
{
    /// <summary>
    /// Holds functions that allow to acsses data
    /// </summary>
    public class DalObjects
    {
        /// <summary>
        /// contains functions that allow to acsses and change the data in DataSource
        /// </summary>
        public DalObjects() // constructor.
        {
            DataSource.Initialize();
        }
        public static void ConstructBaseStation(int id, string name, int chargeSlots, double longtitude, double latitude) // construct a new base station
        {
            #region//construct a new base station
            BaseStation new_base_station = new BaseStation
            {
                Id = id,
                Name = name,
                ChargeSlots = chargeSlots,
                Longtitude = DataSource.sexagesimal(longtitude, 'N'),
                Latitude = DataSource.sexagesimal(latitude, 'E')
            };
            DataSource.BaseStation_arr[DataSource.Config.BaseStation_arr_index] = new_base_station; // add to array
            DataSource.Config.BaseStation_arr_index++; // edvance index to next free cell
            #endregion
        }
        public static void ConstructDrone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery) // construct a new drone
        {
            #region//construct a new drone
            Drone new_drone = new Drone
            {
                Id = id,
                Model = model,
                MaxWeight = maxWeight,
                Status = status,
                Battery = battery,
            };
            DataSource.Drone_arr[DataSource.Config.Drone_arr_index] = new_drone; // add to array
            DataSource.Config.Drone_arr_index++; // edvance index to next free cell
            #endregion
        }
        public static void ConstructParcel(int id, int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime request, int droneId, DateTime schedual, DateTime pickUp, DateTime deliverd) // construct a new parcel
        {
            #region//construct a new parcel
            Parcel new_parcel = new Parcel
            {
                Id = id,
                SenderId = senderId,
                TargetId = targetId,
                Weight = weight,
                Priority = priority,
                Requsted = request,
                DroneId = droneId,
                Scheduled = schedual,
                PickedUp = pickUp,
                Deliverd = deliverd
            };
            DataSource.Parcel_arr[DataSource.Config.Parcel_arr_index] = new_parcel; // add to array
            DataSource.Config.Parcel_arr_index++; // edvance index to next free cell
            DataSource.Config.Parcel_id++; // grow runing number by 1
            #endregion
        }
        public static void ConstructCustomer(int id, string name, string phone, double longtitude, double latitude) // construct a new customer
        {
            #region//construct a new customer
            Customer new_customer = new Customer
            {
                Id = id,
                Name = name,
                Phone = phone,
                Longtitude = DataSource.sexagesimal(longtitude, 'N'),
                Latitude = DataSource.sexagesimal(latitude, 'E')
            };
            DataSource.Customer_arr[DataSource.Config.Customer_arr_index] = new_customer; // add to array
            DataSource.Config.Customer_arr_index++; // edvance index to next free cell
            #endregion
        }
        public static void ConstructDroneCharge(int droneId, int stationId) // construct a new drone charge
        {
            #region//construct a new drone charge
            DroneCharge new_droneCharge = new DroneCharge
            {
                DroneId = droneId,
                StationId = stationId
            };
            DataSource.DroneCharge_arr[DataSource.Config.DroneCharge_arr_index] = new_droneCharge; // add to array
            DataSource.Config.DroneCharge_arr_index++; // edvance index to next free cell
            #endregion
        }
        public static int Get_Drone_arr_index() // returns index of next free cell in drone arr
        {
            return DataSource.Config.Drone_arr_index;
        }
        public static int Get_BaseStation_arr_index() // returns index of next free cell in base station arr
        {
            return DataSource.Config.BaseStation_arr_index;
        } 
        public static int Get_Customer_arr_index() // returns index of next free cell in customer arr
        {
            return DataSource.Config.Customer_arr_index;
        }  
        public static int Get_Parcel_arr_index() // returns index of next free cell in parcel arr
        {
            return DataSource.Config.Parcel_arr_index;
        }
        public static void Print_Drone(int id) // find drone in array and print it
        {
            #region//find drone in array and print it
            int index;
            for (index = 0; index < DataSource.Config.Drone_arr_index; index++) // search drone by id
            {
                if (DataSource.Drone_arr[index].Id == id)
                {
                    break;
                }
            }
            Console.WriteLine(DataSource.Drone_arr[index].ToString()); // print
            #endregion
        }
        public static void Print_BaseStation(int id) // find base station in array and print it
        {
            #region//find base station in array and print it
            int index;
            for (index = 0; index < DataSource.Config.BaseStation_arr_index; index++) // search base station by id
            {
                if (DataSource.BaseStation_arr[index].Id == id)
                {
                    break;
                }
            }
            Console.WriteLine(DataSource.BaseStation_arr[index].ToString()); // print
            #endregion
        }
        public static void Print_Customer(int id) // find customer in array and print it
        {
            #region//find customer in array and print it
            int index;
            for (index = 0; index < DataSource.Config.Customer_arr_index; index++) // search customer by id
            {
                if (DataSource.Customer_arr[index].Id == id)
                {
                    break;
                }
            }
            Console.WriteLine(DataSource.Customer_arr[index].ToString()); // print
            #endregion
        }
        public static void Print_Parcel(int id)// find parcel in array and print it
        {
            #region//find parcel in array and print it
            int index;
            for (index = 0; index < DataSource.Config.Parcel_arr_index; index++) // search parcel by id
            {
                if (DataSource.Parcel_arr[index].Id == id)
                {
                    break;
                }
            }
            Console.WriteLine(DataSource.Parcel_arr[index].ToString()); // print
            #endregion
        }
        public static void Associate_Drone_to_Parcel(int id) // associate a drone to a parcel
        {
            #region//associate a drone to a parcel
            int index;
            for (index = 0; index < DataSource.Config.Parcel_arr_index; index++) // find parcel by id
            {
                if (DataSource.Parcel_arr[index].Id == id)
                {
                    break;
                }
            }
            WeightCategories Requested_weight = DataSource.Parcel_arr[index].Weight; // set requested weight by weight categories
            for (int i = 0; i < DataSource.Config.Drone_arr_index; i++) // search for a drone
            {
                if (DataSource.Drone_arr[i].Status == DroneStatuses.Available && DataSource.Drone_arr[i].MaxWeight >= Requested_weight) // if is avalibale and maches weight
                {
                    DataSource.Drone_arr[i].Status = DroneStatuses.Delivery;
                    DataSource.Parcel_arr[index].DroneId = DataSource.Drone_arr[i].Id;
                    break;
                }
            }
            #endregion
        }
        public static void Print_list_of_BaseStations() // print all the base stations in array
        {
            for (int i = 0; i < DataSource.Config.BaseStation_arr_index; i++)
            {
                Console.WriteLine(DataSource.BaseStation_arr[i].ToString());
            }
        }
        public static void Print_list_of_Drones() // print all the drones in array
        {
            for (int i = 0; i < DataSource.Config.Drone_arr_index; i++)
            {
                Console.WriteLine(DataSource.Drone_arr[i].ToString());
            }
        }
        public static void Print_list_of_Customers() // print all the customers in array
        {
            for (int i = 0; i < DataSource.Config.Customer_arr_index; i++)
            {
                Console.WriteLine(DataSource.Customer_arr[i].ToString());
            }
        }
        public static void print_list_of_Parcels() // print all the parcels in array
        {
            for (int i = 0; i < DataSource.Config.Parcel_arr_index; i++)
            {
                Console.WriteLine(DataSource.Parcel_arr[i].ToString());
            }
        }
        public static void Print_not_associate() // print all the parcels that are not associated to a drone
        {
            #region//print all the parcels that are not associated to a drone
            bool flag = true; // flag
            for (int i = 0; i < DataSource.Config.Parcel_arr_index; i++) // for all parcels in array
            {
                if (DataSource.Parcel_arr[i].DroneId == 0) // if not associated
                {
                    Console.WriteLine(DataSource.Parcel_arr[i].ToString()); // print
                    flag = false;
                }
            }
            if (flag) // if all parcels have been associated to a drone
            {
                Console.WriteLine(" All parcels have been associated to a drone ");
            }
            #endregion
        }
        public static void Print_free_BaseStation() // print all the charge base stations that are not full
        {
            #region//print all the charge base stations that are not full
            bool flag = true; // flag
            for (int i = 0; i < DataSource.Config.BaseStation_arr_index; i++) // for all base stations in array
            {
                if (DataSource.BaseStation_arr[i].ChargeSlots != 0) // if free
                {
                    Console.WriteLine(DataSource.BaseStation_arr[i].ToString()); // print
                    flag = false;
                }
            }
            if (flag) // if all base station charging slots are full
            {
                Console.WriteLine(" All base station charging slots are full ");
            }
            #endregion
        }
        public static void Update_Parcle_pickup(int id) // update parcel pickup
        {
            #region//update parcel pickup
            for (int i = 0; i < DataSource.Config.Parcel_arr_index; i++) // search
            {
                if (DataSource.Parcel_arr[i].Id == id) // if id match
                {
                    DataSource.Parcel_arr[i].PickedUp = DateTime.Now; // currnt time
                    break;
                }
            }
            #endregion
        }
        public static void Update_Parcle_delivery(int id) // update parcel delivery
        {
            #region//update parcel delivery
            for (int i = 0; i < DataSource.Config.Parcel_arr_index; i++)// search
            {
                if (DataSource.Parcel_arr[i].Id == id) // if id match
                {
                    DataSource.Parcel_arr[i].Deliverd = DateTime.Now;  // currnt time
                    break;
                }
            }
            #endregion
        }
        public static void Update_DroneCharge(int droneId, int stationId) // update drones charging
        {
            #region//update drones charging
            ConstructDroneCharge(droneId, stationId); // call construct
            for (int i = 0; i < DataSource.Config.Drone_arr_index; i++) // search
            {
                if (DataSource.Drone_arr[i].Id == droneId) // if id match
                {
                    DataSource.Drone_arr[i].Status = DroneStatuses.Maintenance;
                    break;
                }
            }
            for (int i = 0; i < DataSource.Config.BaseStation_arr_index; i++) // search
            {
                if (DataSource.BaseStation_arr[i].Id == stationId) // if id match
                {
                    DataSource.BaseStation_arr[i].ChargeSlots -= 1;
                    break;
                }
            }
            #endregion
        }

        public static void Release_DroneCharge(int droneId, int stationId) // releas drone from charging
        {
            #region//releas drone from charging
            bool flag =false; // flag
            for (int i = 0; i < DataSource.Config.DroneCharge_arr_index; i++)
            {
                if (DataSource.DroneCharge_arr[i].DroneId == droneId && DataSource.DroneCharge_arr[i].StationId == stationId)
                {
                    DroneCharge[] New_DroneCharge_arr = new DroneCharge[10]; // array of drone charges
                    if ((DataSource.Config.DroneCharge_arr_index - 1) == i)
                    {
                        for(int j=0;j< (DataSource.Config.DroneCharge_arr_index-1);j++)
                        {
                            New_DroneCharge_arr[j] = DataSource.DroneCharge_arr[j];
                        }
                        for (int j = 0; j < (DataSource.Config.DroneCharge_arr_index); j++)
                        {
                            DataSource.DroneCharge_arr[j] = New_DroneCharge_arr[j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < (DataSource.Config.DroneCharge_arr_index-1); j++)
                        {
                            if(j==i)
                            {
                                flag = true;
                            }
                            if(flag == false)
                            {
                                New_DroneCharge_arr[j] = DataSource.DroneCharge_arr[j];
                            }
                            else
                            {
                                New_DroneCharge_arr[j] = DataSource.DroneCharge_arr[j+1];
                            }
                        }
                        for (int j = 0; j < (DataSource.Config.DroneCharge_arr_index); j++)
                        {
                            DataSource.DroneCharge_arr[j] = New_DroneCharge_arr[j];
                        }
                    }
                }
            }       
            for (int i = 0; i < DataSource.Config.Drone_arr_index; i++)
            {
                if (DataSource.Drone_arr[i].Id == droneId)
                {
                    DataSource.Drone_arr[i].Status = DroneStatuses.Available;
                    break;
                }
            }
            for (int i = 0; i < DataSource.Config.BaseStation_arr_index; i++)
            {
                if (DataSource.BaseStation_arr[i].Id == stationId)
                {
                    DataSource.BaseStation_arr[i].ChargeSlots += 1;
                    break;
                }
            }
            #endregion
        }
        public static void Start_program() { DataSource.Initialize(); } // call initialize to start program with random data
        public static void Print_menu() // print option menu
        {
            Console.WriteLine("TO SELECT OPTION ENTER SECTION NUMBER\n\n1) ADDING OPTIONS:\n\t1.1) Add base station\n\t1.2) Add drone\n\t1.3) Add customer\n\t1.4) Add parcel\n\n2) UPDATE OPTIONS:\n\t2.1) Assign parcel to customer\n\t2.2) Collect parcel by drone\n\t2.3) Deliver parcel to customer\n\t2.4) Send drone to charge at base station\n\t2.5) Release drone from charging\n\n3) DISPLAY DATA:\n\t3.1) Display base station\n\t3.2) Display drone\n\t3.3) Display customer\n\t3.4) Display parcel\n\n4) DISPLAY LISTS\n\t4.1) Display list of base stations\n\t4.2) Display list of drones\n\t4.3) Display list of customers\n\t4.4) Display list of parcels\n\t4.5) Display list of parcels that are not assigned to drone\n\t4.6) Display list of base stations with free charging stations\n\n5) EXIT\n");
        }
        public static void Print_intro() // print a welcome intro
        {
            #region//print a welcome intro
            Console.WriteLine
                ( 
                 "                 _                                             \n   " +
                  "             | |                                              \n   " +
                 "__      _____| | ___ ___  _ __ ___   ___                     \n   " +
                 "\\ \\ /\\ /  / _\\ |/ __/ _ \\| '_ ` _ \\ / _ \\              \n   " +
                  " \\ V  V  / __/ | (_| (_) | | | | | | __ /                     \n   " +
                  "  \\_ /\\_/\\___|_|\\___\\___/|_| |_| |_|\\___|     \n \n \n  " +




                 "                 __/\\__             \n   " +
                 "               `==/\\==`             \n  " +
                 "      ____________/__\\____________    \n " +
                 "      /____________________________\\    \n " +
                 "        __||__||__/.--.\\__||__||__       \n  " +
                 "      /__|___|___( >< )___|___|__\\       \n " +
                 "                 _/`--`\\_                  \n " +
                 "                (/------\\)                  \n "
                             );



            #endregion
        }
    }
}
