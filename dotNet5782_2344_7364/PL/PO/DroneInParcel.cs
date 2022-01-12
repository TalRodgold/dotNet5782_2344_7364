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

    public class DroneInParcel : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("id"); }  }
        private double battery;
        public double Battery { get { return battery; } set { battery = value; OnPropertyChanged("battery"); } }
        private Location location;
        public Location CurrentLocation { get { return location; } set { location = value; OnPropertyChanged("location"); } }
        public override string ToString()
        {
            return $" Drone #{Id}: \n Battery = {Battery * 100}% \n Current location: {CurrentLocation.ToString()} \n";
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
