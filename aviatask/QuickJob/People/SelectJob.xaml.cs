using Aviatask.CreateAccount;
using Aviatask.Settings;
using CefSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;

namespace Aviatask.QuickJob.People
{
    /// <summary>
    /// Interaction logic for selectJob.xaml
    /// </summary>
    public partial class SelectJob : Page
    {
        public string PilotUsername { get; set; }
        public string PilotName { get; set; }
        public string PilotSurname { get; set; }
        public string selectedJobID { get; set; }
        public double selectedJobDistance { get; set; }
        public class selectedMapIcao
        {
           public double lat { get; set; }
           public double lon { get; set; }
        }


        public static bool SelectJobClose { get; set; }
        public bool resized;

        public static int tablet_size { get; set; }



        selectedMapIcao selectedMapIcaoStart = new selectedMapIcao();
        selectedMapIcao selectedMapIcaoEnd = new selectedMapIcao();

        JobGen.People people_Job_Gen = new JobGen.People();
        JobGen.Cargo cargo_Job_Gen = new JobGen.Cargo();

        JobGen.Utils jobList = new JobGen.Utils();

        public void ThreadedUIUpdates()
        {
            while (!SelectJobClose)
            {

                Application.Current.Dispatcher.Invoke(() =>
                {

                    if (tablet_size == 0)
                    {
                        txt_Jobs_Label.FontSize = 20;
                        txt_Jobs_Label.Margin = new Thickness(0, 0, 0,10);

                        txt_JobID_label.FontSize = 16;
                        txt_JobType_label.FontSize = 16;
                        txt_Dep_label.FontSize = 16;
                        txt_Arr_label.FontSize = 16;
                        txt_Dist_label.FontSize = 16;
                        txt_Desc_label.FontSize = 16;
                        txt_Weight_label.FontSize = 16;
                        txt_Map_label.FontSize = 16;

                        gridd.Margin = new Thickness(0, 0, 0, 0);

                        scroll_Jobs.Width = 320;
                        panel_Jobs.Width = 320;

                        scroll_Jobs.Height = 933;

                        btn_GoBack.FontSize = 14;
                        btn_GoBack.Width = 320;
                        btn_GoBack.Margin = new Thickness(0, 0, 0, 0);

                        btn_acceptjob.FontSize = 14;
                        btn_acceptjob.Width = 460;
                        btn_acceptjob.Margin = new Thickness(0, 0, 0, 0);

                        

                        this.Width = 1920;
                        this.Height = 1016;




                    }
                    if (tablet_size == 1)
                    {
                        txt_Jobs_Label.FontSize = 16;
                        txt_Jobs_Label.VerticalAlignment = VerticalAlignment.Top;
                        
                        stack_1.VerticalAlignment = VerticalAlignment.Top;

                        gridd.Margin = new Thickness (0,-80,0,0);
                        btn_acceptjob.Margin = new Thickness(0, 0, 0, 0);
                        txt_JobID_label.FontSize = 13;
                        txt_JobType_label.FontSize = 13;
                        txt_Dep_label.FontSize = 13;
                        txt_Arr_label.FontSize = 13;
                        txt_Dist_label.FontSize = 13;
                        txt_Desc_label.FontSize = 13;
                        txt_Weight_label.FontSize = 13;
                        txt_Map_label.FontSize = 13;


                        scroll_Jobs.Width = 266;
                        panel_Jobs.Width = 266;

                        scroll_Jobs.Height = 750;

                        btn_GoBack.FontSize = 11;
                        btn_GoBack.Margin = new Thickness(0, 10, 0, 0);
                        btn_GoBack.VerticalAlignment = VerticalAlignment.Bottom; 
                        btn_GoBack.HorizontalAlignment = HorizontalAlignment.Center;

                        btn_acceptjob.FontSize = 11;
                        btn_acceptjob.Width = 460;



                        this.Width = 1600;
                        this.Height = 848;
                    }
                });


            }
        }


        public void ThreadedReloadBrowser()
        {
            while (true)
            {
                MessageBox.Show(tablet_size.ToString());
                if (tablet_size == 0)
                {
                    //Browser part
                    string html = "<!doctype html>" +
                        "<html lang=\"en\"><head><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/openlayers/4.6.4/ol.css\" type=\"text/css\"><style></style>" +
                        "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/openlayers/4.6.4/ol.js\"></script><title>OpenLayers example</title></head><body><div id=\"map\" class=\"map\"></div><script type=\"text/javascript\">" +
                         "var map = new ol.Map({target: 'map',layers:[new ol.layer.Tile({source: new ol.source.OSM()})],view: new ol.View({center: ol.proj." +
                         $"fromLonLat([{selectedMapIcaoStart.lon}, {selectedMapIcaoStart.lat}])" +
                            ",zoom: 5})});" +
                            $"var lonlat = ol.proj.fromLonLat([{selectedMapIcaoStart.lon}, {selectedMapIcaoStart.lat}]);      " +
                                $"var location2 = ol.proj.fromLonLat([{selectedMapIcaoEnd.lon}, {selectedMapIcaoEnd.lat}]);" +
                        "var linie2style = [\r\n\t\t\t\t// linestring\r\n\t\t\t\tnew ol.style.Style({\r\n\t\t\t\t  stroke: new ol.style.Stroke({\r\n\t\t\t\t\tcolor: '#d12710',\r\n\t\t\t\t\twidth: 3\r\n\t\t\t\t  })\r\n\t\t\t\t})\r\n\t\t\t  ];\r\n\t\t\t  \t\t\t\r\n\t\t\tvar linie2 = new ol.layer.Vector({\r\n\t\t\t\t\tsource: new ol.source.Vector({\r\n\t\t\t\t\tfeatures: [new ol.Feature({\r\n\t\t\t\t\t\tgeometry: new ol.geom.LineString([lonlat, location2]),\r\n\t\t\t\t\t\tname: 'Line',\r\n\t\t\t\t\t})]\r\n\t\t\t\t})\r\n\t\t\t});\r\n\t\t\t\r\n\t\t\tlinie2.setStyle(linie2style);\r\n\t\t\tmap.addLayer(linie2);\r\n      \r\n    </script>\r\n  </body>\r\n</html>";
                    browser.LoadHtml(html);
                }
                if (tablet_size == 1)
                {
                    
                    string html = "<!doctype html>" +
                        "<html lang=\"en\"><head><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/openlayers/4.6.4/ol.css\" type=\"text/css\"><style></style>" +
                        "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/openlayers/4.6.4/ol.js\"></script><title>OpenLayers example</title></head><body><div id=\"map\" class=\"map\"></div><script type=\"text/javascript\">" +
                         "var map = new ol.Map({target: 'map',layers:[new ol.layer.Tile({source: new ol.source.OSM()})],view: new ol.View({center: ol.proj." +
                         $"fromLonLat([{selectedMapIcaoStart.lon}, {selectedMapIcaoStart.lat}])" +
                            ",zoom: 5})});" +
                            $"var lonlat = ol.proj.fromLonLat([{selectedMapIcaoStart.lon}, {selectedMapIcaoStart.lat}]);      " +
                                $"var location2 = ol.proj.fromLonLat([{selectedMapIcaoEnd.lon}, {selectedMapIcaoEnd.lat}]);" +
                        "var linie2style = [\r\n\t\t\t\t// linestring\r\n\t\t\t\tnew ol.style.Style({\r\n\t\t\t\t  stroke: new ol.style.Stroke({\r\n\t\t\t\t\tcolor: '#d12710',\r\n\t\t\t\t\twidth: 3\r\n\t\t\t\t  })\r\n\t\t\t\t})\r\n\t\t\t  ];\r\n\t\t\t  \t\t\t\r\n\t\t\tvar linie2 = new ol.layer.Vector({\r\n\t\t\t\t\tsource: new ol.source.Vector({\r\n\t\t\t\t\tfeatures: [new ol.Feature({\r\n\t\t\t\t\t\tgeometry: new ol.geom.LineString([lonlat, location2]),\r\n\t\t\t\t\t\tname: 'Line',\r\n\t\t\t\t\t})]\r\n\t\t\t\t})\r\n\t\t\t});\r\n\t\t\t\r\n\t\t\tlinie2.setStyle(linie2style);\r\n\t\t\tmap.addLayer(linie2);\r\n      \r\n    </script>\r\n  </body>\r\n</html>";
                    browser.LoadHtml(html);
                   
                }
                break;
            }


        }


        public SelectJob(string username, string name, string surname)
        {
            
            PilotUsername = username;
            PilotName = name;
            PilotSurname = surname;

            InitializeComponent();



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
                    button.Name = "_" + (jobID + 1) + "_btn_job";
                    button.Margin = new Thickness(0, 10, 0, 10);
                    button.VerticalAlignment = VerticalAlignment.Center;
                    button.HorizontalAlignment = HorizontalAlignment.Stretch;
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

            selectedMapIcaoStart.lon = jobList.AllJobs[jobIndex].startLon;
            selectedMapIcaoStart.lat = jobList.AllJobs[jobIndex].startLat;

            selectedMapIcaoEnd.lon = jobList.AllJobs[jobIndex].endLon;
            selectedMapIcaoEnd.lat = jobList.AllJobs[jobIndex].endLat;









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
                NavigationService.Content = new QuickJob.People.Job(PilotUsername, PilotName, PilotSurname, textbox_StartIcao.Text, textbox_endIcao.Text, selectedJobID, textbox_jobName.Text, selectedJobDistance, textbox_weight.Text, textBox_jobDesc.Text);
            }
            else if (textbox_StartIcao.Text != "Select job" && textbox_jobID.Text.StartsWith("CT"))
            {
                NavigationService.Content = new QuickJob.Cargo(PilotUsername, PilotName, PilotSurname, textbox_StartIcao.Text, textbox_endIcao.Text, selectedJobID, textbox_jobName.Text, selectedJobDistance, textbox_weight.Text, textBox_jobDesc.Text);
            }
            else
                MessageBox.Show("Select job from left");


        }

        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.main_menu.MainMenuClose = false;
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Thread UIupdates = new Thread(new ThreadStart(ThreadedUIUpdates));

            UIupdates.Start();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MessageBox.Show("Resized");
            Thread BrowserUpdates = new Thread(new ThreadStart(ThreadedReloadBrowser));
            BrowserUpdates.Start();
        }
    }
}
