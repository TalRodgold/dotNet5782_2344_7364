using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// class for location
        /// </summary>
        public class Location
        {
            public double Longitude { set; get; }
            public double Latitude { set; get; }
            public override string ToString()
            {
                return $" \n\tLongitude = {sexagesimal(Longitude, 'N')}: \n\tLatitude = {sexagesimal(Latitude, 'E')} \n";
            }
            public Location(double longitude, double latitude) // constructor
            {
                Longitude = longitude;
                Latitude = latitude;
            }
            internal static string sexagesimal(double decimalDegrees, Char c) // convert double to sexagesimal
            {
                #region//convert double to sexagesimal
                // caluclate seconds
                double latDegrees = decimalDegrees;
                int latSeconds = (int)Math.Round(latDegrees * 60 * 60);
                // calculate degrees
                double latDegreesWithFraction = decimalDegrees;
                int degrees = (int)latDegreesWithFraction;
                // calculate minutes
                double fractionalDegrees = latDegrees - degrees;
                double minutesWithFraction = 60 * fractionalDegrees;
                int minutes = (int)minutesWithFraction;
                // caluclate seconds with fraction
                double fractionalMinutes = minutesWithFraction - minutes;
                double secondsWithFraction = 60 * fractionalMinutes;
                return $"{degrees}°{minutes}'{string.Format("{0:F3}", secondsWithFraction)}\"{c}"; // return string of cordinents
                #endregion
            }
            public string LongitudeInSexa() // return longitude in sexagesimal
            {
                return sexagesimal(Longitude, 'N');
            }
            public string LatitudeInSexa() // return latitude in sexagesimal
            {
                return sexagesimal(Latitude, 'E');
            }
        }
    }
    
}
