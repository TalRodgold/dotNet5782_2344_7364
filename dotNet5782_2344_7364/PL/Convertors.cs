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
using System.Runtime.CompilerServices;

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
            return (double)value / 100;

        }
    }
    internal class BatteryToConverter : IValueConverter
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public object Convert(object value, Type targetType, object parrameter, CultureInfo culture)
        {
            double battery = (double)value;
            if (battery < 0.1)
                return Brushes.DarkRed;
            if (battery < 0.2)
                return Brushes.Red;
            if (battery < 0.4)
                return Brushes.Yellow;
            if (battery < 0.6)
                return Brushes.GreenYellow;
            return Brushes.Green;
        }
        public object ConvertBack(object value, Type targetType, object parrameter, CultureInfo culture)
        {
            throw new Exception();
        }
       
    }

    internal class ConvertComboBoxToVisability : IValueConverter
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public object Convert(object value, Type targetType, object parrameter, CultureInfo culture)
        {
            if (value.Equals(1))
            {
                return Visibility.Visible;
            }
            return Visibility.Hidden;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public object ConvertBack(object value, Type targetType, object parrameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }
    internal class ConvertLatitudeToSexa : IValueConverter
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public object Convert(object value, Type targetType, object parrameter, CultureInfo culture)
        {
            return ((PO.Location)value).LatitudeInSexa();//BO
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public object ConvertBack(object value, Type targetType, object parrameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }
    internal class ConvertLongitudeToSexa : IValueConverter
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public object Convert(object value, Type targetType, object parrameter, CultureInfo culture)
        {
            return ((PO.Location)value).LongitudeInSexa();//BO  
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public object ConvertBack(object value, Type targetType, object parrameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }
    
}
