﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Customer
    {
        /// <summary>
        /// This struct contains data for a customer including:
        /// Id number for each costomer, costomers name, customers phone number
        /// Longitude andLatitude
        /// </summary>
        public int? Id { get; set; } // Id number for each costomer
        public string Name { get; set; } // costomers name
        public string Phone { get; set; } // customers phone number
        public double Longtitude { get; set; } // Longitude 
        public double Latitude { get; set; } // Latitude
        public override string ToString() // return string with all the data
        {
            return $" Customer #{Id}: \n Name = {Name} \n Phone = {Phone} \n Longtitude = {DalObjects.DataSource.sexagesimal(Longtitude, 'N')} \n Latitude = {DalObjects.DataSource.sexagesimal(Longtitude, 'E')} \n";
        }
    }
}
