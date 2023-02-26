using Aviatask.CreateAccount;
using Aviatask.Settings;
using CefSharp;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aviatask.QuickJob
{
    /// <summary>
    /// Interaction logic for SelectJob_Page.xaml
    /// </summary>
    public partial class SelectJob
    {
        public string PilotUsername { get; set; }
        public string PilotName { get; set; }
        public string PilotSurname { get; set; }
        public string selectedJobID { get; set; }
        public double selectedJobDistance { get; set; }

        JobGen.People people_Job_Gen = new JobGen.People();
        JobGen.Cargo cargo_Job_Gen = new JobGen.Cargo();

        JobGen.Utils jobList = new JobGen.Utils();



        public SelectJob(string username, string name, string surname)
        {
            InitializeComponent();
            PilotUsername = username;
            PilotName = name;
            PilotSurname = surname;

            string path = $"profiles/{username}";

            if (File.Exists(path + "/profile.json") && File.Exists(path + "/quickjob_settings.json"))
            {
                string profileFilePath = $"profiles/{username}/profile.json";
                string quickJobGenFilePath = $"profiles/{username}/quickjob_settings.json";

                string encryptedTextProfile = File.ReadAllText(profileFilePath);
                string decryptedTextProfile = create_account_utils.DecryptText(encryptedTextProfile, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");

                string encryptedTextJobGen = File.ReadAllText(quickJobGenFilePath);
                string decryptedTextJobGen = create_account_utils.DecryptText(encryptedTextJobGen, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");


                account_classes.PilotDetails pilot = JsonConvert.DeserializeObject<account_classes.PilotDetails>(decryptedTextProfile);
                settings_generation_classes.quick_job_generation quickJobSettings = JsonConvert.DeserializeObject<settings_generation_classes.quick_job_generation>(decryptedTextJobGen);

                //Here add new jobs

                for (int i = 0; i < quickJobSettings.AirportPeopleIterations; i++)
                    people_Job_Gen.generateJobAirportPeopleTransport(pilot.ICAO, int.Parse(quickJobSettings.maxDistance.ToString()), pilot.LatDec, pilot.LongDec);

                for (int i = 0; i < quickJobSettings.CargoJobGenIterations; i++)
                    cargo_Job_Gen.generateJobAirportCargo(pilot.ICAO, int.Parse(quickJobSettings.maxDistance.ToString()), pilot.LatDec, pilot.LongDec, pilot.Type, pilot.Username, pilot.Name, pilot.Surname);


                //Each job needs to be added to all jobs list

                foreach (var job in people_Job_Gen.jobsPeople)
                {
                    jobList.AllJobs.Add(job);
                }

                foreach (var job in cargo_Job_Gen.jobsCargo)
                {
                    jobList.AllJobs.Add(job);
                }

                int jobID = 0;

                foreach (var job in jobList.AllJobs)
                {

                    Wpf.Ui.Controls.Button button = new Wpf.Ui.Controls.Button();
                    button.Content = job.id;
                    button.MinWidth = 300;
                    button.Name = "_" + (jobID + 1) + "_btn_job";
                    button.Margin = new Thickness(0, 10, 0, 10);
                    button.VerticalAlignment = VerticalAlignment.Center;
                    button.Click += button_job_Click;

                    panel_Jobs.Children.Add(button);
                    jobID++;

                }

            }


        }

        private void button_job_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            int firstUnderscoreIndex = clickedButton.Name.IndexOf("_");
            int secondUnderscoreIndex = clickedButton.Name.IndexOf("_", firstUnderscoreIndex + 1);
            int jobIndex = (int.Parse(clickedButton.Name.Substring(firstUnderscoreIndex + 1, secondUnderscoreIndex - firstUnderscoreIndex - 1))) - 1;


            textbox_StartIcao.Text = jobList.AllJobs[jobIndex].start_ICAO;
            textbox_endIcao.Text = jobList.AllJobs[jobIndex].end_ICAO;
            textbox_jobName.Text = jobList.AllJobs[jobIndex].job_name;
            textbox_jobID.Text = jobList.AllJobs[jobIndex].id;
            textbox_distanceNM.Text = $"{jobList.AllJobs[jobIndex].job_distance} nm";
            textbox_distanceKM.Text = $"{Math.Round(jobList.AllJobs[jobIndex].job_distance * 1.852, 2)} km";


            textBox_jobDesc.Text = jobList.AllJobs[jobIndex].description;
            selectedJobID = jobList.AllJobs[jobIndex].id;
            selectedJobDistance = jobList.AllJobs[jobIndex].job_distance;


            string html = "<!doctype html>" +
            "<html lang=\"en\"><head><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/openlayers/4.6.4/ol.css\" type=\"text/css\"><style>.map {height: 885px;width: 860px;}</style>" +
            "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/openlayers/4.6.4/ol.js\"></script><title>OpenLayers example</title></head><body><div id=\"map\" class=\"map\"></div><script type=\"text/javascript\">" +
            "var map = new ol.Map({target: 'map',layers:[new ol.layer.Tile({source: new ol.source.OSM()})],view: new ol.View({center: ol.proj." +
            $"fromLonLat([{jobList.AllJobs[jobIndex].startLon}, {jobList.AllJobs[jobIndex].startLat}])" +
            ",zoom: 5})});" +
            $"var lonlat = ol.proj.fromLonLat([{jobList.AllJobs[jobIndex].startLon}, {jobList.AllJobs[jobIndex].startLat}]);      " +
            $"var location2 = ol.proj.fromLonLat([{jobList.AllJobs[jobIndex].endLon}, {jobList.AllJobs[jobIndex].endLat}]);" +
            "var linie2style = [\r\n\t\t\t\t// linestring\r\n\t\t\t\tnew ol.style.Style({\r\n\t\t\t\t  stroke: new ol.style.Stroke({\r\n\t\t\t\t\tcolor: '#d12710',\r\n\t\t\t\t\twidth: 3\r\n\t\t\t\t  })\r\n\t\t\t\t})\r\n\t\t\t  ];\r\n\t\t\t  \t\t\t\r\n\t\t\tvar linie2 = new ol.layer.Vector({\r\n\t\t\t\t\tsource: new ol.source.Vector({\r\n\t\t\t\t\tfeatures: [new ol.Feature({\r\n\t\t\t\t\t\tgeometry: new ol.geom.LineString([lonlat, location2]),\r\n\t\t\t\t\t\tname: 'Line',\r\n\t\t\t\t\t})]\r\n\t\t\t\t})\r\n\t\t\t});\r\n\t\t\t\r\n\t\t\tlinie2.setStyle(linie2style);\r\n\t\t\tmap.addLayer(linie2);\r\n      \r\n    </script>\r\n  </body>\r\n</html>";
            browser.LoadHtml(html);





            if (jobList.AllJobs[jobIndex].type == "peopleTransport")
            {
                textbox_weight.Text = $"PAX {jobList.AllJobs[jobIndex].weight}";
            }

            if (jobList.AllJobs[jobIndex].type == "cargoTransport")
            {
                textbox_weight.Text = $"{jobList.AllJobs[jobIndex].weight} kg";
            }




        }

        private void btn_acceptjob_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_StartIcao.Text != "Select job" && textbox_jobID.Text.StartsWith("PT"))
            {
               // QuickJob.People peopleTranportJob = new QuickJob.People(PilotUsername, PilotName, PilotSurname, textbox_StartIcao.Text, textbox_endIcao.Text, selectedJobID, textbox_jobName.Text, selectedJobDistance, textbox_weight.Text, textBox_jobDesc.Text);
                //peopleTranportJob.Show();
                //this.Close();
            }
            else if (textbox_StartIcao.Text != "Select job" && textbox_jobID.Text.StartsWith("CT"))
            {
                QuickJob.Cargo cargoTranportJob = new QuickJob.Cargo(PilotUsername, PilotName, PilotSurname, textbox_StartIcao.Text, textbox_endIcao.Text, selectedJobID, textbox_jobName.Text, selectedJobDistance, textbox_weight.Text, textBox_jobDesc.Text);
               // cargoTranportJob.Show();
                //this.Close();
            }
            else
                MessageBox.Show("Select job from left");


        }

        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
