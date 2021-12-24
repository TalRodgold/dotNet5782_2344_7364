using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BlApi;

namespace PL
{

    public class Refresh : INotifyPropertyChanged
    {
        public string Name
        {
            get { return Name; }
            set { Name = value; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void funcPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
     
    }
}
