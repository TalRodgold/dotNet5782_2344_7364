using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi.DO;
using DalApi;

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
            IDal name = new DalObjects.DalObjects();
            PrintFunc(1); // print ascii art.
            PrintFunc(2); // print menu.
            while (true) // untill user enters 5 continue running
            {
                PrintFunc(3);// print choice request
                string UserInput = Console.ReadLine(); // read user input
                switch (UserInput)
                {
                    case ("1.1")://BaseStation addition
                        #region// BaseStation addition
                        PrintFunc(5); // print to user data he needs to type in                   
                        int userId;
                        int.TryParse(Console.ReadLine(), out userId);
                        string userName = Console.ReadLine();
                        int userChargeSlots;
                        int.TryParse(Console.ReadLine(), out userChargeSlots);
                        double userLongtitude;
                        double.TryParse(Console.ReadLine(), out userLongtitude);
                        double userLatitude;
                        double.TryParse(Console.ReadLine(), out userLatitude);
                        name.ConstructBaseStation(userId, userName, userChargeSlots, userLongtitude, userLatitude);
                        break;
                        #endregion
                    case ("1.2")://Drone addition
                        #region// Drone addition
                        PrintFunc(6); // print to user data he needs to type in  
                        int userDroneId;
                        int.TryParse(Console.ReadLine(), out userDroneId);
                        string userModel = Console.ReadLine();
                        WeightCategories userWeightCategories;
                        WeightCategories.TryParse(Console.ReadLine(), out userWeightCategories);
                        name.ConstructDrone(userDroneId, userModel, userWeightCategories);
                        break;
                    #endregion
                    case ("1.3")://Customer addition
                        #region// Customer addition
                        PrintFunc(7); // print to user data he needs to type in 
                        int userCustomerId;
                        int.TryParse(Console.ReadLine(), out userCustomerId);
                        string userCustomerName = Console.ReadLine();
                        string userPhoneNumber = Console.ReadLine();
                        double userCustomerLongtitude;
                        double.TryParse(Console.ReadLine(), out userCustomerLongtitude);
                        double userCustomerLatitude;
                        double.TryParse(Console.ReadLine(), out userCustomerLatitude);
                        name.ConstructCustomer(userCustomerId, userCustomerName, userPhoneNumber, userCustomerLatitude, userCustomerLatitude);
                        break;
                        #endregion
                    case ("1.4")://Parcel addition
                        #region// Parcel addition
                        PrintFunc(8); // print to user data he needs to type in 
                     
                        int userSenderId;
                        int.TryParse(Console.ReadLine(), out userSenderId);
                        int userTargetId;
                        int.TryParse(Console.ReadLine(), out userTargetId);
                        WeightCategories userParcelWeightCategories;
                        WeightCategories.TryParse(Console.ReadLine(), out userParcelWeightCategories);
                        Priorities userParcelPriorities;
                        Priorities.TryParse(Console.ReadLine(), out userParcelPriorities);
                        DateTime userRequested;
                        DateTime.TryParse(Console.ReadLine(), out userRequested);
                        name.ConstructParcel(userSenderId, userTargetId, userParcelWeightCategories, userParcelPriorities, userRequested, 0, null, null, null);
                        break;
                      #endregion
                    case ("2.1"): //Update Percel to Drone
                        #region//Update Percel to Drone
                        PrintFunc(9); //print
                        int parcelId;
                        int.TryParse(Console.ReadLine(), out parcelId);
                        PrintFunc(11);
                        int droneId;
                        int.TryParse(Console.ReadLine(), out droneId);
                        name.AssociateDroneToParcel( droneId, parcelId);
                        break;
                        #endregion
                    case ("2.2")://Update Parcle pickup
                        #region//Update Parcle pickup
                        PrintFunc(9);//print
                        int parcelId1;
                        int.TryParse(Console.ReadLine(), out parcelId1);
                        name.UpdateParclePickup(parcelId1);
                        break;
                        #endregion
                    case ("2.3")://Update Parcle delivery
                        #region//Update Parcle delivery
                        PrintFunc(9);//print
                        int parcelId2;
                        int.TryParse(Console.ReadLine(), out parcelId2);
                        name.UpdateParcleDelivery(parcelId2);
                        break;
                        #endregion
                    case ("2.4")://Print free BaseStation
                        #region//Print free BaseStation
                        Console.Write(name.BaseStationfreeToString());
                        PrintFunc(11);
                        int droneId2;
                        int.TryParse(Console.ReadLine(), out droneId2);
                        PrintFunc(10);
                        int baseId;
                        int.TryParse(Console.ReadLine(), out baseId);
                        name.UpdateDroneCharge(droneId2, baseId,DateTime.Now);
                        break;
                        #endregion
                    case ("2.5")://release DroneCharge
                        #region//release DroneCharge
                        PrintFunc(11);//print
                        int droneId1;
                        int.TryParse(Console.ReadLine(), out droneId1);
                        PrintFunc(10);//print
                        int baseId1;
                        int.TryParse(Console.ReadLine(), out baseId1);
                        name.ReleaseDroneCharge(droneId1, baseId1);
                        break;
                        #endregion
                    case ("3.1")://Print BaseStation
                        #region//Print BaseStation
                        PrintFunc(10);//print
                        int baseId2;
                        int.TryParse(Console.ReadLine(), out baseId2);
                        Console.WriteLine(name.ReturnBaseStationDataById(baseId2));
                        break;
                        #endregion
                    case ("3.2")://Print Drone
                        #region//Print Drone
                        PrintFunc(11);//print
                        int droneId3;
                        int.TryParse(Console.ReadLine(), out droneId3);
                        Console.WriteLine(name.ReturnDroneDataById(droneId3));
                        break;
                        #endregion
                    case ("3.3")://Print Customer
                        #region//Print Customer
                        PrintFunc(12);//print
                        int customerId;
                        int.TryParse(Console.ReadLine(), out customerId);
                        Console.WriteLine(name.ReturnCustomerDataById(customerId));                       
                        break;
                        #endregion
                    case ("3.4")://Print Parcel
                        #region//Print Parcel
                        PrintFunc(9);//print
                        int parcelId3;
                        int.TryParse(Console.ReadLine(), out parcelId3);
                        Console.WriteLine(name.ReturnParcelDataById(parcelId3));
                        break;
                        #endregion
                    case ("4.1")://Display list of base stations
                        #region//Display list of base stations
                        Console.Write(name.BaseStationListToString());
                        break;
                        #endregion
                    case ("4.2")://Display list of drones
                        #region//Display list of drones
                        for (int i = 0; i < name.GetCountOfDroneList(); i++)
                        {
                            Console.WriteLine(name.DroneListToString());
                        }
                        break;
                        #endregion
                    case ("4.3")://Display list of customers
                        #region//Display list of customers
                        for (int i = 0; i < name.GetCountOfCustomerList(); i++)
                        {
                            Console.WriteLine(name.CustomerListToString());
                        }
                        break;
                        #endregion
                    case ("4.4")://Display list of parcels
                        #region//Display list of parcels
                        for (int i = 0; i < name.GetCountOfParcelList(); i++)
                        {
                            Console.WriteLine(name.ParcelListToString());
                        }
                        break;
                        #endregion
                    case ("4.5")://Display list of parcels who are not associated to a drone
                        #region//Display list of parcels who are not associated to a drone
                        Console.Write(name.ParcelsNotAssociatedToString());
                        break;
                        #endregion
                    case ("4.6")://Display list of base stations with free charging stations
                        #region//Display list of base stations with free charging stations
                        Console.Write(name.BaseStationfreeToString());
                        break;
                        #endregion
                    case ("5")://exit
                        #region//exit
                        return;
                    #endregion
                    default:
                        PrintFunc(4);// if invalid input                    
                        break;
                }
            }
        }
        /// <summary>
        /// printing function
        /// </summary>
        /// <param name="i"></param>
        static void PrintFunc(int i) // print function
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
                Console.WriteLine("Please enter following data: \n 1) Id \n 2) Model \n 3) WeightCategories(0 for Light, 1 for Medium, 2 for Heavy) \n ");
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
                Console.WriteLine("Please enter following data: \n 1) sender id \n 2) target id \n 3) WeightCategories(0 for Light, 1 for Medium, 2 for Heavy) \n 4) priorities( 0 for Regular, 1 for Express, 2 for Urgant ) \n 5)requested time ");
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

