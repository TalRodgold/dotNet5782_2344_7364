using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using System.Diagnostics;
using BlApi;
using static System.Math;
using System.Runtime.CompilerServices;

using DalApi;

namespace BlApi
{
    /// <summary>
    /// run a simulation on drones
    /// </summary>
    class Simulator
    {
        private const int delay = 500;
        enum Maintenance { Starting, Going, Charging };
        Drone drone;
        BlApi.IBl bl;

        internal Simulator(BlApi.IBl BL, int droneId, Action action, Func<bool> checkStop)
        {
            bl = BL;
            var dal = DalFactory.GetDal("DalXml");
            drone = bl.GetDroneById(droneId);
            Maintenance maintenance = Maintenance.Starting;
            do
            {
                if (!bl.CheckAvailableParcels(drone) && drone.Battery == 1)
                {
                    break;
                }
                    switch (drone.DroneStatuses)
                    {
                    case Enums.DroneStatuses.Available:
                        if (!sleepDelayTime()) break;
                        lock (bl)
                        {
                            bl.UpdateAssosiateDrone(drone.Id);
                        }
                        break;
                    case Enums.DroneStatuses.Delivery:
                        {
                            switch (maintenance)
                            {
                                case Maintenance.Starting:
                                    if (!sleepDelayTime()) break;
                                    lock (bl)
                                    {
                                        bl.PickupParcelByDrone(drone.Id);
                                        maintenance = Maintenance.Going;
                                    }
                                    break;
                                case Maintenance.Going:
                                    if (!sleepDelayTime()) break;
                                    lock (bl)
                                    {
                                        bl.DeliveryParcelByDrone(drone.Id);
                                        if(drone.Battery<0.2)
                                        {
                                            bl.UpdateSendDroneToCharge(drone.Id);
                                            maintenance = Maintenance.Charging;
                                            break;
                                        }
                                        maintenance = Maintenance.Starting;
                                    }
                                    break;
                                case Maintenance.Charging:
                                    if (!sleepDelayTime()) break;
                                    lock (bl)
                                    {
                                        if (drone.Battery == 1)
                                        {
                                            bl.UpdateReleseDrone(drone.Id);
                                            maintenance = Maintenance.Starting;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    break;
                            }
                            break;                           
                        }
                    case Enums.DroneStatuses.Maintenance:
                        if (!sleepDelayTime()) break;
                        lock (bl)
                        {

                            while (drone.Battery < 1)
                            {
                                drone.Battery += 0.02;
                                action();

                            }
                            bl.UpdateReleseDrone(drone.Id);
                            maintenance = Maintenance.Starting;
    
                        }
                        break;
                    }
                drone = bl.GetDroneById(droneId);
                action();
            } while (!checkStop());
        }

        private static bool sleepDelayTime()
        {
            try
            {
                Thread.Sleep(delay);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}





//class Simulator
//{
//    private Stopwatch stopWatch;
//    private const double speedDrone = 1.5;
//    private const int delay = 500;
//    private const double time = delay / 1000.0;
//    private const double step = speedDrone / time;
//    enum Maintenance { Starting, Going, Charging };
//    DroneToList drone;
//    BlApi.IBl bl;

//    internal Simulator(BlApi.IBl BL, int droneId, Action action, Func<bool> checkStop)
//    {
//        bl = BL;
//        var dal = DalFactory.GetDal("DalXml");
//        drone = bl.GetListOfDronesToList().FirstOrDefault(d => d.Id == droneId);
//        int? parcelId = 0;
//        int? satationId = 0;
//        BaseStation station = null;
//        double distance = 0.0;
//        int batteryUsage = 0;
//        BO.Parcel parcel = null;
//        bool pickedUp = false;
//        Customer customer = null;
//        int id = 0;
//        Maintenance maintenance = Maintenance.Starting;
//        void initDeliverry(int? id)
//        {
//            parcel = bl.GetParcelById(id);
//            batteryUsage = (int)parcel.Weight;
//            pickedUp = parcel.PickupTime is not null;
//            customer = bl.GetCustomerById((int)(pickedUp ? parcel.Reciver.Id : parcel.Sender.Id));
//        }

//        do
//        {
//            switch (drone.DroneStatuses)
//            {
//                case Enums.DroneStatuses.Available:
//                    {
//                        if (!sleepDelayTime()) break;
//                        lock (bl)
//                        {

//                            parcelId = bl.GetListOfParcels().Where(p => p?.AssociationTime == null
//                            && (Enums.WeightCategories)(p.Weight) <= drone.Weight
//                            && bl.CalculateWhetherTheDroneHaveEnoghBattery(bl.calculateDistance(drone.CurrentLocation, bl.GetCustomerById(parcel.Sender.Id).Location), drone)).FirstOrDefault().Id;
//                            switch (parcelId, drone.Battery)
//                            {
//                                case (0, 10):
//                                    break;
//                                case (0, _):
//                                    satationId = bl.calculateMinDistance(drone.CurrentLocation);
//                                    if (satationId != 0)
//                                    {
//                                        drone.DroneStatuses = Enums.DroneStatuses.Maintenance;
//                                        maintenance = Maintenance.Starting;
//                                        bl.UpdateSendDroneToCharge(drone.Id);
//                                    }
//                                    break;
//                                case (_, _):
//                                    try
//                                    {
//                                        bl.UpdateAssosiateDrone(drone.Id);
//                                        drone.NumberOfParcelInTransit = parcelId;
//                                        initDeliverry(parcelId);
//                                        drone.DroneStatuses = Enums.DroneStatuses.Delivery;
//                                    }
//                                    catch (Exception exception)
//                                    {
//                                        ///לטפל בחריגה פה
//                                        break;
//                                    }
//                                    break;
//                            }
//                        }
//                        break;
//                    }
//                case Enums.DroneStatuses.Maintenance:
//                    switch (maintenance)
//                    {
//                        case Maintenance.Starting:
//                            lock (bl)
//                            {
//                                try { station = bl.GetBaseStationById(satationId); }
//                                catch { }
//                                distance = Distance(drone, station.Location);
//                                maintenance = Maintenance.Going;
//                            }
//                            break;
//                        case Maintenance.Going:
//                            if (distance < 0.01)
//                            {
//                                lock (bl)
//                                {
//                                    drone.CurrentLocation = station.Location;
//                                    maintenance = Maintenance.Charging;
//                                }
//                            }
//                            else
//                            {
//                                if (!sleepDelayTime()) break;
//                                lock (bl)
//                                {
//                                    double difference = distance < step ? distance : step;
//                                    distance -= difference;
//                                    drone.Battery = bl.calculateBattery(drone, distance);
//                                }
//                            }
//                            break;
//                        case Maintenance.Charging:
//                            if (drone.Battery == 100)
//                            {
//                                lock (bl)
//                                {
//                                    drone.DroneStatuses = Enums.DroneStatuses.Available;
//                                    bl.UpdateReleseDrone(droneId);
//                                }
//                            }
//                            else
//                            {
//                                if (!sleepDelayTime()) break;
//                                {
//                                    lock (bl)
//                                        drone.Battery = bl.calculateBattery(drone, distance);
//                                }
//                            }
//                            break;
//                        default:
//                            //זריקת חריגה
//                            break;
//                    }
//                    break;
//                case Enums.DroneStatuses.Delivery:
//                    lock (bl)
//                    {
//                        try
//                        {
//                            if (parcelId == 0) initDeliverry((int)drone.NumberOfParcelInTransit);
//                        }
//                        catch (Exception exception)
//                        {
//                            throw;
//                        }
//                        distance = Distance(drone, customer.Location);
//                    }
//                    if (distance < 0.01 || drone.Battery == 0)
//                    {
//                        lock (bl)
//                        {
//                            drone.CurrentLocation = customer.Location;
//                            if (pickedUp)
//                            {
//                                dal.UpdateParcleDelivery(parcelId);
//                                drone.DroneStatuses = Enums.DroneStatuses.Available;
//                            }
//                            else
//                            {
//                                dal.UpdateParclePickup(parcelId);
//                                customer = bl.GetCustomerById(parcel.Reciver.Id);
//                                pickedUp = true;
//                            }
//                        }
//                    }
//                    else
//                    {
//                        if (!sleepDelayTime()) break;
//                        lock (bl)
//                        {
//                            double difference = distance < step ? distance : step;
//                            double p = difference / distance;
//                            drone.Battery = bl.calculateBattery(drone, distance);
//                            double Longitude = drone.CurrentLocation.Latitude + (customer.Location.Latitude - drone.CurrentLocation.Latitude) * p;
//                            double Lattitude = drone.CurrentLocation.Longitude + (customer.Location.Longitude - drone.CurrentLocation.Longitude) * p;
//                            drone.CurrentLocation = new Location(Lattitude, Longitude);
//                        }
//                    }
//                    break;
//                default:
//                    break;
//            }
//            action();
//        } while (!checkStop());
//    }

//    private double Distance(DroneToList drone, Location location)
//    {
//        return bl.calculateDistance(drone.CurrentLocation, location);
//    }

//    private static bool sleepDelayTime()
//    {
//        try
//        {
//            Thread.Sleep(delay);
//        }
//        catch (Exception ex)
//        {
//            return false;
//        }
//        return true;
//    }
//}
