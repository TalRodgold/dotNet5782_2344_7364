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
using System.Collections.ObjectModel;

namespace PL
{
    public class Refresh : INotifyPropertyChanged
    {
        private IBl bl = BlFactory.GetBl("BL");
        public ObservableCollection<BO.CustomerToList> CustomerUpdate
        {
            get { return CustomerUpdate; }
            set { CustomerUpdate = bl.GetListOfCustomerToList(); }
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
