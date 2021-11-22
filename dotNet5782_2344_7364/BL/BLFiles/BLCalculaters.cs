using System;
using IDAL;
using IBL.BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace IBL
{
    public partial class BL : IBl
    {
        #region//Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        /// <summary>
        /// //Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="drone1"></param>
        /// <returns></returns>
        public double CalculateBattery(DroneToList drone = null, Drone drone1 = null)//Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        {
            int baseStationId;
            double distance;
            double battery = 0;
            if (drone1 == null)
            {
                Random rnd = new Random();
                //double banan = ListOfDronsBL.Find(element => element.Id == id).Battery);
                baseStationId = CalculateMinDistance(drone.CurrentLocation);
                IDAL.DO.BaseStation newBaseStation = dal.GetBaseStation(baseStationId);
                distance = CalculateDistance(drone.CurrentLocation, new Location(newBaseStation.Longtitude, newBaseStation.Latitude));
                switch (drone.DroneStatuses)
                {
                    case Enums.DroneStatuses.Available:
                        battery = distance * ElectricityUseAvailiblity;
                        battery = battery / 100;
                        battery = (double)rnd.Next((int)battery, 100) / 100;//i think it not lottering good
                        break;
                    case Enums.DroneStatuses.Delivery:
                        switch (drone.Weight)
                        {
                            case Enums.WeightCategories.Light:
                                battery = distance * ElectricityUseLightWeight;
                                battery = (double)rnd.Next((int)battery, 100) / 100;//i think it not lottering good
                                break;
                            case Enums.WeightCategories.Medium:
                                battery = distance * ElectricityUseMediumWeight;
                                battery = (double)rnd.Next((int)battery, 100) / 100;//i think it not lottering good
                                break;
                            case Enums.WeightCategories.Heavy:
                                battery = distance * ElectricityUseHeavyWeight;
                                battery = (double)rnd.Next((int)battery, 100) / 100;//i think it not lottering good
                                break;
                        }
                        break;
                    case Enums.DroneStatuses.Maintenance:
                        battery = (double)rnd.Next(0, 20) / 100;//i think it not lottering good
                        break;
                }
            }
            else
            {
                distance = CalculateDistance(drone1.CurrentLocation, drone1.ParcelInTransit.DeliveryLocation);
                switch (drone.Weight)
                {
                    case Enums.WeightCategories.Light:
                        battery = distance * ElectricityUseLightWeight;
                        break;
                    case Enums.WeightCategories.Medium:
                        battery = distance * ElectricityUseMediumWeight;
                        break;
                    case Enums.WeightCategories.Heavy:
                        battery = distance * ElectricityUseHeavyWeight;
                        break;
                }
            }
            return battery;
        }
        #endregion
        #region//calculate location
        public void CalculateLocation(DroneToList drone)
        {
            switch (drone.DroneStatuses)
            {
                case Enums.DroneStatuses.Available:
                    return;
                case Enums.DroneStatuses.Delivery:

                    double a = CalculateDistance(drone.CurrentLocation, GetBaseStationById(dal.GetDroneCharge(drone.Id).StationId).Location);
                    double b = CalculateDistance(drone.CurrentLocation, GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).SenderId).Location);
                    double c = CalculateDistance(drone.CurrentLocation, GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).TargetId).Location);
                    if (a >= b && a >= c)
                    {
                        ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetBaseStationById(dal.GetDroneCharge(drone.Id).StationId).Location;
                        return;
                    }
                    if (b >= a && b >= c)
                    {
                        ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).SenderId).Location;
                        return;
                    }
                    else
                    {
                        ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetCustomerById(dal.GetParcel(drone.NumberOfParcelInTransit).TargetId).Location;
                        return;
                    }
                case Enums.DroneStatuses.Maintenance:
                    ListOfDronsBL.Find(element => element.Id == drone.Id).CurrentLocation = GetBaseStationById(dal.GetDroneCharge(drone.Id).StationId).Location;
                    return;

            }
        }
        #endregion
        #region//Calculate distance between 2 location return double
        /// <summary>
        /// CalculateDistance
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double CalculateDistance(Location x, Location y)//Calculate distance between 2 location return double
        {
            double ConvertToRadians(double angle)
            {
                return (Math.PI / 180) * angle;
            }
            int R = 6371;

            double f1 = ConvertToRadians(x.Latitude);
            double f2 = ConvertToRadians(y.Latitude);

            double df = ConvertToRadians(x.Latitude - y.Latitude);
            double dl = ConvertToRadians(x.Longitude - y.Longitude);

            double a = Math.Sin(df / 2) * Math.Sin(df / 2) +
            Math.Cos(f1) * Math.Cos(f2) *
            Math.Sin(dl / 2) * Math.Sin(dl / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Calculate the distance.
            double d = R * c;

            return d;
        }
        #endregion
        #region//Convert battery to distance by state and weight 
        /// <summary>
        /// //Convert battery to distance by state and weight 
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        public double ConvertBatteryToDistance(DroneToList drone)//Convert battery to distance by state and weight 
        {
            double battery = drone.Battery;
            double distance = 0;
            switch (drone.DroneStatuses)
            {
                case Enums.DroneStatuses.Available:

                    distance = (battery * 100) / ElectricityUseAvailiblity;
                    break;
                case Enums.DroneStatuses.Delivery:
                    switch (drone.Weight)
                    {
                        case Enums.WeightCategories.Light:
                            distance = (battery * 100) / ElectricityUseLightWeight;
                            break;
                        case Enums.WeightCategories.Medium:
                            distance = (battery * 100) / ElectricityUseMediumWeight;
                            break;
                        case Enums.WeightCategories.Heavy:
                            distance = (battery * 100) / ElectricityUseHeavyWeight;
                            break;
                    }
                    break;
                    //case Enums.DroneStatuses.Maintenance:
                    //  distance = (battery * 100) /DroneInCharging;////
                    //break;
            }
            return distance;
        }
        #endregion
        #region//Calculate min distance between loction y and 2 option 1.the closer station 2.the closer station and more 2 terms
        public int CalculateMinDistance(Location y, Predicate<BaseStation> predicate = null, Predicate<BaseStation> predicate1 = null)
        {
            double min = double.MaxValue;
            int baseStationId = 0;
            double distance;
            if (predicate == null && predicate1 == null)
            {

                foreach (var item in dal.GetListOfBaseStation())//check which station is clothest for the sender
                {
                    distance = CalculateDistance(new Location(item.Latitude, item.Latitude), y);
                    if (distance < min)
                    {
                        baseStationId = item.Id;
                        min = distance;
                    }
                }
            }
            else
            {
                foreach (var item in dal.GetListOfBaseStation())//check which station is clothest for the sender
                {
                    distance = CalculateDistance(new Location(item.Latitude, item.Latitude), y);
                    if (distance < min && (predicate != null) && (predicate1 != null))
                    {
                        baseStationId = item.Id;
                        min = distance;
                    }
                }
            }
            return baseStationId;
        }
        #endregion

        public int ReciveParcelId(Parcel parcel)
        {
            Predicate<IDAL.DO.Parcel> predicate = element => element.SenderId == parcel.Sender.Id; // predicat to find parcel based on senders id

            IDAL.DO.Parcel newParcel = dal.GetParcel(parcel.Sender.Id, predicate); // get parcel of dal type based on senders id
            return newParcel.Id;

        }
        #region//calculate state of parcel 
        /// <summary>
        /// calculate state of parcel 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Enums.ParcelStatus StatusCalculate(IDAL.DO.Parcel p)
        {
            if (p.Deliverd != DateTime.MinValue)
                return Enums.ParcelStatus.Supplied;
            if (p.PickedUp != DateTime.MinValue)
                return Enums.ParcelStatus.Collected;
            if (p.Scheduled != DateTime.MinValue)//AssociationTime
                return Enums.ParcelStatus.Associated;
            return Enums.ParcelStatus.Defined;

        }
        #endregion
    }
}
