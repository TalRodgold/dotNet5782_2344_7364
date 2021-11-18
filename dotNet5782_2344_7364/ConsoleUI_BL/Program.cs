using System;
using IBL;
using IBL.BO;
namespace ConsoleUI_BL
{
    class Program
    {
        static IBL.BL bl; // call constructor
        public enum Option { Addop = 1, Updateop, DisplayByIdop, ListDisplayop } // enum for option menu
        /// <summary>
        /// main program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bl = new BL();
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
                    default:
                        PrintFunc(24);
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
                    case InputOption.ADDBaseStation:
                        #region//BaseStation addition
                        PrintFunc(7);//print to user data he needs to type in  
                        int userId;
                        int.TryParse(Console.ReadLine(), out userId);
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
                        int.TryParse(Console.ReadLine(), out userDroneId);
                        string userModel = Console.ReadLine();
                        Enums.WeightCategories userWeightCategories;
                        Enums.WeightCategories.TryParse(Console.ReadLine(), out userWeightCategories);
                        int userBaseStation;
                        int.TryParse(Console.ReadLine(), out userBaseStation);
                        Drone newDrone = new Drone(userDroneId, userModel, userWeightCategories);
                        bl.AddDrone(newDrone, userBaseStation);
                        break;
                    #endregion
                    case InputOption.ADDCastomer:
                        #region// Customer addition
                        PrintFunc(9); // print to user data he needs to type in 
                        int userCustomerId;
                        int.TryParse(Console.ReadLine(), out userCustomerId);
                        string userCustomerName = Console.ReadLine();
                        string userPhoneNumber = Console.ReadLine();
                        double userCustomerLongtitude;
                        double.TryParse(Console.ReadLine(), out userCustomerLongtitude);
                        double userCustomerLatitude;
                        double.TryParse(Console.ReadLine(), out userCustomerLatitude);
                        Location location = new Location(userCustomerLongtitude, userCustomerLatitude);
                        Customer newCastomer = new Customer(userCustomerId, userCustomerName, userPhoneNumber, location);
                        bl.AddCustomer(newCastomer);
                        break;
                    #endregion
                    case InputOption.ADDParcel:
                        #region// Parcel addition
                        PrintFunc(10); // print to user data he needs to type in 
                        int userSenderId;
                        int.TryParse(Console.ReadLine(), out userSenderId);
                        int userTargetId;
                        int.TryParse(Console.ReadLine(), out userTargetId);
                        CustomerInParcel userSenderIdd = new CustomerInParcel(userSenderId, bl.GetCustomerById(userSenderId).Name);
                        CustomerInParcel userTargetIdd = new CustomerInParcel(userTargetId, bl.GetCustomerById(userTargetId).Name);
                        Enums.WeightCategories userParcelWeightCategories;
                        Enums.WeightCategories.TryParse(Console.ReadLine(), out userParcelWeightCategories);
                        Enums.Priorities userParcelPriorities;
                        Enums.Priorities.TryParse(Console.ReadLine(), out userParcelPriorities);
                        bl.AddParcel(userSenderIdd, userTargetIdd, userParcelWeightCategories, userParcelPriorities);

                        break;
                    #endregion
                    default:
                        PrintFunc(24);
                        break;
                }
            }
            catch ( IBL.BO.IdAlreadyExsistsExceptions exception)
            {

                Console.WriteLine(exception.ToString());
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
                int.TryParse(Console.ReadLine(), out userInput);
                up = (update)userInput;
                switch (up)
                {
                    case update.DroneRenameOrRemodel:
                        #region//Update drone name or model or both
                        PrintFunc(11);// print to user data he needs to type in 
                        int userDroneId;
                        int.TryParse(Console.ReadLine(), out userDroneId);
                        string userModel = Console.ReadLine();
                        bl.UpdateDroneModel(userDroneId, userModel);//function update drone model
                        break;
                    #endregion
                    case update.BaseStationRenameOrNumOfChargeSlot:
                        #region//Update Base Station name or free charge slots number or both
                        PrintFunc(12);// print to user data he needs to type in 
                        int userBasestationId;
                        int.TryParse(Console.ReadLine(), out userBasestationId);
                        string UserNewBaseStationName = Console.ReadLine();
                        int userNewFreeChargeSlots;
                        int.TryParse(Console.ReadLine(), out userNewFreeChargeSlots);
                        bl.UpdateBaseStation(userBasestationId, UserNewBaseStationName, userNewFreeChargeSlots);
                        break;
                    #endregion
                    case update.CustomerRenameOrRephone:
                        #region//Update Customer name or phone or both
                        PrintFunc(13);// print to user data he needs to type in 
                        int userDroneId1;
                        int.TryParse(Console.ReadLine(), out userDroneId1);
                        string UserNewCustomerName = Console.ReadLine();            
                        string userNewPhone = Console.ReadLine();                     
                        bl.UpdateCustomer(userDroneId1, UserNewCustomerName, userNewPhone);
                        break;
                    #endregion
                    case update.SendDronetocharge:
                        #region//send drone to charge
                        PrintFunc(14);// print to user data he needs to type in 
                        int userDroneId2;
                        int.TryParse(Console.ReadLine(), out userDroneId2);
                        bl.UpdateSendDroneToCharge(userDroneId2);
                        break;
                    #endregion
                    case update.Release:
                        #region//release drone from charge slot
                        PrintFunc(14); // print to user data he needs to type in 
                        int userDroneId3;
                        int.TryParse(Console.ReadLine(), out userDroneId3);
                        double time;
                        double.TryParse(Console.ReadLine(), out time);
                        bl.UpdateReleseDrone(userDroneId3, time);
                        break;
                    #endregion
                    case update.Assign:
                        #region//associate drone to base station
                        PrintFunc(14);// print to user data he needs to type in 
                        int userDroneId4;
                        int.TryParse(Console.ReadLine(), out userDroneId4);
                        bl.UpdateAssosiateDrone(userDroneId4);
                        break;
                    #endregion
                    case update.Collect:
                        #region//collect parcel by drone
                        PrintFunc(14); // print to user data he needs to type in 
                        int userDroneId5;
                        int.TryParse(Console.ReadLine(), out userDroneId5);
                        bl.PickupParcelByDrone(userDroneId5);
                        break;
                    #endregion
                    case update.Deliver:
                        #region//deliver parcel 
                        PrintFunc(14);// print to user data he needs to type in 
                        int userDroneId6;
                        int.TryParse(Console.ReadLine(), out userDroneId6);
                        bl.DilaveryParcelByDrone(userDroneId6);
                        break;
                    #endregion
                    default:
                        PrintFunc(24);
                        break;
                }

            }
            catch (IBL.BO.IdNotExsistException exception)
            {

                Console.WriteLine(exception.ToString());
            }
            catch (IBL.BO.UnavailableExeption exception)
            {

                Console.WriteLine(exception.ToString());
            }
        }
           
        #endregion
        public enum display { basestation = 1, drone, customer, parcel } // enum for display option
        private static void Display()//cases for display
        {
            #region//cases for display
            display d;
          
            PrintFunc(5);
            d = (display)int.Parse(Console.ReadLine());
            
            int id;

            switch (d)
            {
                case display.basestation:
                    PrintFunc(16);
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine(bl.GetBaseStationById(id).ToString());
                    break;
                case display.drone:
                    PrintFunc(14);
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine(bl.GetDroneById(id).ToString());
                    break;
                case display.customer:
                    PrintFunc(15);
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine(bl.GetCustomerById(id).ToString());
                    break;
                case display.parcel:
                    PrintFunc(17);
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine(bl.GetParcelById(id).ToString());
                    break;
                default:
                    PrintFunc(24);
                    break;
            }
            #endregion
        }
        public enum displaylist { basestationList = 1, droneList, customerList, parcelList, notAssigned, freeChargingStations }
        private static void Displaylist()//cases for display list
        {
            #region//cases for display list
            displaylist d;
            PrintFunc(6);
            d = (displaylist)int.Parse(Console.ReadLine());
            switch (d)
            {
                case displaylist.basestationList:
                    PrintFunc(18);
                    break;
                case displaylist.droneList:
                    PrintFunc(19);
                    break;
                case displaylist.customerList:
                    PrintFunc(20);
                    break;
                case displaylist.parcelList:
                    PrintFunc(21);
                    break;
                case displaylist.notAssigned:
                    PrintFunc(22);
                    break;
                case displaylist.freeChargingStations:
                    PrintFunc(23);
                    break;
                default:
                    break;
            }
            #endregion
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
            #region//if i = 2  print main menu
            if (i == 2) // print a main menu
            {
                Console.WriteLine("TO SELECT OPTION ENTER SECTION NUMBER\n\n1) ADDING OPTIONS:\n2) UPDATE OPTIONS:\n\n3) DISPLAY DATA:\n\n4) DISPLAY LISTS\n\n5) EXIT\n");
                return;
            }
            //
            #endregion
            #region//if i = 3 print  choice request for adding
            if (i == 3)
            {
                Console.WriteLine("Please enter your ADD choice:\n\t1.1) Add base station\n\t1.2) Add drone\n\t1.3) Add customer\n\t1.4) Add parcel\n\n");  // print choice request
                return;
            }
            #endregion
            #region// if i = 4 print choice request for updating
            if (i == 4) //request for updating
            {
                Console.WriteLine("Please enter your Update choice:\n\t2.1) Assign parcel to customer\n\t2.2) Collect parcel by drone\n\t2.3) Deliver parcel to customer\n\t2.4) Send drone to charge at base station\n\t2.5) Release drone from charging\n");
                return;
            }
            #endregion
            #region// if i = 5 print choice request for Displaying By Id
            if (i == 5) //  request for Display By Id 
            {
                Console.WriteLine("Please enter your Display choice:\n\t3.1) Display base station\n\t3.2) Display drone\n\t3.3) Display customer\n\t3.4) Display parcel\n");
                return;
            }
            #endregion
            #region// if i = 6 print choice request for  List Display
            if (i == 6) // print to user data he needs to type in  
            {
                Console.WriteLine("Please enter your Display list choice:\n\t4.1) Display list of base stations\n\t4.2) Display list of drones\n\t4.3) Display list of customers\n\t4.4) Display list of parcels\n\t4.5) Display list of parcels that are not assigned to drone\n\t4.6) Display list of base stations with free charging stations\n ");
                return;
            }
            #endregion
            #region//print to user data he needs to type in
            if (i == 7) // print to user data he needs to type in  
            {
                Console.WriteLine("Please enter following data: \n 1) Id \n 2) Name \n 3) Longtitude \n 4) Latitude \n 5) ChargeSlots ");
                return;
            }
            #endregion
            #region// if i = 8 print to user data he needs to type in 
            if (i == 8)// print to user data he needs to type in  
            {
                Console.WriteLine("Please enter following data: \n 1) Id \n 2) Model \n 3) WeightCategories(0 for Light, 1 for Medium, 2 for Heavy) \n 4) base station Id to associate");
                return;
            }
            #endregion
            #region// if i = 9 print to user data he needs to type in 
            if (i == 9) // print to user data he needs to type in 
            {
                Console.WriteLine("Please enter following data: \n 1) Id \n 2) Name \n 3) phone number \n 4) Longtitude \n 5) Latitude ");
                return;
            }
            #endregion
            #region// if i = 10 print to user data he needs to type in 
            if (i == 10) // print to user data he needs to type in 
            {
                Console.WriteLine("Please enter following data: \n 1) sender id \n 2) target id \n 3) WeightCategories(0 for Light, 1 for Medium, 2 for Heavy) \n 4) priorities( 0 for Regular, 1 for Express, 2 for Urgant ) \n ");
                return;
            }
            #endregion
            #region// if i = 11 print user input for Drone associate
            if (i == 11) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter Drone ID: ");
                Console.WriteLine("Pleas enter Model to change to");
                return;
            }
            #endregion
            #region// if i = 12 print user input for Drone associate
            if (i == 12) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter Base stationId ID: ");
                Console.WriteLine("Pleas enter Model to change to");
                Console.WriteLine("Pleas enter New Base Station Name");
                Console.WriteLine("Pleaenter New Free Charge Slots number");
                return;
            }
            #endregion
            #region// if i = 13 print user input for Drone associate
            if (i == 13) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter Customer ID: ");
                Console.WriteLine("Pleas enter New Customer Name");
                Console.WriteLine("Pleas enter New Customer Phone");
                return;
            }
            #endregion
            #region// if i = 14 print user input for Drone associate
            if (i == 14) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter Drone ID: ");
                return;
            }
            #endregion
            #region// if i = 15 print user input for Drone associate
            if (i == 15) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter Customer ID: ");
                return;
            }
            #endregion
            #region// if i = 16 print user input for Drone associate
            if (i == 16) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter Base Station ID: ");
                return;
            }
            #endregion
            #region// if i = 17 print user input for Drone associate
            if (i == 17) // print to user data he needs to type in 
            {
                Console.WriteLine("Pleas enter Parcel ID: ");
                return;
            }
            #endregion
            #region// if i = 18 print list of base stations
            if (i == 18)
            {
                foreach (var item in bl.GetListOfBaseStations())
                {
                    Console.WriteLine(item.ToString());
                }
            }
            #endregion
            #region// if i = 19 print list of drones
            if (i == 19)
            {
                foreach (var item in bl.GetListOfDrones())
                {
                    Console.WriteLine(item.ToString());
                }
            }
            #endregion
            #region// if i = 20 print list of customers
            if (i == 20)
            {
                foreach (var item in bl.GetListOfCustomers())
                {
                    Console.WriteLine(item.ToString());
                }
            }
            #endregion
            #region// if i = 21 print list of parcels
            if (i == 21)
            {
                foreach (var item in bl.GetListOfParcels())
                {
                    Console.WriteLine(item.ToString());
                }
            }
            #endregion
            #region// if i = 22 print list of not assigned
            if (i == 22)
            {
                foreach (var item in bl.GetListOfNotAssigned())
                {
                    Console.WriteLine(item.ToString());
                }
            }
            #endregion
            #region// if i = 23 print list of freeChargingStations
            if (i == 23)
            {
                foreach (var item in bl.GetListOfFreeChargingStations())
                {
                    Console.WriteLine(item.ToString());
                }
            }
            #endregion
            #region// if i = 24 print invalid input
            if (i == 24) // invalid input
            {
                Console.WriteLine("INVALID INPUT");
                return;
            }
            #endregion
            return;
        }
    }  
}