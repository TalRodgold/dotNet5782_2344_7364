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

    public class CustomerInParcel : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("id"); } }
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("name"); }  }
        public override string ToString()
        {
            return $"Customer #{Id}: \n\t Name = {Name}";
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