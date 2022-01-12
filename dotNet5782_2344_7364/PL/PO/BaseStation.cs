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

    public class BaseStation : INotifyPropertyChanged
    {
        //BO.BaseStation baseStation;
        //public BO.BaseStation baseStation_ { get { return baseStation; } set { OnPropertyChanged("baseStation_"); }
        //}
        private int? id = null;
        public int? Id { get { return id; } set { id = value;OnPropertyChanged("id"); }  }
        private string name;
        public string Name { get {return name; } set { name = value; OnPropertyChanged("Name"); } }
        private Location location;
        public Location Location { get { return location; } set { location = value; OnPropertyChanged("location"); }  }
        private int numberOfFreeChargingSlots;
        public int NumberOfFreeChargingSlots { get { return numberOfFreeChargingSlots; } set { numberOfFreeChargingSlots = value; OnPropertyChanged("numberOfFreeChargingSlots"); } }
        private List<DroneInCharging> listOfDroneInCharging;
        public List<DroneInCharging> ListOfDroneInCharging { get { return listOfDroneInCharging; } set { listOfDroneInCharging = value; OnPropertyChanged("listOfDroneInCharging"); }  }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}