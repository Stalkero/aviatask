using CefSharp;
using CefSharp.Wpf;
using Newtonsoft.Json;
using someapp.CreateAccount;
using someapp.Settings;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using static someapp.debug_params;

namespace someapp.QuickJob
{
    /// <summary>
    /// Interaction logic for quick_job_window.xaml
    /// </summary>
    public partial class quick_job_window
    {

        quick_job_utils job_Utils = new quick_job_utils();
        debug_params.debug_tools debug_Tools = new debug_params.debug_tools();


        public quick_job_window(string username)
        {
            InitializeComponent();


            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("disable-web-security", "1");
            settings.CefCommandLineArgs.Add("disable-xss-auditor", "1");




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


                for (int i = 0; i < quickJobSettings.AirportPeopleIterations; i++)
                    job_Utils.generateJobAirportPeopleTransport(pilot.ICAO, int.Parse(quickJobSettings.maxDistance.ToString()), pilot.LatDec, pilot.LongDec);


                int jobID = 0;

                foreach(var job in job_Utils.jobs)
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

            if (debug_Tools.debugMsg)
                MessageBox.Show("Button " + jobIndex + " was clicked");


            textbox_StartIcao.Text = job_Utils.jobs[jobIndex].start_ICAO;
            textbox_endIcao.Text = job_Utils.jobs[jobIndex].end_ICAO;
            textbox_jobName.Text = job_Utils.jobs[jobIndex].job_name;
            textbox_distanceNM.Text = $"{Math.Round((job_Utils.jobs[jobIndex].job_distance / 1852)),2} nm";
            textbox_distanceKM.Text = $"{Math.Round((job_Utils.jobs[jobIndex].job_distance / 1000),2)} km";
            textBox_jobDesc.Text = job_Utils.jobs[jobIndex].description;

            string html = "<!doctype html>" +
            "<html lang=\"en\"><head><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/openlayers/4.6.4/ol.css\" type=\"text/css\"><style>.map {height: 850px;width: 850px;}</style>" +
            "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/openlayers/4.6.4/ol.js\"></script><title>OpenLayers example</title></head><body><div id=\"map\" class=\"map\"></div><script type=\"text/javascript\">" +
            "var map = new ol.Map({target: 'map',layers:[new ol.layer.Tile({source: new ol.source.OSM()})],view: new ol.View({center: ol.proj." +
            $"fromLonLat([{job_Utils.jobs[jobIndex].startLon}, {job_Utils.jobs[jobIndex].startLat}])" +
            ",zoom: 5})});" +
            $"var lonlat = ol.proj.fromLonLat([{job_Utils.jobs[jobIndex].startLon}, {job_Utils.jobs[jobIndex].startLat}]);      " +
            $"var location2 = ol.proj.fromLonLat([{job_Utils.jobs[jobIndex].endLon}, {job_Utils.jobs[jobIndex].endLat}]);" +
            "var linie2style = [\r\n\t\t\t\t// linestring\r\n\t\t\t\tnew ol.style.Style({\r\n\t\t\t\t  stroke: new ol.style.Stroke({\r\n\t\t\t\t\tcolor: '#d12710',\r\n\t\t\t\t\twidth: 3\r\n\t\t\t\t  })\r\n\t\t\t\t})\r\n\t\t\t  ];\r\n\t\t\t  \t\t\t\r\n\t\t\tvar linie2 = new ol.layer.Vector({\r\n\t\t\t\t\tsource: new ol.source.Vector({\r\n\t\t\t\t\tfeatures: [new ol.Feature({\r\n\t\t\t\t\t\tgeometry: new ol.geom.LineString([lonlat, location2]),\r\n\t\t\t\t\t\tname: 'Line',\r\n\t\t\t\t\t})]\r\n\t\t\t\t})\r\n\t\t\t});\r\n\t\t\t\r\n\t\t\tlinie2.setStyle(linie2style);\r\n\t\t\tmap.addLayer(linie2);\r\n      \r\n    </script>\r\n  </body>\r\n</html>";
            browser.LoadHtml(html);





            if (job_Utils.jobs[jobIndex].type == "peopleTransport")
            {
                textbox_weight.Text = $"PAX {job_Utils.jobs[jobIndex].weight}";
            }

            


        }
    }
}
