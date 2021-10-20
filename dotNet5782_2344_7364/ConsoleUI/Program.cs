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
            Console.WriteLine("");
            int User_input = Console.Read();
            switch (User_input)
            {
                case (1)://BaseStation addition
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
                case (2)://Drone addition
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
                case (3)://Castomer addition
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
                case (4)://Percel addition
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
                    DalObjects.DalObjects.ConstructParcel(User_ParcelId, User_SenderId, User_TargetId, User_ParcelWeightCategories, User_ParcelPriorities, User_Requested,0,DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                    #endregion
                    break;
                default:
                    break;
            }

        }
    }
}
