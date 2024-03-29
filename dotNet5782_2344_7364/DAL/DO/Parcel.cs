﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Parcel
    {
        /// <summary>
        /// This struct contains data for a parcel including:
        /// Id number for each parcel, Senders Id, Targets Id, The weight of the parcel,
        /// parcels priority, requested time, Drones Id, Scheduled time, PickedUp time
        /// and Deliverd time
        /// </summary>
        public int? Id { get; set; }  // Id number for each parcel
        public int? SenderId { get; set; } // Senders Id
        public int? ReciverId { get; set; } // Targets Id
        public WeightCategories Weight { get; set; } // The weight of the parcel
        public Priorities Priority { get; set; } // parcels priority
        public DateTime? CreatingTime { get; set; } //requested time
        public int? DroneId { get; set; } // Drones Id
        public DateTime? AssociatedTime { get; set; } // Scheduled time
        public DateTime? PickedUp { get; set; }  // PickedUp time
        public DateTime? Deliverd { get; set; } // Deliverd time
        public override string ToString() // return string with all the data
        {
            return $" Parcel #{Id}: \n Sender Id = {SenderId} \n Target Id = {ReciverId} \n Weight = {Weight} \n Priority = {Priority} \n Requsted = {CreatingTime} \n Drone Id = {DroneId} \n Scheduled = {AssociatedTime} \n Picked up = {PickedUp} \n Deliverd = {Deliverd} \n";
        }
    }
}