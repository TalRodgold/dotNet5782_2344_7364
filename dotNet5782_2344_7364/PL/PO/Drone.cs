using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BO;

namespace PO
{
    public class Drone : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value;OnPropertyChanged("id"); } }
        private string model;
        public string Model { get { return model; } set { model = value; OnPropertyChanged("model"); }  }
        private Enums.WeightCategories weight;
        public Enums.WeightCategories Weight { get { return weight; } set { weight = value; OnPropertyChanged("weight"); } }
        private double battery;
        public double Battery { get { return battery; } set { battery=value; OnPropertyChanged("battery"); } }
        private Enums.DroneStatuses droneStatuses;
        public Enums.DroneStatuses DroneStatuses { get { return droneStatuses; } set { droneStatuses = value; OnPropertyChanged("droneStatuses"); } }
        private ParcelInTransit parcelInTransit;
        public ParcelInTransit ParcelInTransit { get { return parcelInTransit; } set { parcelInTransit = value; OnPropertyChanged("parcelInTransit"); } }
        private Location location;
        public Location CurrentLocation { get { return location; } set { location = value; OnPropertyChanged("location"); } }
        public override string ToString()
        {
            return $" Drone #{Id}: \n Model = {Model} \n Weight = {Weight} \n Battery = {Battery * 100}% \n Statuses = {DroneStatuses} \n Parcel in transit {ParcelInTransit} \n Current location: {CurrentLocation.ToString()} \n";
        }
        public event PropertyChangedEventHandler PropertyChanged; 
        private void OnPropertyChanged(string prpertyName)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prpertyName));
            }
        }

    }
}
