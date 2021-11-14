﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            public int Id { set; get; }
            public string Model { set; get; }
            public Enums.WeightCategories Weight { set; get; }
            public double Battery { set; get; }
            public Enums.DroneStatuses DroneStatuses { set; get; }
            public Location CurrentLocation { set; get; }
            public int NumberOfParcelInTransit { set; get; }
            public override string ToString()
            {
                return $" Drone #{Id}: \n Battery = {Battery} Weight = {Weight} \n Battery = {Battery} \n Drone Status = {DroneStatuses} \n Current location = {CurrentLocation} \n Number of parcel in transit = {NumberOfParcelInTransit} \n";
            }
            public DroneToList(int id, string model, Enums.WeightCategories weight , double battery, Enums.DroneStatuses droneStatuses, Location location, int numberOfParcels)
            {
                Id = id;
                Model = model;
                Weight = weight;
                Battery = battery;
                DroneStatuses = droneStatuses;
                DroneStatuses = droneStatuses;
                CurrentLocation = location;
                NumberOfParcelInTransit = numberOfParcels;
            }
        }
    }
    
}