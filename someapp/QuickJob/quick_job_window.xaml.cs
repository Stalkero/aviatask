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
                quick_job_utils job_Utils = new quick_job_utils();

                job_Utils.generateJobAirport(pilot.ICAO,1000,pilot.LatDec,pilot.LongDec);

                foreach(var job in job_Utils.jobs)
                {
                    combo_Job_selector.Items.Add($"{job.start_ICAO} - {job.end_ICAO}");
                }


            }


        }
    }
}


//test commit