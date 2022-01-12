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

    public class DroneToList : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("id"); } }
        private string model;
        public string Model { get { return model; } set { model = value; OnPropertyChanged("model"); } }
        private Enums.WeightCategories weight;
        public Enums.WeightCategories Weight { get { return weight; } set { weight = value; OnPropertyChanged("weight"); } }
        private double battery;
        public double Battery { get { return battery; } set { battery = value; OnPropertyChanged("battery"); } }
        private Enums.DroneStatuses droneStatuses;
        public Enums.DroneStatuses DroneStatuses { get { return droneStatuses; } set { droneStatuses = value; OnPropertyChanged("droneStatuses"); } }
        private Location location;
        public Location CurrentLocation { get { return location; } set { location = value; OnPropertyChanged("location"); } }
        private int? numberOfParcelInTransit;
        public int? NumberOfParcelInTransit { get { return numberOfParcelInTransit; } set{ numberOfParcelInTransit = value;OnPropertyChanged("numberOfParcelInTransit"); } }

        public override string ToString()
        {
            if (NumberOfParcelInTransit == null) // if not inistialized
            {
                return $" Drone #{Id}: \n Weight = {Weight} \n Battery = {Battery * 100}% \n Drone Status = {DroneStatuses} \n Current location:  {CurrentLocation.ToString()} \n Number of parcel in transit = 0 \n";

            }
            return $" Drone #{Id}: \n Weight = {Weight} \n Battery = {Battery * 100}% \n Drone Status = {DroneStatuses} \n Current location:  {CurrentLocation.ToString()} \n Number of parcel in transit = {NumberOfParcelInTransit} \n";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prpertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prpertyName));
            }
        }

    }
}
