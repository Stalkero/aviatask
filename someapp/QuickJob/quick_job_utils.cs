using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace someapp.QuickJob
{
    internal class quick_job_utils
    {
        public class job_info
        {
           // public string job_name { get; set; }
           // public string job_description { get; set; }
            public double job_distance { get; set; }
            public string start_ICAO { get; set; }
            public string end_ICAO { get; set; }
            //public int weight { get; set; }
        }

        public List<job_info> jobs = new List<job_info>();
        Random random = new Random();


        //Improve it then
        public void generateJobAirport(string startICAO, int distance, float startLat, float startLon)
        {
            MessageBox.Show("Searching");

            string fileName = "db/airports.csv";

            var startLoc = new GeoCoordinate(startLat, startLon);

            foreach (var line in File.ReadLines(fileName))
            {
                var columns = line.Split('\t');

                var endLoc = new GeoCoordinate(double.Parse(columns[15]), double.Parse(columns[16]));
                var calculatedDistance = startLoc.GetDistanceTo(endLoc);

               // MessageBox.Show($"{columns[1]} {calculatedDistance.ToString()}");


                if (calculatedDistance < distance * 1852 && columns[1] != startICAO)
                {

                    job_info job = new job_info
                    {
                        job_distance = calculatedDistance,
                        start_ICAO = startICAO,
                        end_ICAO = columns[1]
                    };

                    jobs.Add(job);
                }


            }
            MessageBox.Show("Found");




        }
            

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

    }
}
