using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObjects
{
    public class DalObjects
    {
        public DalObjects() // constructor
        {
            DataSource.Initialize();
        }
        public static void ConstructBaseStation(int id, string name, int chargeSlots, double longtitude, double latitude) // construct a new base station
        {
            BaseStation new_base_station = new BaseStation
            {
                Id = id,
                Name = name,
                ChargeSlots = chargeSlots,
                Longtitude = longtitude,
                Latitude = latitude
            };
            DataSource.BaseStation_arr[DataSource.Config.BaseStation_arr_index] = new_base_station;
            DataSource.Config.BaseStation_arr_index++;
        }

        public static void ConstructDrone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery) // construct a new drone
        {
            Drone new_drone = new Drone
            {
                Id = id,
                Model = model,
                MaxWeight = maxWeight,
                Status = status,
                Battery = battery,
            };
            DataSource.Drone_arr[DataSource.Config.Drone_arr_index] = new_drone;
            DataSource.Config.Drone_arr_index++;
        }

        public static void ConstructParcel(int id, int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime request, int droneId, DateTime schedual, DateTime pickUp, DateTime deliverd) // construct a new parcel
        {
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
            DataSource.Parcel_arr[DataSource.Config.Parcel_arr_index] = new_parcel;
            DataSource.Config.Parcel_arr_index++;
        }

        public static void ConstructCustomer(int id, string name, string phone, double longtitude, double latitude) // construct a new customer
        {
            Customer new_customer = new Customer
            {
                Id = id,
                Name = name,
                Phone = phone,
                Longtitude = longtitude,
                Latitude = latitude
            };
            DataSource.Customer_arr[DataSource.Config.Customer_arr_index] = new_customer;
            DataSource.Config.Customer_arr_index++;
        }
        public static int Get_Drone_arr_index()
        {
            return DataSource.Config.Drone_arr_index;
        }
        public static int Get_BaseStation_arr_index()
        {
            return DataSource.Config.BaseStation_arr_index;
        }
        public static int Get_Customer_arr_index()
        {
            return DataSource.Config.Customer_arr_index;
        }
        public static int Get_Parcel_arr_index()
        {
            return DataSource.Config.Parcel_arr_index;
        }
        public static void Print_Drone(int index)
        {
            Console.WriteLine(DataSource.Drone_arr[index].ToString());
        }
        public static void Print_BaseStation(int index)
        {
            Console.WriteLine(DataSource.BaseStation_arr[index].ToString());
        }
        public static void Print_Customer(int index)
        {
            Console.WriteLine(DataSource.Customer_arr[index].ToString());
        }
        public static void Print_Parcel(int index)
        {
            Console.WriteLine(DataSource.Parcel_arr[index].ToString());
        }
        public static void Associate_Drone_to_Parcel(int id)
        {
            int index; 
            for (index = 0; index < DataSource.Config.Parcel_arr_index; index++)
            {
                if (DataSource.Parcel_arr[index].Id == id)
                {
                    break;
                }
            }
            WeightCategories Requested_weight = DataSource.Parcel_arr[index].Weight;
            for (int i = 0; i < DataSource.Config.Drone_arr_index; i++)
            {
                if (DataSource.Drone_arr[i].Status == DroneStatuses.Available && DataSource.Drone_arr[i].MaxWeight >= Requested_weight)
                {
                    DataSource.Drone_arr[i].Status = DroneStatuses.Delivery;
                    DataSource.Parcel_arr[index].DroneId = DataSource.Drone_arr[i].Id;
                }
            }
        }
        public static void Print_not_associate()
        {
            for (int i = 0; i < DataSource.Config.Parcel_arr_index; i++)
            {
                if (DataSource.Parcel_arr[i].DroneId == 0)
                {
                    Print_Parcel(i);
                }
            }
        }
        public static void Print_free_BaseStation()
        {
            for (int i = 0; i < DataSource.Config.BaseStation_arr_index; i++)
            {
                if (DataSource.BaseStation_arr[i].ChargeSlots != 0)
                {
                    Print_BaseStation(i);
                }
            }
        }
        public static void Update_Parcle_pickup(int id)
        {
            for (int i = 0; i < DataSource.Config.Parcel_arr_index; i++)
            {
                if (DataSource.Parcel_arr[i].Id == id)
                {
                    DataSource.Parcel_arr[i].PickedUp = DateTime.Now;
                }
            }
        }
        public static void Update_Parcle_delivery(int id)
        {
            for (int i = 0; i < DataSource.Config.Parcel_arr_index; i++)
            {
                if (DataSource.Parcel_arr[i].Id == id)
                {
                    DataSource.Parcel_arr[i].Deliverd = DateTime.Now;
                }
            }
        }
    }
}
