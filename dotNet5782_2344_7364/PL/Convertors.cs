using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class BatteryToProgressBar:IValueConverter
    {
        public object Convert(object value, Type targetType, object parrameter, CultureInfo culture)
        {
            return (double)value * 100;
        }
        public object ConvertBack(object value, Type targetType, object parrameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }
    internal class BatteryToConverter : IValueConverter
    {
        public object Convert(object value,Type targetType,object parrameter,CultureInfo culture)
        {
            double battery = (double)value;
            if (battery < 0.1)
                return Brushes.DarkRed;
            if (battery < 0.2)
                return Brushes.DarkRed;
            if (battery < 0.4)
                return Brushes.DarkRed;
            if (battery < 0.6)
                return Brushes.DarkRed;
            return Brushes.Green;
        }
        public object ConvertBack(object value, Type targetType, object parrameter, CultureInfo culture)
        {
            throw new Exception();
        }



    }
}
