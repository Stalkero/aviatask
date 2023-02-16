using someapp.QuickJob;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace someapp.JobGen
{
    internal class utils
    {
        private static Random randomm = new Random();


        public List<quick_job_classes.job_info> AllJobs = new List<quick_job_classes.job_info>();


        static GeoCoordinate GetLocationAtDistance(GeoCoordinate startLocation, double distanceNM, double bearing)
        {
            var distance = distanceNM * 1.852;
            var radius = 6371.0; // Earth's radius in kilometers
            var latitude = startLocation.Latitude * Math.PI / 180.0;
            var longitude = startLocation.Longitude * Math.PI / 180.0;
            var bearingRad = bearing * Math.PI / 180.0;

            var newLatitude = Math.Asin(Math.Sin(latitude) * Math.Cos(distance / radius) +
                                        Math.Cos(latitude) * Math.Sin(distance / radius) * Math.Cos(bearingRad));

            var newLongitude = longitude + Math.Atan2(Math.Sin(bearingRad) * Math.Sin(distance / radius) * Math.Cos(latitude),
                                                      Math.Cos(distance / radius) - Math.Sin(latitude) * Math.Sin(newLatitude));

            newLatitude = newLatitude * 180.0 / Math.PI;
            newLongitude = newLongitude * 180.0 / Math.PI;

            return new GeoCoordinate(newLatitude, newLongitude);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[randomm.Next(s.Length)]).ToArray());
        }
    }
}
