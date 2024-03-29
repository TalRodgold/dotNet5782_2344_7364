﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// class for drone to list
    /// </summary>
    public class DroneToList
    {
        public int? Id { set; get; } = null;
        public string Model { set; get; }
        public Enums.WeightCategories Weight { set; get; }
        public double Battery { set; get; }
        public Enums.DroneStatuses DroneStatuses { set; get; }
        public Location CurrentLocation { set; get; }
        public int? NumberOfParcelInTransit { set; get; } = null;
        public override string ToString()
        {
            if (NumberOfParcelInTransit == null) // if not inistialized
            {
                return $" Drone #{Id}: \n Weight = {Weight} \n Battery = {Battery * 100}% \n Drone Status = {DroneStatuses} \n Current location:  {CurrentLocation.ToString()} \n Number of parcel in transit = 0 \n";

            }
            return $" Drone #{Id}: \n Weight = {Weight} \n Battery = {Battery * 100}% \n Drone Status = {DroneStatuses} \n Current location:  {CurrentLocation.ToString()} \n Number of parcel in transit = {NumberOfParcelInTransit} \n";
        }
        public DroneToList() { NumberOfParcelInTransit = null; } // dedault
        public DroneToList(int? id, string model, Enums.WeightCategories weight, double battery, Enums.DroneStatuses droneStatuses, Location location, int? numberOfParcels) // constructor
        {
            Id = id;
            Model = model;
            Weight = weight;
            Battery = battery;
            DroneStatuses = droneStatuses;
            CurrentLocation = location;
            NumberOfParcelInTransit = numberOfParcels;
        }
    }
}
