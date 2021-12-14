using System;
using BlApi;
using BlApi.BO;
namespace ConsoleUI_BL
{
    class Program
    {
        static BlApi.BL bl = new BL(); // call constructor
        public enum Option { Addop = 1, Updateop, DisplayByIdop, ListDisplayop, Exit } // enum for option menu
        /// <summary>
        /// main program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            PrintFunc(1);
            while (true)
            {
                Option option; // read users option choice
                PrintFunc(2);//print menu
                int userInput;
                int.TryParse(Console.ReadLine(), out userInput);
                option = (Option)userInput;
                switch (option)
                {
                    case Option.Addop: // add
                        Input();
                        break;
                    case Option.Updateop: // update
                        Update();
                        break;
                    case Option.DisplayByIdop: // display
                        Display();
                        break;
                    case Option.ListDisplayop: // display all
                        Displaylist();
                        break;
                    case Option.Exit: // exit
                        return;
                    default:
                        PrintFunc(24); // invalid input
                        break;
                }
            }
        }
        #region//cases for add
        public enum InputOption { ADDBaseStation = 1, ADDDrone, ADDCastomer, ADDParcel } // enum for input option
        /// <summary>
        /// All input operations
        /// </summary>
        private static void Input()//cases for add
        {   
            try
            {
                InputOption input;
                PrintFunc(3);//print choice request for adding
                int userInput;
                int.TryParse(Console.ReadLine(), out userInput);
                input = (InputOption)userInput;
                switch (input)
                {
                    case InputOption.ADDBaseStation://base station addition
                        #region//BaseStation addition
                        PrintFunc(7);//print to user data he needs to type in  
                        int userId;
                        if(!int.TryParse(Console.ReadLine(), out userId))
                        {       
                            PrintFunc(24);
                            break;
                        }
                        string userName = Console.ReadLine();
                        double userLongtitude;
                        double.TryParse(Console.ReadLine(), out userLongtitude);
                        double userLatitude;
                        double.TryParse(Console.ReadLine(), out userLatitude);
                        Location UserLocation = new Location(userLongtitude, userLatitude);
                        int userFreeChargeSlots;
                        int.TryParse(Console.ReadLine(), out userFreeChargeSlots);
                        BaseStation NewBaseStation = new BaseStation(userId, userName, UserLocation, userFreeChargeSlots);
                        bl.AddBaseStation(NewBaseStation);
                        break;
                    #endregion
                    case InputOption.ADDDrone://Drone addition
                        #region// Drone addition
                        PrintFunc(8); // print to user data he needs to type in  
                        int userDroneId;
                        if (!int.TryParse(Console.ReadLine(), out userDroneId))
                        {
                            PrintFunc(24);
                            break;
                        }
                        string userModel = Console.ReadLine();
                        Enums.WeightCategories userWeightCategories;
                        Enums.WeightCategories.TryParse(Console.ReadLine(), out userWeightCategories);
                        if(userWeightCategories>Enums.WeightCategories.Heavy)
                        {
                            PrintFunc(24);
                            break;
                        }
                        int userBaseStation;
                        if (!int.TryParse(Console.ReadLine(), out userBaseStation))
                        {
                            PrintFunc(24);
                            break;
                        }
                        Drone newDrone = new Drone(userDroneId, userModel, userWeightCategories);
                        bl.AddDrone(newDrone, userBaseStation);
                        break;
                    #endregion
                    case InputOption.ADDCastomer://customer addition
                        #region// Customer addition
                        PrintFunc(9); // print to user data he needs to type in 
                        int userCustomerId;
                        if (!int.TryParse(Console.ReadLine(), out userCustomerId))
                        {
                            PrintFunc(24);
                            break;
                        }
                        string userCustomerName = Console.ReadLine();
                        string userPhoneNumber = Console.ReadLine();
                        double userCustomerLongtitude;
                        if (!double.TryParse(Console.ReadLine(), out userCustomerLongtitude))
                        {
                            PrintFunc(24);
                            break;
                        }
                        double userCustomerLatitude;
                        if (!double.TryParse(Console.ReadLine(), out userCustomerLatitude))
                        {
                            PrintFunc(24);
                            break;
                        }
                        Location location = new Location(userCustomerLongtitude, userCustomerLatitude);
                        Customer newCastomer = new Customer(userCustomerId, userCustomerName, userPhoneNumber, location);
                        bl.AddCustomer(newCastomer);
                        break;
                    #endregion
                    case InputOption.ADDParcel://parcel addition
                        #region// Parcel addition
                        PrintFunc(10); // print to user data he needs to type in 
                        int userSenderId;
                        int.TryParse(Console.ReadLine(), out userSenderId);
                        int userTargetId;
                        if (!int.TryParse(Console.ReadLine(), out userTargetId))
                        {
                            PrintFunc(24);
                            break;
                        }
                        CustomerInParcel userSenderIdd = new CustomerInParcel(userSenderId, bl.GetCustomerById(userSenderId).Name);
                        CustomerInParcel userTargetIdd = new CustomerInParcel(userTargetId, bl.GetCustomerById(userTargetId).Name);
                        Enums.WeightCategories userParcelWeightCategories;
                        Enums.WeightCategories.TryParse(Console.ReadLine(), out userParcelWeightCategories);
                        if (userParcelWeightCategories > Enums.WeightCategories.Heavy)
                        {
                            PrintFunc(24);
                            break;
                        }
                        Enums.Priorities userParcelPriorities;
                        Enums.Priorities.TryParse(Console.ReadLine(), out userParcelPriorities);
                        if (userParcelPriorities > Enums.Priorities.Urgent)
                        {
                            PrintFunc(24);
                            break;
                        }
                        bl.AddParcel(userSenderIdd, userTargetIdd, userParcelWeightCategories, userParcelPriorities);
                        break;
                    #endregion
                    default:
                        PrintFunc(24);
                        break;
                }
            }
            catch (Exception exception)
            {
                PrintException(exception);
            }
        }
        #endregion

        #region//cases for update
        /// <summary>
        /// cases for update
        /// </summary>
        public enum update { DroneRenameOrRemodel = 1,BaseStationRenameOrNumOfChargeSlot,CustomerRenameOrRephone, SendDronetocharge, Release,Assign, Collect, Deliver } // enum for update option
        private static void Update()//cases for update
        {
            try
            {
                update up;
                PrintFunc(4);
                int userInput;

                while(!int.TryParse(Console.ReadLine(), out userInput))
                {
                    PrintFunc(24);
                }
                up = (update)userInput;
                switch (up)
                {
                    case update.DroneRenameOrRemodel: // update drone name or model or both
                        #region//Update drone name or model or both
                        PrintFunc(11);// print to user data he needs to type in 
                        int userDroneId;
                        if (!int.TryParse(Console.ReadLine(), out userDroneId))
                        {
                            PrintFunc(24);
                            break;
                        }
                        string userModel = Console.ReadLine();
                        bl.UpdateDroneModel(userDroneId, userModel);//function update drone model
                        break;
                    #endregion
                    case update.BaseStationRenameOrNumOfChargeSlot: // Update Base Station name or free charge slots number or both
                        #region//Update Base Station name or free charge slots number or both
                        PrintFunc(12);// print to user data he needs to type in 
                        int userBasestationId;
                        if (!int.TryParse(Console.ReadLine(), out userBasestationId))
                        {
                            PrintFunc(24);
                            break;
                        }
                        PrintFunc(26);
                        string UserNewBaseStationName = Console.ReadLine();
                        int userNewFreeChargeSlots;
                        PrintFunc(27);
                        int.TryParse(Console.ReadLine(), out userNewFreeChargeSlots);
                        bl.UpdateBaseStation(userBasestationId, UserNewBaseStationName, userNewFreeChargeSlots);
                        break;
                    #endregion
                    case update.CustomerRenameOrRephone: // Update Customer name or phone or both
                        #region//Update Customer name or phone or both
                        PrintFunc(13);// print to user data he needs to type in 
                        int userDroneId1;
                        int.TryParse(Console.ReadLine(), out userDroneId1);
                        string UserNewCustomerName = Console.ReadLine();            
                        string userNewPhone = Console.ReadLine();                     
                        bl.UpdateCustomer(userDroneId1, UserNewCustomerName, userNewPhone);
                        break;
                    #endregion
                    case update.SendDronetocharge: // send drone to charge
                        #region//send drone to charge
                        PrintFunc(14);// print to user data he needs to type in 
                        int userDroneId2;
                        int.TryParse(Console.ReadLine(), out userDroneId2);
                        bl.UpdateSendDroneToCharge(userDroneId2);
                        break;
                    #endregion
                    case update.Release: // release drone from charge slot
                        #region//release drone from charge slot
                        PrintFunc(14); // print to user data he needs to type in 
                        int userDroneId3;
                        int.TryParse(Console.ReadLine(), out userDroneId3);
                        //double time;
                        //PrintFunc(25);
                        //double.TryParse(Console.ReadLine(), out time);
                        bl.UpdateReleseDrone(userDroneId3);
                        break;
                    #endregion
                    case update.Assign: // associate drone to base station
                        #region//associate drone to base station
                        PrintFunc(14);// print to user data he needs to type in 
                        int userDroneId4;
                        int.TryParse(Console.ReadLine(), out userDroneId4);
                        bl.UpdateAssosiateDrone(userDroneId4);
                        break;
                    #endregion
                    case update.Collect: //collect parcel by drone
                        #region//collect parcel by drone
                        PrintFunc(14); // print to user data he needs to type in 
                        int userDroneId5;
                        int.TryParse(Console.ReadLine(), out userDroneId5);
                        bl.PickupParcelByDrone(userDroneId5);
                        break;
                    #endregion
                    case update.Deliver: //deliver parcel 
                        #region//deliver parcel 
                        PrintFunc(14);// print to user data he needs to type in 
                        int userDroneId6;
                        int.TryParse(Console.ReadLine(), out userDroneId6);
                        bl.DeliveryParcelByDrone(userDroneId6);
                        break;
                    #endregion
                    default:
                        PrintFunc(24);
                        break;
                }

            }
            catch (Exception exception)
            {
                PrintException(exception);
            }
        }

        #endregion

        #region//cases for display
        public enum display { basestation = 1, drone, customer, parcel } // enum for display option
        private static void Display()//cases for display
        {
            display d;
            PrintFunc(5);
            d = (display)int.Parse(Console.ReadLine());
            int id;
            try
            {
                switch (d)
                {
                    case display.basestation: // display basestation
                        PrintFunc(16);
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(bl.GetBaseStationById(id).ToString());
                        break;
                    case display.drone: // display drone
                        PrintFunc(14);
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(bl.GetDroneById(id).ToString());
                        break;
                    case display.customer: // display customer
                        PrintFunc(15);
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(bl.GetCustomerById(id).ToString());
                        break;
                    case display.parcel: // display parcel
                        PrintFunc(17);
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(bl.GetParcelById(id).ToString());
                        break;
                    default:
                        PrintFunc(24);                        
                        break;
                }
            }
            catch (Exception exception)
            {
                PrintException(exception);
            }
        }
        #endregion

        #region//cases for display list
        public enum displaylist { basestationList = 1, droneList, customerList, parcelList, notAssigned, freeChargingStations } // enum for display list option
        private static void Displaylist()//cases for display list
        {
            displaylist d;
            PrintFunc(6);
            d = (displaylist)int.Parse(Console.ReadLine());
            switch (d)
            {
                case displaylist.basestationList: // display list of basestations
                    PrintFunc(18);
                    break;
                case displaylist.droneList: // display list of drones
                    PrintFunc(19);
                    break;
                case displaylist.customerList: // display list of customers
                    PrintFunc(20);
                    break;
                case displaylist.parcelList: // display list of parcels
                    PrintFunc(21);
                    break;
                case displaylist.notAssigned: // display list of not assigned
                    PrintFunc(22);
                    break;
                case displaylist.freeChargingStations: // display list of free charging stations
                    PrintFunc(23);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region// print an exception func
        /// <summary>
        /// print an exception
        /// </summary>
        /// <param name="exception"></param>
        private static void PrintException(Exception exception)
        {
            if (exception == null)
            {
                return;
            }
            PrintException(exception.InnerException);
            Console.WriteLine(exception.ToString());
        }
        #endregion

        /// <summary>
        /// printing function
        /// </summary>
        /// <param name="i"></param>
        static void PrintFunc(int i) // print function.
        {
            #region// if i = 1  print a welcome intro
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
            #region// if i = 2  print main menu
            if (i == 2) // print a main menu
            {
                Console.WriteLine("TO SELECT OPTION ENTER SECTION NUMBER\n\n1) ADDING OPTIONS:\n\n2) UPDATE OPTIONS:\n\n3) DISPLAY DATA:\n\n4) DISPLAY LISTS\n\n5) EXIT\n");
                return;
            }
            #endregion
            #region// if i = 3 print  choice request for adding
            if (i == 3)//if i = 3 print  choice request for adding
            {
                Console.WriteLine("Please enter your ADD choice:\n\t1) Add base station\n\t2) Add drone\n\t3) Add customer\n\t4) Add parcel\n\n");  // print choice request
                return;
            }
            #endregion
            #region// if i = 4 print choice request for updating
            if (i == 4) //request for updating       
            {
                Console.WriteLine("Please enter your Update choice:\n\t1) Drone rename model\n\t2) Base station rename or/and change size of charge Slot\n\t3) Customer rename and/or change phone number\n\t4) Send drone to charge at base station\n\t5) Release drone from charging\n\t6) Associate drone\n\t7) Collect parcel by drone\n\t8) Deliver parcel");
                return;
            }
            #endregion
            #region// if i = 5 print choice request for Displaying By Id
            if (i == 5) //  request for Display By Id 
            {
                Console.WriteLine("Please enter your Display choice:\n\t1) Display base station\n\t2) Display drone\n\t3) Display customer\n\t4) Display parcel\n");
                return;
            }
            #endregion
            #region// if i = 6 print choice request for  List Display
            if (i == 6) // print to user data he needs to type in  
            {
                Console.WriteLine("Please enter your Display list choice:\n\t1) Display list of base stations\n\t2) Display list of drones\n\t3) Display list of customers\n\t4) Display list of parcels\n\t5) Display list of parcels that are not assigned to drone\n\t6) Display list of base stations with free charging stations\n ");
                return;
            }
            #endregion
            #region// if i = 7 print to user data he needs to type in to add base station
            if (i == 7) // print to user data he needs to type in  
            {
                Console.WriteLine("Please enter following data: \n 1) Id \n 2) Name \n 3) Longtitude between 31.728959 - 31.806477 \n 4) Latitude between 35.206714 - 35.221416 \n 5) Number of charge slots ");
                return;
            }
            #endregion
            #region// if i = 8 print to user data he needs to type in to add drone
            if (i == 8)// print to user data he needs to type in  
            {
                Console.WriteLine("Please enter following data: \n 1) Id \n 2) Model \n 3) Weight categorie: 0 for Light,  1 for Medium,  2 for Heavy \n 4) base station Id to associate");
                return;
            }
            #endregion
            #region// if i = 9 print to user data he needs to type in to add customer
            if (i == 9) // print to user data he needs to type in 
            {
                Console.WriteLine("Please enter following data: \n 1) Id \n 2) Name \n 3) phone number \n 4) Longtitude between 31.728959 - 31.806477 \n 5) Latitude between 35.206714 - 35.221416 ");
                return;
            }
            #endregion
            #region// if i = 10 print to user data he needs to type in to add parcel
            if (i == 10) // print to user data he needs to type in 
            {
                Console.WriteLine("Please enter following data: \n 1) sender id \n 2) target id \n 3) Weight categories 0 for Light,  1 for Medium,  2 for Heavy \n 4) prioritie  0 for Regular,  1 for Express,  2 for Urgant  \n ");
                return;
            }
            #endregion
            #region// if i = 11 print update drone rename model
            if (i == 11) // print to user the data he needs to type in 
            {
                Console.WriteLine("Please enter drone ID: ");
                Console.WriteLine("Please enter new model name:");
                return;
            }
            #endregion
            #region// if i = 12 print enter base station id
            if (i == 12) // print to user data he needs to type in 
            {
                Console.WriteLine("Please enter base stationId ID: ");
                return;
            }
            #endregion
            #region// if i = 13 print update customer rename and/or phone number
            if (i == 13) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter customer ID: ");
                Console.WriteLine("Pleas enter new customer name");
                Console.WriteLine("Pleas enter new customer phone");
                return;
            }
            #endregion
            #region// if i = 14 print enter drone id
            if (i == 14) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter drone ID: ");
                return;
            }
            #endregion
            #region// if i = 15 print enter customer id
            if (i == 15) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter customer ID: ");
                return;
            }
            #endregion
            #region// if i = 16 print enter station id
            if (i == 16) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter base station ID: ");
                return;
            }
            #endregion
            #region// if i = 17 print enter parcel id
            if (i == 17) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter parcel ID: ");
                return;
            }
            #endregion
            #region// if i = 18 print list of base stations
            if (i == 18)
            {
                foreach (var item in bl.GetListOfBaseStationsToList())
                {
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                    Console.WriteLine(item.ToString());
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                }
            }
            #endregion
            #region// if i = 19 print list of drones
            if (i == 19)
            {
                foreach (var item in bl.GetListOfDronesToList())
                {
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                    Console.WriteLine(item.ToString());
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                }
            }
            #endregion
            #region// if i = 20 print list of customers
            if (i == 20)
            {
                foreach (var item in bl.GetListOfCustomerToList())
                {
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                    Console.WriteLine(item.ToString());
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                }
            }
            #endregion
            #region// if i = 21 print list of parcels
            if (i == 21)
            {
                foreach (var item in bl.GetListOfParcelToList())
                {
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                    Console.WriteLine(item.ToString());
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                }
            }
            #endregion
            #region// if i = 22 print list of not assigned
            if (i == 22)
            {
                foreach (var item in bl.GetListOfNotAssigned())
                {
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                    Console.WriteLine(item.ToString());
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                }
            }
            #endregion
            #region// if i = 23 print list of freeChargingStations
            if (i == 23)
            {
                foreach (var item in bl.GetListOfFreeChargingStations())
                {
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                    Console.WriteLine(item.ToString());
                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - ");
                }
            }
            #endregion
            #region// if i = 24 print invalid input
            if (i == 24) // invalid input
            {
                Console.WriteLine("\n");
                Console.WriteLine("INVALID INPUT");
                Console.WriteLine("\n");
                return;
            }
            #endregion
            //#region// if i = 25 print enter time
            //if (i == 25) // invalid input
            //{
            //    Console.WriteLine("Enter how much time the drone was charging");
            //    return;
            //}
            //#endregion
            #region// if i = 26 print update base station rename 
            if (i == 26) // print to user data he needs to type in 
            {
                Console.WriteLine("Please enter new base station name");                
                return;
            }
            #region// if i = 27 print update base station  charging slots
            if (i == 27) // print to user data he needs to type in 
            {
                Console.WriteLine("Please enter new number of free charging slots ");
                return;
            }
            #endregion
            #endregion
            return;
        }
    }  
}