﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// class for drone
    /// </summary>
    public class Drone 
    {
        public int? Id { set; get; } = null;
        public string Model { set; get; }
        public Enums.WeightCategories Weight { set; get; }
        public double Battery { set; get; }
        public Enums.DroneStatuses DroneStatuses { set; get; }
        public ParcelInTransit ParcelInTransit { set; get; }
        public Location CurrentLocation { set; get; }
        public override string ToString()
        {
            return $" Drone #{Id}: \n Model = {Model} \n Weight = {Weight} \n Battery = {Battery * 100}% \n Statuses = {DroneStatuses} \n Parcel in transit {ParcelInTransit} \n Current location: {CurrentLocation.ToString()} \n";
        }

        public Drone(int? id, string model, Enums.WeightCategories weight, double battery, Enums.DroneStatuses droneStatuses, ParcelInTransit parcelInTransit, Location location) // constructor
        {
            Id = id;
            Model = model;
            Weight = weight;
            Battery = battery;
            DroneStatuses = droneStatuses;
            ParcelInTransit = parcelInTransit;
            CurrentLocation = location;
        }
        public Drone(int? id, string model, Enums.WeightCategories weight) // constructor
        {
            Random rnd = new Random(); // generate randome number

            Id = id;
            Model = model;
            Weight = weight;
            DroneStatuses = Enums.DroneStatuses.Maintenance;
            Battery = (double)rnd.Next(20, 40) / 100;

        }
    }
}
