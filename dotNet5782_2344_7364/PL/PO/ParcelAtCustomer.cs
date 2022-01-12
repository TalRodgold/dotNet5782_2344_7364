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

    public class ParcelAtCustomer : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("id"); } }
        private Enums.WeightCategories weight;
        public Enums.WeightCategories Weight { get { return weight; } set { weight = value; OnPropertyChanged("weight"); }  }
        private Enums.Priorities prioritie;
        public Enums.Priorities Prioritie { get { return prioritie; } set { prioritie = value; OnPropertyChanged("prioritie"); } }
        Enums.ParcelStatus parcelStatus;
        public Enums.ParcelStatus ParcelStatus { get { return parcelStatus; } set { parcelStatus = value;OnPropertyChanged("parcelStatus"); } }
        CustomerInParcel customerInParcel;
        public CustomerInParcel CustomerInParcel { get { return customerInParcel; }  set { customerInParcel = value; OnPropertyChanged("customerInParcel"); } }
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
