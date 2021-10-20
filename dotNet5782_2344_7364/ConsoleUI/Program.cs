using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DalObjects;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            DalObjects.DalObjects.Start_program(); // start program with random values
            DalObjects.DalObjects.Print_intro(); // print ascii art
            DalObjects.DalObjects.Print_menu(); // print menu
            while (true) // untill user enters 5 continue running
            {
                string User_input = Console.ReadLine(); // read user input
                switch (User_input)
                {
                    case ("1.1")://BaseStation addition
                        #region// BaseStation addition
                        Console.WriteLine("Please enter following data: \n 1) Id \n 2) Name \n 3) ChargeSlots \n 4) Longtitude \n 5) Latitude ");                      
                        int User_id;
                        int.TryParse(Console.ReadLine(), out User_id);
                        string User_name = Console.ReadLine();
                        int User_chargeSlots;
                        int.TryParse(Console.ReadLine(), out User_chargeSlots);
                        string User_longtitude = Console.ReadLine();
                        string User_latitude = Console.ReadLine();
                        DalObjects.DalObjects.ConstructBaseStation(User_id, User_name, User_chargeSlots, User_longtitude, User_latitude);
                        break;
                        #endregion
                    case ("1.2")://Drone addition
                        #region// Drone addition
                        Console.WriteLine("Please enter following data: \n 1) Id \n 2) Model \n 3) WeightCategories(1/2/3) \n 4) DroneStatuses(1,2,3) \n 5) battery ");
                        int User_DroneId;
                        int.TryParse(Console.ReadLine(), out User_DroneId);
                        string User_model = Console.ReadLine();
                        WeightCategories User_WeightCategories;
                        WeightCategories.TryParse(Console.ReadLine(), out User_WeightCategories);
                        DroneStatuses User_DroneStatuses;
                        DroneStatuses.TryParse(Console.ReadLine(), out User_DroneStatuses);
                        double battery;
                        double.TryParse(Console.ReadLine(), out battery);
                        DalObjects.DalObjects.ConstructDrone(User_DroneId, User_model, User_WeightCategories, User_DroneStatuses, battery);
                        break;
                    #endregion
                    case ("1.3")://Customer addition
                        #region// Customer addition
                        Console.WriteLine("Please enter following data: \n 1) Id \n 2) Name \n 3) phone number \n 4) Longtitude \n 5) Latitude ");                       
                        int User_CustomerId;
                        int.TryParse(Console.ReadLine(), out User_CustomerId);
                        string User_CustomerName = Console.ReadLine();
                        string User_PhoneNumber = Console.ReadLine();
                        string User_CustomerLongtitude = Console.ReadLine();                        
                        string User_CustomerLatitude = Console.ReadLine();                
                        DalObjects.DalObjects.ConstructCustomer(User_CustomerId, User_CustomerName, User_PhoneNumber, User_CustomerLatitude, User_CustomerLatitude);
                        break;
                        #endregion
                    case ("1.4")://Parcel addition
                        #region// Parcel addition
                        Console.WriteLine("Please enter following data: \n 1) Id \n 2) sender id \n 3) target id \n 4) wight categories \n 5) priorities \n 6)requested time ");                       
                        int User_ParcelId;
                        int.TryParse(Console.ReadLine(), out User_ParcelId);
                        int User_SenderId;
                        int.TryParse(Console.ReadLine(), out User_SenderId);
                        int User_TargetId;
                        int.TryParse(Console.ReadLine(), out User_TargetId);
                        WeightCategories User_ParcelWeightCategories;
                        WeightCategories.TryParse(Console.ReadLine(), out User_ParcelWeightCategories);
                        Priorities User_ParcelPriorities;
                        Priorities.TryParse(Console.ReadLine(), out User_ParcelPriorities);
                        DateTime User_Requested;
                        DateTime.TryParse(Console.ReadLine(), out User_Requested);
                        DalObjects.DalObjects.ConstructParcel(User_ParcelId, User_SenderId, User_TargetId, User_ParcelWeightCategories, User_ParcelPriorities, User_Requested, 0, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                        break;
                      #endregion
                    case ("2.1"): //Update Percel to Drone
                        #region//Update Percel to Drone
                        Console.WriteLine("Enter a Parcel id to associate");
                        int Parcel_id;
                        int.TryParse(Console.ReadLine(), out Parcel_id);
                        DalObjects.DalObjects.Associate_Drone_to_Parcel(Parcel_id);
                        break;
                        #endregion
                    case ("2.2")://Update Parcle pickup
                        #region//Update Parcle pickup
                        int Parcel_id1;
                        int.TryParse(Console.ReadLine(), out Parcel_id1);
                        DalObjects.DalObjects.Update_Parcle_pickup(Parcel_id1);
                        break;
                        #endregion
                    case ("2.3")://Update Parcle delivery
                        #region//Update Parcle delivery
                        int Parcel_id2;
                        int.TryParse(Console.ReadLine(), out Parcel_id2);
                        DalObjects.DalObjects.Update_Parcle_delivery(Parcel_id2);
                        break;
                        #endregion
                    case ("2.4")://Print free BaseStation
                        #region//Print free BaseStation
                        DalObjects.DalObjects.Print_free_BaseStation();
                        int Drone_id;
                        int.TryParse(Console.ReadLine(), out Drone_id);
                        int Base_id;
                        int.TryParse(Console.ReadLine(), out Base_id);
                        DalObjects.DalObjects.Update_DroneCharge(Drone_id, Base_id);
                        break;
                        #endregion
                    case ("2.5")://release DroneCharge
                        #region//release DroneCharge
                        DalObjects.DalObjects.Print_free_BaseStation();
                        int Drone_id1;
                        int.TryParse(Console.ReadLine(), out Drone_id1);
                        int Base_id1;
                        int.TryParse(Console.ReadLine(), out Base_id1);
                        DalObjects.DalObjects.Release_DroneCharge(Drone_id1, Base_id1);
                        break;
                        #endregion
                    case ("3.1")://Print BaseStation
                        #region//Print BaseStation
                        int Base_id2;
                        int.TryParse(Console.ReadLine(), out Base_id2);
                        DalObjects.DalObjects.Print_BaseStation(Base_id2);
                        break;
                        #endregion
                    case ("3.2")://Print Drone
                        #region//Print Drone
                        int Drone_id2;
                        int.TryParse(Console.ReadLine(), out Drone_id2);
                        DalObjects.DalObjects.Print_Drone(Drone_id2);
                        break;
                        #endregion
                    case ("3.3")://Print Customer
                        #region//Print Customer
                        int Customer_id;
                        int.TryParse(Console.ReadLine(), out Customer_id);
                        DalObjects.DalObjects.Print_Customer(Customer_id);                       
                        break;
                        #endregion
                    case ("3.4")://Print Parcel
                        #region//Print Parcel
                        int Parcel_id3;
                        int.TryParse(Console.ReadLine(), out Parcel_id3);
                        DalObjects.DalObjects.Print_Parcel(Parcel_id3);
                        break;
                        #endregion
                    case ("4.1")://Display list of base stations
                        #region//Display list of base stations
                        for (int i = 0; i < DalObjects.DalObjects.Get_BaseStation_arr_index(); i++)
                        {
                            DalObjects.DalObjects.Print_BaseStation(i);
                        }
                        break;
                        #endregion
                    case ("4.2")://Display list of drones
                        #region//Display list of drones
                        for (int i = 0; i < DalObjects.DalObjects.Get_Drone_arr_index(); i++)
                        {
                            DalObjects.DalObjects.Print_Drone(i);
                        }
                        break;
                        #endregion
                    case ("4.3")://Display list of customers
                        #region//Display list of customers
                        for (int i = 0; i < DalObjects.DalObjects.Get_Customer_arr_index(); i++)
                        {
                            DalObjects.DalObjects.Print_Customer(i);
                        }
                        break;
                        #endregion
                    case ("4.4")://Display list of parcels
                        #region//Display list of parcels
                        for (int i = 0; i < DalObjects.DalObjects.Get_Parcel_arr_index(); i++)
                        {
                            DalObjects.DalObjects.Print_Parcel(i);
                        }
                        break;
                        #endregion
                    case ("4.5")://Display list of parcels who are not associated to a drone
                        #region//Display list of parcels who are not associated to a drone
                        DalObjects.DalObjects.Print_not_associate();
                        break;
                        #endregion
                    case ("4.6")://Display list of base stations with free charging stations
                        #region//Display list of base stations with free charging stations
                        DalObjects.DalObjects.Print_free_BaseStation();
                        break;
                        #endregion
                    case ("5")://exit
                        #region//exit
                        return;
                    #endregion
                    default:
                        break;
                }
            }
        }
    }
}

