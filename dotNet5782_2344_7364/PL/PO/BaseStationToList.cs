using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BO;
using System.Windows;

namespace PO
{

    public class BaseStationToList : INotifyPropertyChanged
    {
        //BO.BaseStationToList baseStationToList;
        //public BO.BaseStationToList baseStationToList_ { get { return baseStationToList; } set { OnPropertyChanged("baseStationToList_"); } }
        public event PropertyChangedEventHandler PropertyChanged;
        private int? id;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("id"); } }
        private string name;
        public string Name { get { return name; }  set { name = value;  OnPropertyChanged("Name"); } }
        //public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(BaseStation));
        private int freeChargingSlots;
        public int FreeChargingSlots { get { return freeChargingSlots; } set { freeChargingSlots = value; OnPropertyChanged("FreeChargingSlots"); }  }
        private int occupiedChargingSlots;
        public int OccupiedChargingSlots { get { return occupiedChargingSlots; } set { occupiedChargingSlots = value; OnPropertyChanged("occupiedChargingSlots"); } }//
        private void OnPropertyChanged(string propertyName)
        {
            //if (PropertyChanged != null)
            //{
            //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            //}
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        public override string ToString()
        {
            return $" Base station #{Id}: \n Name = {Name} \n Free charging slots = {FreeChargingSlots} \n Occupied charging slots = {OccupiedChargingSlots} \n ";
        }

    }
}
