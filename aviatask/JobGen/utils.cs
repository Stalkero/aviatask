using Aviatask.QuickJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geolocation;

namespace Aviatask.JobGen
{
    internal class Utils
    {
        private static Random randomm = new Random();


        public List<JobGen.Classes.job_info> AllJobs = new List<Classes.job_info>();

        static Coordinate GetLocationAtDistance(Coordinate startLocation, double distanceNM, double bearing)
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

            return new Coordinate(newLatitude, newLongitude);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[randomm.Next(s.Length)]).ToArray());
        }
    }
}
