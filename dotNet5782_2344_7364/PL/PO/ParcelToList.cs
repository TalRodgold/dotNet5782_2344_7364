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

    public class ParcelToList : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("id"); }  }
        private string senderName;
        public string SendersName { get { return senderName; } set { senderName = value; OnPropertyChanged("senderName"); } }
        private string reciverName;
        public string ReciversName { get { return reciverName; } set { reciverName = value; OnPropertyChanged("reciverName"); }  }
        private Enums.WeightCategories weight;
        public Enums.WeightCategories Weight { get { return weight; } set { weight = value; OnPropertyChanged("weight"); }  }
        private Enums.Priorities prioritie;
        public Enums.Priorities Prioritie { get { return prioritie; } set { prioritie = value; OnPropertyChanged("prioritie"); }  }
        private Enums.ParcelStatus parcelStatus;
        public Enums.ParcelStatus ParcelStatus { get { return parcelStatus; } set { parcelStatus = value; OnPropertyChanged("parcelStatus"); }  }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prpertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prpertyName));
            }
        }
        public override string ToString()
        {
            return $" Parcel #{Id}: \n Sender = {SendersName} \n Reciver = {ReciversName} \n weight = {Weight} \n Prioritie = {Prioritie} \n Parcel status = {ParcelStatus} \n";
        }
    }
}