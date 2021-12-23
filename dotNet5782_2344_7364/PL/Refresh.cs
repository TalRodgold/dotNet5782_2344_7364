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
        private string _user;
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("User"));
                }
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Password"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
