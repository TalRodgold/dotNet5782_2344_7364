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
            for (int i = 0; i < DataSource.Config.DroneCharge_arr_size; i++)
            {
                if (DataSource.DroneCharge_arr[i].DroneId == -1 && DataSource.DroneCharge_arr[i].StationId == -1)
                {
                    DataSource.DroneCharge_arr[i].DroneId = droneId; // add to array id
                    DataSource.DroneCharge_arr[i].StationId = stationId; // add to array id
                    break;
                }
            }   
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
        public static int Find_Drone_by_id(int id) // find drone in array 
        {
            #region//find drone in array
            for (int index = 0; index < DataSource.Config.Drone_arr_index; index++) // search drone by id
            {
                if (DataSource.Drone_arr[index].Id == id)
                {
                    return index; // return index
                }
            }
            return -1; // if not found
            #endregion
        }
        public static int Find_BaseStation_by_id(int id) // find BaseStation in array 
        {
            #region//find BaseStation in array
            for (int index = 0; index < DataSource.Config.BaseStation_arr_index; index++) // search BaseStation by id
            {
                if (DataSource.BaseStation_arr[index].Id == id)
                {
                    return index; // return index
                }
            }
            return -1; // if not found
            #endregion
        }
        public static int Find_Customer_by_id(int id) // find Customer in array 
        {
            #region//find Customer in array
            for (int index = 0; index < DataSource.Config.Customer_arr_index; index++) // searchCustomer by id
            {
                if (DataSource.Customer_arr[index].Id == id)
                {
                    return index; // return index
                }
            }
            return -1; // if not found
            #endregion
        }
        public static int Find_Parcel_by_id(int id) // find Parcel in array 
        {
            #region//find Parcelin array
            for (int index = 0; index < DataSource.Config.Parcel_arr_index; index++) // Parcel by id
            {
                if (DataSource.Parcel_arr[index].Id == id)
                {
                    return index; // return index
                }
            }
            return -1; // if not found
            #endregion
        }
        public static void Print_Drone(int id) // find drone in array and print it
        {
            #region//find drone in array and print it        
            Console.WriteLine(DataSource.Drone_arr[Find_Drone_by_id(id)].ToString()); // print
            #endregion
        }
        public static void Print_BaseStation(int id) // find base station in array and print it
        {
            #region//find base station in array and print it
            Console.WriteLine(DataSource.BaseStation_arr[Find_BaseStation_by_id(id)].ToString()); // print
            #endregion
        }
        public static void Print_Customer(int id) // find customer in array and print it
        {
            #region//find customer in array and print it         
            Console.WriteLine(DataSource.Customer_arr[Find_Customer_by_id(id)].ToString()); // print
            #endregion
        }
        public static void Print_Parcel(int id)// find parcel in array and print it
        {
            #region//find parcel in array and print it     
            Console.WriteLine(DataSource.Parcel_arr[Find_Parcel_by_id(id)].ToString()); // print
            #endregion
        }
        public static void Associate_Drone_to_Parcel(int drone_id, int parcle_id) // associate a drone to a parcel
        {
            #region//associate a drone to a parcel
            DataSource.Parcel_arr[Find_Parcel_by_id(parcle_id)].DroneId = drone_id;
            DataSource.Drone_arr[Find_Drone_by_id(drone_id)].Status = DroneStatuses.Delivery;
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
             DataSource.Parcel_arr[Find_Parcel_by_id(id)].PickedUp = DateTime.Now; // currnt time  
            #endregion
        }
        public static void Update_Parcle_delivery(int id) // update parcel delivery
        {
            #region//update parcel delivery
            DataSource.Parcel_arr[Find_Parcel_by_id(id)].Deliverd = DateTime.Now;  // currnt time
            DataSource.Drone_arr[Find_Drone_by_id(DataSource.Parcel_arr[Find_Parcel_by_id(id)].DroneId)].Status = DroneStatuses.Available; // change drone status to avilable

            #endregion
        }
        public static void Update_DroneCharge(int droneId, int stationId) // update drones charging
        {
            #region//update drones charging
            ConstructDroneCharge(droneId, stationId); // call construct
            DataSource.Drone_arr[Find_Drone_by_id(droneId)].Status = DroneStatuses.Maintenance; // update drone status
            DataSource.BaseStation_arr[Find_BaseStation_by_id(stationId)].ChargeSlots -= 1;   // change number of free charge slots
            #endregion
        }
        public static void Release_DroneCharge(int droneId, int stationId) // releas drone from charging
        {
            #region//releas drone from charging
            DataSource.Drone_arr[Find_Drone_by_id(droneId)].Status = DroneStatuses.Available; // make drone status: Available
            DataSource.BaseStation_arr[Find_BaseStation_by_id(stationId)].ChargeSlots++;
            for (int index = 0; index < DataSource.Config.DroneCharge_arr_size; index++) // search drone by id in drone charge
            {
                if (DataSource.DroneCharge_arr[index].DroneId == droneId)
                {
                    DataSource.DroneCharge_arr[index].DroneId = -1; // set to availeble
                    DataSource.DroneCharge_arr[index].StationId = -1; // set to availeble
                    break;
                }
            }
            #endregion
        }
        public static void Start_program() { DataSource.Initialize(); } // call initialize to start program with random data
    }
}
