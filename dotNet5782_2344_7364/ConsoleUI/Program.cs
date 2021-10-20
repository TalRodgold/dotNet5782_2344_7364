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
            Console.WriteLine("TO SELECT OPTION ENTER SECTION NUMBER \n \n 1) ADDING OPTIONS: \n \t 1.1) Add base station \n \t 1.2) Add drone \n \t  1.3) Add customer \n \t 1.4) Add parcel \n \n 2) UPDATE OPTIONS: \n\t  2.1) Assign parcel to customer \n \t  2.2) Collect parcel by drone \n \t  2.3) Deliver parcel to customer \n \t  2.4) Send drone to charge at base station \n	\t  2.5) Release drone from charging \n \n3) DISPLAY DATA: \n	\t  3.1) Display base station \n \t  3.2) Display drone \n \t  3.3) Display customer \n \t  3.4) Display parcel \n \n 4) DISPLAY LISTS \n \t  4.1) Display list of base stations \n	\t  4.2) Display list of drones \n	\t  4.3) Display list of customers \n	\t  4.4) Display list of parcels \n	\t  4.5) Display list of parcels that are not assigned to drone \n	\t  4.6) Display list of base stations with free charging stations \n \n 5) EXIT \n");
            double User_input = Console.Read();
            switch (User_input)
            {
                case (1.1)://BaseStation addition
                    Console.WriteLine("Please enter following data: \n 1) Id \n 2) Name \n 3) ChargeSlots \n 4) Longtitude \n 5) Latitude ");
                    #region
                    int User_id;
                    int.TryParse(Console.ReadLine(),out User_id);
                    string User_name = Console.ReadLine();
                    int User_chargeSlots;
                    int.TryParse(Console.ReadLine(),out User_chargeSlots);
                    double User_longtitude;
                    double.TryParse(Console.ReadLine(),out User_longtitude);
                    double User_latitude;
                    double.TryParse(Console.ReadLine(),out User_latitude);
                    DalObjects.DalObjects.ConstructBaseStation(User_id, User_name, User_chargeSlots, User_longtitude, User_latitude);
                    #endregion
                    break;
                case (1.2)://Drone addition
                    Console.WriteLine("Please enter following data: \n 1) Id \n 2) Model \n 3) WeightCategories(1/2/3) \n 4) DroneStatuses(1,2,3) \n 5) battery ");
                    #region
                    int User_DroneId;
                    int.TryParse(Console.ReadLine(), out User_DroneId);
                    string User_model = Console.ReadLine();
                    WeightCategories User_WeightCategories;
                    WeightCategories.TryParse( Console.ReadLine(),out User_WeightCategories);
                    DroneStatuses User_DroneStatuses;
                    DroneStatuses.TryParse(Console.ReadLine(),out User_DroneStatuses);
                    double battery;
                    double.TryParse(Console.ReadLine(),out battery);
                    DalObjects.DalObjects.ConstructDrone(User_DroneId, User_model, User_WeightCategories, User_DroneStatuses, battery);
                    #endregion
                    break;
                case (1.3)://Castomer addition
                    Console.WriteLine("Please enter following data: \n 1) Id \n 2) Name \n 3) phone number \n 4) Longtitude \n 5) Latitude ");
                    #region
                    int User_CustomerId;
                    int.TryParse(Console.ReadLine(),out User_CustomerId);
                    string User_CustomerName= Console.ReadLine();
                    string User_PhoneNumber = Console.ReadLine();
                    double User_CustomerLongtitude;
                    double.TryParse(Console.ReadLine(),out User_CustomerLongtitude);
                    double User_CustomerLatitude;
                    double.TryParse(Console.ReadLine(),out User_CustomerLatitude);
                    DalObjects.DalObjects.ConstructCustomer(User_CustomerId, User_CustomerName, User_PhoneNumber, User_CustomerLatitude, User_CustomerLatitude);
                    #endregion
                    break;
                case (1.4)://Percel addition
                    Console.WriteLine("Please enter following data: \n 1) Id \n 2) sender id \n 3) target id \n 4) wight categories \n 5) priorities \n 6)requested time ");
                    #region
                    int User_ParcelId;
                    int.TryParse(Console.ReadLine(), out User_ParcelId);
                    int User_SenderId;
                    int.TryParse(Console.ReadLine(), out User_SenderId) ;
                    int User_TargetId;
                    int.TryParse(Console.ReadLine(),out User_TargetId);
                    WeightCategories User_ParcelWeightCategories;
                    WeightCategories.TryParse(Console.ReadLine(), out User_ParcelWeightCategories);
                    Priorities User_ParcelPriorities;
                    Priorities.TryParse(Console.ReadLine(), out User_ParcelPriorities);
                    DateTime User_Requested;
                    DateTime.TryParse(Console.ReadLine(), out User_Requested);
                    DalObjects.DalObjects.ConstructParcel(User_ParcelId,User_SenderId, User_TargetId, User_ParcelWeightCategories, User_ParcelPriorities, User_Requested,0,DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                    #endregion
                    break;
                case (2.1): //Update Percel to Drone
                    #region
                    for(int i=0;i< DalObjects.DataSource.Config.Get_Drone_arr_index; i++)
                    {
                        if(Drone_arr[i]..)

                    }
                    #endregion
                    break;
                case (2.2):
                    #region
                   
                    #endregion
                    break;
                case (2.3):
                    #region
                    #endregion
                    break;
                case (2.4):
                    #region
                    //print free base station
                    int Base_Id;
                    int.TryParse(Console.ReadLine(), out Base_Id);
                    //function
                    #endregion
                    break;
                case (2.5):
                    #region
                    #endregion
                    break;
                case (3.1):
                    #region
                    int Base_id;
                    int.TryParse(Console.ReadLine(), out Base_id);
                    DalObjects.Get_string_BaseStation(Base_id);
                    #endregion
                    break;
                case (3.2):
                    #region
                    int Drone_id;
                    int.TryParse(Console.ReadLine()out Drone_id);
                    DalObjects.Get_string_Drone(Drone_id);
                    #endregion
                    break;
                case (3.3):
                    #region
                    int Customer_id;
                    int.TryParse(Console.ReadLine(), out Customer_id);
                    DalObjects.Get_string_Castomer(Customer_id);
                    #endregion
                    break;
                case (3.4):
                    #region
                    int Parcel_id;
                    int.TryParse(Console.ReadLine(), out Parcel_id);
                    DalObjects.Get_string_Parcel(Parcel_id);
                    #endregion
                    break;
                case (4.1):
                    #region
                    for(int i=0;i < DalObjects.DataSource.Config.BaseStation_arr_index; i++)
                    {
                        DalObjects.DataSource.BaseStation_arr[i].ToString();
                    }
                    #endregion
                    break;
                case (4.2):
                    #region
                    for (int i = 0; i < DalObjects.Get_Drone_arr_index; i++)
                    {
                        DalObjects.DataSource.Drone_arr[i].ToString();
                    }
                    #endregion
                    break;
                case (4.3):
                    #region
                    for (int i = 0; i < DalObjects.get_Customer_arr_index; i++)
                    {
                        DalObjects.DataSource.Customer_arr[i].ToString();
                    }
                    #endregion
                    break;
                case (4.4):
                    #region
                    for (int i = 0; i < DalObjects.Get_Parcel_arr_index; i++)
                    {
                        DalObjects.DataSource.Parcel_arr[i].ToString();
                    }
                    #endregion
                    break;
                case (4.5):
                    #region
                    #endregion
                    break;
                case (4.6):
                    #region
                    #endregion
                    break;
                case (5):
                    #region
                    #endregion
                    break;
                default:
                    break;
            }

        }
    }
}

