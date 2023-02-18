using someapp.QuickJob;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static someapp.debug_params;

namespace someapp.JobGen
{
    internal class cargo
    {
        debug_params.debug_tools debug_Tools = new debug_params.debug_tools();

        public List<string> job_cargo_types = new List<string>();
        public List<JobGen.classes.job_info> jobsCargo = new List<classes.job_info>();
        public string selectedCargoType { get; set; }

        Random random = new Random();
        Random random2 = new Random();
        private static Random randomm = new Random();


        private void generateCargoType(string type)
        {

            if (type == "Helicopters")
            {
                job_cargo_types.Add("Medical Supplies");
                job_cargo_types.Add("Human Organs");
                job_cargo_types.Add("Blood and Blood Products");
                job_cargo_types.Add("Pharmaceuticals");
                job_cargo_types.Add("Vaccines");
                job_cargo_types.Add("Medical Equipment");
                job_cargo_types.Add("Medical Waste");
                job_cargo_types.Add("Emergency Food and Water Supplies");
                job_cargo_types.Add("Disaster Relief Supplies");
                job_cargo_types.Add("Search and Rescue Equipment");
                job_cargo_types.Add("Communications Equipment");
                job_cargo_types.Add("Firefighting Equipment");
                job_cargo_types.Add("Construction Materials");
                job_cargo_types.Add("Logging Equipment");
                job_cargo_types.Add("Mining Equipment");
                job_cargo_types.Add("Surveying Equipment");
                job_cargo_types.Add("Heavy Machinery");
                job_cargo_types.Add("Generators");
                job_cargo_types.Add("Industrial Parts and Equipment");
                job_cargo_types.Add("Tools and Hardware");
                job_cargo_types.Add("Fuel and Oil");
                job_cargo_types.Add("Agriculture Supplies");
                job_cargo_types.Add("Livestock and Poultry");
                job_cargo_types.Add("Fish and Seafood");
                job_cargo_types.Add("Fruits and Vegetables");
                job_cargo_types.Add("Baked Goods and Canned Foods");
                job_cargo_types.Add("Dairy Products");
                job_cargo_types.Add("Meat and Poultry Products");
                job_cargo_types.Add("Beverages");
                job_cargo_types.Add("Snack Foods");
                job_cargo_types.Add("Frozen Foods");
                job_cargo_types.Add("Dry Goods");
                job_cargo_types.Add("Clothing and Textiles");
                job_cargo_types.Add("Footwear");
                job_cargo_types.Add("Household Goods");
                job_cargo_types.Add("Personal Care Products");
                job_cargo_types.Add("Electronics");
                job_cargo_types.Add("Automotive Parts");
                job_cargo_types.Add("Aerospace Parts");
                job_cargo_types.Add("Defense Supplies");
                job_cargo_types.Add("Musical Instruments");
                job_cargo_types.Add("Artwork");
                job_cargo_types.Add("Jewelry");
                job_cargo_types.Add("Books and Magazines");
                job_cargo_types.Add("Toys and Games");
                job_cargo_types.Add("Sporting Goods");
                job_cargo_types.Add("Camping and Outdoor Gear");
                job_cargo_types.Add("Pet Supplies");
                job_cargo_types.Add("Office Supplies");
                job_cargo_types.Add("School Supplies");
                job_cargo_types.Add("Scientific Equipment");
                job_cargo_types.Add("Photography Equipment");
                job_cargo_types.Add("Entertainment Equipment");
                job_cargo_types.Add("Religious Supplies");
                job_cargo_types.Add("Military Equipment");
                job_cargo_types.Add("Musical Equipment");
                job_cargo_types.Add("Personal Watercraft");
                job_cargo_types.Add("Recreational Vehicles");
                job_cargo_types.Add("Small Boats");
                job_cargo_types.Add("Trailers and Hitches");
                job_cargo_types.Add("Consumer Goods");
                job_cargo_types.Add("Computer and Technology Equipment");
                job_cargo_types.Add("Home Improvement Supplies");
                job_cargo_types.Add("Machinery Parts");
                job_cargo_types.Add("Electrical Supplies");
                job_cargo_types.Add("Plumbing Supplies");
                job_cargo_types.Add("Building Materials");
                job_cargo_types.Add("Cleaning Supplies");
                job_cargo_types.Add("Car and Truck Tires");
                job_cargo_types.Add("Automotive Batteries");
                job_cargo_types.Add("Solar Panels");
                job_cargo_types.Add("Wind Turbine Components");
                job_cargo_types.Add("Large Appliances");
                job_cargo_types.Add("Small Appliances");
                job_cargo_types.Add("Home Furnishings");
                job_cargo_types.Add("Exercise Equipment");
                job_cargo_types.Add("Gymnastics Equipment");
                job_cargo_types.Add("Office Furniture");
                job_cargo_types.Add("Construction Equipment");
                job_cargo_types.Add("Drilling Equipment");
                job_cargo_types.Add("Oil and Gas Equipment");
                job_cargo_types.Add("Offshore Equipment");
                job_cargo_types.Add("Scientific Instruments");
                job_cargo_types.Add("Laboratory Equipment");
                job_cargo_types.Add("Art Supplies");
                job_cargo_types.Add("Craft Supplies");
                job_cargo_types.Add("Construction Tools");
                job_cargo_types.Add("Garden Supplies");
                job_cargo_types.Add("Lumber");
                job_cargo_types.Add("Stone and Masonry");
                job_cargo_types.Add("Concrete Products");
                job_cargo_types.Add("Steel Products");
                job_cargo_types.Add("Automotive Accessories");
                job_cargo_types.Add("Electronic Accessories");
                job_cargo_types.Add("Computer Accessories");
                job_cargo_types.Add("Plumbing Fixtures");
                job_cargo_types.Add("Heating and Cooling Equipment");
                job_cargo_types.Add("Lighting Equipment");
                job_cargo_types.Add("Kitchen Appliances");
                job_cargo_types.Add("Outdoor Furniture");
            }


            int randomJobNameIndex = random.Next(0, 99);
            selectedCargoType = job_cargo_types[randomJobNameIndex];
        }



        public void generateJobAirportCargo(string startICAO, int distance, float startLat, float startLon, string type)
        {

            if (debug_Tools.jobGenInfo)
                MessageBox.Show("Searching");

            string fileName = "db/airports.csv";

            var startLoc = new GeoCoordinate(startLat, startLon);

            foreach (var line in File.ReadLines(fileName))
            {
                var columns = line.Split('\t');

                var endLoc = new GeoCoordinate(double.Parse(columns[15]), double.Parse(columns[16]));
                var calculatedDistance = startLoc.GetDistanceTo(endLoc);



                if (calculatedDistance > 0 && calculatedDistance < distance * 1852 && columns[1] != startICAO)
                {
                    if (debug_Tools.jobGenInfo)
                        MessageBox.Show(calculatedDistance.ToString());




                    generateCargoType(type);



                    string jobDesc = $"Transport of selected goods.\n----------------\nTransport of: {selectedCargoType}";

                    int weight = 0;
                    if (type == "Helicopter")
                        weight = randomm.Next(40, 3000);
                    if (type == "Plane")
                        weight = randomm.Next(40, 5000);


                    //quick_job_classes.job_info job = new quick_job_classes.job_info()
                    JobGen.classes.job_info job = new classes.job_info()
                    {


                        id = $"CT_{utils.RandomString(12)}",
                        job_name = "Cargo transport",
                        job_distance = calculatedDistance,
                        start_ICAO = startICAO,
                        end_ICAO = columns[1],
                        description = jobDesc,
                        type = "cargoTransport",
                        weight = weight,
                        startLat = startLat,
                        startLon = startLon,
                        endLat = double.Parse(columns[15]),
                        endLon = double.Parse(columns[16])
                    };

                    jobsCargo.Add(job);
                }
            }
            if (debug_Tools.jobGenInfo)
                MessageBox.Show("Found");




        }
    }
}
