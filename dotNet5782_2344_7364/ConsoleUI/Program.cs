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
        /// <summary>
        /// main program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DalObjects.DalObjects.Start_program(); // start program with random values
            print_func(1); // print ascii art.
            print_func(2); // print menu
            while (true) // untill user enters 5 continue running
            {
                print_func(3);// print choice request
                string User_input = Console.ReadLine(); // read user input
                switch (User_input)
                {
                    case ("1.1")://BaseStation addition
                        #region// BaseStation addition
                        print_func(5); // print to user data he needs to type in                   
                        int User_id;
                        int.TryParse(Console.ReadLine(), out User_id);
                        string User_name = Console.ReadLine();
                        int User_chargeSlots;
                        int.TryParse(Console.ReadLine(), out User_chargeSlots);
                        double User_longtitude;
                        double.TryParse(Console.ReadLine(), out User_longtitude);
                        double User_latitude;
                        double.TryParse(Console.ReadLine(), out User_latitude);
                        DalObjects.DalObjects.ConstructBaseStation(User_id, User_name, User_chargeSlots, User_longtitude, User_latitude);
                        break;
                        #endregion
                    case ("1.2")://Drone addition
                        #region// Drone addition
                        print_func(6); // print to user data he needs to type in  
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
                        print_func(7); // print to user data he needs to type in 
                        int User_CustomerId;
                        int.TryParse(Console.ReadLine(), out User_CustomerId);
                        string User_CustomerName = Console.ReadLine();
                        string User_PhoneNumber = Console.ReadLine();
                        double User_CustomerLongtitude;
                        double.TryParse(Console.ReadLine(), out User_CustomerLongtitude);
                        double User_CustomerLatitude;
                        double.TryParse(Console.ReadLine(), out User_CustomerLatitude);                                     
                        DalObjects.DalObjects.ConstructCustomer(User_CustomerId, User_CustomerName, User_PhoneNumber, User_CustomerLatitude, User_CustomerLatitude);
                        break;
                        #endregion
                    case ("1.4")://Parcel addition
                        #region// Parcel addition
                        print_func(8); // print to user data he needs to type in 
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
                        print_func(9); 
                        int Parcel_id;
                        int.TryParse(Console.ReadLine(), out Parcel_id);
                        print_func(11);
                        int drone_id;
                        int.TryParse(Console.ReadLine(), out drone_id);
                        DalObjects.DalObjects.Associate_Drone_to_Parcel( drone_id, Parcel_id);
                        break;
                        #endregion
                    case ("2.2")://Update Parcle pickup
                        #region//Update Parcle pickup
                        print_func(9);
                        int Parcel_id1;
                        int.TryParse(Console.ReadLine(), out Parcel_id1);
                        DalObjects.DalObjects.Update_Parcle_pickup(Parcel_id1);
                        break;
                        #endregion
                    case ("2.3")://Update Parcle delivery
                        #region//Update Parcle delivery
                        print_func(9);
                        int Parcel_id2;
                        int.TryParse(Console.ReadLine(), out Parcel_id2);
                        DalObjects.DalObjects.Update_Parcle_delivery(Parcel_id2);
                        break;
                        #endregion
                    case ("2.4")://Print free BaseStation
                        #region//Print free BaseStation
                        DalObjects.DalObjects.Print_free_BaseStation();
                        print_func(11);
                        int Drone_id;
                        int.TryParse(Console.ReadLine(), out Drone_id);
                        print_func(10);
                        int Base_id;
                        int.TryParse(Console.ReadLine(), out Base_id);
                        DalObjects.DalObjects.Update_DroneCharge(Drone_id, Base_id);
                        break;
                        #endregion
                    case ("2.5")://release DroneCharge
                        #region//release DroneCharge
                        print_func(11);
                        int Drone_id1;
                        int.TryParse(Console.ReadLine(), out Drone_id1);
                        print_func(10);
                        int Base_id1;
                        int.TryParse(Console.ReadLine(), out Base_id1);
                        DalObjects.DalObjects.Release_DroneCharge(Drone_id1, Base_id1);
                        break;
                        #endregion
                    case ("3.1")://Print BaseStation
                        #region//Print BaseStation
                        print_func(10);
                        int Base_id2;
                        int.TryParse(Console.ReadLine(), out Base_id2);
                        DalObjects.DalObjects.Print_BaseStation(Base_id2);
                        break;
                        #endregion
                    case ("3.2")://Print Drone
                        #region//Print Drone
                        print_func(11);
                        int Drone_id2;
                        int.TryParse(Console.ReadLine(), out Drone_id2);
                        DalObjects.DalObjects.Print_Drone(Drone_id2);
                        break;
                        #endregion
                    case ("3.3")://Print Customer
                        #region//Print Customer
                        print_func(12);
                        int Customer_id;
                        int.TryParse(Console.ReadLine(), out Customer_id);
                        DalObjects.DalObjects.Print_Customer(Customer_id);                       
                        break;
                        #endregion
                    case ("3.4")://Print Parcel
                        #region//Print Parcel
                        print_func(9);
                        int Parcel_id3;
                        int.TryParse(Console.ReadLine(), out Parcel_id3);
                        DalObjects.DalObjects.Print_Parcel(Parcel_id3);
                        break;
                        #endregion
                    case ("4.1")://Display list of base stations
                        #region//Display list of base stations
                        DalObjects.DalObjects.Print_list_of_BaseStations();
                        break;
                        #endregion
                    case ("4.2")://Display list of drones
                        #region//Display list of drones
                        DalObjects.DalObjects.Print_list_of_Drones();
                        break;
                        #endregion
                    case ("4.3")://Display list of customers
                        #region//Display list of customers
                        DalObjects.DalObjects.Print_list_of_Customers();
                        break;
                        #endregion
                    case ("4.4")://Display list of parcels
                        #region//Display list of parcels
                        DalObjects.DalObjects.print_list_of_Parcels();
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
                        print_func(4);// if invalid input                    
                        break;
                }
            }
        }
        static void print_func(int i) // print
        {
            #region//if i = 1   print a welcome intro
            if (i == 1) // print option welcome intro
            {
                
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



               
                return;
            }
            #endregion
            #region//if i = 2  print menu
            if (i == 2) // print a menu
            {              
                Console.WriteLine("TO SELECT OPTION ENTER SECTION NUMBER\n\n1) ADDING OPTIONS:\n\t1.1) Add base station\n\t1.2) Add drone\n\t1.3) Add customer\n\t1.4) Add parcel\n\n2) UPDATE OPTIONS:\n\t2.1) Assign parcel to customer\n\t2.2) Collect parcel by drone\n\t2.3) Deliver parcel to customer\n\t2.4) Send drone to charge at base station\n\t2.5) Release drone from charging\n\n3) DISPLAY DATA:\n\t3.1) Display base station\n\t3.2) Display drone\n\t3.3) Display customer\n\t3.4) Display parcel\n\n4) DISPLAY LISTS\n\t4.1) Display list of base stations\n\t4.2) Display list of drones\n\t4.3) Display list of customers\n\t4.4) Display list of parcels\n\t4.5) Display list of parcels that are not assigned to drone\n\t4.6) Display list of base stations with free charging stations\n\n5) EXIT\n");
                return;
            }
            #endregion
            #region//if i = 3 print  choice request
            if (i == 3)
            {
                Console.WriteLine("Please enter your choice:");  // print choice request
                return;
            }
            #endregion
            #region// if i = 4 print invalid input
            if (i == 4) // invalid input
            {
                Console.WriteLine("INVALID INPUT");
                return;
            }
            #endregion
            #region// if i = 5 print user input for 1.1
            if (i == 5) // print to user data he needs to type in  
            {
                Console.WriteLine("Please enter following data: \n 1) Id \n 2) Name \n 3) ChargeSlots \n 4) Longtitude \n 5) Latitude ");
                return;
            }
            #endregion
            #region// if i = 6 print user input for 1.2
            if (i == 6)// print to user data he needs to type in  
            {
                Console.WriteLine("Please enter following data: \n 1) Id \n 2) Model \n 3) WeightCategories(0 for Light, 1 for Medium, 2 for Heavy) \n 4) DroneStatuses(0 for Avilable, 1 for Delivery, 2 for Maintenance) \n 5) battery ");
                return;
            }
            #endregion
            #region// if i = 7 print user input for 1.3
            if (i == 7) // print to user data he needs to type in 
            {
                Console.WriteLine("Please enter following data: \n 1) Id \n 2) Name \n 3) phone number \n 4) Longtitude \n 5) Latitude ");
                return;
            }
            #endregion
            #region// if i = 8 print user input for 1.4
            if (i == 8) // print to user data he needs to type in 
            {
                Console.WriteLine("Please enter following data: \n 1) Id \n 2) sender id \n 3) target id \n 4) WeightCategories(0 for Light, 1 for Medium, 2 for Heavy) \n 5) priorities( 0 for Regular, 1 for Express, 2 for Urgant ) \n 6)requested time ");
                return;
            }
            #endregion
            #region// if i = 9 print user input for parcel
            if (i == 9) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter parcel ID: ");
                return;
            }
            #endregion
            #region// if i = 10  print user input for base station
            if (i == 10) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter Base Station ID: ");
                return;
            }
            #endregion
            #region// if i = 11 print user input for Drone
            if (i == 11) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter Drone ID: ");
                return;
            }
            #endregion
            #region// if i = 12 print user input for Customer
            if (i == 12) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter customer ID: ");
                return;
            }
            #endregion
        }
    }
}

