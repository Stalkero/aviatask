using Aviatask.CreateAccount;
using CefSharp;
using Microsoft.Toolkit.Uwp.Notifications;
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
using Windows.Devices.Enumeration;
using Windows.UI.Core;

namespace Aviatask.MainMenu
{
    /// <summary>
    /// Interaction logic for main_menu_page.xaml
    /// </summary>
    public partial class main_menu
    {
        public static bool MainMenuClose { get; set; }

        public static int tablet_size { get; set; }
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


        public void ThreadedUIUpdates()
        {
            while (!MainMenuClose)
            {

                Application.Current.Dispatcher.Invoke(() =>
                {


                    DateTime date = DateTime.Now;

                    string clockMinute = date.Minute.ToString();
                    string clockSecond = date.Second.ToString();

                    string dateMonth = date.Month.ToString();
                    string dateDay = date.Day.ToString();

                    if (date.Minute >= 0 && date.Minute <= 9)
                        clockMinute = $"0{date.Minute.ToString()}";

                    if (date.Second >= 0 && date.Second <= 9)
                        clockSecond = $"0{date.Second.ToString()}";

                    if (date.Day >= 0 && date.Day <= 9)
                        dateDay = $"0{date.Day.ToString()}";
                    if (date.Month >= 0 && date.Month <= 9)
                        dateMonth = $"0{date.Month.ToString()}";



                    txt_Date.Text = $"{date.Year}/{dateMonth}/{dateDay}";
                    txt_Clock.Text = $"{date.Hour}:{clockMinute}:{clockSecond}";
                   


                    if (tablet_size == 0)
                    {              
                        textbox_welcome.FontSize = 20;
                        textbox_welcome.Margin = new Thickness(72,0,0,0);

                        textbox_account_last_job.FontSize = 20;
                        textbox_account_last_job.Margin = new Thickness(0, 0, 72, 0);

                        txt_Clock.FontSize = 60;
                        txt_Clock.Margin = new Thickness(0, 100, 0, 0);

                        txt_Date.FontSize = 35;
                        txt_Date.Margin = new Thickness(0,162,0,0);

                        this.Width = 1920;
                        this.Height = 1016;
                        
                        
                        button_quick_job.Width = 300;
                        button_my_company.Width = 300;
                        button_my_flights.Width = 300;
                        button_my_settings.Width = 300;

                        button_quick_job.Height = 300;
                        button_my_company.Height = 300;
                        button_my_flights.Height = 300;
                        button_my_settings.Height = 300;

                        stack_apps.Margin = new Thickness(0,80,0,0);

                    }
                    if (tablet_size == 1)
                    {
                        textbox_welcome.FontSize = 16;
                        textbox_welcome.Margin = new Thickness(60, -80, 0, 0);

                        textbox_account_last_job.FontSize = 16;
                        textbox_account_last_job.Margin = new Thickness(0, -80, 60, 0);

                        txt_Clock.FontSize = 50;
                        txt_Clock.Margin = new Thickness(0, 10, 0, 0);

                        txt_Date.FontSize = 29;
                        txt_Date.Margin = new Thickness(0, 62, 0, 0);

                        this.Width = 1600;
                        this.Height = 848;

                        button_quick_job.Width = 250;
                        button_my_company.Width = 250;
                        button_my_flights.Width = 250;
                        button_my_settings.Width = 250;

                        button_quick_job.Height = 250;
                        button_my_company.Height = 250;
                        button_my_flights.Height = 250;
                        button_my_settings.Height = 250;

                        stack_apps.Margin = new Thickness(0, 37.5, 0, 0);
                    }
                });


            }
        }


        PilotDetails pilot = new PilotDetails();
        public main_menu(string username, string name, string surname)
        {
            InitializeComponent();            

            pilot.Username = username;
            pilot.Name = name;
            pilot.Surname = surname;
            textbox_welcome.Text = $"Welcome {name} {surname}";


            string path = $"profiles/{username}";
            string logbookFile = path + "/logbook.json";

            if (Directory.Exists(path) && File.Exists(logbookFile))
            {
                string encryptedLogbookFileText = File.ReadAllText(logbookFile);
                string decryptedLogBookFileText = create_account_utils.DecryptText(encryptedLogbookFileText, "5up3r4dv4nc3dC0mpl3xP455w0rdCr34t3dBy5t4lk3r0Th4tS4y5FuckUJKs0Much");

                List<LogBook.Classes.flightHistory> flights = JsonConvert.DeserializeObject<List<LogBook.Classes.flightHistory>>(decryptedLogBookFileText);

                if (flights[0].jobID == "FILL")
                    textbox_account_last_job.Text = "";
                else
                {
                    int lastJobId = flights.Count - 1;

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
            MainMenuClose = true;
            Settings.Main.SettingsClose = false;
            NavigationService.Content = new Settings.Main(pilot.Username, pilot.Name, pilot.Surname);
        }

        private void button_quick_job_Click(object sender, RoutedEventArgs e)
        {
            MainMenuClose = true;
            NavigationService.Content = new QuickJob.People.SelectJob(pilot.Username, pilot.Name, pilot.Surname);

        }

        private void button_my_flights_Click(object sender, RoutedEventArgs e)
        {
            MainMenuClose = true;
            LogBook.Logbook.LogBookClose = false;
            NavigationService.Content = new LogBook.Logbook(pilot.Username, pilot.Name, pilot.Surname);
        }

        private void UiPage_Loaded(object sender, RoutedEventArgs e)
        {
            Thread UIupdates = new Thread(new ThreadStart(ThreadedUIUpdates));
            UIupdates.Start();
        }
    }
}
