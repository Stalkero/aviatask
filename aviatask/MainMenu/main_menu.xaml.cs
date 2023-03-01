﻿using Aviatask.CreateAccount;
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
using Windows.Devices.Enumeration;
using Windows.UI.Core;

namespace Aviatask.MainMenu
{
    /// <summary>
    /// Interaction logic for main_menu_page.xaml
    /// </summary>
    public partial class main_menu 
    {
        bool closing = false;

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

        public void ThreadedClock()
        {
            while(!closing)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DateTime date = DateTime.Now;
                    txt_Date.Text = $"{date.Year}/{date.Month}/{date.Day}";

                    txt_Clock.Text = $"{date.Hour}:{date.Minute}:{date.Second}";
                });

                Thread.Sleep(500);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    DateTime date = DateTime.Now;

                    txt_Clock.Text = $"{date.Hour} {date.Minute} {date.Second}";
                });

            }
        }


        PilotDetails pilot = new PilotDetails();
        public main_menu(string username, string name, string surname)
        {
            InitializeComponent();

            Thread thread = new Thread(new ThreadStart(ThreadedClock));
            thread.Start();



            pilot.Username = username;
            pilot.Name = name;
            pilot.Surname = surname;

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

            Settings_window.Settings settings = new Settings_window.Settings(pilot.Username, pilot.Name, pilot.Surname);
            settings.Show();


        }

        private void button_quick_job_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Content = new QuickJob.SelectJob(pilot.Username, pilot.Name, pilot.Surname);

        }

        private void button_my_flights_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new LogBook.Logbook(pilot.Username, pilot.Name, pilot.Surname);

        }

        private void UiPage_Unloaded(object sender, RoutedEventArgs e)
        {
            closing = true;
        }
    }
}
