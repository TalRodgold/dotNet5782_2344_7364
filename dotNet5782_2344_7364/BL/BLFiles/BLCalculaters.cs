using System;
using DalApi;
using BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace BlApi
{
    /// <summary>
    /// All the calculation functions for BL
    /// </summary>
    internal sealed partial class BL : IBl
    {
        #region//Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        /// <summary>
        /// //Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="drone1"></param>
        /// <returns></returns>
        private double calculateBattery(DroneToList drone = null,double distance = 0.0)//Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        {
            int? baseStationId;
            double battery = drone.Battery;
            Random rnd = new Random();
            baseStationId = calculateMinDistance(drone.CurrentLocation);
            DO.BaseStation newBaseStation = dal.GetBaseStation(baseStationId);
           
            if(distance==0.0)
            {
                distance = calculateDistance(drone.CurrentLocation, new Location(newBaseStation.Longtitude, newBaseStation.Latitude));
                
                switch (drone.DroneStatuses)
                {
                    case Enums.DroneStatuses.Available: // if in availble
                        if (distance != 0)
                            battery = distance * ElectricityUseAvailiblity;
                        battery = battery / 100;
                        battery = (double)rnd.Next((int)battery, 100) / 100;
                        break;
                    case Enums.DroneStatuses.Delivery: // if in delivery
                        switch (drone.Weight)
                        {
                            case Enums.WeightCategories.Light: // if light
                                if (distance != 0)
                                    battery = distance * ElectricityUseLightWeight;
                                battery = (double)rnd.Next((int)battery, 100) / 100;
                                break;
                            case Enums.WeightCategories.Medium: // if medium
                                if (distance != 0)
                                    battery = distance * ElectricityUseMediumWeight;
                                battery = (double)rnd.Next((int)battery, 100) / 100;
                                break;
                            case Enums.WeightCategories.Heavy: // if heavy
                                if (distance != 0)
                                    battery = distance * ElectricityUseHeavyWeight;
                                battery = (double)rnd.Next((int)battery, 100) / 100;
                                break;
                        }
                        break;
                    case Enums.DroneStatuses.Maintenance: // if in maintenance
                        battery = (double)rnd.Next(0, 20) / 100;
                        break;
                }
            }
            else
            {
                //if(distance==0)
                //{
                //    return battery;
                //}
                    switch (drone.Weight)
                {
                    case Enums.WeightCategories.Light: // if light
                        battery -= (distance * ElectricityUseLightWeight)/100;
                        break;
                    case Enums.WeightCategories.Medium: // if medium
                        battery -= (distance * ElectricityUseMediumWeight)/100;
                        break;
                    case Enums.WeightCategories.Heavy: // if heavy
                        battery -= (distance * ElectricityUseHeavyWeight)/100;
                        break;
                }
            }
           
            return battery;
        }
        #endregion
        #region //Calculate battery(distance*electricty by state) 
        private double calculateBattery( Drone drone = null, double distance = 0.0)//Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        {
            double battery = drone.Battery;
            switch (drone.Weight)
            {
                case Enums.WeightCategories.Light: // for light weight
                    battery = (distance * ElectricityUseLightWeight )/ 100;
                    break;
                case Enums.WeightCategories.Medium: // for medium weight
                    battery =( distance * ElectricityUseMediumWeight) / 100;
                    break;
                case Enums.WeightCategories.Heavy: // for heavy weight 
                    battery = (distance * ElectricityUseHeavyWeight / 100);
                    break;
            }
            return battery;
        }
        #endregion
        #region//Calculate distance between 2 location return double
        /// <summary>
        /// CalculateDistance
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private double calculateDistance(Location x, Location y)//Calculate distance between 2 location return double
        {
            double ConvertToRadians(double angle)
            {
                return Math.PI / 180 * angle;
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
        private double convertBatteryToDistance(DroneToList drone)//Convert battery to distance by state and weight 
        {
            double battery = drone.Battery;
            double distance = 0;
            switch (drone.DroneStatuses)
            {
                case Enums.DroneStatuses.Available: // if available
                    distance = battery * 100 / ElectricityUseAvailiblity;
                    break;
                case Enums.DroneStatuses.Delivery: // if in delivary
                    switch (drone.Weight)
                    {
                        case Enums.WeightCategories.Light: // for light weight
                            distance = battery * 100 / ElectricityUseLightWeight;
                            break;
                        case Enums.WeightCategories.Medium: // for medium weight
                            distance = battery * 100 / ElectricityUseMediumWeight;
                            break;
                        case Enums.WeightCategories.Heavy: // for heavy weight
                            distance = battery * 100 / ElectricityUseHeavyWeight;
                            break;
                    }
                    break;
            }
            return distance;
        }
        #endregion
        #region//Calculate min distance between loction y and 2 option 1.the closer station 2.the closer station and more 2 terms
        /// <summary>
        /// Calculate min distance between loction y and 2 option 1.the closer station 2.the closer station and more 2 terms
        /// </summary>
        /// <param name="y"></param>
        /// <param name="predicate"></param>
        /// <param name="predicate1"></param>
        /// <returns></returns>
        private int? calculateMinDistance(Location y, Predicate<BaseStation> predicate = null, Predicate<BaseStation> predicate1 = null)//Calculate min distance between loction y and 2 option 1.the closer station 2.the closer station and more 2 terms
        {
            double min = double.MaxValue;
            int? baseStationId = null;
            double distance;
            if (predicate == null && predicate1 == null)
            {
                foreach (var item in dal.GetListOfBaseStation())//check which station is clothest for the sender
                {
                    distance = calculateDistance(new Location(item.Latitude, item.Latitude), y);
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
                    distance = calculateDistance(new Location(item.Latitude, item.Latitude), y);
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
        #region//Calculate whether the drone have enogh battery
        /// <summary>
        /// Calculate whether the drone have enogh battery
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="drone"></param>
        /// <returns></returns>
        private bool CalculateWhetherTheDroneHaveEnoghBattery(double distance, DroneToList drone)//Calculate whether the drone have enogh battery
        {
            double battery= calculateBattery(drone, distance);
            if ((drone.Battery - battery) < 0 || (battery<0))
            {
                return false;
            }
            return true;
        }
        #endregion
        #region//calculate state of parcel 
        /// <summary>
        /// calculate state of parcel 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Enums.ParcelStatus statusCalculate(DO.Parcel p)
        {
            if (p.Deliverd != null) // deliverd time
                return Enums.ParcelStatus.Supplied;
            if (p.PickedUp != null) // pickup time
                return Enums.ParcelStatus.Collected;
            if (p.AssociatedTime != null) // association time
                return Enums.ParcelStatus.Associated;
            return Enums.ParcelStatus.Defined;
        }
        #endregion
    }
}
