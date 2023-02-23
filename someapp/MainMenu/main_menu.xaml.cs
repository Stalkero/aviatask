using CefSharp.DevTools.Overlay;
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

namespace someapp.MainMenu
{
    /// <summary>
    /// Interaction logic for main_menu.xaml
    /// </summary>
    public partial class main_menu 
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
        }


        public struct CompanyDetails
        {
            public string Name { get; set; }
            public int Balance { get; set; }
            public string CountryOfResidence { get; set; }


        }

        PilotDetails pilot = new PilotDetails();

        public main_menu(string username,string name, string surname)
        {
            InitializeComponent();

            pilot.Username= username;
            pilot.Name= name;
            pilot.Surname= surname;

            textbox_welcome.Text = $"Welcome, {name} {surname}";

            string path = $"profiles/{username}";
            string logbookFile = path + "/logbook.json";

            if (Directory.Exists(path) && File.Exists(logbookFile))
            {
                string encryptedLogbookFileText = File.ReadAllText(logbookFile);
                string decryptedLogBookFileText = create_account_utils.DecryptText(encryptedLogbookFileText, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");

                List<LogBook.classes.flightHistory> flights = JsonConvert.DeserializeObject<List<LogBook.classes.flightHistory>>(decryptedLogBookFileText);

                if (flights[0].jobID == "FILL")
                    textbox_account_last_job.Text = "";
                else
                {
                    int lastJobId =  flights.Count -1;

                    textbox_account_last_job.Text = $"Last job: {flights[lastJobId].jobName} to {flights[lastJobId].endICAO}";
                }


            }
            else
            {
                MessageBox.Show("Profile Corrupted. Directory or logbook file missing");
            }
        }

        private void button_my_settings_Click(object sender, RoutedEventArgs e)
        {
            
            Settings_window.settings settings= new Settings_window.settings(pilot.Username,pilot.Name,pilot.Surname);
            settings.Show();

            this.Close();

        }

        private void button_quick_job_Click(object sender, RoutedEventArgs e)
        {
            QuickJob.selectJob jobSelection = new QuickJob.selectJob(pilot.Username, pilot.Name, pilot.Surname);
            this.Close();
            jobSelection.Show();
        }

        private void button_my_flights_Click(object sender, RoutedEventArgs e)
        {
            LogBook.logbook_window logbook = new LogBook.logbook_window(pilot.Username,pilot.Name,pilot.Surname);
            this.Close();
            logbook.Show();

        }
    }
}
