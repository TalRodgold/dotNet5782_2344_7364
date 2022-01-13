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
                try
                {
                    if (!bl.CheckAvailableParcels(drone))
                    {
                        if ( drone.Battery == 1)
                        {
                            break;
                        }
                        bl.UpdateSendDroneToCharge(drone.Id);
                        drone = bl.GetDroneById(droneId);
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
                                            if (drone.Battery < 0.2)
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
                                        }
                                        break;
                                }
                                break;
                            }
                        case Enums.DroneStatuses.Maintenance:
                            if (!sleepDelayTime()) break;
                            lock (bl)
                            {
                                bl.UpdateReleseDrone(drone.Id);
                                drone = bl.GetDroneById(droneId);
                                if (drone.Battery == 1)
                                {
                                    maintenance = Maintenance.Starting;
                                }
                                else
                                {
                                    bl.UpdateSendDroneToCharge(drone.Id);
                                }
                            }
                            break;
                    }
                    drone = bl.GetDroneById(droneId);
                    action();
                }
                catch (Exception)
                {
                    break;
                }
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
