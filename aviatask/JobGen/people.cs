using Geolocation;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Windows;

namespace Aviatask.JobGen
{
    internal class People
    {
        Random random = new Random();
        Random random2 = new Random();


        public List<string> job_names_people_transport_generate_aiport = new List<string>();
        public string selectedAirportJobName;
        public List <JobGen.Classes.job_info> jobsPeople = new List<Classes.job_info>();



        private void generateJobNamePeopleTransport()
        {
            job_names_people_transport_generate_aiport.Add("Commercial Airline Services");
            job_names_people_transport_generate_aiport.Add("Private Jet Charters");
            job_names_people_transport_generate_aiport.Add("Executive and VIP Transportation");

            int randomJobNameIndex = random.Next(0, 3);

            selectedAirportJobName = job_names_people_transport_generate_aiport[randomJobNameIndex].ToString();
        }

        public void generateJobAirportPeopleTransport(string startICAO, int distance, float startLat, float startLon)
        {

            string fileName = "db/airports.csv";

            var startLoc = new Coordinate(startLat, startLon);

            foreach (var line in File.ReadLines(fileName))
            {
                var columns = line.Split('\t');

                var endLoc = new Coordinate(double.Parse(columns[15]), double.Parse(columns[16]));
                var calculatedDistance = GeoCalculator.GetDistance(startLoc, endLoc,2,DistanceUnit.NauticalMiles);

                if (calculatedDistance > 0 && calculatedDistance < distance && columns[1] != startICAO)
                {
                    generateJobNamePeopleTransport();
                    string jobDesc;
                    int paxCount = 0;

                    if (distance <= 150)
                        paxCount = random2.Next(3, 7);
                    if (distance > 150)
                        paxCount = random2.Next(7, 20);
                    if (distance > 170)
                        paxCount = random2.Next(20, 150);





                    switch (selectedAirportJobName)
                    {
                        case "Commercial Airline Services":
                            jobDesc = "The Commercial Airline " +
                                "Services Specialist is responsible for ensuring a smooth " +
                                "and efficient operation of all commercial airline services. " +
                                "This includes overseeing ground handling services, passenger and cargo handling, " +
                                "and flight dispatch operations.";
                            break;
                        case "Private Jet Charters":
                            jobDesc = "The Private Jet Charters Specialist is responsible for managing the end-to-end " +
                                "operations of private jet charters, including flight planning, aircraft selection, and customer service.";
                            break;
                        case "Executive and VIP Transportation":
                            jobDesc = "The Executive and VIP Transportation Specialist is responsible for managing high-end transportation " +
                                "services for executives, VIPs, and other high-profile clients. The ideal candidate has strong customer service skills, " +
                                "is highly organized, and has experience in luxury transportation";
                            break;
                        default:
                            jobDesc = "No description";
                            break;
                    }


                    JobGen.Classes.job_info job = new Classes.job_info()
                    {
                        id = $"PT_{Utils.RandomString(12)}",
                        job_name = selectedAirportJobName,
                        job_distance = calculatedDistance,
                        start_ICAO = startICAO,
                        end_ICAO = columns[1],
                        description = jobDesc,
                        type = "peopleTransport",
                        weight = paxCount,
                        startLat = startLat,
                        startLon = startLon,
                        endLat = double.Parse(columns[15]),
                        endLon = double.Parse(columns[16])
                    };

                    jobsPeople.Add(job);
                }
            }

        }
    }
}
