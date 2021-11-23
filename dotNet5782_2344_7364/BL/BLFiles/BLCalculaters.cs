using System;
using IDAL;
using IBL.BO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace IBL
{
    /// <summary>
    /// All the calculation functions for BL
    /// </summary>
    public partial class BL : IBl
    {
        #region//Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        /// <summary>
        /// //Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="drone1"></param>
        /// <returns></returns>
        private double calculateBattery(DroneToList drone = null, Drone drone1 = null,double distance = 0.0)//Calculate battery(distance*electricty by state) with 2 option 1.drone to list by lottery value 2.drone by calculation
        {
            int baseStationId;
            
            double battery = 0;
            if (drone1 == null)
            {
                Random rnd = new Random();
                //double banan = ListOfDronsBL.Find(element => element.Id == id).Battery);
                baseStationId = calculateMinDistance(drone.CurrentLocation);
                IDAL.DO.BaseStation newBaseStation = dal.GetBaseStation(baseStationId);
                distance = calculateDistance(drone.CurrentLocation, new Location(newBaseStation.Longtitude, newBaseStation.Latitude));
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
                switch (drone1.Weight)
                {
                    case Enums.WeightCategories.Light:
                        battery = (distance * ElectricityUseLightWeight) / 100;
                        break;
                    case Enums.WeightCategories.Medium:
                        battery = (distance * ElectricityUseMediumWeight) / 100;
                        break;
                    case Enums.WeightCategories.Heavy:
                        battery = (distance * ElectricityUseHeavyWeight) / 100;
                        break;
                }
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
        private double convertBatteryToDistance(DroneToList drone)//Convert battery to distance by state and weight 
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
        /// <summary>
        /// Calculate min distance between loction y and 2 option 1.the closer station 2.the closer station and more 2 terms
        /// </summary>
        /// <param name="y"></param>
        /// <param name="predicate"></param>
        /// <param name="predicate1"></param>
        /// <returns></returns>
        private int calculateMinDistance(Location y, Predicate<BaseStation> predicate = null, Predicate<BaseStation> predicate1 = null)//Calculate min distance between loction y and 2 option 1.the closer station 2.the closer station and more 2 terms
        {
            double min = double.MaxValue;
            int baseStationId = 0;
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
            if ((drone.Battery - calculateBattery(drone, null, distance)) < 0)
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
        private Enums.ParcelStatus statusCalculate(IDAL.DO.Parcel p)
        {
            if (p.Deliverd != DateTime.MinValue)
                return Enums.ParcelStatus.Supplied;
            if (p.PickedUp != DateTime.MinValue)
                return Enums.ParcelStatus.Collected;
            if (p.AssociatedTime != DateTime.MinValue)//AssociationTime
                return Enums.ParcelStatus.Associated;
            return Enums.ParcelStatus.Defined;

        }
        #endregion
    }
}
