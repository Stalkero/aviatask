using Newtonsoft.Json;
using someapp.CreateAccount;
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

        public struct PilotDetails
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Country { get; set; }
            public string Type { get; set; }
            public string ICAO { get; set; }
            public float LatDec { get; set; }
            public float LongDec { get; set; }
        }

        quick_job_utils job_Utils = new quick_job_utils();
        debug_params.debug_tools debug_Tools = new debug_params.debug_tools();


        public quick_job_window(string username)
        {
            InitializeComponent();


            string path = $"profiles/{username}";

            if (Directory.Exists(path) && File.Exists(path + "/profile.json"))
            {
                string profileFilePath = $"profiles/{username}/profile.json";
                string profileFileDecrytpedPath = $"profiles/{username}/profileDecrypted.json";

                string encryptedText = File.ReadAllText(profileFilePath);
                string decryptedText = create_account_utils.DecryptText(encryptedText, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");


                PilotDetails pilot = JsonConvert.DeserializeObject<PilotDetails>(decryptedText);
                

                job_Utils.generateJobAirport(pilot.ICAO,500,pilot.LatDec,pilot.LongDec);


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

            


        }
    }
}
